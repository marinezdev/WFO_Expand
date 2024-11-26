using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion.Captura;

namespace WFO.AccesoDatos.Procesos.Operacion.Captura
{
    public class Tarjetas
    {
        ManejoDatos b = new ManejoDatos();
        
        public int Agregar(prop.Tarjetas tarjetas)
        {
            b.ExecuteCommandSP("Tarjeta_agregar");
            b.AddParameter("@Id_tramite", tarjetas.Id_tramite, SqlDbType.Int);
            b.AddParameter("@Id_banco", tarjetas.Id_banco, SqlDbType.Int);
            b.AddParameter("@Id_modo_pago", tarjetas.Id_modo_pago, SqlDbType.Int);
            b.AddParameter("@Id_periodicidad", tarjetas.Id_periodicidad, SqlDbType.Int);
            b.AddParameter("@Token", tarjetas.Token, SqlDbType.NVarChar, 150);
            return b.InsertUpdateDelete();
        }

        public List<prop.Tarjetas> Tarjetas_Selecionar_PorIdTramite(int IdTramite)
        {
            b.ExecuteCommandSP("Tarjetas_Selecionar_PorIdTramite");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            List<prop.Tarjetas> resultado = new List<prop.Tarjetas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Tarjetas item = new prop.Tarjetas()
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    periodicidad = reader["periodicidad"].ToString(),
                    modo_pago = reader["modo_pago"].ToString(),
                    banco = reader["banco"].ToString(),
                    Token = reader["Token"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int Modificar(int IdTarjeta, int IdTramite)
        {
            b.ExecuteCommandSP("Tarjetas_Alctualizar_PorIdTarjeta");
            b.AddParameter("@IdTarjeta", IdTarjeta, SqlDbType.Int);
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
