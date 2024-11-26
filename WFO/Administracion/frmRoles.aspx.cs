using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades;
using f = WFO.Funciones;

namespace WFO.Administracion
{
    public partial class frmRoles : WFO.Utilerias.Comun
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
                    log.Agregar("Problemas en la carga inicial en Administracion/frmRoles: " + ex.Message);
                    mensajes.MostrarMensaje(this, "Ha habido un error al inicio de la página, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
                }
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Fieldset01.Visible = true;
            Legend01.InnerText = "Nuevo Registro";
            TablaAgregarModificar.Visible = true;
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //Guardar ó modificar el nuevo registro
                prop.Roles rls = new prop.Roles();
                if (ViewState["Id"] != null)
                    rls.IdRol = Funciones.Numeros.ConvertirTextoANumeroEntero(ViewState["Id"].ToString());
                rls.Nombre = txtNombre.Text;

                if (ViewState["Editar"] == null)
                {
                    if (sisRols.Agregar(rls) > 0)
                        mensajes.MostrarMensaje(this, "Se guardó el nuevo Rol");
                    else
                        mensajes.MostrarMensaje(this, "hubo un error al tratar de guardar, avisar al administrador.");
                }
                else
                {
                    if (sisRols.Modificar(rls) == 1)
                        mensajes.MostrarMensaje(this, "Se guaradó el registro modificado.");
                    else
                        mensajes.MostrarMensaje(this, "hubo un error al tratar de guardar la modificación, avisar al administrador.");
                }
                txtNombre.Text = "";
                Fieldset01.Visible = false;
                Legend01.InnerText = "";
                TablaAgregarModificar.Visible = false;
                CargarRoles();
            }
            catch (Exception ex)
            {
                log.Agregar("Problemas en la carga inicial en Administracion/frmRoles: " + ex.Message);
                mensajes.MostrarMensaje(this, "Ha habido un error al guardar un registro, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Fieldset01.Visible = false;
            Legend01.InnerText = "";
            BtnNuevo.Enabled = true;
        }

        protected void LigaEditar_Click(object sender, EventArgs e)
        {
            if (sender is LinkButton)
            {
                try
                {
                    LinkButton btn = sender as LinkButton;
                    GridViewRow row = btn.NamingContainer as GridViewRow;
                    //Obtener detalle del registro
                    prop.Roles detalle = new prop.Roles();
                    detalle = sisRols.SeleccionarPorId(f.Numeros.ConvertirTextoANumeroEntero(row.Cells[1].Text));
                    txtNombre.Text = detalle.Nombre;
                    BtnNuevo.Enabled = false;
                    Fieldset01.Visible = true;
                    Legend01.InnerText = "Edición de Registro";
                    TablaAgregarModificar.Visible = true;
                    ViewState["Id"] = row.Cells[1].Text;
                    ViewState["Editar"] = "1";
                }
                catch (Exception ex)
                {
                    log.Agregar("Problemas en la carga inicial en Administracion/frmRoles: " + ex.Message);
                    mensajes.MostrarMensaje(this, "Ha habido un error al seleccionar un registro de la tabla, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
                }
            }
        }

        #endregion

        #region Metodos ******************************************************************************************************

        protected void CargarRoles()
        {
            sisRols.Roles_Gridview(ref GridView1);
        }




        #endregion


    }
}