using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class AseguradosTitulares
    {
        ManejoDatos b = new ManejoDatos();

        public string SeleccionarTitularIdPorCURP(string curp)
        {
            string consulta = "SELECT Id FROM aseguradostitulares WHERE curp=@curp";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@curp", curp, SqlDbType.Char, 18);
            return b.SelectString();
        }

        public int Agregar(string poliza, string dependencia, string apellidopaterno, string apellidomaterno, string nombres, string fechanacimiento, string rfc, 
        string curp, string sexo, string entidadfederativa, string municipio, string niveltabular, string percepcionordinariabruta, string eventual)
        {
            string consulta = "IF NOT EXISTS(SELECT * FROM AseguradosTitulares WHERE nombres=@nombres AND ApellidoPaterno=@apellidopaterno AND ApellidoMaterno=@apellidomaterno) " +
            "BEGIN " + 
            "INSERT INTO aseguradostitulares (Poliza, Dependencia, apellidopaterno, apellidomaterno, nombres, fechanacimiento, rfc, curp, sexo, entidadfederativa, municipio, niveltabular, percepcionordinariabruta, eventual, estado) " +
            "VALUES (@poliza, @dependencia, @apellidopaterno, @apellidomaterno, @nombres, @fechanacimiento, @rfc, @curp, @sexo, @entidadfederativa, @municipio, @niveltabular, @percepcionordinariabruta, @eventual, 1) " + 
            "END ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@poliza", poliza, SqlDbType.NChar, 10);
            b.AddParameter("@dependencia", dependencia, SqlDbType.Int);
            b.AddParameter("@apellidopaterno", apellidopaterno, SqlDbType.NVarChar, 50);
            b.AddParameter("@apellidomaterno", apellidomaterno, SqlDbType.NVarChar, 50);
            b.AddParameter("@nombres", nombres, SqlDbType.NVarChar, 50);
            b.AddParameter("@fechanacimiento", fechanacimiento, SqlDbType.NChar, 8);
            b.AddParameter("@rfc", rfc, SqlDbType.NChar, 13);
            b.AddParameter("@curp", curp, SqlDbType.NChar, 18);
            b.AddParameter("@sexo", sexo, SqlDbType.NChar, 1);
            b.AddParameter("@entidadfederativa", entidadfederativa, SqlDbType.NChar, 3);
            b.AddParameter("@municipio", municipio, SqlDbType.NChar, 5);
            b.AddParameter("@niveltabular", niveltabular, SqlDbType.NChar, 2);
            b.AddParameter("@percepcionordinariabruta", percepcionordinariabruta, SqlDbType.NVarChar, 50);
            b.AddParameter("@eventual", eventual, SqlDbType.NChar, 2);
            return b.InsertUpdateDelete();
        }
    }
}
