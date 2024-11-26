using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades;

namespace WFO.Negocio.Procesos.Promotoria
{
    public class Promotoria
    {
        AccesoDatos.Procesos.Promotoria.cat_promotoria cat_Promotoria = new AccesoDatos.Procesos.Promotoria.cat_promotoria();
        public prop.Procesos.Promotoria.cat_promotoria ConsultaMegasPromotoria(int IdUsuario)
        {
            return cat_Promotoria.ConsultaMegasPromotoria(IdUsuario);
        }
    }
}
