using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.SupervisionGeneral;

namespace WFO.AccesoDatos.Procesos
{
    public class Mesa
    {
        ManejoDatos b = new ManejoDatos();
        public DataTable Mesas()
        {
           string consulta = "SELECT DISTINCT Nombre FROM mesa WHERE Activo=1 AND Nombre<>'PROMOTORÍA'";
           b.ExecuteCommandQuery(consulta);
           return b.Select();
        }

        public DataSet Tramite_EstatusActualDS(DateTime fechaDesde, DateTime fechaHasta, int IdFlujo)
        {
            DataTable dt = new DataTable();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            b.ExecuteCommandSP("AsaeWFO.dbo.Tramite_EstatusActual");
            b.AddParameter("@Inicio", fechaD, SqlDbType.NVarChar);
            b.AddParameter("@Fin", fechaH, SqlDbType.NVarChar);
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            DataSet ds = b.SelectExecuteFunctions();
            b.ConnectionCloseToTransaction();
            return ds;
        }

        public List<prop.Tramites_Clean> Tramites_Clean(DateTime fechaDesde, DateTime fechaHasta, int IdFlujo)
        {
            DataTable dt = new DataTable();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            b.ExecuteCommandSP("Tramites_Clean");
            b.AddParameter("@Inicio", fechaD, SqlDbType.NVarChar);
            b.AddParameter("@Fin", fechaH, SqlDbType.NVarChar);
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            List<prop.Tramites_Clean> resultado = new List<prop.Tramites_Clean>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Tramites_Clean item = new prop.Tramites_Clean()
                {
                    IdTramite = int.Parse(reader["IdTramite"].ToString()),
                    Folio = reader["Folio"].ToString(),
                    FechaRegistro = reader["FechaRegistro"].ToString(),
                    StatusTramite = reader["StatusTramite"].ToString(),
                    Poliza = reader["Poliza"].ToString(),
                    //KWIK = reader["KWIK"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
        
        public List<prop.Tramites_SuspendidosTotales> Tramites_SuspendidosTotales(DateTime fechaDesde, DateTime fechaHasta, int IdFlujo)
        {
            DataTable dt = new DataTable();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            b.ExecuteCommandSP("Tramites_SuspendidosTotales");
            b.AddParameter("@Inicio", fechaD, SqlDbType.NVarChar);
            b.AddParameter("@Fin", fechaH, SqlDbType.NVarChar);
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            List<prop.Tramites_SuspendidosTotales> resultado = new List<prop.Tramites_SuspendidosTotales>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Tramites_SuspendidosTotales item = new prop.Tramites_SuspendidosTotales()
                {
                    Promotoria = reader["Promotoria"].ToString(),
                    Suspendidos = reader["Suspendidos"].ToString(),

                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.Tramites_Supendidos> Tramites_Suspendidos(DateTime fechaDesde, DateTime fechaHasta, int IdFlujo, int IdPromotoria)
        {
            DataTable dt = new DataTable();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            b.ExecuteCommandSP("Tramites_Suspendidos");
            b.AddParameter("@Inicio", fechaD, SqlDbType.NVarChar);
            b.AddParameter("@Fin", fechaH, SqlDbType.NVarChar);
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            b.AddParameter("@IdPromotoria", IdPromotoria, SqlDbType.Int);
            List<prop.Tramites_Supendidos> resultado = new List<prop.Tramites_Supendidos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Tramites_Supendidos item = new prop.Tramites_Supendidos()
                {
                    IdTramite = int.Parse(reader["IdTramite"].ToString()),
                    Folio = reader["Folio"].ToString(),
                    FechaRegistro = reader["FechaRegistro"].ToString(),
                    StatusTramite = reader["StatusTramite"].ToString(),
                    Poliza = reader["Poliza"].ToString(),
                    KWIK = reader["KWIK"].ToString(),
                    Promotoria = reader["Promotoria"].ToString()

                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.Tramite_EstatusActual> Tramite_EstatusActual(DateTime fechaDesde, DateTime fechaHasta, int IdFlujo)
        {
            DataTable dt = new DataTable();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            b.ExecuteCommandSP("Tramite_EstatusActual");
            b.AddParameter("@Inicio", fechaD, SqlDbType.DateTime);
            b.AddParameter("@Fin", fechaH, SqlDbType.DateTime);
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            List<prop.Tramite_EstatusActual> resultado = new List<prop.Tramite_EstatusActual>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Tramite_EstatusActual item = new prop.Tramite_EstatusActual()
                {
                    IdTramite = reader["IdTramite"].ToString(),
                    FolioCompuesto = reader["FolioCompuesto"].ToString(),
                    FechaRegistro = (DateTime)reader["FechaRegistro"],
                    FechaSolicitud = (DateTime)reader["FechaSolicitud"],
                    StatusTramite = reader["StatusTramite"].ToString(),
                    IdSisLegados = reader["IdSisLegados"].ToString(),
                    kwik = reader["kwik"].ToString(),
                    FechaUltimoMovimiento = reader["FechaUltimoMovimiento"].ToString(),
                    TiempoUltimoStatus = reader["TiempoUltimoStatus"].ToString(),
                    IdUsuarioUltimoMovimiento = reader["IdUsuarioUltimoMovimiento"].ToString(),
                    UsuarioUltimoMovimiento = reader["UsuarioUltimoMovimiento"].ToString(),
                    UltimaMesa = reader["UltimaMesa"].ToString(),
                    FechaRegistroUltimaMesa = reader["FechaRegistroUltimaMesa"].ToString(),
                    FechaTerminoUltimaMesa = reader["FechaTerminoUltimaMesa"].ToString(),
                    TiempoUltimaMesa = reader["TiempoUltimaMesa"].ToString(),
                    UltimaMesaNombre = reader["UltimaMesaNombre"].ToString(),
                    IdStatusUltimaMesa = reader["IdStatusUltimaMesa"].ToString(),
                    StatusUltimaMesa = reader["StatusUltimaMesa"].ToString(),
                    IdUsuarioUltimaMesa = reader["IdUsuarioUltimaMesa"].ToString(),
                    UsaurioUltimaMesa = reader["UsaurioUltimaMesa"].ToString(),
                    NumeroOrden = reader["NumeroOrden"].ToString(),
                    Operacion = reader["Operacion"].ToString(),
                    //Producto = reader["Producto"].ToString(),
                    Contratante = reader["Contratante"].ToString(),
                    //Titular = reader["Titular"].ToString(),
                    //RFC = reader["RFC"].ToString(),
                    UsuarioUltimaMesa = reader["UsuarioUltimaMesa"].ToString(),
                    AgenteClave = reader["AgenteClave"].ToString(),
                    AgenteNombre = reader["AgenteNombre"].ToString(),
                    UltimoMovPromo = reader["UltimoMovPromo"].ToString(),
                    //Institucion = reader["Institucion"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }



        public List<prop.Tramite> Tramite_UltimoEstatusTramite(DateTime fechaDesde, DateTime fechaHasta, int IdFlujo)
        {
            DataTable dt = new DataTable();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            b.ExecuteCommandSP("Tramite_UltimoEstatusTramite");
            b.AddParameter("@Inicio", fechaD, SqlDbType.NVarChar);
            b.AddParameter("@Fin", fechaH, SqlDbType.NVarChar);
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            List<prop.Tramite> resultado = new List<prop.Tramite>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Tramite item = new prop.Tramite()
                {
                    IdTramite = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"].ToString()),
                    FolioCompuesto = reader["FolioCompuesto"].ToString(),
                    NumeroOrden = reader["NumeroOrden"].ToString(),
                    Operacion = reader["Operacion"].ToString(),
                    //Producto = reader["Producto"].ToString(),
                    Contratante = reader["Contratante"].ToString(),
                    //RFC = reader["RFC"].ToString(),
                    //Titular = reader["Titular"].ToString(),
                    FechaSolicitud = Convert.ToDateTime(reader["FechaSolicitud"].ToString()),
                    Estatus = reader["Estatus"].ToString(),
                    IdSisLegados = reader["IdSisLegados"].ToString(),
                    kwik = reader["kwik"].ToString(),
                    Promotoria = reader["Promotoria"].ToString(),
                    GA = reader["GA"].ToString(),
                    GU = reader["GU"].ToString(),
                    PromotoriaZona = reader["PromotoriaZona"].ToString(),
                    TipoServicio = reader["TipoServicio"].ToString(),

                    PrimaTotal = reader["PrimaTotal"].ToString(),
                    PrimaExcedente = reader["PrimaExcedente"].ToString(),
                    PrimaDiferencia = reader["PrimaDiferencia"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.BitacoraSabana> SabanaConsultaBitacoraDescarga()
        {
            b.ExecuteCommandSP("SabanaConsultaBitacoraDescarga");
            List<prop.BitacoraSabana> resultado = new List<prop.BitacoraSabana>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.BitacoraSabana item = new prop.BitacoraSabana()
                {
                    FechaRegistro = reader["FechaRegistro"].ToString(),
                    FechaInicio = reader["FechaInicio"].ToString(),
                    FechaFin = reader["FechaFin"].ToString(),
                    NumRegistros = reader["NumRegistro"].ToString(),
                    Usuario = reader["Usuario"].ToString(),
                    NumSolicitudes = reader["NumSolicitudes"].ToString(),
                };

                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.DetalleMesa> Tramite_InformacionBitacora(int IdTramite)
        {
            b.ExecuteCommandSP("Tramite_InformacionBitacora");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            List<prop.DetalleMesa> resultado = new List<prop.DetalleMesa>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.DetalleMesa item = new prop.DetalleMesa()
                {
                    IdTramite = reader["IdTramite"].ToString(),
                    IdMesa = reader["IdMesa"].ToString(),
                    Nombre = reader["Nombre"].ToString(),
                    FechaRegistro = reader["FechaRegistro"].ToString(),
                    FechaInicio = reader["FechaInicio"].ToString(),
                    FechaTermino = reader["FechaTermino"].ToString(),
                    EstadoMesa = reader["EstadoMesa"].ToString(),
                    Observacion = reader["Observacion"].ToString(),
                    NombreUsuario = reader["NombreUsuario"].ToString(),
                    NMESA = reader["NMESA"].ToString(),
                    NORDENREPORTE = reader["NORDENREPORTE"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }


        public DataTable Sabana(DateTime fechaDesde, DateTime fechaHasta, int IdUsuario, int IdFlujo)
        {
            DataSet dt = new DataSet();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;
            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            string consulta = "";
            consulta = "EXEC SabanaReporteV2 " + "'" + fechaD + "','" + fechaH + "'," + IdUsuario.ToString();
            
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }

        public DataTable DatosReporteSabana(string fechaDesde, string fechaHasta, string idFlujo)
        {
            string consulta = "EXEC SABANA_Generar_Reporte @fechaDesde,@fechaHasta,@idFlujo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("fechaDesde", fechaDesde, SqlDbType.Date);
            b.AddParameter("fechaHasta", fechaHasta, SqlDbType.Date);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            return b.Select();
        }

        public DataTable DatosReporteGeneralMesa(string fechaDesde, string fechaHasta,string idFlujo)
        {
            DataTable dt = new DataTable();
            DataTable mesa = new DataTable();
            DataTable mesaTranspuesta = new DataTable();
            DataTable Suspendidos = new DataTable();
            DataTable SuspendidosTrans = new DataTable();
            DataTable Hold = new DataTable();
            DataTable Rechazado = new DataTable();
            DataTable Ejecucion = new DataTable();
            DataTable Registrado = new DataTable();
            DataTable Proceso = new DataTable();

            string qMesa = "SELECT DISTINCT Nombre as ESTATUS FROM mesa WHERE Activo=1 AND IdFlujo=@idFlujo";
            b.ExecuteCommandQuery(qMesa);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            mesa = b.Select();
            mesa.Rows.Add("TOTAL");

            string tSuspendido = string.Empty;
            string tHold = string.Empty;
            string tRechazado = string.Empty;
            string tEjecucion = string.Empty;
            string tProceso = string.Empty;
            int indice;

            foreach (DataRow mesaNombre in mesa.Rows)
            {
                if (!string.Equals("TOTAL", mesaNombre[0]))
                {
                    string MesaNombre = mesaNombre[0].ToString();
                    tSuspendido += "SELECT ISNULL(SUM(Total),0) as Suspendidos FROM FN_TotalMesa(@fechaDesde,@fechaHasta,'"+MesaNombre+"','9',"+ idFlujo + ") " +
                                   "UNION ALL ";
                    tHold += "SELECT ISNULL(SUM(Total),0) as Hold FROM FN_TotalMesa(@fechaDesde,@fechaHasta,'"+MesaNombre+"','6',"+idFlujo +") " +
                             "UNION ALL ";
                    tRechazado += "SELECT ISNULL(SUM(Total),0) as Rechazados FROM FN_TotalMesa(@fechaDesde,@fechaHasta,'"+MesaNombre+"','18',"+ idFlujo+") " +
                                  "UNION ALL ";
                    tEjecucion += "SELECT ISNULL(SUM(Total),0) as Ejecucion FROM FN_TotalMesa(@fechaDesde,@fechaHasta,'"+MesaNombre+"','2,7,8,12,13,16,10,11',"+idFlujo +") " +
                                  "UNION ALL ";
                    tProceso += "SELECT ISNULL(SUM(Total),0) as Procesado FROM FN_TotalMesa(@fechaDesde,@fechaHasta,'"+MesaNombre+"','17',"+ idFlujo+") " +
                                "UNION ALL ";
                }
            }

            //Totales
            tSuspendido += "SELECT ISNULL(SUM(Total),0) as Suspendidos FROM FN_TotalMesa(@fechaDesde,@fechaHasta,'%','9',@idFlujo)";
            tHold += "SELECT ISNULL(SUM(Total),0) as Hold FROM FN_TotalMesa(@fechaDesde,@fechaHasta,'%','6',@idFlujo)";
            tRechazado += "SELECT ISNULL(SUM(Total),0) as Rechazados FROM FN_TotalMesa(@fechaDesde,@fechaHasta,'%','18',@idFlujo)";
            tEjecucion += "SELECT ISNULL(SUM(Total),0) as Ejecucion FROM FN_TotalMesa(@fechaDesde,@fechaHasta,'%','2,7,8,12,13,16,10,11',@idFlujo)";
            tProceso += "SELECT ISNULL(SUM(Total),0) as Procesado FROM FN_TotalMesa(@fechaDesde,@fechaHasta,'%','17',@idFlujo)";

            mesaTranspuesta = GenerateTransposedTable(mesa);

            b.ExecuteCommandQuery(tSuspendido);
            b.AddParameter("fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            Suspendidos = b.Select();
          
            b.ExecuteCommandQuery(tHold);
            b.AddParameter("fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            Hold = b.Select();

            b.ExecuteCommandQuery(tRechazado);
            b.AddParameter("fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            Rechazado = b.Select();

            b.ExecuteCommandQuery(tEjecucion);
            b.AddParameter("fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            Ejecucion = b.Select();

            b.ExecuteCommandQuery(tProceso);
            b.AddParameter("fechaDesde", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("fechaHasta", fechaHasta, SqlDbType.DateTime);
            b.AddParameter("idFlujo", idFlujo, SqlDbType.Int);
            Proceso = b.Select();

            foreach (DataColumn NMesa in mesaTranspuesta.Columns)
            {
                if (!string.Equals(NMesa.Caption, "ESTATUS"))
                {
                    dt.Columns.Add(NMesa.Caption, typeof(Int32));
                }
                else dt.Columns.Add(NMesa.Caption);
            }

            DataRow OSupendidos = dt.NewRow();
            indice = 0;
            foreach (DataColumn HeaderDG in dt.Columns)
            {
                if (string.Equals("ESTATUS", HeaderDG.ColumnName))
                {
                    OSupendidos[HeaderDG.ColumnName] = "SUSPENDIDOS";
                }
                else
                {
                    OSupendidos[HeaderDG.ColumnName] = Suspendidos.Rows[indice][0];
                    indice++;
                }
            }
            dt.Rows.Add(OSupendidos);

            DataRow OHold = dt.NewRow();
            indice = 0;
            foreach (DataColumn HeaderDG in dt.Columns)
            {
                if (string.Equals("ESTATUS", HeaderDG.ColumnName))
                {
                    OHold[HeaderDG.ColumnName] = "HOLD";
                }
                else
                {
                    OHold[HeaderDG.ColumnName] = Hold.Rows[indice][0];
                    indice++;
                }

            }
            dt.Rows.Add(OHold);

            DataRow ORechazados = dt.NewRow();
            indice = 0;
            foreach (DataColumn HeaderDG in dt.Columns)
            {
                if (string.Equals("ESTATUS", HeaderDG.ColumnName))
                {
                    ORechazados[HeaderDG.ColumnName] = "RECHAZADOS";
                }
                else
                {
                    ORechazados[HeaderDG.ColumnName] = Rechazado.Rows[indice][0];
                    indice++;
                }

            }
            dt.Rows.Add(ORechazados);

            DataRow OEjecutados = dt.NewRow();
            indice = 0;
            foreach (DataColumn HeaderDG in dt.Columns)
            {
                if (string.Equals("ESTATUS", HeaderDG.ColumnName))
                {
                    OEjecutados[HeaderDG.ColumnName] = "EN ATENCIÓN";
                }
                else
                {
                    OEjecutados[HeaderDG.ColumnName] = Ejecucion.Rows[indice][0];
                    indice++;
                }

            }
            dt.Rows.Add(OEjecutados);
            DataRow OProcesados = dt.NewRow();
            indice = 0;
            foreach (DataColumn HeaderDG in dt.Columns)
            {
                if (string.Equals("ESTATUS", HeaderDG.ColumnName))
                {
                    OProcesados[HeaderDG.ColumnName] = "PROCESADOS";
                }
                else
                {
                    OProcesados[HeaderDG.ColumnName] = Proceso.Rows[indice][0];
                    indice++;
                }

            }
            dt.Rows.Add(OProcesados);
            return dt;
        }
        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();
            // Se agregan las columnas haciendo un ciclo para cada fila
            // El encabezado de la primera columna es el mismo. 
            outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());
            // El encabezado para las demas columnas
            foreach (DataRow inRow in inputTable.Rows)
            {
                string newColName = inRow[0].ToString();
                outputTable.Columns.Add(newColName);
            }
            // Se agregan las columnas por cada renglón        
            for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();
                newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }
            return outputTable;
        }

        public List<WFO.Propiedades.Procesos.SupervisionGeneral.Mesa> SeleccionarPorFlujo(string id)
        {
            List<Propiedades.Procesos.SupervisionGeneral.Mesa> resultado = new List<Propiedades.Procesos.SupervisionGeneral.Mesa>();
            try
            {

                b.ExecuteCommandSP("Mesa_Seleccionar");
                b.AddParameter("@idtramite", id, SqlDbType.Int);
                
                var reader = b.ExecuteReader();
                while (reader.Read())
                {
                    Propiedades.Procesos.SupervisionGeneral.Mesa item = new Propiedades.Procesos.SupervisionGeneral.Mesa()
                    {
                        _IdTramiteMesa = string.IsNullOrEmpty(reader["IdTramiteMesa"].ToString()) ? 0 : int.Parse(reader["IdTramiteMesa"].ToString()),
                        _Mesa = reader["Mesa"].ToString(),
                        _Estado = string.IsNullOrEmpty(reader["Estado"].ToString()) ? "" : reader["Estado"].ToString(),
                        _Usuario = string.IsNullOrEmpty(reader["Usuario"].ToString()) ? "" : reader["Usuario"].ToString()
                    };
                    resultado.Add(item);
                }
            }
            catch (Exception )
            {

            }
            finally
            {
                //reader = null;
                b.ConnectionCloseToTransaction();
            }
            return resultado;
        }

    }
}
