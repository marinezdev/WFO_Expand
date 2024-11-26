using DevExpress.Web;
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
    public partial class NuevoTramiteN4 : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Promotoria.Catalogos Catalogos = new Negocio.Procesos.Promotoria.Catalogos();
        WFO.Negocio.Procesos.Promotoria.Archivos archivos = new Negocio.Procesos.Promotoria.Archivos();
        WFO.Negocio.Procesos.Promotoria.TramiteN1 tramiteN1 = new Negocio.Procesos.Promotoria.TramiteN1();

        public string ArchivoMaximo1 { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            ArchivoMaximo1 = System.Web.Configuration.WebConfigurationManager.AppSettings["ArchivoMaximo1"].ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];

                // VALOR DEL TIPO DEL TRAMITE 
                int TipoTramite = 3;
                hfTipoTramite.Value = TipoTramite.ToString();

                FormatosFechas();
                ListaProductos(TipoTramite);
                ListaMonedas();

                MuestraDocumentos();
                MuestraInsumos();
                cargaPromotoria(manejo_sesion.Usuarios.IdUsuario);
                cargarNacionalidadesCombo_db(ref txNacionalidad);
                cargarNacionalidadesCombo_db(ref txTiNacionalidad);

            }
        }
        protected void LisProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Id = Funciones.Numeros.ConvertirTextoANumeroEntero(LisProducto.SelectedValue.ToString());
            LisSbproductos(Id);
        }

        protected void cboTipoContratante_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoContratante();
        }

        protected void dtFechaNacimiento_OnChanged(object sender, EventArgs e)
        {
            txRfc.Text = Funciones.RFC.CalcularRFCFisico(dtFechaNacimiento.Text.Trim(), txNombre.Text.ToUpper().Trim(), txApPat.Text.ToUpper().Trim(), txApMat.Text.ToUpper().Trim());
            antecedentesRFC();
        }

        protected void LisNacionalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelRespuestaNacionalidadFisico.Text = "";
            LabelRespuestaNacionalidadFisico.Text = ValidaPais(Funciones.Numeros.ConvertirTextoANumeroEntero(txNacionalidad.Value.ToString()));
        }

        protected void dtFechaConstitucion_OnChanged(object sender, EventArgs e)
        {
            txRfcMoral.Text = Funciones.RFC.CalcularRFCMoral(dtFechaConstitucion.Text.Trim(), txNomMoral.Text.ToUpper().Trim());
            antecedentesRFC();
        }

        protected void LisTitNacionalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelRespuestaNacionalidadTitular.Text = "";
            LabelRespuestaNacionalidadTitular.Text = ValidaPais(Funciones.Numeros.ConvertirTextoANumeroEntero(txTiNacionalidad.Value.ToString()));
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            double PrimaTotal = double.Parse(txtPrimaTotalGMM.Text.ToString());
            int IdMoneda = int.Parse(cboMoneda.Text.ToString());
            Double PrimaTotalConvertida = convertir(PrimaTotal, IdMoneda);

            Respuesta.Text = "";
            // if (PrimaTotalConvertida < 10000.00)
            //     SumaBasica.Text = "La prima total de acuerdo a cotización no es mayor a 10,000.00 Pesos";
            string RFC = "";
            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                RFC = txRfc.Text.ToString().Trim();
            }
            else
            {
                RFC = txRfcMoral.Text.ToString().Trim();
            }

            string Evalua = Funciones.RFC.ValidaContinuidadRFC(cboTipoContratante.SelectedValue, RFC);
            if (Evalua == "ok")
            {
                CrearTramite();
            }
            else
            {
                Mensajes.Text = Evalua;
            }
        }

        protected void TramiteTerminado(object sender, EventArgs e)
        {
            Session.Remove("documentos");
            Session.Remove("insumos");
            Session["documentos"] = null;
            Session["insumos"] = null;
            Response.Redirect("MisTramites.aspx");

        }
        protected void CrearTramite()
        {
            if (ValidaExpediente())
            {
                if (string.IsNullOrEmpty(EvaluaDocumento()))
                {
                    prop.TramiteN1 NuevoTramite = new prop.TramiteN1();

                    NuevoTramite.IdTipoTramite = 4;

                    manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
                    List<prop.promotoria_usuario> Promotoria_Usuarios = Catalogos.Promotoria_Usuarios(manejo_sesion.Usuarios.IdUsuario);
                    NuevoTramite.IdPromotoria = Promotoria_Usuarios[0].Id;

                    NuevoTramite.IdUsuario = manejo_sesion.Usuarios.IdUsuario;
                    NuevoTramite.IdStatus = 1;
                    NuevoTramite.idPrioridad = 5;
                    NuevoTramite.FechaSolicitud = dtFechaSolicitud.Text;

                    NuevoTramite.IdAgente = 0;
                    if (!string.IsNullOrEmpty(txClaveAgente.Text.ToString()))
                    {
                        List<prop.agente_promotoria_usuario> agente_Promotoria_Usuarios = Catalogos.Agente_Promotoria_Usuarios(manejo_sesion.Usuarios.IdUsuario, txClaveAgente.Text.ToString());
                        if (agente_Promotoria_Usuarios.Count != 0)
                        {
                            NuevoTramite.IdAgente = agente_Promotoria_Usuarios[0].Id;
                        }
                    }

                    NuevoTramite.IdProducto = Convert.ToInt32(LisProducto.SelectedValue);
                    NuevoTramite.IdSubProducto = Convert.ToInt32(LisSubproducto.SelectedValue);
                    //NuevoTramite.IdRiesgo = Convert.ToInt32(LisRiesgos.SelectedValue);
                    NuevoTramite.NumeroOrden = textNumeroOrden.Text;
                    NuevoTramite.idRamo = 2;
                    NuevoTramite.IdSisLegados = "";
                    NuevoTramite.kwik = "";
                    NuevoTramite.IdMoneda = Convert.ToInt32(cboMoneda.SelectedValue);

                    // VARIABLES VACIAS
                    NuevoTramite.TipoPersona = 0;
                    NuevoTramite.Nombre = "";
                    NuevoTramite.ApPaterno = "";
                    NuevoTramite.ApMaterno = "";
                    NuevoTramite.Sexo = "";
                    NuevoTramite.FechaNacimiento = "1900-01-01";
                    NuevoTramite.RFC = "";
                    NuevoTramite.IdNacionalidad = 0;
                    NuevoTramite.FechaConst = "1900-01-01";

                    NuevoTramite.TitularNombre = "";
                    NuevoTramite.TitularApPat = "";
                    NuevoTramite.TitularApMat = "";
                    NuevoTramite.IdTitularNacionalidad = 0;
                    NuevoTramite.TitularSexo = "";
                    NuevoTramite.TitularFechaNacimiento = "1900-01-01";
                    NuevoTramite.TitularContratante = 0;
                    NuevoTramite.PrimaCotizacion = 0;
                    NuevoTramite.Observaciones = "";

                    if (cboTipoContratante.SelectedValue.Equals("Fisica"))
                    {
                        NuevoTramite.TipoPersona = 1;
                        NuevoTramite.Nombre = txNombre.Text;
                        NuevoTramite.ApPaterno = txApPat.Text;
                        NuevoTramite.ApMaterno = txApMat.Text;
                        NuevoTramite.Sexo = txSexo.SelectedValue.ToString();
                        NuevoTramite.FechaNacimiento = dtFechaNacimiento.Text;
                        NuevoTramite.RFC = txRfc.Text;
                        NuevoTramite.IdNacionalidad = Convert.ToInt32(txNacionalidad.Value);
                    }
                    else if (cboTipoContratante.SelectedValue.Equals("Moral"))
                    {
                        NuevoTramite.TipoPersona = 2;
                        NuevoTramite.Nombre = txNomMoral.Text;
                        NuevoTramite.RFC = txRfcMoral.Text;
                        NuevoTramite.FechaConst = dtFechaConstitucion.Text;
                    }

                    if (CheckBox1.Checked.Equals(true))
                    {
                        NuevoTramite.TitularContratante = 1;
                        NuevoTramite.TitularNombre = txTiNombre.Text;
                        NuevoTramite.TitularApPat = txTiApPat.Text;
                        NuevoTramite.TitularApMat = txTiApMat.Text;
                        NuevoTramite.TitularFechaNacimiento = dtFechaNacimientoTitular.Text;
                        NuevoTramite.IdTitularNacionalidad = Convert.ToInt32(txNacionalidad.Value);
                        NuevoTramite.TitularSexo = txtSexoM.SelectedValue.ToString();
                    }

                    if (HombresClave.Checked.Equals(true))
                    {
                        NuevoTramite.HombreClave = 1;
                    }
                    else
                    {
                        NuevoTramite.HombreClave = 0;
                    }


                    NuevoTramite.PrimaCotizacion = Convert.ToDouble(txtPrimaTotalGMM.Text);
                    NuevoTramite.SumaBasica = Convert.ToDouble(txtSumaAseguradaBasica.Text);
                    NuevoTramite.Observaciones = txObervaciones.Text;

                    // REALIZA REGISTRO DEL NUEVO TRAMITE
                    List<prop.RespuestaNuevoTramiteN1> respuestaNuevoTramiteN1s = tramiteN1.NuevoTramiteN1(NuevoTramite);

                    if (respuestaNuevoTramiteN1s[0].Folio == "KO")
                    {
                        Respuesta.Text = "No registrado";
                    }
                    else
                    {

                        string resultadoExpediente = registraDocumentosExpediente(respuestaNuevoTramiteN1s[0].Id, NuevoTramite.IdTipoTramite);
                        string resultadoInsumo = registraDocumentosInsumos(respuestaNuevoTramiteN1s[0].Id, NuevoTramite.IdTipoTramite);
                        string script = "";
                        script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                        LabelFolio.Text = respuestaNuevoTramiteN1s[0].Folio.ToString();
                    }
                }
                else
                {
                    string script = "";
                    script = "ErrorArchvios();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            else
            {
                string script = "";
                script = "FormularioIncompleto();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                Mensajes.Text = "No se han subido archivos al expediente";
            }
        }

        protected void DatotsDeAgente(object sender, EventArgs e)
        {
            DatosAgente();
        }

        private double convertir(double numero, int IdMoneda)
        {
            double total = 0;
            List<prop.cat_moneda> cat_Monedas = Catalogos.BuscaMonedaId(IdMoneda);
            double Moneda = Convert.ToDouble(cat_Monedas[0].Valor);
            total = numero * Moneda;
            return total;
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

        protected void DatosAgente()
        {
            LbAgenteRespuesta.Text = "";
            lbNombreAgente.Text = "";
            lbEmailAgente.Text = "";
            lbTelefonoAgente.Text = "";
            lbExtensionAgente.Text = "";

            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];

            if (!string.IsNullOrEmpty(txClaveAgente.Text.ToString()))
            {
                List<prop.agente_promotoria_usuario> agente_Promotoria_Usuarios = Catalogos.Agente_Promotoria_Usuarios(manejo_sesion.Usuarios.IdUsuario, txClaveAgente.Text.ToString());
                if (agente_Promotoria_Usuarios.Count == 0)
                {
                    LbAgenteRespuesta.Text = "Agente no encotrado";
                }
                else
                {
                    for (int i = 0; i < agente_Promotoria_Usuarios.Count; i++)
                    {
                        lbNombreAgente.Text = agente_Promotoria_Usuarios[i].Nombre;
                        lbEmailAgente.Text = agente_Promotoria_Usuarios[i].Correo;
                        lbTelefonoAgente.Text = agente_Promotoria_Usuarios[i].Telefono;
                        lbExtensionAgente.Text = agente_Promotoria_Usuarios[i].Extencion;
                    }
                }
            }
            else
            {
                LbAgenteRespuesta.Text = "Coloca la clave del agente";
            }
        }

        private string ValidaPais(int Id)
        {

            List<prop.cat_pais> cat_Pais_Sancionado = Catalogos.cat_Pais_Sancionado(Id);
            string respuesta = "";

            if (cat_Pais_Sancionado[0].Sancionado > 0)
            {
                respuesta = "Este país se encuentra en la lista de países sancionados";
            }
            return respuesta;
        }

        private void cargaPromotoria(int id)
        {
            List<prop.promotoria_usuario> Promotoria_Usuarios = Catalogos.Promotoria_Usuarios(id);

            for (int i = 0; i < Promotoria_Usuarios.Count; i++)
            {
                texClave.Text = Promotoria_Usuarios[i].Clave;
                texRegion.Text = Promotoria_Usuarios[i].Clave_Region + " - " + Promotoria_Usuarios[i].Region;
                texGerenteComercial.Text = Promotoria_Usuarios[i].Clave_Gerente + " - " + Promotoria_Usuarios[i].Gerente;
                texEjecuticoComercial.Text = Promotoria_Usuarios[i].Clave_Ejecutivo + " - " + Promotoria_Usuarios[i].Ejecutivo;
                texEjecuticoFront.Text = Promotoria_Usuarios[i].Clave_Front + " - " + Promotoria_Usuarios[i].Front;
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

        protected void antecedentesRFC()
        {
            LabelRespuestaRFCFisico.Text = "";
            LabelRespuestaRFCMoral.Text = "";

            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                string RFC = txRfc.Text.Trim().Replace("-", "");
                List<prop.TramiteN1> tramiteN1s = Catalogos.BustatramiteN1RFC(RFC);
                if (tramiteN1s[0].RFC == "1")
                {
                    LabelRespuestaRFCFisico.Text = "Ya existen trámites registrados para el RFC";
                }
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                string RFC = txRfcMoral.Text.Trim().Replace("-", "");
                List<prop.TramiteN1> tramiteN1s = Catalogos.BustatramiteN1RFC(RFC);
                if (tramiteN1s[0].RFC == "1")
                {
                    LabelRespuestaRFCMoral.Text = "Ya existen trámites registrados para el RFC";
                }
            }

        }

        protected void TipoContratante()
        {
            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                pnPrsFisica.Visible = true;
                pnPrsMoral.Visible = false;
                CheckBox1.Enabled = true;
                CheckBox2.Enabled = true;
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                pnPrsMoral.Visible = true;
                pnPrsFisica.Visible = false;
                CheckBox1.Checked = true;
                CheckB1();
                CheckBox1.Enabled = false;
                CheckBox2.Enabled = false;
            }
            else
            {
                pnPrsFisica.Visible = false;
                pnPrsMoral.Visible = false;
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckB1();
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckB2();
        }

        protected void CheckB1()
        {
            if (CheckBox1.Checked.Equals(true))
            {
                CheckBox2.Checked = false;
                DiferenteContratante.Visible = true;
            }
            else if (CheckBox1.Checked.Equals(false))
            {
                DiferenteContratante.Visible = false;
            }
            else
            {
                DiferenteContratante.Visible = false;
            }
        }

        protected void CheckB2()
        {
            if (CheckBox2.Checked.Equals(true))
            {
                CheckBox1.Checked = false;
                CheckB1();
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

                    // ###Pendiente: Realizar esta acción parametrizable                  // if (fileSize < 41943040)
                    if (fileSize < int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ArchivoMaximoByte1"].ToString()))
                    {
                        List<prop.control_archivos> control_Archivos = archivos.ControlArchivoNuevoID();
                        int IdControlArchivo = control_Archivos[0].Id;
                        string nombreArchivo = IdControlArchivo.ToString().PadLeft(12, '0');
                        string directorioTemporal = Server.MapPath("~") + "\\DocsUp\\";

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
                            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
                            cargaPromotoria(manejo_sesion.Usuarios.IdUsuario);
                        }
                        else { LabRespuestaArchivosCarga.Text = "El archivo no se puede convertir a PDF."; }
                    }
                    else
                    {
                        // ###Pendiente: Hacer las alertas con la función de Rogelio
                        LabRespuestaArchivosCarga.Text = "El archivo excede el límite de " + ArchivoMaximo1 + "MB.";
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
                    string nombreArchivo = IdArchivo.ToString().PadLeft(8, '0') + fileExtension;
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
                    manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
                    cargaPromotoria(manejo_sesion.Usuarios.IdUsuario);
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


        private void FormatosFechas()
        {
            // INICIO DE FECHAS 
            DateTime validateFechaSolicitud = DateTime.Today;

            dtFechaSolicitud.MaxDate = validateFechaSolicitud;
            dtFechaSolicitud.MinDate = validateFechaSolicitud.AddDays(-30);
            dtFechaSolicitud.UseMaskBehavior = true;
            dtFechaSolicitud.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");
            dtFechaSolicitud.Date = DateTime.Today;

            dtFechaConstitucion.MaxDate = DateTime.Today;
            dtFechaConstitucion.UseMaskBehavior = true;
            dtFechaConstitucion.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");

            dtFechaNacimiento.MaxDate = validateFechaSolicitud.AddYears(-18);
            dtFechaNacimiento.UseMaskBehavior = true;
            dtFechaNacimiento.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");

            dtFechaNacimientoTitular.UseMaskBehavior = true;
            dtFechaNacimientoTitular.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");

            dtFechaConstitucion.Date = DateTime.Today;
            dtFechaNacimiento.Date = DateTime.Today.AddYears(-18);
            dtFechaNacimientoTitular.Date = DateTime.Today;
        }

        private void cargarNacionalidadesCombo_db(ref ASPxComboBox objDDL)
        {
            List<prop.cat_pais> cat_pais = Catalogos.Cat_Paises();
            objDDL.DataSource = cat_pais;
            objDDL.TextField = "PaisNombre";
            objDDL.ValueField = "Id";
            objDDL.DataBind();
            objDDL.SelectedIndex = 135;
        }

        protected void MuestraInsumos()
        {
            List<prop.insumos> LstArchivosInsumo = new List<prop.insumos>();
            if (Session["insumos"] != null) { LstArchivosInsumo = (List<prop.insumos>)Session["insumos"]; }

            lstInsumos.DataSource = LstArchivosInsumo;
            lstInsumos.DataValueField = "Id";
            lstInsumos.DataTextField = "NmOriginal";
            lstInsumos.DataBind();
        }

        private void ListaMonedas()
        {
            List<prop.cat_moneda> cat_monedas = Catalogos.Cat_Monedas();
            cboMoneda.DataSource = cat_monedas;
            cboMoneda.DataBind();
            cboMoneda.DataTextField = "Nombre";
            cboMoneda.DataValueField = "Id";
            cboMoneda.DataBind();

            cboMoneda.SelectedIndex = 0;
        }

        private void ListaProductos(int TipoTramite)
        {
            List<prop.cat_producto> cat_Productos = Catalogos.Cat_productos(TipoTramite);
            LisProducto.DataSource = cat_Productos;
            LisProducto.DataBind();
            LisProducto.DataTextField = "Nombre";
            LisProducto.DataValueField = "Id";
            LisProducto.DataBind();
        }

        private void LisSbproductos(int Id)
        {
            List<prop.cat_subproducto> cat_Subproductos = Catalogos.Cat_subproductos(Id);
            LisSubproducto.DataSource = cat_Subproductos;
            LisSubproducto.DataBind();
            LisSubproducto.DataTextField = "Nombre";
            LisSubproducto.DataValueField = "Id";
            LisSubproducto.DataBind();
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

            lstDocumentos.DataSource = LstArchivosDocumento;
            lstDocumentos.DataValueField = "Id";
            lstDocumentos.DataTextField = "NmOriginal";
            lstDocumentos.DataBind();
        }

        private string registraDocumentosExpediente(int pIdTramite, int TipoTramite)
        {
            string msgError = "";
            string strRutaServidor = "";
            string strArchivoOrigen = "";
            try
            {
                strRutaServidor = Server.MapPath("~") + "\\DocsUp\\";

                List<prop.expediente> LstExpediente = new List<prop.expediente>();

                /* COMPRUEBA LA LISTA APÁRTIR DE LA SESION */
                if (Session["documentos"] != null)
                {
                    LstExpediente = (List<prop.expediente>)Session["documentos"];
                }

                List<string> lstArchivos = new List<string>();
                foreach (prop.expediente oDocumento in LstExpediente)
                {
                    strArchivoOrigen = Server.MapPath("~") + "\\DocsUp\\" + oDocumento.NmArchivo;
                    if (File.Exists(strArchivoOrigen))
                    {
                        archivos.Agregar_Expedientes_Tramite(TipoTramite, pIdTramite, oDocumento.Id_Archivo, oDocumento.NmArchivo, oDocumento.NmOriginal, oDocumento.Activo, oDocumento.Fusion, oDocumento.Descripcion);
                        lstArchivos.Add(strArchivoOrigen);
                    }
                }

                List<prop.control_archivos> control_Archivos = archivos.ControlArchivoNuevoID();
                int IdControlArchivo = control_Archivos[0].Id;
                string nombreFusion = IdControlArchivo.ToString().PadLeft(12, '0') + ".pdf";

                msgError = Funciones.ManejoArchivos.Fusiona(lstArchivos, strRutaServidor + nombreFusion);
                if (string.IsNullOrEmpty(msgError))
                {
                    archivos.Agregar_Expedientes_Tramite(TipoTramite, pIdTramite, IdControlArchivo, nombreFusion, "Archivo Fusion", 1, 1, "");
                    msgError = "";
                }
            }

            catch (Exception ex) { msgError = ex.Message; }
            return "";
        }
        private string EvaluaDocumento()
        {
            string msgError = "";
            string strRutaServidor = "";
            string strArchivoOrigen = "";

            try
            {
                strRutaServidor = Server.MapPath("~") + "\\DocsUp\\";
                List<prop.expediente> LstExpediente = new List<prop.expediente>();
                /* COMPRUEBA LA LISTA APÁRTIR DE LA SESION */
                if (Session["documentos"] != null)
                {
                    LstExpediente = (List<prop.expediente>)Session["documentos"];
                }

                List<string> lstArchivos = new List<string>();
                foreach (prop.expediente oDocumento in LstExpediente)
                {
                    strArchivoOrigen = Server.MapPath("~") + "\\DocsUp\\" + oDocumento.NmArchivo;
                    if (File.Exists(strArchivoOrigen))
                    {
                        lstArchivos.Add(strArchivoOrigen);
                    }
                }
                // NUEVO ID DE NUEVO EXPEDIENTE FUSIONADO
                List<prop.control_archivos> control_Archivos = archivos.ControlArchivoNuevoID();
                int IdControlArchivo = control_Archivos[0].Id;
                string nombreFusion = IdControlArchivo.ToString().PadLeft(12, '0') + ".pdf";

                msgError = Funciones.ManejoArchivos.Fusiona(lstArchivos, strRutaServidor + nombreFusion);
            }
            catch (Exception ex) { msgError = ex.Message; }
            return msgError;
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
    }
}

























































