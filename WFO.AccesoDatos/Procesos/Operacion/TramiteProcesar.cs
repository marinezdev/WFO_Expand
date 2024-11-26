using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;
using promotoria = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.AccesoDatos.Procesos.Operacion
{
    public class TramiteProcesar
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.TramiteProcesar> ObtenerTramite(int pIdTramite)
        {
            b.ExecuteCommandSP("Tramite_Selecionar_PorIdTramite");
            b.AddParameter("@IdTramite", pIdTramite, SqlDbType.Int);
            
            List<prop.TramiteProcesar> resultado = new List<prop.TramiteProcesar>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramiteProcesar item = new prop.TramiteProcesar()
                {
                    IdTramite = Convert.ToInt32(reader["Id"].ToString()),
                    Folio = reader["Folio"].ToString(),
                    FechaRegistro = reader["FechaRegistro"].ToString(),
                    //Moneda = reader["Moneda"].ToString(),
                    //PrimaCotizacion = reader["PrimaCotizacion"].ToString(),
                    IdPromotoria = Convert.ToInt32(reader["IdPromotoria"].ToString()),
                    FechaSolicitud = reader["FechaSolicitud"].ToString(),
                    TipoPersona = Convert.ToInt32(reader["TipoPersona"].ToString()),
                    NumeroOrden = reader["NumeroOrden"].ToString(),
                    ContratanteNombre = reader["ContratanteNombre"].ToString(),
                    ContratanteApPaterno = reader["ContratanteApPaterno"].ToString(),
                    ContratanteApMaterno = reader["ContratanteApMaterno"].ToString(),
                    //ContratanteSexo = reader["ContratanteSexo"].ToString(),
                    //RFCContratante = reader["RFCContratante"].ToString(),
                    //Nacionalidad = reader["Nacionalidad"].ToString(),
                    //FechaConst = reader["FechaConst"].ToString(),
                    Contratante = reader["Contratante"].ToString(),
                    //TitularContratante = reader["TitularContratante"].ToString(),
                    //FNacimientoTitular = reader["TitularFechaNacimiento"].ToString(),
                    //TitularNombre = reader["TitularNombre"].ToString(),
                    //TitularApPat = reader["TitularApPat"].ToString(),
                    //TitularApMat = reader["TitularApMat"].ToString(),
                    //TitularNacionalidad = reader["TitularNacionalidad"].ToString(),
                    //TitularSexo = reader["TitularSexo"].ToString(),
                    //Producto = reader["Producto"].ToString(),
                    //SubProducto = reader["SubProducto"].ToString(),
                    StatusTramite = reader["StatusTramite"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramiteProcesar> ObtenerTarea(int pIdUsuario, int pIdMesa, int pIdTramite)
        {
            b.ExecuteCommandSP("spWFOTareaAsignar");
            b.AddParameter("@IdProceso", pIdMesa, SqlDbType.Int);
            b.AddParameter("@IdUsuario", pIdUsuario, SqlDbType.Int);
            b.AddParameter("@IdTramite", pIdTramite, SqlDbType.Int);

            List<prop.TramiteProcesar> resultado = new List<prop.TramiteProcesar>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                if (Convert.ToInt32(reader["Id"].ToString()) <= 0)
                {
                    prop.TramiteProcesar item = new prop.TramiteProcesar()
                    {
                        IdTramite = -1
                    };
                    resultado.Add(item);
                }
                else
                {

                    prop.TramiteProcesar item = new prop.TramiteProcesar()
                    {
                        IdTramite = Convert.ToInt32(reader["Id"].ToString()),
                        Folio = reader["Folio"].ToString(),
                        IdTipoTramite = Convert.ToInt32(reader["IdTipoTramite"].ToString()),
                        FechaRegistro = reader["FechaRegistro"].ToString(),
                        FechaTermino = reader["FechaTermino"].ToString(),
                        IdStatus = Convert.ToInt32(reader["IdStatus"].ToString()),
                        StatusTramite = reader["StatusTramite"].ToString(),
                        IdPromotoria = Convert.ToInt32(reader["IdPromotoria"].ToString()),
                        Promotoria = reader["Promotoria"].ToString(),
                        IdUsuario = Convert.ToInt32(reader["IdUsuario"].ToString()),
                        Usuario = reader["Usuario"].ToString(),
                        IdAgente = Convert.ToInt32(reader["IdAgente"].ToString()),
                        ////Agente = reader["Agente"].ToString(),
                        NumeroOrden = reader["NumeroOrden"].ToString(),
                        FechaSolicitud = reader["FechaSolicitud"].ToString(),
                        //idRamo = Convert.ToInt32(reader["idRamo"].ToString()),
                        idPrioridad = Convert.ToInt32(reader["idPrioridad"].ToString()),
                        Prioridad = reader["Prioridad"].ToString(),
                        IdSisLegados = reader["IdSisLegados"].ToString(),
                        kwik = reader["kwik"].ToString(),

                        SeleccionCompleta = Convert.ToBoolean(reader["SeleccionCompleta"]),

                        TipoPersona = Convert.ToInt32(reader["TipoPersona"].ToString()),
                        Contratante = reader["Contratante"].ToString(),
                        ContratanteNombre = reader["ContratanteNombre"].ToString(),
                        ContratanteApPaterno = reader["ContratanteApPaterno"].ToString(),
                        ContratanteApMaterno = reader["ContratanteApMaterno"].ToString(),

                        //ContratanteSexo = reader["ContratanteSexo"].ToString(),
                        //SexoContratante = reader["SexoContratante"].ToString(),
                        //FNacimientoContratante = reader["FNacimientoContratante"].ToString(),
                        //RFCContratante = reader["RFCContratante"].ToString(),
                        //FechaConst = reader["FechaConst"].ToString(),
                        //Nacionalidad = reader["Nacionalidad"].ToString(),
                        //Nacion = reader["Nacion"].ToString(),
                        //Titular = reader["Titular"].ToString(),
                        //TitularNombre = reader["TitularNombre"].ToString(),
                        //TitularApPat = reader["TitularApPat"].ToString(),
                        //TitularApMat = reader["TitularApMat"].ToString(),
                        //TitularSexo = reader["TitularSexo"].ToString(),
                        //SexoTitular = reader["SexoTitular"].ToString(),
                        //FNacimientoTitular = reader["FNacimientoTitular"].ToString(),
                        //TitularNacionalidad = reader["TitularNacionalidad"].ToString(),
                        //PrimaCotizacion = reader["PrimaCotizacion"].ToString(),

                        //IdMoneda = Convert.ToInt32(reader["IdMoneda"].ToString()),
                        //Moneda = reader["Moneda"].ToString(),
                        //TitularContratante = reader["TitularContratante"].ToString(),
                        //Observaciones = reader["Observaciones"].ToString(),
                        //IdProducto = Convert.ToInt32(reader["IdProducto"].ToString()),
                        //Producto = reader["Producto"].ToString(),
                        //IdSubProducto = Convert.ToInt32(reader["IdSubProducto"].ToString()),
                        //SubProducto = reader["SubProducto"].ToString(),
                        //SumaBasica = Convert.ToDouble(reader["SumaBasica"].ToString()),
                        //HombreClave = Convert.ToInt32(reader["HombreClave"].ToString().Trim()),

                        //IdInstitucion = Convert.ToInt32(reader["IdInstitucion"].ToString()),

                        //Institucion = reader["Institucion"].ToString(),


                        //Convert.ToDouble(reader["SumaBasica"].ToString()),
                        //Convert.ToInt32(reader["HombreClave"].ToString()),

                    };
                    resultado.Add(item);
                }

            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramiteProcesar> ObtenerTramite(int pIdUsuario, int pIdMesa, int pIdTramite)
        {
            b.ExecuteCommandSP("spWFOTramiteAsignar");
            b.AddParameter("@IdMesa", pIdMesa, SqlDbType.Int);
            b.AddParameter("@IdUsuario", pIdUsuario, SqlDbType.Int);
            b.AddParameter("@IdTramite", pIdTramite, SqlDbType.Int);

            List<prop.TramiteProcesar> resultado = new List<prop.TramiteProcesar>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                if (Convert.ToInt32(reader["Id"].ToString()) <= 0)
                {
                    prop.TramiteProcesar item = new prop.TramiteProcesar()
                    {
                        IdTramite = -1
                    };
                    resultado.Add(item);
                }
                else
                {

                    prop.TramiteProcesar item = new prop.TramiteProcesar()
                    {
                        IdTramite = Convert.ToInt32(reader["Id"].ToString()),
                        Folio = reader["Folio"].ToString(),
                        TipoServicio = reader["TipoServicio"].ToString(),
                        IdTipoTramite = Convert.ToInt32(reader["IdTipoTramite"].ToString()),
                        FechaRegistro = reader["FechaRegistro"].ToString(),
                        FechaTermino = reader["FechaTermino"].ToString(),
                        IdStatus = Convert.ToInt32(reader["IdStatus"].ToString()),
                        StatusTramite = reader["StatusTramite"].ToString(),
                        IdPromotoria = Convert.ToInt32(reader["IdPromotoria"].ToString()),
                        Promotoria = reader["Promotoria"].ToString(),
                        IdUsuario = Convert.ToInt32(reader["IdUsuario"].ToString()),
                        Usuario = reader["Usuario"].ToString(),
                        IdAgente = Convert.ToInt32(reader["IdAgente"].ToString()),
                        ////Agente = reader["Agente"].ToString(),
                        NumeroOrden = reader["NumeroOrden"].ToString(),
                        FechaSolicitud = reader["FechaSolicitud"].ToString(),
                        //idRamo = Convert.ToInt32(reader["idRamo"].ToString()),
                        idPrioridad = Convert.ToInt32(reader["idPrioridad"].ToString()),
                        Prioridad = reader["Prioridad"].ToString(),
                        IdSisLegados = reader["IdSisLegados"].ToString(),
                        kwik = reader["kwik"].ToString(),

                        SeleccionCompleta = Convert.ToBoolean(reader["SeleccionCompleta"]),

                        TipoPersona = Convert.ToInt32(reader["TipoPersona"].ToString()),
                        Contratante = reader["Contratante"].ToString(),
                        ContratanteNombre = reader["ContratanteNombre"].ToString(),
                        ContratanteApPaterno = reader["ContratanteApPaterno"].ToString(),
                        ContratanteApMaterno = reader["ContratanteApMaterno"].ToString(),

                        //ContratanteSexo = reader["ContratanteSexo"].ToString(),
                        //SexoContratante = reader["SexoContratante"].ToString(),
                        //FNacimientoContratante = reader["FNacimientoContratante"].ToString(),
                        //RFCContratante = reader["RFCContratante"].ToString(),
                        //FechaConst = reader["FechaConst"].ToString(),
                        //Nacionalidad = reader["Nacionalidad"].ToString(),
                        //Nacion = reader["Nacion"].ToString(),
                        //Titular = reader["Titular"].ToString(),
                        //TitularNombre = reader["TitularNombre"].ToString(),
                        //TitularApPat = reader["TitularApPat"].ToString(),
                        //TitularApMat = reader["TitularApMat"].ToString(),
                        //TitularSexo = reader["TitularSexo"].ToString(),
                        //SexoTitular = reader["SexoTitular"].ToString(),
                        //FNacimientoTitular = reader["FNacimientoTitular"].ToString(),
                        //TitularNacionalidad = reader["TitularNacionalidad"].ToString(),
                        //PrimaCotizacion = reader["PrimaCotizacion"].ToString(),

                        //IdMoneda = Convert.ToInt32(reader["IdMoneda"].ToString()),
                        //Moneda = reader["Moneda"].ToString(),
                        //TitularContratante = reader["TitularContratante"].ToString(),
                        //Observaciones = reader["Observaciones"].ToString(),
                        //IdProducto = Convert.ToInt32(reader["IdProducto"].ToString()),
                        //Producto = reader["Producto"].ToString(),
                        //IdSubProducto = Convert.ToInt32(reader["IdSubProducto"].ToString()),
                        //SubProducto = reader["SubProducto"].ToString(),
                        //SumaBasica = Convert.ToDouble(reader["SumaBasica"].ToString()),
                        //HombreClave = Convert.ToInt32(reader["HombreClave"].ToString().Trim()),

                        //IdInstitucion = Convert.ToInt32(reader["IdInstitucion"].ToString()),

                        //Institucion = reader["Institucion"].ToString(),


                        //Convert.ToDouble(reader["SumaBasica"].ToString()),
                        //Convert.ToInt32(reader["HombreClave"].ToString()),

                    };
                    resultado.Add(item);
                }
                
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
        
        public List<prop.TramiteProcesado> PromotoriaAcepta(int IdTramite, bool StatusPoliza, int IdUsuario, string ObservacionPublica, string ObservacionPrivada)
        {
            b.ExecuteCommandSP("spWFOPromoRevisa");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@PolizaAceptada", StatusPoliza, SqlDbType.Bit);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@Observacion", ObservacionPublica, SqlDbType.NChar);
            
            List<prop.TramiteProcesado> resultado = new List<prop.TramiteProcesado>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramiteProcesado item = new prop.TramiteProcesado()
                {
                    IdTramite = Convert.ToInt32(reader["IdTramite"].ToString()),
                    Accion = reader["Accion"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramiteProcesado> ReingresarTramite(int IdTramite, int IdUsuario, string ObservacionPublica, string ObservacionPrivada)
        {
            b.ExecuteCommandSP("spWFOTramiteReingresar");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@ObservacionPub", ObservacionPublica, SqlDbType.NChar);
            b.AddParameter("@ObservacionPriv", ObservacionPrivada, SqlDbType.NChar);

            List<prop.TramiteProcesado> resultado = new List<prop.TramiteProcesado>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramiteProcesado item = new prop.TramiteProcesado()
                {
                    IdTramite = Convert.ToInt32(reader["IdTramite"].ToString()),
                    Accion = reader["Accion"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
        
        public List<prop.TramiteProcesado> ProcesarTramiteTarea(int IdTramite, int IdMesa, int IdUsuario, int IdStatusMesa, string ObsPublica, string ObsPrivada, string MotivosRechazo)
        {
            //int intStatusMesa = (int)IdStatusMesa;

            b.ExecuteCommandSP("spWFOTramiteProcesarTarea");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@IdMesa", IdMesa, SqlDbType.Int);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@IdStatusMesa", IdStatusMesa, SqlDbType.Int);
            b.AddParameter("@ObservacionPub", ObsPublica, SqlDbType.VarChar);
            b.AddParameter("@ObservacionPriv", ObsPrivada, SqlDbType.VarChar);
            b.AddParameter("@MotivosRechazo", MotivosRechazo, SqlDbType.VarChar);

            List<prop.TramiteProcesado> resultado = new List<prop.TramiteProcesado>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramiteProcesado item = new prop.TramiteProcesado()
                {
                    IdTramite = Convert.ToInt32(reader["IdTramite"].ToString()),
                    Accion = reader["Accion"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramiteProcesado> ProcesarTramitePausarAtrapadoAsignado(int IdTramite, int IdMesa, int IdUsuario, Funciones.VariablesGlobales.StatusMesa IdStatusMesa, string ObsPublica, string ObsPrivada, string MotivosRechazo)
        {
            int intStatusMesa = (int)IdStatusMesa;
            b.ExecuteCommandSP("spWFOTramitePausarAtrapadoAsignado");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@IdMesa", IdMesa, SqlDbType.Int);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@IdStatusMesa", intStatusMesa, SqlDbType.Int);
            b.AddParameter("@ObservacionPub", ObsPublica, SqlDbType.VarChar);
            b.AddParameter("@ObservacionPriv", ObsPrivada, SqlDbType.VarChar);
            b.AddParameter("@MotivosRechazo", MotivosRechazo, SqlDbType.VarChar);

            List<prop.TramiteProcesado> resultado = new List<prop.TramiteProcesado>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramiteProcesado item = new prop.TramiteProcesado()
                {
                    IdTramite = Convert.ToInt32(reader["IdTramite"].ToString()),
                    Accion = reader["Accion"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }


        public List<prop.TramiteProcesado> ProcesarTramitePausarAtrapado(int IdMesa, int IdUsuario, Funciones.VariablesGlobales.StatusMesa IdStatusMesa, string ObsPublica, string ObsPrivada, string MotivosRechazo)
        {
            int intStatusMesa = (int)IdStatusMesa;
            b.ExecuteCommandSP("spWFOTramitePausarAtrapado");
            b.AddParameter("@IdMesa", IdMesa, SqlDbType.Int);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@IdStatusMesa", intStatusMesa, SqlDbType.Int);
            b.AddParameter("@ObservacionPub", ObsPublica, SqlDbType.VarChar);
            b.AddParameter("@ObservacionPriv", ObsPrivada, SqlDbType.VarChar);
            b.AddParameter("@MotivosRechazo", MotivosRechazo, SqlDbType.VarChar);

            List<prop.TramiteProcesado> resultado = new List<prop.TramiteProcesado>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramiteProcesado item = new prop.TramiteProcesado()
                {
                    IdTramite = Convert.ToInt32(reader["IdTramite"].ToString()),
                    Accion = reader["Accion"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramiteProcesado> ProcesarTramite(int IdTramite, int IdMesa, int IdUsuario, Funciones.VariablesGlobales.StatusMesa IdStatusMesa, string ObsPublica, string ObsPrivada, string MotivosRechazo)
        {
            int intStatusMesa = (int)IdStatusMesa;

            b.ExecuteCommandSP("spWFOTramiteProcesar");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@IdMesa", IdMesa, SqlDbType.Int);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@IdStatusMesa", intStatusMesa, SqlDbType.Int);
            b.AddParameter("@ObservacionPub", ObsPublica, SqlDbType.VarChar);
            b.AddParameter("@ObservacionPriv", ObsPrivada, SqlDbType.VarChar);
            b.AddParameter("@MotivosRechazo", MotivosRechazo, SqlDbType.VarChar);
            
            List<prop.TramiteProcesado> resultado = new List<prop.TramiteProcesado>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramiteProcesado item = new prop.TramiteProcesado()
                {
                    IdTramite = Convert.ToInt32(reader["IdTramite"].ToString()),
                    Accion = reader["Accion"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramiteProcesado> ProcesarTramiteCallCenter(int IdTramite, int IdProceso, int IdUsuario, Funciones.VariablesGlobales.StatusMesa IdStatusMesa, string ObsPublica, string ObsPrivada, string MotivosRechazo, string LlamadaFolio, string LlamadaIntento)
        {
            int intStatusMesa = (int)IdStatusMesa;

#if DEBUG
            System.Diagnostics.Debug.WriteLine("exec spWFOTramiteProcesarCallCenter");
            System.Diagnostics.Debug.WriteLine("@IdTramite = " + IdTramite.ToString() + ",");
            System.Diagnostics.Debug.WriteLine("@IdProceso = " + IdProceso.ToString() + ",");
            System.Diagnostics.Debug.WriteLine("@IdUsuario = " + IdUsuario.ToString() + ",");
            System.Diagnostics.Debug.WriteLine("@IdStatusProceso = " + intStatusMesa.ToString() + ",");
            System.Diagnostics.Debug.WriteLine("@ObservacionPub = '" + ObsPublica + "',");
            System.Diagnostics.Debug.WriteLine("@ObservacionPriv = '" + ObsPrivada + "',");
            System.Diagnostics.Debug.WriteLine("@MotivosRechazo = '" + MotivosRechazo + "',");
            System.Diagnostics.Debug.WriteLine("@LlamadaFolio = '" + LlamadaFolio + "',");
            System.Diagnostics.Debug.WriteLine("@LlamadaIntento = '" + LlamadaIntento + "'");
#endif

            b.ExecuteCommandSP("spWFOTramiteProcesarCallCenter");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@IdProceso", IdProceso, SqlDbType.Int);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@IdStatusProceso", intStatusMesa, SqlDbType.Int);
            b.AddParameter("@ObservacionPub", ObsPublica, SqlDbType.VarChar);
            b.AddParameter("@ObservacionPriv", ObsPrivada, SqlDbType.VarChar);
            b.AddParameter("@MotivosRechazo", MotivosRechazo, SqlDbType.VarChar);
            b.AddParameter("@LlamadaFolio", LlamadaFolio, SqlDbType.VarChar);
            b.AddParameter("@LlamadaIntento", LlamadaIntento, SqlDbType.VarChar);

            List<prop.TramiteProcesado> resultado = new List<prop.TramiteProcesado>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramiteProcesado item = new prop.TramiteProcesado()
                {
                    IdTramite = Convert.ToInt32(reader["IdTramite"].ToString()),
                    Accion = reader["Accion"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramiteProcesado> EnviarTramite(int IdTramite, int IdMesa, int IdMesaToSend, int IdUsuario, string observacionesPublicas, string observacionesPrivadas)
        {
            b.ExecuteCommandSP("spWFOTramiteEnviar");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@IdMesaEnvia", IdMesa, SqlDbType.Int);
            b.AddParameter("@IdMesaRecibe", IdMesaToSend, SqlDbType.Int);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@ObservacionPublica", observacionesPublicas, SqlDbType.VarChar);
            b.AddParameter("@ObservacionPrivada", observacionesPrivadas, SqlDbType.VarChar);
            
            List<prop.TramiteProcesado> resultado = new List<prop.TramiteProcesado>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramiteProcesado item = new prop.TramiteProcesado()
                {
                    IdTramite = Convert.ToInt32(reader["IdTramite"].ToString()),
                    Accion = reader["Accion"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.RespuestaTramite> ActualizarTramite(int IdUsuario, int IdTramite, promotoria.TramiteN1 tramite)
        {
            b.ExecuteCommandSP("spWFOTramiteActualizar");
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@IdMoneda", tramite.IdMoneda, SqlDbType.Int);
            b.AddParameter("@IdTipoTramite", tramite.IdTipoTramite, SqlDbType.Int);
            b.AddParameter("@IdRiesgo", tramite.IdRiesgo, SqlDbType.Int);
            b.AddParameter("@HombreClave", tramite.HombreClave, SqlDbType.Int);
            b.AddParameter("@PrimaCotizacion", tramite.PrimaCotizacion, SqlDbType.Float);
            b.AddParameter("@SumaBasica", tramite.SumaBasica, SqlDbType.Float);
            b.AddParameter("@NumeroOrden", tramite.NumeroOrden, SqlDbType.NVarChar);
            b.AddParameter("@FechaSolicitud", string.Format("{0:yyyy/MM/dd}", DateTime.Parse(tramite.FechaSolicitud)), SqlDbType.DateTime);
            b.AddParameter("@TipoPersona", tramite.TipoPersona, SqlDbType.Int);
            b.AddParameter("@Nombre", tramite.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@ApPaterno", tramite.ApPaterno, SqlDbType.NVarChar);
            b.AddParameter("@ApMaterno", tramite.ApMaterno, SqlDbType.NVarChar);
            b.AddParameter("@Sexo", tramite.Sexo, SqlDbType.NVarChar);
            b.AddParameter("@FechaNacimiento", string.Format("{0:yyyy/MM/dd}", DateTime.Parse(tramite.FechaNacimiento)), SqlDbType.DateTime);
            b.AddParameter("@RFC", tramite.RFC, SqlDbType.NVarChar);
            b.AddParameter("@IdNacionalidad", tramite.IdNacionalidad, SqlDbType.Int);
            b.AddParameter("@FechaConst", string.Format("{0:yyyy/MM/dd}", DateTime.Parse(tramite.FechaConst)), SqlDbType.DateTime);
            b.AddParameter("@TitularContratante", tramite.TitularContratante, SqlDbType.Int);
            b.AddParameter("@TitularNombre", tramite.TitularNombre, SqlDbType.NVarChar);
            b.AddParameter("@TitularApPat", tramite.TitularApPat, SqlDbType.NVarChar);
            b.AddParameter("@TitularApMat", tramite.TitularApMat, SqlDbType.NVarChar);
            b.AddParameter("@IdTitularNacionalidad", tramite.IdTitularNacionalidad, SqlDbType.Int);
            b.AddParameter("@TitularSexo", tramite.TitularSexo, SqlDbType.NVarChar);
            b.AddParameter("@TitularFechaNacimiento", string.Format("{0:yyyy/MM/dd}", DateTime.Parse(tramite.TitularFechaNacimiento)), SqlDbType.DateTime);
            b.AddParameter("@IdInstitucion", tramite.IdInstitucion, SqlDbType.Int);

            List<prop.RespuestaTramite> resultado = new List<prop.RespuestaTramite>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.RespuestaTramite item = new prop.RespuestaTramite()
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Folio = reader["Folio"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramiteProcesado> ProcesarTramiteSeleccionCompleta(int IdTramite, int IdUsuario, int Chec1, int Chec2)
        {
            b.ExecuteCommandSP("spWFOSeleccionCompleta");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@Chec1", Chec1, SqlDbType.Int);
            b.AddParameter("@Chec2", Chec2, SqlDbType.Int);

            List<prop.TramiteProcesado> resultado = new List<prop.TramiteProcesado>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramiteProcesado item = new prop.TramiteProcesado()
                {
                    IdTramite = Convert.ToInt32(reader["IdTramite"].ToString()),
                    Accion = reader["Accion"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public prop.TipoTramite ObtenerTipoTramite(int pIdTramite)
        {
            b.ExecuteCommandSP("Tramite_Consulta_TipoTramite");
            b.AddParameter("@IdTramite", pIdTramite, SqlDbType.Int);

            prop.TipoTramite resultado = new prop.TipoTramite();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = Convert.ToInt32(reader["Id"].ToString());
                resultado.Nombre = reader["Nombre"].ToString();
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public promotoria.cat_riesgos ObtenerRiesgoTramite(int pIdTramite)
        {
            b.ExecuteCommandSP("Tramite_Consulta_IdRiesgo");
            b.AddParameter("@IdTramite", pIdTramite, SqlDbType.Int);

            promotoria.cat_riesgos resultado = new promotoria.cat_riesgos();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = Convert.ToInt32(reader["IdRiesgo"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}