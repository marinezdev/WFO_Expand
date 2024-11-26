using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades
{
    public class UrlCifrardo
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public bool Result { get; set; }

        public string IdMesa { get; set; }
        public string IdTramite { get; set; }

        public string IdTipoTramite { get; set; }

        public string IdProceso { get; set; }
        public string Procesable { get; set; }
        public string Archivo { get; set; }

    }
}
