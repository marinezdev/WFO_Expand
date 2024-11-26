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
    public partial class anexaArchivosRes : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Promotoria.Promotoria promotoria = new WFO.Negocio.Procesos.Promotoria.Promotoria();
        WFO.Negocio.Procesos.Promotoria.Archivos archivos = new Negocio.Procesos.Promotoria.Archivos();
        WFO.Negocio.Procesos.Promotoria.Catalogos Catalogos = new Negocio.Procesos.Promotoria.Catalogos();
        WFO.Negocio.Procesos.Promotoria.TramiteN1 tramiteN1 = new Negocio.Procesos.Promotoria.TramiteN1();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["Sesion"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pintaChecks();
            pintRegreso();
            ConsultaMegasPromotoria();

            if (!IsPostBack)
            {
                pintaCabeceraHtml();
                MuestraInfoExpediente();
                MuestraDocumentos();
                //MuestraDatos();
            }
        }

        protected void Button_Continuar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EvaluaDocumento()))
            {
                // CARGA CONTENIDO DEL TRAMITE.
                prop.TramiteN1 oTramite = (prop.TramiteN1)Session["tramite"];


                oTramite.IdAgente = 0;
                if (!string.IsNullOrEmpty(txIdAgente.Text.ToString()))
                {
                    List<prop.agente_promotoria_usuario> agente_Promotoria_Usuarios = Catalogos.Agente_Promotoria_Usuarios(manejo_sesion.Usuarios.IdUsuario, txIdAgente.Text.ToString());
                    if (agente_Promotoria_Usuarios.Count != 0)
                    {
                        oTramite.IdAgente = agente_Promotoria_Usuarios[0].Id;
                    }
                }

                oTramite.IdUsuario = manejo_sesion.Usuarios.IdUsuario;

                List<prop.RespuestaNuevoTramiteN1> respuestaNuevoTramiteN1s = tramiteN1.NuevoTramiteN1(oTramite);

                //DataTable DataFolio = oAdmTramite.spTramiteNuevo(oTramite, oTramiteComple);

                string Folio = "";
                int IdTramite = 0;
                //string Error = "";

                //foreach (DataRow dr in DataFolio.Rows)
                //{
                //    Folio = dr["Folio"].ToString();
                //    IdTramite = Convert.ToInt32(dr["Id"].ToString());

                //}

                if (respuestaNuevoTramiteN1s[0].Folio != "KO")
                {
                    Folio = respuestaNuevoTramiteN1s[0].Folio.ToString();
                    IdTramite = Convert.ToInt32(respuestaNuevoTramiteN1s[0].Id.ToString());

                    List<prop.expediente> LstExpediente = new List<prop.expediente>();
                    if (Session["documentos"] != null)
                        LstExpediente = (List<prop.expediente>)Session["documentos"];

                    string msgError = registraDocumentos(IdTramite, LstExpediente);

                    Session.Remove("documentos");
                    Session.Remove("insumos");
                    Session.Remove("AnexoArchivos");
                    Session.Remove("tramite");
                    Session.Remove("TamExpedinte");

                    /* EJECUTAR PROCEDIMEINTO DE FUCION DE ARCHIVOS*/
                    string script = "swal({" +
                                                "title:'Registro terminado.'," +
                                                "text: 'Nuevo folio:  " + Folio + " '," +
                                                "icon: 'success'," +
                                                "buttons:" +
                                            "{" +
                                            "confirm:" +
                                                "{" +
                                                "text: 'Aceptar'," +
                                                        "value: true," +
                                                        "visible: true," +
                                                        "className: 'btn btn-info'," +
                                                        "closeModal: true" +
                                                "}" +
                                            "}" +
                                        "}).then(" +
                                            "function() {" +
                                                "var url = 'MisTramites.aspx'; " +
                                                "$(location).attr('href', url); " +
                                            "}" +
                                        ");";

                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                else
                {
                    string script = "";
                    script = "swal('Algo ocurrió!', 'No es posible registrar el trámite, inténtelo mas tarde.!', {" +
                                    "icon: 'warning'," +
                                    "buttons:" +
                                "{" +
                                "confirm:" +
                                "{" +
                                    "className: 'btn btn-warning'" +

                                "}" +
                            "}," +
                                "}); ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            else
            {
                string script = "";
                script = "swal('Advertencia!', 'Uno de tus archivos se encuentra dañado y no es posible procesarlo, recomendamos revisar y abrir el contenido de los archivos seleccionados !', {" +
                                "icon: 'warning'," +
                                "buttons:" +
                            "{" +
                            "confirm:" +
                            "{" +
                                "className: 'btn btn-warning'" +

                            "}" +
                        "}," +
                            "}); ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }

        }

        private string EvaluaDocumento()
        {
            string msgError = "";
            string strRutaServidor = "";
            string strArchivoOrigen = "";

            try
            {
                prop.expediente expediente = new prop.expediente();

                //strRutaServidor = Server.MapPath("~") + "\\DocsUp\\";
                strRutaServidor = Server.MapPath("~") + expediente.CarpetaInicial;
                List<prop.expediente> LstExpediente = new List<prop.expediente>();
                /* COMPRUEBA LA LISTA APÁRTIR DE LA SESION */
                if (Session["documentos"] != null)
                {
                    LstExpediente = (List<prop.expediente>)Session["documentos"];
                }

                List<string> lstArchivos = new List<string>();
                foreach (prop.expediente oDocumento in LstExpediente)
                {
                    //strArchivoOrigen = Server.MapPath("~") + "\\DocsUp\\" + oDocumento.NmArchivo;
                    strArchivoOrigen = Server.MapPath("~") + expediente.CarpetaInicial + oDocumento.NmArchivo;
                    if (File.Exists(strArchivoOrigen))
                    {
                        lstArchivos.Add(strArchivoOrigen);
                    }
                }
                // NUEVO ID DE NUEVO EXPEDIENTE FUSIONADO
                List<prop.control_archivos> control_Archivos = archivos.ControlArchivoNuevoID();
                int IdControlArchivo = control_Archivos[0].Id;
                string nombreFusion = control_Archivos[0].Clave + ".pdf";
                //string nombreFusion = control_Archivos[0].Clave + IdControlArchivo.ToString().PadLeft(12, '0') + ".pdf";

                msgError = Funciones.ManejoArchivos.Fusiona(lstArchivos, strRutaServidor + nombreFusion);
            }
            catch (Exception ex) { msgError = ex.Message; }
            return msgError;
        }

        private string registraDocumentos(int pIdTramite, List<prop.expediente> pLstDocumentos)
        {
            string msgError = "";
            string strRutaServidor = "";
            string strArchivoOrigen = "";
            try
            {
                prop.expediente expediente = new prop.expediente();

                //strRutaServidor = Server.MapPath("~") + "\\DocsUp\\";
                strRutaServidor = Server.MapPath("~") + expediente.CarpetaInicial;

                List<prop.expediente> LstExpediente = new List<prop.expediente>();
                //wfiplib.admExpediente oAdmExp = new wfiplib.admExpediente();

                /* COMPRUEBA LA LISTA APÁRTIR DE LA SESION */
                if (Session["documentos"] != null)
                {
                    LstExpediente = (List<prop.expediente>)Session["documentos"];
                }

                List<string> lstArchivos = new List<string>();
                foreach (prop.expediente oDocumento in LstExpediente)
                {
                    //strArchivoOrigen = Server.MapPath("~") + "\\DocsUp\\" + oDocumento.NmArchivo;
                    strArchivoOrigen = Server.MapPath("~") + expediente.CarpetaInicial + oDocumento.NmArchivo;
                    if (File.Exists(strArchivoOrigen))
                    {
                        oDocumento.Id_Tramite = pIdTramite;
                        //oAdmExp.NuevoRes(oDocumento); //archivos.Agregar_Expedientes_Tramite(pIdTramite, oDocumento.Id_Archivo, oDocumento.NmArchivo, oDocumento.NmOriginal, oDocumento.Activo, oDocumento.Fusion, oDocumento.Descripcion);
                        archivos.Agregar_Expedientes_Tramite(3, pIdTramite, oDocumento.Id_Archivo, oDocumento.NmArchivo, oDocumento.NmOriginal, oDocumento.Activo, oDocumento.Fusion, oDocumento.Descripcion);
                        lstArchivos.Add(strArchivoOrigen);

                    }
                }

                List<prop.control_archivos> control_Archivos = archivos.ControlArchivoNuevoID();
                int IdControlArchivo = control_Archivos[0].Id;
                string nombreFusion = control_Archivos[0].Clave + ".pdf";
                //string nombreFusion = control_Archivos[0].Clave + IdControlArchivo.ToString().PadLeft(12, '0') + ".pdf";

                msgError = Funciones.ManejoArchivos.Fusiona(lstArchivos, strRutaServidor + nombreFusion);
                if (string.IsNullOrEmpty(msgError))
                {
                    //archivos.Agregar_Expedientes_Tramite(pIdTramite, IdControlArchivo, nombreFusion, "Archivo Fusion", 1, 1, "");
                    //prop.expediente oFusion = new prop.expediente();
                    //oFusion.Id_Archivo = IdControlArchivo;
                    //oFusion.Id_Tramite = pIdTramite;
                    //oFusion.NmArchivo = nombreFusion;
                    //oFusion.NmOriginal = "";
                    //oFusion.Activo = 1;
                    //oFusion.Fusion = 1;
                    archivos.Agregar_Expedientes_Tramite(3, pIdTramite, IdControlArchivo, nombreFusion, "Archivo Fusion", 1, 1, "");
                    //archivos.Expediente_Mover(strRutaServidor + nombreFusion, expediente.CarpetaArchivada + nombreFusion);
                    File.Copy(strRutaServidor + nombreFusion, expediente.CarpetaArchivada + nombreFusion);
                    //oAdmExp.NuevoRes(oFusion);
                    msgError = "";
                }
            }

            catch (Exception ex) { msgError = ex.Message; }
            return "";

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

            // VALIDA LA CARGA DE LOS ARHIVOS.
            if (fileUpDocumento.HasFile)
            {
                //Variable en donde se almacena los archivos seleccionados
                HttpFileCollection multipleFiles = Request.Files;

                // TAMAÑO MAXIMO DE SUBIDA VALOR DEL 1 MEGABIY A BITS
                int mega = 1048576;
                int Meg = 0;

                if (Session["TamExpedinte"] != null)
                {
                    Meg = Convert.ToInt32(hfArchivos.Value) - Convert.ToInt32(Session["TamExpedinte"]);
                }
                else
                {
                    // CONSULTA A LA BASE DE DATOS LOS 10 MEGAS NECESARIOS
                    Meg = Convert.ToInt32(hfArchivos.Value); ;
                }

                int TamMax = mega * Meg;
                int TamArchi = 0;
                int TamArchRegistrados = 0;

                //leer cada archivo seleccionado
                for (int fileCount = 0; fileCount < multipleFiles.Count; fileCount++)
                {
                    //archivo seleccionado
                    HttpPostedFile uploadedFile = multipleFiles[fileCount];
                    int num = uploadedFile.ContentLength;

                    TamArchi += num;
                }

                // AGREGAR VALIDACION DE VARIABLE DE SESION 
                if (TamArchi > TamMax)
                {
                    LabRespuestaArchivosCarga.Text = "La carga de archivos sobrepaso los " + Meg + " MG";
                }
                else
                {
                    int ArchivosRegistrados = 0;
                    int ArchivosNoRegistrados = 0;
                    //leer cada archivo seleccionado
                    for (int fileCount = 0; fileCount < multipleFiles.Count; fileCount++)
                    {
                        //archivo seleccionado
                        HttpPostedFile uploadedFile = multipleFiles[fileCount];

                        String fileExtension = System.IO.Path.GetExtension(uploadedFile.FileName).ToLower();

                        if (".pdf".Contains(fileExtension) ^ ".jpg".Contains(fileExtension) ^ ".png".Contains(fileExtension))
                        {
                            prop.expediente expedientes = new prop.expediente();

                            // NUEVO ID DEL CADA ARCHIV A CARGAR EN  EL EXPEDIENTE.
                            List<prop.control_archivos> control_Archivos = archivos.ControlArchivoNuevoID();

                            int IdControlArchivo = control_Archivos[0].Id;
                            string nombreArchivo = control_Archivos[0].Clave;
                            //string nombreArchivo = control_Archivos[0].Clave + IdControlArchivo.ToString().PadLeft(12, '0');
                            //string directorioTemporal = Server.MapPath("~") + "\\DocsUp\\";
                            string directorioTemporal = Server.MapPath("~") + expedientes.CarpetaInicial;
                            string fileExtension2 = "";

                            uploadedFile.SaveAs(directorioTemporal + nombreArchivo + fileExtension);

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
                                if (LstArchivosDocumento.Count > 0)
                                {
                                    expedientes.Id = LstArchivosDocumento.Max(x => x.Id) + 1;
                                }
                                else
                                {
                                    expedientes.Id = 1;
                                }

                                for (int i = 0; i < LstArchivosDocumento.Count; i++)
                                {
                                    LstArchivosDocumento[i].Elemento = 0;
                                }

                                expedientes.Id_Archivo = IdControlArchivo;
                                expedientes.NmArchivo = nombreArchivo;
                                expedientes.NmOriginal = uploadedFile.FileName;
                                expedientes.Tam = uploadedFile.ContentLength;
                                expedientes.Activo = 1;
                                expedientes.Fusion = 0;
                                expedientes.Descripcion = "";
                                expedientes.Elemento = 1;

                                LstArchivosDocumento.Add(expedientes);

                                TablaArchivos.DataSource = LstArchivosDocumento;
                                TablaArchivos.DataBind();

                                //string script2 = "";
                                //script2 = "$('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); retirar();";
                                //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);

                                //lstDocumentos.DataSource = LstArchivosDocumento;
                                //lstDocumentos.DataValueField = "Id_Archivo";
                                //lstDocumentos.DataTextField = "NmOriginal";
                                //lstDocumentos.DataBind();

                                Session["documentos"] = LstArchivosDocumento;
                                //manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
                            }
                            ArchivosRegistrados = ArchivosRegistrados + 1;
                            TamArchRegistrados = TamArchRegistrados + uploadedFile.ContentLength;
                        }
                        else
                        {
                            ArchivosNoRegistrados = ArchivosNoRegistrados + 1;

                        }
                    }

                    int total = TamArchRegistrados / mega;

                    if (Session["TamExpedinte"] != null)
                    {
                        total = total + Convert.ToInt32(Session["TamExpedinte"]);
                        Session["TamExpedinte"] = total;
                    }
                    else
                    {
                        Session["TamExpedinte"] = total;
                    }
                    LabRespuestaArchivosCarga.Text = "El archivo registrados " + ArchivosRegistrados + "<BR> Rechazados " + ArchivosNoRegistrados + "<BR> Tamaño total " + total + " MG";
                }
            }
            else
            {
                LabRespuestaArchivosCarga.Text = "No a cargado ningun tipo de archivo.";
            }

            ltMuestraPdf.Text = "";

            MuestraInfoExpediente();
        }

        protected void EliminarArchivo(object sender, EventArgs e)
        {
            int delId = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            if (delId > 0)
            {
                List<prop.expediente> LstArchExpediente = new List<prop.expediente>();
                List<prop.expediente> LstArchExpedienteTmp = new List<prop.expediente>();
                if (Session["documentos"] != null) { LstArchExpediente = (List<prop.expediente>)Session["documentos"]; }
                //int contador = 0;
                int TamArchRegistrados = 0;


                if (LstArchExpediente.Count > 0)
                {
                    for (int i = 0; i < LstArchExpediente.Count; i++)
                    {
                        if (delId == LstArchExpediente[i].Id_Archivo)
                        {
                            // SUMA DE LOS ARCHIVOS NO ELIMINADO
                            TamArchRegistrados = TamArchRegistrados + LstArchExpediente[i].Tam;
                            LstArchExpediente.Remove(LstArchExpediente[i]);
                        }
                    }
                }



                for (int i = 0; i < LstArchExpediente.Count; i++)
                {
                    if (LstArchExpediente.Count - 1 == i)
                    {
                        LstArchExpediente[i].Elemento = 1;
                    }
                    else
                    {
                        LstArchExpediente[i].Elemento = 0;
                    }
                    LstArchExpediente[i].Id = i + 1;
                    LstArchExpedienteTmp.Add(LstArchExpediente[i]);
                }


                // TAMAÑO MAXIMO DE SUBIDA VALOR DEL 1 MEGABIY A BITS
                int mega = 1048576;
                int max = Convert.ToInt32(hfArchivos.Value);
                int total = TamArchRegistrados / mega;
                if (Session["TamExpedinte"] != null)
                {
                    total = Convert.ToInt32(Session["TamExpedinte"]) - total;
                    Session["TamExpedinte"] = total;
                }

                int restante = max - total;
                LabRespuestaArchivosCarga.Text = "Archivo elimindado.";


                Session["documentos"] = LstArchExpedienteTmp;


                TablaArchivos.DataSource = LstArchExpedienteTmp;
                TablaArchivos.DataBind();

                ltMuestraPdf.Text = "";

                //lstDocumentos.DataSource = LstArchExpedienteTmp;
                //lstDocumentos.DataValueField = "Id";
                //lstDocumentos.DataTextField = "NmOriginal";
                //lstDocumentos.DataBind();


            }
            MuestraInfoExpediente();

        }
        protected void SubirElmento(object sender, EventArgs e)
        {
            int delId = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            if (delId > 0)
            {
                List<prop.expediente> LstArchExpediente = new List<prop.expediente>();
                List<prop.expediente> LstArchExpedienteNueva = new List<prop.expediente>();
                List<prop.expediente> LstArchExpedienteRetorno = new List<prop.expediente>();

                if (Session["documentos"] != null) { LstArchExpediente = (List<prop.expediente>)Session["documentos"]; }

                prop.expediente ExpedienteSelect = new prop.expediente();

                for (int i = 0; i < LstArchExpediente.Count; i++)
                {
                    if (delId == LstArchExpediente[i].Id_Archivo)
                    {
                        ExpedienteSelect = LstArchExpediente[i];
                        LstArchExpediente.Remove(LstArchExpediente[i]);
                    }
                }

                for (int i = 0; i < LstArchExpediente.Count; i++)
                {
                    if ((ExpedienteSelect.Id - 1) == LstArchExpediente[i].Id)
                    {
                        LstArchExpedienteNueva.Add(ExpedienteSelect);
                        LstArchExpedienteNueva.Add(LstArchExpediente[i]);
                    }
                    else
                    {
                        LstArchExpedienteNueva.Add(LstArchExpediente[i]);
                    }
                }

                for (int i = 0; i < LstArchExpedienteNueva.Count; i++)
                {
                    if (LstArchExpedienteNueva.Count - 1 == i)
                    {
                        LstArchExpedienteNueva[i].Elemento = 1;
                    }
                    else
                    {
                        LstArchExpedienteNueva[i].Elemento = 0;
                    }

                    LstArchExpedienteNueva[i].Id = i + 1;
                    LstArchExpedienteRetorno.Add(LstArchExpedienteNueva[i]);
                }


                Session["documentos"] = LstArchExpedienteRetorno;

                TablaArchivos.DataSource = LstArchExpedienteRetorno;
                TablaArchivos.DataBind();

                ltMuestraPdf.Text = "";
            }

            MuestraInfoExpediente();
        }
        protected void BajarElmento(object sender, EventArgs e)
        {
            int delId = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            if (delId > 0)
            {
                List<prop.expediente> LstArchExpediente = new List<prop.expediente>();
                List<prop.expediente> LstArchExpedienteNueva = new List<prop.expediente>();
                List<prop.expediente> LstArchExpedienteRetorno = new List<prop.expediente>();

                if (Session["documentos"] != null) { LstArchExpediente = (List<prop.expediente>)Session["documentos"]; }

                prop.expediente ExpedienteSelect = new prop.expediente();

                for (int i = 0; i < LstArchExpediente.Count; i++)
                {
                    if (delId == LstArchExpediente[i].Id_Archivo)
                    {
                        ExpedienteSelect = LstArchExpediente[i];
                        LstArchExpediente.Remove(LstArchExpediente[i]);
                    }
                }

                for (int i = 0; i < LstArchExpediente.Count; i++)
                {
                    if ((ExpedienteSelect.Id + 1) == LstArchExpediente[i].Id)
                    {
                        LstArchExpedienteNueva.Add(LstArchExpediente[i]);

                        LstArchExpedienteNueva.Add(ExpedienteSelect);
                    }
                    else
                    {
                        LstArchExpedienteNueva.Add(LstArchExpediente[i]);
                    }
                }

                for (int i = 0; i < LstArchExpedienteNueva.Count; i++)
                {
                    if (LstArchExpedienteNueva.Count - 1 == i)
                    {
                        LstArchExpedienteNueva[i].Elemento = 1;
                    }
                    else
                    {
                        LstArchExpedienteNueva[i].Elemento = 0;
                    }
                    LstArchExpedienteNueva[i].Id = i + 1;
                    LstArchExpedienteRetorno.Add(LstArchExpedienteNueva[i]);
                }

                Session["documentos"] = LstArchExpedienteRetorno;

                TablaArchivos.DataSource = LstArchExpedienteRetorno;
                TablaArchivos.DataBind();

                ltMuestraPdf.Text = "";
            }

            MuestraInfoExpediente();
        }

        protected void MostrarArchivo(object sender, EventArgs e)
        {
            int delId = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            List<prop.expediente> LstArchExpediente = new List<prop.expediente>();
            prop.expediente Expediente = new prop.expediente();
            if (Session["documentos"] != null) { LstArchExpediente = (List<prop.expediente>)Session["documentos"]; }

            if (LstArchExpediente.Count > 0)
            {
                for (int i = 0; i < LstArchExpediente.Count; i++)
                {
                    if (delId == LstArchExpediente[i].Id_Archivo)
                    {
                        Expediente = LstArchExpediente[i];
                    }
                }
            }

            ltMuestraPdf.Text = "";
            ltMuestraPdf.Text = "<iframe src='" + EncripParametros("Archivo=" + Expediente.NmArchivo, "DisplaypdfPromo.aspx").URL + "' style='width:100%; height:540px' style='border: none;'></iframe>";

            MuestraInfoExpediente();

            //string script = "";
            //script = "window.open('" + EncripParametros("Archivo=" + Expediente.NmArchivo, "DisplaypdfPromo.aspx").URL + "','Expediente', 'width = 600, height = 400'); $('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]});";
            //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);



            //string script2 = "";
            //script2 = "$('#ModalPDF').modal('show'); $('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]});";
            //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);

        }





        //protected void btnEliminaDocumento_Click(object sender, EventArgs e)
        //{
        //    if (lstDocumentos.Items.Count > 0 && lstDocumentos.SelectedIndex > -1)
        //    {
        //        List<prop.expediente> LstArchExpediente = new List<prop.expediente>();
        //        List<prop.expediente> LstArchExpedienteTmp = new List<prop.expediente>();
        //        if (Session["documentos"] != null) { LstArchExpediente = (List<prop.expediente>)Session["documentos"]; }
        //        int contador = 0;
        //        int TamArchRegistrados = 0;

        //        foreach (prop.expediente oArchivo in LstArchExpediente)
        //        {
        //            if (contador != lstDocumentos.SelectedIndex)
        //            {
        //                LstArchExpedienteTmp.Add(oArchivo);
        //            }
        //            else
        //            {
        //                // SUMA DE LOS ARCHIVOS NO ELIMINADO
        //                TamArchRegistrados = TamArchRegistrados + oArchivo.Tam;
        //            }

        //            contador += 1;
        //        }
        //        // TAMAÑO MAXIMO DE SUBIDA VALOR DEL 1 MEGABIY A BITS
        //        int mega = 1048576;
        //        int max = Convert.ToInt32(hfArchivos.Value);
        //        int total = TamArchRegistrados / mega;
        //        if (Session["TamExpedinte"] != null)
        //        {
        //            total = Convert.ToInt32(Session["TamExpedinte"]) - total;
        //            Session["TamExpedinte"] = total;
        //        }

        //        int restante = max - total;
        //        LabRespuestaArchivosCarga.Text = "Archivo elimindado.";

        //        lstDocumentos.DataSource = LstArchExpedienteTmp;
        //        lstDocumentos.DataValueField = "Id";
        //        lstDocumentos.DataTextField = "NmOriginal";
        //        lstDocumentos.DataBind();
        //        Session["documentos"] = LstArchExpedienteTmp;


        //        string script = "";
        //        script = "EliminaExpediente(" + restante + "); ";
        //        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        //    }
        //    MuestraInfoExpediente();
        //}

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove("documentos");
            Session.Remove("insumos");
            Session.Remove("AnexoArchivos");
            Session.Remove("tramite");
            Response.Redirect("Default.aspx");
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            if (ValidaExpediente())
            {
                string script = "";
                script = "" +
                "$('#ContenidoPrincipal_BtnContinuar').attr('disabled','disabled');" +
                    "swal({" +
                        "icon: 'info'," +
                        "title: 'Confirmación'," +
                        "text: '¿Deseas crear el tramite?'," +
                        "type: 'warning'," +
                        "buttons:" +
                            "{" +
                                "cancel: " +
                                "{" +
                                    "visible: true," +
                                    "text: 'Cancelar'," +
                                    "className: 'btn btn-danger'" +
                                "}," +
                                "confirm: " +
                                "{" +
                                    "text: 'Aceptar'," +
                                    "className: 'btn btn-primary'" +
                                "}" +
                            "}" +
                    "}).then((Delete) => {" +
                        "if (Delete) {" +
                            "document.getElementById('ContenidoPrincipal_Button1').click(); " +
                        "}else {" +
                            "$('#ContenidoPrincipal_BtnContinuar').removeAttr('disabled');" +
                            "$('#btnAdd').trigger('click');" +
                            "swal.close();" +
                   "}" +
                "});";

                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }


        protected void daNombreDeAgente(object sender, EventArgs e)
        {
            NombreAgente();
        }

        protected void BtnposBack(object sender, EventArgs e)
        {
            string url = Session["URL"].ToString();
            //GuardaDatos();
            Response.Redirect(url);
        }

        protected void MuestraDocumentos()
        {
            /* LISTA LOS ARCHIVOS DEL DOCUMENTO */
            List<prop.expediente> LstArchivosDocumento = new List<prop.expediente>();
            /* COMPRUEBA LA LISTA APÁRTIR DE LA SESION */
            if (Session["documentos"] != null)
            {
                LstArchivosDocumento = (List<prop.expediente>)Session["documentos"];
            }

            TablaArchivos.DataSource = LstArchivosDocumento;
            TablaArchivos.DataBind();

            //lstDocumentos.DataSource = LstArchivosDocumento;
            //lstDocumentos.DataValueField = "Id";
            //lstDocumentos.DataTextField = "NmOriginal";
            //lstDocumentos.DataBind();
        }

        protected void rptTramite_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Consultar"))
            {
                string IdTramite = e.CommandArgument.ToString();

            }
        }



        protected void MuestraInfoExpediente()
        {
            LabelExpedienteRestante.Text = "";
            LabelRestantes.Text = "";

            int max = Convert.ToInt32(hfArchivos.Value);
            LabelExpedienteMax.Text = hfArchivos.Value;

            // CONSULTA EL TAMAÑO PERMITIDO PARA EL USUARIO.
            if (Session["TamExpedinte"] != null)
            {
                int tam = Convert.ToInt32(Session["TamExpedinte"]);

                int restante = max - tam;
                LabelExpedienteRestante.Text = restante.ToString();
                LabelRestantes.Text = restante.ToString();
            }
            else
            {
                LabelExpedienteRestante.Text = max.ToString();
                LabelRestantes.Text = max.ToString();
            }
            string script2 = "";
            script2 = "$('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); retirar();";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
        }

        protected void pintaCabeceraHtml()
        {
            prop.TramiteN1 oTramite = (prop.TramiteN1)Session["tramite"];
            string text = "<table>";

            text = string.Concat(new string[]
            {
                    text,
                    "<tr><td>Nombre(s):</td><td><b>",
                    oTramite.Nombre,
                    " ",
                    oTramite.ApPaterno,
                    " ",
                    oTramite.ApMaterno,
                    "</b></td></tr>"
            });
            //text = text + "<tr><td>RFC:</td><td><b>" + oTramite.RFC + "</b></td></tr>";
            //if (!String.IsNullOrEmpty(oTramite.TitularNombre))
            //{
            //    text = string.Concat(new string[]
            //    {
            //            text,
            //            "<tr><td>Titular:</td>" +
            //                "<td><b>",
            //                    oTramite.TitularNombre," ",oTramite.TitularApPat," ",oTramite.TitularApMat,"</b>" +
            //               "</td>" +
            //            "</tr>"
            //    });
            //}
            text += "</table>";

            ltInfContratante.Text = text;

        }

        protected void ConsultaMegasPromotoria()
        {
            prop.cat_promotoria cat_Promotoria = promotoria.ConsultaMegasPromotoria(manejo_sesion.Usuarios.IdUsuario);

            hfArchivos.Value = cat_Promotoria.Megas.ToString();

            lbNombreAgente.Text = cat_Promotoria.Megas.ToString();
            LabelTamExpediente.Text = cat_Promotoria.Megas.ToString();
        }

        private void pintRegreso()
        {
            if (Session["URL"] != null)
            {
                Regresar.Visible = true;
            }
        }

        private void pintaChecks()
        {
            if (Session["tramite"] != null)
            {
                prop.TramiteN1 oTramite = (prop.TramiteN1)Session["tramite"];

                List<prop.cat_DocRecEmicion> cat_docRecEmicion = catalogos.cat_DocRecEmicion_Selecionar_Persona(oTramite.TipoPersona, oTramite.IdTipoTramite);
                DocRequerid.DataSource = cat_docRecEmicion;
                DocRequerid.ID = "Id";
                DocRequerid.DataTextField = "Documentos";
                DocRequerid.DataValueField = "Id";
                DocRequerid.DataBind();
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
            else
            {
                string script = "";
                script = "ExpedinteIncompleto(); $('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]});";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            return respuesta;
        }

        protected void NombreAgente()
        {
            prop.TramiteN1 oTramite = (prop.TramiteN1)Session["tramite"];

            Mensajes.Text = "";
            lbNombreAgente.Text = "";
            lbEmailAgente.Text = "";
            //lbTelefonoAgente.Text = "";
            lbEmailAlternoAgente.Text = "";

            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];

            if (!string.IsNullOrEmpty(oTramite.IdAgente.ToString()))
            {
                List<prop.agente_promotoria_usuario> agente_Promotoria_Usuarios = Catalogos.Agente_Promotoria_Usuarios(manejo_sesion.Usuarios.IdUsuario, oTramite.IdAgente.ToString());
                if (agente_Promotoria_Usuarios.Count == 0)
                {
                    Mensajes.Text = "Agente no encotrado";
                }
                else
                {
                    for (int i = 0; i < agente_Promotoria_Usuarios.Count; i++)
                    {
                        lbNombreAgente.Text = agente_Promotoria_Usuarios[i].Nombre;
                        lbEmailAgente.Text = agente_Promotoria_Usuarios[i].Correo;
                        //lbTelefonoAgente.Text = agente_Promotoria_Usuarios[i].Telefono;
                        lbEmailAlternoAgente.Text = agente_Promotoria_Usuarios[i].Extencion;
                    }
                }
            }
            else
            {
                Mensajes.Text = "Coloca la clave del agente";
            }
        }

        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string Encrypt = Negocio.Sistema.UrlCifrardo.Encrypt(parametros);
            urlCifrardo.URL = Direccion + "?data=" + Encrypt;
            return urlCifrardo;
        }
    }
}