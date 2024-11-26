using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Operacion
{
    public class Tramites
    {
        public int IdTramite { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FolioCompuesto { get; set; }
        public string NumeroOrden { get; set; }
        public string Operacion { get; set; }
        public string Producto { get; set; }
        public string Contratante { get; set; }
        public string RFC { get; set; }
        public string Titular { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estatus { get; set; }
        public string IdSisLegados { get; set; }
        public string kwik { get; set; }

        public int IdTipoTramite { get; set; }
        public string Fecha { get; set; }

        public string Promotoria { get; set; }
        public string GA { get; set; }
        public string GU { get; set; }
        public string PromotoriaZona { get; set; }
        public string TipoServicio { get; set; }
        
        public string PrimaTotal { get; set; }
        public string PrimaExcedente { get; set; }
        public string PrimaDiferencia { get; set; }

    }
}