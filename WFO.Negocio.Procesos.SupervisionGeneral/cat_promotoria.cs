using System.Web.UI.WebControls;
using System.Collections.Generic;
using DevExpress.Web;

namespace WFO.Negocio.Procesos.SupervisionGeneral
{
    public class cat_promotoria : SupervisionGeneral
    {
        public void Seleccionar_DropdownList(ref DropDownList dropdownlist)
        {
            Funciones.LlenarControles.LlenarDropDownList(ref dropdownlist, catpromotoria.Seleccionar(), "Nombre", "Id");
        }

        public void Seleccionar_ASPxComboBox(ref ASPxComboBox aSPxComboBox)
        {
            Funciones.LlenarControles.LlenarASPxComboBox(ref aSPxComboBox, catpromotoria.Seleccionar(), "Nombre", "Id");
        }

        public void Seeccionar_DropDownListPorNombre(ref DropDownList dropdownlist)
        {
            Funciones.LlenarControles.LlenarDropDownList(ref dropdownlist, catpromotoria.SeleccionarPorNombre(), "Nombre", "Clave");
        }

        public List<Propiedades.Procesos.Promotoria.cat_promotoria> Listado()
        {
            return catpromotoria.Seleccionar();
        }
    }
}
