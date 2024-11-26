using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.SupervisionGeneral;

namespace WFO.Negocio.Procesos.SupervisionGeneral
{
    public class Productividad : SupervisionGeneral
    {
        public List<prop.ProductividadMesaFlujo> Pruductividad_Top3_IdFlujo(int IdFlujo, DateTime FechaIn, DateTime FechaFn)
        {
            DataTable dt = new DataTable();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            FechaIn = FechaIn.Date + In;
            FechaFn = FechaFn.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = FechaIn.ToString(formato);
            string fechaH = FechaFn.ToString(formato);

            return productividad.Pruductividad_Top3_IdFlujo(IdFlujo, fechaD, fechaH);
        }

        public List<prop.ProductividadMesaDetalle> Pruductividad_Mesa_IdFlujo(int IdFlujo, DateTime FechaIn, DateTime FechaFn, int OrdenMesa)
        {
            DataTable dt = new DataTable();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            FechaIn = FechaIn.Date + In;
            FechaFn = FechaFn.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = FechaIn.ToString(formato);
            string fechaH = FechaFn.ToString(formato);

            return productividad.Pruductividad_Mesa_IdFlujo(IdFlujo, fechaD, fechaH, OrdenMesa);
        }

        public DataTable Pruductividad_Mesa_Grafica_IdFlujo(int IdFlujo, DateTime fechaDesde, DateTime fechaHasta, int OrdenMesa)
        {
            return productividad.Pruductividad_Mesa_Grafica_IdFlujo(IdFlujo, fechaDesde, fechaHasta, OrdenMesa);
        }
    }
}
