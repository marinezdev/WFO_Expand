using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.Negocio.Procesos.Promotoria
{
    public class Bitacora
    {
        AccesoDatos.Procesos.Promotoria.Bitacora bitacora = new AccesoDatos.Procesos.Promotoria.Bitacora();

        public List<prop.bitacora> ConsultaBitacora(int Id)
        {
            return bitacora.ConsultaBitacora(Id);
        }

        public List<prop.bitacora> ConsultaUltimaObervacion(int Id)
        {
            return bitacora.ConsultaUltimaBitacora(Id);
        }

        public List<prop.bitacora> Bitacora_Seleccionar_IdTramite(int Id)
        {
            return bitacora.Bitacora_Seleccionar_IdTramite(Id);
        }
    }
}
