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
                    var hardware = new Hardware();
                    DataTable dt = hardware.ReturnDatatableAllHardware();
                    HardwareOverviewGrid.DataSource = dt;
                    HardwareOverviewGrid.DataBind();
                    btnReturn.Visible = false;
                    searchPanel.Visible = true;
                    if (HardwareOverviewGrid.Rows.Count == 0)
                    {
                        lblGridTotalResult.Text = "No result found in the database.";
                    }
                }
                catch (MySqlException ex)
                {
                    ShowMessage(ex.ToString());
                }
            }
        }

        protected void Search(object sender, EventArgs e)
        {
            this.Search();
            searchPanel.Visible = false;
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            try
            {
                string path = "../UserUploads/Attachments/";

                string filePath = (sender as LinkButton).CommandArgument;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.WriteFile(path + Path.GetFileName(filePath));
                Response.End();
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
                DataTable dt = searchHardware.ReturnSearchDatatable(strSearchItem, strSearchText);

                HardwareOverviewGridSearch.DataSource = dt;
                HardwareOverviewGridSearch.DataBind();

                int intTotalResultReturned = HardwareOverviewGridSearch.Rows.Count;
                if (intTotalResultReturned == 0)
                {
                    lblTotalQuery.Text = "No entry found, please use a different keyword or switch between the searchtypes.";
                }
                else
                {
                    lblTotalQuery.Text = "Total result returned: " + intTotalResultReturned;

                }
                mysqlConnectie.Close();
                HardwareOverviewGrid.Visible = false;
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

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            HardwareOverviewGrid.PageIndex = e.NewPageIndex;
            this.Search();
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(HardwareOverviewGrid, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        #region Not used
        protected void lnkShowMoreInfo_Click(object sender, EventArgs e)
        {

            mysqlConnectie.Open();
            String strInternalNr = HardwareOverviewGrid.SelectedRow.Cells[2].ToString();
            Hardware detail = new Hardware(strInternalNr);
            DataTable dt = detail.ReturnDatatableHardwareFromInternal();
            
            selectedRow.DataSource = dt;
            selectedRow.DataBind();
            HardwareOverviewGrid.Visible = false;
            btnReturn.Visible = true;

        }
        #endregion
        //DeleteEvent is used for the datakey - to get details without selecting the rows
        protected void details(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                mysqlConnectie.Open();
                String strInternalNr = HardwareOverviewGrid.DataKeys[e.RowIndex].Value.ToString();
                Session["internalNr"] = strInternalNr;
                lblInternalNr.Text = strInternalNr;
                var hardwareDetail = new Hardware(strInternalNr);
                DataTable dt = hardwareDetail.ReturnDatatableHardwareFromInternal();
                selectedRow.DataSource = dt;
                selectedRow.DataBind();
                HardwareOverviewGrid.Visible = false;
                btnReturn.Visible = true;
                searchPanel.Visible = false;
                btnModifying.Visible = true;
                mysqlConnectie.Close();
            }
            catch (MySqlException ex)
            {
                ShowMessage(ex.ToString());
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            btnReturn.Visible = false;
            Server.Transfer("./hardware-overview.aspx");


        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            mysqlConnectie.Open();

            String strInternalNr = lblInternalNr.Text.ToString();
            String emptyString = "";

            if (txtDatepicker.Text != emptyString)
            {
                String dtePurchaseYear = txtDatepicker.Text.Substring(6);
                String dtePurchaseMonth = txtDatepicker.Text.Substring(0, 2);
                String dtePurchaseDay = txtDatepicker.Text.Substring(3, 2);
                String dtePurchaseDate = dtePurchaseYear.ToString() + '-' + dtePurchaseMonth.ToString() + '-' + dtePurchaseDay.ToString();
                updateInfo("purchaseDate", dtePurchaseDate);
            }
            if (modelNr.Text.Trim() != emptyString)
            {
                updateInfo("modelNr", modelNr.Text.Trim());
            }
            if (warrantyInfo.Text.Trim() != emptyString)
            {
                updateInfo("warranty", warrantyInfo.Text.Trim());
            }
            /*if (internalNr.Text.Trim() != emptyString)
            {
                updateInfo("internalNr", internalNr.Text.Trim());
            }*/
            mysqlConnectie.Close();

            selectedRow.Visible = true;
            mysqlConnectie.Open();
            strInternalNr = lblInternalNr.Text;
            var getHardware = new Hardware(strInternalNr);
            DataTable dt = getHardware.ReturnDatatableHardwareFromInternal();
            selectedRow.DataSource = dt;
            selectedRow.DataBind();
            mysqlConnectie.Close();
            modifyPanel.Visible = false;
            btnModifying.Visible = true;
        }
        protected void btnModifying_Click(object sender, EventArgs e)
        {
            Server.Transfer("./modify-hardware.aspx");
            modifyPanel.Visible = true;
            selectedRow.Visible = false;
            btnModifying.Visible = false;
            getTypeList();
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
        protected void getTypeList()
        {

            var type = new TypeName();
            DataTable dt = type.ReturnDatatableType();
            typeList.DataSource = dt;
            typeList.DataBind();
            mysqlConnectie.Close();

        }

        protected void typeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            mysqlConnectie.Open();
            updateInfo("typeNr", typeList.SelectedIndex.ToString());
            mysqlConnectie.Close();
            lblModifiedType.Text = "The type has been updated!";
        }

        protected void manufacturerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (manufacturerList.SelectedIndex != 0)
            {
                mysqlConnectie.Open();

                updateInfo("manufacturerName", manufacturerList.SelectedValue.ToString());
                mysqlConnectie.Close();
                lblModifiedManufacturer.Text = "The manufacturer has been updated!";
            }
            else
            {
                lblModifiedManufacturer.Text = "When you select a manufacturer, this will update automatically into the database.";

            }
        }
    }



}



