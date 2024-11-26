using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class ArchivosDependencias
    {
        ManejoDatos b = new ManejoDatos();

        //Selección

        public DataTable Seleccionar()
        {
            string consulta = "SELECT a.Nombre, a.Fecha, a.Folio, MAX(b.Estado) AS Estado, " +
            "a.Poliza, " +
            "CASE a.Cobertura " +
            "WHEN 1 THEN 'Básica' " +
            "WHEN 2 THEN 'Potenciada' " +
            "END AS Cobertura, " +
            "a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF " +
            "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
            "ON a.Folio = b.Folio " +
            "GROUP BY a.Nombre, a.Fecha, a.Folio, a.Poliza, a.Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }

        public DataTable SeleccionarPorCoberturaBasicaDependencia(string dependencia)
        {
            DataTable dt = new DataTable();
            try
            {
                string consulta = " " +
                "SELECT 1 AS Orden, 'En Trámite' AS Estado, COUNT(*) AS Totales  FROM archivosdependencias WHERE cobertura=1 AND estado=1 AND OpDep=@dep " +
                "UNION " +
                "SELECT 2 AS Orden, 'Suspendido' AS Suspendido, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=2 AND OpDep=@dep " +
                "UNION " +
                "SELECT 3 AS Orden, 'En Proceso' AS [En  Proceso], COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=3 AND OpDep=@dep " +
                "UNION " +
                "SELECT 4 AS Orden, 'Reenvío' AS Reenvío, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=4 AND OpDep=@dep " +
                "UNION " +
                "SELECT 5 AS Orden, 'Revisión' AS Revisión, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=5 AND OpDep=@dep " +
                "UNION " +
                "SELECT 6 AS Orden, 'Concluído' AS Concluído, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=6 AND OpDep=@dep";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@dep", dependencia, SqlDbType.Int);

                b.ConnectionOpenToTransaction();
                b.BeginTransaction();

                dt = b.SelectExecuteFunctionsToTransaction().Tables[0];
                b.CommitTransaction();

            }
            catch
            {
                b.RollBackTransaction();
            }
            finally
            {
                b.ConnectionCloseToTransaction();
            }

            return dt;
        }

        public DataTable SeleccionarPorCoberturaPotenciadaDependencia(string dependencia)
        {
            DataTable dt = new DataTable();
            try
            {
                string consulta = "SELECT 1 AS Orden, 'En Trámite' AS Estado, COUNT(*) AS Totales  FROM archivosdependencias WHERE cobertura=2 AND estado=1 AND OpDep=@dep " +
                "UNION " +
                "SELECT 2 AS Orden, 'Suspendido' AS Suspendido, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=2 AND estado=2 AND OpDep=@dep " +
                "UNION " +
                "SELECT 3 AS Orden, 'En Proceso' AS[En  Proceso], COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=2 AND estado=3 AND OpDep=@dep " +
                "UNION " +
                "SELECT 4 AS Orden, 'Reenvío' AS Reenvío, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=2 AND estado=4 AND OpDep=@dep " +
                "UNION " +
                "SELECT 5 AS Orden, 'Revisión' AS Revisión, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=2 AND estado=5 AND OpDep=@dep " +
                "UNION " +
                "SELECT 6 AS Orden, 'Concluído' AS Concluído, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=2 AND estado=6 AND OpDep=@dep";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@dep", dependencia, SqlDbType.Int);

                b.ConnectionOpenToTransaction();
                b.BeginTransaction();

                dt = b.SelectExecuteFunctionsToTransaction().Tables[0];
                b.CommitTransaction();

            }
            catch
            {
                b.RollBackTransaction();
            }
            finally
            {
                b.ConnectionCloseToTransaction();
            }

            return dt;
        }

        public DataTable SeleccionarPorEstadoCobertura(string estado, string cobertura)
        {
            string consulta = "SELECT a.Nombre, a.Fecha, a.Folio, MAX(b.Estado) AS Estado, " +
            "a.Poliza, CASE a.Cobertura " +
            "WHEN 1 THEN 'Básica' " +
            "WHEN 2 THEN 'Potenciada' " +
            "END AS Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF " +
            "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
            "ON a.Folio = b.Folio " +
            "WHERE a.Estado=@estado " +
            "AND a.Cobertura=@cobertura " +
            "GROUP BY a.Nombre, a.Fecha, a.Folio, a.Poliza, a.Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, SqlDbType.NVarChar);
            b.AddParameter("@cobertura", cobertura, SqlDbType.NVarChar);
            return b.Select();
        }

        public DataTable SeleccionarPorDependencia(string dependencia)
        {
            string consulta = "SELECT a.Nombre, a.Fecha, a.Folio, MAX(b.Estado) AS Estado, " +
            "a.Poliza, CASE a.Cobertura " +
            "WHEN 1 THEN 'Básica' " +
            "WHEN 2 THEN 'Potenciada' " +
            "END AS Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF, a.OpDep " +
            "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
            "ON a.Folio = b.Folio " +
            "WHERE a.OpDep=@dependencia " +
            "GROUP BY a.Nombre, a.Fecha, a.Folio, a.Poliza, a.Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF, a.OpDep";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@dependencia", dependencia, SqlDbType.Int);
            return b.Select();
        }

        public DataTable SeleccionarPorDependenciaEstadoCobertura(string estado, string cobertura, string dependencia)
        {
            string consulta = "SELECT a.Nombre, a.Fecha, a.Folio, MAX(b.Estado) AS Estado, " +
            "a.Poliza, CASE a.Cobertura " +
            "WHEN 1 THEN 'Básica' " +
            "WHEN 2 THEN 'Potenciada' " +
            "END AS Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF, a.OpDep " +
            "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
            "ON a.Folio = b.Folio " +
            "WHERE a.Estado=@estado " +
            "AND a.Cobertura=@cobertura " +
            "AND a.OpDep=@dependencia " +
            "GROUP BY a.Nombre, a.Fecha, a.Folio, a.Poliza, a.Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF, a.OpDep";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, SqlDbType.NVarChar);
            b.AddParameter("@cobertura", cobertura, SqlDbType.NVarChar);
            b.AddParameter("@dependencia", dependencia, SqlDbType.Int);
            return b.Select();
        }

        public DataTable SeleccionarTramitesPorUsuario(string usuario)
        {
            string consulta = "SELECT a.Nombre, a.Fecha, a.Folio, MAX(b.Estado) AS Estado, a.Poliza, " +
            "CASE a.Cobertura " +
            "WHEN 1 THEN 'Básica' " +
            "WHEN 2 THEN 'Potenciada' " +
            "END AS Cobertura, " +
            "a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF " +
            "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
            "ON a.Folio = b.Folio " +
            "WHERE a.UsuarioAsignado=@usuario " +
            "GROUP BY a.Nombre, a.Fecha, a.Folio, a.Poliza, a.Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, " +
            "a.Archivo100PosCanc, a.Documento, a.CartaPDF";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@usuario", usuario, SqlDbType.Int);
            return b.Select();
        }

        public DataTable SeleccionarTramitesPorUsuarioPorEstadoPorCobertura(string estado, string cobertura, string usuario)
        {
            string consulta = "SELECT a.Nombre, a.Fecha, a.Folio, MAX(b.Estado) AS Estado, " +
            "a.Poliza, CASE a.Cobertura " +
            "WHEN 1 THEN 'Básica' " +
            "WHEN 2 THEN 'Potenciada' " +
            "END AS Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF " +
            "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
            "ON a.Folio = b.Folio " +
            "WHERE a.UsuarioAsignado=@usuario " +
            "AND a.Estado=@estado " +
            "AND a.Cobertura=@cobertura " +
            "GROUP BY a.Nombre, a.Fecha, a.Folio, a.Poliza, a.Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@usuario", usuario, SqlDbType.Int);
            b.AddParameter("@estado", estado, SqlDbType.NVarChar);
            b.AddParameter("@cobertura", cobertura, SqlDbType.NVarChar);
            return b.Select();
        }

        public DataTable SeleccionarPrimerTramiteDisponibleParaAnalista(string cobertura)
        {
            string consulta = "SELECT TOP 1 a.Folio, a.Cobertura, MAX(b.Estado) AS Estado " +
            "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
            "ON a.Folio = b.Folio " +
            "WHERE b.Estado=1 AND a.Folio NOT IN(SELECT Folio FROM ArchivosDependenciasAsignados) " +
            "AND a.Cobertura=@cobertura " +
            "GROUP BY a.folio, a.Cobertura ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@cobertura", cobertura, SqlDbType.Int);
            return b.Select();
        }

        public string SeleccionarTramiteYaAsignadoAAnalista(string idusuario, string estado)
        {
            string consulta = "SELECT a.Folio " +
            "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b ON a.Folio=b.Folio " +
            "LEFT JOIN ArchivosDependenciasAsignados c ON a.Folio = c.Folio " +
            "WHERE a.Folio NOT IN(SELECT Folio FROM ArchivosDependenciasEstados WHERE Estado=@estado) " +
            "AND c.IdUsuario=@idusuario " +
            "GROUP BY a.Folio ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            return b.SelectString();
        }

        public string SeleccionarTramiteAsignadoUsuario(string idusuario)
        {
            string consulta = "SELECT UsuarioAsignado FROM ArchivosDependencias WHERE Estado=1";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.SelectString();
        }

        public DataTable SeleccionarTramiteParaTerminarAsignadoAUsuarioBasica(string idusuario)
        {
            string consulta = "SELECT Folio, Cobertura, UsuarioAsignado, Estado " +
            "FROM ArchivosDependencias " +
            "WHERE Estado=5 OR Estado=4" +
            "AND UsuarioAsignado=@idusuario " +
            "AND Cobertura=1";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.Select();
        }

        public DataTable SeleccionarTramiteParaTerminarAsignadoAUsuarioPotenciacion(string idusuario)
        {
            string consulta = "SELECT Folio, Cobertura, UsuarioAsignado, Estado " +
            "FROM ArchivosDependencias " +
            "WHERE Estado=7 " +
            "AND UsuarioAsignado=@idusuario " +
            "AND Cobertura=2";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            return b.Select();
        }

        public DataTable SeleccionarPrimerTramiteDisponibleParaEjecutivoBasica()
        {
            string consulta = "SELECT Folio, Estado From ArchivosDependencias WHERE Estado=3";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }

        public DataTable SeleccionarPrimerTramiteDisponibleParaAnalistaBackPotenciacion()
        {
            string consulta = "SELECT Folio, Estado From ArchivosDependencias WHERE Estado=3  AND Cobertura=2";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }

        public DataTable SeleccionarPrimerTramiteDisponibleParaAnalistaFrontPotenciacion()
        {
            string consulta = "SELECT Folio, Estado From ArchivosDependencias WHERE Estado=5 AND Cobertura=2";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }

        public DataTable SeleccionarTramiteDevueltoParaRevision()
        {
            string consulta = "SELECT Folio, Estado FROM ArchivosDependencias WHERE estado=6 AND cobertura=2";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }




        public DataTable SeleccionarPorCoberturaBasicaSupervisor()
        {
            DataTable dt = new DataTable();
            try
            {
                string consulta = "SELECT 1 AS Orden, 'En Trámite' AS Estado, COUNT(*) AS Totales  FROM archivosdependencias WHERE cobertura=1 AND estado=1 " +
                "UNION " +
                "SELECT 2 AS Orden, 'Suspendido' AS Suspendido, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=2 " +
                "UNION " +
                "SELECT 3 AS Orden, 'En Proceso' AS[En  Proceso], COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=3 " +
                "UNION " +
                "SELECT 4 AS Orden, 'Reenvío' AS Reenvío, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=4 " +
                "UNION " +
                "SELECT 5 AS Orden, 'Revisión' AS Revisión, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=5 " +
                "UNION " +
                "SELECT 6 AS Orden, 'Concluído' AS Concluído, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=6";
                b.ExecuteCommandQuery(consulta);

                b.ConnectionOpenToTransaction();
                b.BeginTransaction();

                dt = b.SelectExecuteFunctionsToTransaction().Tables[0];
                b.CommitTransaction();

            }
            catch
            {
                b.RollBackTransaction();
            }
            finally
            {
                b.ConnectionCloseToTransaction();
            }

            return dt;
        }

        public DataTable SeleccionarPorCoberturaPotenciadaSupervisor()
        {
            DataTable dt = new DataTable();
            try
            {
                string consulta = "SELECT 1 AS Orden, 'En Trámite' AS Estado, COUNT(*) AS Totales  FROM archivosdependencias WHERE cobertura=2 AND estado=1 " +
                "UNION " +
                "SELECT 2 AS Orden, 'Suspendido' AS Suspendido, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura = 2 AND estado = 2 " +
                "UNION " +
                "SELECT 3 AS Orden, 'En Proceso' AS[En  Proceso], COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura = 2 AND estado = 3 " +
                "UNION " +
                "SELECT 4 AS Orden, 'Reenvío' AS Reenvío, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura = 2 AND estado = 4 " +
                "UNION " +
                "SELECT 5 AS Orden, 'Revisión' AS Revisión, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura = 2 AND estado = 5 " +
                "UNION " + 
                "SELECT 6 AS Orden, 'Concluído' AS Concluído, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura = 2 AND estado = 6";
                b.ExecuteCommandQuery(consulta);

                b.ConnectionOpenToTransaction();
                b.BeginTransaction();

                dt = b.SelectExecuteFunctionsToTransaction().Tables[0];
                b.CommitTransaction();

            }
            catch
            {
                b.RollBackTransaction();
            }
            finally
            {
                b.ConnectionCloseToTransaction();
            }

            return dt;
        }

        public DataTable SeleccionarPorCoberturaBasicaUsuario(string usuario)
        {
            DataTable dt = new DataTable();
            try
            {
                string consulta = "SELECT 1 AS Orden, 'En Trámite' AS Estado, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=1 AND UsuarioAsignado=@usuario " +
                "UNION " +
                "SELECT 2 AS Orden, 'Suspendido' AS Suspendido, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=2 AND UsuarioAsignado=@usuario " +
                "UNION " +
                "SELECT 3 AS Orden, 'En Proceso' AS[En  Proceso], COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=3 AND UsuarioAsignado=@usuario " +
                "UNION " +
                "SELECT 4 AS Orden, 'Reenvío' AS Reenvío, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=4 AND UsuarioAsignado=@usuario " +
                "UNION " +
                "SELECT 5 AS Orden, 'Revisión' AS Revisión, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=5 AND UsuarioAsignado=@usuario " +
                "UNION " +
                "SELECT 6 AS Orden, 'Concluído' AS Concluído, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=1 AND estado=6 AND UsuarioAsignado=@usuario";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@usuario", usuario, SqlDbType.Int);

                b.ConnectionOpenToTransaction();
                b.BeginTransaction();

                dt = b.SelectExecuteFunctionsToTransaction().Tables[0];
                b.CommitTransaction();

            }
            catch
            {
                b.RollBackTransaction();
            }
            finally
            {
                b.ConnectionCloseToTransaction();
            }

            return dt;
        }

        public DataTable SeleccionarPorCoberturaPotenciadaUsuario(string usuario)
        {
            DataTable dt = new DataTable();
            try
            {
                string consulta = "SELECT 1 AS Orden, 'En Trámite' AS Estado, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=2 AND estado=1 AND UsuarioAsignado=@dep " +
                "UNION " +
                "SELECT 2 AS Orden, 'Suspendido' AS Suspendido, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=2 AND estado=2 AND UsuarioAsignado=@dep " +
                "UNION " +
                "SELECT 3 AS Orden, 'En Proceso' AS[En  Proceso], COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=2 AND estado=3 AND UsuarioAsignado=@dep " +
                "UNION " +
                "SELECT 4 AS Orden, 'Reenvío' AS Reenvío, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=2 AND estado=4 AND UsuarioAsignado=@dep " +
                "UNION " +
                "SELECT 5 AS Orden, 'Revisión' AS Revisión, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=2 AND estado=5 AND UsuarioAsignado=@dep " +
                "UNION " +
                "SELECT 6 AS Orden, 'Concluído' AS Concluído, COUNT(*) AS Totales FROM archivosdependencias WHERE cobertura=2 AND estado=6 AND UsuarioAsignado=@dep";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@dep", usuario, SqlDbType.Int);

                b.ConnectionOpenToTransaction();
                b.BeginTransaction();

                dt = b.SelectExecuteFunctionsToTransaction().Tables[0];
                b.CommitTransaction();

            }
            catch
            {
                b.RollBackTransaction();
            }
            finally
            {
                b.ConnectionCloseToTransaction();
            }

            return dt;
        }

        public DataTable SeleccionarDetalle(string folio)
        {
            string consulta = "SELECT * FROM archivosdependencias WHERE Folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            return b.Select();
        }


        //Agregar

        public int AgregarBasica(string nombre, string folio, string poliza, string cobertura, string subidopor, string correo, string asunto, string trimestre, string ann)
        {
            string consulta = "INSERT INTO ArchivosDependencias (Nombre, Folio, Fecha, Poliza, Cobertura, SubidoPor, Correo, Asunto, Trimestre, Ann, Estado) " +
                "VALUES(@nombre, @folio, getdate(), @poliza, @cobertura, @subidopor, @correo, @asunto, @trimestre, @ann, 1)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", nombre, SqlDbType.NVarChar, 250);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
            b.AddParameter("@poliza", poliza, SqlDbType.NVarChar, 50);
            b.AddParameter("@cobertura", cobertura, SqlDbType.NVarChar, 1);
            b.AddParameter("@subidopor", subidopor, SqlDbType.NVarChar, 150);
            b.AddParameter("@correo", correo, SqlDbType.NVarChar, 150);
            b.AddParameter("@asunto", asunto, SqlDbType.NVarChar, 350);
            b.AddParameter("@trimestre", trimestre, SqlDbType.Char, 1);
            b.AddParameter("@ann", ann, SqlDbType.Char, 4);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public int AgregarPotenciacion(string nombre, string folio, string poliza, string cobertura, string subidopor, string correo, string asunto, string quincena, string ann2, string rol)
        {
            string consulta = "INSERT INTO ArchivosDependencias (Nombre, Folio, Fecha, Poliza, Cobertura, SubidoPor, Correo, Asunto, Quincena, Ann2, OpDep, Estado) " +
                    "VALUES(@nombre, @folio, getdate(), @poliza, @cobertura, @subidopor, @correo, @asunto, @quincena, @ann2, @rol, 1)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nombre", nombre, SqlDbType.NVarChar, 250);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
            b.AddParameter("@poliza", poliza, SqlDbType.NVarChar, 50);
            b.AddParameter("@cobertura", cobertura, SqlDbType.NVarChar, 1);
            b.AddParameter("@subidopor", subidopor, SqlDbType.NVarChar, 150);
            b.AddParameter("@correo", correo, SqlDbType.NVarChar, 150);
            b.AddParameter("@asunto", asunto, SqlDbType.NVarChar, 350);
            b.AddParameter("@quincena", quincena, SqlDbType.Int);
            b.AddParameter("@ann2", ann2, SqlDbType.Char, 4);
            b.AddParameter("@rol", rol, SqlDbType.Int);
            return b.InsertUpdateDeleteWithTransaction();
        }



        public int AgregarDependenciaATramite(string idusuario, string folio)
        {
            string consulta = "UPDATE archivosdependencias SET opdep=@idusuario WHERE Folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public int AgregarUsuarioAsignadoATramite(string usuario, string folio)
        {
            string consulta = "UPDATE archivosdependencias SET UsuarioAsignado=@usuario WHERE folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@usuario", usuario, SqlDbType.Int);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public int AgregarPDF(string archivopdf, string folio)
        {
            string consulta = "UPDATE archivosdependencias SET archivopdf=@archivopdf WHERE folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
            b.AddParameter("@archivopdf", archivopdf, SqlDbType.NVarChar);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public int AgregarXML(string archivoxls, string folio)
        {
            string consulta = "UPDATE archivosdependencias SET archivoxls=@archivoxls WHERE folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
            b.AddParameter("@archivoxls", archivoxls, SqlDbType.NVarChar);
            return b.InsertUpdateDeleteWithTransaction();
        }



        public int Agregar100PosicionesPagos(string archivo, string folio)
        {
            string consulta = "UPDATE archivosdependencias SET Archivo100PosPagos=@archivo WHERE folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
            b.AddParameter("@archivo", archivo, SqlDbType.NVarChar);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public int Agregar100PosicionesCancelaciones(string archivo, string folio)
        {
            string consulta = "UPDATE archivosdependencias SET Archivo100PosCanc=@archivo WHERE folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
            b.AddParameter("@archivo", archivo, SqlDbType.NVarChar);
            return b.InsertUpdateDeleteWithTransaction();
        }

        //Actualizar

        public int ActualizarEstado(string folio, string estado)
        {
            string consulta = "UPDATE archivosdependencias SET Estado=@estado WHERE folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public int ActualizarAsunto(string folio, string asunto)
        {
            string consulta = "UPDATE archivosdependencias SET asunto=@asunto WHERE folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
            b.AddParameter("@asunto", asunto, SqlDbType.NVarChar);
            return b.InsertUpdateDeleteWithTransaction();
        }

    }
}
