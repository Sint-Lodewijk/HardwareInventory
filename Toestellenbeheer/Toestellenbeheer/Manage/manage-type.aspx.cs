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
using Toestellenbeheer.Models;


namespace Toestellenbeheer.Manage
{
    public partial class manage_type : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

              //  bindTypeToGrid();
            }
        }

        /*protected void bindTypeToGrid()
        {
            try
            {
                var type = new TypeName();
                DataTable dt = type.ReturnDatatableType();
                typeSelect.DataSource = dt;
                typeSelect.DataBind();

            }
            catch (MySqlException ex)
            {
                lblProblem.Text = ex.ToString();
            }
        }
        */
        protected void btnAddType_Click(object sender, EventArgs e)
        {

            try
            {
                String strType = typeName.Text.ToString();

                var type = new TypeName(strType);
                type.AddTypeToDatabase();
                //bindTypeToGrid();
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

        protected void typeSelect_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int editIndex = e.NewEditIndex;
            string selectedType = typeSelect.DataKeys[editIndex].Value.ToString();
            Session["type"] = selectedType;
            string testsession = Session["type"].ToString();
        }
    }

}