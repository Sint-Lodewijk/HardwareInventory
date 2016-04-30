using System;
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
            initializeSetupModal();
            NextSlide("carousel-init", 1);
        }
        private void NextSlide(string strSlideControl, int times)
        {
            for (int i = 0; i < times; ++i)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Next", "$(function () { $('#" + strSlideControl + "').carousel(" + times + "); });", true);
            }
        }
        protected void btnManufacturer_Click(object sender, EventArgs e)
        {
            var manufacturer = new Manufacturer(txtManufacturer.Value);
            manufacturer.AddManufacturerToDatabase();
            lblAvailibleManufacturer.InnerText = manufacturer.CountManufacturer().ToString();
            initializeSetupModal();
            NextSlide("carousel-init", 2);
        }
    }
}