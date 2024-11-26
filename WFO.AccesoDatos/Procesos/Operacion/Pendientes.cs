using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.AccesoDatos.Procesos.Operacion
{
    public class Pendientes
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.Pendientes> Selecionar_Pendientes_PorId(int IdPendiente, int IdUsuario, int IdFlujo)
        {
            b.ExecuteCommandSP("Pendientes_Selecionar_PorIdpendiente");
            b.AddParameter("@IdPendiente", IdPendiente, SqlDbType.Int);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            List<prop.Pendientes> resultado = new List<prop.Pendientes>();
            var reader = b.ExecuteReader();
                while (reader.Read())
                {
                    prop.Pendientes item = new prop.Pendientes()
                    {
                        IdTramite = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdTramite"].ToString()),
                        IdMesa = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdMesa"].ToString()),
                        IdStatusMesa = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdStatusMesa"].ToString()),
                        IdStatusTramite = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdStatusTramite"].ToString()),
                        FolioCompuesto = reader["FolioCompuesto"].ToString(),
                        Operacion = reader["Operacion"].ToString(),
                        Contratante = reader["Contratante"].ToString(),
                        //RFC = reader["RFC"].ToString(),
                        //Titular = reader["Titular"].ToString(),
                        NombreMesa = reader["NombreMesa"].ToString(),
                        EstatusMesa = reader["EstatusMesa"].ToString(),
                        EstatusTramite = reader["EstatusTramite"].ToString(),
                        FechaRegistro = reader["FechaRegistro"].ToString(),
                        TipoServicio = reader["TipoServicio"].ToString(),
                    };
                    resultado.Add(item);
                }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}
