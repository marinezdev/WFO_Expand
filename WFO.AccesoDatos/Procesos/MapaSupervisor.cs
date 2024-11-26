using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class MapaSupervisor
    {
        ManejoDatos b = new ManejoDatos();

        public DataTable DatosMapaSupervisor(string idFlujo)
        {
            string consulta = "SELECT * FROM FN_MapaSupervisor(@idFlujo)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }
        
        public DataTable DatosMapaSupervisorMesa(string mesa,string idFlujo)
        {
            string consulta = "SELECT * FROM FN_MapaSupervisorMesa (@mesa) WHERE IdFlujo=@idFlujo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("mesa", mesa, SqlDbType.VarChar);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }
        public DataTable DatosSupervisorMesaDetalle(string Idmesa)
        {
            string flujo = "SELECT IdFlujo FROM mesa WHERE Id=@Idmesa";
            b.ExecuteCommandQuery(flujo);
            b.AddParameter("IdMesa", Idmesa, SqlDbType.Int);
            string idFlujo = b.SelectString();
            string tblDatosPoliza = string.Empty;
            switch (idFlujo)
            {
                case "1":
                    tblDatosPoliza = "tramite_det_N1";
                    break;
                case "2":
                    tblDatosPoliza = "tramite_det_N3";
                    break;
            }
            string consulta="SELECT " +
                               "A.IdMesa," +
                               "A.MesaNombre," +
                               "A.IdTramite," +
                               "A.Estado," +
                               "A.FolioCompuesto," +
                               "CASE " +
                                  "WHEN A.reingresos> 1 THEN A.reingresos -1 " +
                                  "ELSE 0 " +
                               "END AS Reingreso," +
                               "CAST(A.FechaRegistro AS DATE) AS Fecha," +
                               "A.Usuario," +
                               "CONVERT(VARCHAR(50), FLOOR(A.tiempoAtencion / 86400)) +'D:' +" +
                               "CONVERT(VARCHAR(50), FLOOR((A.tiempoAtencion / 3600) - FLOOR(A.tiempoAtencion / 86400) * 24)) + 'H:' +" +
                               "CONVERT(VARCHAR(50), FLOOR((A.tiempoAtencion / 60) - FLOOR(A.tiempoAtencion / 3600) * 60)) + 'M:' +" +
                               "CONVERT(VARCHAR(50), A.tiempoAtencion - FLOOR(A.tiempoAtencion / 60) * 60) + 'S' AS tAtencion," +
                               "CONVERT(VARCHAR(50), FLOOR(A.tiempoMesa / 86400)) +'D:' +" +
                               "CONVERT(VARCHAR(50), FLOOR((A.tiempoMesa / 3600) - FLOOR(A.tiempoMesa / 86400) * 24)) + 'H:' +" +
                               "CONVERT(VARCHAR(50), FLOOR((A.tiempoMesa / 60) - FLOOR(A.tiempoMesa / 3600) * 60)) + 'M:' +" +
                               "CONVERT(VARCHAR(50), A.tiempoMesa - FLOOR(A.tiempoMesa / 60) * 60) + 'S' AS tMesa," +
                               "A.Contratante, A.Solicitante " +
                           "FROM " +
                               "(" +
                                 "SELECT " +
                                   "VTM.idTramite," +
                                   "F.FolioCompuesto," +
                                   "VTM.IdStatusMesa AS Estado," +
                                   "VTM.idMesa," +
                                   "VTM.MesaNombre," +
                                   "dbo.FN_ContarReingresos(VTM.idMesa, VTM.IdTramite) AS reingresos," +
                                   "VTM.FechaRegistro," +
                                   "U.Nombre as Usuario," +
                                   "(TP.Nombre + ' ' + TP.ApPaterno + ' ' + TP.ApMaterno) AS Contratante," +
                                   "(TP.TitularNombre + ' ' + TP.TitularApPat + ' ' + TP.TitularApMat) AS Solicitante," +
                                   "CASE " +
                                     "WHEN VTM.FechaInicio IS NULL THEN 0 " +
                                     "WHEN VTM.FechaFin IS NULL THEN DATEDIFF(SECOND, VTM.FechaInicio, GETDATE()) " +
                                     "ELSE(DATEDIFF(SECOND, VTM.FechaInicio, VTM.FechaFin)) " +
                                   "END AS tiempoAtencion," +
                                   "CASE " +
                                     "WHEN (dbo.Fn_TiempoMesa(VTM.IdTramite,@IdMesa)) IS NULL THEN ABS(DATEDIFF(SECOND,VTM.FechaRegistro,GETDATE())) " +
                                     "ELSE dbo.Fn_TiempoMesa(VTM.IdTramite,@IdMesa) " +
                                   "END AS tiempoMesa " +
                                "FROM vw_tramiteMesa VTM " +
                                "INNER JOIN tramite_folio F ON VTM.IdTramite = F.IdTramite " +
                                "INNER JOIN " +tblDatosPoliza +" TP ON VTM.IdTramite = TP.IdTramite " +
                                "LEFT JOIN  usuarios U ON  VTM.IdUsuario =  U.IdUsuario AND VTM.IdFlujo IN (SELECT DISTINCT IdFlujo FROM MESA WHERE ID IN (SELECT IdMesa FROM usuariosMesa WHERE IdUsuario = U.IdUsuario))" +
                                ") A " +
                           "WHERE A.IdMesa=@IdMesa AND A.Estado IN(16,1,2,8,13,11,5,22) " +
                           "ORDER BY A.IdMesa,A.IdTramite";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("IdMesa", Idmesa, SqlDbType.Int);
            return b.Select();

        }
    }
}
