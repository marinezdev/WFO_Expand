using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Operacion.Captura
{
    public class Coasegurados
    {
        public int Id { get; set; }
        public int IdAsegurado { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdTipoAsegurado { get; set; }
        public string Sexo { get; set; }
        public string Edad { get; set; }
        public string Peso { get; set; }
        public string Altura { get; set; }
        public string Interprestacion_larga { get; set; } 
        public int Examen { get; set; }
        public string ExamenEvaluacion { get; set; }
        public int IdTramite { get; set; }
    }
}
