using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.AccesoDatos.Procesos.Promotoria
{
    public class Bitacora
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.bitacora> ConsultaBitacora(int Id)
        {
            b.ExecuteCommandSP("Bitacora_Seleccionar_PorTramite");
            b.AddParameter("@Id_Tramite", Id, SqlDbType.Int);
            List<prop.bitacora> resultado = new List<prop.bitacora>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.bitacora item = new prop.bitacora()
                {
                    Numero = Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Numero"].ToString()),
                    Mesa = reader["Mesa"].ToString(),
                    FechaInicio = Convert.ToDateTime(reader["FechaInicio"].ToString()),
                    FechaTermino = Convert.ToDateTime(reader["FechaTermino"].ToString()),
                    Usuario = reader["Usuario"].ToString(),
                    EstatusMesa = reader["EstatusMesa"].ToString(),
                    Observacion = reader["Observacion"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.bitacora> ConsultaUltimaBitacora(int Id)
        {
            b.ExecuteCommandSP("Bitacora_Concatenar_UltimoRegistro_PorTramite");
            b.AddParameter("@Id_Tramite", Id, SqlDbType.Int);
            List<prop.bitacora> resultado = new List<prop.bitacora>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.bitacora item = new prop.bitacora()
                {
                    Observacion = reader["Observacion"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.bitacora> Bitacora_Seleccionar_IdTramite(int Id)
        {
            b.ExecuteCommandSP("Bitacora_Seleccionar_IdTramite");
            b.AddParameter("@Id_Tramite", Id, SqlDbType.Int);
            List<prop.bitacora> resultado = new List<prop.bitacora>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.bitacora item = new prop.bitacora()
                {
                    Mesa = reader["Mesa"].ToString(),
                    FechaInicio2 = reader["FechaInicio"].ToString(),
                    FechaTermino2 = reader["FechaTermino"].ToString(),
                    Usuario = reader["Usuario"].ToString(),
                    EstatusMesa = reader["EstatusMesa"].ToString(),
                    Observacion = reader["Observacion"].ToString(),
                    ObservacionPrivada = reader["ObservacionPrivada"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}