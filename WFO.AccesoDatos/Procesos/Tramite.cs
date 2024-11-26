using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades;

namespace WFO.AccesoDatos.Procesos
{
    public class Tramite
    {

        ManejoDatos b = new ManejoDatos();
        public DataTable SeleccionarReporteGeneralTotales(string fechaDesde, string fechaHasta,string idFlujo)
        {
            string consulta = "SELECT ST.Nombre AS Descripcion, Count(T.IdStatus) AS Totales," +
                              "CONVERT(DECIMAL(10, 2), (COUNT(T.IdStatus)) * 100 / CAST(SUM(Count(*)) OVER() AS FLOAT)) AS Porcentaje " +
                              "FROM tramite T " +
                              "INNER JOIN statusTramite ST " +
                              "ON  T.IdStatus = ST.Id " +
                              "INNER JOIN tramite_tipo TT ON T.IdTipoTramite=TT.Id "+
                              "WHERE FechaRegistro>= CONVERT(DATETIME, @fechaDesde, 102) " +
                              "AND FechaRegistro<DATEADD(DAY,1,CONVERT(DATETIME, @fechaHasta, 102)) " +
                              "AND TT.IdFlujo=@idFlujo "+
                              "GROUP BY ST.Nombre";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("@fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("@idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }
        public DataTable TopTenSuspendidos(string fechaDesde, string fechaHasta,string idFlujo)
        {
            string consulta = "SELECT TOP 10 " +
                             "B.Promotoria,B.Nombre,B.Zona,B.Suspendido AS NumTramitesSus,B.Ejecucion AS NumTramitesEje " +
                             "FROM " +
                             " (SELECT PV.Promotoria, PV.Nombre,PV.Zona,[Ejecucion],[Suspendido] " +
                             "  FROM " +
                             "   (SELECT " +
                             "    VT.IdTipoTramite,VT.PromotoriaClave AS Promotoria,VT.PromotoriaNombre AS Nombre,VT.EstadoNombre,VT.Zona " +
                             "    FROM vw_tramite VT " +
                             "    INNER JOIN tramite_tipo TT ON VT.IdTipoTramite=TT.Id "+
                             "    WHERE TT.IdFlujo=@idFlujo AND VT.Estado IN(5, 6) AND VT.FechaRegistro>=CONVERT(DATETIME, @fechaDesde, 102) AND VT.FechaRegistro<DATEADD(DAY,1,CONVERT(DATETIME,@fechaHasta, 102)) " +
                             "   ) A " +
                             " PIVOT(COUNT(IdTipoTramite) FOR EstadoNombre IN([Ejecucion],[Suspendido])) PV) B " +
                             "ORDER BY NumTramitesSus DESC";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }
        public DataTable TopTenRecepcion(string fechaDesde, string fechaHasta, string idFlujo)
        {
            string consulta = "SELECT TOP 10 " +
                             "B.Promotoria,B.Zona,B.Nombre,B.Suspendido AS NumTramitesSus,B.Ejecucion AS NumTramitesEje " +
                             "FROM " +
                             " (SELECT PV.Promotoria, PV.Nombre,PV.Zona,[Ejecucion],[Suspendido] " +
                             "  FROM " +
                             "   (SELECT " +
                             "    VT.IdTipoTramite,VT.PromotoriaClave AS Promotoria,VT.PromotoriaNombre AS Nombre,VT.EstadoNombre,VT.Zona " +
                             "    FROM vw_tramite VT " +
                             "    INNER JOIN tramite_tipo TT ON VT.IdTipoTramite=TT.Id "+
                             "    WHERE TT.IdFlujo=@idFlujo AND VT.Estado IN(6,5) AND VT.FechaRegistro>=CONVERT(DATETIME,@fechaDesde, 102) AND VT.FechaRegistro< DATEADD(DAY,1,CONVERT(DATETIME,@fechaHasta, 102))  " +
                             "   ) A " +
                             " PIVOT(COUNT(IdTipoTramite) FOR EstadoNombre IN([Ejecucion],[Suspendido])) PV) B " +
                             "ORDER BY NumTramitesEje DESC";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }
        public DataTable ResumenPromotoria(string estado, string idFlujo)
        {
            string consulta = "SELECT " +
                             "TT.Id AS RAMO," +
                             "VT.Id, TT.Nombre AS NRAMO," +
                             "F.FolioCompuesto AS TRAMITE," +
                             "VT.PromotoriaClave AS CVEPROMOTORIA," +
                             "VT.PromotoriaNombre AS PROMOTORIA," +
                             "CA.Clave AS CLAVEAGENTE," +
                             "CA.Nombre AS AGENTE," +
                             "DATEDIFF(DAY, VT.FechaRegistro, GETDATE()) AS DIAS " +
                             "FROM vw_tramite AS VT " +
                             "INNER JOIN tramite_tipo AS TT ON VT.IdTipoTramite = TT.Id " +
                             "INNER JOIN tramite_folio AS F ON VT.Id = F.Id " +
                             "LEFT OUTER JOIN cat_agentes AS CA ON VT.Id = CA.Id " +
                             "WHERE VT.Estado = @estado AND TT.IdFlujo=@idFlujo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            b.AddParameter("@idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }
        public DataTable ResumenPromotoria(DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable DatosPromotoria = new DataTable();
            string formato = "yyyy-MM-dd";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            string Promotorias = "SELECT id, Nombre,Clave FROM cat_promotorias";
            b.ExecuteCommandQuery(Promotorias);
            DatosPromotoria = b.Select();
            string qResumenPromotoria = "SELECT * FROM ( ";
            foreach (DataRow idP in DatosPromotoria.Rows)
            {
                qResumenPromotoria += "SELECT idPromotoria,PromotoriaClave, PromotoriaNombre AS Promotoria,Registro,Proceso,Hold,Ejecucion,Rechazo,Suspendido,Total " +
                    " FROM Fn_TotalPromotoria(" + idP["id"] + ",'" + idP["Nombre"] + "','" + idP["Clave"] + "','" + fechaD + "','" + fechaH + "') UNION ";
            }
            qResumenPromotoria = qResumenPromotoria.Substring(0, qResumenPromotoria.Length - ("UNION".Length + 1));
            qResumenPromotoria += " ) A ORDER BY A.IdPromotoria";
            b.ExecuteCommandQuery(qResumenPromotoria);
            return b.Select();
        }
        public DataTable PorcentajeSuspendidos(string fechaDesde, string fechaHasta,string idFlujo)
        {
            string consulta = "SELECT * from FN_PorcientoSuspension(@fechaDesde,@fechaHasta,@idFlujo)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }

        public DataTable EstatusTramite(string fechaDesde, string fechaHasta, string status, int modo, string IdFlujo)
        {
            string consulta = string.Empty;
            string tblBitacora = string.Empty;
            string fechaFiltro = string.Empty;

            switch (modo)
            {
                case 1:
                    fechaFiltro = "FechaIngreso";
                    break;
                case 3:
                    fechaFiltro = "FechaEstatus";
                    break;
            }

            switch (IdFlujo)
            {
                case "1":
                    tblBitacora = "vw_bitacora_dos";
                    break;

                case "2":
                    tblBitacora = "vw_bitacora_tres";
                    break;

                case "3":
                    tblBitacora = "bitacora_NC";
                    break;

                case "4":
                    tblBitacora = "bitacora_N4";
                    break;

            }

            if (IdFlujo == "3")
            {
                consulta = "SELECT * ";
                consulta += " FROM ( ";
                consulta += "SELECT ";
                consulta += "tramite.Id ";
                consulta += ", tramite.IdTipoTramite ";
                consulta += ", tramite_tipo.IdFlujo ";
                consulta += ", CONVERT(DATE, tramite.FechaRegistro) AS FechaIngreso ";
                consulta += ", tramite_folio.FolioCompuesto AS FolioTramite ";
                consulta += ", tramite_tipo.Nombre AS Ramo ";
                consulta += ", cat_producto.Nombre AS Producto ";
                consulta += ", statusTramite.Nombre AS Estatus ";
                consulta += ", (SELECT MAX(bitacora_NC.FechaTermino) FROM bitacora_NC WHERE tramite.Id = bitacora_NC.IdTramite) AS FechaEstatus ";
                consulta += ", DATEDIFF(HOUR, (SELECT MAX(bitacora_NC.FechaTermino) FROM bitacora_NC WHERE tramite.Id = bitacora_NC.IdTramite), GETDATE()) AS Tiempo ";
                consulta += ", tramite.IdPromotoria ";
                consulta += ", cat_promotorias.clave AS PromotoriaClave";
                consulta += ", cat_promotorias.Nombre AS Promotoria ";
                consulta += ", tramite.IdAgente AS ClaveAgente ";
                consulta += ", tramite.IdSisLegados AS Poliza  ";
                consulta += ", tramite_prioridad.Nombre AS Prioridad ";
                consulta += "FROM  tramite ";
                consulta += "INNER JOIN tramite_tipo ON tramite_tipo.Id = tramite.IdTipoTramite ";
                consulta += "INNER JOIN tramite_folio ON tramite.Id = tramite_folio.IdTramite ";
                consulta += "INNER JOIN tramite_det_NC ON tramite.Id = tramite_det_NC.IdTramite ";
                consulta += "INNER JOIN tramite_prioridad ON tramite_prioridad.Id = tramite.idPrioridad ";
                consulta += "INNER JOIN statusTramite ON statusTramite.Id = tramite.IdStatus ";
                consulta += "INNER JOIN cat_promotorias ON cat_promotorias.Id = tramite.IdPromotoria ";
                consulta += "INNER JOIN cat_producto ON cat_producto.Id = tramite_det_NC.IdProducto ";
                consulta += "INNER JOIN cat_subproducto ON cat_subproducto.Id = tramite_det_NC.IdSubProducto ";
                consulta += "WHERE tramite_tipo.IdFlujo = @IdFlujo ";
                consulta += " ) reporte ";
                consulta += " WHERE Estatus IN(" + status + ") ";
                consulta += "   AND (FechaEstatus > @fechaDesde AND FechaEstatus < @fechaHasta) ";
            }
            else
            {
                consulta = "SELECT * FROM (" +
                              "SELECT VT.Id, VT.IdTipoTramite, CONVERT(DATE, VT.FechaRegistro) AS FechaIngreso, F.FolioCompuesto AS FolioTramite, TP.Nombre AS Ramo, P.Nombre AS Producto, VT.EstadoNombre AS Estatus, " +
                              "ISNULL(CONVERT(DATE, MAX(VB.FechaTermino)), '') AS FechaEstatus, DATEDIFF(hh, MAX(VB.FechaTermino), GETDATE()) AS Tiempo, VT.IdPromotoria, VT.PromotoriaClave, VT.PromotoriaNombre AS Promotoria, " +
                              "VT.IdAgente AS ClaveAgente, VT.IdSisLegados AS Poliza, PR.Nombre AS Prioridad " +
                              "FROM  vw_tramite AS VT INNER JOIN " +
                              "tramite_folio AS F ON VT.Id = F.IdTramite INNER JOIN " +
                              "cat_producto AS P ON VT.IdTipoTramite = P.Id_TipoTramite INNER JOIN " +
                              "tramite_tipo AS TP ON VT.IdTipoTramite = TP.Id INNER JOIN " +
                              "tramite_prioridad AS PR ON VT.Prioridad = PR.Id INNER JOIN " +
                               tblBitacora + " AS VB ON VT.Id = VB.IdTramite AND VT.IdTipoTramite = VB.IdTipoTramite " +
                              "GROUP BY VT.Id, VT.IdTipoTramite, VT.FechaRegistro, F.FolioCompuesto, TP.Nombre, P.Nombre, VT.EstadoNombre, VT.IdPromotoria, VT.PromotoriaClave, VT.PromotoriaNombre, VT.IdAgente, VT.IdSisLegados, PR.Nombre " +
                            ") A " +
                           "INNER JOIN tramite_tipo TT ON A.IdTipoTramite=TT.Id " +
                           "WHERE Estatus IN(" + status + ") " +
                           "AND " + fechaFiltro + ">= CONVERT(DATETIME,@fechaDesde, 102) AND " + fechaFiltro + "< DATEADD(DAY,1,CONVERT(DATETIME,@fechaHasta, 102)) " +
                           "AND IdFlujo=@IdFlujo";
            }

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("@fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            return b.Select();
        }
        public DataTable MapaSupervisorDetalle(string IdFlujo)
        {
            if (string.Equals(IdFlujo,"TODOS")) IdFlujo = "%";
            string consulta = "SELECT VT.Estado AS ESTADO,VT.EstadoNombre AS ESTATUS, COUNT(VT.Estado) AS TOTAL " +
                       "FROM vw_tramite VT " +
                       "INNER JOIN tramite_tipo TT ON VT.IdTipoTramite=TT.Id "+
                       "WHERE VT.estado =estado  AND TT.Id LIKE @IdFlujo " +
                       " GROUP BY VT.Estado,VT.EstadoNombre";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.VarChar);
            return b.Select();
        }
        public DataTable TotalTramiteEstatus(string fechaDesde, string fechaHasta,string idFlujo)
        {
            string consulta = "SELECT * FROM TotalTramiteEstatusFecha " +
                              "WHERE FECHA>=CONVERT(DATETIME, @fechaDesde, 102) AND FECHA<DATEADD(DAY, 1, CONVERT(DATETIME, @fechaHasta, 102)) " +
                              "AND IDFLUJO=@idFlujo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("@fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("@idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }
        public DataTable TotalTramiteFechaMov(string fechaDesde, string fechaHasta,string idFlujo)
        {
            string consulta = "SELECT * FROM TotalTramiteEstatusFechaMov " +
                              "WHERE FECHA>=CONVERT(DATETIME, @fechaDesde, 102) AND FECHA<DATEADD(DAY, 1, CONVERT(DATETIME, @fechaHasta, 102)) " +
                              "AND IDFLUJO=@idFlujo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("@fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("@idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }
        public DataTable EstatusTramite()
        {
            string consulta = "SELECT Nombre FROM statusTramite WHERE activo=1";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }
        public int TotalMesFranja(DateTime Fecha, string idFlujo)
        {
            string Mes = Fecha.Month.ToString();
            string Annio = Fecha.Year.ToString();
            string consulta = "SELECT COUNT(*) AS Total FROM vw_tramite VT " +
                              "INNER JOIN tramite_tipo TT ON VT.IdTipoTramite=TT.Id " +
                              "WHERE TT.IdFlujo=@idFlujo AND MONTH(VT.FechaRegistro)=@Mes AND YEAR(VT.FechaRegistro)=@Annio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("Mes", Mes, SqlDbType.Int);
            b.AddParameter("Annio", Annio, SqlDbType.Int);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }
        public DataTable TramiteSemana(string mes, string annio,string idFlujo)
        {
            string consulta = "SELECT DS.SEMANA," +
                             "LEFT(CAST(DS.FECHA AS VARCHAR(10)),10) AS FECHA," +
                             "DS.DIA," +
                             "ISNULL(B.TRAMITE, 0) AS TRAMITES " +
                             "FROM FN_DiasSemana(@annio, @mes) DS " +
                             "LEFT JOIN " +
                                "(SELECT A.FECHA, SUM(A.TRAMITE) AS TRAMITE FROM " +
                                    "(SELECT " +
                                        "CAST(VT.FechaRegistro AS DATE) AS FECHA," +
                                        "COUNT(VT.Id) AS TRAMITE " +
                                     "FROM vw_tramite VT " +
                                     "INNER JOIN tramite_tipo TT ON VT.IdTipoTramite=TT.Id " +
                                     "WHERE TT.IdFlujo=@idFlujo AND MONTH(VT.FechaRegistro) =@mes AND YEAR(VT.FechaRegistro) =@annio " +
                                     " GROUP BY  VT.FechaRegistro) A " +
                                "GROUP BY A.FECHA, A.TRAMITE) B " +
                             "ON DS.FECHA = B.FECHA  ORDER BY DS.SEMANA DESC";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("mes", mes, SqlDbType.Int);
            b.AddParameter("annio", annio, SqlDbType.Int);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }
        public DataTable TotalTramiteSemana(string mes, string annio,string idFlujo)
        {
            string consulta = "SELECT SEMANA,SUM(TRAMITES) AS TRAMITES FROM " +
                                       "(SELECT DS.SEMANA," +
                                               "LEFT(CAST(DS.FECHA AS VARCHAR(10)),10) AS FECHA," +
                                               "DS.DIA," +
                                               "ISNULL(B.TRAMITE, 0) AS TRAMITES " +
                                               "FROM FN_DiasSemana(@annio,@mes) DS " +
                                               "LEFT JOIN " +
                                                "(SELECT A.FECHA, SUM(A.TRAMITE) AS TRAMITE FROM " +
                                                    "(SELECT " +
                                                        "CAST(VT.FechaRegistro AS DATE) AS FECHA," +
                                                        "COUNT(VT.Id) AS TRAMITE " +
                                                     "FROM vw_tramite VT " +
                                                     "INNER JOIN tramite_tipo TT ON VT.IdTipoTramite = TT.Id "+
                                                     "WHERE TT.IdFlujo=@idFlujo AND MONTH(VT.FechaRegistro) =@mes AND YEAR(VT.FechaRegistro) =@annio "+
                                                     " GROUP BY  VT.FechaRegistro) A " +
                                               "GROUP BY A.FECHA, A.TRAMITE) B " +
                                         "ON DS.FECHA = B.FECHA) C " +
                                    "GROUP BY SEMANA";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("mes", mes, SqlDbType.Int);
            b.AddParameter("annio", annio, SqlDbType.Int);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }
        public DataTable Tendencia(string annio,string idFlujo)
        {
            string consulta = "SELECT * FROM FN_Tendencia(@annio,@idFlujo)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("annio", annio, SqlDbType.Int);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }
        public DataTable TAT(DateTime fechaDesde, DateTime fechaHasta, string idFlujo)
        {
            string formato = "yyyy-MM-dd";
            string FechaI = fechaDesde.ToString(formato);
            string FechaF = fechaHasta.ToString(formato);
            string consulta = "TAT_Generar_Reporte @FechaI,@FechaF,@idFlujo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@FechaI", fechaDesde, SqlDbType.Date);
            b.AddParameter("@FechaF", fechaHasta, SqlDbType.Date);
            b.AddParameter("@idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }

        public DataTable TramitesPorMesa(string fechainicial, string fechafinal, string statustramite, string mesa)
        {
            b.ExecuteCommandSP("Tramite_Seleccionar_TramitesMesa");
            b.AddParameter("@fechainicial", fechainicial, SqlDbType.VarChar);
            b.AddParameter("@fechafinal", fechafinal, SqlDbType.VarChar);
            b.AddParameter("@statustramite", statustramite, SqlDbType.VarChar);
            b.AddParameter("@mesa", mesa, SqlDbType.Int);
            return b.Select();
        }

        public List<prop.TramitesPorMesa> TramitesPorMesaLista(string fechainicial, string fechafinal, string statustramite, string mesa)
        {
            b.ExecuteCommandSP("Tramite_Seleccionar_TramitesMesa");
            b.AddParameter("@fechainicial", fechainicial, SqlDbType.VarChar);
            b.AddParameter("@fechafinal", fechafinal, SqlDbType.VarChar);
            b.AddParameter("@statustramite", statustramite, SqlDbType.VarChar);
            b.AddParameter("@mesa", mesa, SqlDbType.Int);
            List<prop.TramitesPorMesa> resultado = new List<prop.TramitesPorMesa>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramitesPorMesa item = new prop.TramitesPorMesa()
                {
                    Folio               = reader["Folio"].ToString(),
                    FechaRegistro       = reader["FechaRegistro"].ToString(),
                    FechaTermino        = reader["FechaTermino"].ToString(),
                    NumeroOrden         = reader["NumeroOrden"].ToString(),
                    FechaSolicitud      = reader["FechaSolicitud"].ToString(),
                    StatusTramite       = reader["StatusTramite"].ToString(),
                    NumeroPoliza        = reader["NumeroPoliza"].ToString(),
                    DCNKWIK             = reader["DCNKWIK"].ToString(),
                    Separado            = reader["Separado"].ToString(),
                    Operador            = reader["Operador"].ToString(),
                    IdMesa              = reader["IdMesa"].ToString(),
                    Mesa                = reader["Mesa"].ToString(),
                    StatusMesa          = reader["StatusMesa"].ToString(),
                    ObservacionPublica  = reader["ObservacionPublica"].ToString(),
                    ObservacionPrivada  = reader["ObservacionPrivada"].ToString(),
                    FinArchivo          = reader["FinArchivo"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public DataSet ReporteProductividadPromotorias(string ann, string promotorias)
        {
            b.ExecuteCommandSP("Tramite_Seleccionar_ResumenTramites");
            b.AddParameter("@ann", ann, SqlDbType.Int);
            b.AddParameter("@clavepromotoria", promotorias, SqlDbType.VarChar);
            return b.SelectExecuteFunctions();
        }

        public DataSet ReporteTramitesReingresos(string idusuario, string fechainicial, string fechafinal, string tipotramite)
        {
            b.ExecuteCommandSP("Tramite_Seleccionar_Reingresos");
            var fecha1 = string.Format("{0:yyyyMMdd 00:00:00}", DateTime.Parse(fechainicial + " " + DateTime.Now.ToLongTimeString()));
            var fecha2 = string.Format("{0:yyyyMMdd 23:59:59}", DateTime.Parse(fechafinal + " " + DateTime.Now.ToLongTimeString()));
            b.AddParameter("@fechainicial", fecha1, SqlDbType.VarChar);
            b.AddParameter("@fechafinal", fecha2, SqlDbType.VarChar);
            b.AddParameter("@tipotramite", tipotramite, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.VarChar);
            return b.SelectExecuteFunctions();
        }

        public DataTable ReporteTramitesAnuales()
        {
            b.ExecuteCommandSP("Tramite_Seleccionar_ResumenAnual");
            return b.Select();
        }

        public DataSet ReporteTramitesAnualesGrafico()
        {
            b.ExecuteCommandQuery("exec Tramite_Seleccionar_ResumenAnual_ParaGrafico 2018; exec Tramite_Seleccionar_ResumenAnual_ParaGrafico 2019;");
            return b.SelectExecuteFunctions();
        }

        public DataTable ReporteAlmacenamientoTramites()
        {
            b.ExecuteCommandSP("Tramite_Seleccionar_Almacenamiento");
            b.AddParameter("@Rangos", 12, SqlDbType.VarChar);
            return b.Select();
        }

        public DataTable ReporteAlmacenamientoTramitesDetalle(string promedio)
        {
            b.ExecuteCommandSP("Tramite_Seleccionar_AlmacenamientoDetalle");
            b.AddParameter("@Rango", promedio, SqlDbType.Int);
            return b.Select();
        }
    }
}
