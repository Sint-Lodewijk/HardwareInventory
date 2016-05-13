using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toestellenbeheer.Models;
namespace Toestellenbeheer.Manage
{
    public partial class manage_manufacturer : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindManufacturerToGrid();
            }
        
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvManufacturer, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
                    }
        protected void bindManufacturerToGrid()
        {
            try
            {
                var manufacturer = new Manufacturer();
                manufacturer.BindManufacturer(grvManufacturer);
             
            }
            catch (MySqlException ex)
            {
                lblProblem.Text = ex.ToString();
            }
        }
        protected void btnAddManufacturer_Click(object sender, EventArgs e)
        {
            try
            {
                String strManufacturer = txtManufacturerName.Text.ToString();
                var manufacturer = new Manufacturer(strManufacturer);
                manufacturer.AddManufacturerToDatabase();
                bindManufacturerToGrid();
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
        protected void grvManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["manufacturerName"] = grvManufacturer.SelectedDataKey["manufacturerName"].ToString();
            ButtonPanel.Visible = true;
        }
        public void modalShow()
        {
            udpDetails.Update();
            ScriptManager.RegisterStartupScript(udpDetails, udpDetails.GetType(), "show", "$(function () { $('#modalManufacturer').modal('show'); });", true);
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            txtManufacturerModifying.Text = ViewState["manufacturerName"].ToString();
            manufacturerModalTitle.InnerText = "You are modifying manufacturer: " + ViewState["manufacturerName"].ToString();
            modalShow();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var deleteManufacturer = new Manufacturer(ViewState["manufacturerName"].ToString());
            if (deleteManufacturer.IsRemoved())
            {
                Session["SuccessInfo"] = "Successfully deleted manufacturer " + ViewState["manufacturerName"].ToString();
                Response.Redirect("~/Success.aspx");
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var updateManufacturer = new Manufacturer(ViewState["manufacturerName"].ToString());
            if (updateManufacturer.IsUpdated(txtManufacturerModifying.Text))
            {
                Session["SuccessInfo"] = "Successfully updated manufacturer " + txtManufacturerModifying.Text;
                Response.Redirect("~/Success.aspx");
            }
        }

        protected void grvPreRender(object sender, EventArgs e)
        {
            var gridview = sender as GridView;
            var Sort = new GridViewPreRender(gridview);
            Sort.SetHeader();
        }
    }
}