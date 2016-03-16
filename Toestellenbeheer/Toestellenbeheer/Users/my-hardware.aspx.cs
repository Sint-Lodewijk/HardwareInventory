using Microsoft.AspNet.Identity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                    //for test purpose
                    if (strUserName == null)
                    {
                        strUserName = "testUser";
                    }
                    mysqlConnectie.Open();
                    MySqlCommand getMyHardware = new MySqlCommand("SELECT hardware.serialNr, hardware.internalNr, assignedDate FROM archive JOIN hardware ON archive.internalNr = hardware.internalNr JOIN people ON archive.eventID = people.eventID WHERE returnedDate IS NOT NULL AND nameAD = '" + strUserName + "'", mysqlConnectie);
                    MySqlDataReader adpa;
                    adpa = getMyHardware.ExecuteReader();
                    adpa.Dispose();
                    DataTable dt = new DataTable();
                    dt.Load(getMyHardware.ExecuteReader());
                    grvMyHardware.DataSource = dt;
                    grvMyHardware.DataBind();
                    mysqlConnectie.Close();
                    if (grvMyHardware.Columns.Count == 0)
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