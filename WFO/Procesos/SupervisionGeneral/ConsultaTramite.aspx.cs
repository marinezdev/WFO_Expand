using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using promotoria = WFO.Propiedades.Procesos.Promotoria;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Procesos.SupervisionGeneral
{
    public partial class ConsultaTramite : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Operacion.TramiteProcesar Tramites = new WFO.Negocio.Procesos.Operacion.TramiteProcesar();
        WFO.Negocio.Procesos.Promotoria.Archivos archivos = new Negocio.Procesos.Promotoria.Archivos();
        WFO.Negocio.Procesos.Operacion.Bitacora bitacora = new Negocio.Procesos.Operacion.Bitacora();
        WFO.Negocio.Procesos.Promotoria.Catalogos Catalogos = new Negocio.Procesos.Promotoria.Catalogos();
        WFO.Negocio.Procesos.Operacion.Indicador_StatusMesas indicador = new Negocio.Procesos.Operacion.Indicador_StatusMesas();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Procesable"]))
                hfIdTramite.Value = Request.QueryString["Procesable"].ToString();

            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.Parse(hfIdTramite.Value) > 0)
            {
                if (!IsPostBack)
                {
                    PostBack(int.Parse(hfIdTramite.Value));
                }
            }
        }

        private void PostBack(int pIdTramite)
        {
            // MUESTRA INFORMACION DEL TRÁMITE 
            PintaDatosTramite(pIdTramite);
        }

        private void PintaDatosTramite(int pIdTramite)
        {
            List<prop.TramiteProcesar> Tramite_a_Procesar;

            Tramite_a_Procesar = Tramites.ObtenerTramite(int.Parse(hfIdTramite.Value));
            if (Tramite_a_Procesar.Count > 0)
            {
                
                foreach (var datos in Tramite_a_Procesar)
                {
                    
                    //TABLA INFORMATIVA 
                    InfoFechaRegistro.Text = datos.FechaRegistro.ToString();
                    //InfoGMMoneda.Text = datos.Moneda.ToString();
                    InfoGMPrimaTotal.Text = String.Format("{0:C2}", Convert.ToDecimal(datos.PrimaCotizacion));

                    List<promotoria.promotoria_usuario> Promotoria = Catalogos.Promotoria_Seleccionar_PorIdTramite(datos.IdPromotoria, datos.IdTramite);

                    for (int p = 0; p < Promotoria.Count; p++)
                    {
                        //TABLA INFORMATIVA 
                        InfoClave.Text = Promotoria[p].Clave;
                        InfoRegion.Text = Promotoria[p].Clave_Region + " - " + Promotoria[p].Region;
                        InfoGerente.Text = Promotoria[p].Clave_Gerente + " - " + Promotoria[p].Gerente;
                        InfoEjecutivo.Text = Promotoria[p].Clave_Ejecutivo + " - " + Promotoria[p].Ejecutivo;
                        InfoEjecutivoFront.Text = Promotoria[p].Clave_Front + " - " + Promotoria[p].Front;
                    }

                    DateTime FechaSolicitud = Convert.ToDateTime(datos.FechaSolicitud);
                    InfoNumero.Text = datos.NumeroOrden;
                    InfoFechaSolicitud.Text = FechaSolicitud.ToString("dd/MM/yyyy");

                    if (datos.TipoPersona == 1)
                    {
                        DateTime FechaNacimiento = Convert.ToDateTime(datos.FNacimientoContratante);

                        //TABLA INFORMATIVA 
                        InfoPrsFisica.Visible = true;
                        InfoContratante.Text = "FÍSICA";
                        InfoFNombre.Text = datos.ContratanteNombre;
                        InfoFApellidoP.Text = datos.ContratanteApPaterno;
                        InfoFApellidoM.Text = datos.ContratanteApMaterno;
                        InfoFSexo.Text = datos.ContratanteSexo;
                        InfoFRFC.Text = datos.RFCContratante;
                        InfoFNacionalidad.Text = datos.Nacionalidad;
                        InfoFFechaNa.Text = FechaNacimiento.ToString("dd/MM/yyyy");

                    }
                    else if (datos.TipoPersona == 2)
                    {
                        DateTime FechaConstitucion = Convert.ToDateTime(datos.FechaConst);

                        //TABLA INFORMATIVA 
                        InfoPrsMoral.Visible = true;
                        InfoContratante.Text = "MORAL";
                        InfoMNombre.Text = datos.Contratante;
                        InfoMFechaConsti.Text = FechaConstitucion.ToString("dd/MM/yyyy");
                        InfoMRFC.Text = datos.RFCContratante;

                    }

                    // DATOS DE TITULAR

                    if (datos.TitularContratante == "True")
                    {
                        DateTime FechaNacimiento = Convert.ToDateTime(datos.FNacimientoTitular);

                        //TABLA INFORMATIVA 
                        InfoDiContratante.Visible = true;
                        InfoFContratante.Text = "NO";
                        InfoTNombre.Text = datos.TitularNombre;
                        InfoTApellidoP.Text = datos.TitularApPat;
                        InfoTApellidoM.Text = datos.TitularApMat;
                        InfoTNacionalidad.Text = datos.TitularNacionalidad;
                        InfoTSexo.Text = datos.TitularSexo;
                        InfoTNacimiento.Text = FechaNacimiento.ToString("dd/MM/yyyy");
                    }

                    LabelFolio.Text = datos.Folio;
                    LabelProducto.Text = datos.Producto;
                    LabelSubProducto.Text = datos.SubProducto;


                    // CARGA DE PDF
                    //CargarPFD(datos.IdTramite);
                    CargarPFD(datos.IdTramite, datos.IdTipoTramite);
                    hfIdTipoTramite.Value = datos.IdTipoTramite.ToString();
                    // CARGA BITACORA PÚBLICA
                    CargaBitacoraPublica(datos.IdTramite);

                    // CARGA BITACORA PRIVADA
                    CargaBitacoraPrivada(datos.IdTramite);

                    // CARGA ARCHIVOS EXPEDIENTES
                    CargaExpedientes(datos.IdTramite);

                    // INDICADOR STATUS MESAS
                    CargaIndicadorMesas(datos.IdTramite);

                    // CARGA CARTAS APARTIR DEL ESTATUS
                    MuestraTramitePorEstatus(datos.StatusTramite, datos.IdTramite);
                    
                }
            }
            else
            {
                mensajes.MostrarMensaje(this, "Trámite no disponibles...", "ListarTramites.aspx");
            }
        }

        private void MuestraTramitePorEstatus(string Estatus, int IdTramite)
        {
            
            switch (Estatus)
            {
                case "Hold":
                    LabelNombreCarta.Text = "Carta Hold";
                    MuestraCartaHold(IdTramite);
                    //LabelTipoCarta.Text = "Carta Hold";
                    //ltMuestraCarta.Text = "<iframe src='..\\Promotoria\\Cartas\\CartaHold.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
                    break;
                case "PCI":
                    LabelNombreCarta.Text = "Carta PCI";
                    MuestraCartaPCI(IdTramite);
                    //LabelTipoCarta.Text = "Carta Hold";
                    //ltMuestraCarta.Text = "<iframe src='..\\Promotoria\\Cartas\\CartaPCI.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
                    break;
                case "Ejecucion":
                    LabelNombreCarta.Text = "Carta Ejecución";
                    MuestraCartaEjecuacion(IdTramite);
                    //LabelTipoCarta.Text = "Carta Ejecución";
                    //ltMuestraCarta.Text = "<iframe src='..\\Promotoria\\Cartas\\CartaEjecucion.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
                    break;
                case "Suspendido":
                    LabelNombreCarta.Text = "Carta Suspendido";
                    MuestraCartaSuspendido(IdTramite);
                    //LabelTipoCarta.Text = "Carta Suspendido";
                    //ltMuestraCarta.Text = "<iframe src='..\\Promotoria\\Cartas\\CartaSuspendido.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
                    break;
                case "Rechazo":
                    LabelNombreCarta.Text = "Carta Rechazo";
                    MuestraCartaRechazo(IdTramite);
                    //LabelTipoCarta.Text = "Carta Rechazo";
                    //ltMuestraCarta.Text = "<iframe src='..\\Promotoria\\Cartas\\CartaRechazo.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
                    break;
                case "Cancelado":
                    LabelNombreCarta.Text = "Carta Cancelado";
                    MuestraCartaCancelado(IdTramite);
                    //LabelTipoCarta.Text = "Carta Cancelado";
                    //ltMuestraCarta.Text = "<iframe src='..\\Promotoria\\Cartas\\CartaPDFCancelar.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
                    break;
                default:
                    PanelMostrarCartas.Visible = false;
                    break;
            }
        }

        #region EVENTOS CARTAS
        private void MuestraCartaRechazo(int IdTramite)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string Encrypt = "?data=" + Negocio.Sistema.UrlCifrardo.Encrypt("Id=" + IdTramite);

            LabelTipoCarta.Text = "Carta rechazo";
            //ltMuestraCarta.Text = "<iframe src='Cartas\\CartaRechazo.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
            ltMuestraCarta.Text = "<iframe src='..\\Promotoria\\Cartas\\CartaRechazo.aspx" + Encrypt + "' style='width:100%; height:450px' style='border: none;'></iframe>";
        }

        private void MuestraCartaSuspendido(int IdTramite)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string Encrypt = "?data=" + Negocio.Sistema.UrlCifrardo.Encrypt("Id=" + IdTramite);

            LabelTipoCarta.Text = "Carta suspendido";
            //ltMuestraCarta.Text = "<iframe src='Cartas\\CartaSuspendido.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
            ltMuestraCarta.Text = "<iframe src='..\\Promotoria\\Cartas\\CartaSuspendido.aspx" + Encrypt + "' style='width:100%; height:450px' style='border: none;'></iframe>";
        }

        private void MuestraCartaEjecuacion(int IdTramite)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string Encrypt = "?data=" + Negocio.Sistema.UrlCifrardo.Encrypt("Id=" + IdTramite);

            LabelTipoCarta.Text = "Carta ejecución";
            //ltMuestraCarta.Text = "<iframe src='Cartas\\CartaEjecucion.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
            ltMuestraCarta.Text = "<iframe src='..\\Promotoria\\Cartas\\CartaEjecucion.aspx" + Encrypt + "' style='width:100%; height:450px' style='border: none;'></iframe>";
        }

        private void MuestraCartaHold(int IdTramite)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string Encrypt = "?data=" + Negocio.Sistema.UrlCifrardo.Encrypt("Id=" + IdTramite);

            LabelTipoCarta.Text = "Carta hold";
            //ltMuestraCarta.Text = "<iframe src='Cartas\\CartaHold.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
            ltMuestraCarta.Text = "<iframe src='..\\Promotoria\\Cartas\\CartaHold.aspx" + Encrypt + "' style='width:100%; height:450px' style='border: none;'></iframe>";
        }

        private void MuestraCartaPCI(int IdTramite)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string Encrypt = "?data=" + Negocio.Sistema.UrlCifrardo.Encrypt("Id=" + IdTramite);

            LabelTipoCarta.Text = "Carta PCI";
            //ltMuestraCarta.Text = "<iframe src='Cartas\\CartaPCI.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
            ltMuestraCarta.Text = "<iframe src='..\\Promotoria\\Cartas\\CartaPCI.aspx" + Encrypt + "' style='width:100%; height:450px' style='border: none;'></iframe>";
        }
        private void MuestraCartaCancelado(int IdTramite)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string Encrypt = "?data=" + Negocio.Sistema.UrlCifrardo.Encrypt("Id=" + IdTramite);

            LabelTipoCarta.Text = "Carta Cancelado";
            //ltMuestraCarta.Text = "<iframe src='Cartas\\CartePDFCancelar.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
            ltMuestraCarta.Text = "<iframe src='..\\Promotoria\\Cartas\\CartePDFCancelar.aspx" + Encrypt + "' style='width:100%; height:450px' style='border: none;'></iframe>";
        }
        #endregion


        protected void BtnExpedienteOcultar_click(object sender, EventArgs e)
        {
            Expediente.Visible = false;
            LinkButtonMostrarExpediente.Visible = true;
            LinkButtonOcultarExpediente.Visible = false;
        }

        protected void BtnExpedienteAbrir_click(object sender, EventArgs e)
        {
            int IdTramite = Convert.ToInt32(hfIdTramite.Value);
            int IdTipoTramite = Convert.ToInt32(hfIdTipoTramite.Value);

            List<promotoria.expediente> expedientes = archivos.ConsultaExpediente(IdTramite, IdTipoTramite);
            string strDoctoWeb = "";
            string strDoctoServer = "";

            strDoctoWeb = "..\\..\\ArchivosDefinitivos\\404.pdf";

            if (expedientes.Count > 0)
            {
                foreach (promotoria.expediente oArchivo in expedientes)
                {
                    if (!string.IsNullOrEmpty(oArchivo.NmArchivo))
                    {
                        strDoctoWeb = "..\\..\\DocsUp\\" + oArchivo.NmArchivo;
                        strDoctoServer = Server.MapPath("~") + "\\DocsUp\\" + oArchivo.NmArchivo;
                        if (!File.Exists(strDoctoServer))
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

            string script = "window.open('" + strDoctoWeb.ToString().Replace("\\", "/") + "','Expediente', 'width = 600, height = 400');";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }

        protected void BtnExpedienteMostrar_click(object sender, EventArgs e)
        {
            Expediente.Visible = true;
            LinkButtonMostrarExpediente.Visible = false;
            LinkButtonOcultarExpediente.Visible = true;
        }

        private void CargarPFD(int Id, int IdTipoTramite)
        {
            ltMuestraPdf.Text = "";
            ltMuestraPdf.Text = "<iframe src='" + EncripParametros("IdTramite=" + Id + ",IdTipoTramite=" + IdTipoTramite, "Displaypdf.aspx").URL + "' style='width:100%; height:540px' style='border: none;'></iframe>";

            //int IdTipoTramite = Convert.ToInt32(hfIdTipoTramite.Value);

            //List<promotoria.expediente> expedientes = archivos.ConsultaExpediente(Id, IdTipoTramite);
            //string strDoctoWeb = "";
            //string strDoctoServer = "";

            //strDoctoWeb = "..\\..\\ArchivosDefinitivos\\404.pdf";

            //if (expedientes.Count > 0)
            //{
            //    foreach (promotoria.expediente oArchivo in expedientes)
            //    {
            //        if (!string.IsNullOrEmpty(oArchivo.NmArchivo))
            //        {
            //            strDoctoWeb = "..\\..\\DocsUp\\" + oArchivo.NmArchivo;
            //            strDoctoServer = Server.MapPath("~") + "\\DocsUp\\" + oArchivo.NmArchivo;
            //            if (!File.Exists(strDoctoServer))
            //            {
            //                // AGREGAR ARCHIVO NO ENCONTRADO
            //                strDoctoWeb = "..\\..\\ArchivosDefinitivos\\404.pdf";
            //            }
            //        }
            //        else
            //        {
            //            // AGREGAR ARCHIVO NO ENCONTRADO
            //            strDoctoWeb = "..\\..\\ArchivosDefinitivos\\404.pdf";
            //        }
            //    }
            //}

            //ltMuestraPdf.Text = "";
            //ltMuestraPdf.Text = "<iframe src='" + strDoctoWeb + "' style='width:100%; height:540px' style='border: none;'></iframe>";
        }

        private void CargaBitacoraPublica(int Id)
        {
            List<promotoria.bitacora> bitacoras = bitacora.ConsultaBitacoraPublica(Id);
            rptBitacoraPublica.DataSource = bitacoras;
            rptBitacoraPublica.DataBind();
        }

        private void CargaBitacoraPrivada(int Id)
        {
            List<promotoria.bitacora> bitacoras = bitacora.ConsultaBitacoraPrivada(Id);
            rptBitacoraPrivada.DataSource = bitacoras;
            rptBitacoraPrivada.DataBind();
        }

        private void CargaIndicadorMesas(int Id)
        {
            List<prop.Indicador_StatusMesas> IndicadorDatos = indicador.StatusMesas(Id);
            StatusMesa.DataSource = IndicadorDatos;
            StatusMesa.DataBind();
        }

        private void CargaExpedientes(int Id)
        {
            List<promotoria.expediente> bitacoras = archivos.Expediente_Consultar_PorIdTramite(Id);
            rptExpedientes.DataSource = bitacoras;
            rptExpedientes.DataBind();
        }

        protected void CargaExpedienteID(object sender, CommandEventArgs e)
        {
            int IdTramite = Convert.ToInt32(hfIdTramite.Value);
            int IdExpediente = Convert.ToInt32(e.CommandArgument.ToString());

            promotoria.expediente oArchivo = archivos.Asegurados_Selecionar_PorIdTramite(IdExpediente, IdTramite);
            string strDoctoWeb = "";
            string strDoctoServer = "";

            strDoctoWeb = "..\\..\\ArchivosDefinitivos\\404.pdf";

            if (oArchivo.Id > 0)
            {

                if (!string.IsNullOrEmpty(oArchivo.NmArchivo))
                {
                    strDoctoWeb = "..\\..\\DocsUp\\" + oArchivo.NmArchivo;
                    strDoctoServer = Server.MapPath("~") + "\\DocsUp\\" + oArchivo.NmArchivo;
                    if (!File.Exists(strDoctoServer))
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

            string script = "window.open('" + strDoctoWeb.ToString().Replace("\\", "/") + "','Expediente', 'width = 600, height = 400');";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }

        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            try
            {
                string parametros = Negocio.Sistema.UrlCifrardo.Decrypt(Request.QueryString["data"].ToString());
                string IdProceso = "";
                string Procesable = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string BusqeudaIdProceso = stringBetween(s + ".", "IdProceso=", ".");
                    if (BusqeudaIdProceso.Length > 0)
                    {
                        IdProceso = BusqeudaIdProceso;
                    }

                    string BusqeudaProcesable = stringBetween(s + ".", "Procesable=", ".");
                    if (BusqeudaProcesable.Length > 0)
                    {
                        Procesable = BusqeudaProcesable;
                    }
                }

                if (IdProceso.Length > 0)
                {
                    urlCifrardo.IdProceso = IdProceso.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.IdMesa = "";

                }

                if (Procesable.Length > 0)
                {
                    urlCifrardo.Procesable = Procesable.ToString();
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

        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();

            string Encrypt = Negocio.Sistema.UrlCifrardo.Encrypt(parametros);

            urlCifrardo.URL = Direccion + "?data=" + Encrypt;

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