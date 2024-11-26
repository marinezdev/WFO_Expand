using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using promotoria = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.Procesos.Promotoria
{
    public partial class DisplaypdfPromo : System.Web.UI.Page
    {
        WFO.Negocio.Procesos.Promotoria.Archivos archivos = new Negocio.Procesos.Promotoria.Archivos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Sesion"] != null)
            {
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

                if (urlCifrardo.Result)
                {
                    MuestraPDF(urlCifrardo.Archivo);
                }
                else
                {
                    MuestraPDF("");
                }
            }
        }

        protected void MuestraPDF(string Archivo)
        {
            promotoria.expediente expediente = new promotoria.expediente();
            string strDoctoWeb = "";

            bool busqueda = false;
            strDoctoWeb = "..\\..\\ArchivosDefinitivos\\404.pdf";

            if (!string.IsNullOrEmpty(Archivo))
            {
                strDoctoWeb = Server.MapPath("~") + expediente.CarpetaInicial + Archivo;
                busqueda = true;

            }
            else
            {
                // AGREGAR ARCHIVO NO ENCONTRADO
                strDoctoWeb = "..\\..\\ArchivosDefinitivos\\404.pdf";
            }


            string FilePath = "";

            if (busqueda)
            {
                FilePath = strDoctoWeb;
            }
            else
            {
                FilePath = Server.MapPath(strDoctoWeb);
            }


            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
        }


        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            try
            {
                string parametros = Negocio.Sistema.UrlCifrardo.Decrypt(Request.QueryString["data"].ToString());
                string Archivo = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string BusqeudaIdTramite = stringBetween(s + ".", "Archivo=", ".");
                    if (BusqeudaIdTramite.Length > 0)
                    {
                        Archivo = BusqeudaIdTramite + ".pdf";
                    }
                }

                if (Archivo.Length > 0)
                {
                    urlCifrardo.Archivo = Archivo.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.Archivo = "";

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