using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Negocio.Procesos.Operacion
{
    public class PermisosMesaControles
    {
        AccesoDatos.Procesos.Operacion.PermisosMesaControles permisosMesa = new AccesoDatos.Procesos.Operacion.PermisosMesaControles();

        public prop.PermisosMesaControles PermisosMesaControles_Selecionar(int IdMesa, string NombreControl)
        {
            return permisosMesa.PermisosMesaControles_Selecionar(IdMesa, NombreControl);
        }
    }
}
