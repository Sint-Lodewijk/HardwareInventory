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
using System.Text;
using Toestellenbeheer.Models;

namespace Toestellenbeheer.Overview
{
    public partial class license_overview : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
        protected void grvLicense_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                // String strLicenseCode = grvLicense.DataKeys[e.RowIndex].Value.ToString();
                String strLicenseCode = grvLicenseCode.DataKeys[e.RowIndex].Value.ToString();
                var deleteLicense = new Models.License(strLicenseCode);
                if (deleteLicense.IsRemoved())
                {
                    grvLicenseCode.DataBind();
                }
                else
                {
                    ShowMessage("Exeption occured!");
                }
            }
            catch (MySqlException ex)
            {
                ShowMessage(ex.Message);
            }
        }
        void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<scriptlanguage = 'javascript' > alert('" + msg + "');</ script > ");
        }
        protected void grvLicenseCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grvLicenseCode.SelectedRow;
            String strLicenseCode = grvLicenseCode.SelectedDataKey.Value.ToString();
            var licenseID = new License(strLicenseCode);
            int intLicenseID =  licenseID.GetLicenseID("licenseCode");
            getCorrespondingPeople(intLicenseID);
            getCorrespondingHardware(intLicenseID);
            var detailModalShow = new JSUtility("modalHardwareLicense");
            detailModalShow.ModalShow(this);
            // lblCountPeople.Text = "This license has been assigned to: " + grvLicenseAssignedPeople.Rows.Count.ToString() + " people and ";
            //  lblCountHardware.Text = grvLicenseAssignedHardware.Rows.Count.ToString() + " hardware";
        }
        protected void getCorrespondingPeople(int LicenseID)
        {
            var licenseCorresponding = new License();
            grvLicenseAssignedPeople.DataSource = licenseCorresponding.ReturnLicensePeople(LicenseID);
            grvLicenseAssignedPeople.DataBind();
        }
        protected void getCorrespondingHardware(int LicenseID)
        {
            var licenseCorresponding = new License();
            grvLicenseAssignedHardware.DataSource = licenseCorresponding.ReturnLicenseHardware(LicenseID);
            grvLicenseAssignedHardware.DataBind();
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvLicenseCode, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void grvLicenseAssignedPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void getLicenseCorresponding()
        {
            String strLicenseCode = grvLicenseCode.SelectedDataKey.Value.ToString();
            try
            {
                mysqlConnectie.Open();
                MySqlCommand getLicenseCorresponding = new MySqlCommand("SELECT * FROM hardware where license", mysqlConnectie);
            }
            catch (MySqlException ex)
            {
                lblProblem.Text = ex.ToString();
            }
        }
        protected void grvLicenseAssignedPeople_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (IsPostBack)
            {
                try
                {
                    GridView gridview = (GridView)sender;
                    mysqlConnectie.Open();
                    // String strLicenseCode = grvLicense.DataKeys[e.RowIndex].Value.ToString();
                    String strLicenseEventID = gridview.DataKeys[e.RowIndex].Value.ToString();
                    MySqlCommand deleteLicenseAPeople = new MySqlCommand("Delete From licenseHandler where licenseEventID='" + strLicenseEventID + "'", mysqlConnectie);
                    deleteLicenseAPeople.ExecuteNonQuery();
                    deleteLicenseAPeople.Dispose();
                    mysqlConnectie.Close();
                    /*
                    var licenseID = new License(strLicenseCode);
                    int intLicenseID = licenseID.GetLicenseID("licenseCode");
                    getCorrespondingPeople(intLicenseID);*/

                }
                catch (MySqlException ex)
                {
                    ShowMessage(ex.Message);
                }
            }
        }
        protected void grvLicenseAssignedHardware_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (IsPostBack)
            {
                try
                {
                    GridView gridview = (GridView)sender;
                    mysqlConnectie.Open();
                    // String strLicenseCode = grvLicense.DataKeys[e.RowIndex].Value.ToString();
                    String LicenseEventID = gridview.DataKeys[e.RowIndex].Value.ToString();
                    MySqlCommand deleteLicenseAHardware = new MySqlCommand("Delete From licenseHandler where licenseEventID = @licenseEventID", mysqlConnectie);
                    deleteLicenseAHardware.Parameters.AddWithValue("@licenseEventID", LicenseEventID);
                    deleteLicenseAHardware.ExecuteNonQuery();
                    mysqlConnectie.Close();
                    /*
                    var licenseID = new License(strLicenseCode);
                    int intLicenseID = licenseID.GetLicenseID("licenseCode");
                    getCorrespondingHardware(intLicenseID);*/
                }
                catch (MySqlException ex)
                {
                    ShowMessage(ex.Message);
                }
            }
        }

        protected void btnAssignCode_Click(object sender, EventArgs e)
        {

        }


        protected void grvLicenseFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvLicenseFile, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        protected void grvLicenseFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            String strLicenseCode = grvLicenseFile.SelectedDataKey.Value.ToString();
            var licenseID = new License(strLicenseCode);
            int intLicenseID = licenseID.GetLicenseID("licenseFileLocation");
            getCorrespondingPeople(intLicenseID);
            getCorrespondingHardware(intLicenseID);
            var detailModalShow = new JSUtility("modalHardwareLicense");
            detailModalShow.ModalShow(this);
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/UserUploads/License/") + (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath));
            Response.WriteFile(filePath);
        }
    }
}