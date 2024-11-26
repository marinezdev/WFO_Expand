using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos.Operacion
{
    public class kwik
    {
        ManejoDatos b = new ManejoDatos();

        public int ActualizaKwik(int IdTramite, int IdUsuario, string kwik)
        {
            b.ExecuteCommandSP("Tramite_Actualiza_Kwik");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int, 150);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int, 150);
            b.AddParameter("@kwik", kwik, SqlDbType.VarChar, 150);

            return b.InsertUpdateDeleteWithTransaction();
        }
    }
}
