using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades;

namespace WFO.Negocio.Sistema
{
    public class Roles
    {
        AccesoDatos.Sistema.Roles rls = new AccesoDatos.Sistema.Roles();

        public List<prop.Roles> Seleccionar()
        {
            return rls.Seleccionar();
        }

        public void Roles_Gridview(ref GridView gridview)
        {
            Funciones.LlenarControles.LlenarGridView<prop.Roles>(ref gridview, rls.Seleccionar());
        }

        public void Roles_DropdownList(ref DropDownList dropdownlist)
        {
            WFO.Funciones.LlenarControles.LlenarDropDownList<prop.Roles>(ref dropdownlist, rls.Seleccionar(), "Nombre", "IdRol");
        }

        public prop.Roles SeleccionarPorId(int id)
        {
            return rls.SeleccionarPorId(id);
        }

        public int Agregar(prop.Roles roles)
        {
            return rls.Agregar(roles);
        }

        public int Modificar(prop.Roles roles)
        {
            return rls.Modificar(roles);
        }


    }
}
