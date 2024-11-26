using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.AccesoDatos.Procesos.Operacion
{
    public class Bitacora
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.bitacora> ConsultaBitacoraPublica(int Id)
        {
            b.ExecuteCommandSP("Bitacora_Publica_Selecionar_PorTramite");
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

        public List<prop.bitacora> ConsultaBitacoraPrivada(int Id)
        {
            b.ExecuteCommandSP("Bitacora_Privada_Selecionar_PorTramite");
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
                    Observacion = reader["ObservacionPrivada"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}
