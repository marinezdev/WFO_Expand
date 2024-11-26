using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Promotoria
{
    public class insumos
    {
        public int Id { get; set; }
        public int Id_Tramite { get; set; }
        public int Id_Archivo { get; set; }
        public string NmArchivo { get; set; }
        public string NmOriginal { get; set; }
        public int Activo { get; set; }
        public string Descripcion { get; set; }
        public string RutaTemporal { get; set; }

        public string CarpetaInicial = "\\uploadInsumos\\";

#if DEBUG
        public string CarpetaArchivada = @"C:\rmf\work\ASAE\projects\GIT\WFO_EXPAM\WFO\uploadInsumos_DD\";
#else
        //public string CarpetaArchivada = "\\DocsUp\\";
        public string CarpetaArchivada = @"F:\files\WFOEXPAND\Insumos\";
#endif
    }
}
