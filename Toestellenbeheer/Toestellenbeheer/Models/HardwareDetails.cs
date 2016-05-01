using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Toestellenbeheer.Models
{
    public class HardwareDetails
    {
        protected void DownloadFile(object sender, EventArgs e)
        {
        }
        public void IframeDownload(LinkButton lnkDownloadB, string FilePath, HtmlIframe iframeControl, Page page)
        {
            lnkDownloadB.CommandArgument = FilePath;
            lnkDownloadB.Text = "Not downloading? Try again by clicking here.";
            iframeControl.Src = "~/Download.aspx";
            var openDownloadModal = new JSUtility("modalDownload");
            openDownloadModal.ModalShow(page);
        }
    }
}