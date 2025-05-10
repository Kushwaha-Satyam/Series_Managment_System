using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeriesManagment.BAL;
using SeriesManagment.DAL;

namespace SeriesManagment
{
    public partial class AddSeries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["q"] != null)
                {
                    string decrypted = Crypto.Decrypt(Request.QueryString["q"]);
                    string[] parts = decrypted.Split('&');

                    string mode = parts[0].Split('=')[1];
                    ViewState["Mode"] = mode;

                    if (mode == "E" && parts.Length > 1)
                    {
                        int seriesId = Convert.ToInt32(parts[1].Split('=')[1]);
                        ViewState["SeriesId"] = seriesId;
                        LoadSeriesData(seriesId);
                    }
                }
                else
                {
                    ViewState["Mode"] = "A";
                }
            }
            else if(IsPostBack)
            {
                if (Request.QueryString["q"] != null)
                {
                    string decrypted = Crypto.Decrypt(Request.QueryString["q"]);
                    string[] parts = decrypted.Split('&');

                    string mode = parts[0].Split('=')[1];
                    ViewState["Mode"] = mode;

                    if (mode == "E" && parts.Length > 1)
                    {
                        int seriesId = Convert.ToInt32(parts[1].Split('=')[1]);
                        ViewState["SeriesId"] = seriesId;
                        LoadSeriesData(seriesId);
                    }
                }
                else
                {
                    ViewState["Mode"] = "A";
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SeriesBAL obj = new SeriesBAL();

            // text fields
            obj.SeriesName = txtName.Text;
            obj.Year = txtYear.Text;
            obj.Description = txtDescription.Text;

            // dropDowns
            obj.SeriesType = Convert.ToInt32(ddlType.SelectedValue);
            obj.SeriesStatus = ddlSeriesStatus.SelectedValue;
            obj.MatchStatus = ddlMatchStatus.SelectedValue;
            obj.MatchFormat = ddlMatchFormat.SelectedValue;
            obj.SeriesMatchType = Convert.ToInt32(ddlMatchType.SelectedValue);
            obj.Gender = Convert.ToInt32(ddlGender.SelectedValue);
            obj.TrophyType = Convert.ToInt32(ddlTrophyType.SelectedValue);
            obj.IsActive = ddlIsActive.SelectedValue == "1";

            // dates 
            obj.SeriesStartDate = DateTime.TryParse(txtStartDate.Text, out DateTime sDate) ? sDate : DateTime.MinValue;
            obj.SeriesEndDate = DateTime.TryParse(txtEndDate.Text, out DateTime eDate) ? eDate : DateTime.MinValue;

            SeriesDAL dal = new SeriesDAL();
            string mode = ViewState["Mode"].ToString();

            if (mode == "A")
            {
                int result = dal.InsertSeries(obj);
                if(result == -1)
                {
                    lblMessage.Text = "Series added successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    lblMessage.Text = "Error while adding series.";
                }
            }
            else if (mode == "E")
            {
                obj.SeriesId = Convert.ToInt32(ViewState["SeriesId"]);
                int result = dal.UpdateSeries(obj);
                if (result == -1)
                {
                    lblMessage.Text = "Series Updated successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    lblMessage.Text = "Error while Updating series.";
                }
            }
        }

        private void LoadSeriesData(int seriesId)
        {
            SeriesDAL dal = new SeriesDAL();
            DataTable dt = dal.SearchSeries(null, null, null, null, null);
            DataRow[] rows = dt.Select("SeriesId = " + seriesId);

            if (rows.Length > 0)
            {
                DataRow row = rows[0];

                //populate the fileds with the data
                txtName.Text = row["SeriesName"].ToString();
                ddlType.SelectedValue = row["SeriesType"].ToString();
                ddlSeriesStatus.SelectedValue = row["SeriesStatus"].ToString();
                ddlMatchStatus.SelectedValue = row["MatchStatus"].ToString();
                ddlMatchFormat.SelectedValue = row["MatchFormat"].ToString();
                ddlMatchType.SelectedValue = row["SeriesMatchType"].ToString();
                ddlGender.SelectedValue = row["Gender"].ToString();
                txtYear.Text = row["Year"].ToString();
                ddlTrophyType.SelectedValue = row["TrophyType"].ToString();
                txtStartDate.Text = Convert.ToDateTime(row["SeriesStartDate"]).ToString("yyyy-MM-dd");
                txtEndDate.Text = Convert.ToDateTime(row["SeriesEndDate"]).ToString("yyyy-MM-dd");
                ddlIsActive.SelectedValue = row["IsActive"].ToString();
                txtDescription.Text = row["Description"].ToString();
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

    }
}