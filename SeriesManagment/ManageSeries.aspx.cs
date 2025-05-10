using SeriesManagment.BAL;
using SeriesManagment.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeriesManagment
{
    public partial class ManageSeries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // load all records when page opens
                BindSeriesData(); 
            }
            if (Request.QueryString["delete"] != null)
            {
                string decrypted = Crypto.Decrypt(Request.QueryString["delete"]);
                int seriesId = Convert.ToInt32(decrypted.Split('=')[1]);

                SeriesDAL dal = new SeriesDAL();
                dal.DeleteSeries(seriesId);

                Session["SuccessMessage"] = "Series deleted successfully!";
                Response.Redirect("ManageSeries.aspx");
            }
            if (Session["SuccessMessage"] != null)
            {
                lblMessage.Text = Session["SuccessMessage"].ToString();
                Session.Remove("SuccessMessage");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string encrypted = Crypto.Encrypt("Mode=A");
            Response.Redirect("AddSeries.aspx?q=" + encrypted);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindSeriesData();
        }
        private void BindSeriesData()
        {
            int? apiId = string.IsNullOrWhiteSpace(txtApiId.Text) ? (int?)null : Convert.ToInt32(txtApiId.Text);
            string name = txtName.Text.Trim();
            int? type = ddlType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlType.SelectedValue);
            DateTime? start = string.IsNullOrWhiteSpace(txtStartDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtStartDate.Text);
            DateTime? end = string.IsNullOrWhiteSpace(txtEndDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtEndDate.Text);

            SeriesDAL dal = new SeriesDAL();
            DataTable dt = dal.SearchSeries(apiId, name, type, start, end);

            // sorting
            string sortBy = ddlSortBy.SelectedValue;
            if (sortBy == "startasc")
                dt.DefaultView.Sort = "SeriesStartDate ASC";
            else if (sortBy == "startdesc")
                dt.DefaultView.Sort = "SeriesStartDate DESC";

            dt = dt.DefaultView.ToTable();

            if (dt.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<table id='seriesTable' class='display'>");
                sb.Append("<thead><tr>");
                sb.Append("<th>Delete Series</th><th>ID</th><th>Series Name</th><th>Series Type</th><th>Series Start date</th><th>Series End date</th>");
                sb.Append("</tr></thead><tbody>");

                foreach (DataRow row in dt.Rows)
                {
                    int sid = Convert.ToInt32(row["SeriesId"]);
                    string encrypted = Crypto.Encrypt("Mode=E&Sid=" + sid);
                    string encryptedDelete = Crypto.Encrypt("Sid=" + sid);

                    sb.Append("<tr>");
                    sb.Append($"<td><a href='ManageSeries.aspx?delete={encryptedDelete}' onclick='return confirm(\"Are you sure you want to delete this series?\");'>Delete</a></td>");
                    sb.Append($"<td>{row["SeriesApiId"]}</td>");
                    sb.Append($"<td><a href='AddSeries.aspx?q={encrypted}'>{row["SeriesName"]}</a></td>");
                    sb.Append($"<td>{GetSeriesTypeName(Convert.ToInt32(row["SeriesType"]))}</td>");
                    sb.Append($"<td>{Convert.ToDateTime(row["SeriesStartDate"]).ToShortDateString()}</td>");
                    sb.Append($"<td>{Convert.ToDateTime(row["SeriesEndDate"]).ToShortDateString()}</td>");
                    sb.Append("</tr>");
                }

                sb.Append("</tbody></table>");
                ltTable.Text = sb.ToString();
            }
            else
            {
                ltTable.Text = "<p style='text-align:center;'>No series found.</p>";
            }
        }

        private string GetSeriesTypeName(int typeId)
        {
            switch (typeId)
            {
                case 1: return "International";
                case 2: return "Domestic";
                case 3: return "Women";
                case 4: return "Mens";
                default: return "Unknown";
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageSeries.aspx");
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report.aspx");
        }

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    int? apiId = string.IsNullOrWhiteSpace(txtApiId.Text) ? (int?)null : Convert.ToInt32(txtApiId.Text);
        //    string name = txtName.Text.Trim();
        //    int? type = ddlType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlType.SelectedValue);
        //    DateTime? start = string.IsNullOrWhiteSpace(txtStartDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtStartDate.Text);
        //    DateTime? end = string.IsNullOrWhiteSpace(txtEndDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtEndDate.Text);

        //    SeriesDAL dal = new SeriesDAL();
        //    DataTable dt = dal.SearchSeries(apiId, name, type, start, end);

        //    //sorting 
        //    string sortBy = ddlSortBy.SelectedValue;

        //    if (sortBy == "startasc")
        //    {
        //        dt.DefaultView.Sort = "SeriesStartDate ASC";
        //    }
        //    else if (sortBy == "startdesc")
        //    {
        //        dt.DefaultView.Sort = "SeriesStartDate DESC";
        //    }
        //    // apply sort and convert back to DataTable
        //    dt = dt.DefaultView.ToTable(); 

        //    if (dt.Rows.Count > 0)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("<table id='seriesTable' class='display'>");
        //        sb.Append("<thead><tr>");
        //        sb.Append("<th>Delete Series</th><th>ID</th><th>Series Name</th><th>Series Type</th><th>Series Start date</th><th>Series End date</th>");
        //        sb.Append("</tr></thead><tbody>");

        //        foreach (DataRow row in dt.Rows)
        //        {
        //            int sid = Convert.ToInt32(row["SeriesId"]);
        //            string encrypted = Crypto.Encrypt("Mode=E&Sid=" + sid);
        //            string encryptedDelete = Crypto.Encrypt("Sid=" + sid);

        //            sb.Append("<tr>");
        //            sb.Append($"<td><a href='ManageSeries.aspx?delete={encryptedDelete}' onclick='return confirm(\"Are you sure you want to delete this series?\");'>Delete</a></td>");
        //            sb.Append($"<td>{row["SeriesApiId"]}</td>");
        //            sb.Append($"<td><a href='AddSeries.aspx?q={encrypted}'>{row["SeriesName"]}</a></td>");
        //            sb.Append($"<td>{GetSeriesTypeName(Convert.ToInt32(row["SeriesType"]))}</td>");
        //            sb.Append($"<td>{Convert.ToDateTime(row["SeriesStartDate"]).ToShortDateString()}</td>");
        //            sb.Append($"<td>{Convert.ToDateTime(row["SeriesEndDate"]).ToShortDateString()}</td>");
        //            sb.Append("</tr>");
        //        }

        //        sb.Append("</tbody></table>");
        //        ltTable.Text = sb.ToString();
        //    }
        //    else
        //    {
        //        ltTable.Text = "<p style='text-align:center;'>No series found.</p>";
        //    }
        //}
        //private string GetTrophyTypeName(int id)
        //{
        //    switch (id)
        //    {
        //        case 1: return "Asia Cup";
        //        case 2: return "ICC World Cup T20";
        //        default: return "Other";
        //    }
        //}

        //private string GetGenderName(int id)
        //{
        //    switch (id)
        //    {
        //        case 1: return "Mens";
        //        case 2: return "Womens";
        //        case 3: return "Other";
        //        default: return "Unknown";
        //    }
        //}

    }
}