using System;

namespace WFO.Propiedades.Procesos.Promotoria
{
    public class TramitesCallCenter
    {
        public int IdTramite { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string folioTramite { get; set; }
        public string NumeroOrden { get; set; }
        public string TipoServicio { get; set; }
        public string Contratente { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Promotoria { get; set; }
        public string TelCelular { get; set; }
        public string TelParticular { get; set; }
        public string StatusTramite { get; set; }
    }
}