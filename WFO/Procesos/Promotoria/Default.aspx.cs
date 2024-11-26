using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.Procesos.Promotoria
{
    public partial class EsperaPromotoria : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Promotoria.IndicadorGeneral indicadorGeneral = new Negocio.Procesos.Promotoria.IndicadorGeneral();
        WFO.Negocio.Procesos.Promotoria.TramitesPromotoria tramitesPromotoria = new Negocio.Procesos.Promotoria.TramitesPromotoria();
        WFO.Negocio.Procesos.Promotoria.Catalogos Catalogos = new Negocio.Procesos.Promotoria.Catalogos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
                cboMonthFilter.SelectedValue = DateTime.Now.Month.ToString();

                PintaGrafica(manejo_sesion.Usuarios.IdUsuario, int.Parse(cboMonthFilter.SelectedValue.ToString()));
                MuestraGrafica(manejo_sesion.Usuarios.IdUsuario, manejo_sesion.Usuarios.Nombre, int.Parse(cboMonthFilter.SelectedValue.ToString()));
                LisStatusTramite();
            }
        }

        
        

        protected void MuestraGrafica(int Id, string NombrePromotoria, int Mes)
        {
            // funcionalidad oculpta  // nueva grafica obteniendo los colores desde la db


            //List<prop.IndicadorGeneral> TramitesTotales = indicadorGeneral.SeleccionaEstatusTotales(Id, Mes);

            //ltMuestraGrafica.Text = "<canvas id='myChart' width='400' height='220'></canvas>";
            //string script = "";
            //script = "var ctx = document.getElementById('myChart').getContext('2d');" +
            //         "var myChart = new Chart(ctx, {" +
            //             "type: 'bar'," +
            //             "data: {" +
            //                "labels: [";
            //                string label = "";

            //                for (int i = 0; i < TramitesTotales.Count; i++)
            //                {
            //                    label += "'" + TramitesTotales[i].Estado + "',";
            //                }

            //script += label + "]," +
            //                "datasets: [{ " +
            //                    "label: 'Tramites Promotoria: "+ NombrePromotoria + "',";
            //                    string data = "data: [";

            //                    for (int i = 0; i < TramitesTotales.Count; i++)
            //                    {
            //                        data +=  TramitesTotales[i].Totales + ",";
            //                    }

            //                   data += "],";

            //script += data;

            //script += "backgroundColor: [";
            //                    string backgroundColor = "";
            //                    for (int i = 0; i < TramitesTotales.Count; i++)
            //                    {
            //                        backgroundColor += "'" + TramitesTotales[i].BackgroundColor.Trim().ToString() + "',";
            //                    }
            //                    //backgroundColor += "'#BDC3C7'," +
            //                    //                "'#9B59B6'," +
            //                    //                "'#E74C3C'," +
            //                    //                "'#26B99A'," +
            //                    //                "'#3498DB'";

            //script += backgroundColor;
            //script += "]," +
            //                    "borderColor: [";
            //                    string borderColor = "";
            //                    for (int i = 0; i < TramitesTotales.Count; i++)
            //                    {
            //                        borderColor += "'" + TramitesTotales[i].BorderColor.Trim().ToString() + "',";
            //                    }
            //                            //"'#BDC3C7'," +
            //                            //"'#9B59B6'," +
            //                            //"'#E74C3C'," +
            //                            //"'#26B99A'," +
            //                            //"'#3498DB'";

            //script += borderColor;
            //script += "]," +
            //                    "hoverBackgroundColor: [";
            //                    string hoverBackgroundColor = "";
            //                    for (int i = 0; i < TramitesTotales.Count; i++)
            //                    {
            //                        hoverBackgroundColor += "'" + TramitesTotales[i].HoverBackgroundColor.Trim().ToString() + "',";
            //                    }
            //                        //"'#CFD4D8'," +
            //                        //"'#B370CF'," +
            //                        //"'#E95E4F'," +
            //                        //"'#36CAAB'," +|
            //                        //"'#49A9EA'";

            //script += hoverBackgroundColor;
            //script +=           "]," +
            //                    "borderWidth: 1" +
            //                "}]"+
            //            "},"+
            //            "options:"+
            //            "{"+
            //                "scales:"+
            //                "{"+
            //                    "yAxes: [{"+
            //                        "ticks:"+
            //                        "{"+
            //                            "beginAtZero: true"+
            //                        "}"+
            //                    "}]"+
            //                "}," +
            //                "bezierCurve: false,"+
            //                "animation:"+
            //                "{"+
            //                    "onComplete: descarga"+
            //                "}"+
            //             "}"+
            //        "});" +
            //        "function descarga() {"+
            //            "var url_base64jp = myChart.toBase64Image();" +
            //            "var link = document.getElementById('link');" +
            //            "link.setAttribute('href', url_base64jp);"+
            //        "}" +
            //        "";
            //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }

        protected void PintaGrafica(int Id, int Mes)
        {
            grfGrupoUno.ChartAreas["GrupoUno"].AxisX.Interval = 1;
            grfGrupoUno.ChartAreas["GrupoUno"].AxisY.Interval = 50;

            List<prop.IndicadorGeneral> TramitesTotales = indicadorGeneral.SeleccionaEstatusTotales(Id, Mes);

            grfGrupoUno.DataSource = TramitesTotales;

            // Add serie Totales
            Series serieTotales = grfGrupoUno.Series.Add("totales");
            serieTotales.ChartArea = "GrupoUno";
            serieTotales.Font = new Font("Arial", 6.5F);
            serieTotales.ChartType = SeriesChartType.Column;
            serieTotales.IsValueShownAsLabel = true;
            serieTotales.XValueMember = "Estado";
            serieTotales.YValueMembers = "Totales";
            serieTotales.CustomProperties = "ShowMarkerLines=true";
            //serieTotales.PostBackValue = "item";
            serieTotales.PostBackValue = "#VALX";
            serieTotales.IsValueShownAsLabel = true;

            grfGrupoUno.DataBind();

            serieTotales.Points[0].Color = Color.FromArgb(163, 206, 78);        // Registro
            serieTotales.Points[1].Color = Color.FromArgb(0, 144, 218);         // Proceso
            serieTotales.Points[3].Color = Color.FromArgb(255, 198, 0);         // PCI        
            serieTotales.Points[4].Color = Color.FromArgb(255, 198, 0);         // Hold
            serieTotales.Points[2].Color = Color.FromArgb(255, 198, 0);         // Suspendido
            serieTotales.Points[5].Color = Color.FromArgb(0, 97, 160);          // Ejecucion
            serieTotales.Points[6].Color = Color.FromArgb(219, 10, 91);         // Rechazo
            serieTotales.Points[7].Color = Color.FromArgb(217, 120, 123);       // Cancelado
            serieTotales.Points[8].Color = Color.FromArgb(219, 10, 91);         // Caducado
            serieTotales.Points[9].Color = Color.FromArgb(0, 163, 173);         // Envio de Poliza
        }

        private void LisStatusTramite()
        {
            List<prop.cat_statusTramite> cat_StatusTramites = Catalogos.cat_StatusTramites();
            LisEstatusTramite.DataSource = cat_StatusTramites;
            LisEstatusTramite.DataBind();
            LisEstatusTramite.DataTextField = "Nombre";
            LisEstatusTramite.DataValueField = "Nombre";
            LisEstatusTramite.DataBind();
        }

        protected void Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];

            PintaGrafica(manejo_sesion.Usuarios.IdUsuario, int.Parse(cboMonthFilter.SelectedValue.ToString()));
            MuestraGrafica(manejo_sesion.Usuarios.IdUsuario, manejo_sesion.Usuarios.Nombre, int.Parse(cboMonthFilter.SelectedValue.ToString()));
            LisStatusTramite();
        }

        protected void LisEstatusTramite_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
            PintaGrafica(manejo_sesion.Usuarios.IdUsuario, int.Parse(cboMonthFilter.SelectedValue.ToString()));
            MuestraGrafica(manejo_sesion.Usuarios.IdUsuario, manejo_sesion.Usuarios.Nombre, int.Parse(cboMonthFilter.SelectedValue.ToString()));


            string estado = Convert.ToString(LisEstatusTramite.SelectedValue.ToString());
            LabelEstado.Text = estado;
            ListaTramitesEstatus.Visible = true;

            List<prop.TramitesPromotoria> Tramites = tramitesPromotoria.ListaTramitesPromotoriaEstado(manejo_sesion.Usuarios.IdUsuario, estado, int.Parse(cboMonthFilter.SelectedValue.ToString()));

            rptTramite.DataSource = Tramites;
            rptTramite.DataBind();
        }

        protected void grfGrupoUno_Click(object sender, ImageMapEventArgs e)
        {
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
            PintaGrafica(manejo_sesion.Usuarios.IdUsuario, int.Parse(cboMonthFilter.SelectedValue.ToString()));

            string estado = Convert.ToString(e.PostBackValue);
            LabelEstado.Text = estado;
            ListaTramitesEstatus.Visible = true;

            List<prop.TramitesPromotoria> Tramites = tramitesPromotoria.ListaTramitesPromotoriaEstado(manejo_sesion.Usuarios.IdUsuario, estado, int.Parse(cboMonthFilter.SelectedValue.ToString()));

            rptTramite.DataSource = Tramites;
            rptTramite.DataBind();

            string script = "";
            script = "$('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); ";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }

        protected void rptTramite_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Consultar"))
            {
                string IdTramite = e.CommandArgument.ToString();
                Response.Redirect(EncripParametros("Id=" + IdTramite, "ConsultaTramite.aspx").URL, true);
                //Response.Redirect("ConsultaTramite.aspx?Id=" + IdTramite);
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