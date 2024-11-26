using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Operacion
{
    public class MotivosSuspension
    {
        public int Id { get; set; }
        public int IdTramiteTipo { get; set; }
        public int IdMesa { get; set; }
        public int IdTramiteTipoRechazo { get; set; }
        public int IdParent { get; set; }
        public string MotivoRechazo { get; set; }
        public bool Activo { get; set; }
    }
}
