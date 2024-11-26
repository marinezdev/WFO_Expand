using DevExpress.Web;
using RFC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.Procesos.Promotoria
{
    public partial class NuevoPamSingNow : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Promotoria.Catalogos Catalogos = new Negocio.Procesos.Promotoria.Catalogos();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["Sesion"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
                int TipoTramite = 3;

                //cargarMonedas();
                formatoFechas();
                ListaTipoServicio(TipoTramite);
                //ListaProductos(TipoTramite);

                //cargarNacionalidadesCombo_db(ref txNacionalidad);
                //cargarNacionalidadesCombo_db(ref txTiNacionalidad);

                //cargarEstados_Combo(ref cboEstado);
                //cargarEstados_Combo(ref cboEstado2);

                cargaPromotoria(manejo_sesion.Usuarios.IdUsuario);

                // CAMPOS INAVILITADOS 
                texClave.Attributes["disabled"] = "disabled";
                texRegion.Attributes["disabled"] = "disabled";
                texGerenteComercial.Attributes["disabled"] = "disabled";
                texEjecuticoComercial.Attributes["disabled"] = "disabled";
                texEjecuticoFront.Attributes["disabled"] = "disabled";

                //lblProductoRamo.Text = "* Producto";
                //lblSubProductoRamo.Text = "* Sub Producto";
                //Tramite.Visible = true;
                //ActCPDES.Visible = true;
                //subproducto1.Visible = true;
                //LblOneShot.Visible = true;
                //chkOneShot.Visible = true;
                //LisSbproductos();

                //SumaAseguradaPolizasVigentes.Visible = true;
                //cboMoneda.Attributes.Remove("disabled");
                //cargarMonedas();


                if (Session["tramite"] != null)
                {
                    pintLimpiar();
                    prop.TramiteN1 oTramite = (prop.TramiteN1)Session["tramite"];
                    PostBack(oTramite);
                }
                else
                {
                    Session.Remove("tramite");
                }

            }
        }


        protected void Continuar(object sender, EventArgs e)
        {
            //double PrimaTotal = double.Parse(txtPrimaTotalGMM.Text.ToString());
            //int IdMoneda = int.Parse(cboMoneda.Text.ToString());
            //Double PrimaTotalConvertida = convertir(PrimaTotal, IdMoneda);

            // if (PrimaTotalConvertida < 10000.00)
            //     SumaBasica.Text = "La prima total de acuerdo a cotización no es mayor a 10,000.00 Pesos";
            //string RFC = "";
            //if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            //{
            //    RFC = txRfc.Text.ToString().Trim();
            //}
            //else
            //{
            //    RFC = txRfcMoral.Text.ToString().Trim();
            //}

            //string Evalua = Funciones.RFC.ValidaContinuidadRFC(cboTipoContratante.SelectedValue, RFC);
            //if (Evalua == "ok")
            //{
            // REALIZA LA RECOPILACION DE DATOS
            prop.TramiteN1 tramiteN1 = recuperaCaptura();
            Response.Redirect("anexaArchivosRes.aspx");
            //}
            //else
            //{
            //    Mensajes.Text = Evalua;
            //}
        }

        #region DATOS DE CARGA INICIAL
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

        private void ListaTipoServicio(int TipoTramite)
        {
            List<prop.cat_tipo_servicio> cat_tipo_servicio = Catalogos.cat_tipo_servicio_Seleccionar_PorTipo_tramite(TipoTramite);
            LisTipoServicio.DataSource = cat_tipo_servicio;
            LisTipoServicio.DataBind();
            LisTipoServicio.DataTextField = "Nombre";
            LisTipoServicio.DataValueField = "Id";
            LisTipoServicio.DataBind();
        }

        //private void ListaProductos(int TipoTramite)
        //{
        //    List<prop.cat_producto> cat_Productos = Catalogos.Cat_productos(TipoTramite);
        //    LisProducto.DataSource = cat_Productos;
        //    LisProducto.DataBind();
        //    LisProducto.DataTextField = "Nombre";
        //    LisProducto.DataValueField = "Id";
        //    LisProducto.DataBind();
        //}
        //private void LisSbproductos()
        //{
        //    List<prop.cat_subproducto> cat_Subproductos = Catalogos.Cat_subproductos(Funciones.Numeros.ConvertirTextoANumeroEntero(LisProducto.SelectedValue.ToString()));
        //    LisSubproducto1.DataSource = cat_Subproductos;
        //    LisSubproducto1.DataBind();
        //    LisSubproducto1.DataTextField = "Nombre";
        //    LisSubproducto1.DataValueField = "Id";
        //    LisSubproducto1.DataBind();
        //}

        //private void cargarMonedas()
        //{
        //    List<prop.cat_moneda> cat_monedas = Catalogos.Cat_Monedas();
        //    cboMoneda.DataSource = cat_monedas;
        //    cboMoneda.DataBind();
        //    cboMoneda.DataTextField = "Nombre";
        //    cboMoneda.DataValueField = "Id";
        //    cboMoneda.DataBind();

        //    cboMoneda.SelectedIndex = 0;
        //}

        protected void formatoFechas()
        {
            DateTime validateFechaSolicitud = DateTime.Today;

            dtFechaSolicitud.MaxDate = validateFechaSolicitud;
            dtFechaSolicitud.MinDate = validateFechaSolicitud.AddDays(-60);
            dtFechaSolicitud.UseMaskBehavior = true;
            dtFechaSolicitud.EditFormatString = GetFormatString("dd/MM/yyyy");
            dtFechaSolicitud.Date = DateTime.Today;

            //dtFechaConstitucion.MaxDate = DateTime.Today;
            //dtFechaConstitucion.UseMaskBehavior = true;
            //dtFechaConstitucion.EditFormatString = GetFormatString("dd/MM/yyyy");

            //dtFechaNacimiento.MaxDate = validateFechaSolicitud.AddYears(-18);
            //dtFechaNacimiento.UseMaskBehavior = true;
            //dtFechaNacimiento.EditFormatString = GetFormatString("dd/MM/yyyy");

            //dtFechaNacimientoTitular.UseMaskBehavior = true;
            //dtFechaNacimientoTitular.EditFormatString = GetFormatString("dd/MM/yyyy");

            //dtFechaConstitucion.Date = DateTime.Today;
            //dtFechaNacimiento.Date = DateTime.Today.AddYears(-18);
            //dtFechaNacimientoTitular.Date = DateTime.Today;
        }

        protected string GetFormatString(object value)
        {
            return value == null ? string.Empty : value.ToString();
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

        private void cargarEstados_Combo(ref DropDownList objDDL)
        {
            List<prop.cat_estados> cat_estados = Catalogos.Cat_Direcciones_Estados();
            objDDL.DataSource = cat_estados;
            objDDL.DataTextField = "Estado";
            objDDL.DataValueField = "Id";
            objDDL.DataBind();
            objDDL.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
        }

        private void pintLimpiar()
        {
            if (Session["URL"] != null)
            {
                Limpiar.Visible = true;
            }
        }
        #endregion

        #region EVENTOS POR INTERFAZ
        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            //double SumaAseguradaBasica = double.Parse(txtSumaAseguradaBasica.Text.ToString());
            //int IdMoneda = int.Parse(cboMoneda.Text.ToString());
            //Double SumaAseguradaBasicaConvertida = convertir(SumaAseguradaBasica, IdMoneda);

            //SumaBasica.Text = "";
            //if (SumaAseguradaBasicaConvertida < 10000.00)
            //{
            //    SumaBasica.Text = "la suma asegurada no es mayor a 10,000.00 Pesos";
            //}
            //else
            //{
            //    if (ValidantinuidadRFC())
            //    {
            string script = "";
            script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

            //Continuar();
            //    }
            //}
        }

        //protected void LisProducto1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LisSbproductos();
        //}

        //protected void LisSubproducto1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ValidaProductoSubProducto();
        //}
        //protected void ActividadCPDES_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CPDES();
        //}
        //protected void cboMoneda_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (GrandesSumas())
        //    {
        //        GrandeSumas.Text = "Grandes sumas";
        //        PrimaTotalGrandeSumas.Text = "";
        //    }
        //    else if (GrandesSumasPrimaTotal())
        //    {
        //        GrandeSumas.Text = "Grandes sumas";
        //        PrimaTotalGrandeSumas.Text = "";
        //    }
        //    else
        //    {
        //        GrandeSumas.Text = "";
        //        PrimaTotalGrandeSumas.Text = "";
        //    }
        //}

        //protected void cboTipoContratante_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    TipoContratante();
        //}

        //protected void LisTitNacionalidad_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //textNacionalidad.Text = "jajaja";
        //    textTitularNacionalidad.Text = "";
        //    textTitularNacionalidad.Text = ValidaPais(Funciones.Numeros.ConvertirTextoANumeroEntero(txTiNacionalidad.Value.ToString()));
        //}

        //protected void LisNacionalidad_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    textNacionalidad.Text = "";
        //    textNacionalidad.Text = ValidaPais(Funciones.Numeros.ConvertirTextoANumeroEntero(txNacionalidad.Value.ToString()));
        //}

        //protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckB2();
        //}
        //protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckB1();
        //}

        //protected void PrimaTotalGrandesSumas(object sender, EventArgs e)
        //{
        //    if (GrandesSumasPrimaTotal())
        //    {
        //        PrimaTotalGrandeSumas.Text = "Grandes sumas";
        //        GrandeSumas.Text = "";
        //    }
        //}

        //protected void CalculartSumaAsegurada(object sender, EventArgs e)
        //{
        //    if (GrandesSumas())
        //    {
        //        GrandeSumas.Text = "Grandes sumas";
        //        PrimaTotalGrandeSumas.Text = "";
        //    }
        //    else if (GrandesSumasPrimaTotal())
        //    {
        //        GrandeSumas.Text = "Grandes sumas";
        //        PrimaTotalGrandeSumas.Text = "";
        //    }
        //    else
        //    {
        //        GrandeSumas.Text = "";
        //        PrimaTotalGrandeSumas.Text = "";
        //    }
        //}

        //protected void dtFechaNacimiento_OnChanged(object sender, EventArgs e)
        //{
        //    string strValMsj = "";
        //    DateTime dtValor = DateTime.Today;
        //    if (dtFechaNacimiento.Text.Length > 0)
        //    {
        //        if (IsDate(dtFechaNacimiento.Text.Trim(), ref strValMsj, ref dtValor))
        //        {
        //            try
        //            {
        //                string strNombre = "";
        //                strNombre = removerAcentos(txNombre.Text.ToUpper().Trim());

        //                if (strNombre.Equals("MARIA") || strNombre.Equals("JOSE"))
        //                {
        //                    strNombre += "A";
        //                }

        //                ObtieneRFC rfc = new ObtieneRFC();
        //                txRfc.Text = rfc.RFC13Pocisiones(removerAcentos(txApPat.Text.ToUpper().Trim()), removerAcentos(txApMat.Text.ToUpper().Trim()), removerAcentos(strNombre), dtValor.ToString("yy/MM/dd"));
        //                //antecedentesRFC();
        //            }
        //            catch
        //            {
        //                txRfc.Text = "ERR000000AAA".ToUpper();
        //            }
        //        }
        //        else
        //        {
        //            dtFechaNacimiento.Text = "";
        //            dtFechaNacimiento.Focus();
        //            txRfc.Text = "";
        //        }
        //    }
        //}

        //protected void dtFechaConstitucion_OnChanged(object sender, EventArgs e)
        //{
        //    string strValMsj = "";
        //    DateTime dtValor = DateTime.Today;
        //    if (dtFechaConstitucion.Text.Length > 0)
        //    {
        //        if (IsDate(dtFechaConstitucion.Text.Trim(), ref strValMsj, ref dtValor))
        //        {
        //            try
        //            {
        //                string strMoral = removerAcentos(txNomMoral.Text.ToUpper().Trim());
        //                String[] arrPalabrasNo = { " EL ", " S DE RL ", " DE ", " LAS ", " DEL ", " COMPAÑÍA ", " SOCIEDAD ", " COOPERATIVA ", " S EN C POR A ", " S EN NC ", " PARA ", " POR ", " AL ", " E ", " SCL ", " SNC ", " OF ", " COMPANY ", " MC ", " VON ", " MI ", " SRL CV ", " SA MI ", " LA ", " SA DE CV ", " LOS ", " Y ", " SA ", " CIA ", " SOC ", " COOP ", " A EN P ", " S EN C ", " EN ", " CON ", " SUS ", " SC ", " SCS ", " THE ", " AND ", " CO ", " MAC ", " VAN ", " A ", " SA DE CV ", " COMPAÑÍA ", " COMPANÍA ", " DE ", " LA ", " LAS ", " MC ", " VON ", " DEL ", " LOS ", " Y ", " MAC ", " VAN ", " MI ", " SRL CV MI ", " SRL MI" };
        //                foreach (string strPalabra in arrPalabrasNo)
        //                {
        //                    strMoral = strMoral.Replace(strPalabra, " ");
        //                }

        //                String[] arrPalabras = strMoral.Split(' ');
        //                if (arrPalabras.Length > 3)
        //                {
        //                    strMoral = "";
        //                    strMoral += arrPalabras[0].ToString() + " ";
        //                    strMoral += arrPalabras[1].ToString() + " ";
        //                    strMoral += arrPalabras[2].ToString() + " ";
        //                }

        //                PersonaMoral moral = new PersonaMoral();
        //                txRfcMoral.Text = moral.RetornaLetrasFinalesRFC(strMoral, dtValor.ToString("yy/MM/dd"));
        //                //antecedentesRFC();
        //            }
        //            catch
        //            {
        //                txRfcMoral.Text = "ERR000000AAA".ToUpper();
        //            }
        //        }
        //        else
        //        {
        //            dtFechaConstitucion.Text = "";
        //            dtFechaConstitucion.Focus();
        //            txRfcMoral.Text = "";
        //        }
        //    }
        //}

        protected void BtnLimpiar(object sender, EventArgs e)
        {
            Session.Remove("tramite");
            Session.Remove("TamExpedinte");
            Session.Remove("documentos");

            string url = Session["URL"].ToString();
            Response.Redirect(url);
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        #endregion

        private void PostBack(prop.TramiteN1 oEmisionVidaGMM)
        {
            /***********************************************************************/
            /*****************   PRIMER BLOQUE - POLIZA / SEGURO   *****************/
            /***********************************************************************/

            //cboMoneda.SelectedValue = oEmisionVidaGMM.IdMoneda.ToString();
            //txtSumaAseguradaBasica.Text = oEmisionVidaGMM.SumaBasica.ToString();
            //txtPrimaTotal.Text = oEmisionVidaGMM.SumaPolizas.ToString();
            //if (GrandesSumas())
            //{
            //    PrimaTotalGrandeSumas.Text = "";
            //    GrandeSumas.Text = "Grandes sumas";
            //}

            //txtSumaAseguradaPolizasVigentes.Text = oEmisionVidaGMM.SumaPolizas.ToString();
            //txtPrimaTotal.Text = oEmisionVidaGMM.PrimaTotal.ToString();
            //resultado.SumaPolizas = txtSumaAseguradaPolizasVigentes.Text.ToString();
            //resultado.PrimaTotal = txtPrimaTotal.Text.ToString();
            //if (GrandesSumasPrimaTotal())
            //{
            //    GrandeSumas.Text = "";
            //    PrimaTotalGrandeSumas.Text = "Grandes sumas";
            //}

            //LisProducto.SelectedValue = oEmisionVidaGMM.IdProducto.ToString();
            //LisSbproductos();
            //LisSubproducto1.SelectedValue = oEmisionVidaGMM.IdSubProducto.ToString();
            //ActividadCPDES.SelectedValue = oEmisionVidaGMM.CPDES.ToString();
            //CPDES();
            //textFolioCPDES.Text = oEmisionVidaGMM.FolioCPDES.ToString();
            //EstatusCPDES.SelectedValue = oEmisionVidaGMM.EstatusCPDES.ToString();

            //if (oEmisionVidaGMM.HombreClave == 1)
            //{
            //    HombresClave.Checked = true;
            //}


            //chkOneShot.Checked = oEmisionVidaGMM.OneShot;

            /***********************************************************************/
            /********************   INFORMACION DE LA POLIZA    ********************/
            /***********************************************************************/
            LisTipoServicio.SelectedValue = oEmisionVidaGMM.IdTipoServicio.ToString();

            dtFechaSolicitud.Text = oEmisionVidaGMM.FechaSolicitud.ToString();
            textNumeroOrden.Text = oEmisionVidaGMM.NumeroOrden.ToString();

            //if (oEmisionVidaGMM.TipoPersona == 1)
            //{
            //    cboTipoContratante.SelectedValue = "Fisica";
            //}
            //else
            //{
            //    cboTipoContratante.SelectedValue = "Moral";
            //}

            //cboTipoContratante.SelectedValue = oEmisionVidaGMM.TipoPersona.ToString();

            //TipoContratante();
            //ValidaProductoSubProducto();
            //if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            //{
            txNombre.Text = oEmisionVidaGMM.Nombre.ToString();
            txApPat.Text = oEmisionVidaGMM.ApPaterno.ToString();
            txApMat.Text = oEmisionVidaGMM.ApMaterno.ToString();
            //txNacionalidad.SelectedItem.Value = oEmisionVidaGMM.IdNacionalidad;
            //dtFechaNacimiento.Text = oEmisionVidaGMM.FechaNacimiento.ToString();
            //txSexo.SelectedValue = oEmisionVidaGMM.Sexo.ToString();
            //txRfc.Text = oEmisionVidaGMM.RFC.ToString();
            //cboEstado.SelectedValue = oEmisionVidaGMM.EntidadFederativa.ToString();
            //}
            //else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            //{
            //    txNomMoral.Text = oEmisionVidaGMM.Nombre.ToString();
            //    dtFechaConstitucion.Text = oEmisionVidaGMM.FechaConst.ToString();
            //    txRfcMoral.Text = oEmisionVidaGMM.RFC.ToString();
            //    txTiNacionalidad.Value = oEmisionVidaGMM.IdTitularNacionalidad;
            //}

            //if (oEmisionVidaGMM.TitularNombre.ToString() != "")
            //{
            //    CheckBox1.Checked = true;
            //    CheckB1();
            //    txTiNombre.Text = oEmisionVidaGMM.TitularNombre.ToString();
            //    txTiApPat.Text = oEmisionVidaGMM.TitularApPat.ToString();
            //    txTiApMat.Text = oEmisionVidaGMM.TitularApMat.ToString();
            //    txTiNacionalidad.SelectedItem.Value = oEmisionVidaGMM.TitularNacionalidad.ToString();
            //    txtSexoM.SelectedValue = oEmisionVidaGMM.TitularSexo.ToString();
            //    dtFechaNacimientoTitular.Text = oEmisionVidaGMM.TitularFechaNacimiento.ToString();
            //    cboEstado2.SelectedValue = oEmisionVidaGMM.TitularEntidad.ToString();
            //}

            txDetalle.Text = oEmisionVidaGMM.Observaciones.ToString();
            //antecedentesRFC();

            //textNacionalidad.Text = "";
            //textNacionalidad.Text = ValidaPais(Funciones.Numeros.ConvertirTextoANumeroEntero(txNacionalidad.Value.ToString()));

            //textTitularNacionalidad.Text = "";
            //textTitularNacionalidad.Text = ValidaPais(Funciones.Numeros.ConvertirTextoANumeroEntero(txTiNacionalidad.Value.ToString()));

        }

        //private void ValidaProductoSubProducto()
        //{
        //    // VALORES SELCCIONADOS
        //    string Producto = "";
        //    string SubProducto = "";

        //    Producto = LisProducto.SelectedItem.Text.Trim();
        //    //SubProducto = LisSubproducto1.SelectedItem.Text.Trim();

        //    if (Producto == "METALIFE")
        //    {
        //        if (SubProducto == "RETIRO")
        //        {
        //            // DESABILIAR FORMULARIO 
        //            cboTipoContratante.SelectedValue = "Fisica";
        //            TipoContratante();
        //            cboTipoContratante.Attributes["disabled"] = "enabled";
        //            CheckBox2.Checked = true;
        //            CheckBox1.Checked = false;
        //            CheckB2();
        //            CheckBox2.Enabled = false;
        //            CheckBox1.Enabled = false;
        //        }
        //        else
        //        {
        //            // HABILITAR FORMULARIO
        //            TipoContratante();
        //            cboTipoContratante.Attributes.Remove("disabled");
        //            CheckBox2.Enabled = true;
        //            CheckBox1.Enabled = true;
        //        }
        //    }
        //    else
        //    {
        //        // HABILITAR FORMULARIO
        //        TipoContratante();
        //        cboTipoContratante.Attributes.Remove("disabled");
        //        CheckBox2.Enabled = true;
        //        CheckBox1.Enabled = true;
        //    }
        //}

        //protected bool ValidantinuidadRFC()
        //{
        //    bool resultado = false;
        //    Mensajes.Text = "";

        //    if (cboTipoContratante.SelectedValue.Equals("Fisica"))
        //    {
        //        string FiscoRFC = txRfc.Text.ToString().Trim();
        //        if (FiscoRFC != "" && FiscoRFC != null)
        //        {
        //            if (FiscoRFC.Length == 13)
        //            {
        //                Regex Val = new Regex(@"[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?");
        //                if (Val.IsMatch(FiscoRFC))
        //                {
        //                    resultado = true;
        //                }
        //                else
        //                {
        //                    Mensajes.Text = "RFC Persona Física Inválido ";
        //                }
        //            }
        //            else
        //            {
        //                Mensajes.Text = "El RFC No Contiene 13 Caracteres ";
        //            }
        //        }
        //        else
        //        {
        //            Mensajes.Text = "Coloca el RFC de la Persona Física";
        //        }
        //    }
        //    else if (cboTipoContratante.SelectedValue.Equals("Moral"))
        //    {
        //        string MoralRFC = txRfcMoral.Text.ToString().Trim();
        //        if (MoralRFC != "" && MoralRFC != null)
        //        {
        //            if (MoralRFC.Length == 12)
        //            {
        //                Regex Val = new Regex(@"^[a-zA-Z]{3,4}(\d{6})((\D|\d){3})?$");
        //                if (Val.IsMatch(MoralRFC))
        //                {
        //                    resultado = true;
        //                }
        //                else
        //                {
        //                    Mensajes.Text = "RFC Persona Moral Inválido ";
        //                }
        //            }
        //            else
        //            {
        //                Mensajes.Text = "El RFC No Contiene 12 Caracteres ";
        //            }
        //        }
        //        else
        //        {
        //            Mensajes.Text = "Coloca el RFC Moral ";
        //        }
        //    }
        //    return resultado;
        //}

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

        //protected void CheckB2()
        //{
        //    if (CheckBox2.Checked.Equals(true))
        //    {
        //        CheckBox1.Checked = false;
        //        CheckB1();
        //    }
        //}
        //protected void CheckB1()
        //{
        //    if (CheckBox1.Checked.Equals(true))
        //    {
        //        CheckBox2.Checked = false;
        //        DiferenteContratante.Visible = true;
        //    }
        //    else if (CheckBox1.Checked.Equals(false))
        //    {
        //        DiferenteContratante.Visible = false;
        //    }
        //    else
        //    {
        //        DiferenteContratante.Visible = false;
        //    }
        //}

        //protected void TipoContratante()
        //{
        //    //////////////// GrandeSumas.Text = "Grandes sumas";
        //    if (cboTipoContratante.SelectedValue.Equals("Fisica"))
        //    {
        //        pnPrsFisica.Visible = true;
        //        pnPrsMoral.Visible = false;
        //        CheckBox1.Enabled = true;
        //        CheckBox2.Enabled = true;
        //        // edos.SeleccionarDependencias_DropDrownList(ref cboEstado);
        //    }
        //    else if (cboTipoContratante.SelectedValue.Equals("Moral"))
        //    {
        //        pnPrsMoral.Visible = true;
        //        pnPrsFisica.Visible = false;
        //        CheckBox1.Checked = true;
        //        CheckB1();
        //        CheckBox1.Enabled = false;
        //        CheckBox2.Enabled = false;
        //        // edos.SeleccionarDependencias_DropDrownList(ref cboEstado2);
        //    }
        //    else
        //    {
        //        pnPrsFisica.Visible = false;
        //        pnPrsMoral.Visible = false;
        //    }

        //}

        //protected void CPDES()
        //{
        //    if (ActividadCPDES.SelectedValue.Equals("True"))
        //    {
        //        CPDS.Visible = true;
        //    }
        //    else if (ActividadCPDES.SelectedValue.Equals("False"))
        //    {
        //        CPDS.Visible = false;
        //    }
        //    else
        //    {
        //        CPDS.Visible = false;
        //    }
        //}

        //protected void antecedentesRFC()
        //{
        //    TextantecedentesRFC.Text = "";
        //    textRFCFisica.Text = "";

        //    if (cboTipoContratante.SelectedValue.Equals("Fisica"))
        //    {
        //        string RFC = txRfc.Text.Trim().Replace("-", "");
        //        List<prop.TramiteN1> tramiteN1s = Catalogos.BustatramiteN1RFC(RFC);
        //        if (tramiteN1s[0].RFC == "1")
        //        {
        //            textRFCFisica.Text = "Ya existen trámites registrados para el RFC";
        //        }
        //    }
        //    else if (cboTipoContratante.SelectedValue.Equals("Moral"))
        //    {
        //        string RFC = txRfcMoral.Text.Trim().Replace("-", "");
        //        List<prop.TramiteN1> tramiteN1s = Catalogos.BustatramiteN1RFC(RFC);
        //        if (tramiteN1s[0].RFC == "1")
        //        {
        //            TextantecedentesRFC.Text = "Ya existen trámites registrados para el RFC";
        //        }
        //    }

        //}

        protected double Total(String SumaAsegurda, String TotalCotiacion, String Moneda)
        {
            if (TotalCotiacion.Length == 0)
                TotalCotiacion = "0";

            double Total = 0;
            double SumaAsegurada = double.Parse(SumaAsegurda);
            double TotalCotizacion = double.Parse(TotalCotiacion);
            int IdMoneda = int.Parse(Moneda);
            SumaAsegurada = convertir(SumaAsegurada, IdMoneda);
            TotalCotizacion = convertir(TotalCotizacion, IdMoneda);

            Total = SumaAsegurada + TotalCotizacion;

            return Total;
        }

        private double convertir(double numero, int IdMoneda)
        {
            double total = 0;
            List<prop.cat_moneda> cat_Monedas = Catalogos.BuscaMonedaId(IdMoneda);
            double Moneda = Convert.ToDouble(cat_Monedas[0].Valor);
            total = numero * Moneda;
            return total;
        }

        //protected bool GrandesSumas()
        //{
        //    bool resultado = false;
        //    // Limpia los mensajes 
        //    GrandeSumas.Text = "";
        //    // Compara el valor de la seleccion
        //    if (cboMoneda.SelectedValue != "-1" && txtSumaAseguradaBasica.Text.ToString() != "0.00")
        //    {
        //        // Monto total de la suma aseguada y prima total, apartir del tipo de moneda seleccionada
        //        Double MontoTotal = Total(txtSumaAseguradaBasica.Text.ToString(), txtSumaAseguradaPolizasVigentes.Text.ToString(), cboMoneda.Text.ToString());
        //        // Validacion del monto "MAL PROGRAMADO" el id del dolar es = 2, el regitro esta en SQL cat_moneda, 1500000 dolares
        //        // double ValidacionMonto = double.Parse("1500000");

        //        // RMF (versión 20201013_001)
        //        //double ValidacionMonto = double.Parse("6000000.00");
        //        //ValidacionMonto = convertir(ValidacionMonto, 1);

        //        double ValidacionMonto = double.Parse("1500000.00");
        //        ValidacionMonto = convertir(ValidacionMonto, 2);
        //        if (MontoTotal >= ValidacionMonto)
        //        {
        //            resultado = true;
        //        }
        //    }
        //    return resultado;
        //}
        //protected bool GrandesSumasPrimaTotal()
        //{
        //    bool resultado = false;
        //    PrimaTotalGrandeSumas.Text = "";
        //    if (cboMoneda.SelectedValue != "-1")
        //    {
        //        int IdMoneda = int.Parse(cboMoneda.Text.ToString());
        //        string Primatotal = txtPrimaTotal.Text.ToString();
        //        double Monto;
        //        if (Primatotal != null && Primatotal.Length == 0)
        //        {
        //            Monto = 0.00;
        //        }
        //        else
        //        {
        //            Monto = double.Parse(Primatotal);
        //        }

        //        Monto = convertir(Monto, IdMoneda);

        //        double ValidacionMonto = convertir(double.Parse("200000.00"), 1);
        //        if (Monto >= ValidacionMonto)
        //        {
        //            resultado = true;
        //        }
        //    }
        //    return resultado;
        //}

        public static bool IsDate(string inputDate, ref string strResultado, ref DateTime FechaValue)
        {
            bool isDate = true;
            try
            {
                FechaValue = DateTime.ParseExact(inputDate, "dd/MM/yyyy", null);

                if (FechaValue > DateTime.Today)
                {
                    strResultado = "La fecha no puede ser mayor al día de hoy.";
                    isDate = false;
                }
                else if (FechaValue < FechaValue.AddDays(-60))
                {
                    strResultado = "La fecha no puede ser menor a 60 días a partir del día de hoy.";
                    isDate = false;
                }
                else
                {
                    isDate = true;
                }

            }
            catch (Exception ex)
            {
                isDate = false;
                strResultado = ex.Message;
                Console.Write("Error al Validar la Fecha: " + ex.Message);
            }
            return isDate;
        }
        private static string removerAcentos(String texto)
        {
            string consignos = "áàäéèëíìïóòöúùuÁÀÄÉÈËÍÌÏÓÒÖÚÙÜçÇñÑ";
            string sinsignos = "aaaeeeiiiooouuuAAAEEEIIIOOOUUUcCnN";

            StringBuilder textoSinAcentos = new StringBuilder(texto.Length);
            int indexConAcento;
            foreach (char caracter in texto)
            {
                indexConAcento = consignos.IndexOf(caracter);
                if (indexConAcento > -1)
                    textoSinAcentos.Append(sinsignos.Substring(indexConAcento, 1));
                else
                    textoSinAcentos.Append(caracter);
            }
            return textoSinAcentos.ToString();
        }

        private prop.TramiteN1 recuperaCaptura()
        {
            prop.TramiteN1 NuevoTramite = new prop.TramiteN1();
            NuevoTramite.IdTipoTramite = 3;

            List<prop.promotoria_usuario> Promotoria_Usuarios = Catalogos.Promotoria_Usuarios(manejo_sesion.Usuarios.IdUsuario);
            NuevoTramite.IdPromotoria = Promotoria_Usuarios[0].Id;
            NuevoTramite.IdUsuario = manejo_sesion.Usuarios.IdUsuario;
            NuevoTramite.IdStatus = 1;

            NuevoTramite.IdTipoServicio = Convert.ToInt32(LisTipoServicio.SelectedValue);

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

            //NuevoTramite.CPDES = false;
            //NuevoTramite.FolioCPDES = "";
            //NuevoTramite.EstatusCPDES = "";
            //NuevoTramite.HombreClave = 0;
            //NuevoTramite.OneShot = false;
            NuevoTramite.idPrioridad = 5;

            //if (ActividadCPDES.SelectedValue.Equals("True"))
            //{
            //    NuevoTramite.CPDES = true;
            //    NuevoTramite.FolioCPDES = textFolioCPDES.Text.Trim();
            //    NuevoTramite.EstatusCPDES = EstatusCPDES.Text.Trim();
            //}
            //if (HombresClave.Checked.Equals(true))
            //{
            //    NuevoTramite.HombreClave = 1;
            //    NuevoTramite.idPrioridad = 4;
            //}

            //// PRIORIDAD DE TRAMITE
            //if (GrandesSumas())
            //{
            //    NuevoTramite.idPrioridad = 2;
            //}
            //else if (GrandesSumasPrimaTotal())
            //{
            //    NuevoTramite.idPrioridad = 3;
            //}


            //if (chkOneShot.Checked.Equals(true))
            //{
            //    NuevoTramite.OneShot = true;
            //}

            //if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            //{
            NuevoTramite.TipoPersona = 1;
            NuevoTramite.Nombre = txNombre.Text.Trim();
            NuevoTramite.ApPaterno = txApPat.Text.Trim();
            NuevoTramite.ApMaterno = txApMat.Text.Trim();
            //NuevoTramite.RFC = txRfc.Text.Trim().ToUpper();
            //NuevoTramite.IdNacionalidad = Convert.ToInt32(txNacionalidad.Value);
            //NuevoTramite.FechaNacimiento = dtFechaNacimiento.Text.Trim();
            //NuevoTramite.Sexo = txSexo.SelectedValue.ToString();
            //NuevoTramite.EntidadFederativa = cboEstado.SelectedValue;
            //}
            //else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            //{
            //    NuevoTramite.TipoPersona = 2;
            //    NuevoTramite.Nombre = txNomMoral.Text.Trim();
            //    NuevoTramite.FechaConst = dtFechaConstitucion.Text.Trim();
            //    NuevoTramite.RFC = txRfcMoral.Text.Trim().ToUpper();
            //    NuevoTramite.Nacionalidad = txTiNacionalidad.Text.Trim();
            //}

            //if (CheckBox1.Checked.Equals(true))
            //{
            //    NuevoTramite.TitularNombre = txTiNombre.Text.Trim();
            //    NuevoTramite.TitularApPat = txTiApPat.Text.Trim();
            //    NuevoTramite.TitularApMat = txTiApMat.Text.Trim();
            //    NuevoTramite.IdTitularNacionalidad = Convert.ToInt32(txNacionalidad.Value);
            //    NuevoTramite.TitularFechaNacimiento = dtFechaNacimientoTitular.Text.Trim();
            //    NuevoTramite.TitularSexo = txtSexoM.SelectedValue.ToString();
            //    NuevoTramite.TitularEntidad = cboEstado2.SelectedValue;
            //}

            //NuevoTramite.IdProducto = Convert.ToInt32(LisProducto.SelectedValue);
            //NuevoTramite.IdSubProducto = Convert.ToInt32(LisSubproducto1.SelectedValue);
            NuevoTramite.idRamo = 2;
            NuevoTramite.IdSisLegados = "";
            NuevoTramite.kwik = "";

            NuevoTramite.Observaciones = txDetalle.Text.Trim();

            // NUEVOS DATOS DE AGREGACION
            NuevoTramite.NumeroOrden = textNumeroOrden.Text.Trim();
            NuevoTramite.FechaSolicitud = dtFechaSolicitud.Text;
            //NuevoTramite.SumaBasica = Convert.ToDouble(txtSumaAseguradaBasica.Text);


            //NuevoTramite.SumaPolizas = Convert.ToDouble(txtSumaAseguradaPolizasVigentes.Text);
            //NuevoTramite.PrimaTotal = Convert.ToDouble(txtPrimaTotal.Text);

            //NuevoTramite.IdMoneda = Convert.ToInt32(cboMoneda.SelectedValue);

            Session["tramite"] = NuevoTramite;

            string cadena = HttpContext.Current.Request.Url.AbsoluteUri;
            string[] Separado = cadena.Split('/');
            string Final = Separado[Separado.Length - 1];
            Session["URL"] = Final;

            return NuevoTramite;
        }
    }
}