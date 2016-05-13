using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.DirectoryServices;
using Toestellenbeheer.Models;
using System.IO;
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
                if (ViewState["timeStampAddedLicense"] == null)
                {
                    ViewState["timeStampAddedLicense"] = strTimeStamp;
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
        }
        //Displays the search button
        protected void display_search_button(object sender, EventArgs e)
        {
            licenseOverviewGridSearch.Visible = true;
            RowSelect(licenseOverviewGridSearch);
            btnAssignToSelectedHardwareSearch.Visible = true;
        }
        //Search and bind the entrys
        private void Search()
        {
            try
            {
                String strSearchItem = drpSearchItem.SelectedValue.ToString();
                String strSearchText = txtSearch.Text.Trim();
                // string bindToGridCmd = "SELECT * FROM hardware WHERE @searchItem LIKE '%@searchText%'";
                var searchedHardware = new Hardware();
                searchedHardware.BindGrvSearch(strSearchItem, strSearchText, licenseOverviewGridSearch);
                int intTotalResultReturned = licenseOverviewGridSearch.Rows.Count;
                if (intTotalResultReturned == 0)
                {
                    lblSearchResult.Text = "No entry found for search word: " + strSearchText +
                      " on " + drpSearchItem.SelectedItem.Text + ", please use a different keyword or switch between the search types.";
                }
                else
                {
                    lblSearchResult.Text = "Total result returned: " + intTotalResultReturned + " for "
                        + strSearchText + " on " + drpSearchItem.SelectedItem.Text;
                }
                grvHardwareLicenseSelect.Visible = false;
            }
            catch (MySqlException ex)
            {
                ShowMessage(ex.Message);
            }
        }
        //Function for add license key into the database
        protected void addLicense()
        {
            string strLicenseName = txtLicenseName.Text.Trim();
            string strLicenseCode = txtLicenseCode.Text;
            string strExpireDate = txtDatepicker.Text.Substring(6) + "-" + txtDatepicker.Text.Substring(3, 2) + "-" + txtDatepicker.Text.Substring(0, 2);
            string strExtraInfo = txtExtraInfoLicense.Text;
            if (TestlocationAtt.Text.Trim() == "" || TestlocationAtt.Text == null)
            {
                ViewState["LocationWithTimeStamp"] = "";
            }
            else
            {
                ViewState["LocationWithTimeStamp"] = ViewState["timeStampAddedLicense"] + TestlocationAtt.Text.Trim();
            }
            var toAddLicense = new License(strLicenseCode, strLicenseName, strExpireDate, ViewState["LocationWithTimeStamp"].ToString(), strExtraInfo);
            toAddLicense.AddLicense();
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
                MySqlCommand getMaxIndexLicenseID = new MySqlCommand("SELECT MAX(licenseID) FROM license", mysqlConnectie);
                int intLicenseID = (int)getMaxIndexLicenseID.ExecuteScalar();
                mysqlConnectie.Close();
                var licenseToHardware = new LicenseHandler(strInternalNr, strSerialCode, intLicenseID);
                licenseToHardware.AssignLicenseToHardware();
                Session["SuccessInfo"] = "Congratulations! The license code:" + "<span class=\"labelOutput\">" + txtLicenseCode.Text
                    + "</span>" + " you have entered, has been successfully added into the database and assigned to the hardware with internal nr: " + strInternalNr;
                Server.Transfer("~/Success.aspx");
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.ToString());
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
            String strInternalNr = licenseOverviewGridSearch.Rows[2].ToString();
            String strSerialCode = licenseOverviewGridSearch.Rows[3].ToString();
            var maxLicense = new License();
            int intLicenseID = maxLicense.ReturnMaxLicenseID();
            var licenseToHardware = new LicenseHandler(strInternalNr, strSerialCode, intLicenseID);
            licenseToHardware.AssignLicenseToHardware();
            Session["SuccessInfo"] = "Congratulations! The license code:" + "<span class=\"labelOutput\">" + txtLicenseCode.Text
                    + "</span>" + " you have entered, has been successfully added into the database and assigned to the hardware with internal nr: " + strInternalNr;
            Server.Transfer("~/Success.aspx");
        }
        //Expand or hide hardware grid + change the text of it
        protected void hideShowHardware_Click(object sender, EventArgs e)
        {
            hardwarePanel.Visible = true;
        }
        protected void SearchBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(licenseOverviewGridSearch, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void PeopleBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(licenseOverviewGridPeople, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
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
            Models.User get = new Models.User();
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
                Models.User getUserID = new Models.User(nameAD);
                int userID = getUserID.ReturnEventID();
                var maxLicense = new License();
                int intLicenseID = maxLicense.ReturnMaxLicenseID();
                var assignLicenseToPeople = new LicenseHandler(userID, intLicenseID);
                assignLicenseToPeople.AssignLicenseToPeople();
                Session["SuccessInfo"] = "Congratulations! The license code: " + "<span class=\"labelOutput\">" + txtLicenseCode.Text + "</span>" +
                    " has been successfully assigned to " + "<span class=\"labelOutput\">" +
                    licenseOverviewGridPeople.SelectedRow.Cells[2].Text + "</span>";
                Server.Transfer("~/Success.aspx");
            }
            catch (MySqlException ex)
            {
                if (ex.Number.ToString() == "1062")
                {
                    //testLabel.Text = ex.Message.ToString() + ", please check your input.";
                    lblResult.Text = "The license code: " + "<span style=\"color:red\">" +
                        txtLicenseCode.Text + "</span>" + " you have entered for: " + "<span style=\"color:red\">" +
                        licenseOverviewGridPeople.SelectedRow.Cells[2].Text + "</span>" + " has been assigned to another person.";
                }
                else if (ex.Number.ToString() == "1064")
                {
                    //testLabel.Text = ex.Message.ToString() + ", please check your input.";
                    lblResult.Text = "Apostrophe ('), quotation mark and semicolum is not allowed in the searchword: " + "<span style=\"color:red\">" + txtLicenseCode.Text + "</span>" + ", please delete this marks.";
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
                    Directory.CreateDirectory(path);
                    if (LicenseFileUpload.HasFile)
                    {
                        try
                        {
                            LicenseFileUpload.PostedFile.SaveAs(path
                                + ViewState["timeStampAddedLicense"] + LicenseFileUpload.FileName);
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