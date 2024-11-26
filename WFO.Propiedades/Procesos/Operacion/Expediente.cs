using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Operacion
{
    public class Expediente
    {
        public int IdExpediente { get; set; }
        public int IdTramite { get; set; }
        public string NmArchivo { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public string Fusion { get; set; }
    }
}
