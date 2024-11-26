using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.SupervisionGeneral;
namespace WFO.AccesoDatos.Procesos
{
    public class TramiteMesa
    {
        ManejoDatos b = new ManejoDatos();
        public DataTable DetallePromotoria(string fechaDesde, string fechaHasta, string idPromotoria)
        {
            string consulta = "SELECT VTM.idTramite,F.FolioCompuesto,VTM.FechaRegistro,VTM.FechaInicio,VTM.UsuarioNombre, VTM.MesaNombre,VTM.EstadoTramite " +
                            "FROM vw_tramiteMesa VTM " +
                            "INNER JOIN tramite_folio F ON VTM.idTramite=F.idTramite " +
                            "WHERE VTM.IdPromotoria =@idPromotoria  AND VTM.FechaRegistro>=CONVERT(DATETIME,@fechaDesde, 102) " +
                            "AND VTM.FechaRegistro< DATEADD(DAY,1,CONVERT(DATETIME,@fechaHasta, 102)) " +
                            "AND VTM.EstadoTramite IN (SELECT Nombre FROM statusTramite WHERE Id IN(1,2,4,5,6,7)) ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idPromotoria", idPromotoria, SqlDbType.Int);
            b.AddParameter("@fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("@fechaHasta", fechaHasta, SqlDbType.DateTime);
            return b.Select();
        }
        public DataTable PorcientoMotivosSuspension(string fechaDesde, string fechaHasta,string idFlujo)
        {
            string consulta = "SELECT MOTIVO," +
                              "ISNULL(ENERO,0) AS ENERO," +
                              "ISNULL(FEBRERO,0) AS FEBRERO," +
                              "ISNULL(MARZO,0) AS MARZO," +
                              "ISNULL(ABRIL,0) AS ABRIL," +
                              "ISNULL(MAYO,0) AS MAYO," +
                              "ISNULL(ABRIL,0) AS ABRIL," +
                              "ISNULL(MAYO,0) AS MAYO," +
                              "ISNULL(JUNIO,0) AS JUNIO," +
                              "ISNULL(JULIO,0) AS JULIO," +
                              "ISNULL(AGOSTO,0) AS AGOSTO," +
                              "ISNULL(SEPTIEMBRE,0) AS SEPTIEMBRE," +
                              "ISNULL(OCTUBRE,0) AS OCTUBRE," +
                              "ISNULL(NOVIEMBRE,0) AS NOVIEMBRE," +
                              "ISNULL(DICIEMBRE,0) AS DICIEMBRE FROM FN_PorcientoMotivosSuspension (@fechaDesde,@fechaHasta,@idFlujo)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }

        public int ModificarUsuarioAnterior(string idtramitemesa, string idusuario)
        {
            b.ExecuteCommandSP("TramiteMesa_Modificar_UsuarioAnterior");
            b.AddParameter("@idtramitemesa", idtramitemesa, SqlDbType.Int);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public DataTable SeleccionarDetalle(string idmesa, string statusmesa)
        {
            b.ExecuteCommandSP("TramiteMesa_SeleccionarDetalle");
            b.AddParameter("@idmesa", idmesa, SqlDbType.Int);
            b.AddParameter("@statusmesa", statusmesa, SqlDbType.VarChar);
            return b.Select();
        }

        public List<prop.TramiteMesaAsignar> SeleccionarMesasAsignar(int IdTramite)
        {
            b.ExecuteCommandSP("TramiteMesa_SeleccionarDetalleAsignar");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            List<prop.TramiteMesaAsignar> resultado = new List<prop.TramiteMesaAsignar>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramiteMesaAsignar item = new prop.TramiteMesaAsignar()
                {
                    IdTramite = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdTramite"].ToString()),
                    IdTramiteMesa = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdTramiteMesa"].ToString()),
                    Folio = reader["Folio"].ToString(),
                    EstatusTramite = reader["EstatusTramite"].ToString(),
                    Mesa = reader["Mesa"].ToString(),
                    Estatus = reader["Estatus"].ToString(),
                    Usuario = reader["Usuario"].ToString(),
                    Accion = reader["Accion"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int ActualizarUsuarioMesa(int IdTramitemesa, int IdUsuario)
        {
            b.ExecuteCommandSP("TramiteMesa_Actualizar_Usuario");
            b.AddParameter("@IdTramitemesa", IdTramitemesa, SqlDbType.Int);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public DataTable TramitesProcesables(string fechaDesde, string fechaHasta,string idFlujo)
        {
            string tblDatos = string.Empty;
            switch (idFlujo)
            {
                case "1":
                    tblDatos = "tramite_det_N1";
                    break;
                case "2":
                    tblDatos = "tramite_det_N3";
                    break;


            }
            string consulta = "SELECT " +
                             "VTP.FechaRegistro AS [Fecha Envío]," +
                             "VTP.FolioCompuesto AS [Número de Trámite]," +
                             "VTP.EstadoNombre AS [Estado]," +
                             "VTP.FechaSolicitud AS [Fecha Firma Solicitud]," +
                             "TDN.RFC AS [RFC]," +
                             "TDN.Nombre + ' ' + TDN.ApMaterno + ' ' + TDN.ApMaterno AS [Información del Contratante]," +
                             "TDN.TitularNombre + ' ' + TDN.TitularApPat + ' ' + TDN.TitularApMat AS [Información del Titular] " +
                             "FROM vw_tramiteP VTP, "+ tblDatos +" TDN " +
                             "WHERE VTP.Id = TDN.IdTramite " +
                             "AND VTP.Id IN (" +
                                    "SELECT IdTramite " +
                                    "FROM tramiteMesa " +
                                    "WHERE IdMesa IN (SELECT Id FROM mesa WHERE Nombre = 'SELECCIÓN' AND IdFlujo=@idFlujo) AND IdStatusMesa = (SELECT Id FROM statusMesa WHERE Nombre = 'Procesado') " +
                              ") " +
                              "AND " +
                              "( " +
                                  "( " +
                                    "VTP.Id IN ( " +
                                                "SELECT IdTramite " +
                                                "FROM tramiteMesa " +
                                                "WHERE IdMesa IN (SELECT Id FROM mesa WHERE Nombre = 'REVISIÓN TÉCNICA' AND IdFlujo=@idFlujo) AND IdStatusMesa = (SELECT Id FROM statusMesa WHERE Nombre = 'No Procesable') " +
                                              ") " +
                                 ") " +
                              "OR " +
                                 "( " +
                                    "VTP.Id IN (" +
                                                "SELECT IdTramite " +
                                                "FROM tramiteMesa " +
                                                "WHERE tramiteMesa.IdMesa IN (SELECT Id FROM mesa WHERE Nombre = 'REVISIÓN MÉDICA' AND IdFlujo=@idFlujo) AND IdStatusMesa = (SELECT Id FROM statusMesa WHERE Nombre = 'No Procesable') " +
                                    ") " +
                                  ") " +
                              ") " +
                          "AND VTP.FechaRegistro>=CONVERT(DATETIME,@fechaDesde,102) " +
                          "AND VTP.FechaRegistro<DATEADD(DAY,1,CONVERT(DATETIME,@fechaHasta,102))";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }
        public DataTable DatosTramite(string folio, string rfc, string contratante, string asegurado)
        {
            string consulta = "SELECT " +
                             "VTM.IdTramite," +
                             "F.FolioCompuesto," +
                             "(T3P.Nombre + ' ' + T3P.ApPaterno + ' ' + T3P.ApMaterno) AS Contratante," +
                             "(T3P.TitularNombre + ' ' + T3P.TitularApPat + ' ' + T3P.TitularApMat) AS Titular," +
                             "VTM.IdMesa," +
                             "VTM.MesaNombre," +
                             "VTM.UsuarioNombre," +
                             "TP.Nombre AS prioridad " +
                             "FROM vw_tramiteMesa VTM " +
                             "INNER JOIN  tramite_folio F ON VTM.IdTramite = F.IdTramite " +
                             "INNER JOIN  tramite_det_N1 T3P ON VTM.IdTramite = T3P.IdTramite " +
                             "INNER JOIN tramite_prioridad TP ON VTM.idPrioridad = TP.Id " +
                             "WHERE " +
                             "(F.FolioCompuesto LIKE @folio OR T3P.RFC LIKE @rfc OR(T3P.Nombre + T3P.ApPaterno + T3P.ApMaterno) LIKE @contratante " +
                             " OR (T3P.TitularNombre + T3P.TitularApPat +T3P.TitularApMat) LIKE @asegurado)";

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.VarChar);
            b.AddParameter("@rfc", rfc, SqlDbType.VarChar);
            b.AddParameter("@contratante", contratante, SqlDbType.VarChar);
            b.AddParameter("@asegurado", asegurado, SqlDbType.VarChar);
            return b.Select();
        }
        public DataTable DetalleHoras(string fechaDesde, string fechaHasta, string usuarios,string idFlujo )
        {
            string consulta = "SELECT A.Estado,A.MesaNombre,A.IdUsuario,A.UsuarioNombre,A.diasHabiles,(A.diasHabiles)*8 AS horasHabiles," +
                                   "A.diasNoHabiles,(A.diasNoHabiles)*16 AS horasNoHabiles,ABS((A.diasNoHabiles)*16-(A.diasHabiles)*8) AS horasEfectivas, A.EstadoMesa,A.tramites FROM " +
                                    "(SELECT " +
                                   "VTM.IdStatusMesa AS Estado," +
                                   "VTM.MesaNombre," +
                                   "VTM.IdUsuario," +
                                   "VTM.UsuarioNombre," +
                                   "dbo.fn_GetDiasLaborales(@fechaDesde,@fechaHasta) AS diasHabiles," +
                                   "dbo.fn_GetDiasNoLaborales(@fechaDesde,@fechaHasta) AS diasNoHabiles," +
                                   "CASE " +
                                   "WHEN VTM.IdStatusMesa IN(6,7,12,10,3,4,23) THEN 'ATENDIDOS' " +
                                   "WHEN VTM.IdStatusMesa IN(2,8,13,11,5) THEN 'PROCESADOS' " +
                                   "WHEN VTM.IdStatusMesa IN(16,14) THEN 'PENDIENTES' " +
                                   "END AS EstadoMesa," +
                                   "COUNT(VTM.idTramite) AS tramites " +
                                   "FROM vw_tramiteMesa VTM INNER JOIN Usuarios U ON VTM.IdUsuario=U.IdUsuario " +
                                   "WHERE  VTM.IdStatusMesa<>24 AND VTM.IdFlujo=@idFlujo " +
                                   "AND U.Nombre IN (" + usuarios + ") " +
                                   "AND VTM.IdStatusMesa IN(6,7,12,10,3,4,23,2,8,13,11,5,16,14) " +
                                   "GROUP BY VTM.IdStatusMesa,VTM.IdUsuario,VTM.MesaNombre,VTM.UsuarioNombre ) A";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);

            return b.Select();
        }
        public DataTable TiemposAtencion(DateTime fechaDesde, DateTime fechaHasta,string IdFlujo)
        {
            string formato = "yyyy-MM-dd";
            string FechaI = fechaDesde.ToString(formato);
            string FechaF = fechaHasta.ToString(formato);
            string consulta = "EXEC SP_TiemposTotalesAtencion @FechaI,@FechaF,@IdFlujo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("FechaI", fechaDesde, SqlDbType.Date);
            b.AddParameter("FechaF", fechaHasta, SqlDbType.Date);
            b.AddParameter("IdFlujo", IdFlujo, SqlDbType.Int);
            return b.Select();
        }
    }
}
