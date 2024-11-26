using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Promotoria
{
    public class cat_DocRecEmicion
    {
        public int Id { get; set; }
        public int IdTramiteTipo { get; set; }
        public int TipoPersona { get; set; }
        public string Documentos { get; set;}
    }
}
