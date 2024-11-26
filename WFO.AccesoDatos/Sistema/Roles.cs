using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades;

namespace WFO.AccesoDatos.Sistema
{
    public class Roles
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.Roles> Seleccionar()
        {
            b.ExecuteCommandSP("Roles_Seleccionar");
            List<prop.Roles> resultado = new List<prop.Roles>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Roles item = new prop.Roles()
                {
                    IdRol = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdRol"].ToString()),
                    Nombre = reader["Nombre"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public prop.Roles SeleccionarPorId(int id)
        {
            b.ExecuteCommandSP("Roles_Seleccionar_PorId");
            b.AddParameter("@idrol", id, SqlDbType.Int);
            prop.Roles resultado = new prop.Roles();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.IdRol = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdRol"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int Agregar(prop.Roles roles)
        {
            b.ExecuteCommandSP("Roles_Agregar");
            b.AddParameter("@nombre", roles.Nombre, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        public int Modificar(prop.Roles roles)
        {
            b.ExecuteCommandSP("Roles_Actualizar");
            b.AddParameter("@nombre", roles.Nombre, SqlDbType.NVarChar, 50);
            b.AddParameter("@idrol", roles.IdRol, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

    }
}
