using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Negocio.Procesos.Operacion
{
    public class Pendientes
    {
        AccesoDatos.Procesos.Operacion.Pendientes pendientes = new AccesoDatos.Procesos.Operacion.Pendientes();

        public List<prop.Pendientes> SelecionarPendientes(int IdPendiente, int IdUsuario, int IdFlujo)
        {
            return pendientes.Selecionar_Pendientes_PorId(IdPendiente, IdUsuario, IdFlujo);
        }
    }
}
