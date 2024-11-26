using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.SupervisionGeneral
{
    public class TramitesIncompletos
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public string NumeroPoliza { get; set; }
        public string RFC { get; set; }
        public string Contratante { get; set; }
        public string Titular { get; set; }
        public string Mesa { get; set; }
        public string EstatusMesa { get; set; }
        public string EstatusTramite { get; set; }
    }
}
