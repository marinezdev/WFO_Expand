using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Promotoria;
using iTextSharp.text.html;

namespace WFO.Funciones
{
    public static class ManejoArchivos
    {
        public static bool ConviertePDF(string srcFilename, string dstFilename)
        {
            bool resultado = false;
            //string srcFilename = Filename.Replace(@"\", @"\\");
            iTextSharp.text.Rectangle pageSize = null;

            using (var srcImage = new Bitmap(srcFilename))
            {
                pageSize = new iTextSharp.text.Rectangle(0, 0, srcImage.Width, srcImage.Height);
            }
            using (var ms = new MemoryStream())
            {
                var document = new iTextSharp.text.Document(pageSize, 0, 0, 0, 0);
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms).SetFullCompression();
                document.Open();
                var image = iTextSharp.text.Image.GetInstance(srcFilename);
                document.Add(image);
                document.Close();

                File.WriteAllBytes(dstFilename, ms.ToArray());
            }
            return resultado;
        }

        public static string Fusiona(List<string> lstArchivos, string pArhivoDst)
        {
            string resultado = "";
            try
            {
                var document = new Document();
                var ms = new FileStream(pArhivoDst, FileMode.Create, FileAccess.Write, FileShare.None);
                var pdfCopy = new PdfCopy(document, ms);
                document.Open();

                foreach (string archivo in lstArchivos)
                {
                    if (File.Exists(archivo))
                    {
                        var pdfReader = new PdfReader(archivo);
                        var n = pdfReader.NumberOfPages;
                        for (var page = 0; page < n;)
                        {
                            pdfCopy.AddPage(pdfCopy.GetImportedPage(pdfReader, ++page));
                        }
                        pdfCopy.FreeReader(pdfReader);
                        pdfReader.Close();
                    }
                }
                //pdfCopy.Flush();
                document.Close();
                //ms.WriteTo(new FileStream(pArhivoDst, FileMode.OpenOrCreate));
                resultado = "";
            }
            catch (Exception ex) { resultado = ex.Message; }
            return resultado;
        }

        public static string daDirectorio(string pDirBase, long pId)
        {
            string resultado = "";
            prop.directorio oDir = new prop.directorio();
            if (pId >= 0)
            {
                string hex = (pId / 512).ToString("x").PadLeft(6, '0');
                if (hex.Length == 6)
                {
                    oDir.abuelo = hex.Substring(0, 2);
                    oDir.padre = hex.Substring(2, 2);
                    oDir.hijo = hex.Substring(4, 2);
                }
                resultado = validaDirectorio(pDirBase, oDir);
            }
            return resultado;
        }

        private static string validaDirectorio(string pDirBase, prop.directorio pDir)
        {
            string resultado = "";
            try
            {
                if (!Directory.Exists(pDirBase)) { Directory.CreateDirectory(pDirBase); resultado = pDirBase; }
                resultado = pDirBase + pDir.abuelo + "\\" + pDir.padre + "\\" + pDir.hijo + "\\";
                if (!Directory.Exists(resultado)) { Directory.CreateDirectory(resultado); }
            }
            catch (Exception) { }

            return resultado;
        }

        public static string Adiciona(List<string> lstArchivos, string pArchivoFusion, string pArhivoDst, string pNmUsuario, string pNmSeparador, string pNmLogo)
        {
            string resultado = "";
            var document = new Document();
            try
            {
                var ms = new FileStream(pArhivoDst, FileMode.Create, FileAccess.Write, FileShare.None);
                var pdfCopy = new PdfCopy(document, ms);
                document.Open();

                if (File.Exists(pArchivoFusion))
                {
                    var pdfReader = new PdfReader(pArchivoFusion);
                    var n = pdfReader.NumberOfPages;
                    for (var page = 0; page < n;)
                    {
                        pdfCopy.AddPage(pdfCopy.GetImportedPage(pdfReader, ++page));
                    }
                    pdfCopy.FreeReader(pdfReader);
                    pdfReader.Close();
                }

                // Agrega el separador
                creaSeparador(pNmUsuario, pNmSeparador, pNmLogo);
                if (File.Exists(pNmSeparador))
                {
                    var pdfReader = new PdfReader(pNmSeparador);
                    var n = pdfReader.NumberOfPages;
                    for (var page = 0; page < n;)
                    {
                        pdfCopy.AddPage(pdfCopy.GetImportedPage(pdfReader, ++page));
                    }
                    pdfCopy.FreeReader(pdfReader);
                    pdfReader.Close();
                }

                foreach (string archivo in lstArchivos)
                {
                    if (File.Exists(archivo))
                    {
                        var pdfReader = new PdfReader(archivo);
                        var n = pdfReader.NumberOfPages;
                        for (var page = 0; page < n;)
                        {
                            pdfCopy.AddPage(pdfCopy.GetImportedPage(pdfReader, ++page));
                        }
                        pdfCopy.FreeReader(pdfReader);
                        pdfReader.Close();
                    }
                }
                document.Close();
                resultado = "";
            }
            catch (Exception ex) { resultado = ex.Message; document.Close(); }
            return resultado;
        }

        public static void creaSeparador(string pUsuario, string pNmSeparador, string pNmLogo)
        {
            //Document doc = new Document(PageSize.LETTER);
            //var output = new FileStream(pNmSeparador, FileMode.Create);
            //var writer = PdfWriter.GetInstance(doc, output);

            // Medidas de la hoja A4
            var doc = new Document(PageSize.A4, 30, 30, 50, 50);
            doc.SetMargins(30, 30, 80, 50);
            var output = new FileStream(pNmSeparador, FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, output);


            PdfPCell cell = null;

            // COLOCA ENCABEZADO Y PIE DE PAGINA
            writer.PageEvent = new PDFFooter();

            doc.Open();

            iTextSharp.text.Font fNegroBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fNegro = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 15, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            if (File.Exists(pNmLogo))
            {
                var logo = iTextSharp.text.Image.GetInstance(pNmLogo);
                logo.SetAbsolutePosition(200, 500);
                logo.ScaleAbsoluteHeight(80);
                logo.ScaleAbsoluteWidth(200);
                doc.Add(logo);
            }


            PdfPTable table = new PdfPTable(10);
            table.WidthPercentage = 100;
            table.SpacingBefore = 100F;

            cell = new PdfPCell(new Phrase("\n", fNegroBold));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("\n", fNegroBold));
            cell.BackgroundColor = WebColors.GetRGBColor("#226B9A");
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("\n\n\n\n\n\n\n\n DOCUMENTOS ADICIONADOS", fNegroBold));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("\n USUARIO: ", fNegroBold));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(pUsuario, fNegro));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;


            cell = new PdfPCell(new Phrase("\n FECHA DE ALTERACIÓN:", fNegroBold));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fNegro));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n", fNegroBold));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("\n", fNegroBold));
            cell.BackgroundColor = WebColors.GetRGBColor("#226B9A");
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            doc.Add(table);
            doc.Close();




            //doc.Add(Chunk.NEWLINE);
            //doc.Add(new Paragraph("DOCUMENTOS ADICIONADOS", fNegroBold));
            //doc.Add(new Paragraph(pUsuario, fNegro));
            //doc.Add(new Paragraph(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fNegro));

            //PdfPTable table1 = new PdfPTable(1);
            //table1.DefaultCell.BorderColor = BaseColor.WHITE; 
            //table1.WidthPercentage = 50;
            //PdfPCell cell11 = new PdfPCell();
            //cell11.Colspan = 1;
            //cell11.AddElement(new Paragraph(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fNegro));
            //cell11.VerticalAlignment = Element.ALIGN_BOTTOM;
            //cell11.HorizontalAlignment = Element.ALIGN_CENTER;
            //cell11.FixedHeight = 300.0f;
            //table1.AddCell(cell11);
            //doc.Add(table1);

            doc.Close();
        }

        public class PDFFooter : PdfPageEventHelper
        {
            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);
                PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                tabFot.TotalWidth = 540F;

                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("c:\\logo_grap.jpg");
                PdfPCell imageCell = new PdfPCell(jpg);
                imageCell.Colspan = 10; // either 1 if you need to insert one cell
                imageCell.Border = 0;
                imageCell.MinimumHeight = 50f;
                imageCell.FixedHeight = 50f;
                imageCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabFot.AddCell(imageCell);
                imageCell = null;
                tabFot.WriteSelectedRows(0, -1, 26, document.Top + 50, writer.DirectContent);
            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                iTextSharp.text.Font TextFooter = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                PdfPCell cell;
                tabFot.TotalWidth = 540F;

                cell = new PdfPCell(new Phrase("MetLife México, S.A., Blvd. Manuel Ávila Camacho No.32, pisos SKL, 14 a 20 y PH. Col Lomas de Chapultepec, CP. 11000, Delegación Miguel Hidalgo, Ciudad de México, Tel. 5328-7000 ó lada sin costo 01-800-00 METLIFE", TextFooter));
                cell.Colspan = 10;
                cell.FixedHeight = 15f;
                cell.MinimumHeight = 15f;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = 0;
                tabFot.AddCell(cell);
                cell = null;

                tabFot.WriteSelectedRows(0, -1, 30, document.Bottom, writer.DirectContent);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }
    }
}
