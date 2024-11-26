using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Procesos
{
    public partial class Default : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (Session["Sesion"] == null)
                //    Response.Redirect("../Default.aspx");
                manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
            }
            catch (Exception ex)
            {
                log.Agregar("Problemas en la carga inicial en Procesos/frmDefault: " + ex.Message);
                mensajes.MostrarMensaje(this, "Ha habido un error al iniciar la página, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
            }

        }


    }
}