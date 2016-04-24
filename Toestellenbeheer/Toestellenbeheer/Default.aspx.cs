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
                    var modalInitialize = new JSUtility("initSetupModal");
                    modalInitialize.ModalShow(udpInitialize);
                }
            }
        }

        protected void btnType_Click(object sender, EventArgs e)
        {

        }

        protected void btnManufacturer_Click(object sender, EventArgs e)
        {

        }
    }
}