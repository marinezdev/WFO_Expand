using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades;
using System.Web.UI;
using System.Drawing;
using DevExpress.Web;

namespace WFO.Negocio.Sistema
{
    public class Usuarios
    {
        AccesoDatos.Sistema.Usuarios usrs = new AccesoDatos.Sistema.Usuarios();

        public void Seleccionar_Gridview(ref GridView gridview)
        {
            Funciones.LlenarControles.LlenarGridView<prop.Usuarios>(ref gridview, usrs.SeleccionarTodo());
        }

        public prop.Usuarios SeleccionarPorId(int id)
        {
            return usrs.SeleccionarPorId(id);
        }

        public prop.Usuarios SeleccionarDetalle(string clave, string contra)
        {
            return usrs.SeleccionarDetalle(clave, contra);
        }

        public bool ValidarAcceso(string usuario)
        {
            if (usrs.SeleccionarDiasParaCambioContraseña(usuario) > 0)
                return true;
            else
                return false;
        }

        public void SeleccionarUsuarios_DropDownList(ref DropDownList dropdownlist)
        {
            Funciones.LlenarControles.LlenarDropDownList<prop.Usuarios>(ref dropdownlist, usrs.SeleccionarTodo(), "Nombre", "IdUsuario");
        }

        public void SeleccionarUsuarios_ASPxComboBox(ref ASPxComboBox aSPxComboBox)
        {
            Funciones.LlenarControles.LlenarASPxComboBox<prop.Usuarios>(ref aSPxComboBox, usrs.SeleccionarTodo(), "Nombre", "IdUsuario");
        }


        public int SeleccionarDiasParaCambioContraseña(string usuario)
        {
            return usrs.SeleccionarDiasParaCambioContraseña(usuario);
        }

        public int SeleccionarDiasQuedanAvisoCambioContraseña(string usuario)
        {
            return usrs.SeleccionarDiasQuedanAvisoCambioContraseña(usuario);
        }


        public bool Validar(string clave, string contraseña)
        {
            return usrs.Validar(clave, contraseña);
        }


        public int Agregar(prop.Usuarios usuarios)
        {
            return usrs.Agregar(usuarios);
        }

        public void AgregarUsuarioOperacion(string nombre, string correo, string clave)
        {
            usrs.AgregarUsuarioOperacion(nombre, correo, clave);
        }

        public void AgregarUsuarioPromotoria(string clavepromotoria, string nombre, string correo, string clave)
        {
            usrs.AgregarUsuarioPromotoria(clavepromotoria, nombre, correo, clave);
        }

        public void AgregarUsuarioSuper(string nombre, string correo, string clave)
        {
            usrs.AgregarUsuarioSuper(nombre, correo, clave);
        }

        public int Actualizar(prop.Usuarios usuarios)
        {
            return usrs.Modificar(usuarios);
        }

        public int ActualizarSesion(int id, int estado)
        {
            return usrs.ModificaConectarSesion(id, estado);
        }
        public void RegistroLog(string IdSesion, string Idusuario, string InicioSesion, int modo)
        {
            usrs.ModificaLogSesion(IdSesion, Idusuario, InicioSesion,modo);

        }
        /// <summary>
        /// Desconecta al usuario de la sesión
        /// </summary>
        /// <param name="id"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public int ActualizarDesconectarSesion(int id, int estado)
        {
            return usrs.ModificaDesconectarSesion(id, estado);
        }

        public int ActualizarContraseña(int id, string contrasena)
        {
            return usrs.ModificarContraseña(id, contrasena);
        }

        //Creación de la tabla desde la clase

        public void CreacionTabla(ref GridView gridview)
        {
            gridview.AutoGenerateColumns = false;
            gridview.CellPadding = 4;
            gridview.CellSpacing = 4;
            gridview.DataKeyNames = new string[] { "IdUsuario" };

            BoundField col01 = new BoundField();
            col01.DataField = "IdUsuario";
            col01.HeaderText = "Id";
            gridview.Columns.Add(col01);

            BoundField col02 = new BoundField();
            col02.DataField = "Clave";
            col02.HeaderText = "Clave";
            gridview.Columns.Add(col02);

            BoundField col03 = new BoundField();
            col03.DataField = "Nombre";
            col03.HeaderText = "Nombre";
            gridview.Columns.Add(col03);

            BoundField col04 = new BoundField();
            col04.DataField = "FechaRegistro";
            col04.HeaderText = "Fecha de Registro";
            gridview.Columns.Add(col04);

            BoundField col05 = new BoundField();
            col05.DataField = "RolNombre";
            col05.HeaderText = "Rol";
            gridview.Columns.Add(col05);

            CheckBoxField col06 = new CheckBoxField();
            col06.DataField = "Activo";
            col06.HeaderText = "Estado";
            gridview.Columns.Add(col06);

            ButtonField col07 = new ButtonField();
            col07.ButtonType = ButtonType.Link;
            col07.Text = "Editar";
            col07.CommandName = "Select";
            gridview.Columns.Add(col07);

            gridview.DataSource = usrs.SeleccionarTodo();
            gridview.DataBind();
        }

        public void Gridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string currentCommand = e.CommandName;
            GridViewRow row;
            GridView grid = sender as GridView;

            int rowIndex = int.Parse(e.CommandArgument.ToString());

            if (currentCommand == "Select")
            {
                row = grid.Rows[rowIndex];
                row.Cells[1].BackColor = Color.Red;
                row.Cells[1].ForeColor = Color.White;
            }
        }
    }
}
