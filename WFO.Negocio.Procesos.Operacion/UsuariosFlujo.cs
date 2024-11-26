using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Negocio.Procesos.Operacion
{
    public class UsuariosFlujo
    {
        AccesoDatos.Procesos.Operacion.UsuariosFlujo usuariosFlujo = new AccesoDatos.Procesos.Operacion.UsuariosFlujo();

        public List<prop.UsuariosFlujo> SelecionarFlujo(int Id_Usuario)
        {
            return usuariosFlujo.SelecionarFlujo(Id_Usuario);
        }

        public List<prop.UsuariosFlujo> SelecionarFlujoSabana(int Id_Usuario)
        {
            return usuariosFlujo.SelecionarFlujoSabana(Id_Usuario);
        }
    }
}
