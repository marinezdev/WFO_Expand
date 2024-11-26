using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades
{
    public class TramitesPorMesa
    {
        public string Folio { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaTermino { get; set; }
        public string NumeroOrden { get; set; }
        public string FechaSolicitud { get; set; }
        public string StatusTramite { get; set; }
        public string NumeroPoliza { get; set; }
        public string DCNKWIK { get; set; }
        public string Separado { get; set; }
        public string Operador { get; set; }
        public string IdMesa { get; set; }
        public string Mesa { get; set; }
        public string StatusMesa { get; set; }
        public string ObservacionPublica { get; set; }
        public string ObservacionPrivada { get; set; }
        public string FinArchivo { get; set; }
    }
}
