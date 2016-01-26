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
            if (!IsPostBack)
            {

                bindTypeToGrid();
            }
        }

        protected void bindTypeToGrid()
        {
            try
            {
                mysqlConnectie.Open();

                MySqlCommand bindToGrid = new MySqlCommand("SELECT * FROM type", mysqlConnectie);
                MySqlDataAdapter adpa = new MySqlDataAdapter(bindToGrid);
                bindToGrid.ExecuteNonQuery();
                bindToGrid.Dispose();
                DataSet ds = new DataSet();
                adpa.Fill(ds);
                typeSelect.DataSource = ds;
                typeSelect.DataBind();
                mysqlConnectie.Close();

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

                addType.Parameters.AddWithValue("@Type", strType);


                addType.ExecuteNonQuery();
                addType.Dispose();
                mysqlConnectie.Close();

                bindTypeToGrid();
            }

            catch (MySqlException ex)
            {

                ShowMessage(ex.Message);
            }

        }

        void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<scriptlanguage = 'javascript'> alert('" + msg + "');</ script > ");
        }
    }

}