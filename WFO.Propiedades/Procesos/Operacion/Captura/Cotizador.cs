using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Operacion.Captura
{
    public class Cotizador
    {
        public int Id { get; set; }
        public int pregunta1 { get; set; }
        public int pregunta2 { get; set; }
        public int pregunta3 { get; set; }
        public double estatura { get; set; }
        public double peso { get; set; }
        public int edad { get; set; }
        public int IdPadecimiento { get; set; }
        public int Excamen { get; set; }
    }
}
