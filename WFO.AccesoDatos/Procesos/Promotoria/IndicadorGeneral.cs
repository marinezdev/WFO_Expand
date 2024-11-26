using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.AccesoDatos.Procesos.Promotoria
{
    public class IndicadorGeneral
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.IndicadorGeneral> SeleccionaEstatusTotales(int Id, int Mes)
        {
            b.ExecuteCommandSP("Indicador_General_Promotoria_PorUsuario");
            b.AddParameter("@IdUsuario", Id, SqlDbType.Int);
            b.AddParameter("@Mes", Mes, SqlDbType.Int);
            List<prop.IndicadorGeneral> resultado = new List<prop.IndicadorGeneral>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.IndicadorGeneral item = new prop.IndicadorGeneral()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Estado = reader["Estado"].ToString(),
                    Totales = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Totales"].ToString()),
                    BackgroundColor = reader["BackgroundColor"].ToString(),
                    BorderColor = reader["BorderColor"].ToString(),
                    HoverBackgroundColor = reader["HoverBackgroundColor"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}
