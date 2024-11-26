using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Administracion
{
    public partial class frmCambiarClave : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                sisUsrs.ActualizarContraseña(manejo_sesion.Usuarios.IdUsuario, txtNueva.Text);
                sisUsrs.ActualizarDesconectarSesion(manejo_sesion.Usuarios.IdUsuario, 0);
                mensajes.MostrarMensaje(this, "Se ha cambiado la contraseña exitosamente, debe volver a entrar para que tome efecto.", "../.");
            }
            catch
            {
                //log.Agregar("Problema al intentar cambiar la clave de acceso del usuario en Administracion/frmCambiarClave: " + ex.Message);
                //LblMensajes.Text = "Ha habido un error al intentar cambiar la clave, fin de la operación.";
            }
        }
    }
}