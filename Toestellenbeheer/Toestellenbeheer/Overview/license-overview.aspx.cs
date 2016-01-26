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
                bindLicenseGRVLicense();
            }
        }
        protected void bindLicenseGRVLicense()
        {
            try
            {
                MySqlCommand bindToGrid = new MySqlCommand("SELECT licenseName 'License name', licenseCode 'License code' FROM license", mysqlConnectie);
                mysqlConnectie.Open();
                MySqlDataAdapter adpa = new MySqlDataAdapter(bindToGrid);
                bindToGrid.ExecuteNonQuery();
                bindToGrid.Dispose();
                DataSet ds = new DataSet();
                adpa.Fill(ds);
                grvLicense.DataSource = ds;
                grvLicense.DataBind();
                mysqlConnectie.Close();
            }
            catch (MySqlException ex)
            {
                lblProblem.Text = ex.ToString();
            }
        }
        protected void grvLicense_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                mysqlConnectie.Open();
                // String strLicenseCode = grvLicense.DataKeys[e.RowIndex].Value.ToString();
                String strLicenseCode = grvLicense.DataKeys[e.RowIndex].Value.ToString();

                MySqlCommand deleteLicense = new MySqlCommand("Delete From license where licenseCode='" + strLicenseCode + "'", mysqlConnectie);
                deleteLicense.ExecuteNonQuery();
                deleteLicense.Dispose();
                grvLicense.EditIndex = -1;
                mysqlConnectie.Close();
                bindLicenseGRVLicense();

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
            GridViewRow row = grvLicense.SelectedRow;
            String strLicenseCode = grvLicense.SelectedDataKey.Value.ToString();
            getCorrespondingPeople(strLicenseCode);
            getCorrespondingHardware(strLicenseCode);
           
        }

        protected void getCorrespondingPeople(String strLicenseCode)
        {
            mysqlConnectie.Open();
            MySqlCommand getCorrespondingPeople = new MySqlCommand("SELECT licenseHandler.licenseCode, nameAD from licenseHandler join people on licenseHandler.eventID = people.eventID where licenseHandler.licenseCode = '" + strLicenseCode + "'", mysqlConnectie);
            MySqlDataAdapter adpa = new MySqlDataAdapter(getCorrespondingPeople);
            getCorrespondingPeople.ExecuteNonQuery();
            getCorrespondingPeople.Dispose();
            DataSet ds = new DataSet();
            adpa.Fill(ds);
            grvLicenseAssignedPeople.DataSource = ds;
            grvLicenseAssignedPeople.DataBind();
            mysqlConnectie.Close();
        }
        protected void getCorrespondingHardware(String strLicenseCode)
        {
            mysqlConnectie.Open();
            MySqlCommand getCorrespondingHardware = new MySqlCommand("SELECT serialNr, internalNr, licenseCode FROM licenseHandler WHERE serialNr IS NOT NULL AND internalNr IS NOT NULL AND licenseCode ='" + strLicenseCode + "'", mysqlConnectie);
            MySqlDataAdapter adpa = new MySqlDataAdapter(getCorrespondingHardware);
            getCorrespondingHardware.ExecuteNonQuery();
            getCorrespondingHardware.Dispose();
            DataSet ds = new DataSet();
            adpa.Fill(ds);
            grvLicenseAssignedHardware.DataSource = ds;
            grvLicenseAssignedHardware.DataBind();
            mysqlConnectie.Close();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvLicense, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        protected void grvLicenseAssignedPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void getLicenseCorresponding()
        {
            String strLicenseCode = grvLicense.SelectedDataKey.Value.ToString();
            try
            {
                mysqlConnectie.Open();
                MySqlCommand getLicenseCorresponding = new MySqlCommand("SELECT * FROM hardware where license" , mysqlConnectie);
            }
            catch(MySqlException ex)
            {
                lblProblem.Text = ex.ToString();
            }
        }
    }



}