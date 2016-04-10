using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Toestellenbeheer.Models;
using System.Data;

namespace Toestellenbeheer.Overview
{
    public partial class modify_hardware : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ToModifiedInternalNr"] != null)
                {
                    DropDownList ddlType = (DropDownList)grvModifyHardware.FindControl("ddlType");
                    string strInternalNrModify = Session["ToModifiedInternalNr"].ToString();
                    var modifyHardwareInfo = new Hardware(strInternalNrModify);
                    DataTable dt = modifyHardwareInfo.ReturnDatatableHardwareFromInternal();
                    grvModifyHardware.DataSource = dt;

                    grvModifyHardware.DataBind();

                }

            }
        }



        protected void btnModify_Click(object sender, EventArgs e)
        {

            DropDownList ddlType = grvModifyHardware.Rows[0].FindControl("ddlType") as DropDownList;
            string strType = ddlType.SelectedValue.ToString();

            DropDownList ddlManufacturer = grvModifyHardware.Rows[0].FindControl("ddlManufacturer") as DropDownList;
            string strManufacturer = ddlManufacturer.SelectedValue.ToString();

            TextBox txtPurchaseDate = grvModifyHardware.Rows[0].FindControl("txtPDate") as TextBox;
            string strPDate = txtPurchaseDate.Text;

            TextBox txtExtra = grvModifyHardware.Rows[0].FindControl("txtExtra") as TextBox;
            string strExtra = txtExtra.Text;

            TextBox txtWarranty = grvModifyHardware.Rows[0].FindControl("txtWarranty") as TextBox;
            string strWarranty = txtWarranty.Text;

            TextBox txtModelNr = grvModifyHardware.Rows[0].FindControl("txtModelNr") as TextBox;
            string strModelNr = txtModelNr.Text;
            var updateHardware = new Hardware(strExtra, Session["ToModifiedInternalNr"].ToString(), strManufacturer, strModelNr, strPDate, strWarranty, strType);
            updateHardware.UpdateHardware();
        }
    }
}