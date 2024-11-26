using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.SupervisionGeneral
{
    public class TramiteProcesar
    {
        public int IdTramite { get; set; }
        public string Folio { get; set; }
        public int IdTipoTramite { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaTermino { get; set; }
        public int IdStatus { get; set; }
        public string StatusTramite { get; set; }
        public int IdPromotoria { get; set; }
        public string Promotoria { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public int IdAgente { get; set; }
        public string Agente { get; set; }
        public string NumeroOrden { get; set; }
        public string FechaSolicitud { get; set; }
        public int idRamo { get; set; }
        public int idPrioridad { get; set; }
        public string Prioridad { get; set; }
        public string IdSisLegados { get; set; }
        public string kwik { get; set; }
        public int TipoPersona { get; set; }
        public string Contratante { get; set; }
        public string ContratanteNombre { get; set; }
        public string ContratanteApPaterno { get; set; }
        public string ContratanteApMaterno { get; set; }
        public string ContratanteSexo { get; set; }
        public string SexoContratante { get; set; }
        public string FNacimientoContratante { get; set; }
        public string RFCContratante { get; set; }
        public string FechaConst { get; set; }
        public string Nacionalidad { get; set; }
        public string Nacion { get; set; }
        public string Titular { get; set; }
        public string TitularNombre { get; set; }
        public string TitularApPat { get; set; }
        public string TitularApMat { get; set; }
        public string TitularSexo { get; set; }
        public string SexoTitular { get; set; }
        public string FNacimientoTitular { get; set; }
        public string TitularNacionalidad { get; set; }
        public string PrimaCotizacion { get; set; }
        public int IdMoneda { get; set; }
        public string Moneda { get; set; }
        public string TitularContratante { get; set; }
        public string Observaciones { get; set; }
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public int IdSubProducto { get; set; }
        public string SubProducto { get; set; }
    }
}
