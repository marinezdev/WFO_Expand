using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades;

namespace WFO.Negocio.Sistema
{
    public class Configuracion
    {
        AccesoDatos.Sistema.Configuracion config = new AccesoDatos.Sistema.Configuracion();

        public void Seleccionar_Gridview(ref GridView gridview)
        {
            Funciones.LlenarControles.LlenarGridView<prop.Configuracion>(ref gridview, Seleccionar());
        }

        public List<prop.Configuracion> Seleccionar()
        {
            return config.Seleccionar();
        }

        public prop.Configuracion SeleccionarPorId(int id)
        {
            return config.SeleccionarPorId(id);
        }

        public int Agregar(string nombre, int valor)
        {
            return config.Agregar(nombre, valor);
        }

        public int Actualizar(int id, string nombre, int valor)
        {
            return config.Modificar(id, nombre, valor);
        }



    }
}
