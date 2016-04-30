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
                grvLicenseUnassignedPeople.DataSource = dt;
                grvLicenseUnassignedPeople.DataBind();
                bindLicenseGRVLicense();
            }
        }
        protected void bindLicenseGRVLicense()
        {
            try
            {
                MySqlCommand bindToGrid = new MySqlCommand("SELECT  licenseName 'License name', licenseCode 'License code' FROM license", mysqlConnectie);
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
        protected void grvLicense_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPanel.Visible = true;
        }
        protected void grvLicenseUnassignedHardware_RowDeleting(object sender, EventArgs e)
        {
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvLicense, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void grvLicenseUnassignedPeople_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        protected void grvLicenseUnassignedPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void btnAPeople_Click(object sender, EventArgs e)
        {
            PeoplePanel.Visible = true;
            PeoplePopUP.Show();
        }
        protected void btnAHardware_Click(object sender, EventArgs e)
        {
        }
    }
}