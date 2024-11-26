using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.Operacion;
using promotoria = WFO.Propiedades.Procesos.Promotoria;
using captura = WFO.Propiedades.Procesos.Operacion.Captura;
using DevExpress.Web.ASPxTreeList;
using System.IO;
using DevExpress.Web;


namespace WFO.Procesos.Operador
{
    public partial class TramiteTarea : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Operacion.TramiteProcesar Tramites = new WFO.Negocio.Procesos.Operacion.TramiteProcesar();
        WFO.Negocio.Procesos.Operacion.Mesas mesas = new Negocio.Procesos.Operacion.Mesas();
        WFO.Negocio.Procesos.Promotoria.Archivos archivos = new Negocio.Procesos.Promotoria.Archivos();
        WFO.Negocio.Procesos.Promotoria.Catalogos Catalogos = new Negocio.Procesos.Promotoria.Catalogos();
        WFO.Negocio.Procesos.Operacion.Bitacora bitacora = new Negocio.Procesos.Operacion.Bitacora();
        WFO.Negocio.Procesos.Operacion.MotivosSuspension _MotivosHold = new Negocio.Procesos.Operacion.MotivosSuspension();
        WFO.Negocio.Procesos.Operacion.CapturaMasiva.Catalogos capturaMasiva = new Negocio.Procesos.Operacion.CapturaMasiva.Catalogos();
        WFO.Negocio.Procesos.Operacion.CapturaMasiva.Asegurados asegurados = new Negocio.Procesos.Operacion.CapturaMasiva.Asegurados();
        WFO.Negocio.Procesos.Operacion.CapturaMasiva.CoAsegurados coAsegurados = new Negocio.Procesos.Operacion.CapturaMasiva.CoAsegurados();
        WFO.Negocio.Procesos.Operacion.CapturaMasiva.AgentesDXN agentesDXN = new Negocio.Procesos.Operacion.CapturaMasiva.AgentesDXN();
        WFO.Negocio.Procesos.Operacion.CapturaMasiva.Tarjetas tarjetas = new Negocio.Procesos.Operacion.CapturaMasiva.Tarjetas();
        WFO.Negocio.Procesos.Operacion.CapturaMasiva.Asegurado_direciones asegurado_Direciones = new Negocio.Procesos.Operacion.CapturaMasiva.Asegurado_direciones();
        WFO.Negocio.Procesos.Operacion.PermisosMesaControles permisosMesa = new Negocio.Procesos.Operacion.PermisosMesaControles();
        WFO.Negocio.Procesos.Operacion.Indicador_StatusMesas indicador = new Negocio.Procesos.Operacion.Indicador_StatusMesas();
        WFO.Negocio.Procesos.Operacion.Cat_CheckBox_Mesa cat_CheckBox = new Negocio.Procesos.Operacion.Cat_CheckBox_Mesa();

        List<prop.TramiteProcesar> Tramite_a_Procesar;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Evitamos la carga automatica de la tarea de call center
            Session["TramitesAutomaticos"] = false;


            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
            hfIdTramite.Value = "0";

            if (urlCifrardo.Result)
            {
                hfIdProceso.Value = urlCifrardo.IdProceso;

                if (urlCifrardo.Procesable.Length > 0)
                {
                    hfIdTramite.Value = urlCifrardo.Procesable;
                }
            }
            else
            {
                string script = "";
                script = "window.location.href='MisTramitesCallCenter.aspx'; ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }

            //hfIdTramite.Value = "0";
            //hfIdProceso.Value = Request.QueryString["IdProceso"].ToString();
            //if (!String.IsNullOrEmpty(Request.QueryString["Procesable"]))
            //    hfIdTramite.Value = Request.QueryString["Procesable"].ToString();

            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MotivosSuspension();

            if (int.Parse(hfIdProceso.Value) > 0)
            {
                if (!IsPostBack)
                {
                    PostBack(int.Parse(hfIdProceso.Value), manejo_sesion.Usuarios.IdUsuario);
                }
            }
        }

        private void MotivosSuspension()
        {
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
            List<prop.MotivosSuspension> lsMotivosSuspension = _MotivosHold.SelecionarMotivosProcesos(int.Parse(urlCifrardo.IdProceso));

            //List<prop.MotivosSuspension> lsMotivosSuspension = _MotivosHold.SelecionarMotivosProcesos(int.Parse(Request.QueryString["IdProceso"].ToString()));

            treeListHold.ClearNodes();
            treeListHold.DataSource = lsMotivosSuspension.Where(MotivoSuspension => lsMotivosSuspension.FirstOrDefault(valor => MotivoSuspension.IdTramiteTipoRechazo == 1) != null);           // SELECT * FROM cat_Tramite_RechazosTipos;
            treeListHold.DataBind();
            treeListHold.ExpandToLevel(3);

            treeListSuspender.ClearNodes();
            treeListSuspender.DataSource = lsMotivosSuspension.Where(MotivoSuspension => lsMotivosSuspension.FirstOrDefault(valor => MotivoSuspension.IdTramiteTipoRechazo == 2) != null);      // SELECT * FROM cat_Tramite_RechazosTipos;
            treeListSuspender.DataBind();
            treeListSuspender.ExpandToLevel(3);

            treeListCancelar.ClearNodes();
            treeListCancelar.DataSource = lsMotivosSuspension.Where(MotivoSuspension => lsMotivosSuspension.FirstOrDefault(valor => MotivoSuspension.IdTramiteTipoRechazo == 3) != null);      // SELECT * FROM cat_Tramite_RechazosTipos;
            treeListCancelar.DataBind();
            treeListCancelar.ExpandToLevel(3);

            //treeListRechazar.ClearNodes();
            //treeListRechazar.DataSource = lsMotivosSuspension.Where(MotivoSuspension => lsMotivosSuspension.FirstOrDefault(valor => MotivoSuspension.IdTramiteTipoRechazo == 4) != null);      // SELECT * FROM cat_Tramite_RechazosTipos;
            //treeListRechazar.DataBind();
            //treeListRechazar.ExpandToLevel(3);

        }

        private void PostBack(int pIdMesa, int IdUsuario)
        {
            // Permisos de Botones
            btnHold.OnClientClick = "ShowHold(); return false;";
            btnSuspender.OnClientClick = "ShowSuspender(); return false;";
            btnRechazar.OnClientClick = "ShowRechazar(); return false;";
            btnPausa.OnClientClick = "ShowPausa(); return false;";
            btnCancelacion.OnClientClick = "ShowCancelar(); return false;";

            PintaProceso(pIdMesa, IdUsuario);
            VerificaTareaDisponible(pIdMesa);
        }

        private void PintaProceso(int pIdMesa, int IdUsuario)
        {
            // Verifica que el usuario tenga acceso al proceso
            List<prop.Procesos> mesa = mesas.SelecionarUsuariosProcesos(IdUsuario, pIdMesa);

            if (mesa.Count > 0)
            {
                ListaMonedas();
                //ListaTipoTramite();
                //cargarNacionalidadesCombo_db(ref txNacionalidad);
                //cargarNacionalidadesCombo_db(ref txTiNacionalidad);

                LabelNombreMesa.Text = "Proceso - " + mesa[0].nombre;
                hfNombreProceso.Value = mesa[0].nombre;



                // ACTIVA LOS CONTROLES EN LAS MESAS
                prop.PermisosMesaControles permisosMesaControles = new prop.PermisosMesaControles();

                btnHold.Visible = true;         //Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "btnHold")).Activo);
                btnSuspender.Visible = true;    //Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "btnSuspender")).Activo);


                btnAceptar.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "btnAceptar")).Activo);
                btnPCI.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "btnPCI")).Activo);
                btnPausa.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "btnPausa")).Activo);
                btnDetener.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "btnDetener")).Activo);
                btnRechazar.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "btnRechazar")).Activo);
                btnEnviaMesa.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "btnEnviaMesa")).Activo);
                btnSelccionCompleta.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "btnSelccionCompleta")).Activo);
                btnCoasegurados.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "btnCoasegurados")).Activo);
                EditarCaptura.Visible = false; // Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "EditarCaptura")).Activo);
                btnCancelacion.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "btnCancelacion")).Activo);
                PolizaSisLegados.Visible = true; // Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "PolizaSisLegados")).Activo);
                KWIK.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "KWIK")).Activo);
                Captura.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "Captura")).Activo);
                MesaCapturaMasiva.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "MesaCapturaMasiva")).Activo);
                LabelConsultaCaptura.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "LabelConsultaCaptura")).Activo);
                LabelRiesgos.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "LabelRiesgos")).Activo);
                LisRiesgos.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "LisRiesgos")).Activo);
                PanelRiesgo.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "PanelRiesgo")).Activo);
                PanelCapturaCoasegurados.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "PanelCapturaCoasegurados")).Activo);
                PanelCapturaCoasegurados.Enabled = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "PanelCapturaCoasegurados")).Activo);
                PanelDatosKwik.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "PanelDatosKwik")).Activo);
                LinkButtonMuestraCaptura.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "LinkButtonMuestraCaptura")).Activo);
                PanelDatosN3.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "PanelDatosN3")).Activo);
                PanelDatosN1C4.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "PanelDatosN1C4")).Activo);
                PanelCheckBoxList.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "PanelCheckBoxList")).Activo);
                PanelObservacionesPubicas.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "PanelObservacionesPubicas")).Activo);
                PanelSeleccionCompleta.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "PanelSeleccionCompleta")).Activo);

                // NUEVO DATO REQUERIMIENTO JUNIO 2020
                PanelInstitucionesNC.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "PanelInstitucionesNC")).Activo);
                PanelInstitucionesInfo.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "PanelInstitucionesInfo")).Activo);

                // NF
                PanelSeleccionCompletaChecs.Visible = Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(pIdMesa, "PanelSeleccionCompletaChecs")).Activo);

                FormatosFechas();
                //CargaCatalogosCaptura();
                //CargaCatalogosCapturaMasiva();

                TextNumPolizaSisLegado.Enabled = true;

                switch (mesa[0].nombre)
                {
                    case "CALIDAD":
                        btnAceptar.Text = "No procede";
                        TextNumPolizaSisLegado.Attributes["disabled"] = "enabled";

                        break;

                    case "KWIK":
                        TextNumPolizaSisLegado.Attributes["disabled"] = "enabled";
                        break;

                    default:
                        break;
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        private void VerificaTareaDisponible(int pIdMesa)
        {
            if (Convert.ToBoolean(Session["TramitesAutomaticos"]) == false && hfIdTramite.Value.ToString() == "0")
            {
                Response.Redirect("Default.aspx", true);
                return;
            }

            // Obtenemos la tarea a realizar
            Tramite_a_Procesar = Tramites.ObtenerTarea(manejo_sesion.Usuarios.IdUsuario, pIdMesa, int.Parse(hfIdTramite.Value));

            // Verificamos si hay una tarea disponible
            if (Tramite_a_Procesar.Count > 0)
            {
                for (int i = 0; i < Tramite_a_Procesar.Count; i++)
                {
                    // COLOCA EL ID DEL TRAMITE PARA SER UTILIZADO DESPUES
                    hfIdTramite.Value = Tramite_a_Procesar[i].IdTramite.ToString();
                    hfTipoTramite.Value = Tramite_a_Procesar[i].IdTipoTramite.ToString();

                    LabelFlujo.Text = Tramites.ObtenerTipoTramite(Tramite_a_Procesar[i].IdTramite).Nombre;

                    // VOLVERLO PARAMETRIZABLE, LOS VALORES NO DEBEN DE ESTAR DEFINIDOS EN EL CODIGO
                    // MODIFICAR CONSULTA DE BUSQUEDA DEL RIESGO OBTENIDA EN EL TRAMITE_DEL_N3
                    if (Tramite_a_Procesar[i].IdTipoTramite == 2)
                    {
                        LabelRiesgos.Visible = true;
                        LisRiesgos.Visible = true;
                        PanelRiesgo.Visible = true;
                        LisRiesgos.SelectedValue = Tramites.ObtenerRiesgoTramite(Tramite_a_Procesar[i].IdTramite).Id.ToString();

                        InfoRiesgo.Text = LisRiesgos.SelectedItem.Text.ToLowerInvariant();
                    }


                    if (Int32.Parse(hfIdTramite.Value) <= 0)
                    {
                        Response.Redirect("Default.aspx?msj=1", true);
                    }
                    else
                    {
                        List<prop.Mesa> mesaToSend = mesas.ObtenerMesasToSend(int.Parse(hfIdTramite.Value), manejo_sesion.Usuarios.IdUsuario, pIdMesa);
                        if (mesaToSend.Count > 0)
                        {
                            cboToSend.DataSource = mesaToSend;
                            cboToSend.DataValueField = "Id";
                            cboToSend.DataTextField = "nombre";
                            cboToSend.DataBind();
                            cboToSend.Visible = true;
                        }

                        // LLENA LA INFORMACION DEL TRAMITE EN TODOS LOS CAMPOS, SELECT Y LABES

                        //FORMULARIO DE ADMICION
                        FechaRegistroAdmicion.Text = Tramite_a_Procesar[i].FechaRegistro.ToString();
                        cboMoneda.SelectedValue = Tramite_a_Procesar[i].IdMoneda.ToString();
                        txtPrimaTotalGMM.Text = String.Format("{0:N0}", Convert.ToDecimal(Tramite_a_Procesar[i].PrimaCotizacion));

                        //TABLA INFORMATIVA 
                        InfoFechaRegistro.Text = Tramite_a_Procesar[i].FechaRegistro.ToString();
                        InfoGMMoneda.Text = cboMoneda.SelectedItem.ToString();
                        InfoGMPrimaTotal.Text = String.Format("{0:C2}", Convert.ToDecimal(Tramite_a_Procesar[i].PrimaCotizacion));

                        //NUEVO SDATOS 
                        DropDownListTipoTramite.SelectedValue = Tramite_a_Procesar[i].IdInstitucion.ToString();

                        //// RMF --- 220225 --- Actualización de sistema 
                        //// ##### --- Aquí debería de ir el tipo de Servicio
                        //if (Tramite_a_Procesar[i].Institucion.ToString().Length > 3)
                        //{
                        //    LabelInstitucionesInfo.Text = Tramite_a_Procesar[i].Institucion.ToString();
                        //}
                        //else
                        //{
                        //    LabelInstitucionesInfo.Text = "No registrado";
                        //}


                        List<promotoria.promotoria_usuario> Promotoria = Catalogos.Promotoria_Seleccionar_PorIdTramite(Tramite_a_Procesar[i].IdPromotoria, Tramite_a_Procesar[i].IdTramite);

                        for (int p = 0; p < Promotoria.Count; p++)
                        {
                            texClave.Text = Promotoria[p].Clave;
                            texRegion.Text = Promotoria[p].Clave_Region + " - " + Promotoria[p].Region;
                            texGerenteComercial.Text = Promotoria[p].Clave_Gerente + " - " + Promotoria[p].Gerente;
                            texEjecuticoComercial.Text = Promotoria[p].Clave_Ejecutivo + " - " + Promotoria[p].Ejecutivo;
                            texEjecuticoFront.Text = Promotoria[p].Clave_Front + " - " + Promotoria[p].Front;

                            //TABLA INFORMATIVA 
                            InfoClave.Text = Promotoria[p].Clave;
                            InfoZona.Text = Promotoria[p].Zona;
                            //InfoRegion.Text = Promotoria[p].Clave_Region + " - " + Promotoria[p].Region;
                            //InfoGerente.Text = Promotoria[p].Clave_Gerente + " - " + Promotoria[p].Gerente;
                            InfoEjecutivo.Text = Promotoria[p].Clave_Ejecutivo + " - " + Promotoria[p].Ejecutivo;
                            InfoEjecutivoFront.Text = Promotoria[p].Clave_Front + " - " + Promotoria[p].Front;
                        }

                        textNumeroOrden.Text = Tramite_a_Procesar[i].NumeroOrden;
                        DateTime FechaSolicitud = Convert.ToDateTime(Tramite_a_Procesar[i].FechaSolicitud);
                        dtFechaSolicitud.Text = FechaSolicitud.ToString("dd/MM/yyyy");
                        cboTipoContratante.SelectedValue = Tramite_a_Procesar[i].TipoPersona.ToString();

                        //TABLA INFORMATIVA 
                        InfoNumero.Text = Tramite_a_Procesar[i].NumeroOrden;
                        InfoFechaSolicitud.Text = FechaSolicitud.ToString("dd/MM/yyyy");
                        // hfRFC.Value = Tramite_a_Procesar[i].RFCContratante;

                        // MUESTRA FORMULARIO APARTIR DEL TIPO DE CONTRATANTE
                        TipoContratante();
                        if (Tramite_a_Procesar[i].TipoPersona.ToString() == "1")
                        {
                            // DATOS PERSONA FISICA
                            txNombre.Text = Tramite_a_Procesar[i].ContratanteNombre;
                            txApPat.Text = Tramite_a_Procesar[i].ContratanteApPaterno;
                            txApMat.Text = Tramite_a_Procesar[i].ContratanteApMaterno;
                            txSexo.SelectedValue = Tramite_a_Procesar[i].ContratanteSexo;
                            txRfc.Text = Tramite_a_Procesar[i].RFCContratante;
                            DateTime FechaNacimiento = Convert.ToDateTime(Tramite_a_Procesar[i].FNacimientoContratante);
                            dtFechaNacimiento.Text = FechaNacimiento.ToString("dd/MM/yyyy");
                            txNacionalidad.SelectedIndex = Convert.ToInt32(Tramite_a_Procesar[i].Nacionalidad) - 1;
                            //txNacionalidad.SelectedIndex = 136;

                            //TABLA INFORMATIVA 
                            InfoPrsFisica.Visible = true;
                            //InfoContratante.Text = "FÍSICA";
                            InfoFNombre.Text = Tramite_a_Procesar[i].ContratanteNombre;
                            InfoFApellidoP.Text = Tramite_a_Procesar[i].ContratanteApPaterno;
                            InfoFApellidoM.Text = Tramite_a_Procesar[i].ContratanteApMaterno;
                            InfoFSexo.Text = txSexo.SelectedItem.ToString();
                            InfoFRFC.Text = Tramite_a_Procesar[i].RFCContratante;
                            // 220223 InfoFNacionalidad.Text = txNacionalidad.SelectedItem.ToString();
                            InfoFFechaNa.Text = FechaNacimiento.ToString("dd/MM/yyyy");

                        }
                        else if (Tramite_a_Procesar[i].TipoPersona.ToString() == "2")
                        {
                            // DATOS PERSONA MORAL
                            txNomMoral.Text = Tramite_a_Procesar[i].Contratante;
                            DateTime FechaConstitucion = Convert.ToDateTime(Tramite_a_Procesar[i].FechaConst);
                            dtFechaConstitucion.Text = FechaConstitucion.ToString("dd/MM/yyyy");
                            txRfcMoral.Text = Tramite_a_Procesar[i].RFCContratante;

                            //TABLA INFORMATIVA 
                            InfoPrsMoral.Visible = true;
                            //InfoContratante.Text = "MORAL";
                            InfoMNombre.Text = Tramite_a_Procesar[i].Contratante;
                            InfoMFechaConsti.Text = FechaConstitucion.ToString("dd/MM/yyyy");
                            InfoMRFC.Text = Tramite_a_Procesar[i].RFCContratante;

                        }

                        // DATOS DE TITULAR

                        if (Tramite_a_Procesar[i].TitularContratante == "True")
                        {
                            CheckBox1.Checked = true;
                            CheckB1();

                            txTiNombre.Text = Tramite_a_Procesar[i].TitularNombre;
                            txTiApPat.Text = Tramite_a_Procesar[i].TitularApPat;
                            txTiApMat.Text = Tramite_a_Procesar[i].TitularApMat;
                            //txTiNacionalidad.SelectedItem.Text = Tramite_a_Procesar[i].TitularNacionalidad.Trim().ToString().ToUpper();
                            txTiNacionalidad.SelectedIndex = Convert.ToInt32(Tramite_a_Procesar[i].TitularNacionalidad) - 1;
                            txtSexoM.SelectedValue = Tramite_a_Procesar[i].TitularSexo;
                            DateTime FechaNacimiento = Convert.ToDateTime(Tramite_a_Procesar[i].FNacimientoTitular);
                            dtFechaNacimientoTitular.Text = FechaNacimiento.ToString("dd/MM/yyyy");

                            //TABLA INFORMATIVA 
                            InfoDiContratante.Visible = true;
                            InfoFContratante.Text = "NO";
                            InfoTNombre.Text = Tramite_a_Procesar[i].TitularNombre;
                            InfoTApellidoP.Text = Tramite_a_Procesar[i].TitularApPat;
                            InfoTApellidoM.Text = Tramite_a_Procesar[i].TitularApMat;
                            InfoTNacionalidad.Text = txTiNacionalidad.SelectedItem.ToString();
                            InfoTSexo.Text = txtSexoM.SelectedItem.ToString();
                            InfoTNacimiento.Text = FechaNacimiento.ToString("dd/MM/yyyy");
                        }

                        LabelFolio.Text = Tramite_a_Procesar[i].Folio;
                        LabelNumeroPoliza.Text = Tramite_a_Procesar[i].IdSisLegados;
                        LabelProducto.Text = Tramite_a_Procesar[i].Producto;
                        LabelSubProducto.Text = Tramite_a_Procesar[i].SubProducto;


                        if (Tramite_a_Procesar[i].HombreClave == 1)
                        {
                            CheckBoxHombreClave.Checked = true;

                        }
                        else
                        {
                            CheckBoxHombreClave.Checked = false;
                        }

                        txtSumaAseguradaBasica.Text = Tramite_a_Procesar[i].SumaBasica.ToString();
                        TextNumPolizaSisLegado.Text = "";  // Tramite_a_Procesar[i].IdSisLegados.ToString();

                        // CARGA DE PDF
                        CargarPFD(Tramite_a_Procesar[i].IdTramite);

                        // CARGA BITACORA PÚBLICA
                        CargaBitacoraPublica(Tramite_a_Procesar[i].IdTramite);

                        // CARGA BITACORA PRIVADA
                        CargaBitacoraPrivada(Tramite_a_Procesar[i].IdTramite);

                        // CARGA ARCHIVOS EXPEDIENTES
                        CargaExpedientes(Tramite_a_Procesar[i].IdTramite);

                        // CONSULTA REGISTRO DE ASEGURADOS
                        //CargaAsegurados(Tramite_a_Procesar[i].RFCContratante.ToString(), Tramite_a_Procesar[i].ContratanteNombre, Tramite_a_Procesar[i].ContratanteApPaterno, Tramite_a_Procesar[i].ContratanteApMaterno, Convert.ToDateTime(Tramite_a_Procesar[i].FNacimientoContratante));

                        //CargaDatosKWIK(Tramite_a_Procesar[i].IdTramite);

                        // INDICADOR STATUS MESAS
                        CargaIndicadorMesas(Tramite_a_Procesar[i].IdTramite);

                        pintaChecks(pIdMesa, Tramite_a_Procesar[i].IdTramite);



                        if (Tramites.ConsultaSeleccionCompleta(Tramite_a_Procesar[i].IdTramite))
                        {
                            if (Tramite_a_Procesar[i].SeleccionCompleta)
                            {
                                DropDownListSeleccionCompleta.SelectedValue = "1";
                            }
                            else
                            {
                                DropDownListSeleccionCompleta.SelectedValue = "0";
                            }

                            /*
                            btnAceptar.Text = Tramite_a_Procesar[i].SeleccionCompleta;

                            if (string.IsNullOrEmpty(Tramite_a_Procesar[i].SeleccionCompleta))
                            {
                                btnAceptar.Text = Convert.ToInt32(Tramite_a_Procesar[i].SeleccionCompleta.ToString()).ToString();
                                DropDownListSeleccionCompleta.SelectedValue = Convert.ToInt32(Tramite_a_Procesar[i].SeleccionCompleta.ToString()).ToString();
                            }
                            */
                            DropDownListSeleccionCompleta.Attributes["disabled"] = "enabled";
                        }

                    }
                }
            }
            else
            {
                mensajes.MostrarMensaje(this, "No hay trámites disponibles...", "Default.aspx");
            }
        }

        protected void EvaluaCheckDocumentos()
        {
            int IdMesa = Convert.ToInt32(hfIdProceso.Value.ToString());

            // CONSULTA EL TOTAL DE CHECKS PINTADOS EN LA MESA CORRESPONDIENTE 
            List<prop.Cat_CheckBox_Mesa> catalogo = cat_CheckBox.CheckBox_Mesa_Seleccionar_PorIdMesa(IdMesa);

            int TamCatalogo = 0;

            // EN CASO DE NO REQUERIR DOCUMENTOS, CONTINUARA EL PROCESO NORMAL
            if (catalogo.Count > 0)
            {
                // LLENA LA LISTA DE CHECKS SELECIONADOS
                List<ListItem> selected = new List<ListItem>();
                foreach (ListItem item in DocRequerid.Items)
                    if (item.Selected)
                        selected.Add(item);

                // VALIDA LOS DOCUMENTOS QUE SON REQUERIDOS REALIZA BUSQUEDA
                foreach (prop.Cat_CheckBox_Mesa DocumentosRequeridos in catalogo)
                {
                    TamCatalogo += 1;

                    int IdDoumento = 0;
                    int Requerido = 0;
                    string Documento = "";

                    IdDoumento = DocumentosRequeridos.Id;
                    Requerido = DocumentosRequeridos.Requerido;
                    Documento = DocumentosRequeridos.Documentos;

                    if (Requerido == 1)
                    {
                        // SI LA LISTA DE CHECKS NO TIENE NADA
                        if (selected.Count == 0)
                        {
                            string script = "";
                            script = "CheckRequeridoDocumentacion(); ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                        }
                        else
                        {
                            // REALIZAR UNA BUSQUEDA DEL ID DENTRO DE LA LISTADE DOCUMENTOS REQUERIDOS
                            if (selected.Cast<ListItem>().Any(x => x.Value == IdDoumento.ToString()))
                            {
                                if (TamCatalogo == catalogo.Count)
                                {
                                    string script = "";
                                    script = "$('#ContinuarTramite').modal('show');";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                                }
                            }
                            else
                            {
                                string script = "";
                                script = "CheckRequerido('" + Documento + "'); ";
                                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                            }
                        }
                    }
                    else
                    {
                        if (TamCatalogo == catalogo.Count)
                        {
                            string script = "";
                            script = "$('#ContinuarTramite').modal('show');";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                        }
                    }
                }
            }
            else
            {
                string script = "";
                script = "$('#ContinuarTramite').modal('show');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnSeleccionCompleta_Click(object sender, EventArgs e)
        {
            if (ValidaMesaSeleccionCompleta())
            {
                Funciones.VariablesGlobales.StatusMesa IdStatusMesaSolicitudApoyo = Funciones.VariablesGlobales.StatusMesa.SolicitudApoyo;
                int IdTramite = Int32.Parse(hfIdTramite.Value);
                int Chec1 = 0;
                int Chec2 = 0;

                if (CheckBoxRevisioTecnica.Checked.Equals(true))
                {
                    Chec1 = 1;
                }

                if (CheckBoxRevisionMedica.Checked.Equals(true))
                {
                    Chec2 = 1;
                }
                /* SE AGREGAN LAS NUEVAS MESAS*/
                List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultadoSelecion = Tramites.ProcesarTramiteSeleccionCompleta(IdTramite, manejo_sesion.Usuarios.IdUsuario, Chec1, Chec2);
                if (objResultadoSelecion[0].IdTramite > 0)
                {
                    /* PROSESA EL TRAMITE NORMAL*/
                    /*************************************************************************************************************************/


                    List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.ProcesarTramite(IdTramite, int.Parse(hfIdProceso.Value), manejo_sesion.Usuarios.IdUsuario, IdStatusMesaSolicitudApoyo, "", txtObservacionesPrivadas.Text.Trim().Replace("'", ""), "");
                    if (objResultado[0].IdTramite > 0)
                    {
                        int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());
                        // REGISTRO DE ARCHIVOS - COLOCAR DESPUES DE ACTUALIZAR EL TRAMITE
                        if (Session["documentos"] != null) { string resultadoExpediente = registraDocumentosExpediente(IdTramite, TipoTramite); }
                        if (Session["insumos"] != null) { string resultadoInsumo = registraDocumentosInsumos(IdTramite, TipoTramite); }

                        if (objResultado[0].Accion == "KO")
                        {
                            int IdMesa = int.Parse(hfIdProceso.Value.ToString());

                            log.Agregar("Error de aceptación de trámite ID = " + IdTramite + ", en el proceso = " + IdMesa + " , respuesta de store procedure spWFOTramiteProcesar: " + objResultado[0].Accion);
                        }

                        TramiteTerminado();
                        Response.Redirect(EncripParametros("IdProceso=" + hfIdProceso.Value.ToString(), "TramiteTarea.aspx").URL, true);
                        //Response.Redirect("TramiteTarea.aspx?IdProceso=" + hfIdProceso.Value.ToString(), true);
                    }
                }
            }
            else
            {
                string script = "";
                script = "AlertaMotivosSeleccionCompleta();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected bool ValidaMesaSeleccionCompleta()
        {
            bool resultado = false;

            if (CheckBoxRevisioTecnica.Checked == true)
            {
                resultado = true;
            }
            else if (CheckBoxRevisionMedica.Checked == true)
            {
                resultado = true;
            }

            return resultado;
        }

        protected void btnTaskDo_Click(object sender, EventArgs e)
        {
            string NombreMesa = hfNombreProceso.Value;
            prop.PermisosMesaControles permisosMesaControles = new prop.PermisosMesaControles();

            if (txtObservacionesPrivadas.Text.Trim().Length > 3)
            {
                //EvaluaCheckDocumentos();
                string script = "";
                script = "$('#screenTaskOkModal').modal('show');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            else
            {
                string script = "";
                script = "AlertaObservaciones(); ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnTaskOk_Click(object sender, EventArgs e)
        {
            string NombreMesa = hfNombreProceso.Value;
            prop.PermisosMesaControles permisosMesaControles = new prop.PermisosMesaControles();

            if (TextNumPolizaSisLegado.Text.Trim().Length > 3)
            {
                //EvaluaCheckDocumentos();
                string script = "";
                script = "$('#screenTaskOkModal').modal('show');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            else
            {
                string script = "";
                script = "AlertaFolioLlamada(); ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnAceptarValida_Click(object sender, EventArgs e)
        {
            //string NombreMesa = hfNombreMesa.Value;
            //prop.PermisosMesaControles permisosMesaControles = new prop.PermisosMesaControles();

            //switch (NombreMesa)
            //{
            //    case "ADMISIÓN":
            //        if (Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(Convert.ToInt32(hfIdProceso.Value), "PanelSeleccionCompleta")).Activo))
            //        {
            //            if (DropDownListSeleccionCompleta.SelectedValue == "-1")
            //            {
            //                string script = "";
            //                script = "AlertaSeleccionCompleta(); ";
            //                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            //            }
            //            else
            //            {
            //                EvaluaCheckDocumentos();
            //            }
            //        }
            //        else
            //        {
            //            EvaluaCheckDocumentos();
            //        }
            //        break;
            //    case "CALIDAD":
            //        if (txtObservacionesPublicas.Text.Length > 0)
            //        {
            //            EvaluaCheckDocumentos();
            //        }
            //        else
            //        {
            //            string script = "";
            //            script = "AlertaObservacionesPublicas(); ";
            //            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            //        }
            //        break;
            //    case "EJECUCIÓN":
            //        if (TextNumPolizaSisLegado.Text.Trim().Length > 3)
            //        {
            //            EvaluaCheckDocumentos();
            //        }
            //        else
            //        {
            //            string script = "";
            //            script = "AlertaPoliza(); ";
            //            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            //        }
            //        break;
            //    default:
            //        EvaluaCheckDocumentos();
            //        break;
            //}
        }

        #region Captura MASIVA

        protected void BtnCaptura_Click(object sender, EventArgs e)
        {
            PanelCapturaMasiva.Visible = true;
            PanelCapturaMasiva.Enabled = true;

            GuardarCapturaMasiva.Visible = true;
            Captura.Visible = false;
            CancelarCapturaMasiva.Visible = true;
        }

        protected void BtnCancelarCapturaMasiva_Click(object sender, EventArgs e)
        {
            PanelCapturaMasiva.Visible = false;
            PanelCapturaMasiva.Enabled = false;
            GuardarCapturaMasiva.Visible = false;
            Captura.Visible = true;
            CancelarCapturaMasiva.Visible = false;
        }

        protected void BtnCancelarCotizadorSolicitudSimplificadaGMM_Click(object sender, EventArgs e)
        {
            PanelCapturaExamen.Visible = false;
            btnExamenCotizacion.Visible = true;
        }

        protected void BtnMostrarExamenCotizador(object sender, EventArgs e)
        {
            PanelCapturaExamen.Visible = true;
            btnExamenCotizacion.Visible = false;
            PanelCapturaExamen.Enabled = true;
        }

        protected void BtnEvaluarCotizadorSolicitudSimplificadaGMM_Click(object sender, EventArgs e)
        {
            captura.Cotizador cotizador = new captura.Cotizador();
            cotizador.Id = Convert.ToInt32(hfIdTramite.Value);
            cotizador.pregunta1 = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            cotizador.pregunta2 = Convert.ToInt32(DropDownList2.SelectedValue.ToString());
            cotizador.pregunta3 = Convert.ToInt32(cboPregunta3.SelectedValue.ToString());
            cotizador.IdPadecimiento = Convert.ToInt32(ASPxComboBoxPadecimiento.SelectedIndex.ToString());
            cotizador.estatura = Convert.ToDouble(TextBoxAltura.Text.ToString().Trim());
            cotizador.peso = Convert.ToDouble(TextBoxPeso.Text.ToString().Trim());
            cotizador.edad = Convert.ToInt32(TextBoxEdad.Text.ToString().Trim());

            string examen = Funciones.CSSGMM.Cotizador(cotizador);

            if (examen == "ACEPTADO" || examen == "DECLINADO")
            {
                if (examen == "ACEPTADO")
                {
                    cotizador.Excamen = 1;
                }
                else
                {
                    cotizador.Excamen = 0;
                };

                if (asegurados.Asegurados_Cotizador_Actualizar(cotizador) == 1)
                {
                    Label54.Text = "Cotizador Solicitud Simplificada GMM Registrado Asegurado: " + examen;
                    PanelCapturaExamen.Visible = false;
                    LimpiaCapturaCotizador();
                }
                else
                {
                    Label54.Text = "A ocurrido un error en el registro";
                }
            }
            else
            {
                Label54.Text = "A ocurrido un error   un error en su examen, contacte al administrador";
            }
        }

        protected void BtnCapturaMasiva_Click(object sender, EventArgs e)
        {
            ///
            PanelCapturaMasivaMesaCaptura.Visible = true;
            PanelCapturaMasivaMesaCaptura.Enabled = true;
            GuardarDireccion.Visible = true;
            GuardarMesaCapturaMasiva.Visible = true;
            MesaCapturaMasiva.Visible = false;
            CancelarMesaCapturaMasiva.Visible = true;

            //CargaTarjetas();
            //CargaCoasegurados(hfRFC.Value.ToString().Trim());
            //CargaAseguradoCaptura(hfIdTramite.Value.ToString());
        }

        protected void BtnCancelarMesaCapturaMasiva_Click(object sender, EventArgs e)
        {
            GuardarDireccion.Visible = false;
            PanelCapturaMasivaMesaCaptura.Visible = false;
            PanelCapturaMasivaMesaCaptura.Enabled = false;
            GuardarMesaCapturaMasiva.Visible = false;
            MesaCapturaMasiva.Visible = true;
            CancelarMesaCapturaMasiva.Visible = false;
        }

        protected void ccboCatModoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Bancos();
        }

        protected void Carga_Bancos()
        {
            // CONTROLES DE INICIO
            RequiredFieldValidator17.Enabled = true;
            TextToken.Enabled = true;
            TextToken.Attributes.Remove("disabled");
            TextToken.Text = "";
            //TextToken.Enabled = true;


            // TOMA EL VALOR DEL MODO DE PAGO
            int IdModoPago = Convert.ToInt32(cboCatModoPago.SelectedValue.ToString());
            List<captura.cat_bancos> cat_bancos = capturaMasiva.Cat_Bancos(IdModoPago);

            if (cat_bancos.Count > 0)
            {
                // LLENA EL SELECT DE BANCOS APARTIR DEL MODO DE PAGO
                cboCatBancos.DataSource = cat_bancos;
                cboCatBancos.DataBind();
                cboCatBancos.DataTextField = "banco";
                cboCatBancos.DataValueField = "Id";
                cboCatBancos.DataBind();

                // DETERMINA EL LARGO DEL CAMPO DEL TOKEN
                if (cat_bancos[1].LargoToken == 0)
                {
                    TextToken.Text = "";
                    RequiredFieldValidator17.Enabled = false;
                    TextToken.Attributes["disabled"] = "disabled";
                }
                else
                {
                    TextToken.MaxLength = cat_bancos[1].LargoToken;

                }
            }
        }

        protected void CargaCatalogosCaptura()
        {
            List<captura.cat_modo_pago> cat_modo_pago = capturaMasiva.Cat_Modo_Pago();
            cboCatModoPago.DataSource = cat_modo_pago;
            cboCatModoPago.DataBind();
            cboCatModoPago.DataTextField = "modo_pago";
            cboCatModoPago.DataValueField = "Id";
            cboCatModoPago.DataBind();

            List<captura.cat_periodicidad> cat_periodicidad = capturaMasiva.Cat_Periodicidad();
            cboCatPerioricidad.DataSource = cat_periodicidad;
            cboCatPerioricidad.DataBind();
            cboCatPerioricidad.DataTextField = "periodicidad";
            cboCatPerioricidad.DataValueField = "Id";
            cboCatPerioricidad.DataBind();

            List<promotoria.cat_riesgos> cat_Riesgos = catalogos.Cat_Riesgos();
            LisRiesgos.DataSource = cat_Riesgos;
            LisRiesgos.DataBind();
            LisRiesgos.DataTextField = "Riesgo";
            LisRiesgos.DataValueField = "Id";
            LisRiesgos.DataBind();

            List<captura.cat_TipoAsegurados> cat_TipoAsegurados = capturaMasiva.Cat_TipoAsegurados_Seleccionar();
            cboCat_Parentesco.DataSource = cat_TipoAsegurados;
            cboCat_Parentesco.DataBind();
            cboCat_Parentesco.DataTextField = "Interprestacion_larga";
            cboCat_Parentesco.DataValueField = "Id";
            cboCat_Parentesco.DataBind();

            List<captura.cat_padecimiento> cat_Padecimientos = capturaMasiva.Cat_Padecimientos();
            ASPxComboBoxPadecimiento.DataSource = cat_Padecimientos;
            ASPxComboBoxPadecimiento.TextField = "Nombre";
            ASPxComboBoxPadecimiento.ValueField = "Id";
            ASPxComboBoxPadecimiento.DataBind();

            ASPxComboBoxPadecimiento2.DataSource = cat_Padecimientos;
            ASPxComboBoxPadecimiento2.TextField = "Nombre";
            ASPxComboBoxPadecimiento2.ValueField = "Id";
            ASPxComboBoxPadecimiento2.DataBind();
        }

        private void CargaAsegurados(string RFC, string Nombre, string APaterno, string AMaterno, DateTime Fecha)
        {
            captura.Asegurados DatosAsegurados;
            int IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            DatosAsegurados = asegurados.Consulta_Asegurados_PorRFC(RFC, IdTramite);

            //List<captura.cat_catastroficos> cat_Catastroficos;

            //cat_Catastroficos = asegurados.Cat_Catastroficos_Selecionar(Nombre.Trim(), APaterno.Trim(), AMaterno.Trim(), Fecha);

            //if (cat_Catastroficos.Count > 0)
            //{
            //    LabelConsultaCaptura.Text = DatosAsegurados.Respuesta.ToString() + " - Contratante en lista catastrófica";
            //}
            //else
            //{
            //    LabelConsultaCaptura.Text = DatosAsegurados.Respuesta.ToString();
            //}

            // CARGA UMAN Y AGENTE
            CargaUMANAgente(IdTramite);

            // CARGA COASEGURADOS
            CargaCoasegurados(RFC);

            // CARGA REGISTRO DE TARJETAS
            CargaTarjetas();

            // CARGA DIRECCIONES 
            CargaDireciones();
        }

        protected void EliminaTarjeta(object sender, CommandEventArgs e)
        {
            int IdTarjeta = Convert.ToInt32(e.CommandArgument.ToString());
            int IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            int respuesta = tarjetas.Modificar(IdTarjeta, IdTramite);

            if (respuesta == 1)
            {
                CargaTarjetas();
            }
            else
            {
                LabelRespuestaTarjeta.Text = "Error al retirar tarjeta";
            }
        }

        protected void ModificarCoaegurado(object sender, CommandEventArgs e)
        {
            btnCoasegurados.Visible = false;
            ButtonCoasegurados.Visible = false;
            PanelCapturaCoasegurados.Visible = true;
            PanelCapturaCoasegurados.Enabled = true;
            ButtonActualizarCoasegurados.Visible = true;

            int IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            int IdCoasegurado = Convert.ToInt32(e.CommandArgument.ToString());

            //hfIdCoasegurado.Value = IdCoasegurado.ToString();

            captura.Coasegurados coasegurados = coAsegurados.CoAsegurado_Seleccionar_PorID(IdCoasegurado, IdTramite);
            TextCoAseNombre.Text = coasegurados.Nombre;
            TextCoAseApaterno.Text = coasegurados.APaterno;
            TextCoAseMaterno.Text = coasegurados.AMaterno;
            dtCoAsFechaNacimiento.Text = coasegurados.FechaNacimiento.ToShortDateString();
            cboCat_Parentesco.SelectedValue = coasegurados.IdTipoAsegurado.ToString();
            cboCoAsegSexo.SelectedValue = coasegurados.Sexo.ToString();
            TextCoAseEdad.Text = coasegurados.Edad.ToString();
            TextCoAasegPeso.Text = coasegurados.Peso.ToString();
            TextCoAasegAltura.Text = coasegurados.Altura.ToString();

            // EVALUA COASEGURADOS SI REALIZAN O NO REALIZAN EXAMEN DE COTIZACION A PARTIR DEL ASEGURADO TITULAR
            if (asegurados.Asegurados_Selecionar_PorIdTramite(Convert.ToInt32(hfIdTramite.Value)).Examen == 1)
            {
                LabelRespuestaCoasegurados.Text = "Asegurado Aceptado en Cotizador Solicitud Simplificada GMM";
            }
            else
            {
                LabelRespuestaCoasegurados.Text = "Asegurado Declinado en Cotizador Solicitud Simplificada GMM";
            }
        }

        protected void ModificarCoaeguradoMesaCaptura(object sender, CommandEventArgs e)
        {
            btnCoaseguradosMesaCaptura.Visible = false;
            ButtonCoaseguradosMesaCaptura.Visible = false;
            PanelCapturaCoaseguradoMesaCaptura.Visible = true;
            PanelCapturaCoaseguradoMesaCaptura.Enabled = true;
            ButtonActualizarCoaseguradosMesaCaptura.Visible = true;

            int IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            int IdCoasegurado = Convert.ToInt32(e.CommandArgument.ToString());

            //hfIdCoasegurado.Value = IdCoasegurado.ToString();

            captura.Coasegurados coasegurados = coAsegurados.CoAsegurado_Seleccionar_PorID(IdCoasegurado, IdTramite);
            TextCoAseNombreMesaCaptura.Text = coasegurados.Nombre;
            TextCoAseApaternoMesaCaptura.Text = coasegurados.APaterno;
            TextCoAseMaternoMesaCaptura.Text = coasegurados.AMaterno;
            dtCoAsFechaNacimientoMesaCaptura.Text = coasegurados.FechaNacimiento.ToShortDateString();
            cboCat_ParentescoMesaCaptura.SelectedValue = coasegurados.IdTipoAsegurado.ToString();
            cboCoAsegSexoMesaCaptura.SelectedValue = coasegurados.Sexo.ToString();


            // EVALUA COASEGURADOS SI REALIZAN O NO REALIZAN EXAMEN DE COTIZACION A PARTIR DEL ASEGURADO TITULAR
            if (asegurados.Asegurados_Selecionar_PorIdTramite(Convert.ToInt32(hfIdTramite.Value)).Examen == 1)
            {
                LabelRespuestaCoaseguradosMesaCaptura.Text = "Asegurado Aceptado en Cotizador Solicitud Simplificada GMM";
            }
            else
            {
                LabelRespuestaCoaseguradosMesaCaptura.Text = "Asegurado Declinado en Cotizador Solicitud Simplificada GMM";
            }
        }

        protected void BtnSumaUMAN_click(object sender, EventArgs e)
        {
            LabelUMAN.Visible = false;
            LabelUMANAcptable.Visible = false;
            LabelUMAN.Text = "";
            LabelUMANAcptable.Text = "";
            LabelUMANANoencontrado.Text = "";
            LabelUMANANoencontrado.Visible = false;
            PanelCapturaExamen.Visible = false;
            PanelCapturaExamen.Enabled = false;
            btnExamenCotizacion.Visible = false;
            Label54.Text = "";
            string RFC = "";  //hfRFC.Value.ToString().Trim()
            int IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            int UMAN = asegurados.GM_UMAN_Seleccionar_PorRFC(RFC, IdTramite).SA_Total;
            int SumaUMAN = Convert.ToInt32(txtSumaUMAN.Text.ToString());

            if (UMAN == 0)
            {
                if (asegurados.Asegurados_CotizacionExamen_Actualizar(Convert.ToInt32(hfIdTramite.Value), 0) == 1)
                {
                    LabelUMANANoencontrado.Text = "Asegurado nuevo, no encontrado en base metlife";
                    LabelUMANANoencontrado.Visible = true;
                    btnExamenCotizacion.Visible = true;
                }
            }
            else
            {
                if (SumaUMAN <= UMAN)
                {
                    if (asegurados.Asegurados_CotizacionExamen_Actualizar(Convert.ToInt32(hfIdTramite.Value), 1) == 1)
                    {
                        LabelUMANAcptable.Text = "Suma UMAN en base registrada: " + UMAN;
                        LabelUMANAcptable.Visible = true;
                        btnExamenCotizacion.Visible = true;
                    }
                }
                else
                {
                    if (asegurados.Asegurados_CotizacionExamen_Actualizar(Convert.ToInt32(hfIdTramite.Value), 0) == 1)
                    {
                        LabelUMAN.Text = "Suma UMAN es mayor a la registrada " + UMAN;
                        LabelUMAN.Visible = true;

                        PanelCapturaExamen.Visible = true;
                        PanelCapturaExamen.Enabled = true;
                    }
                }
            }
            //CargaCoasegurados(RFC);
        }

        protected void BtnAgente_click(object sender, EventArgs e)
        {
            LabelAgente.Text = "";
            cboFiltroAgente.Visible = false;
            LabelFiltroAgente.Visible = false;
            string clave = txtAgente.Text.ToString().Trim();

            List<captura.AgentesDXN> agentesDXNs = agentesDXN.AgentesDXN_Selecionar_PorClave(clave);
            if (agentesDXNs.Count > 1)
            {
                if (agentesDXNs.Count > 2)
                {
                    cboFiltroAgente.Visible = true;
                    LabelFiltroAgente.Visible = true;

                    cboFiltroAgente.DataSource = agentesDXNs;
                    cboFiltroAgente.DataBind();
                    cboFiltroAgente.DataTextField = "Nomenclatura";
                    cboFiltroAgente.DataValueField = "Clave_Agente_Promotor_Ind_Prv";
                    cboFiltroAgente.DataBind();

                    LabelAgente.Text = "contiene mas de una clave";
                }
                else
                {
                    LabelAgente.Text = agentesDXNs[1].Clave_Agente_Promotor_Ind_Prv;
                }
            }
            else
            {
                LabelAgente.Text = "Clave de Agente no encontrada";
            }
        }

        protected void cboFiltroAgente_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelAgente.Text = "";
            LabelAgente.Text = cboFiltroAgente.SelectedValue.ToString();
        }

        protected void cboPregunta3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPregunta3.SelectedValue == "1")
            {
                LabelPadecimiento.Visible = true;
                ASPxComboBoxPadecimiento.Visible = true;
            }
            else
            {
                LabelPadecimiento.Visible = false;
                ASPxComboBoxPadecimiento.Visible = false;
            }
        }

        protected void cboPregunta4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList5.SelectedValue == "1")
            {
                Label41.Visible = true;
                ASPxComboBoxPadecimiento2.Visible = true;
            }
            else
            {
                Label41.Visible = false;
                ASPxComboBoxPadecimiento2.Visible = false;
            }
        }

        protected void NuevoCoasegurado(object sender, EventArgs e)
        {
            btnCoasegurados.Visible = false;
            PanelCapturaCoasegurados.Visible = true;
            PanelCapturaCoasegurados.Enabled = true;
            LabelRespuestaCoasegurados.Text = "";
            ButtonActualizarCoasegurados.Visible = false;
            ButtonCoasegurados.Visible = true;
            LimpiaCaptura();

            // EVALUA COASEGURADOS SI REALIZAN O NO REALIZAN EXAMEN DE COTIZACION A PARTIR DEL ASEGURADO TITULAR
            if (asegurados.Asegurados_Selecionar_PorIdTramite(Convert.ToInt32(hfIdTramite.Value)).Examen == 1)
            {
                LabelRespuestaCoasegurados.Text = "Asegurado Aceptado en Cotizador Solicitud Simplificada GMM";
            }
            else
            {
                LabelRespuestaCoasegurados.Text = "Asegurado Declinado en Cotizador Solicitud Simplificada GMM";
            }
        }

        protected void NuevoCoaseguradoMesaCaptura(object sender, EventArgs e)
        {
            btnCoaseguradosMesaCaptura.Visible = false;
            PanelCapturaCoaseguradoMesaCaptura.Visible = true;
            LabelRespuestaCoaseguradosMesaCaptura.Text = "";
            ButtonActualizarCoaseguradosMesaCaptura.Visible = false;
            ButtonCoaseguradosMesaCaptura.Visible = true;

        }

        protected void BtnCancelarCoasegurado_Click(object sender, EventArgs e)
        {
            PanelCapturaCoasegurados.Visible = false;
            btnCoasegurados.Visible = true;
        }

        protected void BtnCancelarCoaseguradoMesaCaptura_Click(object sender, EventArgs e)
        {
            PanelCapturaCoaseguradoMesaCaptura.Visible = false;
            btnCoaseguradosMesaCaptura.Visible = true;
        }

        protected void BtnActualizarCoasegurado(object sender, EventArgs e)
        {
            LabelRespuestaCoasegurados.Text = "";

            captura.Cotizador cotizador = new captura.Cotizador();
            cotizador.pregunta1 = Convert.ToInt32(DropDownList3.SelectedValue.ToString());
            cotizador.pregunta2 = Convert.ToInt32(DropDownList4.SelectedValue.ToString());
            cotizador.pregunta3 = Convert.ToInt32(DropDownList5.SelectedValue.ToString());
            cotizador.IdPadecimiento = Convert.ToInt32(ASPxComboBoxPadecimiento2.SelectedIndex.ToString());
            cotizador.estatura = Convert.ToDouble(TextCoAasegAltura.Text.ToString().Trim());
            cotizador.peso = Convert.ToDouble(TextCoAasegPeso.Text.ToString().Trim());
            cotizador.edad = Convert.ToInt32(TextCoAseEdad.Text.ToString().Trim());

            string examen = Funciones.CSSGMM.Cotizador(cotizador);

            if (examen == "ACEPTADO" || examen == "DECLINADO")
            {
                if (examen == "ACEPTADO")
                {
                    cotizador.Excamen = 1;
                }
                else
                {
                    cotizador.Excamen = 0;
                };

                captura.Coasegurados coasegurados = new captura.Coasegurados();
                //coasegurados.RFC = hfRFC.Value.ToString().Trim();
                //coasegurados.Id = Convert.ToInt32(hfIdCoasegurado.Value);
                coasegurados.Nombre = TextCoAseNombre.Text.Trim();
                coasegurados.APaterno = TextCoAseApaterno.Text.Trim();
                coasegurados.AMaterno = TextCoAseMaterno.Text.Trim();
                coasegurados.FechaNacimiento = Convert.ToDateTime(dtCoAsFechaNacimiento.Text);
                coasegurados.IdTipoAsegurado = Convert.ToInt32(cboCat_Parentesco.SelectedValue.Trim());
                coasegurados.Sexo = cboCoAsegSexo.SelectedValue.ToString();
                coasegurados.Edad = TextCoAseEdad.Text.ToString();
                coasegurados.Peso = TextCoAasegPeso.Text.ToString();
                coasegurados.Altura = TextCoAasegAltura.Text.ToString();
                coasegurados.Examen = cotizador.Excamen;
                coasegurados.IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());

                if (coAsegurados.ActualizarCoasegurado(coasegurados) == 1)
                {
                    LabelRespuestaCoasegurados.Text = "CoAsegurado actualizado";
                    LimpiaCaptura();

                    PanelCapturaCoasegurados.Visible = false;
                    btnCoasegurados.Visible = true;

                    CargaCoasegurados(coasegurados.RFC);
                }
                else
                {
                    LabelRespuestaCoasegurados.Text = "Error de Registro" + coasegurados.Id;
                }
            }
            else
            {
                LabelRespuestaCoasegurados.Text = "A ocurrido un error   un error en su examen, contacte al administrador";
            }
        }

        protected void BtnActualizarCoaseguradoMesaCaptura(object sender, EventArgs e)
        {
            LabelRespuestaCoasegurados.Text = "";

            captura.Coasegurados coasegurados = new captura.Coasegurados();
            //coasegurados.RFC = hfRFC.Value.ToString().Trim();
            //coasegurados.Id = Convert.ToInt32(hfIdCoasegurado.Value);
            coasegurados.Nombre = TextCoAseNombreMesaCaptura.Text.Trim();
            coasegurados.APaterno = TextCoAseApaternoMesaCaptura.Text.Trim();
            coasegurados.AMaterno = TextCoAseMaternoMesaCaptura.Text.Trim();
            coasegurados.FechaNacimiento = Convert.ToDateTime(dtCoAsFechaNacimientoMesaCaptura.Text);
            coasegurados.IdTipoAsegurado = Convert.ToInt32(cboCat_ParentescoMesaCaptura.SelectedValue.Trim());
            coasegurados.Sexo = cboCoAsegSexoMesaCaptura.SelectedValue.ToString();

            if (coAsegurados.ActualizarCoaseguradoMesaCaptura(coasegurados) == 1)
            {
                LabelRespuestaCoaseguradosMesaCaptura.Text = "CoAsegurado actualizado";
                LimpiaMesaCaptura(); ;

                PanelCapturaCoaseguradoMesaCaptura.Visible = false;
                btnCoaseguradosMesaCaptura.Visible = true;

                CargaCoasegurados(coasegurados.RFC);
            }
            else
            {
                LabelRespuestaCoaseguradosMesaCaptura.Text = "Error de Registro" + coasegurados.Id;
            }
        }

        protected void BtnRegistrarCoasegurado(object sender, EventArgs e)
        {
            LabelRespuestaCoasegurados.Text = "";

            captura.Cotizador cotizador = new captura.Cotizador();
            cotizador.pregunta1 = Convert.ToInt32(DropDownList3.SelectedValue.ToString());
            cotizador.pregunta2 = Convert.ToInt32(DropDownList4.SelectedValue.ToString());
            cotizador.pregunta3 = Convert.ToInt32(DropDownList5.SelectedValue.ToString());
            cotizador.IdPadecimiento = Convert.ToInt32(ASPxComboBoxPadecimiento2.SelectedIndex.ToString());
            cotizador.estatura = Convert.ToDouble(TextCoAasegAltura.Text.ToString().Trim());
            cotizador.peso = Convert.ToDouble(TextCoAasegPeso.Text.ToString().Trim());
            cotizador.edad = Convert.ToInt32(TextCoAseEdad.Text.ToString().Trim());

            string examen = Funciones.CSSGMM.Cotizador(cotizador);

            if (examen == "ACEPTADO" || examen == "DECLINADO")
            {
                if (examen == "ACEPTADO")
                {
                    cotizador.Excamen = 1;
                }
                else
                {
                    cotizador.Excamen = 0;
                };

                captura.Coasegurados coasegurados = new captura.Coasegurados();
                //coasegurados.RFC = hfRFC.Value.ToString().Trim();
                coasegurados.Nombre = TextCoAseNombre.Text.Trim();
                coasegurados.APaterno = TextCoAseApaterno.Text.Trim();
                coasegurados.AMaterno = TextCoAseMaterno.Text.Trim();
                coasegurados.FechaNacimiento = Convert.ToDateTime(dtCoAsFechaNacimiento.Text);
                coasegurados.IdTipoAsegurado = Convert.ToInt32(cboCat_Parentesco.SelectedValue.Trim());
                coasegurados.Sexo = cboCoAsegSexo.SelectedValue.ToString();
                coasegurados.Edad = TextCoAseEdad.Text.ToString();
                coasegurados.Peso = TextCoAasegPeso.Text.ToString();
                coasegurados.Altura = TextCoAasegAltura.Text.ToString();
                coasegurados.Examen = cotizador.Excamen;
                coasegurados.IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());

                if (coAsegurados.AgregarCoasegurado(coasegurados) == 1)
                {
                    LabelRespuestaCoasegurados.Text = "CoAsegurado Registrado";
                    LimpiaCaptura();

                    CargaCoasegurados(coasegurados.RFC);
                }
                else
                {
                    LabelRespuestaCoasegurados.Text = "Error de Registro";
                }
            }
            else
            {
                LabelRespuestaCoasegurados.Text = "A ocurrido un error   un error en su examen, contacte al administrador";
            }
        }


        protected void BtnRegistrarCoaseguradoMesaCaptura(object sender, EventArgs e)
        {
            LabelRespuestaCoaseguradosMesaCaptura.Text = "";

            captura.Coasegurados coasegurados = new captura.Coasegurados();
            //coasegurados.RFC = hfRFC.Value.ToString().Trim();
            coasegurados.Nombre = TextCoAseNombreMesaCaptura.Text.Trim();
            coasegurados.APaterno = TextCoAseApaternoMesaCaptura.Text.Trim();
            coasegurados.AMaterno = TextCoAseMaternoMesaCaptura.Text.Trim();
            coasegurados.FechaNacimiento = Convert.ToDateTime(dtCoAsFechaNacimientoMesaCaptura.Text);
            coasegurados.IdTipoAsegurado = Convert.ToInt32(cboCat_ParentescoMesaCaptura.SelectedValue.Trim());
            coasegurados.Sexo = cboCoAsegSexoMesaCaptura.SelectedValue.ToString();
            coasegurados.Edad = "0";
            coasegurados.Peso = "0";
            coasegurados.Altura = "0";
            coasegurados.Examen = 0;
            coasegurados.IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());

            if (coAsegurados.AgregarCoasegurado(coasegurados) == 1)
            {
                LabelRespuestaCoaseguradosMesaCaptura.Text = "CoAsegurado Registrado";
                LimpiaMesaCaptura();

                CargaCoasegurados(coasegurados.RFC);
            }
            else
            {
                LabelRespuestaCoaseguradosMesaCaptura.Text = "Error de Registro";
            }

        }

        protected void EliminaCoasegurado(object sender, CommandEventArgs e)
        {
            int IdCoasegurado = Convert.ToInt32(e.CommandArgument.ToString());
            int IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());

            int respuesta = coAsegurados.ModificarCoasegurado(IdCoasegurado, IdTramite);

            if (respuesta == 1)
            {
                //CargaCoasegurados(hfRFC.Value.ToString().Trim());
            }
            else
            {
                LabelRespuestaCoasegurados.Text = "Error al retirar CoAsegurado";
            }
        }

        protected void BtnRegistrarTarjeta_click(object sender, EventArgs e)
        {
            LabelRespuestaTarjeta.Text = "";

            captura.Tarjetas tarjeta = new captura.Tarjetas();
            tarjeta.Id_tramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            tarjeta.Id_banco = Convert.ToInt32(cboCatBancos.SelectedValue.ToString());
            tarjeta.Id_modo_pago = Convert.ToInt32(cboCatModoPago.SelectedValue.ToString());
            tarjeta.Id_periodicidad = Convert.ToInt32(cboCatPerioricidad.SelectedValue.ToString());
            tarjeta.Token = TextToken.Text.ToString().Trim();

            int respuesta = tarjetas.Agregar(tarjeta);

            if (respuesta == 1)
            {
                LabelRespuestaTarjeta.Text = "Tarjeta agregada";
                cboCatPerioricidad.SelectedValue = "-1";
                cboCatModoPago.SelectedValue = "-1";
                cboCatBancos.SelectedValue = "-1";
                TextToken.Text = "";
            }
            else
            {
                LabelRespuestaTarjeta.Text = "Error al realizar el registro";
            }


            CargaTarjetas();
        }

        protected void BtnNuevaTarjeta_click(object sender, EventArgs e)
        {
            ButtonNuevaTarjeta.Visible = false;
            PanelTarjeta.Visible = true;
            PanelTarjeta.Enabled = true;
        }

        protected void CargaCoasegurados(string RFC)
        {
            int IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            List<captura.Coasegurados> coasegurados = coAsegurados.CoAsegurados_Selecionar_PorIdTramite_RFC(IdTramite, RFC);
            RepeterCoasegurados.DataSource = coasegurados;
            RepeterCoasegurados.DataBind();

            RepeterCoaseguradosMesaCaptura.DataSource = coasegurados;
            RepeterCoaseguradosMesaCaptura.DataBind();
        }

        protected void CargaTarjetas()
        {
            // CARGA REGISTRO DE TARJETAS
            int IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            List<captura.Tarjetas> TotalTarjetas = tarjetas.Tarjetas_Selecionar_PorIdTramite(IdTramite);
            RepeterTarjetas.DataSource = TotalTarjetas;
            RepeterTarjetas.DataBind();

            RepeterTarjetasMesaCaptura.DataSource = TotalTarjetas;
            RepeterTarjetasMesaCaptura.DataBind();
        }

        protected void CargaDireciones()
        {
            int IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            List<captura.Asegurado_direciones> Direcions = asegurado_Direciones.Asegurado_Direcion_Selecionar_PorIdTramite(IdTramite);
            RepeaterDireciones.DataSource = Direcions;
            RepeaterDireciones.DataBind();
        }

        protected void LimpiaCaptura()
        {
            TextCoAseNombre.Text = "";
            TextCoAseApaterno.Text = "";
            TextCoAseMaterno.Text = "";
            dtCoAsFechaNacimiento.Date = DateTime.Today;
            cboCat_Parentesco.SelectedValue = "-1";
            cboCoAsegSexo.SelectedValue = "0";
            TextCoAseEdad.Text = "";
            TextCoAasegPeso.Text = "";
            TextCoAasegAltura.Text = "";
            DropDownList3.SelectedValue = "-1";
            DropDownList4.SelectedValue = "-1";
            DropDownList5.SelectedValue = "-1";
            ASPxComboBoxPadecimiento2.SelectedIndex = 0;
        }

        protected void LimpiaMesaCaptura()
        {
            TextCoAseNombreMesaCaptura.Text = "";
            TextCoAseApaternoMesaCaptura.Text = "";
            TextCoAseMaternoMesaCaptura.Text = "";
            dtCoAsFechaNacimientoMesaCaptura.Date = DateTime.Today;
            cboCat_ParentescoMesaCaptura.SelectedValue = "-1";
            cboCoAsegSexoMesaCaptura.SelectedValue = "0";
        }


        protected void CargaUMANAgente(int IdTramite)
        {
            captura.Asegurados AseguradoDatos = asegurados.Asegurados_Selecionar_PorIdTramite(IdTramite);
            if (AseguradoDatos.Id > 0)
            {
                LabelUMANtxt.Text = AseguradoDatos.UMAN.ToString();
                LabelAgentetxt.Text = AseguradoDatos.Clave_agente.ToString();
                LabelLugarFirma.Text = AseguradoDatos.EstadoFirma.ToString();
                LabelFechaFirmaSolicitudtxt.Text = AseguradoDatos.FechaFirmaSolicitud.ToString();

                if (AseguradoDatos.Examen == 1)
                {
                    LabelCotizaciontxt.Text = "ACEPTADO";
                }
                else
                {
                    LabelCotizaciontxt.Text = "DECLINADO";
                }
            }
        }

        protected void LimpiaCapturaCotizador()
        {
            DropDownList1.SelectedValue = "-1";
            DropDownList2.SelectedValue = "-1";
            cboPregunta3.SelectedValue = "-1";
            ASPxComboBoxPadecimiento.SelectedIndex = 0;
            TextBoxAltura.Text = "";
            TextBoxPeso.Text = "";
            TextBoxEdad.Text = "";
        }

        protected void BtnGuardarCaptura_click(object sender, EventArgs e)
        {
            Label14.Visible = true;
            Label14.Text = "";
            string UMAN = txtSumaUMAN.Text.Trim();
            string FechaFirmaSolicitud = cpoFechaFirmaSolicitud.Text;
            string agente = "";
            string clave = txtAgente.Text.ToString().Trim();

            if (cboCatEstados.Value == null)
            {
                Label14.Text = "Selecione Lugar de firma";
            }
            else
            {
                if (UMAN.Length > 0)
                {
                    int IdEstado = Convert.ToInt32(cboCatEstados.Value.ToString());
                    List<captura.AgentesDXN> agentesDXNs = agentesDXN.AgentesDXN_Selecionar_PorClave(clave);
                    if (agentesDXNs.Count > 1)
                    {
                        if (agentesDXNs.Count > 2)
                        {
                            agente = cboFiltroAgente.SelectedValue;
                            RegistraCaptura(agente, UMAN, FechaFirmaSolicitud, IdEstado);
                        }
                        else
                        {
                            agente = agentesDXNs[1].Clave_Agente_Promotor_Ind_Prv;
                            RegistraCaptura(agente, UMAN, FechaFirmaSolicitud, IdEstado);
                        }
                    }
                    else
                    {
                        Label14.Text = "Clave de Agente no encontrada";
                    }
                }
                else
                {
                    Label14.Text = "UMAN no colocado";
                }
            }
        }

        protected void RegistraCaptura(string agente, string UMAN, string FechaFirmaSolicitud, int IdEstado)
        {
            int IdTramite = Convert.ToInt32(hfIdTramite.Value);
            string strRFC = "";
            ////strRFC = hfRFC.value();
            if (asegurados.Asegurados_Actualiza_UMAN_Agente(IdTramite, strRFC, agente, UMAN, FechaFirmaSolicitud, IdEstado) == 1)
            {
                txtSumaUMAN.Text = "";
                txtAgente.Text = "";
                LabelUMAN.Text = "";
                LabelAgente.Text = "";
                Label54.Text = "";
                PanelCapturaCoasegurados.Visible = false;
                LabelRespuestaCoasegurados.Text = "";
                PanelTarjeta.Visible = false;
                LabelRespuestaTarjeta.Text = "";
                PanelCapturaMasiva.Visible = false;
                PanelCapturaMasiva.Enabled = false;
                GuardarCapturaMasiva.Visible = false;
                Captura.Visible = true;
                CancelarCapturaMasiva.Visible = false;
                Label14.Visible = true;
                Label42.Text = "Captura Registrada ";

            }
            else
            {
                Label14.Text = "Error en la captura";
            }
        }

        // DIRECCIONES MESA DE CAPTURA
        protected void BtnBuscaCP_click(object sender, EventArgs e)
        {
            LabelRespuestaCP.Text = "";
            captura.cat_direcciones Cat_CP = capturaMasiva.Cat_CP_Selecionar_PorCP(TextBoxBCP.Text.ToString().Trim());
            if (Cat_CP.IdColonia > 0)
            {
                cboCatDireccionesEstados.SelectedItem = cboCatDireccionesEstados.Items.FindByValue(Cat_CP.Estado.ToString());
                cboCatDireccionesEstados_SelectedIndexChanged(null, null);
                cboCatDireccionesPoblacion.SelectedItem = cboCatDireccionesPoblacion.Items.FindByValue(Cat_CP.Poblacion.ToString());
                cboCatDireccionesPoblacion_SelectedIndexChanged(null, null);
                cboCatDireccionesCP.SelectedItem = cboCatDireccionesCP.Items.FindByValue(Cat_CP.CP.ToString());
                cboCatDireccionesCP_SelectedIndexChanged(null, null);
            }
            else
            {
                LabelRespuestaCP.Text = "CP No Encontrado";
            }
        }

        protected void CargaCatalogosCapturaMasiva()
        {
            cboCatDireccionesEstados.Text = "";
            List<captura.cat_estados> cat_Direcciones = capturaMasiva.Cat_Direcciones_Estados();
            cboCatDireccionesEstados.DataSource = cat_Direcciones;
            cboCatDireccionesEstados.TextField = "Estado";
            cboCatDireccionesEstados.ValueField = "Id";
            cboCatDireccionesEstados.DataBind();

            cboCatEstados.DataSource = cat_Direcciones;
            cboCatEstados.TextField = "Estado";
            cboCatEstados.ValueField = "Id";
            cboCatEstados.DataBind();


            List<captura.cat_PlanMedicalife> CatPlanMedicalife = capturaMasiva.Cat_PlanMedicalife_Seleccionar();
            cbCatPlanMedicalife.DataSource = CatPlanMedicalife;
            cbCatPlanMedicalife.DataBind();
            cbCatPlanMedicalife.DataTextField = "Plan";
            cbCatPlanMedicalife.DataValueField = "Id";
            cbCatPlanMedicalife.DataBind();

            List<captura.cat_deducible> cat_Deducibles = capturaMasiva.Cat_Deducible_Seleccionar();
            cbCatDeduciblePesos.DataSource = cat_Deducibles;
            cbCatDeduciblePesos.DataBind();
            cbCatDeduciblePesos.DataTextField = "deducible";
            cbCatDeduciblePesos.DataValueField = "Id";
            cbCatDeduciblePesos.DataBind();

            //List<captura.cat_causa_seguro> cat_Causa_Seguros  = capturaMasiva.Cat_Causa_Seguro_Seleccionar();
            //cbCatCausaSeguro.DataSource = cat_Causa_Seguros;
            //cbCatCausaSeguro.DataBind();
            //cbCatCausaSeguro.DataTextField = "causa_seguro";
            //cbCatCausaSeguro.DataValueField = "Id";
            //cbCatCausaSeguro.DataBind();

            List<captura.cat_regiones> cat_Regiones = capturaMasiva.Cat_Region_Seleccionar();
            cbCatRegion.DataSource = cat_Regiones;
            cbCatRegion.DataBind();
            cbCatRegion.DataTextField = "region";
            cbCatRegion.DataValueField = "Id";
            cbCatRegion.DataBind();

            List<captura.cat_tipo_producto> cat_Tipo_Productos = capturaMasiva.Cat_Tipo_Producto_Seleccionar();
            cbCatTipoProducto.DataSource = cat_Tipo_Productos;
            cbCatTipoProducto.DataBind();
            cbCatTipoProducto.DataTextField = "tipo_producto";
            cbCatTipoProducto.DataValueField = "Id";
            cbCatTipoProducto.DataBind();

            List<captura.cat_TipoAsegurados> cat_TipoAsegurados = capturaMasiva.Cat_TipoAsegurados_Seleccionar();
            cboCat_ParentescoMesaCaptura.DataSource = cat_TipoAsegurados;
            cboCat_ParentescoMesaCaptura.DataBind();
            cboCat_ParentescoMesaCaptura.DataTextField = "Interprestacion_larga";
            cboCat_ParentescoMesaCaptura.DataValueField = "Id";
            cboCat_ParentescoMesaCaptura.DataBind();
        }

        protected void cboCatDireccionesEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCatDireccionesPoblacion.Text = "";
            int estado = Convert.ToInt32(cboCatDireccionesEstados.Value.ToString());
            List<captura.cat_poblaciones> cat_Direcciones_Poblacion = capturaMasiva.Cat_Direcciones_Poblacion(estado);
            cboCatDireccionesPoblacion.DataSource = cat_Direcciones_Poblacion;
            cboCatDireccionesPoblacion.TextField = "Poblacion";
            cboCatDireccionesPoblacion.ValueField = "Id";
            cboCatDireccionesPoblacion.DataBind();
        }

        protected void cboCatDireccionesPoblacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCatDireccionesCP.Text = "";
            int Poblacion = Convert.ToInt32(cboCatDireccionesPoblacion.Value.ToString());
            List<captura.cat_cp> cat_Direcciones_CP = capturaMasiva.Cat_Direcciones_CP_Selecionar_PorIdPoblacion(Poblacion);
            cboCatDireccionesCP.DataSource = cat_Direcciones_CP;
            cboCatDireccionesCP.TextField = "CP";
            cboCatDireccionesCP.ValueField = "Id";
            cboCatDireccionesCP.DataBind();
        }

        protected void cboCatDireccionesCP_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCatDireccionesColonia.Text = "";
            int CP = Convert.ToInt32(cboCatDireccionesCP.Value.ToString());
            List<captura.cat_colonia> cat_Colonias = capturaMasiva.Cat_Direcion_Colonia_PorIdCP(CP);
            cboCatDireccionesColonia.DataSource = cat_Colonias;
            cboCatDireccionesColonia.TextField = "Colonia";
            cboCatDireccionesColonia.ValueField = "Id";
            cboCatDireccionesColonia.DataBind();
        }

        protected void BtnGuardarDirecion_Click(object sender, EventArgs e)
        {
            int IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            Label21.Text = "";
            captura.Asegurado_direciones direcion = new captura.Asegurado_direciones();

            if (cboCatDireccionesEstados.SelectedIndex.ToString() == "-1" || cboCatDireccionesPoblacion.SelectedIndex.ToString() == "-1" || cboCatDireccionesCP.SelectedIndex.ToString() == "-1" || cboCatDireccionesColonia.SelectedIndex.ToString() == "-1" || TextBox4.Text == "")
            {
                Label21.Text = "Completa el formulario";
            }
            else
            {
                direcion.IdTramite = IdTramite;
                direcion.IdEstado = Convert.ToInt32(cboCatDireccionesEstados.Value.ToString());
                direcion.IdPoblacion = Convert.ToInt32(cboCatDireccionesPoblacion.Value.ToString());
                direcion.IdCP = Convert.ToInt32(cboCatDireccionesCP.Value.ToString());
                direcion.IdColonia = Convert.ToInt32(cboCatDireccionesColonia.Value.ToString());
                direcion.Direccion = TextBox4.Text.ToString().Trim();

                if (asegurado_Direciones.Asegurado_Agregar_Direccion(direcion) == 1)
                {
                    Label21.Text = "Captura registrada";
                    CargaDireciones();
                    TextBoxBCP.Text = "";
                    cboCatDireccionesEstados.Value = "";
                    cboCatDireccionesPoblacion.Value = "";
                    cboCatDireccionesCP.Value = "";
                    cboCatDireccionesColonia.Value = "";
                    TextBox4.Text = "";
                }
                else
                {
                    Label21.Text = "Ocurrio un error en el registro";
                }
            }
        }

        protected void CargaAseguradoCaptura(string IdTramite)
        {
            int Id = Convert.ToInt32(IdTramite);
            prop.Captura.AseguradoCaptura aseguradoCaptura = asegurados.Asegurado_Captura_Seleccionar(Id);


            if (aseguradoCaptura.Id > 0)
            {
                prop.Captura.Asegurados Asegurado = asegurados.Asegurados_Selecionar_PorIdTramite(Id);
                TextBoxAseguradoTelefono.Text = Asegurado.Telefono;
                TextBoxAseguradoEmail.Text = Asegurado.Email;

                if (Asegurado.Antiguedad.Length > 0)
                {
                    DateTime FechaAn = Convert.ToDateTime(Asegurado.Antiguedad);
                    FechaAntiguedad.Text = FechaAn.ToShortDateString();
                }


                cbCatPlanMedicalife.SelectedValue = aseguradoCaptura.IdPlan.ToString();
                cbCatDeduciblePesos.SelectedValue = aseguradoCaptura.IdDeducible.ToString();
                cbCatCausaSeguro.SelectedValue = aseguradoCaptura.IdCausaSeguro.ToString();
                cbCatRegion.SelectedValue = aseguradoCaptura.IdRegiones.ToString();
                cbCatTipoProducto.SelectedValue = aseguradoCaptura.IdTipoProducto.ToString();
            }
        }

        protected void BtnGuardarDatosAsegurado_Click(object sender, EventArgs e)
        {
            int IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            string Telefono = TextBoxAseguradoTelefono.Text.Trim().ToString();
            string Email = TextBoxAseguradoEmail.Text.Trim().ToString();
            DateTime Fecha = Convert.ToDateTime(FechaAntiguedad.Text);

            int IdPlan = Convert.ToInt32(cbCatPlanMedicalife.SelectedValue.ToString());
            int IdDeducible = Convert.ToInt32(cbCatDeduciblePesos.SelectedValue.ToString());
            int IdCausaSeguro = Convert.ToInt32(cbCatCausaSeguro.SelectedValue.ToString());
            int IdRegiones = Convert.ToInt32(cbCatRegion.SelectedValue.ToString());
            int IdTipoProducto = Convert.ToInt32(cbCatTipoProducto.SelectedValue.ToString());


            string strRFC = "";
            //strRFC = hfRFC.Value.ToString().Trim();
            if (asegurados.Asegurados_Actualizar_Datos(IdTramite, strRFC, Telefono, Email, Fecha) == 1)
            {

                prop.Captura.AseguradoCaptura aseguradoCaptura = asegurados.Asegurado_Captura_Seleccionar(IdTramite);
                if (aseguradoCaptura.Id > 0)
                {
                    if (asegurados.Asegurado_Captura_Actualizar(IdTramite, IdPlan, IdDeducible, IdCausaSeguro, IdTipoProducto, IdRegiones) == 1)
                    {
                        BtnCancelarMesaCapturaMasiva_Click(null, null);
                        TextBoxAseguradoTelefono.Text = "";
                        TextBoxAseguradoEmail.Text = "";
                        Label42.Text = "Captura registrada";
                    }
                    else
                    {
                        Label46.Text = "Error de registro";
                    }
                }
                else
                {
                    if (asegurados.Asegurado_Captura_Registrar(IdTramite, IdPlan, IdDeducible, IdCausaSeguro, IdTipoProducto, IdRegiones) == 1)
                    {
                        BtnCancelarMesaCapturaMasiva_Click(null, null);
                        TextBoxAseguradoTelefono.Text = "";
                        TextBoxAseguradoEmail.Text = "";
                        Label42.Text = "Captura registrada";
                    }
                    else
                    {
                        Label46.Text = "Error de registro";
                    }
                }
            }
            else
            {
                Label46.Text = "Error de registro";
            }
        }

        protected void EliminaDireccion(object sender, CommandEventArgs e)
        {
            int IdDireccion = Convert.ToInt32(e.CommandArgument.ToString());
            int IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            if (asegurado_Direciones.Asegurado_Direcion_Desactivar_PorIdAseguradoDireccion(IdDireccion, IdTramite) == 1)
            {
                CargaDireciones();
                Label21.Text = "Eliminado";
            }
            else
            {
                Label21.Text = "Error en la eliminacion";
            }
        }

        protected void LinkButtonPoblacion_Click(object sender, EventArgs e)
        {
            LabelRespuestaCP.Text = "";
            LabelRespuestaCPSucceful.Text = "";
            TextBoxDato.Text = "";
            LabelCampo.Text = "";
            ButtonAgregarPoblacion.Visible = true;
            ButtonAgregarCP.Visible = false;
            ButtonAgregarColonia.Visible = false;

            if (cboCatDireccionesEstados.Text == "")
            {
                LabelRespuestaCP.Text = "Selecciona un estado";
            }
            else
            {
                LabelTituloDirecciones.Text = "Agregar población ha estado: " + cboCatDireccionesEstados.Text;
                LabelCampo.Text = "población";
                string script = "";
                script = "$('#AgreDirecciones').modal('show');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void BtnAgregarPoblacion_Click(object sender, EventArgs e)
        {
            LabelRespuestaCP.Text = "";
            LabelRespuestaCPSucceful.Text = "";
            //TextBoxDato.Text = "";
            LabelCampo.Text = "";
            ButtonAgregarPoblacion.Visible = false;
            ButtonAgregarCP.Visible = false;
            ButtonAgregarColonia.Visible = false;

            string script = "";
            script = "$('#AgreDirecciones').modal('hide'); $('body').removeClass('modal-open'); $('.modal-backdrop').remove();";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

            if (TextBoxDato.Text.Length > 0)
            {
                int IdEstado = Convert.ToInt32(cboCatDireccionesEstados.Value.ToString());
                if (capturaMasiva.AgregarPoblacion(IdEstado, TextBoxDato.Text.ToString().Trim()) == 1)
                {

                    LabelRespuestaCPSucceful.Text = "Poblacion Agregada";
                    cboCatDireccionesEstados_SelectedIndexChanged(null, null);
                }
                else
                {
                    LabelRespuestaCP.Text = "Error de registro";
                }
            }
            else
            {
                LabelRespuestaCP.Text = "Error no colocaste la población";
            }
        }

        protected void LinkButtonCP_Click(object sender, EventArgs e)
        {
            LabelRespuestaCP.Text = "";
            LabelRespuestaCPSucceful.Text = "";
            TextBoxDato.Text = "";
            LabelCampo.Text = "";
            ButtonAgregarPoblacion.Visible = false;
            ButtonAgregarCP.Visible = true;
            ButtonAgregarColonia.Visible = false;

            if (cboCatDireccionesPoblacion.Text == "")
            {
                LabelRespuestaCP.Text = "Selecciona una población";
            }
            else
            {
                LabelCampo.Text = "CP";
                LabelTituloDirecciones.Text = "Agregar CP ha población: " + cboCatDireccionesPoblacion.Text;
                string script = "";
                script = "$('#AgreDirecciones').modal('show');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void BtnAgregarCP_Click(object sender, EventArgs e)
        {
            LabelRespuestaCP.Text = "";
            LabelRespuestaCPSucceful.Text = "";
            //TextBoxDato.Text = "";
            LabelCampo.Text = "";
            ButtonAgregarPoblacion.Visible = false;
            ButtonAgregarCP.Visible = false;
            ButtonAgregarColonia.Visible = false;

            string script = "";
            script = "$('#AgreDirecciones').modal('hide'); $('body').removeClass('modal-open'); $('.modal-backdrop').remove();";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

            if (TextBoxDato.Text.Length > 0)
            {
                int IdPoblacion = Convert.ToInt32(cboCatDireccionesPoblacion.Value.ToString());
                if (capturaMasiva.AgregarCP(IdPoblacion, TextBoxDato.Text.ToString().Trim()) == 1)
                {
                    LabelRespuestaCPSucceful.Text = "CP Agregado";
                    cboCatDireccionesPoblacion_SelectedIndexChanged(null, null);
                }
                else
                {
                    LabelRespuestaCP.Text = "Error de registro Verifica tu CP";
                }
            }
            else
            {
                LabelRespuestaCP.Text = "Error no colocaste CP";
            }
        }

        protected void LinkButtonColonia_Click(object sender, EventArgs e)
        {
            LabelRespuestaCP.Text = "";
            LabelRespuestaCPSucceful.Text = "";
            TextBoxDato.Text = "";
            LabelCampo.Text = "";
            ButtonAgregarPoblacion.Visible = false;
            ButtonAgregarCP.Visible = false;
            ButtonAgregarColonia.Visible = true;

            if (cboCatDireccionesCP.Text == "")
            {
                LabelRespuestaCP.Text = "Selecciona un CP";
            }
            else
            {
                LabelCampo.Text = "Colonia";
                LabelTituloDirecciones.Text = "Agregar colonia ha CP: " + cboCatDireccionesCP.Text;
                string script = "";
                script = "$('#AgreDirecciones').modal('show');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void BtnAgregarColonia_Click(object sender, EventArgs e)
        {
            LabelRespuestaCP.Text = "";
            LabelRespuestaCPSucceful.Text = "";
            //TextBoxDato.Text = "";
            LabelCampo.Text = "";
            ButtonAgregarPoblacion.Visible = false;
            ButtonAgregarCP.Visible = false;
            ButtonAgregarColonia.Visible = false;

            string script = "";
            script = "$('#AgreDirecciones').modal('hide'); $('body').removeClass('modal-open'); $('.modal-backdrop').remove();";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

            if (TextBoxDato.Text.Length > 0)
            {
                int IdCP = Convert.ToInt32(cboCatDireccionesCP.Value.ToString());
                if (capturaMasiva.AgregarColonia(IdCP, TextBoxDato.Text.ToString().Trim()) == 1)
                {
                    LabelRespuestaCPSucceful.Text = "Colonia Agregada";
                    cboCatDireccionesCP_SelectedIndexChanged(null, null);
                }
                else
                {
                    LabelRespuestaCP.Text = "Error de registro";
                }
            }
            else
            {
                LabelRespuestaCP.Text = "Error no colocaste la colonia";
            }
        }


        // DATOS MOSTRADOS EN MESA KWIK
        protected void CargaDatosKWIK(int IdTramite)
        {

            captura.Tramite_KWIK tramite_KWIK = asegurados.Tramite_Consulta_KWIK(IdTramite);
            if (tramite_KWIK.RFC != null)
            {
                LabelInfFechaFirmaSolicitud.Text = tramite_KWIK.FechaFirmaSolicitud;
                LabelInfSucursal.Text = tramite_KWIK.Estado;
                LabelInfRFC.Text = tramite_KWIK.RFC;
                LabelInfAP.Text = tramite_KWIK.ApPaterno;
                LabelInfAM.Text = tramite_KWIK.ApMaterno;
                LabelInfNombre.Text = tramite_KWIK.Nombre;
                LabelInfNPoliza.Text = tramite_KWIK.IdSisLegados;
                LabelInfNoSolicitud.Text = tramite_KWIK.NumeroOrden;
                LabelInfClaveAgente.Text = tramite_KWIK.Clave;
                LabelInfClavePromotoria.Text = tramite_KWIK.ClavePromotoria;
                LabelInfNombrePromotoria.Text = tramite_KWIK.NombrePromotoria;
                LabelInfSolicitudNV.Text = tramite_KWIK.FolioCompuesto;
            }
        }


        #endregion

        #region Eventos Controles de Mesa Admision

        protected void BtnGuardarCaptura_Click(object sender, EventArgs e)
        {
            int Id_Tramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
            int Id_Usuario = manejo_sesion.Usuarios.IdUsuario;

            promotoria.TramiteN1 ActualizaTramiteDatos = new promotoria.TramiteN1();

            ActualizaTramiteDatos.IdMoneda = Convert.ToInt32(cboMoneda.SelectedValue);
            ActualizaTramiteDatos.PrimaCotizacion = Convert.ToDouble(txtPrimaTotalGMM.Text);
            ActualizaTramiteDatos.SumaBasica = Convert.ToDouble(txtSumaAseguradaBasica.Text);
            ActualizaTramiteDatos.NumeroOrden = textNumeroOrden.Text;
            ActualizaTramiteDatos.FechaSolicitud = dtFechaSolicitud.Text;
            ActualizaTramiteDatos.IdTipoTramite = Convert.ToInt32(hfTipoTramite.Value.ToString());
            ActualizaTramiteDatos.IdRiesgo = Convert.ToInt32(LisRiesgos.SelectedValue);
            // VARIABLES VACIAS
            ActualizaTramiteDatos.TipoPersona = 0;
            ActualizaTramiteDatos.Nombre = "";
            ActualizaTramiteDatos.ApPaterno = "";
            ActualizaTramiteDatos.ApMaterno = "";
            ActualizaTramiteDatos.Sexo = "";
            ActualizaTramiteDatos.FechaNacimiento = "1900-01-01";
            ActualizaTramiteDatos.RFC = "";
            ActualizaTramiteDatos.IdNacionalidad = 0;
            ActualizaTramiteDatos.FechaConst = "1900-01-01";

            ActualizaTramiteDatos.TitularNombre = "";
            ActualizaTramiteDatos.TitularApPat = "";
            ActualizaTramiteDatos.TitularApMat = "";
            ActualizaTramiteDatos.IdTitularNacionalidad = 0;
            ActualizaTramiteDatos.TitularSexo = "";
            ActualizaTramiteDatos.TitularFechaNacimiento = "1900-01-01";
            ActualizaTramiteDatos.TitularContratante = 0;


            if (cboTipoContratante.SelectedValue.Equals("1"))
            {
                ActualizaTramiteDatos.TipoPersona = 1;
                ActualizaTramiteDatos.Nombre = txNombre.Text;
                ActualizaTramiteDatos.ApPaterno = txApPat.Text;
                ActualizaTramiteDatos.ApMaterno = txApMat.Text;
                ActualizaTramiteDatos.Sexo = txSexo.SelectedValue.ToString();
                ActualizaTramiteDatos.FechaNacimiento = dtFechaNacimiento.Text;
                ActualizaTramiteDatos.RFC = txRfc.Text;
                ActualizaTramiteDatos.IdNacionalidad = Convert.ToInt32(txNacionalidad.SelectedIndex) + 1;
            }
            else if (cboTipoContratante.SelectedValue.Equals("2"))
            {
                ActualizaTramiteDatos.TipoPersona = 2;
                ActualizaTramiteDatos.Nombre = txNomMoral.Text;
                ActualizaTramiteDatos.RFC = txRfcMoral.Text;
                ActualizaTramiteDatos.FechaConst = dtFechaConstitucion.Text;
            }

            if (CheckBox1.Checked.Equals(true))
            {
                ActualizaTramiteDatos.TitularContratante = 1;
                ActualizaTramiteDatos.TitularNombre = txTiNombre.Text;
                ActualizaTramiteDatos.TitularApPat = txTiApPat.Text;
                ActualizaTramiteDatos.TitularApMat = txTiApMat.Text;
                ActualizaTramiteDatos.TitularFechaNacimiento = dtFechaNacimientoTitular.Text;
                ActualizaTramiteDatos.IdTitularNacionalidad = Convert.ToInt32(txTiNacionalidad.SelectedIndex) + 1;
                ActualizaTramiteDatos.TitularSexo = txtSexoM.SelectedValue.ToString();
            }

            if (CheckBoxHombreClave.Checked.Equals(true))
            {
                ActualizaTramiteDatos.HombreClave = 1;
            }
            else
            {
                ActualizaTramiteDatos.HombreClave = 0;
            }

            if (DropDownListTipoTramite.SelectedValue.Equals("-1"))
            {
                //ActualizaTramiteDatos.IdInstitucion = 0;
            }
            else
            {
                ActualizaTramiteDatos.IdInstitucion = Convert.ToInt32(DropDownListTipoTramite.SelectedValue);
            }
            List<prop.RespuestaTramite> respuestaTramites = Tramites.ActualizarTramite(Id_Usuario, Id_Tramite, ActualizaTramiteDatos);
            if (respuestaTramites[0].Folio == "OK")
            {
                string script = "";
                script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                TituloModal.Text = "Actualización de trámite";
                MensajeModal.Text = "Trámite actualizado exitosamente";
            }
            else
            {
                LabelRegistrarCaptura.Text = "ERROR NO REGISTRADO, VERIFICAR TRAMITE";
            }
        }

        protected void TramiteActualizado(object sender, EventArgs e)
        {
            TramiteTerminado();

            // no se procesan en automatico las llamadas ya que esto lo hace call center
            Response.Redirect("MisTramitesCallCenter.aspx", true);


            //Response.Redirect(EncripParametros("IdProceso=" + hfIdProceso.Value.ToString(), "TramiteTarea.aspx").URL, true);
            //Response.Redirect("TramiteTarea.aspx?IdProceso=" + hfIdProceso.Value.ToString(), true);
            //Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            PanelEtitar.Visible = true;
            PanelEtitar.Enabled = true;
            GuardarCaptura.Visible = true;
            EditarCaptura.Visible = false;
            CancelarCaptura.Visible = true;
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            PanelEtitar.Visible = false;
            PanelEtitar.Enabled = false;
            GuardarCaptura.Visible = false;
            EditarCaptura.Visible = true;
            CancelarCaptura.Visible = false;
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckB1();
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckB2();
        }

        protected void dtFechaNacimiento_OnChanged(object sender, EventArgs e)
        {
            txRfc.Text = Funciones.RFC.CalcularRFCFisico(dtFechaNacimiento.Text.Trim(), txNombre.Text.Trim(), txApPat.Text.Trim(), txApMat.Text.Trim());
            antecedentesRFC();
        }

        protected void dtFechaConstitucion_OnChanged(object sender, EventArgs e)
        {
            txRfcMoral.Text = Funciones.RFC.CalcularRFCMoral(dtFechaConstitucion.Text.Trim(), txNomMoral.Text.Trim());
            antecedentesRFC();
        }

        protected void LisNacionalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelRespuestaNacionalidadFisico.Text = "";
            LabelRespuestaNacionalidadFisico.Text = ValidaPais(Funciones.Numeros.ConvertirTextoANumeroEntero(txNacionalidad.Value.ToString()));
        }

        protected void LisTitNacionalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelRespuestaNacionalidadTitular.Text = "";
            LabelRespuestaNacionalidadTitular.Text = ValidaPais(Funciones.Numeros.ConvertirTextoANumeroEntero(txTiNacionalidad.Value.ToString()));
        }

        private string ValidaPais(int Id)
        {
            List<promotoria.cat_pais> cat_Pais_Sancionado = Catalogos.cat_Pais_Sancionado(Id);
            string respuesta = "";

            if (cat_Pais_Sancionado[0].Sancionado > 0)
            {
                respuesta = "Este país se encuentra en la lista de países sancionados";
            }
            return respuesta;
        }

        protected void antecedentesRFC()
        {
            LabelRespuestaRFCFisico.Text = "";
            LabelRespuestaRFCMoral.Text = "";

            if (cboTipoContratante.SelectedValue.Equals("1"))
            {
                string RFC = txRfc.Text.Trim().Replace("-", "");
                List<promotoria.TramiteN1> tramiteN1s = Catalogos.BustatramiteN1RFC(RFC);
                if (tramiteN1s[0].RFC == "1")
                {
                    LabelRespuestaRFCFisico.Text = "Ya existen trámites registrados para el RFC";
                }
            }
            else if (cboTipoContratante.SelectedValue.Equals("2"))
            {
                string RFC = txRfcMoral.Text.Trim().Replace("-", "");
                List<promotoria.TramiteN1> tramiteN1s = Catalogos.BustatramiteN1RFC(RFC);
                if (tramiteN1s[0].RFC == "1")
                {
                    LabelRespuestaRFCMoral.Text = "Ya existen trámites registrados para el RFC";
                }
            }
        }

        protected void cboTipoContratante_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoContratante();
        }

        protected void TipoContratante()
        {
            if (cboTipoContratante.SelectedValue.Equals("1"))
            {
                pnPrsFisica.Visible = true;
                pnPrsMoral.Visible = false;
                CheckBox1.Enabled = true;
                CheckBox2.Enabled = true;
            }
            else if (cboTipoContratante.SelectedValue.Equals("2"))
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

        private void FormatosFechas()
        {
            // INICIO DE FECHAS 
            dtFechaSolicitud.UseMaskBehavior = true;
            dtFechaSolicitud.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");
            dtFechaSolicitud.Date = DateTime.Today;

            dtFechaConstitucion.UseMaskBehavior = true;
            dtFechaConstitucion.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");

            dtFechaNacimiento.UseMaskBehavior = true;
            dtFechaNacimiento.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");

            dtFechaNacimientoTitular.UseMaskBehavior = true;
            dtFechaNacimientoTitular.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");

            dtCoAsFechaNacimiento.MaxDate = DateTime.Today;
            dtCoAsFechaNacimiento.UseMaskBehavior = true;
            dtCoAsFechaNacimiento.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");
            dtCoAsFechaNacimiento.Date = DateTime.Today;

            dtCoAsFechaNacimientoMesaCaptura.MaxDate = DateTime.Today;
            dtCoAsFechaNacimientoMesaCaptura.UseMaskBehavior = true;
            dtCoAsFechaNacimientoMesaCaptura.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");
            dtCoAsFechaNacimientoMesaCaptura.Date = DateTime.Today;

            FechaAntiguedad.MaxDate = DateTime.Today;
            FechaAntiguedad.UseMaskBehavior = true;
            FechaAntiguedad.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");
            FechaAntiguedad.Date = DateTime.Today;

            cpoFechaFirmaSolicitud.UseMaskBehavior = true;
            cpoFechaFirmaSolicitud.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");
            cpoFechaFirmaSolicitud.Date = DateTime.Today;
            cpoFechaFirmaSolicitud.MinDate = Convert.ToDateTime("01/01/2019");
        }

        private void ListaMonedas()
        {
            List<promotoria.cat_moneda> cat_monedas = Catalogos.Cat_Monedas();
            cboMoneda.DataSource = cat_monedas;
            cboMoneda.DataBind();
            cboMoneda.DataTextField = "Nombre";
            cboMoneda.DataValueField = "Id";
            cboMoneda.DataBind();
        }

        //private void ListaTipoTramite()
        //{
        //    List<promotoria.cat_Instituciones> Cat_Instituciones = Catalogos.cat_instituciones();
        //    DropDownListTipoTramite.DataSource = Cat_Instituciones;
        //    DropDownListTipoTramite.DataBind();
        //    DropDownListTipoTramite.DataTextField = "Banco";
        //    DropDownListTipoTramite.DataValueField = "Id";
        //    DropDownListTipoTramite.DataBind();
        //}

        private void cargarNacionalidadesCombo_db(ref ASPxComboBox objDDL)
        {
            List<promotoria.cat_pais> cat_pais = Catalogos.Cat_Paises();
            objDDL.DataSource = cat_pais;
            objDDL.TextField = "PaisNombre";
            objDDL.ValueField = "Id";
            objDDL.DataBind();
        }

        private void CargaIndicadorMesas(int Id)
        {
            List<prop.Indicador_StatusMesas> IndicadorDatos = indicador.StatusMesas(Id);
            StatusMesa.DataSource = IndicadorDatos;
            StatusMesa.DataBind();
        }

        #endregion

        #region "CARGA DE EXPEDIENTE HE INSUMOS"
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

        protected void btnSubirDocumento_Click(object sender, EventArgs e)
        {
            LabRespuestaArchivosCarga.Text = "";
            /* LISTA LOS ARCHIVOS DEL DOCUMENTO */
            List<promotoria.expediente> LstArchivosDocumento = new List<promotoria.expediente>();
            // SI EXISTE LA VARIABLE DE SESION LLENA LA LISTA
            if (Session["documentos"] != null)
            {
                LstArchivosDocumento = (List<promotoria.expediente>)Session["documentos"];
            }

            if (fileUpDocumento.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fileUpDocumento.FileName).ToLower();
                string fileExtension2 = "";
                if (".pdf".Contains(fileExtension) ^ ".jpg".Contains(fileExtension) ^ ".png".Contains(fileExtension))
                {
                    int fileSize = fileUpDocumento.PostedFile.ContentLength;
                    promotoria.expediente expedientes = new promotoria.expediente();

                    if (fileSize < 41943040)
                    {
                        List<promotoria.control_archivos> control_Archivos = archivos.ControlArchivoNuevoID();
                        int IdControlArchivo = control_Archivos[0].Id;
                        string nombreArchivo = IdControlArchivo.ToString().PadLeft(12, '0');
                        //string directorioTemporal = Server.MapPath("~") + "\\DocsUp\\";
                        string directorioTemporal = Server.MapPath("~") + expedientes.CarpetaArchivada;

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

        protected void btnEliminaDocumento_Click(object sender, EventArgs e)
        {
            if (lstDocumentos.Items.Count > 0 && lstDocumentos.SelectedIndex > -1)
            {
                List<promotoria.expediente> LstArchExpediente = new List<promotoria.expediente>();
                List<promotoria.expediente> LstArchExpedienteTmp = new List<promotoria.expediente>();
                if (Session["documentos"] != null) { LstArchExpediente = (List<promotoria.expediente>)Session["documentos"]; }
                int contador = 0;
                foreach (promotoria.expediente oArchivo in LstArchExpediente)
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

        protected void btnSubirInsumo_Click(object sender, EventArgs e)
        {
            List<promotoria.insumos> LstArchivosInsumo = new List<promotoria.insumos>();
            if (Session["insumos"] != null) { LstArchivosInsumo = (List<promotoria.insumos>)Session["insumos"]; }
            if (fileUpInsumo.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fileUpInsumo.FileName).ToLower();
                promotoria.insumos oInsumo = new promotoria.insumos();
                int fileSize = fileUpInsumo.PostedFile.ContentLength;
                if (fileSize < 41943040)
                {

                    List<promotoria.control_archivos> control_Archivos = archivos.ControlArchivoNuevoID();
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

        protected void btnEliminaInsumo_Click(object sender, EventArgs e)
        {
            if (lstInsumos.Items.Count > 0 && lstInsumos.SelectedIndex > -1)
            {
                List<promotoria.insumos> LstArchInsumos = new List<promotoria.insumos>();
                List<promotoria.insumos> LstArchInsumosTmp = new List<promotoria.insumos>();
                if (Session["insumos"] != null) { LstArchInsumos = (List<promotoria.insumos>)Session["insumos"]; }
                int contador = 0;
                foreach (promotoria.insumos oInsumo in LstArchInsumos)
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

        private string registraDocumentosExpediente(int pIdTramite, int TipoTramite)
        {
            string msgError = "";
            string strRutaServidor = "";
            string strArchivoOrigen = "";

            promotoria.expediente expediente = new promotoria.expediente();
            string directorioTemporal = Server.MapPath("~") + expediente.CarpetaArchivada;
            //string directorioTemporal = Server.MapPath("~") + "\\DocsUp\\";

            List<promotoria.expediente> LstExpediente = new List<promotoria.expediente>();
            if (Session["documentos"] != null)
            {
                LstExpediente = (List<promotoria.expediente>)Session["documentos"];
            }

            List<string> lstArchivos = new List<string>();
            foreach (promotoria.expediente oDocumento in LstExpediente)
            {
                //strArchivoOrigen = Server.MapPath("~") + "\\DocsUp\\" + oDocumento.NmArchivo;
                strArchivoOrigen = Server.MapPath("~") + oDocumento.CarpetaArchivada + oDocumento.NmArchivo;
                if (File.Exists(strArchivoOrigen))
                {
                    archivos.Agregar_Expedientes_Tramite(TipoTramite, pIdTramite, oDocumento.Id_Archivo, oDocumento.NmArchivo, oDocumento.NmOriginal, oDocumento.Activo, oDocumento.Fusion, oDocumento.Descripcion);
                    lstArchivos.Add(strArchivoOrigen);
                }
            }

            // CONSULTA ID DEL EXPEDIENTE FUCIONADO
            List<promotoria.expediente> expedientes = archivos.ConsultaExpediente(pIdTramite, TipoTramite);
            string ArchFusionAnt = "";
            int Id_Expediente = 0;
            if (expedientes.Count > 0)
            {
                ArchFusionAnt = directorioTemporal + expedientes[0].NmArchivo;
                Id_Expediente = expedientes[0].Id;
            }

            List<promotoria.control_archivos> control_Archivos = archivos.ControlArchivoNuevoID();
            int IdControlArchivo = control_Archivos[0].Id;
            string nombreFusion = directorioTemporal + IdControlArchivo.ToString().PadLeft(12, '0') + ".pdf";
            if (File.Exists(nombreFusion))
            {
                File.Delete(nombreFusion);
            }

            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
            string nmSeparador = directorioTemporal + manejo_sesion.Usuarios.IdUsuario + ".pdf";
            string nmLogo = Server.MapPath("~\\Imagenes") + @"\logo_sep.png";


            msgError = WFO.Funciones.ManejoArchivos.Adiciona(lstArchivos, ArchFusionAnt, nombreFusion, manejo_sesion.Usuarios.Nombre, nmSeparador, nmLogo);

            if (string.IsNullOrEmpty(msgError))
            {
                archivos.ModificarExpedienteFusion(Id_Expediente);
                archivos.Agregar_Expedientes_Tramite(TipoTramite, pIdTramite, IdControlArchivo, IdControlArchivo.ToString().PadLeft(12, '0') + ".pdf", "Archivo Fucion Agrgacion", 1, 1, "");
                msgError = "";
            }
            else
            {
                Mensajes.Text = msgError;
            }

            return msgError;
        }

        private string registraDocumentosInsumos(int pIdTramite, int TipoTramite)
        {
            List<promotoria.insumos> LstArchivosInsumo = new List<promotoria.insumos>();
            if (Session["insumos"] != null) { LstArchivosInsumo = (List<promotoria.insumos>)Session["insumos"]; }

            string strArchivoOrigen = "";

            foreach (promotoria.insumos oDocumento in LstArchivosInsumo)
            {
                strArchivoOrigen = Server.MapPath("~") + "\\DocsInsumos\\" + oDocumento.NmArchivo;

                if (File.Exists(strArchivoOrigen))
                {
                    archivos.Agregar_Insumo_Tramite(TipoTramite, pIdTramite, oDocumento.Id_Archivo, oDocumento.NmArchivo, oDocumento.NmOriginal, oDocumento.Activo, oDocumento.Descripcion);
                }
            }

            return "";
        }

        #endregion

        private void pintaChecks(int IdMesa, int IdTramite)
        {
            List<prop.Cat_CheckBox_Mesa> catalogo = cat_CheckBox.CheckBox_Mesa_Seleccionar_PorIdMesa(IdMesa);
            DocRequerid.DataSource = catalogo;
            DocRequerid.ID = "Id";
            DocRequerid.DataTextField = "Documentos";
            DocRequerid.DataValueField = "Id";
            DocRequerid.DataBind();
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
                    //strDoctoWeb = "..\\..\\DocsUp\\" + oArchivo.NmArchivo;
                    //strDoctoServer = Server.MapPath("~") + "\\DocsUp\\" + oArchivo.NmArchivo;
                    strDoctoWeb = "..\\.." + oArchivo.CarpetaArchivada + oArchivo.NmArchivo;
                    strDoctoServer = Server.MapPath("~") + oArchivo.CarpetaArchivada + oArchivo.NmArchivo;
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

            string script = "";
            script = "$('#Expediente').modal('hide'); $('body').removeClass('modal-open'); $('.modal-backdrop').remove(); window.open('" + strDoctoWeb.ToString().Replace("\\", "/") + "','Expediente', 'width = 600, height = 400');";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            /*
            string script = "$('#Expediente').modal('hide'); ";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            */
        }

        private void CargarPFD(int Id)
        {
            int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());
            ltMuestraPdf.Text = "";
            ltMuestraPdf.Text = "<iframe src='" + EncripParametros("IdTramite=" + Id + ",IdTipoTramite=" + TipoTramite, "Displaypdf.aspx").URL + "' style='width:100%; height:540px' style='border: none;'></iframe>";


            //int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());

            //List<promotoria.expediente> expedientes = archivos.ConsultaExpediente(Id, TipoTramite);
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

        protected void BtnExpediente_click(object sender, EventArgs e)
        {
            AnexoArchivos.Visible = true;
            LinkButtonAgregarArchivos.Visible = false;
            LinkButtonCancelarArchivos.Visible = true;
        }

        protected void BtnExpedienteOcultarFrom_click(object sender, EventArgs e)
        {
            AnexoArchivos.Visible = false;
            LinkButtonAgregarArchivos.Visible = true;
            LinkButtonCancelarArchivos.Visible = false;
        }

        protected void BtnExpedienteOcultar_click(object sender, EventArgs e)
        {
            Expediente.Visible = false;
            LinkButtonMostrarExpediente.Visible = true;
            LinkButtonOcultarExpediente.Visible = false;
        }

        protected void BtnExpedienteMostrar_click(object sender, EventArgs e)
        {
            Expediente.Visible = true;
            LinkButtonMostrarExpediente.Visible = false;
            LinkButtonOcultarExpediente.Visible = true;
        }

        protected void BtnCapturaAbrir_click(object sender, EventArgs e)
        {
            int IdTramite = Convert.ToInt32(hfIdTramite.Value);
            int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());

            List<promotoria.expediente> expedientes = archivos.ConsultaExpediente(IdTramite, TipoTramite);
            string strDoctoWeb = "";

            strDoctoWeb = "ConsultaCaptura.aspx?Id=" + IdTramite;

            string script = "window.open('" + strDoctoWeb.ToString().Replace("\\", "/") + "','Expediente', 'width = 800, height = 400');";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }

        protected void BtnExpedienteAbrir_click(object sender, EventArgs e)
        {
            int IdTramite = Convert.ToInt32(hfIdTramite.Value);
            int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());

            List<promotoria.expediente> expedientes = archivos.ConsultaExpediente(IdTramite, TipoTramite);
            string strDoctoWeb = "";
            string strDoctoServer = "";

            strDoctoWeb = "..\\..\\ArchivosDefinitivos\\404.pdf";

            if (expedientes.Count > 0)
            {
                foreach (promotoria.expediente oArchivo in expedientes)
                {
                    if (!string.IsNullOrEmpty(oArchivo.NmArchivo))
                    {
                        //strDoctoWeb = "..\\..\\DocsUp\\" + oArchivo.NmArchivo;
                        //strDoctoServer = Server.MapPath("~") + "\\DocsUp\\" + oArchivo.NmArchivo;
                        strDoctoWeb = "..\\.." + oArchivo.CarpetaArchivada + oArchivo.NmArchivo;
                        strDoctoServer = Server.MapPath("~") + oArchivo.CarpetaArchivada + oArchivo.NmArchivo;
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

        protected void btnSendToMesa_Click(object sender, EventArgs e)
        {
            if (cboToSend.SelectedIndex != -1)
            {
                int IdMesaToSend = int.Parse(cboToSend.SelectedValue.ToString());
                int IdMesa = int.Parse(hfIdProceso.Value.ToString());
                int IdTramite = int.Parse(hfIdTramite.Value.ToString());
                int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());

                string observaciones = txtObservacionesPrivadas.Text.ToString().Trim().Replace("'", "");

                List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.EnviarTramite(IdTramite, IdMesa, IdMesaToSend, manejo_sesion.Usuarios.IdUsuario, observaciones, "");
                if (objResultado[0].IdTramite > 0)
                {
                    // REGISTRO DE ARCHIVOS - COLOCAR DESPUES DE ACTUALIZAR EL TRAMITE
                    if (Session["documentos"] != null) { string resultadoExpediente = registraDocumentosExpediente(IdTramite, TipoTramite); }
                    if (Session["insumos"] != null) { string resultadoInsumo = registraDocumentosInsumos(IdTramite, TipoTramite); }
                    /*
                    string script = "";
                    script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                    TituloModal.Text = "Operación realizada";
                    MensajeModal.Text = objResultado[0].Accion;
                    */
                    TramiteTerminado();
                    Response.Redirect(EncripParametros("IdProceso=" + hfIdProceso.Value.ToString(), "TramiteTarea.aspx").URL, true);
                    //Response.Redirect("TramiteTarea.aspx?IdProceso=" + hfIdProceso.Value.ToString(), true);
                }
            }
        }

        protected void btnAceptarSeleccionCompleta_Click(object sender, EventArgs e)
        {
            Funciones.VariablesGlobales.StatusMesa IdStatusMesaProcesado = Funciones.VariablesGlobales.StatusMesa.SeleccionCompleta;
            int IdTramite = Int32.Parse(hfIdTramite.Value);
            prop.PermisosMesaControles permisosMesaControles = new prop.PermisosMesaControles();

            switch (hfNombreProceso.Value.ToString())
            {
                case "ADMISIÓN":
                    if (Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(Convert.ToInt32(hfIdProceso.Value), "PanelSeleccionCompleta")).Activo))
                    {
                        int resultado3 = Tramites.ActualizaSeleccionCompleta(IdTramite, manejo_sesion.Usuarios.IdUsuario, Convert.ToInt32(DropDownListSeleccionCompleta.SelectedValue));
                    }
                    break;
                case "EJECUCIÓN":
                    int resultado2 = Tramites.ActualizaPolizaSistemasLegados(IdTramite, manejo_sesion.Usuarios.IdUsuario, TextNumPolizaSisLegado.Text);
                    break;
                case "KWIK":
                    int resultado = Tramites.ActualizaKwik(IdTramite, manejo_sesion.Usuarios.IdUsuario, TextNumKwik.Text);
                    break;

                default:
                    break;
            }

            List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.ProcesarTramite(IdTramite, int.Parse(hfIdProceso.Value), manejo_sesion.Usuarios.IdUsuario, IdStatusMesaProcesado, "", txtObservacionesPrivadas.Text.Trim().Replace("'", ""), "");
            if (objResultado[0].IdTramite > 0)
            {
                int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());
                // REGISTRO DE ARCHIVOS - COLOCAR DESPUES DE ACTUALIZAR EL TRAMITE
                if (Session["documentos"] != null) { string resultadoExpediente = registraDocumentosExpediente(IdTramite, TipoTramite); }
                if (Session["insumos"] != null) { string resultadoInsumo = registraDocumentosInsumos(IdTramite, TipoTramite); }

                if (objResultado[0].Accion == "KO")
                {
                    int IdMesa = int.Parse(hfIdProceso.Value.ToString());

                    log.Agregar("Error de aceptación de trámite ID = " + IdTramite + ", en mesa IDMESA = " + IdMesa + " , respuesta de store procedure spWFOTramiteProcesar: " + objResultado[0].Accion);
                }

                TramiteTerminado();
                Response.Redirect(EncripParametros("IdProceso=" + hfIdProceso.Value.ToString(), "TramiteTarea.aspx").URL, true);
                //Response.Redirect("TramiteTarea.aspx?IdProceso=" + hfIdProceso.Value.ToString(), true);
            }
        }



        protected void RegistraCheckBoxList()
        {
            prop.Cat_CheckBox_Mesa _CheckBox_Mesa = new prop.Cat_CheckBox_Mesa();
            _CheckBox_Mesa.Id = int.Parse(hfIdTramite.Value.ToString());
            _CheckBox_Mesa.IdMesa = int.Parse(hfIdProceso.Value.ToString());

            foreach (ListItem item in DocRequerid.Items)
            {
                if (item.Selected)
                {
                    _CheckBox_Mesa.IdTramite = int.Parse(hfIdTramite.Value.ToString());
                    _CheckBox_Mesa.IdMesa = int.Parse(hfIdProceso.Value.ToString());
                    _CheckBox_Mesa.Id = int.Parse(item.Value);

                    cat_CheckBox.Agregar_Check(_CheckBox_Mesa);
                }
            }
        }

        protected void btnTaskDoModal_Click(object sender, EventArgs e)
        {
            //Funciones.VariablesGlobales.StatusMesa IdStatusMesaProcesado = Funciones.VariablesGlobales.StatusMesa.Procesado;
            int IdTramite = Int32.Parse(hfIdTramite.Value);
            //prop.PermisosMesaControles permisosMesaControles = new prop.PermisosMesaControles();

            //switch (hfNombreMesa.Value.ToString())
            //{
            //    case "ADMISIÓN":
            //        if (Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(Convert.ToInt32(hfIdProceso.Value), "PanelSeleccionCompleta")).Activo))
            //        {
            //            int resultado3 = Tramites.ActualizaSeleccionCompleta(IdTramite, manejo_sesion.Usuarios.IdUsuario, Convert.ToInt32(DropDownListSeleccionCompleta.SelectedValue));
            //        }
            //        break;
            //    case "EJECUCIÓN":
            //        int resultado2 = Tramites.ActualizaPolizaSistemasLegados(IdTramite, manejo_sesion.Usuarios.IdUsuario, TextNumPolizaSisLegado.Text);
            //        break;
            //    case "KWIK":
            //        int resultado = Tramites.ActualizaKwik(IdTramite, manejo_sesion.Usuarios.IdUsuario, TextNumKwik.Text);
            //        break;

            //    default:
            //        break;
            //}

            //List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.ProcesarTramite(IdTramite, int.Parse(hfIdProceso.Value), manejo_sesion.Usuarios.IdUsuario, IdStatusMesaProcesado, "", txtObservacionesPrivadas.Text, "");
            //if (objResultado[0].IdTramite > 0)
            //{
            //    int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());
            //    // REGISTRO DE ARCHIVOS - COLOCAR DESPUES DE ACTUALIZAR EL TRAMITE
            //    if (Session["documentos"] != null) { string resultadoExpediente = registraDocumentosExpediente(IdTramite, TipoTramite); }
            //    if (Session["insumos"] != null) { string resultadoInsumo = registraDocumentosInsumos(IdTramite, TipoTramite); }
            //    // REGISTRA EL CHECKLIST
            //    RegistraCheckBoxList();

            //    if (objResultado[0].Accion == "KO")
            //    {
            //        int IdMesa = int.Parse(hfIdProceso.Value.ToString());

            //        log.Agregar("Error de aceptación de trámite ID = " + IdTramite + ", en mesa IDMESA = " + IdMesa + " , respuesta de store procedure spWFOTramiteProcesar: " + objResultado[0].Accion);
            //    }
            //    TramiteTerminado();
            //    TextNumPolizaSisLegado.Text = "";
            //    Response.Redirect("TramiteTarea.aspx?IdMesa=" + hfIdProceso.Value.ToString(), true);
            //}
        }

        protected void btnTaskOkModal_Click(object sender, EventArgs e)
        {
            //Funciones.VariablesGlobales.StatusMesa IdStatusMesaProcesado = Funciones.VariablesGlobales.StatusMesa.Procesado;
            int IdTramite = Int32.Parse(hfIdTramite.Value);
            //prop.PermisosMesaControles permisosMesaControles = new prop.PermisosMesaControles();

            //switch (hfNombreMesa.Value.ToString())
            //{
            //    case "ADMISIÓN":
            //        if (Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(Convert.ToInt32(hfIdProceso.Value), "PanelSeleccionCompleta")).Activo))
            //        {
            //            int resultado3 = Tramites.ActualizaSeleccionCompleta(IdTramite, manejo_sesion.Usuarios.IdUsuario, Convert.ToInt32(DropDownListSeleccionCompleta.SelectedValue));
            //        }
            //        break;
            //    case "EJECUCIÓN":
            //        int resultado2 = Tramites.ActualizaPolizaSistemasLegados(IdTramite, manejo_sesion.Usuarios.IdUsuario, TextNumPolizaSisLegado.Text);
            //        break;
            //    case "KWIK":
            //        int resultado = Tramites.ActualizaKwik(IdTramite, manejo_sesion.Usuarios.IdUsuario, TextNumKwik.Text);
            //        break;

            //    default:
            //        break;
            //}

            int IdStatusMesaProcesado = 4;  // hace falta crear el enumerador

            List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.ProcesarTramiteTarea(IdTramite, int.Parse(hfIdProceso.Value), manejo_sesion.Usuarios.IdUsuario, IdStatusMesaProcesado, "", txtObservacionesPrivadas.Text.Trim().Replace("'", ""), "");
            if (objResultado[0].IdTramite > 0)
            {
                int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());
                // REGISTRO DE ARCHIVOS - COLOCAR DESPUES DE ACTUALIZAR EL TRAMITE
                ////if (Session["documentos"] != null) { string resultadoExpediente = registraDocumentosExpediente(IdTramite, TipoTramite); }
                ////if (Session["insumos"] != null) { string resultadoInsumo = registraDocumentosInsumos(IdTramite, TipoTramite); }
                ////// REGISTRA EL CHECKLIST
                ////RegistraCheckBoxList();

                if (objResultado[0].Accion == "KO")
                {
                    int IdMesa = int.Parse(hfIdProceso.Value.ToString());

                    log.Agregar("Error de aceptación de trámite ID = " + IdTramite + ", en mesa IDMESA = " + IdMesa + " , respuesta de store procedure spWFOTramiteProcesar: " + objResultado[0].Accion);
                }
                TramiteTerminado();
                TextNumPolizaSisLegado.Text = "";
                Response.Redirect(EncripParametros("IdProceso=" + hfIdProceso.Value.ToString(), "TramiteTarea.aspx").URL, true);
                //Response.Redirect("TramiteTarea.aspx?IdProceso=" + hfIdProceso.Value.ToString(), true);
            }

            Response.Redirect("Default.aspx", true);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //Funciones.VariablesGlobales.StatusMesa IdStatusMesaProcesado = Funciones.VariablesGlobales.StatusMesa.Procesado;
            //int IdTramite = Int32.Parse(hfIdTramite.Value);
            //prop.PermisosMesaControles permisosMesaControles = new prop.PermisosMesaControles();

            //switch (hfNombreMesa.Value.ToString())
            //{
            //    case "ADMISIÓN":
            //        if (Convert.ToBoolean((permisosMesaControles = permisosMesa.PermisosMesaControles_Selecionar(Convert.ToInt32(hfIdProceso.Value), "PanelSeleccionCompleta")).Activo))
            //        {
            //            int resultado3 = Tramites.ActualizaSeleccionCompleta(IdTramite, manejo_sesion.Usuarios.IdUsuario, Convert.ToInt32(DropDownListSeleccionCompleta.SelectedValue));
            //        }
            //        break;
            //    case "EJECUCIÓN":
            //        int resultado2 = Tramites.ActualizaPolizaSistemasLegados(IdTramite, manejo_sesion.Usuarios.IdUsuario, TextNumPolizaSisLegado.Text);
            //        break;
            //    case "KWIK":
            //        int resultado = Tramites.ActualizaKwik(IdTramite, manejo_sesion.Usuarios.IdUsuario, TextNumKwik.Text);
            //        break;

            //    default:
            //        break;
            //}

            //List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.ProcesarTramite(IdTramite, int.Parse(hfIdProceso.Value), manejo_sesion.Usuarios.IdUsuario, IdStatusMesaProcesado, "", txtObservacionesPrivadas.Text, "");
            //if (objResultado[0].IdTramite > 0)
            //{
            //    int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());
            //    // REGISTRO DE ARCHIVOS - COLOCAR DESPUES DE ACTUALIZAR EL TRAMITE
            //    if (Session["documentos"] != null) { string resultadoExpediente = registraDocumentosExpediente(IdTramite, TipoTramite); }
            //    if (Session["insumos"] != null) { string resultadoInsumo = registraDocumentosInsumos(IdTramite, TipoTramite); }
            //    // REGISTRA EL CHECKLIST
            //    RegistraCheckBoxList();

            //    if (objResultado[0].Accion == "KO")
            //    {
            //        int IdMesa = int.Parse(hfIdProceso.Value.ToString());

            //        log.Agregar("Error de aceptación de trámite ID = " + IdTramite + ", en mesa IDMESA = " + IdMesa + " , respuesta de store procedure spWFOTramiteProcesar: " + objResultado[0].Accion);
            //    }
            //    TramiteTerminado();
            //    TextNumPolizaSisLegado.Text = "";
            //    Response.Redirect("TramiteTarea.aspx?IdMesa=" + hfIdProceso.Value.ToString(), true);
            //}

            Response.Redirect("Default.aspx", true);
        }

        protected void btnPCI_Click(object sender, EventArgs e)
        {
            Funciones.VariablesGlobales.StatusMesa IdStatusMesa = Funciones.VariablesGlobales.StatusMesa.PCI;
            int IdTramite = Int32.Parse(hfIdTramite.Value);
            int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());

            List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.ProcesarTramite(IdTramite, int.Parse(hfIdProceso.Value), manejo_sesion.Usuarios.IdUsuario, IdStatusMesa, txtObservacionesPublicasHold.Text.Trim().Replace("'", ""), txtObservacionesPrivadas.Text.Trim().Replace("'", ""), "");
            if (!Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "PopupScript"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "LimpiarForm();", true);
            }

            /* EL ESTATUS PCI NO REQUIERE REGISTRAR ARCHIVOS */
            if (objResultado[0].IdTramite > 0)
            {
                if (objResultado[0].Accion == "KO")
                {
                    int IdMesa = int.Parse(hfIdProceso.Value.ToString());

                    log.Agregar("Error de PCI de trámite ID = " + IdTramite + ", en Proceso = " + IdMesa + " , respuesta de store procedure spWFOTramiteProcesar: " + objResultado[0].Accion);
                }
                else
                {
                    // REGISTRO DE ARCHIVOS - COLOCAR DESPUES DE ACTUALIZAR EL TRAMITE
                    if (Session["documentos"] != null) { string resultadoExpediente = registraDocumentosExpediente(IdTramite, TipoTramite); }
                    if (Session["insumos"] != null) { string resultadoInsumo = registraDocumentosInsumos(IdTramite, TipoTramite); }

                    // SE ELIMINA EL EXPEDIENTE
                    BorrarExpediente(IdTramite, TipoTramite);
                }
                Response.Redirect(EncripParametros("IdProceso=" + hfIdProceso.Value.ToString(), "TramiteTarea.aspx").URL, true);
                //Response.Redirect("TramiteTarea.aspx?IdProceso=" + hfIdProceso.Value.ToString(), true);
            }
        }

        protected void BorrarExpediente(int IdTramite, int IdTipoTramite)
        {
            // TIPO DE TRAMITE
            int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());
            // CONSULTA EL EXPEDIENTE QUE SE MUESTRA
            List<promotoria.expediente> expedientes = archivos.ConsultaExpediente(IdTramite, TipoTramite);
            string strDoctoServer = "";

            // EN CASO DE NO ENCONTRAR UN EXPEDIENTE.
            if (expedientes.Count > 0)
            {
                foreach (promotoria.expediente oArchivo in expedientes)
                {
                    // OBTIENE EL NOMBRW DEL ARCHIVO
                    if (!string.IsNullOrEmpty(oArchivo.NmArchivo))
                    {
                        // RUTA DEL ARCHIVO A ELIMINAR
                        //strDoctoServer = Server.MapPath("~") + "\\DocsUp\\" + oArchivo.NmArchivo;
                        strDoctoServer = Server.MapPath("~") + oArchivo.CarpetaArchivada + oArchivo.NmArchivo;
                        // VALIDA LA EXISTENCIA DEL ARCHIVO.
                        if (File.Exists(strDoctoServer))
                        {
                            // ELIMINA EL ARCHIVO
                            File.Delete(strDoctoServer);
                            // COPIA EL AECHIVO PCI Y LO RENOMBRE CON EL ARCHIVO ANTERIOR
                            File.Copy(Server.MapPath("~") + "\\ArchivosDefinitivos\\PCI.pdf", strDoctoServer);
                        }
                        else
                        {
                            // COPIA EL AECHIVO PCI Y LO RENOMBRE CON EL ARCHIVO ANTERIOR
                            File.Copy(Server.MapPath("~") + "\\ArchivosDefinitivos\\PCI.pdf", strDoctoServer);
                        }
                    }
                    else
                    {
                        log.Agregar("No encontro el nombre del expediente del IdTramite: " + IdTramite);
                    }
                }
            }
            else
            {
                log.Agregar("No encontro el tramite del IdTramite: " + IdTramite);
            }

        }

        protected void btnAplicarContactoEfectivo_Click(object sender, EventArgs e)
        {
            var nodos = treeListHold.GetSelectedNodes();
            int IdMotivoRechazo = 0;
            string MotivosRechazos = "";

            if (nodos.Count > 0)
            {
                if (txtObservacionesPublicasHold.Text.Length > 0)
                {
                    MotivosRechazos = "";
                    foreach (TreeListNode node in nodos)
                    {
                        IdMotivoRechazo = Convert.ToInt32(node.GetValue("id"));
                        if (MotivosRechazos.Length > 0)
                            MotivosRechazos += ",";
                        MotivosRechazos += IdMotivoRechazo.ToString();
                    }

                    treeListHold.UnselectAll();

                    Funciones.VariablesGlobales.StatusMesa IdStatusMesa = Funciones.VariablesGlobales.StatusMesa.ContactoEfectivo;
                    int IdTramite = Int32.Parse(hfIdTramite.Value);
                    int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());

                    List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.ProcesarTramiteCallCenter(IdTramite, int.Parse(hfIdProceso.Value), manejo_sesion.Usuarios.IdUsuario, IdStatusMesa, txtObservacionesPublicasHold.Text.Trim().Replace("'", ""), txtObservacionesPrivadas.Text.Trim().Replace("'", ""), MotivosRechazos, TextNumPolizaSisLegado.Text.Trim().Replace("'", ""), txtNumeroLlamada.Text.Trim().Replace("'", ""));
                    if (!Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "PopupScript"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "LimpiarForm();", true);
                    }

                    if (objResultado[0].IdTramite > 0)
                    {
                        string script = "";
                        script = "pnlPopMotivosHold.Hide(); $('#myModal').modal({backdrop: 'static', keyboard: false}); ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    }
                }
                else
                {
                    string script = "";
                    script = "AlertaObservaciones();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            else
            {
                string script = "";
                script = "AlertaMotivosHold();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnAplicarCancelar_Click(object sender, EventArgs e)
        {
            var nodos = treeListCancelar.GetSelectedNodes();
            int IdMotivoRechazo = 0;
            string MotivosRechazos = "";

            if (nodos.Count > 0)
            {
                if (txObservacionesCancelacion.Text.Length > 0)
                {
                    MotivosRechazos = "";
                    foreach (TreeListNode node in nodos)
                    {
                        IdMotivoRechazo = Convert.ToInt32(node.GetValue("id"));
                        if (MotivosRechazos.Length > 0)
                            MotivosRechazos += ",";
                        MotivosRechazos += IdMotivoRechazo.ToString();
                    }

                    treeListCancelar.UnselectAll();

                    Funciones.VariablesGlobales.StatusMesa IdStatusMesa = Funciones.VariablesGlobales.StatusMesa.Cancelado;
                    int IdTramite = Int32.Parse(hfIdTramite.Value);
                    int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());

                    List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.ProcesarTramite(IdTramite, int.Parse(hfIdProceso.Value), manejo_sesion.Usuarios.IdUsuario, IdStatusMesa, txObservacionesCancelacion.Text.Trim().Replace("'", ""), txtObservacionesPrivadas.Text.Trim().Replace("'", ""), MotivosRechazos);
                    if (!Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "PopupScript"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "LimpiarForm();", true);
                    }

                    if (objResultado[0].IdTramite > 0)
                    {
                        // REGISTRO DE ARCHIVOS - COLOCAR DESPUES DE ACTUALIZAR EL TRAMITE
                        if (Session["documentos"] != null) { string resultadoExpediente = registraDocumentosExpediente(IdTramite, TipoTramite); }
                        if (Session["insumos"] != null) { string resultadoInsumo = registraDocumentosInsumos(IdTramite, TipoTramite); }

                        string script = "";
                        script = "pnlPopCancelar.Hide(); $('#myModal').modal({backdrop: 'static', keyboard: false}); ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    }
                }
                else
                {
                    string script = "";
                    script = "AlertaObservaciones();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            else
            {
                string script = "";
                script = "AlertaMotivosCancelacion();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnAplicarContactoNoEfectivo_Click(object sender, EventArgs e)
        {
            var nodos = treeListSuspender.GetSelectedNodes();
            int IdMotivoRechazo = 0;
            string MotivosRechazos = "";

            if (nodos.Count > 0)
            {
                if (txtObservacionesPublicasSuspender.Text.Length > 0)
                {
                    MotivosRechazos = "";
                    foreach (TreeListNode node in nodos)
                    {
                        IdMotivoRechazo = Convert.ToInt32(node.GetValue("id"));
                        if (MotivosRechazos.Length > 0)
                            MotivosRechazos += ",";
                        MotivosRechazos += IdMotivoRechazo.ToString();
                    }

                    treeListHold.UnselectAll();

                    Funciones.VariablesGlobales.StatusMesa IdStatusMesa = Funciones.VariablesGlobales.StatusMesa.ContactoNoEfectivo;
                    int IdTramite = Int32.Parse(hfIdTramite.Value);
                    int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());

                    List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.ProcesarTramiteCallCenter(IdTramite, int.Parse(hfIdProceso.Value), manejo_sesion.Usuarios.IdUsuario, IdStatusMesa, txtObservacionesPublicasSuspender.Text.Trim().Replace("'", ""), txtObservacionesPrivadas.Text.Trim().Replace("'", ""), MotivosRechazos, TextNumPolizaSisLegado.Text.Trim().Replace("'", ""), txtNumeroLlamada.Text.Trim().Replace("'", ""));
                    if (!Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "PopupScript"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "LimpiarForm();", true);
                    }

                    if (objResultado[0].IdTramite > 0)
                    {
                        string script = "";
                        script = "pnlPopMotivosSuspender.Hide(); $('#myModal').modal({backdrop: 'static', keyboard: false}); ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    }
                }
                else
                {
                    string script = "";
                    script = "AlertaObservaciones();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            else
            {
                string script = "";
                script = "AlertaMotivosHold();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnDetener_Click(object sender, EventArgs e)
        {
            Session["TramitesAutomaticos"] = false;
            btnDetener.Attributes["disabled"] = "enabled";
            mensajes.MostrarMensaje(this, "Se ha detenido la asignación de tramites para la mesa actual.");
        }

        protected void btnCtrlPausarTramite_Click(object sender, EventArgs e)
        {
            Funciones.VariablesGlobales.StatusMesa IdStatusMesa = Funciones.VariablesGlobales.StatusMesa.Pausa;
            int IdTramite = Int32.Parse(hfIdTramite.Value);
            int TipoTramite = int.Parse(hfTipoTramite.Value.ToString());

            List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.ProcesarTramite(IdTramite, int.Parse(hfIdProceso.Value), manejo_sesion.Usuarios.IdUsuario, IdStatusMesa, txtObservacionesPublicasPausar.Text.Trim().Replace("'", ""), txtObservacionesPrivadas.Text.Trim().Replace("'", ""), "");
            if (!Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "PopupScript"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "LimpiarForm();", true);
            }

            if (objResultado[0].IdTramite > 0)
            {
                // REGISTRO DE ARCHIVOS - COLOCAR DESPUES DE ACTUALIZAR EL TRAMITE
                if (Session["documentos"] != null) { string resultadoExpediente = registraDocumentosExpediente(IdTramite, TipoTramite); }
                if (Session["insumos"] != null) { string resultadoInsumo = registraDocumentosInsumos(IdTramite, TipoTramite); }

                TramiteTerminado();
                Response.Redirect(EncripParametros("IdProceso=" + hfIdProceso.Value.ToString(), "TramiteTarea.aspx").URL, true);
                //Response.Redirect("TramiteTarea.aspx?IdProceso=" + hfIdProceso.Value.ToString(), true);
            }
        }

        protected void Rechazo(string MotivosRechazos)
        {
            if (txtObservacionesPublicasRechazara.Text.Length > 0)
            {
                Funciones.VariablesGlobales.StatusMesa IdStatusMesa = Funciones.VariablesGlobales.StatusMesa.Rechazo;
                int IdTramite = Int32.Parse(hfIdTramite.Value);
                int TipoTramite = int.Parse(hfTipoTramite.Value);

                List<Propiedades.Procesos.Operacion.TramiteProcesado> objResultado = Tramites.ProcesarTramite(IdTramite, int.Parse(hfIdProceso.Value), manejo_sesion.Usuarios.IdUsuario, IdStatusMesa, txtObservacionesPublicasRechazara.Text.Trim().Replace("'", ""), txtObservacionesPrivadas.Text.Trim().Replace("'", ""), MotivosRechazos);
                if (!Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "PopupScript"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "LimpiarForm();", true);
                }

                GeneraCartaEstatusTramite(IdTramite, "Rechazo", TipoTramite);

                if (objResultado[0].IdTramite > 0)
                {
                    // REGISTRO DE ARCHIVOS - COLOCAR DESPUES DE ACTUALIZAR EL TRAMITE
                    if (Session["documentos"] != null) { string resultadoExpediente = registraDocumentosExpediente(IdTramite, TipoTramite); }
                    if (Session["insumos"] != null) { string resultadoInsumo = registraDocumentosInsumos(IdTramite, TipoTramite); }

                    string script = "";
                    script = "pnlPopMotivosRechazar.Hide(); $('#myModal').modal({backdrop: 'static', keyboard: false});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                    TituloModal.Text = "Operación realizada";
                    MensajeModal.Text = "Trámite rechazado";
                    TramiteTerminado();
                    //Response.Redirect("TramiteTarea.aspx?IdMesa=" + hfIdProceso.Value.ToString(), true);
                }
            }
            else
            {
                string script = "";
                script = "AlertaObservaciones();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnAplicarRechazar_Click(object sender, EventArgs e)
        {
            List<prop.MotivosSuspension> lsMotivosSuspension = _MotivosHold.SelecionarMotivos(int.Parse(Request.QueryString["IdMesa"].ToString()));
            lsMotivosSuspension.Where(MotivoSuspension => lsMotivosSuspension.FirstOrDefault(valor => MotivoSuspension.IdTramiteTipoRechazo == 4) != null);      // SELECT * FROM cat_Tramite_RechazosTipos;

            var nodos = treeListRechazar.GetSelectedNodes();
            int IdMotivoRechazo = 0;
            string MotivosRechazos = "";

            if (lsMotivosSuspension.Count > 0)
            {
                if (nodos.Count > 0)
                {
                    if (txtObservacionesPublicasRechazara.Text.Length > 0)
                    {
                        MotivosRechazos = "";
                        foreach (TreeListNode node in nodos)
                        {
                            IdMotivoRechazo = Convert.ToInt32(node.GetValue("id"));
                            if (MotivosRechazos.Length > 0)
                                MotivosRechazos += ",";
                            MotivosRechazos += IdMotivoRechazo.ToString();
                        }

                        treeListRechazar.UnselectAll();

                        Rechazo(MotivosRechazos);
                    }
                    else
                    {
                        string script = "";
                        script = "AlertaObservaciones();";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    }
                }
                else
                {
                    string script = "";
                    script = "AlertaMotivosRechazo();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            else
            {
                Rechazo(MotivosRechazos);
            }
        }

        protected void TramiteTerminado()
        {
            Session.Remove("documentos");
            Session.Remove("insumos");
            Session["documentos"] = null;
            Session["insumos"] = null;
        }


        protected void GeneraCartaEstatusTramite(int IdTramite, string Estatus, int TipoTramite)
        {
            WFO.Negocio.Procesos.Promotoria.Cartas cartas = new Negocio.Procesos.Promotoria.Cartas();

            string folio = LabelFolio.Text.ToString().Trim();
            string directorioTemporal = "";
            switch (Estatus)
            {
                case "Hold":
                    directorioTemporal = Server.MapPath("~") + "\\Cartas\\CartaHold_" + manejo_sesion.Usuarios.IdUsuario + "_" + folio + ".pdf";
                    if (System.IO.File.Exists(directorioTemporal))
                    {
                        File.Delete(directorioTemporal);
                        cartas.CartaHoldPDF(IdTramite, Response, 1, manejo_sesion.Usuarios.IdUsuario);
                    }
                    else
                    {
                        cartas.CartaHoldPDF(IdTramite, Response, 1, manejo_sesion.Usuarios.IdUsuario);
                    }
                    break;
                case "Rechazo":
                    directorioTemporal = Server.MapPath("~") + "\\Cartas\\CartaRechazo_" + manejo_sesion.Usuarios.IdUsuario + "_" + folio + ".pdf";
                    if (System.IO.File.Exists(directorioTemporal))
                    {
                        File.Delete(directorioTemporal);
                        cartas.CartaRechazoPDF(IdTramite, Response, 1, manejo_sesion.Usuarios.IdUsuario);
                    }
                    else
                    {
                        cartas.CartaRechazoPDF(IdTramite, Response, 1, manejo_sesion.Usuarios.IdUsuario);
                    }
                    break;
                case "Suspendido":
                    directorioTemporal = Server.MapPath("~") + "\\Cartas\\CartaSuspendido_" + manejo_sesion.Usuarios.IdUsuario + "_" + folio + ".pdf";
                    if (System.IO.File.Exists(directorioTemporal))
                    {
                        File.Delete(directorioTemporal);
                        cartas.CartaSuspendidoPDF(IdTramite, Response, 1, manejo_sesion.Usuarios.IdUsuario);
                    }
                    else
                    {
                        cartas.CartaSuspendidoPDF(IdTramite, Response, 1, manejo_sesion.Usuarios.IdUsuario);
                    }
                    break;
            }

            registraDocExp(IdTramite, directorioTemporal, TipoTramite);

        }

        private string registraDocExp(int pIdTramite, string directorioCarta, int TipoTramite)
        {
            string msgError = "";
            List<string> lstArchivos = new List<string>();

            lstArchivos.Add(directorioCarta);

            promotoria.expediente expediente = new promotoria.expediente();
            string directorioTemporal = Server.MapPath("~") + expediente.CarpetaArchivada;

            // CONSULTA ID DEL EXPEDIENTE FUCIONADO
            List<promotoria.expediente> expedientes = archivos.ConsultaExpediente(pIdTramite, TipoTramite);
            string ArchFusionAnt = "";
            int Id_Expediente = 0;
            if (expedientes.Count > 0)
            {
                ArchFusionAnt = directorioTemporal + expedientes[0].NmArchivo;
                Id_Expediente = expedientes[0].Id;
            }

            List<promotoria.control_archivos> control_Archivos = archivos.ControlArchivoNuevoID();
            int IdControlArchivo = control_Archivos[0].Id;
            string nombreFusion = directorioTemporal + IdControlArchivo.ToString().PadLeft(12, '0') + ".pdf";
            if (File.Exists(nombreFusion))
            {
                File.Delete(nombreFusion);
            }

            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
            string nmSeparador = directorioTemporal + manejo_sesion.Usuarios.IdUsuario + ".pdf";
            string nmLogo = Server.MapPath("~\\Imagenes") + @"\logo_sep.png";


            msgError = WFO.Funciones.ManejoArchivos.Adiciona(lstArchivos, ArchFusionAnt, nombreFusion, manejo_sesion.Usuarios.Nombre, nmSeparador, nmLogo);

            if (string.IsNullOrEmpty(msgError))
            {
                archivos.ModificarExpedienteFusion(Id_Expediente);
                archivos.Agregar_Expedientes_Tramite(TipoTramite, pIdTramite, IdControlArchivo, IdControlArchivo.ToString().PadLeft(12, '0') + ".pdf", "Archivo Fucion Agrgacion", 1, 1, "");
                msgError = "";
            }
            else
            {
                Mensajes.Text = msgError;
            }

            return msgError;
        }


        #region "Eventos Controles Botones Status Trámites"

        protected void treeList_DataBoundHold(object sender, EventArgs e)
        {
            SetNodeSelectionSettings(ref treeListHold);
        }
        protected void treeList_CustomDataCallbackHold(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomDataCallbackEventArgs e)
        {
            // e.Result = treeListHold.SelectionCount.ToString();
        }
        protected void pnlCallbackMotHold_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
        }
        protected void pnlCallbackCancelar_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {

        }

        protected void treeList_DataBoundSuspender(object sender, EventArgs e)
        {
            SetNodeSelectionSettings(ref treeListSuspender);
        }

        protected void treeList_DataBoundRechazo(object sender, EventArgs e)
        {
            SetNodeSelectionSettings(ref treeListRechazar);
        }

        protected void treeList_CustomDataCallbackSuspender(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomDataCallbackEventArgs e)
        {
            // e.Result = treeListSuspender.SelectionCount.ToString();
        }

        protected void treeList_CustomDataCallbackRechazo(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomDataCallbackEventArgs e)
        {
            // e.Result = treeListSuspender.SelectionCount.ToString();
        }

        protected void pnlCallbackMotSuspender_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
        }

        protected void treeList_CustomDataCallbackCancelar(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomDataCallbackEventArgs e)
        {
            e.Result = treeListCancelar.SelectionCount.ToString();
        }

        protected void treeList_DataBoundCancelar(object sender, EventArgs e)
        {
            SetNodeSelectionSettings(ref treeListCancelar);
        }

        protected void treeList_DataBoundRechazar(object sender, EventArgs e)
        {
            SetNodeSelectionSettings(ref treeListSuspender);
        }
        protected void treeList_CustomDataCallbackRechazar(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomDataCallbackEventArgs e)
        {
            // e.Result = treeListSuspender.SelectionCount.ToString();
        }
        protected void pnlCallbackMotRechazar_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
        }

        private void SetNodeSelectionSettings(ref ASPxTreeList Motivos)
        {
            TreeListNodeIterator iterator = Motivos.CreateNodeIterator();
            TreeListNode node;
            while (true)
            {
                node = iterator.GetNext();
                if (node == null) break;
            }
        }

        #endregion

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