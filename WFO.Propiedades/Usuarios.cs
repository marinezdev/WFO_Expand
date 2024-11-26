using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Contraseña { get; set; }
        public int IdEstado { get; set; }
        public int IdRol { get; set; }
        public string RolNombre { get; set; }
        public string FechaConectado { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaCambioClave { get; set; }
        public string Correo { get; set; }
        public string Conectado { get; set; }
        public bool Activo { get; set; }
        public int Dependencia { get; set; }
    }
}
