using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace PracowniaProgramowaniaProjekt1
{
    public static class FabrykaPdf
    {
        public static void stworz_pdf(Dictionary<string,List<string>> wyszukiwania, DirectoryInfo folder_do_zapisu_pdfow)
        {
            //cos niezbednego do pdf
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            PdfDocument pdf1 = new PdfDocument();
            pdf1.Info.Title = "Wyszukiwanie "+DateTime.Now.ToString();
            PdfPage strona1 = pdf1.AddPage();
            XGraphics grafika1 = XGraphics.FromPdfPage(strona1);
            grafika1.DrawString("Wyszukiwanie " + DateTime.Now.ToString(), new XFont("Times New Roman", 22), XBrushes.Black, new XRect(0,0,strona1.Width,100), XStringFormats.Center);
            grafika1.DrawString("Szukano " + wyszukiwania.Count + " słów", new XFont("Times New Roman", 14), XBrushes.Black, new XRect(0, 50, strona1.Width, 100), XStringFormats.Center);
            int margines_gorny = 150;
            int i = 1;
            foreach (string slowo in wyszukiwania.Keys)
            {
                grafika1.DrawString(i+". \""+slowo+"\"", new XFont("Times New Roman", 14, XFontStyle.Bold), XBrushes.Black, new XPoint(50, margines_gorny));
                margines_gorny += 25;
                if (margines_gorny >= strona1.Height - 25)
                {
                    strona1 = pdf1.AddPage();
                    grafika1 = XGraphics.FromPdfPage(strona1);
                    margines_gorny = 100;
                }
                foreach (string jezyk in wyszukiwania[slowo])
                {
                    grafika1.DrawString(jezyk, new XFont("Times New Roman", 12), XBrushes.Black, new XPoint(70, margines_gorny));
                    margines_gorny += 20;
                    if (margines_gorny >= strona1.Height - 25)
                    {
                        strona1 = pdf1.AddPage();
                        grafika1 = XGraphics.FromPdfPage(strona1);
                        margines_gorny = 100;
                    }
                }
                margines_gorny += 10;
                if (margines_gorny>=strona1.Height-25)
                {
                    strona1 = pdf1.AddPage();
                    grafika1 = XGraphics.FromPdfPage(strona1);
                    margines_gorny = 60;
                }
                i++;
            }
            pdf1.Save(folder_do_zapisu_pdfow.FullName + "\\Wyszukiwanie " + DateTime.Now.ToString().Replace(':', '.') + ".pdf");
        }
    }
}
