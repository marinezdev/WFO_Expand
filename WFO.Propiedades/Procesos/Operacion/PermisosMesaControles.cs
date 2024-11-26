using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Operacion
{
    public class PermisosMesaControles
    {
        public int Id { get; set; }
        public int IdMesa { get; set; }
        public string NombreControl { get; set; }
        public int Activo { get; set; }
    }
}
