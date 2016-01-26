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
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Web.Security;
using System.Drawing;

namespace Toestellenbeheer
{
    public partial class hardware_overview : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mysqlConnectie.Open();
                MySqlCommand bindToGrid = new MySqlCommand("SELECT pictureLocation, DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'purchaseDate', typeNr, manufacturerName, serialNr, internalNr, warranty, extraInfo, DATE_FORMAT(addedDate, '%Y-%m-%d') 'addedDate', attachmentLocation FROM hardware", mysqlConnectie);
                MySqlDataAdapter adpa = new MySqlDataAdapter(bindToGrid);
                bindToGrid.ExecuteNonQuery();
                bindToGrid.Dispose();
                DataSet ds = new DataSet();
                adpa.Fill(ds);
                HardwareOverviewGrid.DataSource = ds;
                HardwareOverviewGrid.DataBind();
                btnReturn.Visible = false;
                searchPanel.Visible = true;
            }
        }

        protected void Search(object sender, EventArgs e)
        {
            this.BindGrid();
            searchPanel.Visible = false;
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            //try { 
            string path = "../UserUploads/Attachments/";

            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(path + Path.GetFileName(filePath));
            Response.End();
            }
          //  catch (Exception ex)
            //{
           //     lblTotalQuery.Text = "Problem with downloading, please check if you added a attachment to the hardware.";
            //}
            //}
       
        private void BindGrid()
        {
            try
            {
               
                mysqlConnectie.Open();
                //GetImagePaths();
                String strSearchItem = drpSearchItem.SelectedValue.ToString();
                String strSearchText = txtSearch.Text.Trim();
                // string bindToGridCmd = "SELECT * FROM hardware WHERE @searchItem LIKE '%@searchText%'";
                MySqlCommand bindToGrid = new MySqlCommand("SELECT pictureLocation, DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'Purchase date', typeNr 'Type nr', manufacturerName 'Manufacturer', serialNr 'Serial Nr', internalNr 'Internal Nr', warranty 'Warranty', extraInfo 'Extra info', DATE_FORMAT(addedDate, '%Y-%m-%d') 'Added date', attachmentLocation FROM hardware WHERE " + strSearchItem + " LIKE '%" + strSearchText + "%';", mysqlConnectie);
               

                MySqlDataAdapter adpa = new MySqlDataAdapter(bindToGrid);
                bindToGrid.ExecuteNonQuery();
                bindToGrid.Dispose();
                DataSet ds = new DataSet();
                adpa.Fill(ds);
                HardwareOverviewGridSearch.DataSource = ds;
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
            this.BindGrid();
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

            MySqlCommand viewDetails = new MySqlCommand("SELECT pictureLocation, DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'Purchase date', typeNr 'Type nr', manufacturerName 'Manufacturer', serialNr 'Serial Nr', internalNr 'Internal Nr', warranty 'Warranty', extraInfo 'Extra info', DATE_FORMAT(addedDate, '%Y-%m-%d') 'Added date', attachmentLocation FROM hardware WHERE internalNr " +
                "LIKE '" + HardwareOverviewGrid.SelectedRow.Cells[2].Text + "'", mysqlConnectie);


            MySqlDataAdapter adpa = new MySqlDataAdapter(viewDetails);
            viewDetails.ExecuteNonQuery();
            viewDetails.Dispose();
            DataSet ds = new DataSet();
            adpa.Fill(ds);
            selectedRow.DataSource = ds;
            selectedRow.DataBind();
            HardwareOverviewGrid.Visible = false;
            btnReturn.Visible = true;

        }
        #endregion
        //DeleteEvent is used for the datakey - to get details without selecting the rows
        protected void details(object sender, GridViewDeleteEventArgs e)
        {
            try { 
            mysqlConnectie.Open();
            String strInternalNr = HardwareOverviewGrid.DataKeys[e.RowIndex].Value.ToString();

            MySqlCommand viewDetails = new MySqlCommand("SELECT pictureLocation, DATE_FORMAT(purchaseDate, '%Y-%m-%d') 'Purchase date', typeNr 'Type nr', manufacturerName 'Manufacturer', serialNr 'Serial Nr', internalNr 'Internal Nr', warranty 'Warranty', extraInfo 'Extra info', DATE_FORMAT(addedDate, '%Y-%m-%d') 'Added date', attachmentLocation FROM hardware WHERE internalNr " +
                "LIKE '" + strInternalNr + "'", mysqlConnectie);
            MySqlCommand f = new MySqlCommand("Delete hardware From hardware where licenseCode LIKE 'test'",mysqlConnectie);
            f.ExecuteNonQuery();
            f.Dispose();
            MySqlDataAdapter adpa = new MySqlDataAdapter(viewDetails);
            viewDetails.ExecuteNonQuery();
            viewDetails.Dispose();
            DataSet ds = new DataSet();
            adpa.Fill(ds);
            selectedRow.DataSource = ds;
            selectedRow.DataBind();
            HardwareOverviewGrid.Visible = false;
                btnReturn.Visible = true;
                searchPanel.Visible = false;
        }
            catch(MySqlException ex)
            {
                ShowMessage(ex.ToString());
            }
            }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            btnReturn.Visible = false;
            Server.Transfer("./hardware-overview.aspx");

        }
    }



}



