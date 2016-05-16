using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Toestellenbeheer.Models;
namespace Toestellenbeheer.Manage
{
    public partial class assign_license : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var people = new Models.User();
                DataTable dt = people.ReturnDataTable();
                licenseOverviewGridPeople.DataSource = dt;
                licenseOverviewGridPeople.DataBind();
            }
        }

        protected void grvLicense_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPanel.Visible = true;
        }
        protected void grvLicenseUnassignedHardware_RowDeleting(object sender, EventArgs e)
        {
        }
        protected void Hardware_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvHardwareLicenseSelect, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvLicenseCode, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void grvLicenseUnassignedPeople_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        protected void grvLicenseUnassignedPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void grvPreRender(object sender, EventArgs e)
        {
            var gridview = sender as GridView;
            var Sort = new GridViewPreRender(gridview);
            Sort.SetHeader();
        }
        protected void AssignLicense(string license, string type)
        {
            String nameAD = licenseOverviewGridPeople.SelectedDataKey["Domain Name"].ToString();
            Models.User getUserID = new Models.User(nameAD);
            int userID = getUserID.ReturnEventID();
            var licenseID = new License(license);
            int intLicenseID = licenseID.GetLicenseID(type);
            var assignLicenseToPeople = new LicenseHandler(userID, intLicenseID);
            assignLicenseToPeople.AssignLicenseToPeople();
            var ShowSuccessAlert = new JSUtility();
            ShowSuccessAlert.ShowAlert(this, "Successfully add the license and assigned to people.", "alert-success");

        }
        protected void assignLicenseToPeople(object sender, EventArgs e)
        {
            try
            {
                if (grvLicenseCode.SelectedDataKey.Value == null)
                {
                    String strLicense = grvLicenseFile.SelectedDataKey.Value.ToString();
                    AssignLicense(strLicense, "licenseFileLocation");
                }
                else
                {
                    String strLicense = grvLicenseCode.SelectedDataKey.Value.ToString();
                    AssignLicense(strLicense, "licenseCode");

                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number.ToString() == "1062")
                {
                    var ShowSuccessAlert = new JSUtility();
                    ShowSuccessAlert.ShowAlert(this, "<strong>Warning</strong>, " + ex.Message, "alert-success");
                }

            }
        }
        protected void getUserFromAD(GridView grvLicenseOverviewPeople)
        {
            Models.User get = new Models.User();
            DataTable dt = get.ReturnDataTable();
            grvLicenseOverviewPeople.DataSource = dt;
            grvLicenseOverviewPeople.DataBind();
        }
        protected void assignToSelectedHardwareSearch_Click(object sender, EventArgs e)
        {
            String strInternalNr = grvHardwareLicenseSelect.SelectedDataKey["internalNr"].ToString();
            String strSerialCode = grvHardwareLicenseSelect.SelectedDataKey["serialNr"].ToString();
            if (grvLicenseCode.SelectedDataKey["licenseCode"] == null)
            {
                var License = new License(grvLicenseFile.SelectedDataKey["licenseFileLocation"].ToString());
                int intLicenseID = License.GetLicenseID("licenseFileLocation");
                var licenseToHardware = new LicenseHandler(strInternalNr, strSerialCode, intLicenseID);
                licenseToHardware.AssignLicenseToHardware();
                var ShowSuccessAlert = new JSUtility();
                ShowSuccessAlert.ShowAlert(this, "Congratulations! The license is assigned to the hardware with internal nr: " + strInternalNr, "alert-success");

            }
            else
            {
                var License = new License(grvLicenseCode.SelectedDataKey["licenseCode"].ToString());
                int intLicenseID = License.GetLicenseID("licenseCode");
                var licenseToHardware = new LicenseHandler(strInternalNr, strSerialCode, intLicenseID);
                licenseToHardware.AssignLicenseToHardware();
                var ShowSuccessAlert = new JSUtility();
                ShowSuccessAlert.ShowAlert(this, "Congratulations! The license is assigned to the hardware with internal nr: " + strInternalNr, "alert-success");


            }
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            this.Search();
        }
        //Search and bind the entrys
        private void Search()
        {

            String strSearchItem = drpSearchItem.SelectedValue.ToString();
            String strSearchText = txtSearch.Text.Trim();
            if (strSearchText != "")
            {
                HardwareLicense.FilterExpression = "" + strSearchItem + " LIKE '%" + strSearchText + "%'";
                grvHardwareLicenseSelect.DataBind();
            }
            else
            {
                HardwareLicense.FilterExpression = "1 = 1";
                grvHardwareLicenseSelect.DataBind();
            }
        }



        protected void SearchBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(licenseOverviewGridSearch, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void grvHardwareLicenseSelect_PageIndexChanged(object sender, EventArgs e)
        {
            hardwarePanel.Visible = true;
        }
        protected void licenseOverviewGridPeople_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            licenseOverviewGridPeople.PageIndex = e.NewPageIndex;
            getUserFromAD(licenseOverviewGridPeople);
        }
        protected void PeopleBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(licenseOverviewGridPeople, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
    }
}