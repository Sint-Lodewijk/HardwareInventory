using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Security.Permissions;

namespace Toestellenbeheer.Manage
{
    public partial class add_license : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        DirectoryEntry entry = new DirectoryEntry("LDAP://magnix.dc.intranet");
        // set up domain context


        protected void Page_Load(object sender, EventArgs e)
        {
            btnAssignToSelectedHardwareSearch.Visible = false;
            hardwarePanel.Visible = false;
            peoplePanel.Visible = false;

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
        //When click search button, the right panel stays appear
        protected void Search_Click(object sender, EventArgs e)
        {
            this.BindGrid();
            btnAssignToSelectedHardwareSearch.Visible = true;
            btnAssignToSelectedHardware.Visible = false;
            hardwarePanel.Visible = true;


        }
        //Displays the search button
        protected void display_search_button(object sender, EventArgs e)
        {
            btnAssignToSelectedHardwareSearch.Visible = true;
            hardwarePanel.Visible = true;

        }
        //Search and bind the entrys
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
        //Function assign, used for both 
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
        //When click the button, use the assign function to assign the right hardware.
        protected void assignToSelectedHardware_Click(object sender, EventArgs e)
        {

            String internalNr = grvHardwareLicenseSelect.SelectedRow.Cells[3].Text;
            String strSerialCode = grvHardwareLicenseSelect.SelectedRow.Cells[4].Text;
            assign(internalNr, strSerialCode);

        }
        //Shows the error message
        void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<scriptlanguage = 'javascript'> An error has been occured, please check the errorcode -> ('" + msg + "');</ script > ");
        }

        //Uses the assign function to assign the license to selected hardware
        protected void assignToSelectedHardwareSearch_Click(object sender, EventArgs e)
        {
            String internalNr = licenseOverviewGridSearch.SelectedRow.Cells[3].Text;
            String strSerialCode = licenseOverviewGridSearch.SelectedRow.Cells[4].Text;
            assign(internalNr, strSerialCode);
        }

        //Expand or hide hardware grid + change the text of it
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
            else if (hideShowHardware.Text == "Assign to hardware")
            {
                hideShowHardware.Text = "Hide hardware";
                hardwarePanel.Visible = true;

            }




        }

        //Expand or hide people grid + change the text of it
        protected void hideShowPeople_Click(object sender, EventArgs e)
        {
            if (hideShowPeople.Text == "Assign to people")
            {
                hideShowPeople.Text = "Hide people";
                peoplePanel.Visible = true;
                getUserFromAD();

            }
            else if (hideShowPeople.Text == "Hide people")
            {
                hideShowPeople.Text = "Assign to people";
                peoplePanel.Visible = false;

            }



        }

        //Displays the hardwarePanel when click
        protected void displayHardwarePanel(object sender, GridViewSortEventArgs e)
        {
            hardwarePanel.Visible = true;
        }
        //Get users from ad and display it in the gridview named licenseOverviewGridPeopleSearch
        protected void getUserFromAD()
        {
            DirectoryEntry rootDSE = rootDSE = new DirectoryEntry("LDAP://magnix.dc.intranet", "readonly@dc.intranet", "id.13542");

            DirectorySearcher search = new DirectorySearcher(rootDSE);

            search.PageSize = 1001;// To Pull up more than 100 records.

            search.Filter = "(&(objectClass=user)(!userAccountControl:1.2.840.113556.1.4.803:=2))";//UserAccountControl will only Include Non-Disabled Users.
            SearchResultCollection result = search.FindAll();
            String DisplayName, EmailAddress, DomainName, Department, Title, Company, memberof, aaa;
            DisplayName = EmailAddress = DomainName = Department = Title = Company = memberof = aaa = "";
            foreach (SearchResult item in result)
            {
                if (item.Properties["cn"].Count > 0)
                {
                    DisplayName = item.Properties["cn"][0].ToString();
                }
                if (item.Properties["mail"].Count > 0)
                {
                    EmailAddress = item.Properties["mail"][0].ToString();
                }
                if (item.Properties["SamAccountName"].Count > 0)
                {
                    DomainName = item.Properties["SamAccountName"][0].ToString();
                }
                if (item.Properties["department"].Count > 0)
                {
                    Department = item.Properties["department"][0].ToString();
                }
                if (item.Properties["title"].Count > 0)
                {
                    Title = item.Properties["title"][0].ToString();
                }
                if (item.Properties["company"].Count > 0)
                {
                    Company = item.Properties["company"][0].ToString();
                }
                if (item.Properties["DistinguishedName"].Count > 0)
                {
                    memberof = item.Properties["DistinguishedName"][0].ToString();
                }
                if (item.Properties["AccountExpirationDate"].Count > 0)
                {
                    aaa = item.Properties["AccountExpirationDate"][0].ToString();
                }
                DataTable dt = new DataTable();
                //dt.Columns("DisplayName", "EmailAddress", "DomainName", "Department", "Title", "Company", "memberof");
                dt.Columns.Add(new DataColumn("Display Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Domain Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Email Address", typeof(string)));
                dt.Columns.Add(new DataColumn("Department", typeof(string)));
                dt.Columns.Add(new DataColumn("Title", typeof(string)));
                dt.Columns.Add(new DataColumn("Company", typeof(string)));
                dt.Columns.Add(new DataColumn("Member Of", typeof(string)));

                dt.Rows.Add(DisplayName, DomainName, EmailAddress, Department, Title, Company, memberof);

                rootDSE.Dispose();
                licenseOverviewGridPeople.DataSource = dt;

                licenseOverviewGridPeople.DataBind();
            }
        }

        protected void grvHardwareLicenseSelect_PageIndexChanged(object sender, EventArgs e)
        {
            hardwarePanel.Visible = true;
        }
        protected void selectPeopleGridview_Click(object sender, EventArgs e)
        {
            peoplePanel.Visible = true;

        }
        protected void assignLicenseToPeople(object sender, EventArgs e)
        {
            try
            {
                String nameAD = licenseOverviewGridPeople.SelectedRow.Cells[2].Text;
                String strLicenseCode = txtLicenseCode.Text;
                mysqlConnectie.Open();
                MySqlCommand addNewLicense = new MySqlCommand("INSERT INTO license (licenseCode) values (@licenseCode)", mysqlConnectie);
                addNewLicense.Parameters.AddWithValue("@licenseCode", strLicenseCode);
                addNewLicense.ExecuteNonQuery();
                addNewLicense.Dispose();
                //     MySqlCommand listOutType = new MySqlCommand("INSERT INTO license (licenseName, licenseCode, serialNr, internalNr) values (@licenseName, @licenseCode, @serialNr, @internalNr)", mysqlConnectie);
                MySqlCommand assignLicenseToPeople = new MySqlCommand("INSERT INTO people (nameAD, licenseCode) values (@nameAD, @licenseCode)", mysqlConnectie);

                //using (MySqlDataAdapter data1 = new MySqlDataAdapter(listOutType))
                //     data1.Fill(Type, "type");
                assignLicenseToPeople.Parameters.AddWithValue("@nameAD", nameAD);
                assignLicenseToPeople.Parameters.AddWithValue("@licenseCode", strLicenseCode);

                assignLicenseToPeople.ExecuteNonQuery();
                assignLicenseToPeople.Dispose();
                MySqlCommand updateLicenseStatus = new MySqlCommand("UPDATE license SET nameAD=" + "'"+ nameAD + "'" + "  WHERE licenseCode=" + "'" + strLicenseCode +"'", mysqlConnectie);
                updateLicenseStatus.ExecuteNonQuery();
                updateLicenseStatus.Dispose();
                mysqlConnectie.Close();
                testLabel.Text = "Congratulations! The license code: " + "<span class=\"labelOutput\">" + txtLicenseCode.Text + "</span>" +
                    " has been successfully assigned to " + "<span class=\"labelOutput\">" +
                    licenseOverviewGridPeople.SelectedRow.Cells[2].Text + "</span>";
                btnAssignLicenseToPeople.Text = "Assign to people";
            }
            catch (MySqlException ex)
            {
                if (ex.Number.ToString() == "1062")
                {
                    //testLabel.Text = ex.Message.ToString() + ", please check your input.";
                    testLabel.Text = "The license code: " + "<span style=\"color:red\">" +
                        txtLicenseCode.Text + "</span>" + " you have entered for: " + "<span style=\"color:red\">" +
                        licenseOverviewGridPeople.SelectedRow.Cells[2].Text + "</span>" + " has been assigned to another person.";

                }
                else if (ex.Number.ToString() == "1064")
                {

                    //testLabel.Text = ex.Message.ToString() + ", please check your input.";
                    testLabel.Text = "Apostrophe ('), quotation mark and semicolum is not allow in the searchword: " + "<span style=\"color:red\">" + txtLicenseCode.Text + "</span>" + ", please delete this marks.";

                }
                else { ShowMessage(ex.Message); }
            }
        }

    }
}