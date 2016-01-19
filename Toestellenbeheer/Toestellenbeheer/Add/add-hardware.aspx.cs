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

namespace Toestellenbeheer.Manage
{
    public partial class add_hardware : System.Web.UI.Page
    {
        #region MySqlConnection Connection and Page Lode
        //MySqlConnection mysqlConnectie = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            try { 
            getTypeList();
            }
            catch(Exception ex)
            {
                txtResultUpload.Text = ex.ToString();
            }
        }
        protected void getTypeList()
        {
            if (!IsPostBack) { 
            DataSet Type = new DataSet();
            string listTypeOut = "select type from type";
            MySqlCommand listOutType = new MySqlCommand(listTypeOut, mysqlConnectie);
            using (MySqlDataAdapter data1 = new MySqlDataAdapter(listOutType))
                data1.Fill(Type, "type");
            typeList.DataSource = Type.Tables["type"];
            typeList.DataBind();

            typeList.DataTextField = "type";
            typeList.DataValueField = "type";
            typeList.DataBind();
            mysqlConnectie.Close();
            }
        }
        #endregion
        #region Insert Data
        protected void Submit_Click(object sender, EventArgs e)
        {
            String strSerialNr = Serialnr.Text.ToString();
            String strWarrantyInfo = warrantyInfo.Text.ToString();
            String strInternalNr = internalNr.Text.ToString();
            String strExtrainfo = extraInfo.Text.ToString();
            String mImagePath = Testlocation.Text.ToString();
            String mAttPath = TestlocationAtt.Text;
            int intSelectedTypeIndex = typeList.SelectedIndex;
            String strSelectedManufacturer = manufacturerList.SelectedItem.ToString();
            //testSelected.Text = strSelectedType;
            String dtePurchaseYear = txtDatepicker.Text.Substring(6);
            String dtePurchaseMonth = txtDatepicker.Text.Substring(0, 2);
            String dtePurchaseDay = txtDatepicker.Text.Substring(3, 2);
            String dtePurchaseDate = dtePurchaseYear.ToString() + '-' + dtePurchaseMonth.ToString() + '-' + dtePurchaseDay.ToString();
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
                if (strInternalNr == "")
                {
                    txtResultUpload.Text = "Internal number is required, please add this!";

                }
                else if (strSerialNr == "")
                {
                    txtResultUpload.Text = "Serial number is required, please add this";
                }
               
                else
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

                    //Use the mysql to connect the database
                    mysqlConnectie.Open();
                    MySqlCommand addHIEP = new MySqlCommand("Insert into hardware (purchaseDate, serialNr, internalNr,  warranty, extraInfo, manufacturerName, addedDate, pictureLocation, typeNr, attachmentLocation) values (@purchaseDate, @serialNr, @internalNr,  @warranty, @extraInfo, @manufacturerName, @addedDate, @pictureLocation, @typeNr, @attachmentLocation)", mysqlConnectie);

                    //add parameters (assaign the values to the column.)
                    addHIEP.Parameters.AddWithValue("@purchaseDate", dtePurchaseDate);
                    addHIEP.Parameters.AddWithValue("@serialNr", strSerialNr);
                    addHIEP.Parameters.AddWithValue("@internalNr", strInternalNr);
                    addHIEP.Parameters.AddWithValue("@warranty", strWarrantyInfo);
                    addHIEP.Parameters.AddWithValue("@extraInfo", strExtrainfo);
                    //addHIEP.Parameters.AddWithValue("@attLocation", mAttPath);
                    // addHIEP.Parameters.AddWithValue("@addedDate", dteAddedDate);
                    addHIEP.Parameters.AddWithValue("@manufacturerName", strSelectedManufacturer);

                    addHIEP.Parameters.AddWithValue("@addedDate", dteAddedDate);
                    addHIEP.Parameters.AddWithValue("@pictureLocation", mImagePath);

                    addHIEP.Parameters.AddWithValue("@typeNr", intSelectedTypeIndex);
                    addHIEP.Parameters.AddWithValue("@attachmentLocation", TestlocationAtt.Text);

                    addHIEP.Parameters.AddWithValue("@nameAD", dteAddedDate);


                    addHIEP.ExecuteNonQuery();
                    addHIEP.Dispose();
                    txtResultUpload.Text = "Congratulations! The device with a internal nr: " + "<span style=\"color:red\">" + strInternalNr + "</span>" +
                       " and a serial nr: " + "<span style=\"color:red\">" + strSerialNr + "</span>" + " successfully added to the database.";

                    
                    mysqlConnectie.Close();
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number.ToString() == "1062")
                {
                    //testLabel.Text = ex.Message.ToString() + ", please check your input.";
                    txtResultUpload.Text = "The device with a internal nr: " + "<span style=\"color:red\">" + strInternalNr + "</span>" +
                        " and a serial nr: " + "<span style=\"color:red\">" + strSerialNr + "</span>" + " already exist in de database.";

                }
                else { ShowMessage(ex.Message); }


            }
            //test the value - removeable
            test.Text = dtePurchaseDate.ToString();
        }

      
        void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<scriptlanguage = 'javascript'> alert('" + msg + "');</ script > ");
        }

        #endregion
        //Upload the selected file and return the right path
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
                        String mImagePath = PictureUpload.FileName.ToString();
                        Testlocation.Text = mImagePath;
                        ResultUploadImg.Text = "File uploaded!";

                        /*mysqlConnectie.Open();
                        
                        MySqlCommand addPicture = new MySqlCommand("Insert into fileLocation (pictureLocation) values (@pictureLocation)", mysqlConnectie);
                        addPicture.Parameters.AddWithValue("@pictureLocation", mImagePath);
                        addPicture.ExecuteNonQuery();
                        addPicture.Dispose();*/
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
        //Upload the attachment and return the path.

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
                        String mAttachPath = AttachmentUpload.FileName.ToString();
                        TestlocationAtt.Text = AttachmentUpload.FileName.ToString();
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
      //  protected void typeList_ChangedIndex(object sender, EventArgs e)
       // {
      //      int intTypeSelected = typeList.SelectedIndex;
      //  }
    }
}


