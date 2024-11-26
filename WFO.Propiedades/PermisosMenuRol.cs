using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades
{
    public class PermisosMenuRol
    {
        public int IdMenu { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
        public int Pertenecea { get; set; }
        public int Idrol { get; set; }
        public bool Activo { get; set; }
    }
}
