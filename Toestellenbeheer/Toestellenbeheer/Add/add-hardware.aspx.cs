using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using MySql.Data;
using Toestellenbeheer.Models;
namespace Toestellenbeheer.Manage
{
    /// <summary>
    /// Class add_hardware - Add a hardware into the database.
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class add_hardware : System.Web.UI.Page
    {
        #region MySqlConnection Connection and Page Lode        
        /// <summary>
        /// Initialize MySqlConnection for whole file.
        /// </summary>
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                addResultPanel.Visible = false;
                if (!IsPostBack)
                {
                    String strTimeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    if (ViewState["timeStampAddedHardware"] == null)
                    {
                        ViewState["timeStampAddedHardware"] = strTimeStamp;
                    }
                }
            }
            catch (Exception ex)
            {
                txtResultUpload.Text = ex.ToString();
            }
        }
        #endregion
        #region Insert Data
        ///<summary>Add a hardware into the database
        ///with the filled information</summary>
        ///<remarks>NULL in the database (table) will be overwritten with a empty string
        ///</remarks>
        protected void Submit_Click(object sender, EventArgs e)
        {
            String strSerialNr = Serialnr.Text;
            String strWarrantyInfo = warrantyInfo.Text.ToString();
            String strInternalNr = internalNr.Text;
            if (strSerialNr == "")
            {
               serialError.Text= "The serial nr is necessary!";
            }
            else if (strInternalNr == "")
            {
               internalError.Text = "The internal nr is necessary!";
            }
            else
            {
                String strExtraInfo = extraInfo.Text.ToString();
                String mImagePath = Testlocation.Text.ToString();
                String mAttPath = TestlocationAtt.Text;
                String strModel = modelNr.Text;
                int intSelectedTypeIndex = typeList.SelectedIndex + 1;
                String strSelectedManufacturer = manufacturerList.SelectedItem.ToString();
                String dtePurchaseYear = txtDatepicker.Text.Substring(6);
                String dtePurchaseDay = txtDatepicker.Text.Substring(0, 2);
                String dtePurchaseMonth = txtDatepicker.Text.Substring(3, 2);
                //dtePurchaseDate converts the datepicker to database usable date
                String dtePurchaseDate = dtePurchaseYear + '-' + dtePurchaseMonth + '-' + dtePurchaseDay;
                String dteAddedDate = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime addedDate = DateTime.Today;
                DateTime purchaseDate = new DateTime(int.Parse(dtePurchaseYear), int.Parse(dtePurchaseMonth), int.Parse(dtePurchaseDay), 0, 0, 0);
                int result = DateTime.Compare(purchaseDate, addedDate);
                if (result > 0)
                {
                    dtePurchaseDate = dteAddedDate;
                }
                try
                {
                    if (mImagePath == "" || mImagePath == null)
                    {
                        ViewState["pictureLocation"] = "";
                    }
                    else
                    {
                        ViewState["pictureLocation"] = ViewState["timeStampAddedHardware"] + mImagePath;
                    }
                    if (TestlocationAtt.Text == "" || TestlocationAtt.Text == null)
                    {
                        ViewState["attachmentLocation"] = "";
                    }
                    else
                    {
                        ViewState["attachmentLocation"] = ViewState["timeStampAddedHardware"] + TestlocationAtt.Text;
                    }
                    txtResultUpload.Text = "Congratulations! The device with a internal nr: " + "<span style=\"color:red\">" + strInternalNr + "</span>" +
                       " and a serial nr: " + "<span style=\"color:red\">" + strSerialNr + "</span>" + " successfully added to the database.";
                    var hardware = new Hardware(dteAddedDate, ViewState["attachmentLocation"].ToString(), strExtraInfo, strInternalNr, strSelectedManufacturer, strModel, ViewState["pictureLocation"].ToString(),
                        dtePurchaseDate, strSerialNr, strWarrantyInfo, typeList.SelectedValue);
                    hardware.AddHardwareIntoDatabase();
                    viewJustAddedHardware();

                }
              
                catch (MySqlException ex)
                {
                    if (ex.Number.ToString() == "1062")
                    {
                        txtResultUpload.Text = "The device with a internal nr: " + "<span style=\"color:red\">" + strInternalNr + "</span>" +
                            " and a serial nr: " + "<span style=\"color:red\">" + strSerialNr + "</span>" + " already exist in de database.";
                    }
                    else { ShowMessage(ex.Message); }
                }
                addHardwarePanel.Visible = false;
                addResultPanel.Visible = true;
            }
        }
        /// <summary>
        /// Views the just added hardware.
        /// </summary>
        protected void viewJustAddedHardware()
        {
            try
            {
                String strInternalNr = internalNr.Text.ToString();
                var hardware = new Hardware(strInternalNr);
                DataTable dt = hardware.ReturnDatatableHardwareFromInternal();
                grvJustAddedHardware.DataSource = dt;
                grvJustAddedHardware.DataBind();
            }
            catch (MySqlException ex)
            {
                MysqlExeptionhandler(ex);
            }
        }
        /// <summary>
        /// Mysql exeptionhandler.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void MysqlExeptionhandler(MySqlException ex)
        {
            if (ex.Number.ToString() == "1062")
            {
                //testLabel.Text = ex.Message.ToString() + ", please check your input.";
                txtResultUpload.Text = "This device already exist in de database.";
            }
            else { ShowMessage(ex.Message); }
        }
        /// <summary>
        /// Download the file in /Attachments folder when pressing the linkbutton in Gridview
        /// </summary>
        protected void DownloadFile(object sender, EventArgs e)
        {
            string path = "../UserUploads/Attachments/";
            Directory.CreateDirectory(path);
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(path + Path.GetFileName(filePath));
            Response.End();
        }
        /// <summary>
        /// Shows the message at the end of the page.
        /// </summary>
        /// <param name="msg">The message.</param>
        /// <remarks>
        /// Trying to change to JavaScript Alert...
        ///  </remarks>
        void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<scriptlanguage = 'javascript'> alert('" + msg + "');</ script > ");
        }
        #endregion
        //Upload the selected picture and return the right path
        /// <summary>
        /// Upload the selected picture and return to /Images folder
        /// </summary>
        /// <remarks>
        /// Only the pictures with the extensions ".gif", ".png", ".jpeg", ".jpg",".bmp",".tif" are allowed
        /// </remarks>
        public void Upload_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //Initialize the Boolean FileOk to status false;
                Boolean fileOK = false;
                String path = Server.MapPath("~/UserUploads/Images/");
                Directory.CreateDirectory(path);
                if (PictureUpload.HasFile)
                {
                    //Gets the extension of the uploaded file
                    String fileExtension =
                        System.IO.Path.GetExtension(PictureUpload.FileName).ToLower();
                    String[] allowedExtensions =
                        {".gif", ".png", ".jpeg", ".jpg",".bmp",".tif"};
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExtension == allowedExtensions[i])
                        {
                            fileOK = true;
                        }
                    }
                }
                if (fileOK == true)
                {
                    try
                    {
                        //Save the uploaded file to the predefined path
                        PictureUpload.PostedFile.SaveAs(path
                            + ViewState["timeStampAddedHardware"].ToString() + PictureUpload.FileName);
                        String mImagePath = PictureUpload.FileName.ToString();
                        //Gets the path and return this path to ASP Label control - Testlocation
                        Testlocation.Text = mImagePath;
                        ResultUploadImg.Text = "File uploaded!";
                    }
                    catch (Exception ex)
                    {
                        ResultUploadImg.Text = "File could not be uploaded. Because: " + ex.ToString();
                    }
                }
                else
                {
                    ResultUploadImg.Text = "Not a extension of image";
                }
            }
        }
        //When click, add another hardware        
        /// <summary>
        /// Handles Click event - Add an other hardware -> return to add-hardware page
        /// </summary>
        protected void btnAddAnotherHardware_Click(object sender, EventArgs e)
        {
            Server.Transfer("./add-hardware.aspx");
        }
        //Upload the attachment and return the path.
        protected void UploadAttachment_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                String path = Server.MapPath("~/UserUploads/Attachments/");
                Directory.CreateDirectory(path);
                if (AttachmentUpload.HasFile)
                {
                    try
                    {
                        AttachmentUpload.PostedFile.SaveAs(path
                           + ViewState["timeStampAddedHardware"].ToString() + AttachmentUpload.FileName);
                        String mAttachPath = AttachmentUpload.FileName.ToString();
                        TestlocationAtt.Text = AttachmentUpload.FileName.ToString();
                        ResultUploadAtta.Text = "File uploaded!";
                    }
                    catch (Exception ex)
                    {
                        ResultUploadAtta.Text = "File could not be uploaded. Because: " + ex.ToString();
                    }
                }
            }
            else
            {
                ResultUploadAtta.Text = "Do you not want to add a attachment?";
            }
        }
    }
}

