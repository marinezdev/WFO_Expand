using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class ArchivosDependenciasAsignados
    {
        ManejoDatos b = new ManejoDatos();

        public string Seleccionar(string usuario)
        {
            string consulta = "SELECT folio FROM ArchivosDependenciasAsignados WHERE IdUsuario=@usuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@usuario", usuario, SqlDbType.NVarChar);
            return b.SelectString();
        }

        public int Agregar(string usuario, string folio)
        {
            string consulta = "INSERT INTO ArchivosDependenciasAsignados VALUES (@folio, @usuario)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            b.AddParameter("@usuario", usuario, SqlDbType.NVarChar);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public int Eliminar(string folio)
        {
            string consulta = "DELETE FROM ArchivosDependenciasAsignados WHERE folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            return b.InsertUpdateDeleteWithTransaction();
        }
    }
}
