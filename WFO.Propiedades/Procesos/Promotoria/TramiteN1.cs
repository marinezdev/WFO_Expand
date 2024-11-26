using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Promotoria
{
    public class TramiteN1
    {
        public int IdTipoTramite { get; set; }
        public int IdPromotoria { get; set; }
        public int IdUsuario { get; set; }
        public int IdStatus { get; set; }
        public int idPrioridad { get; set; }

        public int IdInstitucion { get; set; }

        public string FechaSolicitud { get; set; }
        public int IdAgente { get; set; }
        public string NumeroOrden { get; set; }
        public int idRamo { get; set; }
        public string IdSisLegados { get; set; }
        public string kwik { get; set; }
        public int IdMoneda { get; set; }
        public int TipoPersona { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Sexo { get; set; }
        public string FechaNacimiento { get; set; }
        public string RFC { get; set; }
        public string FechaConst { get; set; }
        public int IdNacionalidad { get; set; }
        public string TitularNombre { get; set; }
        public string TitularApPat { get; set; }
        public string TitularApMat { get; set; }
        public int IdTitularNacionalidad { get; set; }
        public string TitularSexo { get; set; }
        public string TitularFechaNacimiento { get; set; }
        public double PrimaCotizacion { get; set; }
        public int TitularContratante { get; set; }
        public string Observaciones { get; set; }
        public int IdProducto { get; set; }
        public int IdSubProducto { get; set; }
        public int IdRiesgo { get; set; }
        public double SumaBasica { get; set; }
        public int HombreClave { get; set; }
        public bool OneShot { get; set; }
        public bool CPDES { get; set; }
        public string FolioCPDES { get; set; }
        public string EstatusCPDES { get; set; }
        public string Nacionalidad { get; set; }
        public string EntidadFederativa { get; set; }
        public string TitularNacionalidad { get; set; }
        public string TitularEntidad { get; set; }
        public double SumaPolizas { get; set; }
        // public double PrimaTotal { get; set; }

        /// <summary>
        ///   NUEVA CAPTURA
        /// </summary>
        public int IdTipoServicio { get; set; }
        public int IdPromotoriaZona { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }

        public string PrimaTotal { get; set; }
        public string PrimaExcedente { get; set; }
        public string PrimaDiferencia { get; set; }
    }

    public class RespuestaNuevoTramiteN1
    {
        public int Id { get; set; }
        public string Folio { get; set; }
    }
}
