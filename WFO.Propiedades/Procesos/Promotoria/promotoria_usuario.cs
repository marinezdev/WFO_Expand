using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Promotoria
{
    public class promotoria_usuario
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Zona { get; set; }
        public string Clave_Region { get; set; }
        public string Region { get; set; }
        public string Clave_Gerente { get; set; }
        public string Gerente { get; set; }
        public string Clave_Ejecutivo { get; set; }
        public string Ejecutivo { get; set; }
        public string Clave_Front { get; set; }
        public string Front { get; set; }
    }
}
