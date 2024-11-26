using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.AccesoDatos.Procesos.Operacion
{
    public class MotivosSuspension
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.MotivosSuspension> SelecionarMotivosProcesos(int IdProceso)
        {
            List<prop.MotivosSuspension> resultado = new List<prop.MotivosSuspension>();
            b.ExecuteCommandSP("Procesos_MotivosSuspension_Listar");
            b.AddParameter("@IdProceso", IdProceso, SqlDbType.Int);
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.MotivosSuspension item = new prop.MotivosSuspension()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    IdTramiteTipo = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdTramiteTipo"].ToString()),
                    IdMesa = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdProceso"].ToString()),
                    IdTramiteTipoRechazo = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdTramiteProceso_TipoProceso"].ToString()),
                    IdParent = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdParent"].ToString()),
                    MotivoRechazo = reader["Motivo"].ToString(),
                    Activo = bool.Parse(reader["Activo"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.MotivosSuspension> SelecionarMotivos(int IdMesa)
        {
            List<prop.MotivosSuspension> resultado = new List<prop.MotivosSuspension>();

            b.ExecuteCommandSP("MotivosSuspension_Listar");
            b.AddParameter("@Id_Mesa", IdMesa, SqlDbType.Int);
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.MotivosSuspension item = new prop.MotivosSuspension()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    IdTramiteTipo = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdTramiteTipo"].ToString()),
                    IdMesa = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdMesa"].ToString()),
                    IdTramiteTipoRechazo = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdTramiteTipoRechazo"].ToString()),
                    IdParent = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdParent"].ToString()),
                    MotivoRechazo = reader["MotivoRechazo"].ToString(),
                    Activo = bool.Parse(reader["Activo"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}
