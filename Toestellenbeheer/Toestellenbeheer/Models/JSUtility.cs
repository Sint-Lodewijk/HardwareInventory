using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Toestellenbeheer.Models
{
    public class JSUtility
    {
        public string ControlID { get; set; }

        public JSUtility()
        {

        }
        public JSUtility(string strControlID)
        {
            ControlID = strControlID;
        }
        public void ModalShowUpdate(UpdatePanel udp)
        {

            udp.Update();
            ScriptManager.RegisterStartupScript(udp, udp.GetType(), "show", "$(function () { $('#" + ControlID + "').modal('show'); });", true);

        }
        public void ModalShow(UpdatePanel udp)
        {

            udp.Update();
            ScriptManager.RegisterStartupScript(udp, udp.GetType(), "show", "$(function () { $('#" + ControlID + "').modal('show'); });", true);

        }
        public void DetailsPopUp(string strInternalNr, GridView grvDetail, Image ImageID, UpdatePanel udpDetails)
        {
            var detailHardware = new Hardware(strInternalNr);
            DataTable dt = detailHardware.ReturnDatatableHardwareFromInternal();
            grvDetail.DataSource = dt;
            grvDetail.DataBind();
            ImageID.ImageUrl = "../UserUploads/Images/" + detailHardware.PicLocation();
            var detailModalShow = new JSUtility(ControlID);
            detailModalShow.ModalShowUpdate(udpDetails);
        }

    }
}
