using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.AccesoDatos.Procesos.Operacion
{
    public class Indicador_StatusMesas
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.Indicador_StatusMesas> StatusMesas(int IdTramite)
        {
            b.ExecuteCommandSP("Indicador_StatusMesas_PorIdTramite");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            List<prop.Indicador_StatusMesas> resultado = new List<prop.Indicador_StatusMesas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Indicador_StatusMesas item = new prop.Indicador_StatusMesas()
                {
                    Mesa = reader["Mesa"].ToString(),
                    StatusMesa = reader["StatusMesa"].ToString(),
                    Color = reader["Color"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}
