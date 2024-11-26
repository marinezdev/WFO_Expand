using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Procesos.Supervision
{
    public partial class sprTramitesReingresosV2 : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Operacion.UsuariosFlujo usuariosFlujo = new Negocio.Procesos.Operacion.UsuariosFlujo();

        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (IU.ManejadorSesion)Session["Sesion"];
            if (!IsPostBack)
            {
                CargaFlujos(manejo_sesion.Usuarios.IdUsuario);
            }
        }

        protected void CargaFlujos(int Id)
        {
            List<prop.UsuariosFlujo> Flujos = usuariosFlujo.SelecionarFlujo(Id);
            cbFlujos.DataSource = Flujos;
            cbFlujos.DataBind();
            cbFlujos.DataTextField = "Nombre";
            cbFlujos.DataValueField = "Id";
            cbFlujos.DataBind();
        }

        protected void imbtnConsultar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton lbtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)lbtn.NamingContainer;
            Response.Redirect("~/Supervision/OpConsultaTramite.aspx?Id=" + GV03.DataKeys[row.RowIndex].Value);
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            CargaInicial(DXFechaInicio.Text, DXFechaTermino.Text, cbFlujos.SelectedItem.Value.ToString());
        }

        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            CargarGrafico((DataTable)ViewState["Tabla2"]);
            DataTable datatable1 = new DataTable();
            DataTable datatable2 = new DataTable();
            datatable1 = (DataTable)ViewState["Tabla3"];
            datatable2 = (DataTable)ViewState["Tabla4"];

            DataView dv = new DataView(datatable1);
            dv.RowFilter = "MESA='" + Convert.ToString(e.PostBackValue) + "'";
            string cadena = string.Empty;
            foreach (DataRowView dr in dv)
            {
                cadena += dr[0] + ",";
            }

            if (cadena.Length == 0)
                return;
            cadena = cadena.Substring(0, cadena.Length - 1);
            DataView dv2 = new DataView(datatable2);
            dv2.RowFilter = "Id IN(" + cadena + ")";
            GV03.DataSource = dv2;
            GV03.DataBind();
        }

        protected void CargaInicial(string fechainicial, string fechafinal, string tipotramite)
        {
            if (DXFechaInicio.Text == "" || DXFechaTermino.Text == "" || cbFlujos.SelectedItem.Value.ToString() == "0")
                return;

            DataSet ds = new DataSet();
            //tramite.ReporteTramitesReingresos(fechainicial, fechafinal, tipotramite);
            //wfiplib.LlenarControles.LlenarGridView(ref GV01, ds.Tables[0]);
            ds = rprV2.CargarGrafico(ref Chart1, manejo_sesion.Usuarios.IdUsuario.ToString(), DXFechaInicio.Text, DXFechaTermino.Text, cbFlujos.SelectedItem.Value.ToString());

            //Carga gráfico
            //CargarGrafico(ds.Tables[1]);
            Funciones.LlenarControles.LlenarGridView(ref GV01, ds.Tables[0]);
            ViewState["Tabla2"] = ds.Tables[1];
            ViewState["Tabla3"] = ds.Tables[2];
            //ViewState["Tabla4"] = ds.Tables[3];
        }

        protected void CargarGrafico(DataTable datatable)
        {
            //Carga gráfico
            Chart1.DataSource = datatable;

            Series serie01 = Chart1.Series.Add("Procesos Realizados");
            serie01.ChartArea = "GrupoUno";
            serie01.Font = new Font("Arial", 6.5F);
            serie01.ChartType = SeriesChartType.Bar;
            serie01.Color = Color.SteelBlue;
            serie01.IsValueShownAsLabel = true;
            serie01.XValueMember = "Nombre";
            serie01.YValueMembers = "ProcesosRealizados";
            serie01.CustomProperties = "ShowMarkerLines=true";
            serie01.PostBackValue = "#VALX";
            serie01.IsValueShownAsLabel = true;

            Series serie02 = Chart1.Series.Add("Total de Reingresos");
            serie02.ChartArea = "GrupoUno";
            serie02.Font = new Font("Arial", 6.5F);
            serie02.ChartType = SeriesChartType.Bar;
            serie02.Color = Color.Red;
            serie02.IsValueShownAsLabel = true;
            serie02.YValueMembers = "TotaldeReingresos";
            serie02.CustomProperties = "ShowMarkerLines=true";
            serie02.PostBackValue = "#VALX";
            serie02.IsValueShownAsLabel = true;

            Chart1.DataBind();
        }
    }
}