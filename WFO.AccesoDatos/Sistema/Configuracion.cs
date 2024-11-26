using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades;

namespace WFO.AccesoDatos.Sistema
{
    public class Configuracion
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.Configuracion> Seleccionar()
        {
            b.ExecuteCommandQuery("Configuracion_Seleccionar");
            List<prop.Configuracion> resultado = new List<prop.Configuracion>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Configuracion item = new prop.Configuracion()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Valor = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Valor"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public prop.Configuracion SeleccionarPorId(int id)
        {
            b.ExecuteCommandSP("Configuracion_Seleccionar_PorId");
            b.AddParameter("@id", id, SqlDbType.Int);
            prop.Configuracion resultado = new prop.Configuracion();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString());
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.Valor = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Valor"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int Agregar(string nombre, int valor)
        {
            b.ExecuteCommandSP("Configuracion_Agregar");
            b.AddParameter("@nombre", nombre, SqlDbType.VarChar, 50);
            b.AddParameter("@valor", valor, SqlDbType.Int);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public int Modificar(int id, string nombre, int valor)
        {
            b.ExecuteCommandSP("Configuracion_Actualizar");
            b.AddParameter("@id", id, SqlDbType.Int);
            b.AddParameter("@nombre", nombre, SqlDbType.VarChar, 50);
            b.AddParameter("@valor", valor, SqlDbType.Int);
            return b.InsertUpdateDeleteWithTransaction();
        }

    }
}
