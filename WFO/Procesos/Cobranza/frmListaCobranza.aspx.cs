using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Procesos.Cobranza
{
    public partial class frmListaCobranza : WFO.Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargaInicial();
        }

        protected void rptTramite_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void rptTramite_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Consultar"))
            {
                string cob = "";
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                if (commandArgs[1] == "Básica")
                    cob = "1";
                else
                    cob = "2";
                //if (commandArgs[2] != "6")
                Response.Redirect("frmCobranza.aspx?folio=" + commandArgs[0] + "&cobertura=" + cob + "&estado=" + commandArgs[2]);
            }
        }

        protected void lnkReasignar_Click(object sender, EventArgs e)
        {

        }

        protected void CargaInicial()
        {
            lco.MostrarTramites(manejo_sesion.Usuarios.IdRol.ToString(), Request["estado"], Request["cobertura"], 
                manejo_sesion.Usuarios.IdUsuario.ToString(), ref rptTramite);
        }

        protected string Estados(string dato, string cobertura)
        {
            string valor = "";
            if (cobertura == "Básica")
            {
                switch (dato)
                {
                    case "1":
                        valor = "En Trámite";
                        break;
                    case "2":
                        valor = "Suspendido";
                        break;
                    case "3":
                        valor = "En Proceso";
                        break;
                    case "4":
                        valor = "Reenvío de Trámite";
                        break;
                    case "5":
                        valor = "En Revisión";
                        break;
                    case "6":
                        valor = "Concluído";
                        break;
                    default:
                        valor = "No Definido";
                        break;
                }
            }
            else if (cobertura == "Potenciada")
            {
                switch (dato)
                {
                    case "1":
                        valor = "En Trámite";
                        break;
                    case "2":
                        valor = "Suspendido";
                        break;
                    case "3":
                        valor = "En Proceso";
                        break;
                    case "4":
                        valor = "Reenvío de Trámite";
                        break;
                    case "5":
                        valor = "En Revisión";
                        break;
                    case "6":
                        valor = "Incompleto";
                        break;
                    case "7":
                        valor = "Carta";
                        break;
                    case "8":
                        valor = "Concluído";
                        break;
                    default:
                        valor = "No Definido";
                        break;
                }
            }
            return valor;
        }

    }
}