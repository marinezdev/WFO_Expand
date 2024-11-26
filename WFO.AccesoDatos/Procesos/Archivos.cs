using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class Archivos
    {
        ManejoDatos b = new ManejoDatos();

        public DataTable SeleccionarPorFolio(string folio)
        {
            string consulta = "SELECT * FROM archivos WHERE folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
            return b.Select();
        }

        public int Agregar(string folio, string archivo)
        {
            string consulta = "INSERT INTO archivos VALUES(@folio, @archivo)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
            b.AddParameter("@archivo", archivo, SqlDbType.NVarChar, 150);
            return b.InsertUpdateDelete();
        }



    }
}
