using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.Negocio.Procesos.Promotoria
{
    public class IndicadorGeneral
    {
        AccesoDatos.Procesos.Promotoria.IndicadorGeneral indicadorGeneral = new AccesoDatos.Procesos.Promotoria.IndicadorGeneral();

        public List<prop.IndicadorGeneral> SeleccionaEstatusTotales(int Id, int Mes)
        {
            return indicadorGeneral.SeleccionaEstatusTotales(Id, Mes);
        }
    }
}
