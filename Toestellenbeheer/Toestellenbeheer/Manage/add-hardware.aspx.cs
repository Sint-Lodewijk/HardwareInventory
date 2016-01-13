using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Toestellenbeheer.Manage
{
    public partial class add_hardware : System.Web.UI.Page
    {
        #region MySqlConnection Connection and Page Lode
        //MySqlConnection mysqlConnectie = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        MySqlConnection mysqlConnectie = new MySqlConnection("Server = localhost; Port=3306;Database=Toestellenbeheer;Uid=root;Pwd=JIANINg****520");
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion
        #region Insert Data
        protected void Submit_Click(object sender, EventArgs e)
        {
            String strSerialNr = Serialnr.Text;
            String strWarrantyInfo = warrantyInfo.Text;
            String strInternalNr = internalNr.Text;
            String strExtrainfo = extraInfo.Text;
            String mImagePath = Testlocation.Text;
            String mAttPath = TestlocationAtt.Text;

            int dtePurchaseYear = purchasedateCalendar.SelectedDate.Year;
            int dtePurchaseMonth = purchasedateCalendar.SelectedDate.Month;
            int dtePurchaseDay = purchasedateCalendar.SelectedDate.Day;
            String dtePurchaseDate = dtePurchaseYear.ToString() + '-' + dtePurchaseMonth.ToString() + '-' + dtePurchaseDay.ToString();
            String dteAddedDate = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                /*
                    MySqlCommand addHIEP= new MySqlCommand("Insert into hardware (purchaseDate, serialNr, internalNr, pictureLocation, warranty, extraInfo, attachmentLocation) values (@purchaseDate, @serialNr, @internalNr, @picLocation, @warranty, @extraInfo, @attLocation)", mysqlConnectie);
                    addHIEP.Parameters.AddWithValue("@purchaseDate", dtePurchaseDate);
                    addHIEP.Parameters.AddWithValue("@serialNr", strSerialNr);
                    addHIEP.Parameters.AddWithValue("@internalNr", strExtraInfo);
                    addHIEP.Parameters.AddWithValue("@picLocation", strExtraInfo);
                    addHIEP.Parameters.AddWithValue("@warranty", strWarrantyInfo);
                    addHIEP.Parameters.AddWithValue("@extraInfo", strExtraInfo);
                    addHIEP.Parameters.AddWithValue("@attLocation", strExtraInfo);
                    */
                mysqlConnectie.Open();
                
                MySqlCommand addHIEP = new MySqlCommand("Insert into hardware (purchaseDate, serialNr, internalNr, pictureLocation, warranty, extraInfo, attachmentLocation, addedDate) values (@purchaseDate, @serialNr, @internalNr, @picLocation, @warranty, @extraInfo, @attLocation, @addedDate)", mysqlConnectie);
                addHIEP.Parameters.AddWithValue("@purchaseDate", dtePurchaseDate);
                addHIEP.Parameters.AddWithValue("@serialNr", strSerialNr);
                addHIEP.Parameters.AddWithValue("@internalNr", internalNr);
                addHIEP.Parameters.AddWithValue("@picLocation", mImagePath);
                addHIEP.Parameters.AddWithValue("@warranty", strWarrantyInfo);
                addHIEP.Parameters.AddWithValue("@extraInfo", strExtrainfo);
                addHIEP.Parameters.AddWithValue("@attLocation", mAttPath);
                addHIEP.Parameters.AddWithValue("@addedDate", dteAddedDate);

                addHIEP.ExecuteNonQuery();
                addHIEP.Dispose();
            }
            catch (MySqlException ex)
            {
                ShowMessage(ex.Message);
            }
            test.Text = dtePurchaseDate.ToString();              
           }

        void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<scriptlanguage = 'javascript'><script> alert('" + msg + "');</ script > ");
        }

        #endregion

        public void Upload_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Boolean fileOK = false;
                String path = Server.MapPath("~/UserUploads/Images/");
                if (PictureUpload.HasFile)
                {
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

                if (fileOK)
                {
                    try
                    {
                        PictureUpload.PostedFile.SaveAs(path
                            + PictureUpload.FileName);
                       String mImagePath = path.ToString() + PictureUpload.FileName.ToString();
                        Testlocation.Text = mImagePath;
                        ResultUploadImg.Text = "File uploaded!";
                    }
                    catch (Exception ex)
                    {
                        ResultUploadImg.Text = "File could not be uploaded.";
                    }
                }
                else
                {
                    ResultUploadImg.Text = "Not a extension of image";
                }
            }
        }

        protected void UploadAttachment_Click(object sender, EventArgs e)
        
        {
            if (IsPostBack)
            {
                String path = Server.MapPath("~/UserUploads/Attachments/");
                if (AttachmentUpload.HasFile)
                {
                    try
                    {
                        AttachmentUpload.PostedFile.SaveAs(path
                            + AttachmentUpload.FileName);
                        String mAttachPath = path.ToString() + AttachmentUpload.FileName.ToString();
                        TestlocationAtt.Text = mAttachPath;
                        ResultUploadAtta.Text = "File uploaded!";
                    }
                    catch (Exception ex)
                    {
                        ResultUploadAtta.Text = "File could not be uploaded.";
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
    
    
