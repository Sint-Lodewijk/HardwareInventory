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
using System.Drawing;

namespace Toestellenbeheer.Manage
{
    public partial class add_license : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);


        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        //Change the color when selected
        protected void hardwareLicenseSelection_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvHardwareLicenseSelect.Rows)
            {
                if (row.RowIndex == grvHardwareLicenseSelect.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
        //Get the row
                    GridViewRow selectedRow = grvHardwareLicenseSelect.SelectedRow;


                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
        }

        protected void assignToSelectedHardware_Click(object sender, EventArgs e)
        {

            
                String strLicenseCode = txtLicenseCode.Text;
                String internalNr = grvHardwareLicenseSelect.SelectedRow.Cells[3].Text;
                String strLicenseName = txtLicnseName.Text;
                String strSerialCode = grvHardwareLicenseSelect.SelectedRow.Cells[4].Text;

            //Accessing TemplateField Column controls
            try {



                mysqlConnectie.Open();
                //     MySqlCommand listOutType = new MySqlCommand("INSERT INTO license (licenseName, licenseCode, serialNr, internalNr) values (@licenseName, @licenseCode, @serialNr, @internalNr)", mysqlConnectie);
                MySqlCommand listOutType = new MySqlCommand("INSERT INTO license (licenseName, licenseCode, serialNr, internalNr) values (@licenseName, @licenseCode, @serialNr, @internalNr)", mysqlConnectie);

                //using (MySqlDataAdapter data1 = new MySqlDataAdapter(listOutType))
                //     data1.Fill(Type, "type");
                listOutType.Parameters.AddWithValue("@licenseName", strLicenseName);
                listOutType.Parameters.AddWithValue("@licenseCode", strLicenseCode);
                listOutType.Parameters.AddWithValue("@serialNr", strSerialCode);
                listOutType.Parameters.AddWithValue("@internalNr", internalNr);
                listOutType.ExecuteNonQuery();
                listOutType.Dispose();
                mysqlConnectie.Close();


                testLabel.Text = internalNr + strLicenseCode + strSerialCode;
            }
            catch (MySqlException ex)
            {
                ShowMessage(ex.Message);
            }
            //test the value - removeable
        }

        void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<scriptlanguage = 'javascript'> alert('" + msg + "');</ script > ");
        }

       
    }

}