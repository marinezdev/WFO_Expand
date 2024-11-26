using System.Web.UI.WebControls;

namespace WFO.Negocio.Procesos.SupervisionGeneral
{
    public class Usuarios : SupervisionGeneral
    {
        public void SeleccionarUsuarios(ref GridView gridview)
        {
            Funciones.LlenarControles.LlenarGridView(ref gridview, usuarios.SeleccionarTodo());
        }

        public void SeleccionarUsuariosPorNombre(ref GridView gridview, string nombre)
        {
            Funciones.LlenarControles.LlenarGridView(ref gridview, usuarios.SeleccionarBuscarPorNombre(nombre));
        }

        public int ActualizarUsuarioBloqueado(string idusuario)
        {
            return usuarios.ModificarEstadoUsuarioBloqueado(idusuario);
        }

    }
}
