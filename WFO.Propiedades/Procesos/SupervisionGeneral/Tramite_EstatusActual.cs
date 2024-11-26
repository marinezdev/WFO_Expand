using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.SupervisionGeneral
{
    public class Tramites_Clean {
        public int IdTramite { get; set; }
        public string Folio { get; set; }
        public string FechaRegistro { get; set; }
        public string StatusTramite { get; set; }
        public string Poliza { get; set; }
        public string KWIK { get; set; }
    }

    public class Tramites_Supendidos
    {
        public int IdTramite { get; set; }
        public string Folio { get; set; }
        public string FechaRegistro { get; set; }
        public string StatusTramite { get; set; }
        public string Poliza { get; set; }
        public string KWIK { get; set; }
        public string Promotoria { get; set; }
    }

    public class Tramites_SuspendidosTotales
    {
        public string Promotoria { get; set; }
        public string Suspendidos { get; set; }
    }

    public class Tramite_EstatusActual
    {
        public string IdTramite { get; set; }
        public string FolioCompuesto { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string StatusTramite { get; set; }
        public string IdSisLegados { get; set; }
        public string kwik { get; set; }
        public string FechaUltimoMovimiento { get; set; }
        public string TiempoUltimoStatus { get; set; }
        public string IdUsuarioUltimoMovimiento { get; set; }
        public string UsuarioUltimoMovimiento { get; set; }
        public string UltimaMesa { get; set; }
        public string FechaRegistroUltimaMesa { get; set; }
        public string FechaTerminoUltimaMesa { get; set; }
        public string TiempoUltimaMesa { get; set; }
        public string UltimaMesaNombre { get; set; }
        public string IdStatusUltimaMesa { get; set; }
        public string StatusUltimaMesa { get; set; }
        public string IdUsuarioUltimaMesa { get; set; }
        public string UsaurioUltimaMesa { get; set; }
        public string NumeroOrden { get; set; }
        public string Operacion { get; set; }
        public string Producto { get; set; }
        public string Contratante { get; set; }
        public string Titular { get; set; }
        public string RFC { get; set; }
        public string UsuarioUltimaMesa { get; set; }
        public string AgenteClave { get; set; }
        public string AgenteNombre { get; set; }
        public string UltimoMovPromo { get; set; }
        public string Institucion { get; set; }
    }
}
