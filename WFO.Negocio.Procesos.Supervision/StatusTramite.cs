using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace WFO.Negocio.Procesos.Supervision
{
    public class StatusTramite
    {
        AccesoDatos.Tablas.StatusTramite statustramite = new AccesoDatos.Tablas.StatusTramite();

        public void StatusTramite_DropDownList(ref DropDownList dropdownlist)
        {
            Funciones.LlenarControles.LlenarDropDownList(ref dropdownlist, statustramite.Seleccionar(), "Nombre", "Nombre");
        }
    }
}
