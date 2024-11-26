using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class BitacoraDos
    {
        ManejoDatos b = new ManejoDatos();
        public DataTable DatosProductividad(string fechaDesde, string fechaHasta,string usuarios, string idFlujo)
        {
            string tblBitacora = string.Empty;
            switch (idFlujo)
            {
                case "1":
                    tblBitacora = "vw_bitacora_dos";
                    break;
                case "2":
                    tblBitacora = "vw_bitacora_tres";
                    break;
            }
            string consulta= "SELECT " +
                             "B.operador," +
                             "B.MesaNombre AS mesaNombre," +
                             "B.aceptados AS aceptado," +
                             "B.proceso," +
                             "B.tramite," +
                             "B.rechazo," +
                             "B.pausa," +
                             "B.totalTramites," +
                             "CONVERT(VARCHAR(50), FLOOR(B.tiempo / 86400)) + 'D:' +" +
                             "CONVERT(VARCHAR(50), FLOOR((B.tiempo / 3600) - FLOOR(B.tiempo / 86400) * 24)) + 'H:' +" +
                             "CONVERT(VARCHAR(50), FLOOR((B.tiempo / 60) - FLOOR(B.tiempo / 3600) * 60)) + 'M:' +" +
                             "CONVERT(VARCHAR(50), B.tiempo - FLOOR(B.tiempo / 60) * 60) + 'S' AS tiempo " +
                             "FROM " +
                                "(" +
                                  "SELECT " +
                                   "A.operador," +
                                   "A.MesaNombre," +
                                   "SUM(ISNULL(A.aceptados, 0)) AS aceptados," +
                                   "SUM(ISNULL(A.proceso, 0)) AS proceso," +
                                   "SUM(ISNULL(a.tramite, 0)) AS tramite," +
                                   "SUM(ISNULL(a.rechazo, 0)) AS rechazo," +
                                   "SUM(ISNULL(a.pausa, 0)) AS pausa," +
                                   "(SUM(ISNULL(A.aceptados, 0)) + SUM(ISNULL(A.proceso, 0)) + SUM(ISNULL(a.tramite, 0)) +SUM(ISNULL(a.rechazo, 0))) AS totalTramites," +
                                   "SUM(A.Tiempo) AS tiempo " +
                                   "FROM " +
                                      "(" +
                                        "SELECT BD.Usuario AS operador, BD.MesaNombre," +
                                        "CASE " +
                                          "WHEN BD.Estado IN(17,14) THEN COUNT(BD.Estado) " +
                                        "END AS aceptados," +
                                        "CASE " +
                                          "WHEN BD.Estado IN(2,8,13,11,5) THEN COUNT(BD.Estado) " +
                                        "END AS proceso," +
                                        "CASE " +
                                          "WHEN BD.Estado IN(6,7,12,10,3,4,23) THEN COUNT(BD.Estado) " +
                                        "END AS tramite," +
                                        "CASE " +
                                          "WHEN BD.Estado=18 THEN COUNT(BD.Estado) " +
                                        "END AS rechazo," +
                                        "CASE " +
                                          "WHEN BD.Estado IN(16) THEN COUNT(BD.Estado) " +
                                        "END AS pausa," +
                                        "CASE " +
                                          "WHEN BD.Estado IN(16) THEN 0 " +
                                          "ELSE BD.tiempo " +
                                        "END AS tiempo " +
                                        "FROM " + tblBitacora +" BD " +
                                        "WHERE BD.estado IN(2,6,7,8,12,13,16,17,18,10,11,3,5,4,14,23) " +
                                        " AND (BD.FechaInicio >=CONVERT(DATETIME,@fechaDesde,102) AND BD.FechaInicio <DATEADD(DAY,1,CONVERT(DATETIME,@fechaHasta,102))) " +
                                        " AND BD.Usuario IN (" + usuarios + ") GROUP BY BD.Usuario, BD.MesaNombre, BD.tiempo, BD.Estado" +
                                    ") A " +
                                    "GROUP BY A.operador, A.MesaNombre " +
                                ") B";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("@fechaHasta", fechaHasta, SqlDbType.DateTime);
            return b.Select();
        }
        public DataTable DatosDetalleProductividad(string fechaDesde, string fechaHasta, string usuario,string mesa, string idFlujo)
        {
            string tblBitacora = string.Empty;
            switch (idFlujo)
            {
                case "1":
                    tblBitacora = "vw_bitacora_dos";
                    break;
                case "2":
                    tblBitacora = "vw_bitacora_tres";
                    break;
            }
            string consulta = "SELECT " +
                               "VBD.IdTramite,F.FolioCompuesto,VBD.IdUsuario,VBD.Usuario AS UsuarioNombre,VBD.IdMesa,VBD.MesaNombre," +
                               "CASE " +
                                  "WHEN VBD.Estado IN(17,14) THEN 'ACEPTADO' " +
                                  "WHEN VBD.Estado =18 THEN 'RECHAZADO' " +
                                  "WHEN VBD.Estado IN(6,7,12,10,3,4,23) THEN 'EN TRÁMITE' " +
                                  "WHEN VBD.Estado IN(2,8,13,11,5) THEN 'EN PROCESO' " +
                                  "WHEN VBD.Estado = 16 THEN 'EN PAUSA' " +
                               "END AS EstadoNombre," +
                               "VBD.FechaInicio," +
                               "VBD.FechaTermino," +
                               "CONVERT(VARCHAR(50), FLOOR(VBD.tiempo / 86400)) + 'D:' +" +
                               "CONVERT(VARCHAR(50), FLOOR((VBD.tiempo / 3600) - FLOOR(VBD.tiempo / 86400) * 24)) + 'H:' +" +
                               "CONVERT(VARCHAR(50), FLOOR((VBD.tiempo / 60) - FLOOR(VBD.tiempo / 3600) * 60)) + 'M:' + " +
                               "CONVERT(VARCHAR(50), VBD.tiempo - FLOOR(VBD.tiempo / 60) * 60) + 'S' AS tiempo " +
                               "FROM " + tblBitacora +" VBD " +
                               "INNER JOIN tramite_folio F " +
                               "ON VBD.IdTramite = F.IdTramite " +
                               "WHERE VBD.Estado IN(2,6,7,8,12,13,16,17,18,10,11,3,5,4,14,23) " +
                               "AND (VBD.FechaInicio >=CONVERT(DATETIME,@fechaDesde,102) AND VBD.FechaInicio < DATEADD(DAY,1,CONVERT(DATETIME,@fechaHasta,102))) " +
                               "AND VBD.MesaNombre=@mesa AND VBD.Usuario=@usuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("@fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("@mesa", mesa, SqlDbType.VarChar);
            b.AddParameter("@usuario", usuario, SqlDbType.VarChar);
            return b.Select();
        }
        public DataTable Franja(DateTime Fecha,string idFlujo)
        {
            string Mes = Fecha.Month.ToString();
            string Dia = Fecha.Day.ToString();
            string Annio = Fecha.Year.ToString();
            string tblBitacora = string.Empty;
            switch (idFlujo)
            {
                case "1":
                    tblBitacora = "vw_bitacora_dos";
                    break;
                case "2":
                    tblBitacora = "vw_bitacora_tres";
                    break;
            }

            string consulta = "SELECT " +
                                  "HL.hora AS Franja," +
                                  "ISNULL(B.ingresados, 0) as ingresados," +
                                  "ISNULL(B.tocados, 0) as tocados," +
                                  "ISNULL(B.ejecutados, 0) as ejecutados " +
                                  "FROM vw_HorarioLaboral HL LEFT JOIN " +
                                  " (SELECT hora,[ingresados],[tocados],[ejecutados] " +
                                  "  FROM " +
                                  "   (SELECT " +
                                  "    DATEPART(HOUR, FechaInicio) AS hora," +
                                  "    CASE " +
                                  "      WHEN EstadoMesa = 1 THEN 'ingresados' " +
                                  "      WHEN EstadoMesa IN (2, 6, 7, 8, 12, 13, 16, 18, 9, 10, 11) THEN 'tocados' " +
                                  "      WHEN EstadoMesa = 17 THEN 'ejecutados' " +
                                  "    END AS Movimiento," +
                                  "    IdTramite " +
                                  "    FROM " +tblBitacora +
                                  "    WHERE DATEPART(HOUR, FechaInicio) >= 7 " +
                                  "    AND DATEPART(HOUR, FechaInicio) <= 22 " +
                                  "    AND DAY(FechaInicio) =@Dia " +
                                  "    AND MONTH(FechaTermino) =@Mes " +
                                  "    AND YEAR(FechaTermino) =@Annio " +
                                  "    ) A " +
                                  "  PIVOT(COUNT(IdTramite) FOR Movimiento IN([ingresados],[tocados],[ejecutados])) PV) B " +
                                  "ON HL.hora = B.hora ORDER BY HL.hora";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@Dia", Dia, SqlDbType.Int);
            b.AddParameter("@Mes", Mes, SqlDbType.Int);
            b.AddParameter("@Annio", Annio, SqlDbType.Int);
            return b.Select();
        }
    }
}
