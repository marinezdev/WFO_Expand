using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion.Captura;
using WFO.Funciones;

namespace WFO.Negocio.Procesos.Operacion.CapturaMasiva
{
    public class Tarjetas
    {
        AccesoDatos.Procesos.Operacion.Captura.Tarjetas tarjetas = new AccesoDatos.Procesos.Operacion.Captura.Tarjetas();

        public int Agregar(prop.Tarjetas tarjeta)
        {
            return tarjetas.Agregar(tarjeta);
        }

        public List<prop.Tarjetas> Tarjetas_Selecionar_PorIdTramite(int IdTramite)
        {
            return tarjetas.Tarjetas_Selecionar_PorIdTramite(IdTramite);
        }

        public int Modificar(int IdTarjeta, int IdTramite)
        {
            return tarjetas.Modificar(IdTarjeta, IdTramite);
        }
    }
}
