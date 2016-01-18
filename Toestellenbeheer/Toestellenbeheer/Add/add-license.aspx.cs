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
            btnAssignToSelectedHardwareSearch.Visible = false;
            hardwarePanel.Visible = false;
        }
        //Change the color when selected
        protected void hardwareLicenseSelection_Click(object sender, EventArgs e)
        {
            btnAssignToSelectedHardware.Visible = true;

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
            hardwarePanel.Visible = true;
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            this.BindGrid();
            btnAssignToSelectedHardwareSearch.Visible = true;
            btnAssignToSelectedHardware.Visible = false;
            hardwarePanel.Visible = true;


        }
        protected void display_search_button(object sender, EventArgs e)
        {
            btnAssignToSelectedHardwareSearch.Visible = true;
            hardwarePanel.Visible = true;

        }

        private void BindGrid()
        {
            try
            {
                mysqlConnectie.Open();
                String strSearchItem = drpSearchItem.SelectedValue.ToString();
                String strSearchText = txtSearch.Text.Trim();
                // string bindToGridCmd = "SELECT * FROM hardware WHERE @searchItem LIKE '%@searchText%'";
                MySqlCommand bindToGrid = new MySqlCommand("SELECT typeNr `Type nr`,manufacturerName `Manufacturer`,internalNr 'Internal nr', serialNr 'Serial nr' FROM hardware WHERE " + strSearchItem + " LIKE '%" + strSearchText + "%';", mysqlConnectie);
    
                MySqlDataAdapter adpa = new MySqlDataAdapter(bindToGrid);
                bindToGrid.ExecuteNonQuery();
                bindToGrid.Dispose();
                DataSet ds = new DataSet();

                adpa.Fill(ds);
                licenseOverviewGridSearch.DataSource = ds;
                licenseOverviewGridSearch.DataBind();
                int intTotalResultReturned = licenseOverviewGridSearch.Rows.Count;
                if (intTotalResultReturned == 0)
                {
                    testLabel.Text = "No entry found, please use a different keyword or switch between the searchtypes.";
                }
                else
                {
                    testLabel.Text = "Total result returned: " + intTotalResultReturned;

                }
                mysqlConnectie.Close();
                grvHardwareLicenseSelect.Visible = false;
            }
            catch (MySqlException ex)
            {
                ShowMessage(ex.Message);
            }

        }
        protected void assign(String internalNr, String SerialNr)
        {

            String strLicenseCode = txtLicenseCode.Text;

            String strLicenseName = txtLicenseName.Text;

            //Accessing TemplateField Column controls
            try
            {



                mysqlConnectie.Open();
                //     MySqlCommand listOutType = new MySqlCommand("INSERT INTO license (licenseName, licenseCode, serialNr, internalNr) values (@licenseName, @licenseCode, @serialNr, @internalNr)", mysqlConnectie);
                MySqlCommand listOutType = new MySqlCommand("INSERT INTO license (licenseName, licenseCode, serialNr, internalNr) values (@licenseName, @licenseCode, @serialNr, @internalNr)", mysqlConnectie);

                //using (MySqlDataAdapter data1 = new MySqlDataAdapter(listOutType))
                //     data1.Fill(Type, "type");
                listOutType.Parameters.AddWithValue("@licenseName", strLicenseName);
                listOutType.Parameters.AddWithValue("@licenseCode", strLicenseCode);
                listOutType.Parameters.AddWithValue("@serialNr", SerialNr);
                listOutType.Parameters.AddWithValue("@internalNr", internalNr);
                listOutType.ExecuteNonQuery();
                listOutType.Dispose();
                mysqlConnectie.Close();


                testLabel.Text = strLicenseCode + " has been assigned to device with a internal number: " + internalNr + " and a serial code " + SerialNr;
            }
            catch (MySqlException ex)
            {
                if (ex.Number.ToString() == "1062")
                {
                    //testLabel.Text = ex.Message.ToString() + ", please check your input.";
                    testLabel.Text = "The license code: " + "<span style=\"color:red\">" + strLicenseCode + "</span>" + " you have entered for internal number: " + "<span style=\"color:red\">" + internalNr + "</span>" + " has been assigned to another device.";

                }
                else if (ex.Number.ToString() == "1064")
                {
                    
                    //testLabel.Text = ex.Message.ToString() + ", please check your input.";
                    testLabel.Text = "Apostrophe ('), quotation mark and semicolum is not allow in the searchword: " + "<span style=\"color:red\">" + txtSearch + "</span>" + ", please delete this marks.";

                }
                else { ShowMessage(ex.Message); }

            }
            //test the value - removeable
        }

        protected void assignToSelectedHardware_Click(object sender, EventArgs e)
        {

            String internalNr = grvHardwareLicenseSelect.SelectedRow.Cells[3].Text;
            String strSerialCode = grvHardwareLicenseSelect.SelectedRow.Cells[4].Text;
            assign(internalNr, strSerialCode);

        }

        void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<scriptlanguage = 'javascript'> An error has been occured, please check the errorcode -> ('" + msg + "');</ script > ");
        }

        protected void assignToSelectedHardwareSearch_Click(object sender, EventArgs e)
        {
            String internalNr = licenseOverviewGridSearch.SelectedRow.Cells[3].Text;
            String strSerialCode = licenseOverviewGridSearch.SelectedRow.Cells[4].Text;
            assign(internalNr, strSerialCode);
        }
        //Expand or hide hardware grid
        protected void hideShowHardware_Click(object sender, EventArgs e)
        {
            if (hideShowHardware.Text == "Assign to hardware")
            {
                hideShowHardware.Text = "Hide hardware";
                hardwarePanel.Visible = true;

            }
            else if (hideShowHardware.Text == "Hide hardware")
            {
                hideShowHardware.Text = "Assign to hardware";
                hardwarePanel.Visible = false;

            }
          
        }
    }

}