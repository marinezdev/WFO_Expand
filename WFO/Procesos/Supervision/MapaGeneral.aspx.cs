using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Procesos.Supervision
{
    public partial class MapaGeneral : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Operacion.Mesas mesas = new Negocio.Procesos.Operacion.Mesas();
        WFO.Negocio.Procesos.Operacion.MapaGeneral TableroControl = new Negocio.Procesos.Operacion.MapaGeneral();
        WFO.Negocio.Procesos.Operacion.UsuariosFlujo usuariosFlujo = new Negocio.Procesos.Operacion.UsuariosFlujo();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["TramitesAutomaticos"] = true;

                    if (!String.IsNullOrEmpty(Request.QueryString["msj"]))
                    {
                        if (Request.QueryString["msj"].ToString() == "1")
                        {
                            mensajes.MostrarMensaje(this, "No hay trámites disponibles...");
                        }
                    }

                    manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
                    CargaFlujos(manejo_sesion.Usuarios.IdUsuario);
                    //PintaMesas(manejo_sesion.Usuarios.IdUsuario);
                }
            }
            catch (Exception ex)
            {
                log.Agregar(ex.Message + " // " + ex.Source);
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

        protected void CargaFlujos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdFlujo = Convert.ToInt32(cbFlujos.SelectedValue.ToString());
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
            PintaMesas(IdFlujo);
        }

        protected void PintaMesas(int IdFlujo)
        {
            List<prop.MapaGeneral> WFODashboard = TableroControl.Dashboard(IdFlujo);
            
            string MesaUsuario = "";
            for (int i = 0; i < WFODashboard.Count; i++)
            {
                int Tramites = WFODashboard[i].TramitesDisponibles + WFODashboard[i].TramitesReingresos;

                MesaUsuario += "<div class='control-label col-md-4 col-sm-4 col-xs-6'>" +
                            "<div class='x_panel text-center'>" +
                                "<a href='MapaGeneralDetalle.aspx?IdFlujo=" + IdFlujo.ToString() + "&IdMesa=" + WFODashboard[i].IdMesa.ToString() + "'>" +
                                    "<table style='border: 0px solid #5A738E; width:100%'>" +
                                        "<tr style='vertical-align: center; text-align: center; '>" +
                                            "<td>" +
                                                "<i class='fa " + WFODashboard[i].Icono + " fa-5x'></i>" +
                                                "<div class='form-group text-center'>" +
                                                    "<hr />" +
                                                    "<h2><small>" + WFODashboard[i].Mesa + "</small></h2><br/>" +
                                                "</div>" +
                                            "</td>" +
                                            "<td>" +
                                                "<div class='form-group text-center'>" +
                                                    "<i class='fa fa-male fa-3x'></i>   <strong style='font-size: 20px;'>" + WFODashboard[i].UsuariosConectados.ToString() + "<strong/><br/><br/>" +
                                                    "<i class='fa fa-book fa-2x'></i>   <strong style='font-size: 20px;'>" + Tramites.ToString() + "<strong/>" + 
                                                 "</div>" +
                                            "</td>" +
                                        "</tr>" +
                                    "</table>" +
                                "</a>" +
                            "</div>" +
                         "</div>";
            }

            MesasLiteral.Text = MesaUsuario;
        }
    }
}