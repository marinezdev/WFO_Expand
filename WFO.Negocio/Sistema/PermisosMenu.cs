using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades;

namespace WFO.Negocio.Sistema
{
    public class PermisosMenu
    {
        AccesoDatos.Sistema.PermisosMenu pm = new AccesoDatos.Sistema.PermisosMenu();

        public List<prop.PermisosMenu> Seleccionar()
        {
            return pm.Seleccionar();
        }

        public int Agregar(prop.PermisosMenu pem)
        {
            return pm.Agregar(pem);
        }

        public int Actualizar(int idrol, int idmenu, int activo)
        {
            return pm.Modificar(idrol, idmenu, activo);
        }



    }
}
