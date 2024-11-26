using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

namespace WFO.Procesos.Cobranza
{
    public partial class Default : WFO.Utilerias.Comun
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarRol(manejo_sesion.Usuarios.IdRol.ToString());
            }
        }

        protected void grfGrupoUno_Click(object sender, ImageMapEventArgs e)
        {
            string estado = Convert.ToString(e.PostBackValue);
            ltTemp.Text = estado;
            switch (estado)
            {
                case "En Trámite":
                    estado = "1";
                    break;
                case "Suspendido":
                    estado = "2";
                    break;
                case "En Proceso":
                    estado = "3";
                    break;
                case "Reenvío Trámite":
                    estado = "4";
                    break;
                case "En Revisión":
                    estado = "5";
                    break;
                case "Concluído":
                    estado = "6";
                    break;
            }

            Response.Redirect("frmListaCobranza.aspx?estado=" + estado + "&cobertura=" + RblCobertura.SelectedValue);
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            
        }

        protected void RblCobertura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RblCobertura.SelectedValue == "1")
                ValidarRol(manejo_sesion.Usuarios.IdRol.ToString());
            if (RblCobertura.SelectedValue == "2")
                ValidarRol(manejo_sesion.Usuarios.IdRol.ToString());
        }

        protected void ValidarRol(string rol)
        {

            //Validar si se le asigna o le da seguimiento a un registro asignado al analista
            string cadena = string.Empty;
            if (def.ValidarAsignacionAnalista("1", manejo_sesion.Usuarios.IdUsuario.ToString(), manejo_sesion.Usuarios.IdRol.ToString(), ref cadena))
            {
                //Enviar a Cobranza para hacer/continuar trámite
                Response.Redirect("frmCobranza.aspx?" + cadena, true);
            }
            if (def.ValidarAsignacionAnalista("2", manejo_sesion.Usuarios.IdUsuario.ToString(), manejo_sesion.Usuarios.IdRol.ToString(), ref cadena))
            {
                //Enviar a Cobranza para hacer/continuar trámite
                Response.Redirect("frmCobranza.aspx?" + cadena, true);
            }
            if (def.ValidarAsignacionAnalistaBackBasicaMetLife(manejo_sesion.Usuarios.IdUsuario.ToString(), manejo_sesion.Usuarios.IdRol.ToString(), ref cadena))
            {
                //enviar a cobranza para continuar trámite
                Response.Redirect("frmCobranza.aspx?" + cadena, true);
            }
            if (def.ValidarAsignacionAnalistaBackPotenciacionMetlife(ref cadena) && manejo_sesion.Usuarios.IdRol==8)
            {
                Response.Redirect("frmCobranza.aspx?" + cadena, true);
            }
            if (def.ValidarAsignacionAnalistaFrontPotenciacionMetLife(ref cadena) && manejo_sesion.Usuarios.IdRol == 9)
            {
                Response.Redirect("frmCobranza.aspx?" + cadena, true);
            }

            else
            {
                grfGrupoUno.ChartAreas["GrupoUno"].AxisX.IsLabelAutoFit = false;
                grfGrupoUno.ChartAreas["GrupoUno"].AxisX.Interval = 1;
                grfGrupoUno.ChartAreas["GrupoUno"].AxisY.Interval = 50;
                grfGrupoUno.ChartAreas["GrupoUno"].AxisX.LabelStyle.Font = new Font("Arial", 6.5F);
                grfGrupoUno.ChartAreas["GrupoUno"].AxisY.LabelStyle.Font = new Font("Arial", 6.5F);
                grfGrupoUno.ChartAreas["GrupoUno"].AxisX.LabelStyle.Angle = 35;
                grfGrupoUno.ChartAreas["GrupoUno"].AxisX.LabelStyle.IsStaggered = false;
                grfGrupoUno.ChartAreas["GrupoUno"].AxisX.LabelStyle.Enabled = true;
                grfGrupoUno.AntiAliasing = AntiAliasingStyles.All;

                grfGrupoUno.DataSource = def.ValidarRol(rol, manejo_sesion.Usuarios.IdUsuario.ToString(), RblCobertura.SelectedValue);
                Series serieTotales = grfGrupoUno.Series.Add("totales");
                serieTotales.ChartArea = "GrupoUno";
                serieTotales.Font = new Font("Arial", 6.5F);
                serieTotales.ChartType = SeriesChartType.Column;
                serieTotales.IsValueShownAsLabel = true;
                serieTotales.XValueMember = "Estado";
                serieTotales.YValueMembers = "Totales";
                serieTotales.CustomProperties = "ShowMarkerLines=true";
                serieTotales.PostBackValue = "#VALX";
                serieTotales.IsValueShownAsLabel = true;

                grfGrupoUno.DataBind();
            }

        }



    }
}