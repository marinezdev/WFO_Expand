using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Operacion.Captura
{
    public class cat_bancos
    {
        public int Id { get; set; }
        public string clave { get; set; }
        public string banco { get; set; }
        public int LargoToken { get; set; }
        public int Activo { get; set; }
    }
}
