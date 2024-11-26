using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.Negocio.Procesos.Operacion
{
    public class Bitacora
    {
        AccesoDatos.Procesos.Operacion.Bitacora bitacora = new AccesoDatos.Procesos.Operacion.Bitacora();

        public List<prop.bitacora> ConsultaBitacoraPublica(int Id)
        {
            return bitacora.ConsultaBitacoraPublica(Id);
        }

        public List<prop.bitacora> ConsultaBitacoraPrivada(int Id)
        {
            return bitacora.ConsultaBitacoraPrivada(Id);
        }
    }
}
