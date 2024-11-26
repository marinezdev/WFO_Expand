using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class Usuarios
    {
        ManejoDatos b = new ManejoDatos();
        public DataTable NombreUsuarios(string idFlujo)
        {
            string consulta= "SELECT U.IdUsuario,U.Nombre, UF.IdFlujo FROM Usuarios U "+
                             "INNER JOIN usuariosFlujo UF ON U.IdUsuario = UF.IdUsuario "+
                             "WHERE UF.IdFlujo=@idFlujo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }
        public DataTable DatosRelojChecador(string fechaDesde, string fechaHasta, string idFlujo)
        {
            string consulta = "SELECT " +
                                   "A.idUsuario," +
                                   "B.Nombre," +
                                   "A.Fecha," +
                                   "(CONVERT(VARCHAR(50), FLOOR((A.Espera / 3600) - FLOOR(A.Espera / 86400) * 24)) + ' H:' + " +
                                   "CONVERT(VARCHAR(50), FLOOR((A.Espera / 60) - FLOOR(A.Espera / 3600) * 60)) + ' M')  AS TiempoTotal " +
                                   "FROM " +
                                        "(SELECT " +
                                            "idUsuario," +
                                            "CONVERT(DATE, inicioSesion) as Fecha," +
                                            "SUM(DATEDIFF(SECOND, inicioSesion, FinSesion)) AS Espera " +
                                            "FROM logUsuarios " +
                                            "WHERE (inicioSesion >= CONVERT(DATETIME,@fechaDesde,102) AND inicioSesion<DATEADD(DAY,1,CONVERT(DATETIME,@fechaHasta,102))) " +
                                            "GROUP BY idUsuario, CONVERT(DATE, inicioSesion)) A " +
                                   "INNER JOIN  " +
                                   "("+
                                     "SELECT U.IdUsuario,U.Nombre, UF.IdFlujo FROM Usuarios U " +
                                     "INNER JOIN usuariosFlujo UF ON U.IdUsuario = UF.IdUsuario " +
                                     "WHERE UF.IdFlujo=@idFlujo" +
                                   ") B "+
                                   "ON A.idUsuario = B.IdUsuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();

        }

    }
}
