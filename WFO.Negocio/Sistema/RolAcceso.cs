using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades;

namespace WFO.Negocio.Sistema
{
    public class RolAcceso
    {
        AccesoDatos.Sistema.RolAcceso ra = new AccesoDatos.Sistema.RolAcceso();

        public List<prop.RolAcceso> Seleccionar()
        {
            return ra.Seleccionar();
        }

        public void RolAcceso_Gridview(ref GridView gridview)
        {
            Funciones.LlenarControles.LlenarGridView<prop.RolAcceso>(ref gridview, ra.Seleccionar());
        }

        public prop.RolAcceso SeleccionarPorId(int id)
        {
            return ra.SeleccionarPorId(id);
        }

        public string SeleccionarPorRol(int idrol)
        {
            return ra.SeleccionarPorRol(idrol);
        }

        public int Agregar(string idrol, string rutaacceso)
        {
            return ra.Agregar(idrol, rutaacceso);
        }

        public int Modificar(string idrol, string rutaacceso)
        {
            return ra.Modificar(idrol, rutaacceso);
        }

    }
}
