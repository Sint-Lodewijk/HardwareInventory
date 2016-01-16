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
            MySqlCommand bindToGrid = new MySqlCommand("SELECT licenseName 'License name', licenseCode 'License code', serialNr 'Serial nr', internalNr 'Internal Nr' FROM license", mysqlConnectie);
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
        /*trying to add remove license methode
        protected void removeSelectedLicense_Click(object sender, EventArgs e)
        {
            try
            {
                mysqlConnectie.Open();
                String strSearchItem = drpSearchItem.SelectedValue.ToString();
                String strSearchText = txtSearch.Text.Trim();
                MySqlCommand removeSelectedLicense = new MySqlCommand("DELETE FROM hardware WHERE serialNr=" + +" and `internalNr`='')" + strSearchText + " %';", mysqlConnectie);
                MySqlCommand bindToGrid = new MySqlCommand("SELECT * FROM hardware WHERE " + strSearchItem + " LIKE '%" + strSearchText + "%';", mysqlConnectie);

                removeSelectedLicense.ExecuteNonQuery();
                removeSelectedLicense.Dispose();
            }
            catch (MySqlException ex)
            {
                ShowMessage(ex.Message);
            }
        }*/
    }



}