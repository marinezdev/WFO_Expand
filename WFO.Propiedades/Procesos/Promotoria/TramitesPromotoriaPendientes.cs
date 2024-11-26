using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Promotoria
{
    public  class TramitesPromotoriaPendientes
    {
        public int IdTramite { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Folio { get; set; }
        public string TipoFlujo { get; set; }
        public string TipoServicio { get; set; }
        public string Contratante { get; set; }
        public DateTime FechaFirmaSolicitud { get; set; }
        public string StatusTramite { get; set; }
        public string NumeroOrden { get; set; }
        public string NumeroPoliza { get; set; }
        public string PromotoriaClave { get; set; }
        public string GA { get; set; }
        public string GU { get; set; }
        public string PromotoriaZonaClave { get; set; }
        public string PrimaTotal { get; set; }
        public string PrimaExcedente { get; set; }
        public string PrimaDiferencia { get; set; }
        public string SuspensionMotivos { get; set; }
        public string ObservacionesPrivadas { get; set; }
        public string ObservacionesPublicas { get; set; }
    }
}
