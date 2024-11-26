using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Operacion
{
    public class MapaGeneral
    {
        public int IdMesa { get; set; }
        public string Mesa { get; set; }
        public string Icono { get; set; }
        public string Color { get; set; }
        public int UsuariosConectados { get; set; }
        public int TramitesDisponibles { get; set; }
        public int TramitesReingresos { get; set; }
        public int TotalTramites { get; set; }
    }
}
