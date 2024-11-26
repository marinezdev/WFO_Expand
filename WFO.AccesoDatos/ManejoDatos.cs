using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WFO.AccesoDatos
{
    internal sealed class ManejoDatos
    {
        private SqlConnection dbconnection = null;
        private SqlCommand cmd;
        SqlTransaction transaction;

        public ManejoDatos()
        {
            CloseConnection();
        }

        /// <summary>
        /// Abre una conexión de datos
        /// </summary>		
        private void OpenConnection()
        {
            dbconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString);
            dbconnection.Open();
        }

        /// <summary>
        /// Cierra una conexión de datos
        /// </summary>		
        public void CloseConnection()
        {
            if (dbconnection != null && dbconnection.State == ConnectionState.Open)
            {
                dbconnection.Close();
                dbconnection.Dispose();
            }

        }

        /// <summary>
        /// Procesa una consulta de datos 
        /// </summary>
        internal void ExecuteCommandQuery(string query)
        {
            cmd = new SqlCommand()
            {
                CommandType = CommandType.Text,
                CommandText = query
            };
            cmd.CommandTimeout = 0;
        }

        /// <summary>
        /// Procesa un stored procedure
        /// </summary>		
        internal void ExecuteCommandSP(string name)
        {
            cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = name
            };
            cmd.CommandTimeout = 0;
        }

        /// <summary>
        /// Agrega un parametro a una consulta
        /// </summary>
        /// <param name="name">Nombre del parametro</param>
        /// <param name="value">Valor asignado al parametro</param>
        /// <param name="type">Tipo de dato</param>
        internal void AddParameter(string name, object value, SqlDbType type)
        {
            SqlParameter param = new SqlParameter()
            {
                ParameterName = name,
                Value = value,
                SqlDbType = type
            };
            cmd.Parameters.Add(param);
        }

        /// <summary>
        /// Agrega un parametro para cadenas con tamaño (sólo usar para cadenas)
        /// </summary>
        /// <param name="name">Nombre del párametro</param>
        /// <param name="value">Valor del parámetro</param>
        /// <param name="type">Tipo del parámetro</param>
        /// <param name="size">Tamaño del parámetro</param>
        internal void AddParameter(string name, object value, SqlDbType type, int size)
        {
            SqlParameter param = new SqlParameter()
            {
                ParameterName = name,
                Value = value,
                SqlDbType = type,
                Size = size
            };
            cmd.Parameters.Add(param);
        }

        /// <summary>
        /// Agrega un parametro de retorno a una consulta
        /// </summary>
        /// <param name="name">Nombre del parametro</param>
        internal void AddParameterWithReturnValue(string name)
        {
            SqlParameter param = new SqlParameter(name, SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(param);
        }

        /// <summary>
        /// Obtiene un datatable con los registros obtenidos de una consulta
        /// </summary>		
        internal DataTable Select()
        {
            DataTable dt = new DataTable();
            OpenConnection();
            cmd.Connection = dbconnection;
            cmd.CommandTimeout = 0;
            dt.Load(cmd.ExecuteReader());
            CloseConnection();
            return dt;
        }

        /// <summary>
        /// Obtiene una fila de un registro con los datos obtenidos de una consulta
        /// </summary>		
        internal DataRow SelectDataRow()
        {
            DataTable dt = new DataTable();
            OpenConnection();
            cmd.Connection = dbconnection;
            cmd.CommandTimeout = 0;
            dt.Load(cmd.ExecuteReader());
            CloseConnection();
            return dt.Rows[0];
        }

        /// <summary>
        /// Obtiene un valor de un registro con los datos obtenidos de una consulta
        /// </summary>
        internal string SelectString()
        {
            DataTable dt = new DataTable();
            OpenConnection();
            cmd.Connection = dbconnection;
            cmd.CommandTimeout = 0;
            dt.Load(cmd.ExecuteReader());
            CloseConnection();
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }

        /// <summary>
        /// Ejecuta una consulta en funciones o stored procedures.
        /// Este método es para poder ejecutar más de una consulta a la vez y obtener
        /// un valor con una función de sql server como el @@identity.
        /// También se puede usar para agregar, actualizar o borrar
        /// </summary>
        /// <example>
        /// INSERT INTO tabla (campo1, campo2) VALUES(@param1, @param2)
        /// SELECT @@Identity
        /// ExecuteCommandQuery(consulta);
        /// AddParameter("@param1",valor1, DbType.String);
        /// AddParameter("@param2",valor2, DbType.String);
        /// return SelectWithReturnValue();
        /// En este procedimiento, se obtendría el valor de SELECT @@Identity y ese valor sería el devuelto.
        /// </example>
        /// <returns>Valor obtenido</returns>
        internal int SelectWithReturnValue()
        {
            OpenConnection();
            cmd.Connection = dbconnection;
            cmd.CommandTimeout = 0;
            int obtenido = int.Parse(cmd.ExecuteScalar().ToString());
            CloseConnection();
            return obtenido;
        }

        internal string SelectWithReturnValuePrueba()
        {
            OpenConnection();
            cmd.Connection = dbconnection;
            cmd.CommandTimeout = 0;
            string obtenido = cmd.ExecuteScalar().ToString();
            CloseConnection();
            return obtenido;
        }

        /// <summary>
        /// Ejecuta una consulta con funciones o stored procedures.
        /// Devuelve un dataset con los datos obtenidos.
        /// </summary>
        /// <example>
        /// ExecuteCommandQuery("SELECT dbofunciondeprueba(@param1, @param2) AS Users");
        /// AddParameter("Ramiro","@param1", DbType.String);
        /// AddParameter("Ramirez","@param2", DbType.String);
        /// return SelectExecuteFunctions().Rows[0][0].ToString;
        /// </example>
        /// <returns>Dataset con el valor obtenido</returns>
        internal DataSet SelectExecuteFunctions()
        {
            DataSet ds = new DataSet();
            OpenConnection();
            cmd.Connection = dbconnection;
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(cmd);
            sqldataadapter.SelectCommand.CommandTimeout = 0;
            sqldataadapter.Fill(ds);
            CloseConnection();
            return ds;
        }

        /// <summary>
        /// Ejecuta una consulta de inserción, actualización o borrado, devuelve un valor mayor
        /// a uno si se ejecutó correctamente, cero si no.
        /// </summary>		
        internal int InsertUpdateDelete()
        {
            OpenConnection();
            cmd.Connection = dbconnection;
            cmd.CommandTimeout = 0;
            int contar = cmd.ExecuteNonQuery();
            CloseConnection();
            return contar;
        }

        /// <summary>
        /// Ejecuta una consulta de inserción con un valor de retorno (para usarse en stored procedures)
        /// </summary>				
        internal int InsertWithReturnValue()
        {
            int recibido = 0;
            OpenConnection();
            cmd.Connection = dbconnection;
            cmd.CommandTimeout = 0;
            cmd.ExecuteScalar();
            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                if (cmd.Parameters[i].Direction == ParameterDirection.Output ||
                    cmd.Parameters[i].Direction == ParameterDirection.InputOutput ||
                    cmd.Parameters[i].Direction == ParameterDirection.ReturnValue)
                {
                    recibido = (int)cmd.Parameters[i].Value;
                }
            }
            CloseConnection();
            return recibido;
        }

        #region Procesos para transacciones ************************************************

        /// <summary>
        /// Proceso para transacciones de inserción o actualización de datos
        /// con transacción.
        /// </summary>
        /// <returns></returns>
        internal int InsertUpdateDeleteWithTransaction()
        {
            int contar = 0;
            try
            {
                OpenConnection();
                cmd.Connection = dbconnection;
                transaction = dbconnection.BeginTransaction();
                cmd.Transaction = transaction;
                contar = cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception xx)
            {
                WFO.RegistraLog.RegistraLog log = new RegistraLog.RegistraLog("Log", HttpContext.Current.Server.MapPath("~"), "WFO Error Datos");
                log.Agregar("Error al intentar agregar, actualizar, modificar: " + xx);
                transaction.Rollback();
            }
            finally
            {
                CloseConnection();
            }
            return contar;
        }

        /// <summary>
        /// Comienza una transacción para un proceso
        /// </summary>
        internal void BeginTransaction()
        {
            transaction = dbconnection.BeginTransaction();
            cmd.Transaction = transaction;
        }

        /// <summary>
        /// Abre una conexión de datos para un proceso de transacción
        /// </summary>
        internal void ConnectionOpenToTransaction()
        {
            OpenConnection();
        }

        /// <summary>
        /// Ejecuta un proceso NonQuery dentro de una transacción
        /// </summary>
        /// <returns></returns>
        internal int ExecuteNonQueryToTransaction()
        {
            int contar = 0;
            cmd.Connection = dbconnection;
            cmd.CommandTimeout = 0;
            contar = cmd.ExecuteNonQuery();
            return contar;
        }

        /// <summary>
        /// Ejecuta un proceso Scalar con retorno de valor dentro de 
        /// una transacción
        /// </summary>
        /// <returns></returns>
        internal int ExecuteScalarToTransactionWithReturnValue()
        {
            int obtenido = 0;
            cmd.Connection = dbconnection;
            cmd.CommandTimeout = 0;
            obtenido = int.Parse(cmd.ExecuteScalar().ToString());
            return obtenido;
        }

        internal DataSet SelectExecuteFunctionsToTransaction()
        {
            DataSet ds = new DataSet();
            cmd.Connection = dbconnection;
            cmd.CommandTimeout = 0;
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(cmd);
            sqldataadapter.Fill(ds);
            return ds;
        }



        /// <summary>
        /// Confirma un proceso dentro de una transacción
        /// </summary>
        internal void CommitTransaction()
        {
            transaction.Commit();
        }

        /// <summary>
        /// Cierra la conexión dentro de un proceso 
        /// transacción
        /// </summary>
        internal void ConnectionCloseToTransaction()
        {
            CloseConnection();
        }

        /// <summary>
        /// Devuelve la transacción al inicio si hay
        /// una falla.
        /// </summary>
        internal void RollBackTransaction()
        {
            transaction.Rollback();
        }

        #endregion

        #region Procesos para llenar listas con DataReader

        /// <summary>
        /// Ejecuta un datareader par allenar listas (casi genérico
        /// </summary>
        /// <remarks>
        /// Ejemplo de uso dentro de un método
        /// string consulta = "SELECT * FROM insserviciosdetalle";
        /// b.ExecuteCommandQuery(consulta);
        /// List<InsServiciosDetalleEntity> resultado = new List<InsServiciosDetalleEntity>();
        /// var reader = b.ExecuteReader();
        /// while (reader.Read())
        /// {
        ///    InsServiciosDetalleEntity itm = new InsServiciosDetalleEntity()
        ///    {
        ///         IdInsServicios = reader["IdInsServiciosDetalle"].ToString()
        ///    };
        ///    resultado.Add(itm);
        /// }
        /// reader = null;
        /// b.ConnectionClose();
        /// return resultado;
        /// </remarks>
        /// <returns>DataReader para leerlo y llenar un List<></returns>
        internal SqlDataReader ExecuteReader()
        {
            OpenConnection();
            cmd.Connection = dbconnection;
            cmd.CommandTimeout = 0;
            var reader = cmd.ExecuteReader();
            return reader;

        }
        #endregion
    }
}
