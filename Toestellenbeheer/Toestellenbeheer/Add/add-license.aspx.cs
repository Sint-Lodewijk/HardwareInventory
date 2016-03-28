﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.DirectoryServices;
using Toestellenbeheer.Models;
namespace Toestellenbeheer.Manage
{
    public partial class add_license : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        DirectoryEntry entry = new DirectoryEntry(SetupFile.AD.ADRootPath); // set up domain context


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnAssignToSelectedHardwareSearch.Visible = false;
                //hardwarePanel.Visible = false;
                //peoplePanel.Visible = false;
                String strTimeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                if (ViewState["timeStampAddedHardware"] == null)
                {
                    ViewState["timeStampAddedHardware"] = strTimeStamp;
                }
            }


        }
        //Change the color when selected
        protected void hardwareLicenseSelection_Click(object sender, EventArgs e)
        {
            btnAssignToSelectedHardware.Visible = true;
            RowSelect(grvHardwareLicenseSelect);

        }
        protected void RowSelect(GridView grvSelect)
        {
            foreach (GridViewRow row in grvSelect.Rows)
            {
                if (row.RowIndex == grvSelect.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                    //Get the row
                    GridViewRow selectedRow = grvSelect.SelectedRow;


                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
        }
        //When click search button, the right panel stays appear
        protected void Search_Click(object sender, EventArgs e)
        {
            this.Search();
            btnAssignToSelectedHardwareSearch.Visible = true;
            btnAssignToSelectedHardware.Visible = false;
            this.HardwarePanelPopUP.Show();


        }
        //Displays the search button
        protected void display_search_button(object sender, EventArgs e)
        {
            RowSelect(licenseOverviewGridSearch);
            btnAssignToSelectedHardwareSearch.Visible = true;

        }
        //Search and bind the entrys
        private void Search()
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
                    testLabel.Text = "No entry found, please use a different keyword or switch between the search types.";
                }
                else
                {
                    testLabel.Text = "Total result returned: " + intTotalResultReturned;

                }
                grvHardwareLicenseSelect.Visible = false;
            }
            catch (MySqlException ex)
            {
                ShowMessage(ex.Message);
            }
            finally
            {
                mysqlConnectie.Close();

            }

        }

        //Function for add license key into the database
        protected void addLicense()
        {
            try
            {

                mysqlConnectie.Open();
                MySqlCommand addLicense = new MySqlCommand("INSERT INTO license (licenseName, licenseCode, expireDate, extraInfo, licenseFileLocation) values (@licenseName, @licenseCode, @expireDate, @extraInfo, @licenseFileLocation)", mysqlConnectie);
                addLicense.Parameters.AddWithValue("@licenseName", txtLicenseName.Text.Trim());
                addLicense.Parameters.AddWithValue("@expireDate", txtDatepickerExpire.Text.Trim());
                addLicense.Parameters.AddWithValue("@extraInfo", txtExtraInfoLicense.Text.Trim());
                addLicense.Parameters.AddWithValue("@licenseFileLocation", ViewState["timeStampAddedHardware"] + TestlocationAtt.Text.Trim());


                addLicense.ExecuteNonQuery();
                addLicense.Dispose();
                testLabel.Text = "Congratulations! The license code:" + "<span class=\"labelOutput\">" + txtLicenseCode.Text
                    + "</span>" + " you have entered, has been successfully added into the database. If want want assign this license to any hardware or people, please not use this page!";
                // testLabel.Text = licenseCode.Text + " has been assigned to device with a internal number: " + internalNr + " and a serial code " + SerialNr;
            }
            catch (MySqlException ex)
            {
                if (ex.Number.ToString() == "1062")
                {
                    //testLabel.Text = ex.Message.ToString() + ", please check your input.";
                    //        testLabel.Text = "The license code: " + "<span style=\"color:red\">" + txtLicenseCode.Text + "</span>" + " you have entered for internal number: " + "<span style=\"color:red\">" + internalNr + "</span>" + " has been assigned to another device.";
                    testLabel.Text = "The license code: " + "<span style=\"color:red\">" + txtLicenseCode.Text + "</span>" + " you have entered, already exists in de database!";

                }
                else if (ex.Number.ToString() == "1064")
                {

                    //testLabel.Text = ex.Message.ToString() + ", please check your input.";
                    testLabel.Text = "Apostrophe ('), quotation mark and semicolum is not allow in the searchword: " + "<span style=\"color:red\">" + txtSearch + "</span>" + ", please delete this marks.";

                }
                else { ShowMessage(ex.Message); }

            }
            finally
            {
                mysqlConnectie.Close();

            }
        }
        protected void btnAddLicense_click(object sender, EventArgs e)
        {
            addLicense();
        }
        //When click the button, use the assign function to assign the right hardware.
        protected void assignToSelectedHardware_Click(object sender, EventArgs e)
        {
            try
            {
                String strLicenseCode = txtLicenseCode.Text;

                String strLicenseName = txtLicenseName.Text;
                String strInternalNr = grvHardwareLicenseSelect.SelectedDataKey["internalNr"].ToString();
                String strSerialCode = grvHardwareLicenseSelect.SelectedDataKey["serialNr"].ToString();
                addLicense();

                mysqlConnectie.Open();
                MySqlCommand addLicenseCommand = new MySqlCommand("INSERT INTO licenseHandler (internalNr, serialNr, licenseCode) values (@internalNr, @serialNr, @licenseCode)", mysqlConnectie);
                addLicenseCommand.Parameters.AddWithValue("@internalNr", strInternalNr);
                addLicenseCommand.Parameters.AddWithValue("@serialNr", strSerialCode);
                addLicenseCommand.Parameters.AddWithValue("@licenseCode", strLicenseCode);

                addLicenseCommand.ExecuteNonQuery();
                addLicenseCommand.Dispose();
                mysqlConnectie.Close();
            }
            catch (MySqlException ex)
            {
            }
        }
        //Shows the error message
        void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<scriptlanguage = 'javascript'> An error has been occured, please check the errorcode -> ('" + msg + "');</ script > ");
        }

        //Uses the assign function to assign the license to selected hardware
        protected void assignToSelectedHardwareSearch_Click(object sender, EventArgs e)
        {
            String internalNr = licenseOverviewGridSearch.SelectedDataKey["internalNr"].ToString();
            String strSerialCode = licenseOverviewGridSearch.SelectedDataKey["serialNr"].ToString();

        }

        //Expand or hide hardware grid + change the text of it
        protected void hideShowHardware_Click(object sender, EventArgs e)
        {

            hardwarePanel.Visible = true;


            this.HardwarePanelPopUP.Show();

        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvHardwareLicenseSelect, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        //Expand or hide people grid + change the text of it
        protected void hideShowPeople_Click(object sender, EventArgs e)
        {
            peoplePanel.Visible = true;
            getUserFromAD(licenseOverviewGridPeople);

            this.PeoplePopUP.Show();




        }

        //Displays the hardwarePanel when click
        protected void displayHardwarePanel(object sender, GridViewSortEventArgs e)
        {
            hardwarePanel.Visible = true;
        }
        //Get users from ad and display it in the gridview named licenseOverviewGridPeopleSearch
        protected void getUserFromAD(GridView grvLicenseOverviewPeople)
        {
            User get = new User();
            DataTable dt = get.ReturnDataTable();
            grvLicenseOverviewPeople.DataSource = dt;

            grvLicenseOverviewPeople.DataBind();
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
                String nameAD = licenseOverviewGridPeople.SelectedDataKey["Domain Name"].ToString();
                String strLicenseCode = txtLicenseCode.Text;
                addLicense();
                mysqlConnectie.Open();

                MySqlCommand assignLicenseToPeople = new MySqlCommand("INSERT INTO licenseHandler (eventID, licenseCode) values (@eventID, @licenseCode)", mysqlConnectie);
                User getUserID = new User(nameAD);
                int userID = getUserID.ReturnEventID();
                assignLicenseToPeople.Parameters.AddWithValue("@licenseCode", strLicenseCode);
                assignLicenseToPeople.Parameters.AddWithValue("@eventID", userID);

                assignLicenseToPeople.ExecuteNonQuery();
                assignLicenseToPeople.Dispose();

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
                    testLabel.Text = "Apostrophe ('), quotation mark and semicolum is not allowed in the searchword: " + "<span style=\"color:red\">" + txtLicenseCode.Text + "</span>" + ", please delete this marks.";

                }
                else { ShowMessage(ex.Message); }
            }
        }
        protected void btnUploadLicense_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    String path = Server.MapPath("~/UserUploads/License/");
                    if (LicenseFileUpload.HasFile)
                    {
                        try
                        {
                            LicenseFileUpload.PostedFile.SaveAs(path
                                + ViewState["timeStampAddedHardware"] + LicenseFileUpload.FileName);
                            String mAttachPath = LicenseFileUpload.FileName.ToString();
                            TestlocationAtt.Text = LicenseFileUpload.FileName.ToString();
                            ResultUploadAtta.Text = "License file uploaded!";


                        }
                        catch (Exception ex)
                        {
                            ResultUploadAtta.Text = "License file could not be uploaded. Because: " + ex.ToString();
                        }
                    }
                }
                else
                {
                    ResultUploadAtta.Text = "Do you not want to add a license?";
                }
            }
            catch (MySqlException ex)
            {
                ShowMessage(ex.ToString());
            }
        }

        protected void licenseOverviewGridPeople_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            licenseOverviewGridPeople.PageIndex = e.NewPageIndex;
            getUserFromAD(licenseOverviewGridPeople);
        }
    }
}