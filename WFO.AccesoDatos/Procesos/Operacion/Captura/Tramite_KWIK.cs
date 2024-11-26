using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion.Captura;

namespace WFO.AccesoDatos.Procesos.Operacion.Captura
{
    public class Tramite_KWIK
    {
        ManejoDatos b = new ManejoDatos();

        public prop.Tramite_KWIK Tramite_Consulta_KWIK(int IdTramite)
        {
            b.ExecuteCommandSP("Tramite_Consulta_KWIK");
            b.AddParameter("@Idtramite", IdTramite, SqlDbType.Int);
            prop.Tramite_KWIK resultado = new prop.Tramite_KWIK();
            var reader = b.ExecuteReader();

            while (reader.Read())
            {
                resultado.FechaFirmaSolicitud = reader["FechaFirmaSolicitud"].ToString();
                resultado.Estado = reader["Estado"].ToString();
                resultado.RFC = reader["RFC"].ToString();
                resultado.ApPaterno = reader["ApPaterno"].ToString();
                resultado.ApMaterno = reader["ApMaterno"].ToString();
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.IdSisLegados = reader["IdSisLegados"].ToString();
                resultado.NumeroOrden = reader["NumeroOrden"].ToString();
                resultado.ClavePromotoria = reader["ClavePromotoria"].ToString();
                resultado.Clave = reader["Clave"].ToString();
                resultado.NombrePromotoria = reader["NombrePromotoria"].ToString();
                resultado.FolioCompuesto = reader["FolioCompuesto"].ToString();
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;

        }
    }
}
