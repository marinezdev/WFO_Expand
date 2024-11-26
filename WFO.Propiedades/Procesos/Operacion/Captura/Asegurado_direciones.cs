using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Operacion.Captura
{
    public class Asegurado_direciones
    {
        public int Id { get; set; }
        public int IdTramite { get; set; }
        public int IdEstado { get; set; }
        public string EstadoNombre { get; set; }
        public int IdPoblacion { get; set; }
        public string PoblacionNombre { get; set; }
        public int IdCP { get; set; }
        public string CP { get; set; }
        public int IdColonia { get; set; }
        public string ColoniaNombre { get; set; }
        public string Direccion { get; set; }
    }
}
