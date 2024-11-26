using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Promotoria
{
    public class cat_subproducto
    {
        public int Id { get; set; }
        public int Id_CatProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
