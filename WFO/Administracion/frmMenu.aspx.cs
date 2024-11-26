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
    public partial class frmMenu : WFO.Utilerias.Comun
    {
        #region Eventos ******************************************************************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];

                if (!IsPostBack)
                {
                    CargarOpcionesMenu();
                }
            }
            catch (Exception ex)
            {
                log.Agregar("Problemas en la carga inicial en Administracion/frmMenu: " + ex.Message);
                mensajes.MostrarMensaje(this, "Ha habido un error al iniciar la página, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                fieldset01.Visible = true;
                Legend01.InnerText = "Nuevo Registro";
                TablaAgregarModificar.Visible = true;
                txtNombre.Text = "";
                txtUrl.Text = "";
                CargaPertenencias();
            }
            catch (Exception ex)
            {
                log.Agregar("Problemas en la carga inicial en Administracion/frmMenu: " + ex.Message);
                mensajes.MostrarMensaje(this, "Ha habido un error al tratar de agregar un nuevo registro, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
            }
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombre.Text == "")
                    return;
                int idmenu = 0;
                //Guardar ó modificar el nuevo registro
                if (ViewState["Id"] != null)
                    idmenu = int.Parse(ViewState["Id"].ToString());

                if (ViewState["Editar"] == null)
                {
                    if (sisMenu.Agregar(txtNombre.Text, txtUrl.Text, f.Numeros.ConvertirTextoANumeroEntero(ddlPerteneceA.SelectedValue)) > 0)
                        mensajes.MostrarMensaje(this, "Se guardó la nueva opción.");
                    else
                        mensajes.MostrarMensaje(this, "Hubo un error al tratar de guardar, avisar al administrador.");
                }
                else
                {
                    if (sisMenu.Modificar(txtNombre.Text, txtUrl.Text, f.Numeros.ConvertirTextoANumeroEntero(ddlPerteneceA.SelectedValue), idmenu) == 1)
                        mensajes.MostrarMensaje(this, "Se guardó el registro modificado.");
                    else
                        mensajes.MostrarMensaje(this, "Hubo un error al tratar de guardar la modificación, avisar al administrador.");
                }
                txtNombre.Text = "";
                fieldset01.Visible = false;
                Legend01.InnerText = "";
                BtnNuevo.Enabled = true;
                TablaAgregarModificar.Visible = false;
                CargarOpcionesMenu();
            }
            catch (Exception ex)
            {
                log.Agregar("Problemas en la carga inicial en Administracion/frmMenu: " + ex.Message);
                mensajes.MostrarMensaje(this, "Ha habido un error al guardar ó modificar el registro, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            fieldset01.Visible = false;
            Legend01.InnerText = "";
            BtnNuevo.Enabled = true;
        }

        protected void LigaEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is LinkButton)
                {
                    LinkButton btn = sender as LinkButton;
                    GridViewRow row = btn.NamingContainer as GridViewRow;
                    CargaPertenencias();
                    //Obtener detalle del registro
                    prop.Menu detalle = new prop.Menu();
                    detalle = sisMenu.SeleccionarPorId(f.Numeros.ConvertirTextoANumeroEntero(row.Cells[1].Text));
                    txtNombre.Text = detalle.Descripcion;
                    txtUrl.Text = detalle.URL;
                    ddlPerteneceA.SelectedValue = detalle.PerteneceA.ToString();
                    fieldset01.Visible = true;
                    Legend01.InnerText = "Edición de Registro";
                    BtnNuevo.Enabled = false;
                    TablaAgregarModificar.Visible = true;
                    ViewState["Id"] = row.Cells[1].Text;
                    ViewState["Editar"] = "1";
                }
            }
            catch (Exception ex)
            {
                log.Agregar("Problemas en la carga inicial en Administracion/frmMenu: " + ex.Message);
                mensajes.MostrarMensaje(this, "Ha habido un error al seleccionar el registro, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
            }
        }

        #endregion

        #region Metodos ******************************************************************************************************

        protected void CargaPertenencias()
        {
            sisMenu.SeleccionarPertenencia(ref ddlPerteneceA);
        }

        protected void CargarOpcionesMenu()
        {
            sisMenu.Seleccionar_GridView(ref GridView1);
        }




        #endregion


    }
}