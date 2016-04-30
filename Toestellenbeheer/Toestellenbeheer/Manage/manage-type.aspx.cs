using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using Toestellenbeheer.Models;
namespace Toestellenbeheer.Manage
{
    public partial class manage_type : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindTypeToGrid();
            }
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(typeSelect, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void bindTypeToGrid()
        {
            try
            {
                var type = new TypeName();
                DataTable dt = type.ReturnDatatableType();
                typeSelect.DataSource = dt;
                typeSelect.DataBind();
            }
            catch (MySqlException ex)
            {
                lblProblem.Text = ex.ToString();
            }
        }
        public void modalShow()
        {
            udpDetails.Update();
            ScriptManager.RegisterStartupScript(udpDetails, udpDetails.GetType(), "show", "$(function () { $('#modalType').modal('show'); });", true);
        }
        protected void btnAddType_Click(object sender, EventArgs e)
        {
            try
            {
                String strType = typeName.Text.ToString();
                var type = new TypeName(strType);
                type.AddTypeToDatabase();
                bindTypeToGrid();
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
        protected void typeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["type"] = typeSelect.SelectedDataKey["type"].ToString();
            ButtonPanel.Visible = true;
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            txtType.Text = ViewState["type"].ToString();
            typeModalTitle.InnerText = "You are modifying type: " + ViewState["type"].ToString();
            modalShow();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var deleteType = new TypeName(ViewState["type"].ToString());
            if (deleteType.IsRemoved())
            {
                Session["SuccessInfo"] = "Successfully deleted type " + ViewState["type"].ToString();
                Response.Redirect("~/Success.aspx");
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var updateType = new TypeName(ViewState["type"].ToString());
            if (updateType.IsUpdated(txtType.Text))
            {
                Session["SuccessInfo"] = "Successfully updated type " + txtType.Text;
                Response.Redirect("~/Success.aspx");
            }
        }
    }
}