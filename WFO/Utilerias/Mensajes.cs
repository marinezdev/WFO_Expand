using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace WFO.Utilerias
{
    public class Mensajes
    {
        /// <summary>
        /// Mensaje de alerta simple
        /// </summary>
        /// <param name="PaginaActual"></param>
        /// <param name="mensaje"></param>
        public void MostrarMensaje(Page PaginaActual, string mensaje)
        {
            PaginaActual.ClientScript.RegisterStartupScript(PaginaActual.GetType(), "Alerta", "javascript: alert('" + mensaje + "');", true);
        }

        public void MostrarMensajeSM(Page PaginaActual, string mensaje)
        {
            ScriptManager.RegisterStartupScript(PaginaActual, GetType(), "Alerta", "javascript: alert('" + mensaje + "');", true);
        }

        public void MostrarMensajeSM(Page PaginaActual, string mensaje, string pagina1)
        {
            ScriptManager.RegisterStartupScript(PaginaActual, GetType(), "Alerta", "javascript: alert('" + mensaje + "'); window.location.href='" + pagina1 + ";')", true);
        }

        /// <summary>
        /// Dependiendo del mensaje, lleva a una página
        /// </summary>
        /// <param name="PaginaActual"></param>
        /// <param name="mensaje"></param>
        /// <param name="pagina1"></param>
        public void MostrarMensaje(Page PaginaActual, string mensaje, string pagina1)
        {
            PaginaActual.ClientScript.RegisterStartupScript(PaginaActual.GetType(), "Alerta", "javascript: alert('" + mensaje + "'); window.location.href='" + pagina1 + "';", true);
        }


        /// <summary>
        /// Envia a una página o a otra
        /// </summary>
        /// <param name="PaginaActual"></param>
        /// <param name="mensaje"></param>
        public void MensajeConfirmacion(Page PaginaActual, string mensaje, string pagina1, string pagina2)
        {
            PaginaActual.ClientScript.RegisterStartupScript(PaginaActual.GetType(), "MensajeConfirmacion", "<script>if (confirm('" + mensaje + "')) { window.location.href='" + pagina1 + "'; } else { window.location.href='" + pagina2 + "'; }</script>");
        }


        /// <summary>
        /// En pruebas
        /// </summary>
        /// <param name="PaginaActual"></param>
        private void MensajesSM(Page PaginaActual)
        {
            //System.Web.UI debe estar disponible
            //ScriptManager.RegisterStartupScript(this, GetType(), "ConfirmSubmit", "javascript: alert('Agregue los datos a los campos');", true);
        }
    }
}