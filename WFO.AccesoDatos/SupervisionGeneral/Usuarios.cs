using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades;
using f = WFO.Funciones;

namespace WFO.AccesoDatos.SupervisionGeneral
{
    /// <summary>
    /// Esta clase controla los usuarios por supervisor, idependiente del administrador
    /// </summary>
    public class Usuarios
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.Usuarios> SeleccionarTodo()
        {
            b.ExecuteCommandSP("Usuarios_SupervisionGeneral_Seleccionar");
            List<prop.Usuarios> resultado = new List<prop.Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Usuarios item = new prop.Usuarios()
                {
                    IdUsuario = f.Numeros.ConvertirTextoANumeroEntero(reader["IdUsuario"].ToString()),
                    Clave = reader["Clave"].ToString(),
                    FechaCambioClave = reader["FechaCambioClave"].ToString(),
                    Nombre = reader["Nombre"].ToString(),
                    RolNombre = reader["RolNombre"].ToString(),
                    Activo = bool.Parse(reader["Activo"].ToString()),
                    Conectado = reader["Conectado"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.Usuarios> SeleccionarBuscarPorNombre(string nombre)
        {
            b.ExecuteCommandSP("Usuarios_SupervisionGeneral_BuscarPorNombre");
            b.AddParameter("@nombre", "%" + nombre + "%", SqlDbType.VarChar, 50);
            List<prop.Usuarios> resultado = new List<prop.Usuarios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Usuarios item = new prop.Usuarios()
                {
                    IdUsuario = f.Numeros.ConvertirTextoANumeroEntero(reader["IdUsuario"].ToString()),
                    Clave = reader["Clave"].ToString(),
                    FechaCambioClave = reader["FechaCambioClave"].ToString(),
                    Nombre = reader["Nombre"].ToString(),
                    RolNombre = reader["RolNombre"].ToString(),
                    Activo = bool.Parse(reader["Activo"].ToString()),
                    Conectado = reader["Conectado"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int ModificarEstadoUsuarioBloqueado(string idusuario)
        {
            b.ExecuteCommandSP("Usuarios_Modificar_Desbloquear");
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }


    }
}
