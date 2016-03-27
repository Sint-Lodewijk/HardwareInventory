﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toestellenbeheer.Models;

namespace Toestellenbeheer.Archive
{
    public partial class people_history : System.Web.UI.Page
    {
        MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getUserFromAD();
            }
        }
        protected void grvPeopleAD_SelectedIndexChanged(object sender, EventArgs e)
        {
            String strNameAD = grvPeopleAD.SelectedRow.Cells[1].Text.ToString();
            getHardwareFromNameAD(strNameAD);
        }
        protected void getHardwareFromNameAD(String nameAD)
        {
            try
            {
                mysqlConnectie.Open();
                MySqlCommand bindToGrid = new MySqlCommand("SELECT serialNr, internalNr, DATE_FORMAT(assignedDate, '%Y-%m-%d') 'assignedDate',DATE_FORMAT(returnedDate, '%Y-%m-%d') 'returnedDate' FROM archive JOIN people on archive.eventID = people.eventID WHERE nameAD = '" + nameAD + "'", mysqlConnectie);
                MySqlDataAdapter adpa = new MySqlDataAdapter(bindToGrid);
                bindToGrid.ExecuteNonQuery();
                bindToGrid.Dispose();
                DataSet ds = new DataSet();
                adpa.Fill(ds);
                grvHardwareOfPeople.DataSource = ds;
                grvHardwareOfPeople.DataBind();
                mysqlConnectie.Close();
            }
            catch (MySqlException ex)
            {

            }
        }

        protected void getUserFromAD()
        {
            try
            {
                GetADUser get = new GetADUser;
                DataTable dt = get.returnDataTable();
                grvPeopleAD.DataSource = dt;

                grvPeopleAD.DataBind();
            }
            catch (Exception ex)
            {
                lblProblem.Text = "An problem has occured, due: " + ex.Message;
            }
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvPeopleAD.PageIndex = e.NewPageIndex;
            grvPeopleAD.DataBind();
        }

        protected void grvPeopleAD_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvPeopleAD, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        protected void grvPeopleAD_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }
    }
}