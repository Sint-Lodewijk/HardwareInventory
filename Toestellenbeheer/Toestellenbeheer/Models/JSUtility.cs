using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

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
        public void ModalShow(UpdatePanel udp)
        {

            udp.Update();
            ScriptManager.RegisterStartupScript(udp, udp.GetType(), "show", "$(function () { $('#" + ControlID + "').modal('show'); });", true);

        }
    }
}
