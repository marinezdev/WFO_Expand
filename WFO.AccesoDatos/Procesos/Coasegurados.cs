using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class Coasegurados
    {
        ManejoDatos b = new ManejoDatos();

        public int Agregar(string apellidopaterno, string apellidomaterno, string nombres, string fechanacimiento, 
        string curp, string sexo, string fechaafiliacion, string tipo, string fechaingresocolectividad, string aseguradotitular, string estado, string curptitular, 
        string certificado)
        {
            string consulta = "IF NOT EXISTS(SELECT * FROM Coasegurados WHERE nombres=@nombres AND ApellidoPaterno=@apellidopaterno AND ApellidoMaterno=@apellidomaterno) " +
            "BEGIN " +
            "INSERT INTO coasegurados (apellidopaterno, apellidomaterno, nombres, fechanacimiento, curp, sexo, fechaafiliacion, tipo, fechaingresocolectividad, aseguradotitular, estado, curptitular, certificado) " +
            "VALUES (@apellidopaterno, @apellidomaterno, @nombres, @fechanacimiento, @curp, @sexo, @fechaafiliacion, @tipo, @fechaingresocolectividad, @aseguradotitular, 1, @curptitular, @certificado) " +
            "END ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@apellidopaterno", apellidopaterno.Trim(), SqlDbType.NVarChar, 50);
            b.AddParameter("@apellidomaterno", apellidomaterno.Trim(), SqlDbType.NVarChar, 50);
            b.AddParameter("@nombres", nombres.Trim(), SqlDbType.NVarChar, 50);
            b.AddParameter("@fechanacimiento", fechanacimiento.Trim(), SqlDbType.NChar, 8);
            b.AddParameter("@curp", curp.Trim(), SqlDbType.NChar, 18);
            b.AddParameter("@sexo", sexo.Trim(), SqlDbType.NChar, 1);
            b.AddParameter("@fechaafiliacion", fechaafiliacion, SqlDbType.NChar, 8);
            b.AddParameter("@tipo", tipo.Trim(), SqlDbType.NChar, 1);
            b.AddParameter("@fechaingresocolectividad", fechaingresocolectividad.Trim(), SqlDbType.NChar, 8);
            b.AddParameter("@aseguradotitular", aseguradotitular.Trim(), SqlDbType.Int);
            b.AddParameter("@curptitular", curptitular.Trim(), SqlDbType.NChar, 18);
            b.AddParameter("@certificado", certificado.Trim(), SqlDbType.NChar, 13);
            return b.InsertUpdateDelete();
        }


    }
}
