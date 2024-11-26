using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Negocio.Procesos.Operacion
{
    public class MapaGeneral
    {
        AccesoDatos.Procesos.Operacion.MapaGeneral TableroControl = new AccesoDatos.Procesos.Operacion.MapaGeneral();

        public List<prop.MapaGeneral> Dashboard(int IdFlujo)
        {
            return TableroControl.getDashboard(IdFlujo);
        }

        public List<prop.MapaGeneral> DashboardMesa(int IdFlujo, int IdMesa)
        {
            return TableroControl.getDashboardMesa(IdFlujo, IdMesa);
        }

        public List<prop.MapaGeneral> DashboardMesaDetalle(int IdFlujo, int IdMesa)
        {
            return TableroControl.getDashboardMesaDetalle(IdFlujo, IdMesa);
        }

        public List<prop.MapaGeneralMesaDetalleTramite> getDashboardMesaDetalleTramite(int IdFlujo, int IdMesa)
        {
            return TableroControl.getDashboardMesaDetalleTramite(IdFlujo, IdMesa);
        }
        
    }
}
