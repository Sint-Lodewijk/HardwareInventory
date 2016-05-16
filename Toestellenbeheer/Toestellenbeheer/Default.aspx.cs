﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toestellenbeheer.Models;
namespace Toestellenbeheer
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["success"] == "truncate")
                {
                    var ShowTruncateSuccess = new JSUtility();
                    ShowTruncateSuccess.ShowAlert(this, "<strong>Success!</strong> Database truncate successful, please initialize the database or <a href=\""+ ResolveUrl("~/Manage/manage-database#restore") +"\"> Restore </a> the previous backup.", "alert-success");
                }
                var totalType = new TypeName();
                var totalManufacturer = new Manufacturer();
                if (totalType.CountType() == 0 || totalManufacturer.CountManufacturer() == 0)
                {
                    initializeSetupModal();
                    lblTypeAvailible.InnerText = totalType.CountType().ToString();
                    lblAvailibleManufacturer.InnerText = totalManufacturer.CountManufacturer().ToString();
                }
            }
        }
        private void initializeSetupModal()
        {
            var modalInitialize = new JSUtility("initSetupModal");
            modalInitialize.ModalShowUpdate(udpInitialize);
        }
        protected void btnType_Click(object sender, EventArgs e)
        {
            var type = new TypeName(txtType.Value);
            type.AddTypeToDatabase();
            lblTypeAvailible.InnerText = type.CountType().ToString();
            NextSlide("carousel-init", 1);
            initializeSetupModal();
        }
        private void NextSlide(string strSlideControl, int times)
        {
            for (int i = 0; i < times; ++i)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Next", "$(function () { $('#" + strSlideControl + "').carousel(" + times + "); });", true);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "DeleteFade", "$(function () { $('#initSetupModal').removeClass('fade') });", true);

        }
        protected void btnManufacturer_Click(object sender, EventArgs e)
        {
            var manufacturer = new Manufacturer(txtManufacturer.Value);
            manufacturer.AddManufacturerToDatabase();
            lblAvailibleManufacturer.InnerText = manufacturer.CountManufacturer().ToString();
            NextSlide("carousel-init", 2);
            initializeSetupModal();
        }
    }
}