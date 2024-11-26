using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades;

namespace WFO.Administracion
{
    public partial class frmConfiguracionGeneral : WFO.Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (IU.ManejadorSesion)Session["Sesion"];

            if (!IsPostBack)
            {
                CargarConfiguracion();
            }
        }


        protected void CargarConfiguracion()
        {
            sisConfig.Seleccionar_Gridview(ref GridView1);
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtValor.Text = "";
            Fieldset01.Visible = true;
            Legend01.InnerText = "Nuevo Registro";
            BtnAgregar.Enabled = false;
            TablaAgregarModificar.Visible = true;
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //Guardar nuevo registro
                prop.Configuracion config = new prop.Configuracion();
                if (ViewState["Id"] != null)
                    config.Id = Funciones.Numeros.ConvertirTextoANumeroEntero(ViewState["Id"].ToString());
                config.Nombre = txtNombre.Text;
                config.Valor = Funciones.Numeros.ConvertirTextoANumeroEntero(txtValor.Text);

                if (ViewState["Editar"] == null)
                {
                    if (sisConfig.Agregar(config.Nombre, config.Valor) > 0)
                        mensajes.MostrarMensaje(this, "Se guardó el nuevo registro.");
                    else
                        mensajes.MostrarMensaje(this, "Hubo un error al tratar de guardar, avisar al administrador.");
                }
                else
                {
                    if (sisConfig.Actualizar(config.Id, config.Nombre, config.Valor) == 1)
                        mensajes.MostrarMensaje(this, "Se guardó el registro modificado.");
                    else
                        mensajes.MostrarMensaje(this, "Hubo un error al tratar de guardar el cambio, avisar al administrador.");
                }
                Fieldset01.Visible = false;
                Legend01.InnerText = "";
                BtnAgregar.Enabled = true;
                TablaAgregarModificar.Visible = false;
                txtNombre.Text = "";
                txtValor.Text = "";
                CargarConfiguracion();
            }
            catch (Exception ex)
            {
                log.Agregar("Problemas al guardar los datos en Administracion/frmConfiguracionGeneral: " + ex.Message);
                mensajes.MostrarMensaje(this, "Ha habido un error al guardar los datos, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
            }

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Fieldset01.Visible = false;
            Legend01.InnerText = "";
            BtnAgregar.Enabled = true;
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
                    prop.Configuracion detalle = new prop.Configuracion();
                    detalle = sisConfig.SeleccionarPorId(Funciones.Numeros.ConvertirTextoANumeroEntero(row.Cells[1].Text));
                    txtNombre.Text = detalle.Nombre;
                    txtValor.Text = detalle.Valor.ToString();
                    Fieldset01.Visible = true;
                    Legend01.InnerText = "Edición del Registro";
                    BtnAgregar.Enabled = false;
                    TablaAgregarModificar.Visible = true;
                    ViewState["Id"] = row.Cells[1].Text;
                    ViewState["Editar"] = "1";
                }
                catch (Exception ex)
                {
                    log.Agregar("Problemas en la carga inicial en Administracion/frmConfiguracionGeneral: " + ex.Message);
                    mensajes.MostrarMensaje(this, "Ha habido un error al seleccionar un registro de la tabla, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
                }
            }
        }

    }
}