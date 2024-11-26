using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Negocio.Procesos.Operacion
{
    public class MotivosSuspension
    {
        AccesoDatos.Procesos.Operacion.MotivosSuspension _MotivosSuspension = new AccesoDatos.Procesos.Operacion.MotivosSuspension();

        public List<prop.MotivosSuspension> SelecionarMotivos(int IdMesa)
        {
            return _MotivosSuspension.SelecionarMotivos(IdMesa);
        }
        public List<prop.MotivosSuspension> SelecionarMotivosProcesos(int IdProceso)
        {
            return _MotivosSuspension.SelecionarMotivosProcesos(IdProceso);
        }
    }
}
