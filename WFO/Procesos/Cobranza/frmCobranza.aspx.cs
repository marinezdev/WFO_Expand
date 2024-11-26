using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.Globalization;
using System.Text;
using System.IO;

namespace WFO.Procesos.Cobranza
{
    public partial class frmCobranza : WFO.Utilerias.Comun
    {
        public DataTable dt;

        #region Eventos ***************************************************************************************************************************************

        protected void Page_Init(object sender, EventArgs e)
        {
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["folio"] != null && Request["estado"] != null)
                ProcesarDetalleFoliado(Request["folio"].ToString(), Request["estado"].ToString());
            else
                trSubirExcelDependencia.Visible = true;

            if (!IsPostBack)
            {
                txtFecha.Text = DateTime.Now.ToShortDateString();
                if (Request["folio"] != null)
                {
                    //Cargar los registros relacionados con el no. de folio (si los hubiere)
                    //CargarTablaPorFolio(Request["folio"].ToString(), Request["cobertura"].ToString());
                    lblFolio.Text = "<strong>Folio:</strong> " + Request["folio"];
                }
            }
        }

        protected void txtNoPoliza_TextChanged(object sender, EventArgs e)
        {
            cob.NombreCliente(txtNoPoliza.Text, ref txtCliente);
        }


        //botones básica
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            //Se cancelan los registros que corresponden al folio

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //pd.Eliminar(Request["folio"].ToString());

                //ad.Eliminar(Request["folio"].ToString());
                //Cambiar el estado a 3 del proceso a reenviado
                //ad.ActualizarErrores(Request["folio"].ToString(), txtErrores.Text);
                //ad.ActualizarAsunto(Request["folio"].ToString(), ASPxHtmlEditor1.Html);
                cob.AgregarEstadoDependencia(Request["folio"].ToString(), "2");
                cob.ActualizarEstado(Request["folio"].ToString(), "2");
                mensajes.MostrarMensaje(this, "Se enviaron las observaciones de errores a la dependencia con el folio no. " + Request["folio"], "Default.aspx");

            }
            else
            {
                //Permanece igual sin cambios.
            }
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            //guardar los datos del gridview a la base de datos

            if (rdbCobertura.SelectedValue == "1")
            {
                AgregarGridABDBasica(gvBasica);
                cob.AgregarEstadoDependencia(Request["folio"], "3"); //Solicitando recibo fiscal
                cob.ActualizarEstado(Request["folio"], "3");
                mensajes.MostrarMensaje(this, "Se solicita el recibo fiscal correspondiente.", "Default.aspx");
            }
            else if (rdbCobertura.SelectedValue == "2")
            {
                //ad.ActualizarAsunto(Request["folio"].ToString(), ASPxHtmlEditor1.Html);
                //ade.Agregar(Request["folio"], 3);   //Genera 100 posiciones, solicitud a analista front
                //ad.AgregarEstado(Request["folio"], 3);
                mensajes.MostrarMensaje(this, "Se guardaron los datos exitosamente.", "Default.aspx");
            }
        }

        protected void BtnTerminar_Click(object sender, EventArgs e)
        {
            //Terminado. entregado a Dependencia básica
            cob.AgregarEstadoDependencia(Request["folio"], "6");
            cob.ActualizarEstado(Request["folio"], "6");
            mensajes.MostrarMensaje(this, "Se enviaron los archivos a la Dependencia.", "Default.aspx");
        }

        //Botones potenciacion
        protected void BtnValidacion_Click(object sender, EventArgs e)
        {
            //Analista asae valida archivo (estado 3, si esta bien saldrá con estado 5)
            //Guardado de 100 posiciones pagos y cancelaciones
            if (gvPotenciacion.Rows.Count <= 0)
            {
                mensajes.MostrarMensaje(this, "Ningún dato para guardar");
                return;
            }

            string rtndr = string.Empty;
            foreach (GridViewRow gv in gvPotenciacion.Rows)
            {
                if (gv.Cells[0].Text.Contains("&nbsp;") || gv.Cells[1].Text.Contains("&nbsp;") || gv.Cells[2].Text.Contains("&nbsp;") ||
                    gv.Cells[3].Text.Contains("&nbsp;") || gv.Cells[4].Text.Contains("&nbsp;") || gv.Cells[5].Text.Contains("&nbsp;") ||
                    gv.Cells[6].Text.Contains("&nbsp;") || gv.Cells[7].Text.Contains("&nbsp;") || gv.Cells[8].Text.Contains("&nbsp;") ||
                    gv.Cells[9].Text.Contains("&nbsp;") ||
                    gv.Cells[10].Text.Contains("&nbsp;") || gv.Cells[11].Text.Contains("&nbsp;") || gv.Cells[12].Text.Contains("&nbsp;") ||
                    gv.Cells[13].Text.Contains("&nbsp;") || gv.Cells[14].Text.Contains("&nbsp;") || gv.Cells[15].Text.Contains("&nbsp;") ||
                    gv.Cells[16].Text.Contains("&nbsp;") || gv.Cells[17].Text.Contains("&nbsp;") || gv.Cells[18].Text.Contains("&nbsp;") ||
                    gv.Cells[19].Text.Contains("&nbsp;") || gv.Cells[20].Text.Contains("&nbsp;") || gv.Cells[21].Text.Contains("&nbsp;") ||
                    gv.Cells[22].Text.Contains("&nbsp;") || gv.Cells[23].Text.Contains("&nbsp;") || gv.Cells[24].Text.Contains("&nbsp;") ||
                    gv.Cells[25].Text.Contains("&nbsp;") || gv.Cells[26].Text.Contains("&nbsp;") || gv.Cells[27].Text.Contains("&nbsp;") ||
                    gv.Cells[28].Text.Contains("&nbsp;") || gv.Cells[29].Text.Contains("&nbsp;")
                    )
                    return;
                rtndr = gv.Cells[0].Text.Trim();
                cob.AgregarPolizaDetallePotenciacion(
                    txtNoPoliza.Text.Trim(), //Poliza
                    gv.Cells[0].Text.Trim(),    //Dependencia    
                    gv.Cells[1].Text.Trim(),    //Apaterno       
                    gv.Cells[2].Text.Trim(),    //AMaterno       
                    gv.Cells[3].Text.Trim(),    //Nombres        
                    gv.Cells[4].Text.Trim(),    //Fecha Nacimiento
                    gv.Cells[5].Text.Trim(),    //RFC            
                    gv.Cells[6].Text.Trim(),    //CURP           
                    gv.Cells[7].Text.Trim(),    //Sexo
                    gv.Cells[8].Text.Trim(),    //Código Entidad Federativa
                    gv.Cells[9].Text.Trim(),    //Código Municipio
                    gv.Cells[10].Text.Trim(),   //Nivel Tabular
                    gv.Cells[11].Text.Trim(),   //Percepcion Ordinaria Bruta Mensual
                    gv.Cells[12].Text.Trim(),   //Eventual
                    gv.Cells[13].Text.Trim(),   //Apellido Paterno Asegurado 
                    gv.Cells[14].Text.Trim(),   //Apellido Materno Asegurado 
                    gv.Cells[15].Text.Trim(),   //Nombres Asegurado          
                    gv.Cells[16].Text.Trim(),   //Fecha Nacimiento Asegurado
                    gv.Cells[17].Text.Trim(),   //CURP Asegurado             
                    gv.Cells[18].Text.Trim(),   //Sexo Asegurado
                    gv.Cells[19].Text.Trim(),   //Fecha Afiliacion asegurado
                    gv.Cells[20].Text.Trim(),   //Tipo Asegurado
                    gv.Cells[21].Text.Trim(),   //Fecha Ingreso a la Colectividad
                    gv.Cells[22].Text.Trim(),   //Suma Asegurada Basica
                    gv.Cells[23].Text.Trim(),   //Suma Asegurada Potenciada
                    gv.Cells[24].Text.Trim(),   //Suma Asegurada total
                    gv.Cells[25].Text.Trim(),   //Prima potenciada quincenal a reportar
                    gv.Cells[26].Text.Trim(),   //Forma de pago
                    gv.Cells[27].Text.Trim(),   //Monto de ajuste en la prima potenciada
                    gv.Cells[28].Text.Trim(),   //Importe total a pagar de la prima potenciada
                    gv.Cells[29].Text.Trim(),   //Fecha de antigüedad del asegurado en la suma asegurada            
                    "2",
                    Request["folio"],
                    ddlTrimestreQuincena.SelectedValue,
                    ddlAnn.SelectedValue
                    );
            }

            //Guardar los asegurados titulares
            foreach (GridViewRow gv in gvPotenciacion.Rows)
            {
                if (gv.Cells[0].Text.Contains("&nbsp;") || gv.Cells[1].Text.Contains("&nbsp;") || gv.Cells[2].Text.Contains("&nbsp;") ||
                    gv.Cells[3].Text.Contains("&nbsp;") || gv.Cells[4].Text.Contains("&nbsp;") || gv.Cells[5].Text.Contains("&nbsp;") ||
                    gv.Cells[6].Text.Contains("&nbsp;") || gv.Cells[7].Text.Contains("&nbsp;") || gv.Cells[8].Text.Contains("&nbsp;") ||
                    gv.Cells[9].Text.Contains("&nbsp;") || gv.Cells[10].Text.Contains("&nbsp;") || gv.Cells[11].Text.Contains("&nbsp;") ||
                    gv.Cells[12].Text.Contains("&nbsp;")
                    )
                    return;

                string dep = cob.ObtenerIdDependencia(gv.Cells[0].Text.Trim());    //Dependencia 
                cob.AgregarAseguradoTitular(
                    txtNoPoliza.Text.Trim(), //Poliza
                    dep,                        //Dependencia    
                    gv.Cells[1].Text.Trim(),    //Apaterno       
                    gv.Cells[2].Text.Trim(),    //AMaterno       
                    gv.Cells[3].Text.Trim(),    //Nombres        
                    gv.Cells[4].Text.Trim(),    //Fecha Nacimiento
                    gv.Cells[5].Text.Trim(),    //RFC            
                    gv.Cells[6].Text.Trim(),    //CURP           
                    gv.Cells[7].Text.Trim(),    //Sexo
                    gv.Cells[8].Text.Trim(),    //Código Entidad Federativa
                    gv.Cells[9].Text.Trim(),    //Código Municipio
                    gv.Cells[10].Text.Trim(),   //Nivel Tabular
                    gv.Cells[11].Text.Trim(),   //Percepcion Ordinaria Bruta Mensual
                    gv.Cells[12].Text.Trim()    //Eventual
                    );
            }

            //Guardar los coasegurados
            foreach (GridViewRow gv in gvPotenciacion.Rows)
            {
                if (gv.Cells[6].Text.Contains("&nbsp;") || gv.Cells[13].Text.Contains("&nbsp;") || gv.Cells[14].Text.Contains("&nbsp;") ||
                    gv.Cells[15].Text.Contains("&nbsp;") || gv.Cells[16].Text.Contains("&nbsp;") || gv.Cells[17].Text.Contains("&nbsp;") ||
                    gv.Cells[18].Text.Contains("&nbsp;") || gv.Cells[19].Text.Contains("&nbsp;") || gv.Cells[20].Text.Contains("&nbsp;") ||
                    gv.Cells[21].Text.Contains("&nbsp;") || gv.Cells[6].Text.Contains("&nbsp;")
                    )
                    return;

                string id = cob.ObtenerIdAseguradoTitular(gv.Cells[6].Text.Trim()); //CURP
                cob.AgregarCoasegurado(
                    gv.Cells[13].Text.Trim(),   //Apellido Paterno Asegurado 
                    gv.Cells[14].Text.Trim(),   //Apellido Materno Asegurado 
                    gv.Cells[15].Text.Trim(),   //Nombres Asegurado          
                    gv.Cells[16].Text.Trim(),   //Fecha Nacimiento Asegurado
                    gv.Cells[17].Text.Trim(),   //CURP Asegurado             
                    gv.Cells[18].Text.Trim(),   //Sexo Asegurado
                    gv.Cells[19].Text.Trim(),   //Fecha Afiliacion asegurado
                    gv.Cells[20].Text.Trim(),   //Tipo Asegurado
                    gv.Cells[21].Text.Trim(),   //Fecha Ingreso a la Colectividad
                    id,                         //Id Asegurado Titular
                    "1",                        //Estado
                    gv.Cells[6].Text.Trim(),    //CURP
                    ""                          //Certificado (pendiente)
                    );
            }


            string rutaArchivos = "../../ArchivosTemporales/";
            string sFileName1 = string.Empty;
            string sFileName2 = string.Empty;
            //Generar archivo 100 posiciones Pagos y Cancelaciones

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                var txtBuilder = new StringBuilder();
                int registros = 0;
                string retDep = cob.ObtenerRetenedorDependencia(rtndr);
                string retenedor = "";
                foreach (DataRow row in cob.SeleccionarCienPosicionesPagos(Request["folio"].ToString(), txtPeriodoReporte.Text, retDep).Rows)
                {
                    txtBuilder.AppendLine(row[0].ToString());
                    retenedor = row[0].ToString().Substring(1, 7);
                    registros++;
                }

                string suma = cob.CienPosicionesSumaPagos(Request["folio"].ToString()).Replace(".", "");
                int lognsuma = 12 - suma.Length;
                suma = suma.PadLeft(lognsuma + suma.Length, '0');

                int longcontados = 8 - registros.ToString().Length;
                string contados = registros.ToString().PadLeft(longcontados + registros.ToString().Length, '0');

                txtBuilder.AppendLine("2" + retenedor + "075P" + contados + suma);

                var txtContent = txtBuilder.ToString();
                var txtStream = new MemoryStream(Encoding.UTF8.GetBytes(txtContent));

                sFileName1 = "100PosicionesPagos.txt"; //System.IO.Path.GetRandomFileName();
                //string sGenName1 = "100PosicionesPagos.txt";

                using (StreamWriter SW = new StreamWriter(Server.MapPath(rutaArchivos + sFileName1)))
                {
                    SW.WriteLine(txtBuilder);
                    SW.Close();
                }

                if (cob.SeleccionarCienPosicionesCancelaciones(Request["folio"].ToString(), txtPeriodoReporte.Text, retDep).Rows.Count > 0)
                {
                    var txtBuilder2 = new StringBuilder();
                    int registros2 = 0;
                    string retenedor2 = "";
                    foreach (DataRow row2 in cob.SeleccionarCienPosicionesCancelaciones(Request["folio"].ToString(), txtPeriodoReporte.Text, retDep).Rows)
                    {
                        txtBuilder2.AppendLine(row2[0].ToString());
                        retenedor2 = row2[0].ToString().Substring(1, 7);
                        registros2++;
                    }

                    string suma2 = cob.CienPosicionesSumaCancelaciones(Request["folio"].ToString()).Replace(".", "");
                    int lognsuma2 = 12 - suma2.Length;
                    suma2 = suma2.PadLeft(lognsuma + suma.Length, '0');

                    int longcontados2 = 8 - registros2.ToString().Length;
                    string contados2 = registros2.ToString().PadLeft(longcontados2 + registros2.ToString().Length, '0');

                    txtBuilder2.AppendLine("2" + retenedor + "075C" + contados + suma);

                    var txtContent2 = txtBuilder.ToString();
                    var txtStream2 = new MemoryStream(Encoding.UTF8.GetBytes(txtContent));

                    sFileName2 = "100PosicionesCancelaciones.txt"; //System.IO.Path.GetRandomFileName();
                    //string sGenName2 = "100PosicionesPagos.txt";

                    StreamWriter SW2 = new StreamWriter(Server.MapPath(rutaArchivos + sFileName2));
                    SW2.WriteLine(txtBuilder2);
                    SW2.Close();
                }

                //Agregar los archivos
                cob.Agregar100PosicionesPagos(sFileName1, Request["folio"]);
                cob.Agregar100PosicionesCancelaciones(sFileName2, Request["folio"]);

                cob.AgregarEstadoDependencia(Request["folio"], "3");
                cob.ActualizarEstado(Request["folio"], "3");
                mensajes.MostrarMensaje(this, "Se guardaron los datos y se envió un mensaje al analista back para procesarlos.", "Default.aspx");
            }
        }

        protected void BtnAceptar01_Click(object sender, EventArgs e)
        {
            //Analista back revisa y envía a analista front
            cob.AgregarEstadoDependencia(Request["folio"], "5");
            cob.ActualizarEstado(Request["folio"], "5");
            mensajes.MostrarMensaje(this, "Se guardaron los datos y se envió un mensaje al analista front para procesarlos.", "Default.aspx");

        }

        protected void BtnAceptar02_Click(object sender, EventArgs e)
        {
            //Analista front revisa y reenvía a analista asae
            cob.AgregarEstadoDependencia(Request["folio"], "7");
            cob.ActualizarEstado(Request["folio"], "7");
            mensajes.MostrarMensaje(this, "Se guardaron los datos exitosamente.", "Default.aspx");
        }

        protected void BtnCancelar02_Click(object sender, EventArgs e)
        {
            //analista cancela por errores y reenvía a analista de back
            cob.AgregarEstadoDependencia(Request["folio"], "6");
            cob.ActualizarEstado(Request["folio"], "6");
            mensajes.MostrarMensaje(this, "Se envia mensaje a back para revisión.", "Default.aspx");
        }

        protected void BtnTerminar02_Click(object sender, EventArgs e)
        {
            //Terminan los procesos luego de una revisada por parte del analista asae
            cob.AgregarEstadoDependencia(Request["folio"], "8");
            cob.ActualizarEstado(Request["folio"], "8");
            mensajes.MostrarMensaje(this, "Se envia mensaje a Dependencia, fin del proceso.", "Default.aspx");
        }



        //Gridviews
        protected void gvArchivos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string carpeta = "../../ArchivosTemporales/";

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowObject = (DataRowView)e.Row.DataItem;
                HyperLink hlkNombresarchivos = (HyperLink)e.Row.FindControl("hlkNombresArchivos");

                hlkNombresarchivos.NavigateUrl = carpeta + rowObject["Archivo"].ToString();
            }
        }


        //Selectores
        protected void lnkQuitarArchivo_Click(object sender, EventArgs e)
        {
            LinkButton imgbtn1 = sender as LinkButton;
            GridViewRow row1 = imgbtn1.NamingContainer as GridViewRow;

            DataTable dt = new DataTable();
            dt = null;
            dt.Rows.RemoveAt(row1.RowIndex);
            ViewState["archivosAgregados"] = dt;

            ViewState["archivoTemporal"] = null;
            ViewState["archivoTemporal"] = ViewState["archivosAgregados"];

            string carpeta1 = "../../ArchivosTemporales/";
            carpeta1 = Server.MapPath(carpeta1);
            if (File.Exists(carpeta1 + imgbtn1.CommandArgument))
                File.Delete(carpeta1 + imgbtn1.CommandArgument);

            gvArchivos.DataSource = ViewState["archivosAgregados"];
            gvArchivos.DataBind();
        }

        protected void rdbCobertura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbCobertura.SelectedIndex == 0)
            {
                ddlTrimestreQuincena.Items.Clear();

                ViewState["cobertura"] = "ba";

                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("Seleccionar", "0"));
                items.Add(new ListItem("1er. Trimestre", "1"));
                items.Add(new ListItem("2do. Trimestre", "2"));
                items.Add(new ListItem("3er. Trimestre", "3"));
                items.Add(new ListItem("4to. Trimestre", "4"));
                ddlTrimestreQuincena.Items.AddRange(items.ToArray());

                lblTrimestreQuincena.Text = "Trimestre:";
            }
            else if (rdbCobertura.SelectedIndex == 1)
            {
                ddlTrimestreQuincena.Items.Clear();

                ViewState["cobertura"] = "po";

                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("Seleccionar", "0"));
                items.Add(new ListItem("1era.", "1"));
                items.Add(new ListItem("2da.", "2"));
                items.Add(new ListItem("3era.", "3"));
                items.Add(new ListItem("4ta.", "4"));
                items.Add(new ListItem("5ta.", "5"));
                items.Add(new ListItem("6ta.", "6"));
                items.Add(new ListItem("7ma.", "7"));
                items.Add(new ListItem("8va.", "8"));
                items.Add(new ListItem("9na.", "9"));
                items.Add(new ListItem("10ma.", "10"));
                items.Add(new ListItem("11va.", "11"));
                items.Add(new ListItem("12va.", "12"));
                items.Add(new ListItem("13va.", "13"));
                items.Add(new ListItem("14va.", "14"));
                items.Add(new ListItem("15va.", "15"));
                items.Add(new ListItem("16va.", "16"));
                items.Add(new ListItem("17va.", "17"));
                items.Add(new ListItem("18va.", "18"));
                items.Add(new ListItem("19na.", "19"));
                items.Add(new ListItem("20ma.", "20"));
                items.Add(new ListItem("21a.", "21"));
                items.Add(new ListItem("22da.", "22"));
                items.Add(new ListItem("23ra.", "23"));
                items.Add(new ListItem("24ta.", "24"));

                ddlTrimestreQuincena.Items.AddRange(items.ToArray());

                lblTrimestreQuincena.Text = "Quincena:";
            }
            trTrimestre.Visible = true;
        }

        protected void txtFoliovalidar_TextChanged(object sender, EventArgs e)
        {

        }


        //Subida de archivos
        protected void btnSubirExcel_Click(object sender, EventArgs e)
        {
            //Sube el archivo de excel por la dependencia

            if (!AsyncFileUpload1.HasFile)
            {
                return;
            }            
            if (AsyncFileUpload1.HasFile)
            {
                DataTable dt = new DataTable();
                string carpeta = "../../ArchivosTemporales/";
                carpeta = Server.MapPath(carpeta);
                string nombreArchivo = AsyncFileUpload1.FileName;
                AsyncFileUpload1.SaveAs(carpeta + nombreArchivo);

                if (manejo_sesion.Usuarios.IdRol == 6) //Dependencia
                {
                    //Si es nuevo y no tiene folio
                    string nofolio = string.Empty;
                    if (string.IsNullOrEmpty(Request["folio"]))
                    {
                        if (string.IsNullOrEmpty(txtFoliovalidar.Text))
                            nofolio = cob.GenerarFolio();
                        else
                            nofolio = txtFoliovalidar.Text;

                        //Guardar en bd
                        if (rdbCobertura.SelectedValue == "1")
                            cob.AgregarBasica(nombreArchivo, nofolio, txtNoPoliza.Text, rdbCobertura.SelectedValue, txtNombreSolicitante.Text, txtSolicitanteCorreo.Text, txtAsunto.Text, ddlTrimestreQuincena.SelectedValue, ddlAnn.SelectedValue);
                        else
                            cob.AgregarPotenciacion(nombreArchivo, nofolio, txtNoPoliza.Text, rdbCobertura.SelectedValue, txtNombreSolicitante.Text, txtSolicitanteCorreo.Text, txtAsunto.Text, ddlTrimestreQuincena.SelectedValue, ddlAnn.SelectedValue, manejo_sesion.Usuarios.IdRol.ToString());

                        cob.AgregarEstadoDependencia(nofolio, "1");

                        //Agregar el usuario de la dependencia
                        cob.AgregarDependenciaATramite(manejo_sesion.Usuarios.IdUsuario.ToString(), nofolio);
                        mensajes.MostrarMensaje(this, "Se guardaron los datos exitosamente. Número de Folio Asignado: " + nofolio, "Default.aspx");
                    }
                    else
                    {
                        mensajes.MostrarMensaje(this, "Se guardaron los datos exitosamente.", "Default.aspx");
                    }
                }
                else if (manejo_sesion.Usuarios.IdRol == 3) //analistas
                {

                }
                else if (manejo_sesion.Usuarios.IdRol == 8 || manejo_sesion.Usuarios.IdRol == 9) //analista back y front metlife
                {
                    cob.AgregarArchivo(Request["folio"], nombreArchivo);
                    cob.MostrarArchivosPorFolio_GridView(Request["folio"], ref gvArchivos);
                }
            }
        }

        protected void BtnSubirExcelParaValidar_Click(object sender, EventArgs e)
        {
            try
            {
                //Sube el archivo de excel por el analista para revisarse
                ExcelPackage pagina = new ExcelPackage(AsyncFileUpload2.FileContent);
                dt = Funciones.ManejoExcel.Excel_A_TablaDeDatos(pagina);

                if (dt.Columns.Count == 26 && rdbCobertura.SelectedValue == "2")
                {
                    mensajes.MostrarMensaje(this, "El archivo que intenta cargar no es de Cobertura Básica");
                    return;
                }
                else if (dt.Columns.Count == 30 && rdbCobertura.SelectedValue == "1")
                {
                    mensajes.MostrarMensaje(this, "El archivo que intenta cargar no es de Potenciación");
                    return;
                }

                if (rdbCobertura.SelectedValue == "1") //Basica
                {
                    CargarTablaBasica(gvBasica, dt);

                    ProcesarEncabezadoFijo(gvBasica);

                    BtnContinuar.Visible = true;
                    BtnCancelar.Visible = true;
                }
                else if (rdbCobertura.SelectedValue == "2") //Potenciada
                {
                    CargarTablaPotenciacion(gvPotenciacion, dt);

                    ProcesarEncabezadoFijo(gvPotenciacion);

                    BtnValidacion.Visible = true;
                    BtnCancelar.Visible = true;
                }
            }
            catch 
            {
                mensajes.MostrarMensaje(this, "Error al procesar el archivo, revise bien sus datos.");
            }
        }



        #endregion

        #region Metodos ***************************************************************************************************************************************

        protected void ProcesarEncabezadoFijo(GridView grid)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Key", "<script>HacerEncabezadoEstatico('" + grid.ClientID + "', 300, 1170, 80, true); </script>", false);
        }

        protected void CargarTablaBasica(GridView gridview, DataTable dt)
        {
            cob.TablaBasica(gridview, dt);
        }

        protected void CargarTablaPotenciacion(GridView gridview, DataTable dt)
        {
            cob.TablaPotenciacion(gridview, dt);
        }

        protected void CargarTablaPorFolio(string folio, string cobertura)
        {
            if (cobertura == "Básica")
            {
                //CargarTabla(gvAgregado, pd.SeleccionarPorId(folio));
                //Session["RegistrosTemporales"] = pd.SeleccionarPorId(folio);
                ProcesarEncabezadoFijo(gvBasica);
            }
            else if (cobertura == "Potenciada")
            {
                //CargarTabla(gvPotenciacion, pd.SeleccionarPorId(folio));
                //Session["RegistrosTemporales"] = pd.SeleccionarPorId(folio);
                ProcesarEncabezadoFijo(gvPotenciacion);
            }
        }

        protected void ProcesarDetalleFoliado(string folio, string estado)
        {
            //Ejecuta una operación dependiendo del rol

            string rutaArchivos = "../../ArchivosTemporales/";

            if (!string.IsNullOrEmpty(folio))
            {
                //Obtener el detalle del registro

                dt = cob.DetalleFolio(folio);

                txtNoPoliza.Text                    = dt.Rows[0]["Poliza"].ToString();
                txtNoPoliza_TextChanged(null, null);
                txtFecha.Text                       = dt.Rows[0]["Fecha"].ToString();
                txtNombreSolicitante.Text           = dt.Rows[0]["SubidoPor"].ToString();
                txtSolicitanteCorreo.Text           = dt.Rows[0]["Correo"].ToString();
                txtAsunto.Text                      = dt.Rows[0]["Asunto"].ToString();

                rdbCobertura.SelectedValue = dt.Rows[0]["Cobertura"].ToString();
                rdbCobertura_SelectedIndexChanged(null, null);
                

                if (dt.Rows[0]["Cobertura"].ToString() == "1")
                    ddlTrimestreQuincena.SelectedValue = dt.Rows[0]["Trimestre"].ToString();
                else if (dt.Rows[0]["Cobertura"].ToString() == "2")
                    ddlTrimestreQuincena.SelectedValue = dt.Rows[0]["Quincena"].ToString();

                ddlAnn.SelectedValue = dt.Rows[0]["Ann"].ToString();

                if (dt.Rows[0]["Cobertura"].ToString() == "1")
                {
                    //Básica
                    switch (estado)
                    {
                        case "1":   //Inicio Dependencia->Analista
                            trSubirExcelDependencia.Visible = false;
                            trSubirExcelParaValidar.Visible = true;
                            trArchivoExcel.Visible = true;
                            cob.MostrarArchivosPorFolio_GridView(folio, ref gvArchivos);
                            break;
                        case "2": //Revisión analista (analista->Dependencia ó analista->back)
                            break;
                        case "3": //Procesado normal (back->analista)
                                  //trArchivoExcel.Visible = true;
                            cob.MostrarArchivosPorFolio_GridView(folio, ref gvArchivos);

                            //trErrores.Visible = false;

                            break;
                        case "4": //Vuelto a procesar (dependencia->analista)
                            trArchivoExcel.Visible = true;
                            trSubirExcelParaValidar.Visible = true;


                            trNoFolioExiste.Visible = false;
                            //trSubirExcel.Visible = false;
                            //trErrores.Visible = false;
                            break;
                        case "5":
                            BtnContinuar.Visible = false;
                            BtnTerminar.Visible = true;
                            BtnCancelar.Visible = true;

                            //trSubirExcel.Visible = false;
                            trSubirExcelParaValidar.Visible = false;
                            trArchivoExcel.Visible = true;

                            gvBasica.Visible = false;
                            gvPotenciacion.Visible = false;

                            dvEspacioPDF.Visible = true;
                            ltMuestraPdf.Text = "";
                            ltMuestraPdf.Text = "<embed type='application/pdf' height='100%' width='100%' src='" + rutaArchivos + dt.Rows[0]["ArchivoPDF"] + "'></embed>";
                            break;
                        case "6":   //Concluído (analista->dependencia)
                                    //trSubirExcel.Visible = false;
                            trSubirExcelParaValidar.Visible = false;
                            BtnContinuar.Visible = false;
                            break;
                    }
                }
                else if (dt.Rows[0]["Cobertura"].ToString() == "2")
                {
                    //potenciada
                    rdbCobertura.SelectedIndex = 1;
                    rdbCobertura_SelectedIndexChanged(null, null);
                    ddlTrimestreQuincena.SelectedValue = dt.Rows[0][15].ToString();
                    ddlAnn.SelectedValue = dt.Rows[0][16].ToString();
                    trArchivoExcel.Visible = true;
                    //BtnValidacion.Visible = true;

                    switch (estado)
                    {
                        case "1":   //Revisión analista
                            cob.MostrarArchivosPorFolio_GridView(folio, ref gvArchivos);
                            trSubirExcelParaValidar.Visible = true;
                            break;
                        case "2":   //Devuelto Errores (dependencia->analista)
                            cob.MostrarArchivosPorFolio_GridView(folio, ref gvArchivos);
                            break;
                        case "3":   //Procesamiento analista back
                            trSubirExcelDependencia.Visible = true;

                            cob.MostrarArchivosPorFolio_GridView(folio, ref gvArchivos);
                            BtnAceptar01.Visible = true;
                            break;
                        case "4":   //Reprocesamiento
                            cob.MostrarArchivosPorFolio_GridView(folio, ref gvArchivos);
                            //trSubirExcel.Visible = false;
                            trSubirExcelParaValidar.Visible = true;

                            BtnValidacion.Visible = false;

                            break;
                        case "5":   //Revisión analista front
                            trSubirExcelDependencia.Visible = true;
                            cob.MostrarArchivosPorFolio_GridView(folio, ref gvArchivos);
                            BtnAceptar02.Visible = true;
                            BtnCancelar02.Visible = true;
                            break;
                        case "6":   //Revisión analista back errores
                            //Cargar los archivos, si los hubiere
                            CargarArchivosTramite(dt.Rows[0][3].ToString());

                            cob.MostrarArchivosPorFolio_GridView(folio, ref gvArchivos);
                            BtnValidacion.Visible = false;
                            break;
                        case "7":   //Revisión analista
                            cob.MostrarArchivosPorFolio_GridView(folio, ref gvArchivos);
                            BtnTerminar02.Visible = true;
                            BtnCancelar02.Visible = false;
                            BtnCancelar.Visible = false;
                            break;
                        case "8":   //Concluído
                            //Trámite terminado, sólo para revisión
                            break;
                        default:    //comun/inicio
                            trNoFolioExiste.Visible = false;
                            trSubirExcelParaValidar.Visible = true;
                            BtnValidacion.Visible = false;
                            trArchivoExcel.Visible = true;
                            cob.MostrarArchivosPorFolio_GridView(folio, ref gvArchivos);
                            break;
                    }
                }
            }
        }

        protected void AgregarGridABDBasica(GridView gridview)
        {
            foreach (GridViewRow gv in gridview.Rows)
            {
                cob.AgregarPolizaDetalleBasica(txtNoPoliza.Text,
                    ((DataBoundLiteralControl)gv.Cells[0].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[1].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[2].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[3].Controls[0]).Text.Trim(), 
                    ((DataBoundLiteralControl)gv.Cells[4].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[5].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[6].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[7].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[8].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[9].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[10].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[11].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[12].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[13].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[14].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[15].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[16].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[17].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[18].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[19].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[20].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[21].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[22].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[23].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[24].Controls[0]).Text.Trim(),
                    ((DataBoundLiteralControl)gv.Cells[25].Controls[0]).Text.Trim(),
                    "S", 
                    Request["folio"], 
                    ddlTrimestreQuincena.SelectedValue, 
                    ddlAnn.SelectedValue);
            }
        }

        protected void GuardarArchivosTramite()
        {

        }

        protected void CargarArchivosTramite(string poliza)
        {

        }



        #endregion


    }
}