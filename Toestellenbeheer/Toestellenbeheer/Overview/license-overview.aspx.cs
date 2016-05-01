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
        protected void grvLicense_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grvLicenseCode.SelectedRow;
            String strLicenseCode = grvLicenseCode.SelectedDataKey.Value.ToString();
            getCorrespondingPeople(strLicenseCode);
            getCorrespondingHardware(strLicenseCode);
            lblCountPeople.Text = "This license has been assigned to: " + grvLicenseAssignedPeople.Rows.Count.ToString() + " people and ";
            lblCountHardware.Text = grvLicenseAssignedHardware.Rows.Count.ToString() + " hardware";
        }
        protected void getCorrespondingPeople(String strLicenseCode)
        {
            var licenseCorresponding = new Models.License();
            grvLicenseAssignedPeople.DataSource = licenseCorresponding.ReturnLicensePeople(strLicenseCode, false);
            grvLicenseAssignedPeople.DataBind();
        }
        protected void getCorrespondingHardware(String strLicenseCode)
        {
           /* mysqlConnectie.Open();
            MySqlCommand getCorrespondingHardware = new MySqlCommand("SELECT serialNr, internalNr, licenseCode FROM licenseHandler WHERE serialNr IS NOT NULL AND internalNr IS NOT NULL AND licenseCode ='" + strLicenseCode + "'", mysqlConnectie);
            MySqlDataAdapter adpa = new MySqlDataAdapter(getCorrespondingHardware);
            getCorrespondingHardware.ExecuteNonQuery();
            getCorrespondingHardware.Dispose();
            DataSet ds = new DataSet();
            adpa.Fill(ds);
            grvLicenseAssignedHardware.DataSource = ds;
            grvLicenseAssignedHardware.DataBind();
            mysqlConnectie.Close();
            */
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
                    mysqlConnectie.Open();
                    // String strLicenseCode = grvLicense.DataKeys[e.RowIndex].Value.ToString();
                    String strLicenseCode = grvLicenseCode.SelectedDataKey.Value.ToString();
                    String strLicenseEventID = grvLicenseAssignedPeople.DataKeys[e.RowIndex].Value.ToString();
                    MySqlCommand deleteLicenseAPeople = new MySqlCommand("Delete From licenseHandler where licenseEventID='" + strLicenseEventID + "'", mysqlConnectie);
                    deleteLicenseAPeople.ExecuteNonQuery();
                    deleteLicenseAPeople.Dispose();
                    mysqlConnectie.Close();
                    getCorrespondingPeople(strLicenseCode);
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
                    mysqlConnectie.Open();
                    // String strLicenseCode = grvLicense.DataKeys[e.RowIndex].Value.ToString();
                    String strLicenseCode = grvLicenseCode.SelectedDataKey.Value.ToString();
                    String strInternalNr = grvLicenseAssignedHardware.DataKeys[e.RowIndex].Value.ToString();
                    MySqlCommand deleteLicenseAHardware = new MySqlCommand("Delete From licenseHandler where internalNr='" +
                        strInternalNr + "' AND licenseCode = '" + strLicenseCode + "'", mysqlConnectie);
                    deleteLicenseAHardware.ExecuteNonQuery();
                    deleteLicenseAHardware.Dispose();
                    mysqlConnectie.Close();
                    getCorrespondingHardware(strLicenseCode);
                }
                catch (MySqlException ex)
                {
                    ShowMessage(ex.Message);
                }
            }
        }
    }
}