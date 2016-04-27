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
        public void ModalShow(Page page)
        {

            ScriptManager.RegisterStartupScript(page, typeof(Page).GetType(), "show", "$(function () { $('#" + ControlID + "').modal('show'); });", true);

        }
        public void ShowJS(UpdatePanel udp)
        {

            udp.Update();
            ScriptManager.RegisterStartupScript(udp, udp.GetType(), "show", "$(function () { $('#" + ControlID + "').show; });", true);

        }
        public void CloseAlert(UpdatePanel udp)
        {

            udp.Update();
            ScriptManager.RegisterStartupScript(udp, udp.GetType(), "show", "$(function () { $('#" + ControlID + "').hide; });", true);

        }


        /// <summary>
        /// Detailses the pop up.
        /// </summary>
        /// <param name="strInternalNr">The string internal nr.</param>
        /// <param name="grvDetail">The gridviewcontrol to bind the detaildatas.</param>
        /// <param name="ImageID">The image identifier.</param>
        /// <param name="udpDetails">The updatepanel of details details.</param>
        /// <param name="htmlModalTitle">The modal title (HTML control).</param>
        /// <param name="strInternalNr">The string internal nr.</param>
        public void DetailsPopUp(string strInternalNr, GridView grvDetail, Image ImageID, UpdatePanel udpDetails, System.Web.UI.HtmlControls.HtmlGenericControl htmlModalTitle)
        {
            var detailHardware = new Hardware(strInternalNr);
            DataTable dt = detailHardware.ReturnDatatableHardwareFromInternal();
            grvDetail.DataSource = dt;
            grvDetail.DataBind();
            ImageID.ImageUrl = "../UserUploads/Images/" + detailHardware.PicLocation();
            htmlModalTitle.InnerText = "Details of " + strInternalNr; 
            var detailModalShow = new JSUtility(ControlID);
            detailModalShow.ModalShowUpdate(udpDetails);
        }

    }
}
