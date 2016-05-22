using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Toestellenbeheer.Models
{
    public class PDFHandler : Hardware
    {
        public PDFHandler()
        {

        }
        public PDFHandler(string strInternalNr, string strSerialNr, string strManufacturer, string strType, string strModel)
        {
            InternalNr = strInternalNr;
            SerialNr = strSerialNr;
            ManufacturerName = strManufacturer;
            TypeName = strType;
            ModelName = strModel;
        }
        public void CreatePDF(string FileName, string DocTitle, string DocType, string CurrentLocation, string ADUserName)
        {
            var companyFont = FontFactory.GetFont("Segoe UI", 18, Font.BOLD, BaseColor.ORANGE);
            var titleFont = FontFactory.GetFont("Segoe UI", 18, Font.BOLD);
            var subTitleFont = FontFactory.GetFont("Segoe UI", 14, Font.BOLD);
            var boldTableFont = FontFactory.GetFont("Segoe UI", 12, Font.BOLD);
            var endingMessageFont = FontFactory.GetFont("Segoe UI", 10, Font.ITALIC);
            var bodyFont = FontFactory.GetFont("Segoe UI", 12, Font.NORMAL);
            Directory.CreateDirectory(CurrentLocation);

            var filestream = new FileStream(CurrentLocation + FileName + ".pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            var pdfdoc = new Document(PageSize.A4, 36, 36, 36, 36);
            var pdfwriter = PdfWriter.GetInstance(pdfdoc, filestream);
            pdfdoc.Open();
            var companyP = new Paragraph(SetupFile.Company.CompanyName, companyFont);
            companyP.Alignment = Element.ALIGN_RIGHT;
            companyP.SpacingAfter = 50;
            pdfdoc.Add(companyP);
            var titleP = new Paragraph(DocTitle, titleFont);
            titleP.Alignment = Element.ALIGN_LEFT;
            titleP.SpacingAfter = 60;
            var subtitleP = new Paragraph("Details of the " + DocType + "ed hardware", subTitleFont);
            subtitleP.SpacingAfter = 40;
            pdfdoc.Add(titleP);
            pdfdoc.Add(subtitleP);
            PdfPTable hardwaretable = new PdfPTable(2);
            hardwaretable.WidthPercentage = 100;
            hardwaretable.DefaultCell.Border = 0;
            hardwaretable.AddCell("Manufacturer");
            hardwaretable.AddCell(ManufacturerName);
            hardwaretable.AddCell("Type");
            hardwaretable.AddCell(TypeName);
            hardwaretable.AddCell("Model");
            hardwaretable.AddCell(ModelName);
            hardwaretable.AddCell("Internal Nr ");
            hardwaretable.AddCell(InternalNr);
            hardwaretable.AddCell("Serial Nr");
            hardwaretable.AddCell(SerialNr);
            hardwaretable.SpacingAfter = 35;
            pdfdoc.Add(hardwaretable);
            var hintP = new Paragraph("Please keep this contract carefully!", subTitleFont);
            hintP.SpacingAfter = 35;
            pdfdoc.Add(hintP);

            var clarifyP = new Paragraph("I received the hardware described above in a working condition.", subTitleFont);
            clarifyP.SpacingBefore = 130;
            clarifyP.SpacingAfter = 20;
            pdfdoc.Add(clarifyP);
            PdfPTable signtable = new PdfPTable(3);
            signtable.WidthPercentage = 100;
            signtable.DefaultCell.Border = 0;
            signtable.AddCell("Date");
            signtable.AddCell("Signature assignee");
            signtable.AddCell(SetupFile.Company.CompanyName);

            signtable.AddCell(DateTime.Now.Date.ToString("dd/MM/yyyy"));

            signtable.AddCell(ADUserName);
            signtable.AddCell("");
            signtable.SpacingAfter = 30;

            pdfdoc.Add(signtable);
            pdfdoc.Close();

        }

    }
}