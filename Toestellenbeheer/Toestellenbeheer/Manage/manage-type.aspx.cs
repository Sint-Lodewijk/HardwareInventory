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
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvType, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void bindTypeToGrid()
        {
            try
            {
                var type = new TypeName();
                type.BindGrvType(grvType);

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
                if (type.IsAdded())
                {
                    var ShowSuccessAlert = new JSUtility();
                    ShowSuccessAlert.ShowAlert(this, "<strong>Success,</strong> Type added successfully", "alert-success");
                    bindTypeToGrid();
                }
                else
                {
                    var ShowFailedAlert = new JSUtility();
                    ShowFailedAlert.ShowAlert(this, "<strong>Warning,</strong> Type cannot be empty.", "alert-warning");
                }
            }
            catch (MySqlException ex)
            {
                var ShowFailedAlert = new JSUtility();
                var exeption = new MySqlExceptionHandler(ex, "Type");
                ShowFailedAlert.ShowAlert(this, "<strong>Warning, </strong>" + exeption.ReturnMessage(), "alert-warning");
            }
        }
        protected void typeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["type"] = grvType.SelectedDataKey["type"].ToString();
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
                var ShowSuccessAlert = new JSUtility();
                ShowSuccessAlert.ShowAlert(this, "Removed successfully", "alert-success");
                bindTypeToGrid();
                ButtonPanel.Visible = false;

            }
            else
            {
                var FailedAlert = new JSUtility();
                FailedAlert.ShowAlert(this, "Remove failed!", "alert-danger");
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var updateType = new TypeName(ViewState["type"].ToString());
            if (updateType.IsUpdated(txtType.Text))
            {
                var ShowSuccessAlert = new JSUtility();
                ShowSuccessAlert.ShowAlert(this, "Update successfully", "alert-success");
                bindTypeToGrid();
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