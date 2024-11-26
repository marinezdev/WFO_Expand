using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.SupervisionGeneral
{
    public class TramiteMesaAsignar
    {
        public int IdTramite { get; set; }
        public int IdTramiteMesa { get; set; }
        public string Folio { get; set; }
        public string EstatusTramite { get; set; }
        public string Mesa { get; set; }
        public string Estatus { get; set; }
        public string Usuario { get; set; }
        public string Accion { get; set; }
    }
}
