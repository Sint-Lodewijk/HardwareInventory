using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Toestellenbeheer.Manage
{
    public partial class return_hardware : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getAssignedHardware();
            }
        }
        protected void getAssignedHardware()
        {
            try
            {
                mysqlConnectie.Open();
                MySqlCommand bindToGrid = new MySqlCommand("SELECT  DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'purchaseDate', typeNr, manufacturerName, serialNr, internalNr, pictureLocation, nameAD FROM hardware JOIN people on hardware.eventID = people.eventID ", mysqlConnectie);
                MySqlDataAdapter adpa = new MySqlDataAdapter(bindToGrid);
                bindToGrid.ExecuteNonQuery();
                bindToGrid.Dispose();
                DataSet ds = new DataSet();
                adpa.Fill(ds);
                grvHardwarePoolAssigned.DataSource = ds;
                grvHardwarePoolAssigned.DataBind();
                mysqlConnectie.Close();
            }
            catch (MySqlException ex)
            {
                lblResult.Text = ex.ToString();
            }
        }
        protected void grvHardwarePoolAssigned_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvHardwarePoolAssigned, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        protected void btnReturnHardware_Click(object sender, EventArgs e)
        {
            String strInternalNr = grvHardwarePoolAssigned.SelectedDataKey.Value.ToString();
            mysqlConnectie.Open();
            MySqlCommand unassignHardware = new MySqlCommand("UPDATE hardware SET eventID='' WHERE internalNr='"+strInternalNr+"'", mysqlConnectie);
            unassignHardware.ExecuteNonQuery();
            unassignHardware.Dispose();
            mysqlConnectie.Close();
            getAssignedHardware();
        }
    }
    
}