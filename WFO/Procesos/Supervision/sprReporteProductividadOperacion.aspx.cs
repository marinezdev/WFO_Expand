using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.Operacion;
using prosu = WFO.Propiedades.Procesos.SupervisionGeneral;

namespace WFO.Procesos.Supervision
{
    public partial class sprReporteProductividadOperacion : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Operacion.UsuariosFlujo usuariosFlujo = new Negocio.Procesos.Operacion.UsuariosFlujo();

        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (IU.ManejadorSesion)Session["Sesion"];
            if (!IsPostBack)
            {
                CalDesde.EditFormatString = "yyyy-MM-dd";
                CalDesde.Date = DateTime.Today;
                CalDesde.MaxDate = DateTime.Today;
                CalHasta.EditFormatString = "yyyy-MM-dd";
                CalHasta.Date = DateTime.Today;
                CalHasta.MaxDate = DateTime.Today;

                CargaFlujos(manejo_sesion.Usuarios.IdUsuario);

            }
        }

        protected void CargaFlujos(int Id)
        {
            List<prop.UsuariosFlujo> Flujos = usuariosFlujo.SelecionarFlujo(Id);
            cbFlujos.DataSource = Flujos;
            cbFlujos.DataBind();
            cbFlujos.TextField = "Nombre";
            cbFlujos.ValueField = "Id";
            cbFlujos.DataBind();
        }

        protected void btnFiltroMes_Click(object sender, EventArgs e)
        {
            String script;
            Mensaje.Text = "";
            string Mesas = "";
            if (CalDesde.Date <= CalHasta.Date)
            {
                int IdFlujo = Convert.ToInt32(cbFlujos.SelectedItem.Value.ToString());
                List<prosu.ProductividadMesaFlujo> productividads = productividad.Pruductividad_Top3_IdFlujo(IdFlujo,CalDesde.Date, CalHasta.Date);

                if (productividads.Count > 0)
                {
                    foreach (var datos in productividads)
                    {
                        Mesas += "<div class='col-md-6 col-xs-12'>" +
                                    "<div class='x_panel'>" +
                                        "<div class='x_title'>" +
                                            "<h2><small>MESA: <strong>" + datos.Mesa + "</strong></small></h2>" +
                                            "<ul class='nav navbar-right panel_toolbox'>" +
                                                "<li><a class='collapse-link'><i class='fa fa-chevron-up'></i></a>" +
                                                "</li>" +
                                                "<li><a class='close-link'><i class='fa fa-close'></i></a>" +
                                                "</li>" +
                                            "</ul>" +
                                        "<div class='clearfix'></div>" +
                                        "</div>" +
                                        "<div class='x_content'>" +
                                        "<br />" +
                                        "<div class='table-responsive'>" +
                                        "<table class='table table-striped jambo_table bulk_action'>" +
                                            "<thead>" +
                                                "<tr class='headings'>" +
                                                    "<th class='column-title'>Operador</th>" +
                                                    "<th class='column-title'>Total</th>" +
                                                    "<th class='column-title'>Reingresos</th>" +
                                                    "<th class='column-title'>Calidad</th>" +
                                                "</tr>" +
                                            "</thead>" +
                                            "<tbody>" +
                                                "<tr class='even pointer'>" +
                                                    "<td class=' '>" + datos.Nombre1 + "</td>" +
                                                    "<td class=' '>" + datos.Total1 + "</td>" +
                                                    "<td class=' '>" + datos.Reingresos1 + "</td>" +
                                                    "<td class=' '>" + datos.Calidad1 + "</td>" +
                                                  "</tr>" +
                                                  "<tr class='odd pointer'>" +
                                                    "<td class=' '>" + datos.Nombre2 + "</td>" +
                                                    "<td class=' '>" + datos.Total2 + "</td>" +
                                                    "<td class=' '>" + datos.Reingresos2 + "</td>" +
                                                    "<td class=' '>" + datos.Calidad2 + "</td>"+
                                                  "</tr>" +
                                                  "<tr class='even pointer'>" +
                                                    "<td class=' '>" + datos.Nombre3 + "</td>" +
                                                    "<td class=' '>" + datos.Total3 + "</td>" +
                                                    "<td class=' '>" + datos.Reingresos3 + "</td>" +
                                                    "<td class=' '>" + datos.Calidad3 + "</td>" +
                                                  "</tr>" +
                                        "</tbody>" +
                                      "</table>" +
                                    "</div>" +
                                      "<h5 class='card-title'>Fecha Inicio:" + CalDesde.Date.ToShortDateString() + " - Fecha Fin:" + CalHasta.Date.ToShortDateString() + "</h5>" +
                                    "<hr />" +
                                    "<div class='form-group'>" +
                                       "<div class='col-md-6 col-sm-6 col-xs-12'>" +
                                            "<button type = 'button' class='btn btn-primary' onclick='DetalleMesa(" + datos.Abre + "," + IdFlujo.ToString() + ");'>Mostrar detalles</button>" +
                                        "</div>" +
                                        "<div class='col-md-6 col-sm-6 col-xs-12'>" +
                                            "<button type = 'button' class='btn btn-success' onclick='DetalleMesaGraf(" + datos.Abre + "," + IdFlujo.ToString() + ");'>Mostrar gráfica</button>" +
                                        "</div>" +
                                    "</div>" +
                                  "</div>" +
                                "</div>" +
                            "</div>";
                    }

                    MesasLiteral.Text = Mesas;
                }
            }
            else
            {
                Mensaje.Text = "La fecha 'Desde' debe ser menor a la fecha 'Hasta'";
                //rptTramitesEspera.Visible = false;
            }

        }

        [WebMethod]
        public static ConsultasDetalleMesa DetalleMesa(int IdOrden, int IdFlujo, string FechaIn, string FechaFin)
        {
            DateTime CalDesde = Convert.ToDateTime(FechaIn);
            DateTime CalHasta = Convert.ToDateTime(FechaFin);

            DataTable dt = null;

            List<prosu.ProductividadMesaDetalle> productividads = (new WFO.Negocio.Procesos.SupervisionGeneral.Productividad()).Pruductividad_Mesa_IdFlujo(IdFlujo, CalDesde.Date, CalHasta.Date, IdOrden);
            
            /* LLENAR JSON PARA RETORNAR */
            ConsultasDetalleMesa jsonObject = new ConsultasDetalleMesa();
            jsonObject.consulta = new List<DetalleMesa>();
            foreach (var datos in productividads)
            {
                jsonObject.consulta.Add(new DetalleMesa()
                {
                    Nombre = datos.Nombre,
                    Total = Convert.ToInt32(datos.Total),
                    Reingreso = Convert.ToInt32(datos.Reingresos),
                    Calidad = datos.Calidad,
                    Productividad = datos.Productividad,
                });
            }

            return jsonObject;
        }

        [WebMethod]
        public static ModelGraficaPorcentaje DetalleMesaGraf(int IdOrden, int IdFlujo, string FechaIn, string FechaFin)
        {
            DateTime CalDesde = Convert.ToDateTime(FechaIn);
            DateTime CalHasta = Convert.ToDateTime(FechaFin);

            DataTable dt = null;
            dt = (new WFO.Negocio.Procesos.SupervisionGeneral.Productividad()).Pruductividad_Mesa_Grafica_IdFlujo(IdFlujo, CalDesde.Date, CalHasta.Date, IdOrden);

            /* LLENAR JSON PARA RETORNAR */
            ModelGraficaPorcentaje jsonObject = new ModelGraficaPorcentaje();
            jsonObject.tablaModels = new List<TablaModel>();
            jsonObject.tiempos = new List<PeriodosTiempo>();

            foreach (DataColumn column in dt.Columns)
            {
                jsonObject.tiempos.Add(new PeriodosTiempo()
                {
                    tiempo = column.ColumnName.ToString()
                });
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    List<TablaDatoModel> tablaDatos = new List<TablaDatoModel>();
                    for (int i = 0; i <= dt.Columns.Count - 1; i++)
                    {
                        tablaDatos.Add(new TablaDatoModel()
                        {
                            cantidad = row[i].ToString(),
                            YearCantidad = i
                        });
                    }
                    jsonObject.tablaModels.Add(new TablaModel()
                    {
                        label = row["NombreUsuario"].ToString(),
                        tablaDatos = tablaDatos
                    });
                }
            }

            return jsonObject;
        }
    }

    public class DetalleMesa
    {
        public string Nombre { get; set; }
        public int Total { get; set; }
        public int Reingreso { get; set; }
        public string Calidad { get; set; }
        public string Productividad { get; set; }
    }

    public class ConsultasDetalleMesa
    {
        public List<DetalleMesa> consulta { get; set; }
    }

    public class PeriodosTiempo
    {
        public string tiempo { get; set; }
    }

    public class TablaDatoModel
    {
        public string cantidad { get; set; }
        public int YearCantidad { get; set; }
    }

    public class TablaModel
    {
        public int Id { get; set; }
        public string label { get; set; }
        public List<TablaDatoModel> tablaDatos { get; set; }
    }

    public class ModelGraficaPorcentaje
    {
        public List<PeriodosTiempo> tiempos { get; set; }
        public List<TablaModel> tablaModels { get; set; }
    }
}