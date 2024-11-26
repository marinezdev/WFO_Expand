using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades;
using f = WFO.Funciones;
using System.Globalization;

namespace WFO.Administracion
{
    public partial class frmUsuarios : WFO.Utilerias.Comun
    {
        #region Eventos ******************************************************************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (IU.ManejadorSesion)Session["Sesion"];

            if (!IsPostBack)
            {
                try
                {
                    CargarUsuarios();
                    CargarRoles();
                    //CreacionTabla();
                    //ControlesParaElPanel();
                }
                catch 
                {
                    //log.Agregar("Problemas en la carga inicial en Administracion/frmUsuario: " + ex.Message);
                    //mensajes.MostrarMensaje(this, "Ha habido un error al inicio de la página, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
                }
            }
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            
            Fieldset01.Visible = true;
            Legend01.InnerText = "Nuevo Registro";
            txtNombre.Text = "";
            txtClave.Text = "";
            txtContra.Text = "";
            ddlRoles.SelectedIndex = 0;
            txtCambio.Text = "";
            txtCorreo.Text = "";
            chkActivo.Checked = false;
            tablaAgregarModificar.Visible = true;
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Guardar nuevo registro
                prop.Usuarios usu = new prop.Usuarios();
                if (ViewState["Id"] != null)
                    usu.IdUsuario = Funciones.Numeros.ConvertirTextoANumeroEntero(ViewState["Id"].ToString());
                usu.Nombre = txtNombre.Text;
                usu.Clave = txtClave.Text;
                usu.Contraseña = txtContra.Text;
                usu.IdRol = int.Parse(ddlRoles.SelectedValue);
                usu.FechaCambioClave = txtCambio.Text;
                usu.Correo = txtCorreo.Text;
                usu.Activo = chkActivo.Checked;
                if (ViewState["Editar"] == null)
                {
                    if (sisUsrs.Agregar(usu) == 1)
                        mensajes.MostrarMensaje(this, "Se guardó el nuevo registro.");
                    else
                        mensajes.MostrarMensaje(this, "hubo un error al tratar de guardar, avisar al administrador.");
                }
                else
                {
                    if (sisUsrs.Actualizar(usu) == 1)
                        mensajes.MostrarMensaje(this, "Se guardó el registro modificado.");
                    else
                        mensajes.MostrarMensaje(this, "hubo un error al tratar de guardar la modificación, avisar al administrador.");
                }
                Fieldset01.Visible = false;
                Legend01.InnerText = "";
                tablaAgregarModificar.Visible = false;
                txtNombre.Text = "";
                txtClave.Text = "";
                txtContra.Text = "";
                ddlRoles.SelectedIndex = 0;
                txtCambio.Text = "";
                txtCorreo.Text = "";
                chkActivo.Checked = false;
                BtnAgregar.Enabled = true;
                CargarUsuarios();
            }
            catch
            {
                //log.Agregar("Problemas al guardar los datos en Administracion/frmUsuario: " + ex.Message);
                //mensajes.MostrarMensaje(this, "Ha habido un error al guardar los datos, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
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
                    BtnAgregar.Enabled = false;
                    LinkButton btn = sender as LinkButton;
                    GridViewRow row = btn.NamingContainer as GridViewRow;
                    //Obtener detalle del registro
                    prop.Usuarios detalle = new prop.Usuarios();
                    detalle = sisUsrs.SeleccionarPorId(f.Numeros.ConvertirTextoANumeroEntero(row.Cells[1].Text));
                    txtNombre.Text = detalle.Nombre;
                    txtClave.Text = detalle.Clave;
                    txtContra.Text = detalle.Contraseña;
                    ddlRoles.SelectedValue = detalle.IdRol.ToString();
                    txtCambio.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(detalle.FechaCambioClave));
                    txtCorreo.Text = detalle.Correo;
                    chkActivo.Checked = detalle.Activo;
                    Fieldset01.Visible = true;
                    Legend01.InnerText = "Edición de Registro";
                    tablaAgregarModificar.Visible = true;
                    ViewState["Id"] = row.Cells[1].Text;
                    ViewState["Editar"] = "1";
                }
                catch
                {
                    //log.Agregar("Problemas en la carga inicial en Administracion/frmRoles: " + ex.Message);
                    //mensajes.MostrarMensaje(this, "Ha habido un error al seleccionar un registro de la tabla, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
                }
            }
        }


        #endregion

        #region Metodos ******************************************************************************************************

        protected void CargarUsuarios()
        {
            sisUsrs.Seleccionar_Gridview(ref GridView1);
        }

        protected void CargarRoles()
        {
            sisRols.Roles_DropdownList(ref ddlRoles);
        }


        #endregion

        //***************** área de pruebas ******************

        protected void CreacionTabla()
        {
            sisUsrs.CreacionTabla(ref gvPrueba);
            //gvPrueba.RowCommand += this.GvPrueba_RowCommand;
        }

        private void GvPrueba_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string currentCommand = e.CommandName;
            GridViewRow row;
            //GridView grid = sender as GridView;

            int rowIndex = int.Parse(e.CommandArgument.ToString());

            if (currentCommand == "Select")
            {
                row = this.gvPrueba.Rows[rowIndex];
                row.Cells[1].BackColor = System.Drawing.Color.Red;
            }
        }

        protected void gvPrueba_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            sisUsrs.Gridview_RowCommand(sender, e);
        }

        protected void ControlesParaElPanel()
        {
            Table tabla = new Table();
            TableCell celda01 = new TableCell();
            Label Lblnombre = new Label();
            Lblnombre.Text = "Nombre:";
            celda01.HorizontalAlign = HorizontalAlign.Right;
            celda01.Controls.Add(Lblnombre);
            TableCell celda02 = new TableCell();
            TextBox txtNombre = new TextBox();
            celda02.Controls.Add(txtNombre);
            TableRow fila01 = new TableRow();
            fila01.Cells.Add(celda01);
            fila01.Cells.Add(celda02);
            tabla.Rows.Add(fila01);

            TableCell celda03 = new TableCell();
            Label LblClave = new Label();
            LblClave.Text = "Clave:";
            celda03.HorizontalAlign = HorizontalAlign.Right;
            celda03.Controls.Add(LblClave);
            TableCell celda04 = new TableCell();
            TextBox txtClave = new TextBox();
            celda04.Controls.Add(txtClave);
            TableRow fila02 = new TableRow();
            fila02.Cells.Add(celda03);
            fila02.Cells.Add(celda04);
            tabla.Rows.Add(fila02);

            TableCell celda05 = new TableCell();
            Label LblContra = new Label();
            LblContra.Text = "Contraseña:";
            celda05.HorizontalAlign = HorizontalAlign.Right;
            celda05.Controls.Add(LblContra);
            TableCell celda06 = new TableCell();
            TextBox txtContra = new TextBox();
            celda06.Controls.Add(txtContra);
            TableRow fila03 = new TableRow();
            fila03.Cells.Add(celda05);
            fila03.Cells.Add(celda06);
            tabla.Rows.Add(fila03);

            TableCell celda07 = new TableCell();
            Label LblRol = new Label();
            LblRol.Text = "Rol:";
            celda07.HorizontalAlign = HorizontalAlign.Right;
            celda07.Controls.Add(LblRol);
            TableCell celda08 = new TableCell();
            DropDownList ddlRol = new DropDownList();
            ddlRol.DataSource = sisRols.Seleccionar();
            ddlRol.DataTextField = "Nombre";
            ddlRol.DataValueField = "IdRol";
            ddlRol.DataBind();
            celda08.Controls.Add(ddlRol);
            TableRow fila04 = new TableRow();
            fila04.Cells.Add(celda07);
            fila04.Cells.Add(celda08);
            tabla.Rows.Add(fila04);

            TableCell celda09 = new TableCell();
            Label lblFecha = new Label();
            lblFecha.Text = "Fecha Cambio Clave:";
            celda09.HorizontalAlign = HorizontalAlign.Right;
            celda09.Controls.Add(lblFecha);
            TableCell celda10 = new TableCell();
            TextBox txtCambioFecha = new TextBox();
            txtCambioFecha.ID = "txtCambioFecha";
            AjaxControlToolkit.CalendarExtender calext01 = new AjaxControlToolkit.CalendarExtender();
            calext01.TargetControlID = "txtCambioFecha";
            calext01.Format = "dd/MM/yyyy";
            celda10.Controls.Add(txtCambioFecha);
            celda10.Controls.Add(calext01);
            TableRow fila05 = new TableRow();
            fila05.Cells.Add(celda09);
            fila05.Cells.Add(celda10);
            tabla.Rows.Add(fila05);

            TableCell celda11 = new TableCell();
            Label LblCorreo = new Label();
            LblCorreo.Text = "Correo Electrónico:";
            celda11.HorizontalAlign = HorizontalAlign.Right;
            celda11.Controls.Add(LblCorreo);
            TableCell celda12 = new TableCell();
            TextBox txtCorreoElectronico = new TextBox();
            celda12.Controls.Add(txtCorreoElectronico);
            TableRow fila06 = new TableRow();
            fila06.Cells.Add(celda11);
            fila06.Cells.Add(celda12);
            tabla.Rows.Add(fila06);

            TableCell celda13 = new TableCell();
            Label LblActivo = new Label();
            LblActivo.Text = "Estado:";
            celda13.HorizontalAlign = HorizontalAlign.Right;
            celda13.Controls.Add(LblActivo);
            TableCell celda14 = new TableCell();
            CheckBox chkEstado = new CheckBox();
            celda14.Controls.Add(chkEstado);
            TableRow fila07 = new TableRow();
            fila07.Cells.Add(celda13);
            fila07.Cells.Add(celda14);
            tabla.Rows.Add(fila07);

            PanelControles.Controls.Add(tabla);
        }



    }
}