using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion.Captura;
using WFO.Funciones;

namespace WFO.Negocio.Procesos.Operacion.CapturaMasiva
{
    public class AgentesDXN
    {
        AccesoDatos.Procesos.Operacion.Captura.AgentesDXN agentesDXN = new AccesoDatos.Procesos.Operacion.Captura.AgentesDXN();

        public List<prop.AgentesDXN> AgentesDXN_Selecionar_PorClave(string Clave)
        {
            return agentesDXN.AgentesDXN_Selecionar_PorClave(Clave);
        }
    }
}
