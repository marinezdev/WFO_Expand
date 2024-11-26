using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades;

namespace WFO.AccesoDatos.Sistema
{
    public class RolAcceso
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.RolAcceso> Seleccionar()
        {
            b.ExecuteCommandSP("RolAcceso_Seleccionar");
            List<prop.RolAcceso> resultado = new List<prop.RolAcceso>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.RolAcceso item = new prop.RolAcceso()
                {
                    IdRol = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdRol"].ToString()),
                    RutaAcceso = reader["RutaAcceso"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public prop.RolAcceso SeleccionarPorId(int id)
        {
            b.ExecuteCommandSP("RolAcceso_Seleccionar_PorId");
            b.AddParameter("@idrol", id, SqlDbType.Int);
            prop.RolAcceso resultado = new prop.RolAcceso();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.RutaAcceso = reader["RutaAcceso"].ToString();
                resultado.IdRol = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdRol"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public string SeleccionarPorRol(int idrol)
        {
            b.ExecuteCommandSP("RolAcceso_Seleccionar_PorRol");
            b.AddParameter("@idrol", idrol, System.Data.SqlDbType.Int);
            return b.SelectString();
        }

        public int Agregar(string idrol, string rutaacceso)
        {
            b.ExecuteCommandSP("RolAcceso_Agregar");
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            b.AddParameter("@rutaacceso", rutaacceso, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        public int Modificar(string idrol, string rutaacceso)
        {
            b.ExecuteCommandSP("RolAcceso_Actualizar");
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            b.AddParameter("@rutaacceso", rutaacceso, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

    }
}
