using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.AccesoDatos.Procesos.Operacion
{
    public class Mesas
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.Mesa> SelecionarMesas(int Id_Usuario, int IdFlujo)
        {
            b.ExecuteCommandSP("Mesas_Selecionar_PorIdUsuario");
            b.AddParameter("@Id_Usuario", Id_Usuario, SqlDbType.Int);
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            List<prop.Mesa> resultado = new List<prop.Mesa>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Mesa item = new prop.Mesa()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    nombre = reader["Nombre"].ToString(),
                    icono = reader["Icono"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.Mesa> SelecionarMesasFlujo(int IdFlujo)
        {
            b.ExecuteCommandSP("Mesas_Selecionar_PorIdFlujo");
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            List<prop.Mesa> resultado = new List<prop.Mesa>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Mesa item = new prop.Mesa()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    nombre = reader["Nombre"].ToString(),
                    icono = reader["Icono"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.Procesos> SelecionarUsuariosProcesos(int IdUsuario, int IdProceso)
        {
            b.ExecuteCommandSP("Procesos_Selecionar_PorIdUsuarioIdProceso");
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@IdProceso", IdProceso, SqlDbType.Int);
            List<prop.Procesos> resultado = new List<prop.Procesos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Procesos item = new prop.Procesos()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    nombre = reader["Nombre"].ToString(),
                    icono = reader["Icono"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.Mesa> SelecionarMesasUsuarioMesa(int Id_Usuario, int Id_Mesa)
        {
            b.ExecuteCommandSP("Mesas_Selecionar_PorIdUsuarioIdMesa");
            b.AddParameter("@Id_Usuario", Id_Usuario, SqlDbType.Int);
            b.AddParameter("@Id_Mesa", Id_Mesa, SqlDbType.Int);
            List<prop.Mesa> resultado = new List<prop.Mesa>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Mesa item = new prop.Mesa()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    nombre = reader["Nombre"].ToString(),
                    icono = reader["Icono"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.Mesa> ObtenerMesasToSend(int Id_Tramite, int Id_Usuario, int Id_Mesa)
        {
            b.ExecuteCommandSP("Mesas_toSend");
            b.AddParameter("@Id_Tramite", Id_Tramite, SqlDbType.Int);
            b.AddParameter("@Id_Usuario", Id_Usuario, SqlDbType.Int);
            b.AddParameter("@Id_Mesa", Id_Mesa, SqlDbType.Int);
            List<prop.Mesa> resultado = new List<prop.Mesa>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Mesa item = new prop.Mesa()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    nombre = reader["Nombre"].ToString(),
                    icono = ""
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public DataTable Seleccionar()
        {
            b.ExecuteCommandSP("Mesa_Seleccionar_IdNombre");
            return b.Select();
        }
    }
}
