﻿using Microsoft.AspNet.Identity;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Toestellenbeheer.Models;
namespace Toestellenbeheer.Users
{
    public partial class my_hardware : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    String strUserName = Context.User.Identity.GetUserName();
                    var uHardware = new Models.Hardware();
                    DataTable dt = uHardware.ReturnUserHardware(strUserName);
                    grvMyHardware.DataSource = dt;
                    grvMyHardware.DataBind();
                    mysqlConnectie.Close();
                    if (grvMyHardware.Rows.Count == 0)
                    {
                        lblStatus.Text = "You do not have lend any hardware yet.";
                    }
                }
                catch (MySqlException ex)
                {
                    lblError.Text = ex.ToString();
                }
            }
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvMyHardware, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void grvMyHardware_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strInternalNr = grvMyHardware.SelectedDataKey["internalNr"].ToString();
            var picLoc = new Models.Hardware(strInternalNr);
            picDetail.ImageUrl = "../UserUploads/Images/" + picLoc.PicLocation();
            var picModal = new JSUtility("hardwareImageModal");
            picModal.ModalShowUpdate(udpDetails);
        }
    }
}