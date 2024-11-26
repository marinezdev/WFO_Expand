using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Operacion.Captura
{
    public class Tarjetas
    {
        public int Id { get; set; }
        public int Id_tramite { get; set; }
        public int Id_banco { get; set; }
        public string banco { get; set; }
        public int Id_modo_pago { get; set; }
        public string modo_pago { get; set; }
        public int Id_periodicidad { get; set; }
        public string periodicidad { get; set; }
        public string Token { get; set; }
    }
}