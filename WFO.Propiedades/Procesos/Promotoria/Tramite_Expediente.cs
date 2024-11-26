using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Promotoria
{
    public class Tramite_Expediente
    {
        public int Id { get; set; }
        public int IdTramite { get; set; }
        public string NombreArchivo { get; set; }
        public string NombreArchivoOriginal { get; set; }
        public bool Activo { get; set; }
        public bool Fusion { get; set; }
    }
}
