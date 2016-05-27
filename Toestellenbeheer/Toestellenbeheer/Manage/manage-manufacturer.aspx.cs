﻿using MySql.Data.MySqlClient;
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
                if (manufacturer.IsAdded())
                {
                    var ShowSuccessAlert = new JSUtility();
                    ShowSuccessAlert.ShowAlert(this, "Added successfully", "alert-success");
                    bindManufacturerToGrid();
                }
                else
                {
                    var ShowFailedAlert = new JSUtility();
                    ShowFailedAlert.ShowAlert(this, "<strong>Warning, </strong>Manufacturer cannot be empty.", "alert-warning");
                }

            }
            catch (MySqlException ex)
            {
                var ShowFailedAlert = new JSUtility();
                var exeption = new MySqlExceptionHandler(ex, "Manufacturer");
                ShowFailedAlert.ShowAlert(this, "<strong>Warning, </strong>" + exeption.ReturnMessage(), "alert-warning");
            }
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
                var ShowSuccessAlert = new JSUtility();
                ShowSuccessAlert.ShowAlert(this, "Removed successfully", "alert-success");
                bindManufacturerToGrid();
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
            var updateManufacturer = new Manufacturer(ViewState["manufacturerName"].ToString());
            if (updateManufacturer.IsUpdated(txtManufacturerModifying.Text))
            {
                var ShowSuccessAlert = new JSUtility();
                ShowSuccessAlert.ShowAlert(this, "Update successfully", "alert-success");
                bindManufacturerToGrid();

            }
            else
            {
                var FailedAlert = new JSUtility();
                FailedAlert.ShowAlert(this, "Update failed!", "alert-danger");
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