using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class TramiteMesaBitacora
    {
        ManejoDatos b = new ManejoDatos();

        public int Agregar(string idusuarioanterior, string idusuarionuevo, string idusuariocambio, string idtramitemesa)
        {
            b.ExecuteCommandSP("TramiteMesaBitacora_Agregar");
            b.AddParameter("@idusuarioanterior", idusuarioanterior, SqlDbType.Int);
            b.AddParameter("@idusuarionuevo", idusuarionuevo, SqlDbType.Int);
            b.AddParameter("@idusuariocambio", idusuariocambio, SqlDbType.Int);
            b.AddParameter("@idtramitemesa", idtramitemesa, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }


    }
}
