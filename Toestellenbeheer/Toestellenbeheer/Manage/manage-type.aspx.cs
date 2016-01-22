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
            if (!IsPostBack) { 
            mysqlConnectie.Open();

            bindTypeToGrid();
            mysqlConnectie.Close();
            }
        }

        protected void bindTypeToGrid()
        {
            try
            {
                MySqlCommand bindToGrid = new MySqlCommand("SELECT * FROM type", mysqlConnectie);
                MySqlDataAdapter adpa = new MySqlDataAdapter(bindToGrid);
                bindToGrid.ExecuteNonQuery();
                bindToGrid.Dispose();
                DataSet ds = new DataSet();
                adpa.Fill(ds);
                typeSelect.DataSource = ds;
                typeSelect.DataBind();
            }
            catch (MySqlException ex)
            {
                lblProblem.Text = ex.ToString();
            }
        }

        protected void btnAddType_Click(object sender, EventArgs e)
        {

            try
            {
                String strType = typeName.Text.ToString();

                mysqlConnectie.Open();
                MySqlCommand addType = new MySqlCommand("Insert into type (type) values (@Type)", mysqlConnectie);

                //add parameters (assaign the values to the column.)
                addType.Parameters.AddWithValue("@Type", strType);


                addType.ExecuteNonQuery();
                addType.Dispose();
                mysqlConnectie.Close();

                bindTypeToGrid();
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
                else {
                    ShowMessage(ex.Message); }


            }
        }

        void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<scriptlanguage = 'javascript'> alert('" + msg + "');</ script > ");
        }
    }


}