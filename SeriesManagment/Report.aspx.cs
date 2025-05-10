using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeriesManagment
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindYearList();
            }
        }

        private void BindYearList()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT Year FROM tbl_Series ORDER BY Year DESC", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                lstYears.Items.Clear();
                while (reader.Read())
                {
                    lstYears.Items.Add(new ListItem(reader["Year"].ToString(), reader["Year"].ToString()));
                }
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string selectedYears = string.Join(",", lstYears.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));

            if (string.IsNullOrEmpty(selectedYears))
            {
                ltReportTable.Text = "<p style='text-align:center; color:red;'>Please select at least one year.</p>";
                return;
            }

            DataTable dt = GetReportData(selectedYears);
            if (dt.Rows.Count > 0)
            {
                ltReportTable.Text = GenerateDynamicTable(dt);
            }
            else
            {
                ltReportTable.Text = "<p style='text-align:center;'>No data found.</p>";
            }
        }

        private DataTable GetReportData(string yearsCsv)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("prc_Report_MatchFormatYearGender", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Years", yearsCsv);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        private string GenerateDynamicTable(DataTable dt)
        {
            var years = new SortedSet<string>();
            var matchFormats = new SortedSet<string>();
            var genders = new Dictionary<int, string> { { 1, "Men" }, { 2, "Women" }, { 3, "Other" } };

            foreach (DataRow row in dt.Rows)
            {
                years.Add(row["Year"].ToString());
                matchFormats.Add(row["MatchFormat"].ToString());
            }

            // Count data lookup: Format + Year + Gender → Count
            Dictionary<string, int> dataMap = new Dictionary<string, int>();
            foreach (DataRow row in dt.Rows)
            {
                string key = $"{row["MatchFormat"]}_{row["Year"]}_{row["Gender"]}";
                dataMap[key] = Convert.ToInt32(row["Total"]);
            }

            // Begin table
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='report-table'>");

            // First header row for years
            sb.Append("<tr><th rowspan='2'>Match Format</th>");
            foreach (var year in years)
            {
                sb.Append($"<th colspan='3'>Year {year}</th>");
            }
            sb.Append("</tr>");

            // Second header row for genders
            foreach (var _ in years)
            {
                sb.Append("<th>Men</th><th>Women</th><th>Other</th>");
            }
            sb.Append("</tr>");

            // Rows for each match format
            foreach (var format in matchFormats)
            {
                sb.Append($"<tr><td>{format}</td>");
                foreach (var year in years)
                {
                    foreach (var gender in genders.Keys)
                    {
                        string key = $"{format}_{year}_{gender}";
                        int count = dataMap.ContainsKey(key) ? dataMap[key] : 0;
                        sb.Append($"<td>{count}</td>");
                    }
                }
                sb.Append("</tr>");
            }

            // total row
            sb.Append("<tr style='background-color:#cce5cc; font-weight:bold;'><td><b>Total</b></td>");
            foreach (var year in years)
            {
                foreach (var gender in genders.Keys)
                {
                    int total = 0;
                    foreach (var format in matchFormats)
                    {
                        string key = $"{format}_{year}_{gender}";
                        if (dataMap.ContainsKey(key))
                            total += dataMap[key];
                    }
                    sb.Append($"<td><b>{total}</b></td>");
                }
            }
            sb.Append("</tr>");

            sb.Append("</table>");
            return sb.ToString();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageSeries.aspx");
        }
    }
}