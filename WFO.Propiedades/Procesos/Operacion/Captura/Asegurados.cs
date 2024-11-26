using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Operacion.Captura
{
    public class Asegurados
    {
        public int Id { get; set; }
        public int IdTramite { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string Certificado { get; set; }
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string FechaNacimiento { get; set; }
        public string Antiguedad { get; set; }
        public int TipoAsegurado { get; set; }
        public int SA_Basica { get; set; }
        public int SA_Potencia { get; set; }
        public int SA_Total { get; set; }
        public int Sexo { get; set; }
        public int Examen { get; set; }
        public string UMAN { get; set; }
        public string Clave_agente { get; set; }
        public int Registro { get; set; }
        public string Respuesta { get; set; }
        public string FechaFirmaSolicitud { get; set; }
        public string EstadoFirma { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}