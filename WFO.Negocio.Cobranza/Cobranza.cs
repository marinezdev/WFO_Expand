using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;
using System.Web.UI.DataVisualization.Charting;

namespace WFO.Negocio.Cobranza
{
    public class Cobranza
    {
        //Variables públicas (sólo para esta clase)
        public int alertasBasica = 0;
        public int alertasVacio = 0;
        public int advertenciasBasica = 0;
        public decimal SumaMontoAjuste = 0;
        public decimal SumaPagarDependencia = 0;
        public int contar = 0;
        public int alertasPotenciacion;
        public int advertenciasPotenciacion;
        public int dependientessincurp = 0;

        //Instancias
        GridViewLocal gridviewpersonalizado = new GridViewLocal();
        AccesoDatos.Procesos.ArchivosDependencias ad = new AccesoDatos.Procesos.ArchivosDependencias();
        AccesoDatos.Procesos.ArchivosDependenciasEstados ade = new AccesoDatos.Procesos.ArchivosDependenciasEstados();
        AccesoDatos.Procesos.Polizas pol = new AccesoDatos.Procesos.Polizas();
        AccesoDatos.Procesos.PolizasDetalle pde = new AccesoDatos.Procesos.PolizasDetalle();
        AccesoDatos.Procesos.Dependencias dep = new AccesoDatos.Procesos.Dependencias();
        AccesoDatos.Procesos.Municipios mun = new AccesoDatos.Procesos.Municipios();
        AccesoDatos.Procesos.AseguradosTitulares at = new AccesoDatos.Procesos.AseguradosTitulares();
        AccesoDatos.Procesos.Coasegurados coa = new AccesoDatos.Procesos.Coasegurados();
        AccesoDatos.Procesos.Archivos arch = new AccesoDatos.Procesos.Archivos();

        #region Basica *************************************************************************************************************************

        public void TablaBasica(GridView gridview, DataTable dt)
        {
            gridviewpersonalizado.GridViewPersonalizado(ref gridview);

            BoundField col01 = new BoundField();
            col01.DataField = "Dependencia";
            col01.HeaderText = "Dependencia";
            gridview.Columns.Add(col01);

            BoundField col02 = new BoundField();
            col02.DataField = "APaterno";
            col02.HeaderText = "Apellido Paterno";
            gridview.Columns.Add(col02);

            BoundField col03 = new BoundField();
            col03.DataField = "AMaterno";
            col03.HeaderText = "Apellido Materno";
            gridview.Columns.Add(col03);

            BoundField col04 = new BoundField();
            col04.DataField = "Nombres";
            col04.HeaderText = "Nombres(s)";
            gridview.Columns.Add(col04);

            BoundField col05 = new BoundField();
            col05.DataField = "FNacimiento";
            col05.HeaderText = "Fecha Nacimiento";
            gridview.Columns.Add(col05);

            BoundField col06 = new BoundField();
            col06.DataField = "RFC";
            col06.HeaderText = "RFC";
            gridview.Columns.Add(col06);

            BoundField col07 = new BoundField();
            col07.DataField = "CURP";
            col07.HeaderText = "CURP";
            gridview.Columns.Add(col07);

            BoundField col08 = new BoundField();
            col08.DataField = "Sexo";
            col08.HeaderText = "Sexo";
            gridview.Columns.Add(col08);

            BoundField col09 = new BoundField();
            col09.DataField = "CEntidadFederativa";
            col09.HeaderText = "Entidad Federativa";
            gridview.Columns.Add(col09);

            BoundField col10 = new BoundField();
            col10.DataField = "CMunicipio";
            col10.HeaderText = "Ciudad/Municipio";
            gridview.Columns.Add(col10);

            BoundField col11 = new BoundField();
            col11.DataField = "NivelTabular";
            col11.HeaderText = "Nivel Tabular";
            gridview.Columns.Add(col11);

            BoundField col12 = new BoundField();
            col12.DataField = "MPercepcionOBM";
            col12.HeaderText = "Monto Percepción Ordinaria Bruta Mensual";
            gridview.Columns.Add(col12);

            BoundField col13 = new BoundField();
            col13.DataField = "Eventual";
            col13.HeaderText = "Eventual";
            gridview.Columns.Add(col13);

            BoundField col14 = new BoundField();
            col14.DataField = "APAsegurado";
            col14.HeaderText = "Apellido Paterno Asegurado";
            gridview.Columns.Add(col14);

            BoundField col15 = new BoundField();
            col15.DataField = "AMAsegurado";
            col15.HeaderText = "Apellido Materno Asegurado";
            gridview.Columns.Add(col15);

            BoundField col16 = new BoundField();
            col16.DataField = "NAsegurado";
            col16.HeaderText = "Nombre(s) Asegurado";
            gridview.Columns.Add(col16);

            BoundField col17 = new BoundField();
            col17.DataField = "FNAsegurado";
            col17.HeaderText = "Fecha Nacimiento Asegurado";
            gridview.Columns.Add(col17);

            BoundField col18 = new BoundField();
            col18.DataField = "CURPAsegurado";
            col18.HeaderText = "CURP Asegurado";
            gridview.Columns.Add(col18);

            BoundField col19 = new BoundField();
            col19.DataField = "SAsegurado";
            col19.HeaderText = "Sexo Asegurado";
            gridview.Columns.Add(col19);

            BoundField col20 = new BoundField();
            col20.DataField = "FAAsegurado";
            col20.HeaderText = "Fecha Afiliación Asegurado";
            gridview.Columns.Add(col20);

            BoundField col21 = new BoundField();
            col21.DataField = "TAsegurado";
            col21.HeaderText = "Tipo de Asegurado";
            gridview.Columns.Add(col21);

            BoundField col22 = new BoundField();
            col22.DataField = "FIColectividad";
            col22.HeaderText = "Fecha Ingreso Colectividad";
            gridview.Columns.Add(col22);

            BoundField col23 = new BoundField();
            col23.DataField = "SABasica";
            col23.HeaderText = "Suma Asegurada Básica";
            gridview.Columns.Add(col23);

            BoundField col24 = new BoundField();
            col24.DataField = "PBTReportar";
            col24.HeaderText = "Prima Básica Trimestre a Reportar";
            gridview.Columns.Add(col24);

            BoundField col25 = new BoundField();
            col25.DataField = "MAPBasica";
            col25.HeaderText = "Monto de Ajuste en Prima Básica";
            gridview.Columns.Add(col25);

            BoundField col26 = new BoundField();
            col26.DataField = "ITPDependencia";
            col26.HeaderText = "Importe total a Pagar por la Dependencia";
            gridview.Columns.Add(col26);

            gridview.RowDataBound += Gridview_RowDataBound;

            gridview.DataSource = dt;
            gridview.DataBind();
        }

        private void Gridview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            contar += 1;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region Items

                string _Dependencia = DataBinder.Eval(e.Row.DataItem, "Dependencia").ToString();
                string _APaterno = DataBinder.Eval(e.Row.DataItem, "APaterno").ToString();
                string _AMaterno = DataBinder.Eval(e.Row.DataItem, "AMaterno").ToString();
                string _Nombres = DataBinder.Eval(e.Row.DataItem, "Nombres").ToString();
                string _FNacimiento = DataBinder.Eval(e.Row.DataItem, "FNacimiento").ToString();
                string _RFC = DataBinder.Eval(e.Row.DataItem, "RFC").ToString();
                string _CURP = DataBinder.Eval(e.Row.DataItem, "CURP").ToString();
                string _Sexo = DataBinder.Eval(e.Row.DataItem, "Sexo").ToString();
                string _CEntidadFederativa = DataBinder.Eval(e.Row.DataItem, "CEntidadFederativa").ToString();
                string _CMunicipio = DataBinder.Eval(e.Row.DataItem, "CMunicipio").ToString();
                string _NivelTabular = DataBinder.Eval(e.Row.DataItem, "NivelTabular").ToString();
                string _MPercepcionOBM = DataBinder.Eval(e.Row.DataItem, "MPercepcionOBM").ToString();
                string _Eventual = DataBinder.Eval(e.Row.DataItem, "Eventual").ToString();
                string _APAsegurado = DataBinder.Eval(e.Row.DataItem, "APAsegurado").ToString();
                string _AMAsegurado = DataBinder.Eval(e.Row.DataItem, "AMAsegurado").ToString();
                string _NAsegurado = DataBinder.Eval(e.Row.DataItem, "NAsegurado").ToString();
                string _FNAsegurado = DataBinder.Eval(e.Row.DataItem, "FNAsegurado").ToString();
                string _CURPAsegurado = DataBinder.Eval(e.Row.DataItem, "CURPAsegurado").ToString();
                string _SAsegurado = DataBinder.Eval(e.Row.DataItem, "SAsegurado").ToString();
                string _FAAsegurado = DataBinder.Eval(e.Row.DataItem, "FAAsegurado").ToString();
                string _TAsegurado = DataBinder.Eval(e.Row.DataItem, "TAsegurado").ToString();
                string _FIColectividad = DataBinder.Eval(e.Row.DataItem, "FIColectividad").ToString();
                string _SABasica = DataBinder.Eval(e.Row.DataItem, "SABasica").ToString();
                string _PBTReportar = DataBinder.Eval(e.Row.DataItem, "PBTReportar").ToString();
                string _MAPBasica = DataBinder.Eval(e.Row.DataItem, "MAPBasica").ToString();
                string _ITPDependencia = DataBinder.Eval(e.Row.DataItem, "ITPDependencia").ToString();

                //if (_Poliza == "")
                //{
                //    it.ValidacionCeldaAlerta(e.Row.Cells[1], "Campo vacío");
                //    alertasBasica += 1;
                //}
                if (_Dependencia.Length > 150)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[0], "Longitud del texto excesiva.");
                    alertasBasica += 1;
                }
                if (!ValidarDependencia(_Dependencia))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[0], "La dependencia no existe o esta mal escrita.");
                    alertasBasica += 1;
                }
                if (_APaterno.Length == 0 || _APaterno.Length > 150)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[1], "Campo vacío/Longitud excesiva.");
                    alertasBasica += 1;
                    alertasVacio += 1;
                }
                if (_AMaterno.Length == 0 || _AMaterno.Length > 150)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[2], "Campo vacío/Longitud excesiva.");
                    alertasBasica += 1;
                    alertasVacio += 1;
                }
                if (_Nombres.Length > 20)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[3], "Longitud excesiva.");
                    alertasBasica += 1;
                    alertasVacio += 1;
                }

                if (!DateTime.TryParseExact(_FNacimiento, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fech) ||
                    _FNacimiento == "")
                {
                    ValidacionCeldaAlerta(e.Row.Cells[4], "Formato de fecha incorrecta/campo vacío.");
                    alertasBasica += 1;
                    alertasVacio += 1;
                }
                if (_FNacimiento.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[4], "Celda vacía");
                    alertasVacio += 1;
                }
                if (_RFC.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[5], "Celda vacía");
                    alertasVacio += 1;
                }
                if (_RFC.Length > 13 || _RFC.Length < 13)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[5], "campo vacío/Longitud de texto excesiva o faltan datos.");
                    alertasBasica += 1;
                }
                if (_FNacimiento != "" && _FNacimiento.Length == 13)
                {
                    if (!_RFC.Contains(_FNacimiento.Substring(2, 6)))
                    {
                        ValidacionCeldaAlerta(e.Row.Cells[5], "La fecha de nacimiento no coincide con la del RFC.");
                        ValidacionCeldaAlerta(e.Row.Cells[6], "La fecha de nacimiento no coincide con la del RFC.");
                        alertasBasica += 1;
                    }
                }

                if (_CURP.Length > 18 || _CURP.Length < 18)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[6], "Campo vacío, longitud excesiva o faltan datos.");
                    alertasBasica += 1;
                }
                if (_CURP.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[6], "Celda vacía");
                    alertasVacio += 1;
                }

                if (_Sexo.Length > 1 || (_Sexo != "H" && _Sexo != "M"))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[7], "Campo vacío, longitud incorrecta o indicador incorrecto.");
                    alertasBasica += 1;
                }

                if (_CEntidadFederativa.Length > 2 || _CEntidadFederativa.Length < 2 || !string.IsNullOrEmpty(dep.SeleccionarPorNombre(_CEntidadFederativa)))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[8], "Campo vacío, clave o longitud incorrecta.");
                    alertasBasica += 1;
                }
                if (_CEntidadFederativa.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[8], "Celda vacía");
                    alertasVacio += 1;
                }

                if (_CMunicipio.Length > 3 || _CMunicipio.Length < 3 || !Municipios(_CEntidadFederativa, _CMunicipio))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[9], "Clave de Ciudad o Municipio no existe o el campo está vacío.");
                    alertasBasica += 1;
                }
                if (_CMunicipio.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[9], "Celda vacía");
                    alertasVacio += 1;
                }
                if (_NivelTabular.Length > 1 || !NivelTabular().Contains(_NivelTabular))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[10], "Campo vacío ó Nivel tabular incorrecto.");
                    alertasBasica += 1;
                }
                if (_NivelTabular.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[10], "Celda vacía");
                    alertasVacio += 1;
                }

                if (_MPercepcionOBM.Length > 9 || _MPercepcionOBM.Contains(","))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[11], "Monto Percepcion Ordinaria incorrecta o tiene comas.");
                    alertasBasica += 1;
                }

                if (_Eventual != "SI" && _Eventual != "NO")
                {
                    ValidacionCeldaAlerta(e.Row.Cells[12], "Campo vacío o texto incorrecto");
                    alertasBasica += 1;
                }
                if (_Eventual.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[12], "Celda vacía");
                    alertasVacio += 1;
                }

                if (_APAsegurado.Length > 20)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[13], "Campo vacío o tamaño excesivo.");
                    alertasBasica += 1;
                }
                if (_AMAsegurado.Length > 20)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[14], "Campo vacío o tamaño excesivo.");
                    alertasBasica += 1;
                }
                if (_NAsegurado.Length > 20)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[15], "Campo vacío o tamaño excesivo.");
                    alertasBasica += 1;
                }
                if (_NAsegurado.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[15], "Celda vacía");
                    alertasVacio += 1;
                }

                DateTime fech1;
                if (_FNAsegurado.Length > 8 || !DateTime.TryParseExact(_FNAsegurado, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fech1))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[16], "Campo vacío o formato de fecha incorrecta.");
                    alertasBasica += 1;
                }
                if (_FNAsegurado.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[16], "Celda vacía");
                    alertasVacio += 1;
                }

                if (_CURPAsegurado.Length != 18)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[17], "campo vacío o longitud incorrecta.");
                    alertasBasica += 1;
                }

                if ((_SAsegurado != "H" && _SAsegurado != "M"))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[18], "Campo vacío o texto incorrecto.");
                    alertasBasica += 1;
                }

                DateTime fech2;
                if (_FAAsegurado.Length != 8 || !DateTime.TryParseExact(_FAAsegurado, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fech2))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[19], "Campo vacío o formato fecha incorrecto.");
                    alertasBasica += 1;
                }

                if (_TAsegurado.Length > 1 || !TipoAsegurado().Contains(_TAsegurado))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[20], "Campo vacío, longitud incorrecta o tipo de asegurado incorrecto.");
                    alertasBasica += 1;
                }

                DateTime fech3;
                if (_FIColectividad.Length != 8 || !DateTime.TryParseExact(_FIColectividad, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fech3))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[21], "Campo vacío o formato de fecha incorrecto.");
                    alertasBasica += 1;
                }

                if (!SumaAseguradaPorNivelTabular(_SABasica, _NivelTabular))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[22], "Campo vacío o tarifa incorrecta.");
                    alertasBasica += 1;
                }
                if (_SABasica.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[22], "Celda vacía");
                    alertasVacio += 1;
                }

                //if (TarifaQuincenalTitulares(_NivelTabular, double.Parse(_SABasica), _TAsegurado) != _ITPDependencia)
                //{
                //    ValidacionCeldaAdvertencia(celda.Row.Cells[25], "Nivel tabular, tipo de asegurado o súma básica incorrecta, verificar.");
                //    alertasBasica += 1;
                //}
                if (_PBTReportar.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[23], "Celda vacía");
                    alertasVacio += 1;
                }

                if (_MAPBasica.Length == 0)
                {
                    e.Row.Cells[24].Text = "0";
                    ValidacionCeldaAlerta(e.Row.Cells[24], "Tarifa incorrecta.");
                }

                _MAPBasica = _MAPBasica.Length == 0 ? "0" : _MAPBasica;
                if (_MAPBasica.Contains("$"))
                    _MAPBasica = _MAPBasica.Replace('$', ' ');
                _PBTReportar = _PBTReportar.Length == 0 ? "0" : _PBTReportar;
                decimal suma = ((decimal.Parse(_PBTReportar) * 6) + decimal.Parse(_MAPBasica));
                if (suma.ToString() != _ITPDependencia)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[25], "Importe incorrecto.");
                    alertasBasica += 1;
                }
                else if (decimal.Parse(_ITPDependencia) < 0)
                {
                    ValidacionCeldaAdvertencia(e.Row.Cells[25], "Importe Negativo");
                }
                //Suma del monto de ajuste
                SumaMontoAjuste += decimal.Parse(_MAPBasica);

                if (_ITPDependencia.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[25], "Celda vacía.");
                    alertasVacio += 1;
                }
                //Suma de Importe a Pagar por la Dependencia
                if (_ITPDependencia.Contains("$"))
                    _ITPDependencia = _ITPDependencia.Replace('$', ' ');
                if (_ITPDependencia == "") _ITPDependencia = "0";
                SumaPagarDependencia += decimal.Parse(_ITPDependencia);

                #endregion
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].RowSpan = 25;
                e.Row.Cells[0].Text = "<font style='font-size: 15px; font-weight: normal'>Registros Totales: " + contar.ToString() +
                    ", <span style='color: red'>alertas: " + alertasBasica.ToString() +
                    "</span>, <span style='color: navy'>celdas vacías: " + alertasVacio.ToString() +
                    "</span>, <span style='color: navy'>Suma Monto Ajuste: " + string.Format("{0:C}", SumaMontoAjuste) +
                    "</span>, <span style='color: navy'>Suma Total a Pagar por Dependencia: " + string.Format("{0:C}", SumaPagarDependencia) + "</span></font>";
            }
        }
        
        public int AgregarBasica(string nombre, string folio, string poliza, string cobertura, string subidopor, string correo, string asunto, string trimestre, string ann)
        {
            return ad.AgregarBasica(nombre, folio, poliza, cobertura, subidopor, correo, asunto, trimestre, ann);
        }

        /// <summary>
        /// Agrega el nuevo estado a ArchivosDependenciasEstados
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public int AgregarEstadoDependencia(string folio, string estado)
        {
            return ade.Agregar(folio, estado);
        }

        public int AgregarDependenciaATramite(string idusuario, string folio)
        {
            return ad.AgregarDependenciaATramite(idusuario, folio);
        }

        public DataTable DetalleFolio(string folio)
        {
            return ad.SeleccionarDetalle(folio);
        }

        public int AgregarPolizaDetalleBasica(string poliza, string dependencia, string apaterno, string amaterno, string nombres, string fnacimiento, string rfc,
            string curp, string sexo, string centidadfederativa, string cmunicipio, string niveltabular, string mpercepcionordinariabrutamensual, string eventual,
            string apasegurado, string amasegurado, string nasegurado, string fnasegurado, string curpasegurado, string sasegurado, string faasegurado,
            string tasegurado, string ficolectividad, string sabasica, string pbtreportar, string mapbasica, string itpdependencia, string estado, string folio, 
            string trimestre, string ann)
        {
            return pde.Agregar(poliza, dependencia, apaterno, amaterno, nombres, fnacimiento, rfc, curp, sexo, centidadfederativa, cmunicipio, niveltabular,
            mpercepcionordinariabrutamensual, eventual, apasegurado, amasegurado, nasegurado, fnasegurado, curpasegurado, sasegurado, faasegurado, tasegurado,
            ficolectividad, sabasica, pbtreportar, mapbasica, itpdependencia, estado, folio, trimestre, ann);
        }

        /// <summary>
        /// Modifica el estado del registro en ArchivosDependencias
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public int ActualizarEstado(string folio, string estado)
        {
            return ad.ActualizarEstado(folio, estado);
        }

        public int AgregarPDF(string archivopdf, string folio)
        {
            return ad.AgregarPDF(archivopdf, folio);
        }

        public int AgregarXML(string archivoxls, string folio)
        {
            return ad.AgregarXML(archivoxls, folio);
        }

        #endregion

        #region Potenciacion *******************************************************************************************************************

        public void TablaPotenciacion(GridView gridview, DataTable dt)
        {
            gridviewpersonalizado.GridViewPersonalizado(ref gridview);

            BoundField col01 = new BoundField();
            col01.DataField = "Dependencia";
            col01.HeaderText = "Dependencia";
            gridview.Columns.Add(col01);

            BoundField col02 = new BoundField();
            col02.DataField = "APaterno";
            col02.HeaderText = "Apellido Paterno";
            gridview.Columns.Add(col02);

            BoundField col03 = new BoundField();
            col03.DataField = "AMaterno";
            col03.HeaderText = "Apellido Materno";
            gridview.Columns.Add(col03);

            BoundField col04 = new BoundField();
            col04.DataField = "Nombres";
            col04.HeaderText = "Nombres(s)";
            gridview.Columns.Add(col04);

            BoundField col05 = new BoundField();
            col05.DataField = "FNacimiento";
            col05.HeaderText = "Fecha Nacimiento";
            gridview.Columns.Add(col05);

            BoundField col06 = new BoundField();
            col06.DataField = "RFC";
            col06.HeaderText = "RFC";
            gridview.Columns.Add(col06);

            BoundField col07 = new BoundField();
            col07.DataField = "CURP";
            col07.HeaderText = "CURP";
            gridview.Columns.Add(col07);

            BoundField col08 = new BoundField();
            col08.DataField = "Sexo";
            col08.HeaderText = "Sexo";
            gridview.Columns.Add(col08);

            BoundField col09 = new BoundField();
            col09.DataField = "CEntidadFederativa";
            col09.HeaderText = "Entidad Federativa";
            gridview.Columns.Add(col09);

            BoundField col10 = new BoundField();
            col10.DataField = "CMunicipio";
            col10.HeaderText = "Ciudad/Municipio";
            gridview.Columns.Add(col10);

            BoundField col11 = new BoundField();
            col11.DataField = "NivelTabular";
            col11.HeaderText = "Nivel Tabular";
            gridview.Columns.Add(col11);

            BoundField col12 = new BoundField();
            col12.DataField = "MPercepcionOBM";
            col12.HeaderText = "Monto Percepción Ordinaria Bruta Mensual";
            gridview.Columns.Add(col12);

            BoundField col13 = new BoundField();
            col13.DataField = "Eventual";
            col13.HeaderText = "Eventual";
            gridview.Columns.Add(col13);

            BoundField col14 = new BoundField();
            col14.DataField = "APAsegurado";
            col14.HeaderText = "Apellido Paterno Asegurado";
            gridview.Columns.Add(col14);

            BoundField col15 = new BoundField();
            col15.DataField = "AMAsegurado";
            col15.HeaderText = "Apellido Materno Asegurado";
            gridview.Columns.Add(col15);

            BoundField col16 = new BoundField();
            col16.DataField = "NAsegurado";
            col16.HeaderText = "Nombre(s) Asegurado";
            gridview.Columns.Add(col16);

            BoundField col17 = new BoundField();
            col17.DataField = "FNAsegurado";
            col17.HeaderText = "Fecha Nacimiento Asegurado";
            gridview.Columns.Add(col17);

            BoundField col18 = new BoundField();
            col18.DataField = "CURPAsegurado";
            col18.HeaderText = "CURP Asegurado";
            gridview.Columns.Add(col18);

            BoundField col19 = new BoundField();
            col19.DataField = "SAsegurado";
            col19.HeaderText = "Sexo Asegurado";
            gridview.Columns.Add(col19);

            BoundField col20 = new BoundField();
            col20.DataField = "FAAsegurado";
            col20.HeaderText = "Fecha Afiliación Asegurado";
            gridview.Columns.Add(col20);

            BoundField col21 = new BoundField();
            col21.DataField = "TAsegurado";
            col21.HeaderText = "Tipo de Asegurado";
            gridview.Columns.Add(col21);

            BoundField col22 = new BoundField();
            col22.DataField = "FIColectividad";
            col22.HeaderText = "Fecha Ingreso Colectividad";
            gridview.Columns.Add(col22);

            BoundField col23 = new BoundField();
            col23.DataField = "SABasica";
            col23.HeaderText = "Suma Asegurada Básica";
            gridview.Columns.Add(col23);

            BoundField col24 = new BoundField();
            col24.DataField = "SAPotenciada";
            col24.HeaderText = "Suma Asegurada Potenciada";
            gridview.Columns.Add(col24);

            BoundField col25 = new BoundField();
            col25.DataField = "SAT";
            col25.HeaderText = "Suma Asegurada Total";
            gridview.Columns.Add(col25);

            BoundField col26 = new BoundField();
            col26.DataField = "PrimaPotenciadaQR";
            col26.HeaderText = "Prima Potenciada Quincenal a Reportar";
            gridview.Columns.Add(col26);

            BoundField col27 = new BoundField();
            col27.DataField = "FormaPago";
            col27.HeaderText = "Forma de Pago";
            gridview.Columns.Add(col27);

            BoundField col28 = new BoundField();
            col28.DataField = "MontoAjustePrimaP";
            col28.HeaderText = "Monto de Ajuste en Prima Potenciada";
            gridview.Columns.Add(col28);

            BoundField col29 = new BoundField();
            col29.DataField = "ImporteTotalPagar";
            col29.HeaderText = "Importe Total a Pagar de la Prima Potenciada";
            gridview.Columns.Add(col29);

            BoundField col30 = new BoundField();
            col30.DataField = "FechaAASA";
            col30.HeaderText = "Fecha de Antigüedad del Asegurado en la Suma Asegurada";
            gridview.Columns.Add(col30);

            gridview.RowDataBound += Gridview_RowDataBound1;

            gridview.DataSource = dt;
            gridview.DataBind();
        }

        private void Gridview_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            contar += 1;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region Items

                //string _Poliza = DataBinder.Eval(e.Row.DataItem, "Poliza").ToString();
                string _Dependencia = DataBinder.Eval(e.Row.DataItem, "Dependencia").ToString();
                string _APaterno = DataBinder.Eval(e.Row.DataItem, "APaterno").ToString();
                string _AMaterno = DataBinder.Eval(e.Row.DataItem, "AMaterno").ToString();
                string _Nombres = DataBinder.Eval(e.Row.DataItem, "Nombres").ToString();
                string _FNacimiento = DataBinder.Eval(e.Row.DataItem, "FNacimiento").ToString();
                string _RFC = DataBinder.Eval(e.Row.DataItem, "RFC").ToString();
                string _CURP = DataBinder.Eval(e.Row.DataItem, "CURP").ToString();
                string _Sexo = DataBinder.Eval(e.Row.DataItem, "Sexo").ToString();
                string _CEntidadFederativa = DataBinder.Eval(e.Row.DataItem, "CEntidadFederativa").ToString();
                string _CMunicipio = DataBinder.Eval(e.Row.DataItem, "CMunicipio").ToString();
                string _NivelTabular = DataBinder.Eval(e.Row.DataItem, "NivelTabular").ToString();
                string _MPercepcionOBM = DataBinder.Eval(e.Row.DataItem, "MPercepcionOBM").ToString();
                string _Eventual = DataBinder.Eval(e.Row.DataItem, "Eventual").ToString();
                string _APAsegurado = DataBinder.Eval(e.Row.DataItem, "APAsegurado").ToString();
                string _AMAsegurado = DataBinder.Eval(e.Row.DataItem, "AMAsegurado").ToString();
                string _NAsegurado = DataBinder.Eval(e.Row.DataItem, "NAsegurado").ToString();
                string _FNAsegurado = DataBinder.Eval(e.Row.DataItem, "FNAsegurado").ToString();
                string _CURPAsegurado = DataBinder.Eval(e.Row.DataItem, "CURPAsegurado").ToString();
                string _SAsegurado = DataBinder.Eval(e.Row.DataItem, "SAsegurado").ToString();
                string _FAAsegurado = DataBinder.Eval(e.Row.DataItem, "FAAsegurado").ToString();
                string _TAsegurado = DataBinder.Eval(e.Row.DataItem, "TAsegurado").ToString();
                string _FIColectividad = DataBinder.Eval(e.Row.DataItem, "FIColectividad").ToString();
                string _SABasica = DataBinder.Eval(e.Row.DataItem, "SABasica").ToString();
                string _SAPotenciada = DataBinder.Eval(e.Row.DataItem, "SAPotenciada").ToString();
                string _SAT = DataBinder.Eval(e.Row.DataItem, "SAT").ToString();
                string _PrimaPQR = DataBinder.Eval(e.Row.DataItem, "PrimaPotenciadaQR").ToString();
                string _FormaPago = DataBinder.Eval(e.Row.DataItem, "FormaPago").ToString();
                string _MontoAPPABPC = DataBinder.Eval(e.Row.DataItem, "MontoAjustePrimaP").ToString();
                string _ImporteTPPPFRSP = DataBinder.Eval(e.Row.DataItem, "ImporteTotalPagar").ToString();
                string _FAAseguradoSAP = DataBinder.Eval(e.Row.DataItem, "FechaAASA").ToString();

                //Validaciones
                //Advertencias
                //Vacías

                if (_Dependencia.Length > 150)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[0], "Longitud del texto excesiva.");
                    alertasPotenciacion += 1;
                }
                if (!ValidarDependencia(_Dependencia))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[0], "La dependencia no existe o esta mal escrita.");
                    alertasPotenciacion += 1;
                }
                if (_APaterno.Length > 150)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[1], "Longitud excesiva.");
                    alertasPotenciacion += 1;
                }
                if (_APaterno.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[1], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_AMaterno.Length > 150)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[2], "Longitud excesiva.");
                    alertasPotenciacion += 1;
                }
                if (_AMaterno.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[2], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_Nombres.Length > 20)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[3], "Longitud excesiva.");
                    alertasPotenciacion += 1;
                }
                if (_Nombres.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[3], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (!DateTime.TryParseExact(_FNacimiento, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fech))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[4], "Formato de fecha incorrecta");
                    alertasPotenciacion += 1;
                }
                if (_FNacimiento.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[4], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_RFC.Length > 13 || _RFC.Length < 13)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[5], "Longitud excesiva o faltan datos.");
                    alertasPotenciacion += 1;
                }
                if (_FNacimiento != "" && _FNacimiento.Length == 13)
                {
                    if (!_RFC.Contains(_FNacimiento.Substring(2, 6)))
                    {
                        ValidacionCeldaAlerta(e.Row.Cells[4], "La fecha de nacimiento no coincide con la del RFC.");
                        ValidacionCeldaAlerta(e.Row.Cells[5], "La fecha de nacimiento no coincide con la del RFC.");
                        alertasPotenciacion += 1;
                    }
                }
                if (_RFC.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[5], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_CURP.Length > 18 || _CURP.Length < 18)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[6], "Longitud excesiva o faltan datos.");
                    alertasPotenciacion += 1;
                }
                if (_CURP.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[6], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_Sexo.Length > 1 || (_Sexo != "H" && _Sexo != "M"))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[7], "Longitud o indicador incorrecto.");
                    alertasPotenciacion += 1;
                }
                if (_Sexo.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[7], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_CEntidadFederativa.Length > 2 || _CEntidadFederativa.Length < 2)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[8], "Clave o longitud incorrecto.");
                    alertasPotenciacion += 1;
                }
                if (_CEntidadFederativa.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[8], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_CMunicipio.Length > 3 || _CMunicipio.Length < 3 || !Municipios(_CEntidadFederativa, _CMunicipio))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[8], "Clave de Ciudad o Municipio no existe.");
                    ValidacionCeldaAlerta(e.Row.Cells[9], "Clave de Ciudad o Municipio no existe.");
                    alertasPotenciacion += 1;
                }
                if (_CMunicipio.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[9], "Celda vacía.");
                    alertasVacio += 1;
                }
                if (_NivelTabular.Length > 2 || !NivelTabular().Contains(_NivelTabular))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[10], "Nivel tabular incorrecto.");
                    alertasPotenciacion += 1;
                }
                if (_NivelTabular.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[10], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_MPercepcionOBM.Length > 9 || _MPercepcionOBM.Contains(","))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[11], "Monto Percepcion Ordinaria incorrecta o tiene comas.");
                    alertasPotenciacion += 1;
                }

                if ((_Eventual != "SI" && _Eventual != "NO"))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[12], "Texto incorrecto");
                    alertasPotenciacion += 1;
                }
                if (_Eventual.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[12], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_APAsegurado.Length > 20)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[13], "Longitud incorrecta.");
                    alertasPotenciacion += 1;
                }
                if (_APAsegurado.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[13], "Celda vacía.");
                    alertasVacio += 1;
                }
                if (_AMAsegurado.Length > 20)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[14], "Longitud incorrecta.");
                    alertasPotenciacion += 1;
                }
                if (_AMAsegurado.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[14], "Celda vacía.");
                    alertasVacio += 1;
                }
                if (_NAsegurado.Length > 20)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[15], "Longitud incorrecta.");
                    alertasPotenciacion += 1;
                }
                if (_NAsegurado.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[15], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_FNAsegurado.Length > 8 || !DateTime.TryParseExact(_FNAsegurado, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fech1))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[16], "Formato de fecha incorrecta.");
                    alertasPotenciacion += 1;
                }
                if (_FNAsegurado.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[16], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_CURPAsegurado.Length > 18 || _CURPAsegurado.Length < 18)
                {
                    ValidacionCeldaAlerta(e.Row.Cells[17], "Longitud incorrecta.");
                    alertasPotenciacion += 1;
                }
                if (_CURPAsegurado.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[17], "Celda vacía.");
                    alertasVacio += 1;
                }

                //Validacion del curp del coasegurado contra el asegurado (que no sea el mismo)
                if (_CURPAsegurado == _CURP && _TAsegurado != "T")
                {
                    ValidacionCeldaCURP(e.Row.Cells[17], "El CURP del dependiente es igual al del titular");
                    dependientessincurp += 1;
                }



                if (_SAsegurado.Length == 0 || (_SAsegurado != "H" && _SAsegurado != "M"))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[18], "Texto incorrecto.");
                    alertasPotenciacion += 1;
                }
                if (_SAsegurado.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[18], "Celda vacía.");
                    alertasVacio += 1;
                }

                DateTime fech2;
                if (_FAAsegurado.Length > 8 || _FAAsegurado.Length < 8 || !DateTime.TryParseExact(_FAAsegurado, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fech2))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[19], "Formato fecha incorrecto.");
                    alertasPotenciacion += 1;
                }
                if (_FAAsegurado.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[19], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_TAsegurado.Length > 1 || !TipoAsegurado().Contains(_TAsegurado))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[20], "Longitud o tipo de asegurado incorrecto.");
                    alertasPotenciacion += 1;
                }
                if (_TAsegurado.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[20], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_FIColectividad.Length > 8 || _FIColectividad.Length < 8 || !DateTime.TryParseExact(_FIColectividad, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fech3))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[21], "Campo vacío o formato de fecha incorrecto.");
                    alertasPotenciacion += 1;
                }
                if (_FIColectividad.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[21], "Celda vacía.");
                    alertasVacio += 1;
                }


                if (!SumaAseguradaBasicaPotenciacion().Contains(_SABasica))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[22], "SAB incorrecta.");
                    alertasPotenciacion += 1;
                }
                if (_SABasica.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[22], "Celda vacía.");
                    alertasVacio += 1;
                }
                if (!SumaAseguradaPotenciada().Contains(_SAPotenciada))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[23], "SAP incorrecta.");
                    alertasPotenciacion += 1;
                }
                if (_SAPotenciada.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[23], "Celda vacía.");
                    alertasVacio += 1;
                }
                if (_SABasica == "") _SABasica = "0";
                if (_SAPotenciada == "") _SAPotenciada = "0";
                decimal suma = decimal.Parse(_SABasica) + decimal.Parse(_SAPotenciada);
                if (!SumaAseguradaPotenciada().Contains(suma.ToString()))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[24], "Clave incorrecta.");
                    alertasPotenciacion += 1;
                }

                if (_PrimaPQR.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[25], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (!TarifaPotenciada(_NivelTabular, _SAT, _TAsegurado).Contains(_PrimaPQR))
                {
                    ValidacionCeldaAdvertencia(e.Row.Cells[25], "Prima incorrecta.");
                    advertenciasPotenciacion += 1;
                }

                if (_FormaPago.Length == 0)
                {
                    //26
                    ValidacionCeldaVacia(e.Row.Cells[26], "Celda vacía.");
                    alertasVacio += 1;
                }
                if (_MontoAPPABPC.Length == 0)
                {
                    //27
                    ValidacionCeldaVacia(e.Row.Cells[27], "Celda vacía.");
                    alertasVacio += 1;
                }
                if (_ImporteTPPPFRSP.Length == 0)
                {
                    //28
                    ValidacionCeldaVacia(e.Row.Cells[28], "Celda vacía.");
                    alertasVacio += 1;
                }
                if (!DateTime.TryParseExact(_FAAseguradoSAP, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fech5))
                {
                    ValidacionCeldaAlerta(e.Row.Cells[29], "Formato de fecha incorrecto.");
                    advertenciasPotenciacion += 1;
                }
                if (_FAAseguradoSAP.Length == 0)
                {
                    ValidacionCeldaVacia(e.Row.Cells[29], "Celda vacía.");
                    alertasVacio += 1;
                }

                #endregion
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].RowSpan = 30;
                e.Row.Cells[0].Text = "<font style='font-size: 15px'>Registros Totales: " + contar.ToString()
                    + ", <span style='color: red'>errores: " + alertasPotenciacion.ToString()
                    + "</span>, <span style='color: orange'>advertencias: " + advertenciasPotenciacion.ToString()
                    + "</span>, <span style='color: purple'>dependientes sin CURP propio: " + dependientessincurp.ToString()
                    + "</span>, <span>celdas vacías: " + alertasVacio.ToString() + "</span></font>";

            }
        }

        public int AgregarPotenciacion(string nombre, string folio, string poliza, string cobertura, string subidopor, string correo, string asunto, string quincena, string ann2, string rol)
        {
            arch.Agregar(folio, nombre);
            return ad.AgregarPotenciacion(nombre, folio, poliza, cobertura, subidopor, correo, asunto, quincena, ann2, rol);
        }

        public int AgregarPolizaDetallePotenciacion(string poliza, string dependencia, string apaterno, string amaterno, string nombres, string fnacimiento, string rfc,
        string curp, string sexo, string centidadfederativa, string cmunicipio, string niveltabular, string mpercepcionordinariabrutamensual, string eventual,
        string apasegurado, string amasegurado, string nasegurado, string fnasegurado, string curpasegurado, string sasegurado, string faasegurado, string tasegurado,
        string ficolectividad, string sabasica, string sapotenciada, string sat, string primapqr, string formapago, string montoappabpc, string importetpppfrsp,
        string faaseguradosap, string estado, string folio, string quincena, string ann)
        {
            return pde.AgregarPotenciacion(poliza, dependencia, apaterno, amaterno, nombres, fnacimiento, rfc,
        curp, sexo, centidadfederativa, cmunicipio, niveltabular, mpercepcionordinariabrutamensual, eventual,
        apasegurado, amasegurado, nasegurado, fnasegurado, curpasegurado, sasegurado, faasegurado, tasegurado,
        ficolectividad, sabasica, sapotenciada, sat, primapqr, formapago, montoappabpc, importetpppfrsp,
        faaseguradosap, estado, folio, quincena, ann);
        }

        public DataTable SeleccionarCienPosicionesPagos(string folio, string periodo, string retenedor)
        {
            return pde.SeleccionarCienPosicionesPagos(folio, periodo, retenedor);
        }

        public string CienPosicionesSumaPagos(string folio)
        {
            return pde.CienPosicionesSumaPagos(folio);
        }

        public DataTable SeleccionarCienPosicionesCancelaciones(string folio, string periodo, string retenedor)
        {
            return pde.SeleccionarCienPosicionesCancelaciones(folio, periodo, retenedor);
        }

        public string CienPosicionesSumaCancelaciones(string folio)
        {
            return pde.CienPosicionesSumaCancelaciones(folio);
        }

        public int Agregar100PosicionesPagos(string archivo, string folio)
        {
            AgregarArchivo(folio, archivo);
            return ad.Agregar100PosicionesPagos(archivo, folio);
        }

        public int Agregar100PosicionesCancelaciones(string archivo, string folio)
        {
            AgregarArchivo(folio, archivo);
            return ad.Agregar100PosicionesCancelaciones(archivo, folio);
        }

        public int ActualizarAsunto(string folio, string asunto)
        {
            return ad.ActualizarAsunto(folio, asunto);
        }

        #endregion

        #region Metodos públicos diversos  *****************************************************************************************************

        public void NombreCliente(string poliza, ref TextBox controlALlenar)
        {
            controlALlenar.Text = pol.NombreCliente(poliza);
        }

        public string GenerarFolio()
        {
            return DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "DH" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
        }

        public string ObtenerIdDependencia(string nombre)
        {
            return dep.SeleccionarIdPorNombre(nombre);
        }

        public string ObtenerIdAseguradoTitular(string curp)
        {
            return at.SeleccionarTitularIdPorCURP(curp);
        }

        public string ObtenerRetenedorDependencia(string nombre)
        {
            return dep.SeleccionarRetenedorPorNombre(nombre);
        }

        public void MostrarArchivosPorFolio_GridView(string folio, ref GridView gridview)
        {
            Funciones.LlenarControles.LlenarGridView(ref gridview, arch.SeleccionarPorFolio(folio));
        }

        public void AgregarArchivo(string folio, string archivo)
        {
            arch.Agregar(folio, archivo);
        }

        #endregion

        #region Metodos privados diversos (uso interno exclusivamente) *************************************************************************
        

        private void ValidacionCeldaAlerta(TableCell celda, string tooltip)
        {
            celda.BackColor = System.Drawing.Color.Red;
            celda.ForeColor = System.Drawing.Color.White;
            celda.ToolTip = tooltip;
        }

        private void ValidacionCeldaVacia(TableCell celda, string tooltip)
        {
            celda.BackColor = System.Drawing.Color.Navy;
            celda.ForeColor = System.Drawing.Color.White;
            celda.ToolTip = tooltip;
        }

        private void ValidacionCeldaAdvertencia(TableCell celda, string tooltip)
        {
            celda.BackColor = System.Drawing.Color.Orange;
            celda.ForeColor = System.Drawing.Color.White;
            celda.ToolTip = tooltip;
        }

        private void ValidacionCeldaCURP(TableCell celda, string tooltip)
        {
            celda.BackColor = System.Drawing.Color.Purple;
            celda.ForeColor = System.Drawing.Color.White;
            celda.ToolTip = tooltip;
        }

        private bool ValidarDependencia(string nombre)
        {
            if (dep.SeleccionarPorNombre(nombre) != "")
                return true;
            else
                return false;
        }

        private bool SumaAseguradaPorNivelTabular(string suma, string nivel)
        {
            if (nivel == "G" && suma == "333")
                return true;
            else if (nivel == "H" && suma == "295")
                return true;
            else if (nivel == "I" && suma == "295")
                return true;
            else if (nivel == "J" && suma == "295")
                return true;
            else if (nivel == "K" && suma == "259")
                return true;
            else if (nivel == "L" && suma == "222")
                return true;
            else if (nivel == "M" && suma == "185")
                return true;
            else if (nivel == "N" && suma == "148")
                return true;
            else if (nivel == "O" && suma == "111")
                return true;
            else if (nivel == "P" && suma == "74")
                return true;
            else if (nivel == "CS" && suma == "266.4")
                return true;
            else
                return false;
        }

        private List<string> SumaAseguradaBasicaPotenciacion()
        {
            List<string> Listado = new List<string>() { "333", "295", "259", "222", "185", "148", "111", "74" };
            return Listado;
        }

        private List<string> SumaAseguradaPotenciada()
        {
            List<string> Listado = new List<string>() { "111", "148", "185", "222", "259", "295", "333", "444", "592", "740", "850", "1000", "34219" };
            return Listado;
        }

        private string TarifaPotenciada(string niveltabular, string sumaseguradatotal, string tipoasegurado)
        {
            string devuelto = string.Empty;

            if (tipoasegurado == "T")
            {
                foreach (var valor in TablaCalculoPotenciadaTitulares())
                {
                    if (valor.nivelTabular.Contains(niveltabular) && valor.sumaAseguradaTotal.Contains(sumaseguradatotal))
                    {
                        devuelto = valor.primaPotenciada;
                    }
                }
            }
            else if (tipoasegurado == "T")
            {
                foreach (var valor2 in TablaCalculoPotenciadaConyuges())
                {
                    if (valor2.nivelTabular.Contains(niveltabular) && valor2.sumaAseguradaTotal.Contains(sumaseguradatotal))
                    {
                        devuelto = valor2.primaPotenciada;
                    }
                }
            }
            else if (tipoasegurado == "H")
            {
                foreach (var valor3 in TablaCalculoPotenciadaHijos())
                {
                    if (valor3.nivelTabular.Contains(niveltabular) && valor3.sumaAseguradaTotal.Contains(sumaseguradatotal))
                    {
                        devuelto = valor3.primaPotenciada;
                    }
                }
            }
            else
                devuelto = "";

            return devuelto;
        }

        private string TarifaQuincenalTitulares(string nivel, double sumaasegurada, string tipoasegurado)
        {
            string calculo = "";
            double a = 0;

            double tarifaNivelG = 0;
            double tarifaNivelH = 0; //H,I,J
            double tarifaNivelK = 0;
            double tarifaNivelL = 0;
            double tarifaNivelM = 0;
            double tarifaNivelN = 0;
            double tarifaNivelO = 0;
            double tarifaNivelP = 0;
#pragma warning disable CS0219 // La variable 'tarifaNivelC' está asignada pero su valor nunca se usa
            double tarifaNivelC = 0;
#pragma warning restore CS0219 // La variable 'tarifaNivelC' está asignada pero su valor nunca se usa

            if (tipoasegurado == "T")
            {
                tarifaNivelG = 307.40;
                tarifaNivelH = 303.92; //H,I,J
                tarifaNivelK = 300.44;
                tarifaNivelL = 294.64;
                tarifaNivelM = 288.84;
                tarifaNivelN = 280.72;
                tarifaNivelO = 269.12;
                tarifaNivelP = 257.52;
            }
            if (tipoasegurado == "C")
            {
                tarifaNivelG = 502.28;
                tarifaNivelH = 496.48; //H,I,J
                tarifaNivelK = 487.20;
                tarifaNivelL = 477.92;
                tarifaNivelM = 466.32;
                tarifaNivelN = 454.72;
                tarifaNivelO = 436.16;
                tarifaNivelP = 416.44;
                tarifaNivelC = 488.36;
            }
            if (tipoasegurado == "H")
            {
                tarifaNivelG = 190.24;
                tarifaNivelH = 184.44; //H,I,J
                tarifaNivelK = 180.96;
                tarifaNivelL = 169.36;
                tarifaNivelM = 151.96;
                tarifaNivelN = 102.08;
                tarifaNivelO = 99.76;
                tarifaNivelP = 93.96;
            }

            switch (nivel)
            {
                case "G":
                    a = tarifaNivelG * 6;
                    calculo = a.ToString();
                    break;
                case "H":
                case "I":
                case "J":
                    a = tarifaNivelH * 6;
                    calculo = a.ToString();
                    break;
                case "K":
                    a = tarifaNivelK * 6;
                    calculo = a.ToString();
                    break;
                case "L":
                    a = tarifaNivelL * 6;
                    calculo = a.ToString();
                    break;
                case "M":
                    a = tarifaNivelM * 6;
                    calculo = a.ToString();
                    break;
                case "N":
                    a = tarifaNivelN * 6;
                    calculo = a.ToString();
                    break;
                case "O":
                    a = tarifaNivelO * 6;
                    calculo = a.ToString();
                    break;
                case "P":
                    a = tarifaNivelP * 6;
                    calculo = a.ToString();
                    break;
                default:
                    calculo = "0";
                    break;

            }
            return calculo;
        }

        private List<string> TipoAsegurado()
        {
            List<string> Listado = new List<string>() { "T", "C", "H", "A", "S" };
            return Listado;
        }

        private List<string> NivelTabular()
        {
            List<string> Listado = new List<string>() { "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "CS" };
            return Listado;
        }

        public bool Municipios(string entidad, string municipio)
        {
            if (mun.SeleccionarPorEntidadMunicipio(entidad, municipio).Rows.Count > 0)
                return true;
            else
                return false;        
        }

        public List<TablasPotenciacion> TablaCalculoPotenciadaTitulares()
        {
            List<TablasPotenciacion> listado = new List<TablasPotenciacion>() {
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "295", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "333", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "444", primaPotenciada = "24.36" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "592", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "740", primaPotenciada = "40.60" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "850", primaPotenciada = "95.12" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "1000", primaPotenciada = "99.76" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "34219", primaPotenciada = "212.28" },

            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "295", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "333", primaPotenciada = "17.40" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "444", primaPotenciada = "26.68" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "592", primaPotenciada = "35.96" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "740", primaPotenciada = "44.08" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "850", primaPotenciada = "97.44" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "1000", primaPotenciada = "103.24" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "34219", primaPotenciada = "215.76" },

            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "295", primaPotenciada = "4.64" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "333", primaPotenciada = "30.16" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "444", primaPotenciada = "44.08" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "592", primaPotenciada = "58.00" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "740", primaPotenciada = "69.60" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "850", primaPotenciada = "149.64" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "1000", primaPotenciada = "157.76" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "34219", primaPotenciada = "339.88" },

            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "259", primaPotenciada = "6.96" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "295", primaPotenciada = "11.60" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "333", primaPotenciada = "35.96" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "444", primaPotenciada = "51.04" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "592", primaPotenciada = "64.96" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "740", primaPotenciada = "75.40" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "850", primaPotenciada = "154.28" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "1000", primaPotenciada = "162.40" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "34219", primaPotenciada = "359.60" },

            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "222", primaPotenciada = "9.28" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "259", primaPotenciada = "16.24" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "295", primaPotenciada = "20.88" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "333", primaPotenciada = "45.24" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "444", primaPotenciada = "60.32" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "592", primaPotenciada = "75.40" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "740", primaPotenciada = "85.84" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "850", primaPotenciada = "160.08" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "1000", primaPotenciada = "168.20" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "34219", primaPotenciada = "382.80" },

            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "185", primaPotenciada = "9.28" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "222", primaPotenciada = "18.56" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "259", primaPotenciada = "24.36" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "295", primaPotenciada = "29.00" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "333", primaPotenciada = "52.20" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "444", primaPotenciada = "66.12" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "592", primaPotenciada = "78.88" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "740", primaPotenciada = "89.32" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "850", primaPotenciada = "153.12" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "1000", primaPotenciada = "162.40" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "34219", primaPotenciada = "380.48" },

            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "148", primaPotenciada = "10.44" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "185", primaPotenciada = "19.72" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "222", primaPotenciada = "27.84" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "259", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "295", primaPotenciada = "37.12" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "333", primaPotenciada = "59.16" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "444", primaPotenciada = "71.92" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "592", primaPotenciada = "84.68" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "740", primaPotenciada = "93.96" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "850", primaPotenciada = "147.32" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "1000", primaPotenciada = "155.44" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "34219", primaPotenciada = "393.24" },

            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal= "111", primaPotenciada = "13.92"},
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "148", primaPotenciada = "24.36" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "185", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "222", primaPotenciada = "41.76" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "259", primaPotenciada = "47.56" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "295", primaPotenciada = "51.04" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "333", primaPotenciada = "55.68" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "444", primaPotenciada = "85.84" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "592", primaPotenciada = "96.28" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "740", primaPotenciada = "106.72" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "850", primaPotenciada = "151.96" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "1000", primaPotenciada = "160.08" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "34219", primaPotenciada = "397.88" }

            };
            return listado;
        }

        public List<TablasPotenciacion> TablaCalculoPotenciadaConyuges()
        {
            List<TablasPotenciacion> listado = new List<TablasPotenciacion>() {
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "295", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "333", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "444", primaPotenciada = "9.28" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "592", primaPotenciada = "18.56" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "740", primaPotenciada = "25.52" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "850", primaPotenciada = "59.16" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "1000", primaPotenciada = "62.64" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "34219", primaPotenciada = "134.56" },

            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "295", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "333", primaPotenciada = "3.48" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "444", primaPotenciada = "12.76" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "592", primaPotenciada = "22.04" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "740", primaPotenciada = "29.00" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "850", primaPotenciada = "63.80" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "1000", primaPotenciada = "68.44" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "34219", primaPotenciada = "142.68" },

            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "295", primaPotenciada = "4.64" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "333", primaPotenciada = "9.28" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "444", primaPotenciada = "24.36" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "592", primaPotenciada = "38.28" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "740", primaPotenciada = "47.56" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "850", primaPotenciada = "102.08" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "1000", primaPotenciada = "107.88" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "34219", primaPotenciada = "233.16" },

            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "259", primaPotenciada = "6.96" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "295", primaPotenciada = "11.60" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "333", primaPotenciada = "16.24" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "444", primaPotenciada = "31.32" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "592", primaPotenciada = "44.08" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "740", primaPotenciada = "54.52" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "850", primaPotenciada = "110.20" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "1000", primaPotenciada = "116.00" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "34219", primaPotenciada = "257.52" },

            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "222", primaPotenciada = "9.28" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "259", primaPotenciada = "16.24" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "295", primaPotenciada = "20.88" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "333", primaPotenciada = "25.52" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "444", primaPotenciada = "44.60" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "592", primaPotenciada = "54.52" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "740", primaPotenciada = "63.80" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "850", primaPotenciada = "118.32" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "1000", primaPotenciada = "125.28" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "34219", primaPotenciada = "284.20" },

            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "185", primaPotenciada = "8.12" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "222", primaPotenciada = "18.56" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "259", primaPotenciada = "24.36" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "295", primaPotenciada = "29.00" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "333", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "444", primaPotenciada = "46.40" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "592", primaPotenciada = "59.16" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "740", primaPotenciada = "69.60" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "850", primaPotenciada = "117.16" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "1000", primaPotenciada = "125.28" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "34219", primaPotenciada = "293.48" },

            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "148", primaPotenciada = "11.60" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "185", primaPotenciada = "19.72" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "222", primaPotenciada = "27.84" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "259", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "295", primaPotenciada = "38.28" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "333", primaPotenciada = "41.76" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "444", primaPotenciada = "55.68" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "592", primaPotenciada = "67.28" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "740", primaPotenciada = "75.40" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "850", primaPotenciada = "118.32" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "1000", primaPotenciada = "125.28" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "34219", primaPotenciada = "329.44" },

            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal= "111", primaPotenciada = "13.92"},
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "148", primaPotenciada = "25.52" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "185", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "222", primaPotenciada = "41.76" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "259", primaPotenciada = "47.56" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "295", primaPotenciada = "52.20" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "333", primaPotenciada = "55.68" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "444", primaPotenciada = "69.60" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "592", primaPotenciada = "81.20" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "740", primaPotenciada = "89.32" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "850", primaPotenciada = "126.44" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "1000", primaPotenciada = "133.40" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "34219", primaPotenciada = "332.92" },

            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "295", primaPotenciada = "4.64" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "333", primaPotenciada = "6.96" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "444", primaPotenciada = "22.04" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "592", primaPotenciada = "34.80" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "740", primaPotenciada = "45.24" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "850", primaPotenciada = "98.60" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "1000", primaPotenciada = "104.40" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "34219", primaPotenciada = "221.56" }

            };
            return listado;
        }

        public List<TablasPotenciacion> TablaCalculoPotenciadaHijos()
        {
            List<TablasPotenciacion> listado = new List<TablasPotenciacion>() {
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "295", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "333", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "444", primaPotenciada = "3.48" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "592", primaPotenciada = "8.12" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "740", primaPotenciada = "12.76" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "850", primaPotenciada = "17.40" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "1000", primaPotenciada = "19.72" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "34219", primaPotenciada = "49.88" },

            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "295", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "333", primaPotenciada = "1.16" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "444", primaPotenciada = "5.80" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "592", primaPotenciada = "10.44" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "740", primaPotenciada = "15.08" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "850", primaPotenciada = "19.72" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "1000", primaPotenciada = "22.04" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "34219", primaPotenciada = "54.52" },

            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "295", primaPotenciada = "2.32" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "333", primaPotenciada = "5.80" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "444", primaPotenciada = "11.60" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "592", primaPotenciada = "18.56" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "740", primaPotenciada = "24.36" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "850", primaPotenciada = "31.32" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "1000", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "34219", primaPotenciada = "88.16" },

            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "259", primaPotenciada = "10.44" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "295", primaPotenciada = "12.76" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "333", primaPotenciada = "16.24" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "444", primaPotenciada = "22.04" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "592", primaPotenciada = "27.84" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "740", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "850", primaPotenciada = "38.28" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "1000", primaPotenciada = "42.92" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "34219", primaPotenciada = "102.08" },

            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "222", primaPotenciada = "15.08" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "259", primaPotenciada = "24.36" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "295", primaPotenciada = "27.84" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "333", primaPotenciada = "30.16" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "444", primaPotenciada = "35.96" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "592", primaPotenciada = "41.76" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "740", primaPotenciada = "48.72" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "850", primaPotenciada = "53.36" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "1000", primaPotenciada = "60.32" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "34219", primaPotenciada = "126.44" },

            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "185", primaPotenciada = "34.80" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "222", primaPotenciada = "48.72" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "259", primaPotenciada = "58.00" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "295", primaPotenciada = "60.32" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "333", primaPotenciada = "63.80" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "444", primaPotenciada = "69.60" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "592", primaPotenciada = "74.24" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "740", primaPotenciada = "81.20" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "850", primaPotenciada = "87.00" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "1000", primaPotenciada = "97.44" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "34219", primaPotenciada = "185.60" },

            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "148", primaPotenciada = "3.48" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "185", primaPotenciada = "35.96" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "222", primaPotenciada = "48.72" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "259", primaPotenciada = "56.84" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "295", primaPotenciada = "59.16" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "333", primaPotenciada = "62.64" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "444", primaPotenciada = "67.28" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "592", primaPotenciada = "71.92" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "740", primaPotenciada = "77.72" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "850", primaPotenciada = "83.52" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "1000", primaPotenciada = "91.64" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "34219", primaPotenciada = "271.44" },

            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "111", primaPotenciada = "3.48"},
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "148", primaPotenciada = "5.80" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "185", primaPotenciada = "39.44" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "222", primaPotenciada = "51.04" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "259", primaPotenciada = "60.32" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "295", primaPotenciada = "62.64" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "333", primaPotenciada = "64.96" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "444", primaPotenciada = "70.76" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "592", primaPotenciada = "75.40" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "740", primaPotenciada = "81.20" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "850", primaPotenciada = "85.84" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "1000", primaPotenciada = "92.80" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "34219", primaPotenciada = "288.84" }

            };
            return listado;
        }

        #endregion

        #region Asegurados Titulares ***********************************************************************************************************

        public int AgregarAseguradoTitular(string poliza, string dependencia, string apellidopaterno, string apellidomaterno, string nombres, string fechanacimiento, string rfc,
        string curp, string sexo, string entidadfederativa, string municipio, string niveltabular, string percepcionordinariabruta, string eventual)
        {
            return at.Agregar(poliza, dependencia, apellidopaterno, apellidomaterno, nombres, fechanacimiento, rfc,
        curp, sexo, entidadfederativa, municipio, niveltabular, percepcionordinariabruta, eventual);
        }

        #endregion

        #region Coasegurados *******************************************************************************************************************

        public int AgregarCoasegurado(string apellidopaterno, string apellidomaterno, string nombres, string fechanacimiento, string curp, 
        string sexo, string fechaafiliacion, string tipo, string fechaingresocolectividad, string aseguradotitular, string estado, string curptitular,
        string certificado)
        {
            return coa.Agregar(apellidopaterno, apellidomaterno, nombres, fechanacimiento, curp, sexo, fechaafiliacion, tipo, fechaingresocolectividad, 
            aseguradotitular, estado, curptitular, "");
        }

        #endregion





    }

    public class TablasPotenciacion
    {
        public string nivelTabular { get; set; }
        public string sumaAseguradaTotal { get; set; }
        public string primaPotenciada { get; set; }

    }
}
