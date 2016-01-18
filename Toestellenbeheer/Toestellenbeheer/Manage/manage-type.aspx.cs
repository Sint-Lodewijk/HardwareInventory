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
using System.Text.RegularExpressions;

namespace Toestellenbeheer.Manage
{
    public partial class manage_type : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddType_Click(object sender, EventArgs e)
        {

            try
            {
                String strTypeNr = typeNr.Text.ToString();
                String strType = typeName.Text.ToString();

                    mysqlConnectie.Open();
                    MySqlCommand addType = new MySqlCommand("Insert into type (typeNr, type) values (@typeNr, @type)", mysqlConnectie);

                    //add parameters (assaign the values to the column.)
                    addType.Parameters.AddWithValue("@typeNr", strTypeNr);
                    addType.Parameters.AddWithValue("@type", strType);


                    addType.ExecuteNonQuery();
                    addType.Dispose();
                    mysqlConnectie.Close();
                    //txtResultUpload.Text = "Congratulations! The device with a internal nr: " + "<span style=\"color:red\">" + strInternalNr + "</span>" +
                    // " and a serial nr: " + "<span style=\"color:red\">" + strSerialNr + "</span>" + " successfully added to the database.";
                }
            
            catch (MySqlException ex)
            {
                if (ex.Number.ToString() == "1062")
                {
                    //testLabel.Text = ex.Message.ToString() + ", please check your input.";
                 //   txtResultUpload.Text = "The device with a internal nr: " + "<span style=\"color:red\">" + strInternalNr + "</span>" +
                 //       " and a serial nr: " + "<span style=\"color:red\">" + strSerialNr + "</span>" + " already exist in de database.";

                }
                else { ShowMessage(ex.Message); }


            }
            //test the value - removeable
        }

        void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<scriptlanguage = 'javascript'> alert('" + msg + "');</ script > ");
        }
    }
    
    
}