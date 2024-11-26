using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion.Captura;

namespace WFO.AccesoDatos.Procesos.Operacion.Captura
{
    public class AgentesDXN
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.AgentesDXN> AgentesDXN_Selecionar_PorClave(string Clave)
        {
            b.ExecuteCommandSP("AgentesDXN_Selecionar_PorClave");
            b.AddParameter("@Clave", Clave, SqlDbType.NVarChar);
            List<prop.AgentesDXN> resultado = new List<prop.AgentesDXN>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.AgentesDXN item = new prop.AgentesDXN()
                {
                    Nomenclatura = reader["Nomenclatura"].ToString(),
                    Clave_Agente_Promotor_Ind_Prv = reader["Clave_Agente_Promotor_Ind_Prv"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
        
    }
}
