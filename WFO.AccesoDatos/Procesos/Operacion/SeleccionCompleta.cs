using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.AccesoDatos.Procesos.Operacion
{
    public class SeleccionCompleta
    {
        ManejoDatos b = new ManejoDatos();

        public int ActualizaSeleccionCompleta(int IdTramite, int IdUsuario, int SelecionCompleta)
        {
            b.ExecuteCommandSP("Tramite_Actualiza_SeleccionCompleta");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int, 150);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int, 150);
            b.AddParameter("@SelecionCompleta", SelecionCompleta, SqlDbType.VarChar, 150);

            return b.InsertUpdateDeleteWithTransaction();
        }

        public bool ConsultaSeleccionCompleta(int IdTramite)
        {
            b.ExecuteCommandSP("Tramite_Consulta_SeleccionCompleta");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);

            bool resultado = false;
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado = Convert.ToBoolean(reader["SELECCIONCOMPLETA"]);  
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}
