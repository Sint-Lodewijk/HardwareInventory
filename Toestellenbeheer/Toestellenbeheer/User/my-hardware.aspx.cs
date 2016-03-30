using Microsoft.AspNet.Identity;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

namespace Toestellenbeheer.Users
{
    public partial class my_hardware : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    String strUserName = Context.User.Identity.GetUserName();
  
                    mysqlConnectie.Open();
                    MySqlCommand getMyHardware = new MySqlCommand("SELECT hardware.pictureLocation, hardware.serialNr, hardware.internalNr, hardware.manufacturerName, type FROM hardware JOIN archive ON hardware.internalNr = archive.internalNr  JOIN people ON people.eventID = archive.eventID  WHERE people.nameAD = '" + strUserName+"'", mysqlConnectie);
                    getMyHardware.Parameters.AddWithValue("@nameAD", strUserName);
                    MySqlDataReader rdrGetMyHardware = getMyHardware.ExecuteReader();
                    DataTable dt = new DataTable();
                    // MySqlDataAdapter adpa = new MySqlDataAdapter(rdrGetMyHardware);
                    dt.Load(rdrGetMyHardware);
                   
                    grvMyHardware.DataSource = dt;
                    grvMyHardware.DataBind();
                    mysqlConnectie.Close();
                    if (grvMyHardware.Rows.Count == 0)
                    {
                        lblStatus.Text = "You do not have lend any hardware yet.";
                    }
                }
                catch (MySqlException ex)
                {
                    lblError.Text = ex.ToString();
                }
            }
        }
    }
}