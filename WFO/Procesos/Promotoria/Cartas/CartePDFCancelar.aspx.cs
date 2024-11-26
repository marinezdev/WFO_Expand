using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System.Data;
using System.IO;

using static iTextSharp.text.Font;
using prop = WFO.Propiedades.Procesos.Promotoria;
using System.Globalization;

namespace WFO.Procesos.Promotoria.Cartas
{
    public partial class CartePDFCancelar : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Promotoria.TramitesPromotoria tramitesPromotoria = new Negocio.Procesos.Promotoria.TramitesPromotoria();
        WFO.Negocio.Procesos.Promotoria.Cartas cartas = new Negocio.Procesos.Promotoria.Cartas();

        protected void Page_Load(object sender, EventArgs e)
        {
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

            if (urlCifrardo.Result)
            {
                //int IdTramite = Convert.ToInt32(Request.Params["Id"].ToString());
                int IdTramite = Convert.ToInt32(urlCifrardo.IdTramite);
                manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];

                List<prop.TramitesPromotoria> tramitesPromotorias = tramitesPromotoria.ConsultaTramitesPromotoria(manejo_sesion.Usuarios.IdUsuario, IdTramite);

                if (tramitesPromotorias.Count > 0)
                {
                    List<prop.carta> carta = cartas.Consulta_Carta(IdTramite, "Cancelado", 1);
                    if (carta.Count > 0)
                    {
                        foreach (prop.carta DatosCarta in carta)
                        {
                            MuestraPDF(DatosCarta);
                        }
                    }
                    else
                    {
                        NoExistente();
                    }
                }
                else
                {
                    NoExistente();
                }
            }
        }

        private void MuestraPDF(prop.carta carta)
        {
            CultureInfo culture = new CultureInfo("es-MX");
            // Medidas de la hoja A4
            var document = new Document(PageSize.A4, 30, 30, 50, 50);
            document.SetMargins(30, 30, 80, 50);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            PdfPCell cell = null;

            // FORMATOS DE TEXTOS 
            Font Text = new Font(FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
            Font TextFooter = new Font(FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.BLACK);
            Font Negrita = new Font(FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);

            // COLOCA ENCABEZADO Y PIE DE PAGINA
            writer.PageEvent = new PDFFooter();

            // RECHAZOS 
            List<prop.motivosRechazo> motivos = cartas.Consulta_MotivosRechazo(carta.Id);
            // OBSERVACIONES 
            List<prop.bitacora> ObservacionesBitacora = cartas.Consulta_Observaciones_Bitacora(carta.Id);


            // DEFINICACION DE VARIABLES UTILIZADAS EN PLANTILLA
            string Fecha = "Ciudad de México, " + carta.FechaTermino.ToString(culture.DateTimeFormat.LongDatePattern, culture);
            string TipoTramite = carta.TipoTramite;
            string Ramo = carta.Operacion;
            string Folio = carta.FolioCompuesto;
            string Titular = "-";

            //if (carta.Titular.Length > 5)
            //{
            //    Titular = carta.Titular;
            //}
            //else
            //{
            //    Titular = carta.Contratante;
            //}

            string Poliza = carta.IdSisLegados;
            string InicioVigencia = carta.FechaTermino.ToString(culture.DateTimeFormat.LongDatePattern, culture);
            //string Productos = carta.Producto;
            string Agente = carta.Agente;
            string Promotoria = carta.Promotoria;

            document.Open();

            PdfPTable table = new PdfPTable(10);
            table.WidthPercentage = 100;
            table.SpacingBefore = 20;

            // ESPACIO EN BLANCO
            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 20F;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // FECHA 
            cell = new PdfPCell(new Phrase(Fecha, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 50f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // TIPO TRAMITE
            cell = new PdfPCell(new Phrase("Tipo de Trámite: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(TipoTramite, Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // RAMO
            cell = new PdfPCell(new Phrase("Ramo: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(Ramo, Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // FOLIO TRAMITE 
            cell = new PdfPCell(new Phrase("Folio: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(Folio, Negrita));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // ESPACIO EN BLANCO
            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 50F;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // NOMBRE DEL TITULAR
            cell = new PdfPCell(new Phrase("Apreciable " + Titular, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;


            cell = new PdfPCell(new Phrase("En atención a tu solicitud de contratación del seguro de " + TipoTramite + " y con base en la información que nos proporcionaste, lamentamos comunicarte que después de revisar con detenimiento tu caso, no será posible proceder con la contratación del plan propuesto.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // ESPACIO EN BLANCO
            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 25F;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // EN LISTA LOS MOTIVOS DE RECHAZO
            if (motivos.Count > 0)
            {
                foreach (prop.motivosRechazo motivosRechazo in motivos)
                {
                    cell = new PdfPCell(new Phrase("   *   " + motivosRechazo.MotivoRechazo, Text));
                    cell.Colspan = 10;
                    cell.FixedHeight = 20f;
                    cell.MinimumHeight = 20f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    table.AddCell(cell);
                    cell = null;
                }
            }
            else
            {

                cell = new PdfPCell(new Phrase("   *   NO SE ENCONTRARON MOTIVOS ", Text));
                cell.Colspan = 10;
                cell.FixedHeight = 20f;
                cell.MinimumHeight = 20f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                table.AddCell(cell);
                cell = null;
            }

            // ESPACIO EN BLANCO
            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 25F;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // OBSERVACIONES
            cell = new PdfPCell(new Phrase("OBSERVACIONES.", Negrita));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // EN LISTA LAS OBSERVACIONES DE BITACORA
            if (ObservacionesBitacora.Count > 0)
            {
                foreach (prop.bitacora observaciones in ObservacionesBitacora)
                {

                    // MOTIVOS DE HOLD OBSERVACIONES 
                    cell = new PdfPCell(new Phrase("   >   " + observaciones.Observacion, Text));
                    cell.Colspan = 10;
                    cell.FixedHeight = 20f;
                    cell.MinimumHeight = 20f;
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.Border = 0;
                    table.AddCell(cell);
                    cell = null;
                }
            }
            else
            {
                // MOTIVOS DE HOLD OBSERVACIONES 
                cell = new PdfPCell(new Phrase(" > ", Text));
                cell.Colspan = 10;
                cell.FixedHeight = 20f;
                cell.MinimumHeight = 20f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                table.AddCell(cell);
                cell = null;
            }

            // ESPACIO EN BLANCO
            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 35F;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;
            

            // ESPACIO EN BLANCO
            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 35F;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("De manera sincera agradecemos haber considero a nuestra empresa,  esperando poder servirte en un futuro.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // ESPACIO EN BLANCO
            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 35F;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Atentamente.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("METLIFE, MÉXICO S.A. de C.V.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // ESPACIO EN BLANCO
            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 35F;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // AGENTE 
            cell = new PdfPCell(new Phrase(Agente, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // PROMOTORIA
            cell = new PdfPCell(new Phrase(Promotoria, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;



            document.Add(table);
            document.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "");
            Response.BinaryWrite(output.ToArray());

        }

        private void NoExistente()
        {
            // Medidas de la hoja A4
            var document = new Document(PageSize.A4, 30, 30, 50, 50);
            document.SetMargins(30, 30, 80, 50);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            PdfPCell cell = null;
            Font Text = new Font(FontFamily.HELVETICA, 30, Font.BOLD, BaseColor.BLACK);

            writer.PageEvent = new PDFFooter();

            document.Open();


            PdfPTable table = new PdfPTable(10);
            table.WidthPercentage = 100;
            table.SpacingBefore = 100F;


            cell = new PdfPCell(new Phrase("\n\n\n\n\n\n\n\n DOCUMENTO NO ENCONTRADO, FAVOR DE COMUNICARTE A SOPORTE PARA VERIFICAR LA EXISTENCIA DEL ARCHIVO", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            document.Add(table);
            document.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "");
            Response.BinaryWrite(output.ToArray());
        }


        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            try
            {
                string parametros = Negocio.Sistema.UrlCifrardo.Decrypt(Request.QueryString["data"].ToString());
                string IdTramite = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string BusqeudaIdTramite = stringBetween(s + ".", "Id=", ".");
                    if (BusqeudaIdTramite.Length > 0)
                    {
                        IdTramite = BusqeudaIdTramite;
                    }


                }

                if (IdTramite.Length > 0)
                {
                    urlCifrardo.IdTramite = IdTramite.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.IdTramite = "";
                }
            }
            catch (Exception)
            {
                urlCifrardo.Result = false;
            }

            return urlCifrardo;
        }

        public static string stringBetween(string Source, string Start, string End)
        {
            string result = "";
            if (Source.Contains(Start) && Source.Contains(End))
            {
                int StartIndex = Source.IndexOf(Start, 0) + Start.Length;
                int EndIndex = Source.IndexOf(End, StartIndex);
                result = Source.Substring(StartIndex, EndIndex - StartIndex);
                return result;
            }

            return result;
        }
    }
}