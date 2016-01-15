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
using System.Net;
using System.Net.Mail;

namespace Toestellenbeheer
{
    public partial class hardware_overview : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mysqlConnectie.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from hardware", mysqlConnectie);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                DataSet ds = new DataSet();
                adp.Fill(ds);
                HardwareOverviewGrid.DataSource = ds;
                HardwareOverviewGrid.DataBind();
                lblSearch.Text = HardwareOverviewGrid.Rows.Count.ToString();
                mysqlConnectie.Close();


            }
        }

        protected void Search(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                /*
                MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                mysqlConnectie.Open();
                string bindToGridCmd = "SELECT * FROM hardware WHERE 'internalNr' LIKE '%d%'";
                MySqlCommand bindToGrid = new MySqlCommand(bindToGridCmd, mysqlConnectie);
                bindToGrid.Parameters.AddWithValue("@searchItem", drpSearchItem.SelectedValue);
                bindToGrid.Parameters.AddWithValue("@searchText", txtSearch.Text.Trim());
                lblSearch.Text = drpSearchItem.SelectedValue;
                DataTable dt = new DataTable();
                using (MySqlDataAdapter sda = new MySqlDataAdapter(bindToGrid))
                {
                    sda.Fill(dt);
                    HardwareOverviewGrid.DataSource = dt;
                    HardwareOverviewGrid.DataBind();
                    mysqlConnectie.Close();

                }
                */
                mysqlConnectie.Open();
                String strSearchItem = drpSearchItem.SelectedValue.ToString();
                String strSearchText = txtSearch.Text.Trim();
               // string bindToGridCmd = "SELECT * FROM hardware WHERE @searchItem LIKE '%@searchText%'";
                MySqlCommand bindToGrid = new MySqlCommand("SELECT * FROM hardware WHERE " + strSearchItem + " LIKE '%" + strSearchText + "%';", mysqlConnectie);
                bindToGrid.Parameters.AddWithValue("@searchItem", strSearchItem);
                bindToGrid.Parameters.AddWithValue("@searchText", strSearchText);
                MySqlDataAdapter adpa = new MySqlDataAdapter(bindToGrid);
                bindToGrid.ExecuteNonQuery();
                bindToGrid.Dispose();
                DataSet ds = new DataSet();
                adpa.Fill(ds);
                HardwareOverviewGrid.DataSource = ds;
                HardwareOverviewGrid.DataBind();
                lblSearch.Text = HardwareOverviewGrid.Rows.Count.ToString();
                mysqlConnectie.Close();
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

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            HardwareOverviewGrid.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }


    }
}
        

