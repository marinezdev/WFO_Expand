using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using f = WFO.Funciones;

namespace WFO.Administracion
{
    public partial class frmPermisosMenu : WFO.Utilerias.Comun
    {
        #region Eventos ******************************************************************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];

            if (!IsPostBack)
            {
                try
                {
                    CargarRoles();
                }
                catch (Exception ex)
                {
                    log.Agregar("Problemas en la carga inicial en Administracion/frmProcesosMenu: " + ex.Message);
                    mensajes.MostrarMensaje(this, "Ha habido un error al inicio de la página, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
                }
            }
        }

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                trOpciones.Visible = true;
                //Llenar treview
                tvwMenu.Nodes.Clear();
                sisMenu.LlenarTreeView(ref tvwMenu, f.Numeros.ConvertirTextoANumeroEntero(ddlRoles.SelectedValue)); //Se muestra todas las opciones disponibles del menu
            }
            catch (Exception ex)
            {
                log.Agregar("Problemas en la carga inicial en Administracion/frmProcesosMenu: " + ex.Message);
                mensajes.MostrarMensaje(this, "Ha habido un error al tratar de mostrar los registros, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
            }

        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            //guarda los valores asignados para el rol seleccionado
            foreach (TreeNode nodo in tvwMenu.Nodes)
            {
                sisPM.Actualizar(f.Numeros.ConvertirTextoANumeroEntero(ddlRoles.SelectedValue), f.Numeros.ConvertirTextoANumeroEntero(nodo.Value), nodo.Checked == true ? 1 : 0);
                foreach (TreeNode child in nodo.ChildNodes)
                {
                    sisPM.Actualizar(f.Numeros.ConvertirTextoANumeroEntero(ddlRoles.SelectedValue), f.Numeros.ConvertirTextoANumeroEntero(child.Value), child.Checked == true ? 1 : 0);
                }
            }
            mensajes.MostrarMensaje(this, "Se guardaron los cambios realizados.", "frmPermisosMenu.aspx");
        }

        #endregion

        #region Metodos ******************************************************************************************************

        protected void CargarRoles()
        {
            sisRols.Roles_DropdownList(ref ddlRoles);
        }

        #endregion
    }
}