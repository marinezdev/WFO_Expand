using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.AccesoDatos.Procesos.Operacion
{
    public class UsuariosFlujo
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.UsuariosFlujo> SelecionarFlujo(int IdUsuario)
        {
            b.ExecuteCommandSP("UsuariosFlujo_Seleccionar_PorIdUsuario");
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            List<prop.UsuariosFlujo> resultado = new List<prop.UsuariosFlujo>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.UsuariosFlujo item = new prop.UsuariosFlujo()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    nombre = reader["Nombre"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.UsuariosFlujo> SelecionarFlujoSabana(int IdUsuario)
        {
            b.ExecuteCommandSP("UsuariosFlujoSabana_Seleccionar_PorIdUsuario");
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            List<prop.UsuariosFlujo> resultado = new List<prop.UsuariosFlujo>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.UsuariosFlujo item = new prop.UsuariosFlujo()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    nombre = reader["Nombre"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}
