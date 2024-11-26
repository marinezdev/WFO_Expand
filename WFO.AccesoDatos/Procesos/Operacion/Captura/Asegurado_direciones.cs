using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion.Captura;

namespace WFO.AccesoDatos.Procesos.Operacion.Captura
{
    public class Asegurado_direciones
    {
        ManejoDatos b = new ManejoDatos();

        public int Asegurado_Agregar_Direccion(prop.Asegurado_direciones Direcion)
        {
            b.ExecuteCommandSP("Asegurado_Agregar_Direccion");
            b.AddParameter("@IdTramite", Direcion.IdTramite, SqlDbType.Int);
            b.AddParameter("@IdEstado", Direcion.IdEstado, SqlDbType.Int);
            b.AddParameter("@IdPoblacion", Direcion.IdPoblacion, SqlDbType.Int);
            b.AddParameter("@IdCP", Direcion.IdCP, SqlDbType.Int);
            b.AddParameter("@IdColonia", Direcion.IdColonia, SqlDbType.Int);
            b.AddParameter("@Direccion", Direcion.Direccion, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        public List<prop.Asegurado_direciones> Asegurado_Direcion_Selecionar_PorIdTramite(int IdTramite)
        {
            b.ExecuteCommandSP("Asegurado_Direcion_Selecionar_PorIdTramite");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);

            List<prop.Asegurado_direciones> resultado = new List<prop.Asegurado_direciones>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Asegurado_direciones item = new prop.Asegurado_direciones()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    EstadoNombre = reader["Estado"].ToString(),
                    PoblacionNombre = reader["Poblacion"].ToString(),
                    CP = reader["CP"].ToString(),
                    ColoniaNombre = reader["Colonia"].ToString(),
                    Direccion = reader["Direccion"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int Asegurado_Direcion_Desactivar_PorIdAseguradoDireccion(int Id, int IdTramite)
        {
            b.ExecuteCommandSP("Asegurado_Direcion_Desactivar_PorIdAseguradoDireccion");
            b.AddParameter("@Id", Id, SqlDbType.Int);
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
