using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using promotoria = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.Procesos.Operador
{
    public partial class Displaypdf : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Promotoria.Archivos archivos = new Negocio.Procesos.Promotoria.Archivos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Sesion"] != null)
            {
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

                if (urlCifrardo.Result)
                {
                    MuestraPDF(Convert.ToInt32(urlCifrardo.IdTramite), Convert.ToInt32(urlCifrardo.IdTipoTramite));
                }
                else
                {
                    MuestraPDF(Convert.ToInt32(0), Convert.ToInt32(0));
                }
            }
        }

        protected void MuestraPDF(int IdTramite, int IdTipoTramite)
        {
            List<promotoria.expediente> expedientes = archivos.ConsultaExpediente(IdTramite, IdTipoTramite);
            string strDoctoWeb = "";

            bool busqueda = false;
            strDoctoWeb = "..\\..\\ArchivosDefinitivos\\404.pdf";

            if (expedientes.Count > 0)
            {
                foreach (promotoria.expediente oArchivo in expedientes)
                {
                    if (!string.IsNullOrEmpty(oArchivo.NmArchivo))
                    {
                        strDoctoWeb = oArchivo.CarpetaArchivada + oArchivo.NmArchivo;

                        if (File.Exists(strDoctoWeb))
                        {
                            busqueda = true;
                        }
                        else
                        {
                            // AGREGAR ARCHIVO NO ENCONTRADO
                            strDoctoWeb = "..\\..\\ArchivosDefinitivos\\404.pdf";
                        }
                    }
                    else
                    {
                        // AGREGAR ARCHIVO NO ENCONTRADO
                        strDoctoWeb = "..\\..\\ArchivosDefinitivos\\404.pdf";
                    }
                }
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
                string IdTipoTramite = "";
                string IdTramite = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string BusqeudaIdTramite = stringBetween(s + ".", "IdTramite=", ".");
                    if (BusqeudaIdTramite.Length > 0)
                    {
                        IdTramite = BusqeudaIdTramite;
                    }

                    string BusqeudaIdTipoTramite = stringBetween(s + ".", "IdTipoTramite=", ".");
                    if (BusqeudaIdTipoTramite.Length > 0)
                    {
                        IdTipoTramite = BusqeudaIdTipoTramite;
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

                if (IdTipoTramite.Length > 0)
                {
                    urlCifrardo.IdTipoTramite = IdTipoTramite.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.IdTipoTramite = "";
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