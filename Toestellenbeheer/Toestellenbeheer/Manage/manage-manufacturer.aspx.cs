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
                DataTable dt = manufacturer.ReturnDatatableManufacturer();
                grvManufacturer.DataSource = dt;
                grvManufacturer.DataBind();

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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ModifyPanel.Visible = true;
            txtManufacturerModifying.Text = ViewState["manufacturerName"].ToString();
            ModifyPopUP.Show();
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
    }

}