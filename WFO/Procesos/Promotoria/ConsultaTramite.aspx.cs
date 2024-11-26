using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.Procesos.Promotoria
{
    public partial class ConsultaTramite : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Promotoria.Archivos archivos = new Negocio.Procesos.Promotoria.Archivos();
        WFO.Negocio.Procesos.Promotoria.TramitesPromotoria tramitesPromotoria = new Negocio.Procesos.Promotoria.TramitesPromotoria();
        WFO.Negocio.Procesos.Operacion.TramiteProcesar Tramites = new WFO.Negocio.Procesos.Operacion.TramiteProcesar();
        WFO.Negocio.Procesos.Promotoria.Bitacora bitacora = new Negocio.Procesos.Promotoria.Bitacora();

        protected void Page_Init(object sender, EventArgs e)
        {
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int IdTramite = 0;
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

                if (urlCifrardo.Result)
                {
                    IdTramite = Convert.ToInt32(urlCifrardo.IdTramite);
                }
                else
                {
                    string script = "";
                    script = "window.location.href='Default.aspx'; ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                //int IdTramite = Convert.ToInt32(Request.Params["Id"].ToString());

                // manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
                List<prop.TramitesPromotoria> tramitesPromotorias = tramitesPromotoria.ConsultaTramitesPromotoria(manejo_sesion.Usuarios.IdUsuario, IdTramite);

                if (ValidaPromotoria(tramitesPromotorias))
                {
                    PintaInformacionTramite(tramitesPromotorias);
                    MuestraPDF(IdTramite, tramitesPromotorias[0].IdTipoTramite);
                    MuestraBitacora(IdTramite);
                }
                
            }
        }

        private void PintaInformacionTramite(List<prop.TramitesPromotoria> Tramite)
        {
            foreach (prop.TramitesPromotoria DatosTramite in Tramite)
            {
                LabelNombre.Text = DatosTramite.Contratante;
                //LabelRFC.Text = DatosTramite.RFC;
                LabelEstatusTramite.Text = DatosTramite.Estatus;
                LabelFolio.Text = DatosTramite.FolioCompuesto;
                LabelFlujo.Text = DatosTramite.Operacion;
                LabelFechaRegistro.Text = DatosTramite.FechaRegistro.ToString();

                // LA INFORMACIÓN MOSTRADA ES APARTIR DEL ESTATUS DEL TRAMITE
                MuestraTramitePorEstatus(DatosTramite.Estatus, DatosTramite.Id);
            }
        }

        private bool ValidaPromotoria(List<prop.TramitesPromotoria> Tramite)
        {
            if (Tramite.Count <= 0)
            {
                Response.Redirect("MisTramites.aspx");
            }
            return true;
        }
        
        private void MuestraPDF(int Id, int IdTipoTramite)
        {
            ltMuestraPdf.Text = "";
            ltMuestraPdf.Text = "<iframe src='" + EncripParametros("IdTramite=" + Id + ",IdTipoTramite=" + IdTipoTramite, "Displaypdf.aspx").URL + "' style='width:100%; height:540px' style='border: none;'></iframe>";
            //List<prop.expediente> expedientes = archivos.ConsultaExpediente(Id, IdTipoTramite);
            //string strDoctoWeb = "";
            //string strDoctoServer = "";

            //strDoctoWeb = "..\\..\\ArchivosDefinitivos\\404.pdf";

            //if (expedientes.Count > 0)
            //{
            //    foreach (prop.expediente oArchivo in expedientes)
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
            //ltMuestraPdf.Text = "<iframe src='" + strDoctoWeb + "' style='width:100%; height:450px' style='border: none;'></iframe>";
        }

        private void MuestraBitacora(int Id)
        {
            List<prop.bitacora> bitacoras = bitacora.ConsultaBitacora(Id);
            rptBitacora.DataSource = bitacoras;
            rptBitacora.DataBind();
        }

        private void MuestraTramitePorEstatus(string Estatus, int IdTramite)
        {
            switch (Estatus)
            {
                case "Hold":
                    ObservacionesCartaHold.Visible = true;
                    AnexoArchivos.Visible = true;
                    RegistrarHold.Visible = true;
                    List<prop.bitacora> bitacoraHold = bitacora.ConsultaUltimaObervacion(IdTramite);
                    LabelObservacionesHold.Text = bitacoraHold[0].Observacion;
                    MuestraCartaHold(IdTramite);
                    break;
                case "PCI":
                    ObservacionesCartaPCI.Visible = true;
                    AnexoArchivos.Visible = true;
                    RegistraPCI.Visible = true;
                    MuestraCartaPCI(IdTramite);
                    break;
                case "Ejecucion":
                    ObservacionesCartaEjecucion.Visible = false;
                    MuestraCartaEjecuacion(IdTramite);
                    break;
                case "Suspendido":
                    ObservacionesCartaSuspendido.Visible = true;
                    AnexoArchivos.Visible = true;
                    RegistrarSuspendido.Visible = true;
                    List<prop.bitacora> bitacoraSuspendido = bitacora.ConsultaUltimaObervacion(IdTramite);
                    LabelObservacionesSuspendido.Text = bitacoraSuspendido[0].Observacion;
                    MuestraCartaSuspendido(IdTramite);
                    break;
                case "Revisión Promotoria":
                    RegistrarRevisionPromotoria.Visible = true;
                    break;
                case "Rechazo":
                    ObservacionesCartaRechazo.Visible = true;
                    MuestraCartaRechazo(IdTramite);
                    break;
                case "Cancelado":
                    ObservacionesCartaCancelado.Visible = true;
                    MuestraCartaCancelado(IdTramite);
                    break;
                default:
                break;
            }
        }
        
        protected void btnSubirDocumento_Click(object sender, EventArgs e)
        {
            LabRespuestaArchivosCarga.Text = "";
            /* LISTA LOS ARCHIVOS DEL DOCUMENTO */
            List<prop.expediente> LstArchivosDocumento = new List<prop.expediente>();
            // SI EXISTE LA VARIABLE DE SESION LLENA LA LISTA
            if (Session["documentos"] != null)
            {
                LstArchivosDocumento = (List<prop.expediente>)Session["documentos"];
            }

            if (fileUpDocumento.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fileUpDocumento.FileName).ToLower();
                string fileExtension2 = "";
                if (".pdf".Contains(fileExtension) ^ ".jpg".Contains(fileExtension) ^ ".png".Contains(fileExtension))
                {
                    int fileSize = fileUpDocumento.PostedFile.ContentLength;
                    prop.expediente expedientes = new prop.expediente();

                    if (fileSize < 41943040)
                    {
                        List<prop.control_archivos> control_Archivos = archivos.ControlArchivoNuevoID();
                        int IdControlArchivo = control_Archivos[0].Id;
                        string nombreArchivo = control_Archivos[0].Clave;
                        string directorioTemporal = Server.MapPath("~") + expedientes.CarpetaInicial;
                        //string nombreArchivo = IdControlArchivo.ToString().PadLeft(12, '0');
                        //string directorioTemporal = Server.MapPath("~") + "\\DocsUp\\";

                        fileUpDocumento.PostedFile.SaveAs(directorioTemporal + nombreArchivo + fileExtension);

                        if (!fileExtension.Equals(".pdf"))
                        {
                            if (Funciones.ManejoArchivos.ConviertePDF(directorioTemporal + nombreArchivo + fileExtension, directorioTemporal + nombreArchivo + ".pdf"))
                            {
                                fileExtension2 = ".pdf";
                            }
                        }

                        fileExtension2 = ".pdf";

                        bool archivoEnPdf = false;
                        if (!fileExtension2.Equals(".pdf"))
                        {
                            archivoEnPdf = false;
                        }
                        else
                        {
                            nombreArchivo = nombreArchivo + fileExtension2;
                            archivoEnPdf = true;
                        }

                        if (archivoEnPdf)
                        {
                            expedientes.Id_Archivo = IdControlArchivo;
                            expedientes.NmArchivo = nombreArchivo;
                            expedientes.NmOriginal = fileUpDocumento.FileName;
                            expedientes.Activo = 1;
                            expedientes.Fusion = 0;
                            expedientes.Descripcion = "";

                            LstArchivosDocumento.Add(expedientes);
                            lstDocumentos.DataSource = LstArchivosDocumento;
                            lstDocumentos.DataValueField = "Id_Archivo";
                            lstDocumentos.DataTextField = "NmOriginal";
                            lstDocumentos.DataBind();

                            Session["documentos"] = LstArchivosDocumento;
                            LabRespuestaArchivosCarga.Text = "Cargado";
                        }
                        else { LabRespuestaArchivosCarga.Text = "El archivo no se puede convertir a PDF."; }
                    }
                    else
                    {
                        LabRespuestaArchivosCarga.Text = "El archivo excede el límite de 40MB.";
                    }
                }
                else
                {
                    LabRespuestaArchivosCarga.Text = "El archivo no es un PDF o JPG.";
                }
            }
            else
            {
                LabRespuestaArchivosCarga.Text = "No a cargado ningun tipo de archivo.";
            }
        }

        protected void btnSubirInsumo_Click(object sender, EventArgs e)
        {
            List<prop.insumos> LstArchivosInsumo = new List<prop.insumos>();
            if (Session["insumos"] != null) { LstArchivosInsumo = (List<prop.insumos>)Session["insumos"]; }
            if (fileUpInsumo.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fileUpInsumo.FileName).ToLower();
                prop.insumos oInsumo = new prop.insumos();
                int fileSize = fileUpInsumo.PostedFile.ContentLength;
                if (fileSize < 41943040)
                {

                    List<prop.control_archivos> control_Archivos = archivos.ControlArchivoNuevoID();
                    int IdArchivo = control_Archivos[0].Id;
                    string nombreArchivo = IdArchivo.ToString().PadLeft(12, '0') + fileExtension;
                    string directorioTemporal = Server.MapPath("~") + "\\DocsInsumos\\";
                    fileUpInsumo.PostedFile.SaveAs(directorioTemporal + nombreArchivo);

                    oInsumo.Id_Archivo = IdArchivo;
                    oInsumo.NmArchivo = nombreArchivo;
                    oInsumo.NmOriginal = fileUpInsumo.FileName;
                    oInsumo.Activo = 1;
                    oInsumo.Descripcion = "";
                    oInsumo.RutaTemporal = directorioTemporal;

                    LstArchivosInsumo.Add(oInsumo);
                    lstInsumos.DataSource = LstArchivosInsumo;
                    lstInsumos.DataValueField = "Id";
                    lstInsumos.DataTextField = "NmOriginal";
                    lstInsumos.DataBind();

                    Session["insumos"] = LstArchivosInsumo;
                    MensajeInsumos.Text = "Cargado";
                }
                else
                {
                    MensajeInsumos.Text = "El archivo excede el límite de 40MB.";
                }
            }
            else
            {
                MensajeInsumos.Text = "No has seleccionado un archivo";
            }
        }

        protected void btnEliminaDocumento_Click(object sender, EventArgs e)
        {
            if (lstDocumentos.Items.Count > 0 && lstDocumentos.SelectedIndex > -1)
            {
                List<prop.expediente> LstArchExpediente = new List<prop.expediente>();
                List<prop.expediente> LstArchExpedienteTmp = new List<prop.expediente>();
                if (Session["documentos"] != null) { LstArchExpediente = (List<prop.expediente>)Session["documentos"]; }
                int contador = 0;
                foreach (prop.expediente oArchivo in LstArchExpediente)
                {
                    if (contador != lstDocumentos.SelectedIndex) { LstArchExpedienteTmp.Add(oArchivo); }
                    contador += 1;
                }
                lstDocumentos.DataSource = LstArchExpedienteTmp;
                lstDocumentos.DataValueField = "Id";
                lstDocumentos.DataTextField = "NmOriginal";
                lstDocumentos.DataBind();
                Session["documentos"] = LstArchExpedienteTmp;
            }
        }

        protected void btnEliminaInsumo_Click(object sender, EventArgs e)
        {
            if (lstInsumos.Items.Count > 0 && lstInsumos.SelectedIndex > -1)
            {
                List<prop.insumos> LstArchInsumos = new List<prop.insumos>();
                List<prop.insumos> LstArchInsumosTmp = new List<prop.insumos>();
                if (Session["insumos"] != null) { LstArchInsumos = (List<prop.insumos>)Session["insumos"]; }
                int contador = 0;
                foreach (prop.insumos oInsumo in LstArchInsumos)
                {
                    if (contador != lstInsumos.SelectedIndex) { LstArchInsumosTmp.Add(oInsumo); }
                    contador += 1;
                }
                lstInsumos.DataSource = LstArchInsumosTmp;
                lstInsumos.DataValueField = "Id";
                lstInsumos.DataTextField = "NmOriginal";
                lstInsumos.DataBind();
                Session["insumos"] = LstArchInsumosTmp;
            }
        }

        protected void CheckBox_Habilita_Insumos(object sender, EventArgs e)
        {
            if (CheckBoxInsumos.Checked.Equals(true))
            {
                PanelInsumos.Visible = true;
            }
            else
            {
                PanelInsumos.Visible = false;
            }
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            //if (ValidaExpediente())
            //{
            // VARIABLES
            //int IdTramite = Convert.ToInt32(Request.Params["Id"].ToString());
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
            int IdTramite = Convert.ToInt32(urlCifrardo.IdTramite);
            int TipoTramite = 0;
                string Observaciones = txObervaciones.Text;
                string script = "";

                // REGISTRO DE ARCHIVOS - COLOCAR DESPUES DE ACTUALIZAR EL TRAMITE
                if (Session["documentos"] != null)
                {
                    string resultadoExpediente = registraDocumentosExpediente(IdTramite, TipoTramite);
                }

                if (Session["insumos"] != null)
                {
                    string resultadoInsumo = registraDocumentosInsumos(IdTramite, TipoTramite);
                }

                List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.ReingresarTramite(IdTramite, manejo_sesion.Usuarios.IdUsuario, Observaciones, "");
                if (objResultado[0].IdTramite > 0)
                {
                    // MENSAJE DE REGISTRO EN CASO DE SER EXITOSO
                    LabelActualizacionTramite.Text = "Actualización exitosa";
                    script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                else
                {
                    // MENSAJE DE REGISTRO EN CASO DE SER EXITOSO
                    LabelActualizacionTramite.Text = "No se pudo reingresar el trámite. Por favor contacte a su Administrador.";
                    script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }


                // MENSAJE DE REGISTRO EN CASO DE SER EXITOSO
                LabelActualizacionTramite.Text = "Actualización exitosa";

                script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            //}
            //else
            //{
            //    Mensajes.Text = "No se han subido archivos al expediente";
            //}
        }
        
        protected void BtnContinuarSuspendido_Click(object sender, EventArgs e)
        {
            //if (ValidaExpediente())
            //{
            // VARIABLES
            //int IdTramite = Convert.ToInt32(Request.Params["Id"].ToString());
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
            int IdTramite = Convert.ToInt32(urlCifrardo.IdTramite);
            int TipoTramite = 0;

                string Observaciones = txObervacionesSuspendido.Text;
                string script = "";

                // REGISTRO DE ARCHIVOS - COLOCAR DESPUES DE ACTUALIZAR EL TRAMITE
                if (Session["documentos"] != null) { string resultadoExpediente = registraDocumentosExpediente(IdTramite, TipoTramite); }
                if (Session["insumos"] != null) { string resultadoInsumo = registraDocumentosInsumos(IdTramite, TipoTramite); }

                List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.ReingresarTramite(IdTramite, manejo_sesion.Usuarios.IdUsuario, Observaciones, "");
                if (objResultado[0].IdTramite > 0)
                {
                    // MENSAJE DE REGISTRO EN CASO DE SER EXITOSO
                    LabelActualizacionTramite.Text = "Actualización exitosa";
                    script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                else
                {
                    // MENSAJE DE REGISTRO EN CASO DE SER EXITOSO
                    LabelActualizacionTramite.Text = "No se pudo reingresar el trámite. Por favor contacte a su Administrador.";
                    script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            //}
            //else
            //{
            //    MensajeSuspendido.Text = "No se han subido archivos al expediente";
            //}
        }

        protected void BtnContinuarPCI_Click(object sender, EventArgs e)
        {
            //if (ValidaExpediente())
            //{
            // VARIABLES
            //int IdTramite = Convert.ToInt32(Request.Params["Id"].ToString());
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
            int IdTramite = Convert.ToInt32(urlCifrardo.IdTramite);
            int TipoTramite = 0;
                string Observaciones = txObervacionesSuspendido.Text;
                string script = "";

                // REGISTRO DE ARCHIVOS, VERIFICA EL CONTENIDO DE LA VARIABLE 
                if (Session["documentos"] != null) { string resultadoExpediente = registraDocumentosExpediente(IdTramite, TipoTramite); }
                if (Session["insumos"] != null) { string resultadoInsumo = registraDocumentosInsumos(IdTramite, TipoTramite); }
                
                
                // COLOCAR EVENTO DE REGISTRO
                List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.ReingresarTramite(IdTramite, manejo_sesion.Usuarios.IdUsuario, Observaciones, "");
                if (objResultado[0].IdTramite > 0)
                {
                    // MENSAJE DE REGISTRO EN CASO DE SER EXITOSO
                    LabelActualizacionTramite.Text = "Actualización exitosa";
                    script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                else
                {
                    // MENSAJE DE REGISTRO EN CASO DE SER EXITOSO
                    LabelActualizacionTramite.Text = "No se pudo reingresar el trámite. Por favor contacte a su Administrador.";
                    script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            //}
            //else
            //{
            //    MensajesPCI.Text = "No se han subido archivos al expediente";
            //}
        }

        protected void BtnContinuarRevisionPromotoria_Click(object sender, EventArgs e)
        {
            // VARIABLES
            //int IdTramite = Convert.ToInt32(Request.Params["Id"].ToString());
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
            int IdTramite = Convert.ToInt32(urlCifrardo.IdTramite);
            string Observaciones = txObervacionesRevisionPromotoria.Text;
            string script = "";

            if (Observaciones.Length > 0)
            {
                List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.PromotoriaAcepta(IdTramite, true, manejo_sesion.Usuarios.IdUsuario, Observaciones, "");
                if (objResultado[0].IdTramite > 0)
                {
                    // MENSAJE DE REGISTRO EN CASO DE SER EXITOSO
                    LabelActualizacionTramite.Text = "Póliza Emitida";
                    script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                else
                {
                    // MENSAJE DE REGISTRO EN CASO DE SER EXITOSO
                    LabelActualizacionTramite.Text = "No se pudo procesar la póliza del trámite. Por favor contacte a su Administrador.";
                    script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            else
            {
                MensajeRevisionPromotoria.Text = "Coloca las observaciones";
            }
        }

        protected void BtnRechazarRevisionPromotoria_Click(object sender, EventArgs e)
        {
            // VARIABLES
            //int IdTramite = Convert.ToInt32(Request.Params["Id"].ToString());
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
            int IdTramite = Convert.ToInt32(urlCifrardo.IdTramite);
            string Observaciones = txObervacionesRevisionPromotoria.Text;
            string script = "";

            if (Observaciones.Length > 0)
            {
                List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.PromotoriaAcepta(IdTramite, false, manejo_sesion.Usuarios.IdUsuario, Observaciones, "");
                if (objResultado[0].IdTramite > 0)
                {
                    // MENSAJE DE REGISTRO EN CASO DE SER EXITOSO
                    LabelActualizacionTramite.Text = "Póliza Emitida";
                    script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                else
                {
                    // MENSAJE DE REGISTRO EN CASO DE SER EXITOSO
                    LabelActualizacionTramite.Text = "No se pudo procesar la póliza del trámite. Por favor contacte a su Administrador.";
                    script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            else
            {
                MensajeRevisionPromotoria.Text = "Coloca las observaciones";
            }
        }
        
        private bool ValidaExpediente()
        {
            bool respuesta = false;
            List<prop.expediente> LstExpediente = new List<prop.expediente>();

            if (Session["documentos"] != null)
                LstExpediente = (List<prop.expediente>)Session["documentos"];

            if (LstExpediente.Count > 0)
            {
                respuesta = true;
            }
            return respuesta;
        }

        private string registraDocumentosExpediente(int pIdTramite, int TipoTramite)
        {
            string msgError = "";
            string strRutaServidor = "";
            string strArchivoOrigen = "";
            prop.expediente expediente = new prop.expediente();

            //string directorioTemporal = Server.MapPath("~") + "\\DocsUp\\";
            //string directorioTemporal = Server.MapPath("~") + expediente.CarpetaArchivada;
            string directorioInicial = Server.MapPath("~") + expediente.CarpetaInicial;
            string directorioArchivos = expediente.CarpetaArchivada;

            List<prop.expediente> LstExpediente = new List<prop.expediente>();
            if (Session["documentos"] != null)
            {
                LstExpediente = (List<prop.expediente>)Session["documentos"];
            }

            List<string> lstArchivos = new List<string>();
            foreach (prop.expediente oDocumento in LstExpediente)
            {
                //strArchivoOrigen = Server.MapPath("~") + "\\DocsUp\\" + oDocumento.NmArchivo;
                //strArchivoOrigen = Server.MapPath("~") + expediente.CarpetaArchivada + oDocumento.NmArchivo;
                strArchivoOrigen = Server.MapPath("~") + expediente.CarpetaInicial + oDocumento.NmArchivo;

                if (File.Exists(strArchivoOrigen))
                {
                    archivos.Agregar_Expedientes_Tramite(TipoTramite, pIdTramite, oDocumento.Id_Archivo, oDocumento.NmArchivo, oDocumento.NmOriginal, oDocumento.Activo, oDocumento.Fusion, oDocumento.Descripcion);
                    lstArchivos.Add(strArchivoOrigen);
                }
            }

            // CONSULTA ID DEL EXPEDIENTE FUCIONADO
            List<prop.expediente> expedientes = archivos.ConsultaExpediente(pIdTramite, TipoTramite);
            string ArchFusionAnt = "";
            int Id_Expediente = 0;
            if (expedientes.Count > 0)
            {
                //ArchFusionAnt = directorioTemporal + expedientes[0].NmArchivo;
                ArchFusionAnt = directorioArchivos + expedientes[0].NmArchivo;
                Id_Expediente = expedientes[0].Id;
            }
            
            List<prop.control_archivos> control_Archivos = archivos.ControlArchivoNuevoID();
            int IdControlArchivo = control_Archivos[0].Id;
            string nombreFusion = directorioInicial + control_Archivos[0].Clave + ".pdf";

            //string nombreFusion = directorioTemporal + IdControlArchivo.ToString().PadLeft(12, '0') + ".pdf";
            if (File.Exists(nombreFusion))
            {
                File.Delete(nombreFusion);
            }

            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
            string nmSeparador = directorioInicial + manejo_sesion.Usuarios.IdUsuario + ".pdf";
            string nmLogo = Server.MapPath("~\\Imagenes") + @"\logo_sep.png";

            
            msgError = WFO.Funciones.ManejoArchivos.Adiciona(lstArchivos, ArchFusionAnt, nombreFusion, manejo_sesion.Usuarios.Nombre, nmSeparador, nmLogo);

            if (string.IsNullOrEmpty(msgError))
            {
                archivos.ModificarExpedienteFusion(Id_Expediente);
                archivos.Agregar_Expedientes_Tramite(TipoTramite, pIdTramite, IdControlArchivo, control_Archivos[0].Clave + ".pdf", "Archivo Fucion Agragacion", 1, 1, "");
                File.Copy(nombreFusion, directorioArchivos + control_Archivos[0].Clave + ".pdf");
                //archivos.Agregar_Expedientes_Tramite(TipoTramite, pIdTramite, IdControlArchivo, IdControlArchivo.ToString().PadLeft(12, '0') + ".pdf","Archivo Fucion Agragacion",1,1,"");
                msgError = "";
            }
            else
            {
                Mensajes.Text = msgError;
            }

            return "";
        }

        private string registraDocumentosInsumos(int pIdTramite, int TipoTramite)
        {
            List<prop.insumos> LstArchivosInsumo = new List<prop.insumos>();
            if (Session["insumos"] != null) { LstArchivosInsumo = (List<prop.insumos>)Session["insumos"]; }

            string strArchivoOrigen = "";

            foreach (prop.insumos oDocumento in LstArchivosInsumo)
            {
                strArchivoOrigen = Server.MapPath("~") + "\\DocsInsumos\\" + oDocumento.NmArchivo;

                if (File.Exists(strArchivoOrigen))
                {
                    archivos.Agregar_Insumo_Tramite(TipoTramite, pIdTramite, oDocumento.Id_Archivo, oDocumento.NmArchivo, oDocumento.NmOriginal, oDocumento.Activo, oDocumento.Descripcion);
                }
            }
            return "";
        }

        protected void TramiteTerminado(object sender, EventArgs e)
        {
            Session.Remove("documentos");
            Session.Remove("insumos");
            Session["documentos"] = null;
            Session["insumos"] = null;
            Response.Redirect("MisTramites.aspx");
        }

        #region EVENTOS CARTAS
        private void MuestraCartaRechazo(int IdTramite)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string Encrypt = "?data=" + Negocio.Sistema.UrlCifrardo.Encrypt("Id=" + IdTramite);

            LabelTipoCarta.Text = "Carta rechazo";
            //ltMuestraCarta.Text = "<iframe src='Cartas\\CartaRechazo.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
            ltMuestraCarta.Text = "<iframe src='Cartas\\CartaRechazo.aspx" + Encrypt + "' style='width:100%; height:450px' style='border: none;'></iframe>";
        }

        private void MuestraCartaSuspendido(int IdTramite)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string Encrypt = "?data=" + Negocio.Sistema.UrlCifrardo.Encrypt("Id=" + IdTramite);

            LabelTipoCarta.Text = "Carta suspendido";
            //ltMuestraCarta.Text = "<iframe src='Cartas\\CartaSuspendido.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
            ltMuestraCarta.Text = "<iframe src='Cartas\\CartaSuspendido.aspx" + Encrypt + "' style='width:100%; height:450px' style='border: none;'></iframe>";
        }

        private void MuestraCartaEjecuacion(int IdTramite)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string Encrypt = "?data=" + Negocio.Sistema.UrlCifrardo.Encrypt("Id=" + IdTramite);

            LabelTipoCarta.Text = "Carta ejecución";
            //ltMuestraCarta.Text = "<iframe src='Cartas\\CartaEjecucion.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
            ltMuestraCarta.Text = "<iframe src='Cartas\\CartaEjecucion.aspx" + Encrypt + "' style='width:100%; height:450px' style='border: none;'></iframe>";
        }

        private void MuestraCartaHold(int IdTramite)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string Encrypt = "?data=" + Negocio.Sistema.UrlCifrardo.Encrypt("Id=" + IdTramite);

            LabelTipoCarta.Text = "Carta hold";
            //ltMuestraCarta.Text = "<iframe src='Cartas\\CartaHold.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
            ltMuestraCarta.Text = "<iframe src='Cartas\\CartaHold.aspx" + Encrypt + "' style='width:100%; height:450px' style='border: none;'></iframe>";
        }

        private void MuestraCartaPCI(int IdTramite)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string Encrypt = "?data=" + Negocio.Sistema.UrlCifrardo.Encrypt("Id=" + IdTramite);

            LabelTipoCarta.Text = "Carta PCI";
            //ltMuestraCarta.Text = "<iframe src='Cartas\\CartaPCI.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
            ltMuestraCarta.Text = "<iframe src='Cartas\\CartaPCI.aspx" + Encrypt + "' style='width:100%; height:450px' style='border: none;'></iframe>";
        }
        private void MuestraCartaCancelado(int IdTramite)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string Encrypt = "?data=" + Negocio.Sistema.UrlCifrardo.Encrypt("Id=" + IdTramite);

            LabelTipoCarta.Text = "Carta Cancelado";
            //ltMuestraCarta.Text = "<iframe src='Cartas\\CartePDFCancelar.aspx?Id=" + IdTramite + "' style='width:100%; height:450px' style='border: none;'></iframe>";
            ltMuestraCarta.Text = "<iframe src='Cartas\\CartePDFCancelar.aspx" + Encrypt + "' style='width:100%; height:450px' style='border: none;'></iframe>";
        }
        #endregion


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