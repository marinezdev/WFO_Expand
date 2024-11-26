using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Procesos.SupervisionGeneral
{
    public partial class Usuarios : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                supervisiongeneralusuarios.SeleccionarUsuarios(ref GVUsuarios);
            }
        }

        protected void LigaDesbloquear_Click(object sender, EventArgs e)
        {
            if (sender is LinkButton)
            {
                try
                {
                    LinkButton btn = sender as LinkButton;
                    GridViewRow row = btn.NamingContainer as GridViewRow;
                    //Obtener detalle del registro
                    supervisiongeneralusuarios.ActualizarUsuarioBloqueado(row.Cells[1].Text);
                    supervisiongeneralusuarios.SeleccionarUsuariosPorNombre(ref GVUsuarios, txtUsuario.Text);
                }
                catch (Exception ex)
                {
                    log.Agregar("Error al intentar desbloquear el usuario: " + ex.Message);
                }
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            supervisiongeneralusuarios.SeleccionarUsuariosPorNombre(ref GVUsuarios, txtUsuario.Text);
        }
    }
}