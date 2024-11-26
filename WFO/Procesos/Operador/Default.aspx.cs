using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Procesos.Operador
{
    public partial class Default : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Operacion.Mesas mesas = new Negocio.Procesos.Operacion.Mesas();
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
                log.Agregar(ex.Message + " // " + ex.Source );
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
            // PintaMesas(manejo_sesion.Usuarios.IdUsuario, IdFlujo);
            PintaMesas_v2(IdFlujo);
        }

        protected void PintaMesas_v2(int IdFlujo)
        {
            WFO.Negocio.Procesos.Operacion.MapaGeneral TableroControl = new Negocio.Procesos.Operacion.MapaGeneral();
            List<prop.MapaGeneral> WFODashboard = TableroControl.Dashboard(IdFlujo);

            string MesaUsuario = "";
            for (int i = 0; i < WFODashboard.Count; i++)
            {
                int Tramites = WFODashboard[i].TramitesDisponibles + WFODashboard[i].TramitesReingresos;

                MesaUsuario += "<div class='control-label col-md-4 col-sm-4 col-xs-6'>" +
                                    "<div class='x_panel text-center' >" +
                                     "<a onclick='TramiteProcesar(" + WFODashboard[i].IdMesa.ToString() + ")' style='color:#ffffff'>" +
                                            //"<a href='TramiteProcesar.aspx?IdMesa=" + WFODashboard[i].IdMesa.ToString() + "' style='color:#ffffff'>" +
                                            "<table style='border: 0px solid #5A738E; width:100%'>" +
                                                "<tr style='vertical-align: center; text-align: center; '>" +
                                                    "<td>" +
                                                        "<img src='" + WFODashboard[i].Icono + "'/>" +    //"<i class='fa " + Dashboard[i].Icono + " fa-5x'></i>" +
                                                        "<div class='form-group text-center'>" +
                                                            "<hr />" +
                                                            "<h2><small style='color:" + WFODashboard[i].Color + ";'>" + WFODashboard[i].Mesa + "</small></h2><br/>" +
                                                        "</div>" +
                                                    "</td>" +
                                                    "<td>" +
                                                        "<div class='form-group text-center'>" +
                                                            "<i style='color:" + WFODashboard[i].Color + ";' class='fa fa-male fa-3x'></i><strong style='font-size: 20px; color:" + WFODashboard[i].Color + "; '>&nbsp;&nbsp;&nbsp;" + WFODashboard[i].UsuariosConectados.ToString() + "<strong/><br/><br/>" +
                                                            "<i style='color:" + WFODashboard[i].Color + ";' class='fa fa-book fa-2x'></i><strong style='font-size: 20px; color:" + WFODashboard[i].Color + "; '>&nbsp;&nbsp;&nbsp;" + Tramites.ToString() + "<strong/>" +
                                                         "</div>" +
                                                    "</td>" +
                                                "</tr>" +
                                            "</table>" +
                                        "</a>" +
                                    "</div>" +
                            "</div>";
            }


            if (IdFlujo == 1)
            {
                int idProceso = 1;
                string Proceso = "Call Center";
                string icono = "../../Imagenes/CallCenter.png";
                string color = "#2A3F54";

                string numUsuariosConectados = "";
                string numTramites = "";

                MesaUsuario += "<div class='control-label col-md-4 col-sm-4 col-xs-6'>" +
                                   "<div class='x_panel text-center' >" +
                                           "<a onclick='TramiteTarea(" + idProceso.ToString() + ")' style='color:#ffffff'>" +
                                           //"<a href='TramiteTarea.aspx?IdProceso=" + idProceso.ToString() + "' style='color:#ffffff'>" +
                                           "<table style='border: 0px solid #5A738E; width:100%'>" +
                                               "<tr style='vertical-align: center; text-align: center; '>" +
                                                   "<td>" +
                                                       "<img src='" + icono + "'/>" +    //"<i class='fa " + Dashboard[i].Icono + " fa-5x'></i>" +
                                                       "<div class='form-group text-center'>" +
                                                           "<hr />" +
                                                           "<h2><small style='color:" + color + ";'>" + Proceso + "</small></h2><br/>" +
                                                       "</div>" +
                                                   "</td>" +
                                                   "<td>" +
                                                       "<div class='form-group text-center'>" +
                                                           "<i style='color:" + color + ";' class='fa fa-male fa-3x'></i><strong style='font-size: 20px; color:" + color + "; '>&nbsp;&nbsp;&nbsp;" + numUsuariosConectados + "<strong/><br/><br/>" +
                                                           "<i style='color:" + color + ";' class='fa fa-book fa-2x'></i><strong style='font-size: 20px; color:" + color + "; '>&nbsp;&nbsp;&nbsp;" + numTramites + "<strong/>" +
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


        [WebMethod]
        public static Propiedades.UrlCifrardo BusquedaId(int Id)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string parametros = "IdMesa=" + Id + ",";

            urlCifrardo.URL = Negocio.Sistema.UrlCifrardo.Encrypt(parametros.ToString());

            if (urlCifrardo.URL.Length > 0)
            {
                urlCifrardo.Result = true;
            }

            return urlCifrardo;
        }


        //protected void PintaMesas(int Id, int IdFlujo)
        //{

        //    List<prop.Mesa> MesasUsurio = mesas.SelecionarMesas(Id, IdFlujo);
        //    string MesaUsuario = "";

        //    int num = 0;
        //    string atributo ="";
        //    for (int i=0; i<MesasUsurio.Count;i++)
        //    {
        //        if(num == 0)
        //        {
        //            atributo = "Card_1";
        //            num = 1;
        //        }
        //        else
        //        {
        //            atributo = "Card_2";
        //            num = 0;
        //        }
        //        MesaUsuario += "<div class='control-label col-md-4 col-sm-4 col-xs-6'>" +
        //                    "<div class='x_panel text-center " + atributo + "'>" +
        //                        "<a href='TramiteProcesar.aspx?IdMesa=" + MesasUsurio[i].Id + "' style='color:#ffffff'>" +
        //                            "<i class='fa " + MesasUsurio[i].icono + " fa-5x'></i>" +
        //                            "<div class='form-group text-center'>" +
        //                                "<hr />" +
        //                                "<h2 style='color:#ffffff'><small style='color:#ffffff'>" + MesasUsurio[i].nombre +"</small></h2>" +
        //                            "</div>" +
        //                        "</a>" +
        //                    "</div>" +
        //                 "</div>";
        //    }

        //    if (IdFlujo == 1) 
        //    {
        //        atributo = "Card_3";
        //        int idProceso = 1;
        //        MesaUsuario += "<div class='control-label col-md-4 col-sm-4 col-xs-6'>" +
        //                    "<div class='x_panel text-center " + atributo + "'>" +
        //                        "<a href='TramiteTarea.aspx?IdProceso=" + idProceso.ToString() + "' style='color:#ffffff'>" +
        //                            "<i class='fa fa-phone fa-5x'></i>" +
        //                            "<div class='form-group text-center'>" +
        //                                "<hr />" +
        //                                "<h2 style='color:#ffffff'><small style='color:#ffffff'>CALL CENTER</small></h2>" +
        //                            "</div>" +
        //                        "</a>" +
        //                    "</div>" +
        //                 "</div>";
        //    }


        //    MesasLiteral.Text = MesaUsuario;
        //}
    }
}