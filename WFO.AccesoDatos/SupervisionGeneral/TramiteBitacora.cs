using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.SupervisionGeneral
{
    public class TramiteBitacora
    {
        ManejoDatos b = new ManejoDatos();

        public int Agregar(string usuariocambio, string usuarioanterior, string tramite, string idpriodidadanterior)
        {
            b.ExecuteCommandSP("TramiteBitacora_Agregar");
            b.AddParameter("@usuariocambio", usuariocambio, SqlDbType.Int);
            b.AddParameter("@usuarioanterior", usuarioanterior, SqlDbType.Int);
            b.AddParameter("@tramite", tramite, SqlDbType.Int);
            b.AddParameter("@idprioridadanterior", idpriodidadanterior, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }




    }
}
