using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.SupervisionGeneral
{
    public class TramiteDetalle
    {
        public string Id { get; set; }
        public string Folio { get; set; }
        public string IdTipoTramite { get; set; }
        public string TipoTramite { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaTermino { get; set; }
        public string FechaSolicitud { get; set; }
        public string IdPromotoria { get; set; }
        public string Promotoría { get; set; }
        public string ClavePromotoria { get; set; }
        public string IdStatus { get; set; }
        public string Status { get; set; }
        public string IdUsuario { get; set; }
        public string IdAgente { get; set; }
        public string NumeroOrden { get; set; }
        public string IdPrioridad { get; set; }
        public string Prioridad { get; set; }
        public string IdSisLegados { get; set; }
        public string Kwik { get; set; }

        public string Operacion { get; set; }
        public string Producto { get; set; }
        public string Contratante { get; set; }
        public string RFC { get; set; }
        public string Titular { get; set; }
    }
}
