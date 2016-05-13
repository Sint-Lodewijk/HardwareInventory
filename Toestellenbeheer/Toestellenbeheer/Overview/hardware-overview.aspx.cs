using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using Toestellenbeheer.Models;
namespace Toestellenbeheer
{
    public partial class hardware_overview : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        /// <summary>
        /// Page Load - Bind the hardware immediately with gridview - HardwareOverviewGrid when page loads, do not change after postback pageload
        /// </summary>
        /// <param name="sender">Page Load</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindHardwareGRV();
                    if (grvHardware.Rows.Count == 0)
                    {
                        lblGridTotalResult.Text = "No result found in the database.";
                    }
                }
                catch (MySqlException ex)
                {
                    ShowMessage(ex.ToString());
                }
            }
            else if (iframeDownload.Src != "")
            {
                iframeDownload.Src = "";
            }
       

        }
        protected void BindHardwareGRV()
        {
            var hardware = new Hardware();
            hardware.BindGrvHardware(grvHardware);
        }
        protected void Search(object sender, EventArgs e)
        {
            this.Search();
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            try
            {
                Session["FilePath"] = "UserUploads/Attachments/";
                Session["FileName"] = (sender as LinkButton).CommandArgument;
                lnkDownloadB.CommandArgument = Session["FileName"].ToString();
                lnkDownloadB.Text = "Not downloading? Try again by clicking here.";
                iframeDownload.Src = "~/Download.aspx";
                var openDownloadModal = new JSUtility("modalDownload");
                openDownloadModal.ModalShow(this);
            }
            catch (Exception ex)
            {
                lblTotalQuery.Text = "Problem with downloading, please check if you added a attachment to the hardware." + ex.ToString();
            }
        }
        private void Search()
        {
            try
            {
                String strSearchItem = drpSearchItem.SelectedValue.ToString();
                String strSearchText = txtSearch.Text.Trim();
                var searchHardware = new Hardware();
                searchHardware.BindGrvSearch(strSearchItem, strSearchText, grvHardware);
                int intTotalResultReturned = grvHardware.Rows.Count;
                if (intTotalResultReturned == 0)
                {
                    lblTotalQuery.Text = "No entry found, please use a different keyword or switch between the searchtypes.";
                }
                else
                {
                    lblTotalQuery.Text = "Total result returned: " + intTotalResultReturned;
                }
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
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvHardware, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void details(object sender, EventArgs e)
        {
            try
            {
                mysqlConnectie.Open();
                String strInternalNr = grvHardware.SelectedDataKey["internalNr"].ToString();
                Session["internalNr"] = strInternalNr;
                var hardwareDetail = new JSUtility(modalHardware.ClientID);
                hardwareDetail.DetailsPopUp(strInternalNr, grvDetail, imgHardware, udpDetails, modalTitle);
            }
            catch (MySqlException ex)
            {
                ShowMessage(ex.ToString());
            }
        }
        protected void btnModifying_Click(object sender, EventArgs e)
        {
            Session["ToModifiedInternalNr"] = Session["internalNr"];
            Server.Transfer("./modify-hardware.aspx");
        }
        protected void updateInfo(String parameter, String value)
        {
            try
            {
                MySqlCommand updateInfo = new MySqlCommand("UPDATE hardware SET " + parameter + "='"
                    + value + "' WHERE internalNr ='" + lblInternalNr.Text.ToString() + "'", mysqlConnectie);
                updateInfo.ExecuteNonQuery();
                updateInfo.Dispose();
            }
            catch (MySqlException ex)
            {
                lblProblem.Text = ex.Number.ToString();
            }
        }
        protected void grvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void lnkDownloadB_Click(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile("../UserUploads/Attachments/" + Path.GetFileName(filePath));
            Response.End();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            var RemoveHardware = new Hardware(grvHardware.SelectedDataKey["internalNr"].ToString(), grvHardware.SelectedDataKey["serialNr"].ToString());
            RemoveHardware.DeleteHardware();
            Response.Redirect(Request.RawUrl);
        }

        protected void grvHardware_PreRender(object sender, EventArgs e)
        {
            var gridview = sender as GridView;
            var Sort = new GridViewPreRender(gridview);
            Sort.SetHeader();
        }
    }
}
