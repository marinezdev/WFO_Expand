using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.AccesoDatos.Procesos.Promotoria
{
    public class TramitesPromotoria
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.TramitesPromotoria> ConsultaTramitesPromotoria(int IdUsuario,int IdTramite)
        {
            b.ExecuteCommandSP("Tramite_Promotoria_PorUsuario");
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            List<prop.TramitesPromotoria> resultado = new List<prop.TramitesPromotoria>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramitesPromotoria item = new prop.TramitesPromotoria()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"].ToString()),
                    FolioCompuesto = reader["FolioCompuesto"].ToString(),
                    NumeroOrden = reader["NumeroOrden"].ToString(),
                    Operacion = reader["Operacion"].ToString(),
                    //Producto = reader["Producto"].ToString(),
                    Contratante = reader["Contratante"].ToString(),
                    //RFC = reader["RFC"].ToString(),
                    FechaSolicitud = Convert.ToDateTime(reader["FechaSolicitud"].ToString()),
                    Estatus = reader["Estatus"].ToString(),
                    IdSisLegados = reader["IdSisLegados"].ToString(),
                    //kwik = reader["kwik"].ToString(),
                    IdTipoTramite = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdTipoTramite"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramitesCallCenter> ListaTramitesCallCenter(int Id)
        {
            b.ExecuteCommandSP("Tramites_CallCenter_Seleccionar_Por_IdUsuario");
            b.AddParameter("@IdUsuario", Id, SqlDbType.Int);
            List<prop.TramitesCallCenter> resultado = new List<prop.TramitesCallCenter>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramitesCallCenter item = new prop.TramitesCallCenter()
                {
                    IdTramite = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdTramite"].ToString()),
                    FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"].ToString()),
                    folioTramite = reader["folioTramite"].ToString(),
                    NumeroOrden = reader["NumeroOrden"].ToString(),
                    Contratente = reader["Contratente"].ToString(),
                    FechaSolicitud = Convert.ToDateTime(reader["FechaSolicitud"].ToString()),
                    Promotoria = reader["Promotoria"].ToString(),
                    TipoServicio = reader["TipoServicio"].ToString(),
                    TelCelular = reader["TelCelular"].ToString(),
                    TelParticular = reader["TelParticular"].ToString(),
                    StatusTramite = reader["StatusTramite"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramitesPromotoriaPendientes> ListaTramitesPromotoriaPendientesReporte(int Id)
        {
            b.ExecuteCommandSP("TramitesPendientes_Promotoria_Seleccionar_Por_IdUsuario");
            b.AddParameter("@IdUsuario", Id, SqlDbType.Int);
            List<prop.TramitesPromotoriaPendientes> resultado = new List<prop.TramitesPromotoriaPendientes>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramitesPromotoriaPendientes item = new prop.TramitesPromotoriaPendientes()
                {
                    IdTramite = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdTramite"].ToString()),
                    FechaRegistro = Convert.ToDateTime(reader["Fecha de envio"].ToString()),
                    Folio = reader["Folio del Tramite"].ToString(),
                    TipoFlujo = reader["Tipo Flujo"].ToString(),
                    TipoServicio = reader["Tipo Servicio"].ToString(),
                    Contratante = reader["Contratante"].ToString(),
                    FechaFirmaSolicitud = Convert.ToDateTime(reader["Fecha Firma Solicitud"].ToString()),
                    StatusTramite = reader["Status Tramite"].ToString(),
                    NumeroOrden = reader["Numero de Orden"].ToString(),
                    NumeroPoliza = reader["Numero de Poliza"].ToString(),
                    PromotoriaClave = reader["Clave Promotoria"].ToString(),
                    GA = reader["GA"].ToString(),
                    GU = reader["GU"].ToString(),
                    PromotoriaZonaClave = reader["Clave Zona"].ToString(),
                    PrimaTotal = reader["PrimaTotal"].ToString(),
                    PrimaExcedente = reader["PrimaExcedente"].ToString(),
                    PrimaDiferencia = reader["PrimaDiferencia"].ToString(),
                    SuspensionMotivos = reader["Motivo Suspension / Rechazo"].ToString(),
                    ObservacionesPrivadas = reader["Observaciones Privadas"].ToString(),
                    ObservacionesPublicas = reader["Observaciones Publicas"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramitesPromotoria> ListaTramitesPromotoria(int Id)
        {
            b.ExecuteCommandSP("Tramites_Promotoria_Seleccionar_Por_IdUsuario");
            b.AddParameter("@IdUsuario", Id, SqlDbType.Int);
            List<prop.TramitesPromotoria> resultado = new List<prop.TramitesPromotoria>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramitesPromotoria item = new prop.TramitesPromotoria()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
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
                    PromotoriaClave = reader["PromotoriaClave"].ToString(),
                    PromotoriaZona = reader["Clave"].ToString(),
                    TipoServicio = reader["TipoServicio"].ToString(),
                    PrimaExcedente = reader["PrimaExcedente"].ToString(),
                    PrimaTotal = reader["PrimaTotal"].ToString(),
                    PrimaDiferencia = reader["PrimaDiferencia"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramitesPromotoria> ListaTramitesPromotoriaFechas(int Id, int IdStatusTramite, DateTime Fecha_Termino, DateTime Fecha_Inicio)
        {
            b.ExecuteCommandSP("Tramites_Promotoria_Seleccionar_Fechas_Por_IdUsuario");
            b.AddParameter("@IdUsuario", Id, SqlDbType.Int);
            b.AddParameter("@IdStatusTramite", IdStatusTramite, SqlDbType.Int);
            b.AddParameter("@Fecha_Inicio", Fecha_Inicio, SqlDbType.DateTime);
            b.AddParameter("@Fecha_Termino", Fecha_Termino, SqlDbType.DateTime);
            List<prop.TramitesPromotoria> resultado = new List<prop.TramitesPromotoria>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramitesPromotoria item = new prop.TramitesPromotoria()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
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
                    kwik = reader["kwik"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramitesPromotoria> ListaTramitesPromotoriaEstado(int Id, string Estado, int Mes)
        {
            b.ExecuteCommandSP("Indicador_General_Promotoria_PorUsuarioEstado");
            b.AddParameter("@IdUsuario", Id, SqlDbType.Int);
            b.AddParameter("@Estado", Estado, SqlDbType.NVarChar);
            b.AddParameter("@Mes", Mes, SqlDbType.Int);
            List<prop.TramitesPromotoria> resultado = new List<prop.TramitesPromotoria>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramitesPromotoria item = new prop.TramitesPromotoria()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
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
                    kwik = reader["kwik"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramitesPromotoria> ListaTramitesPromotoriaPendientes(int Id)
        {
            b.ExecuteCommandSP("Indicador_General_Promotoria_PorUsuarioPendientes");
            b.AddParameter("@IdUsuario", Id, SqlDbType.Int);
            List<prop.TramitesPromotoria> resultado = new List<prop.TramitesPromotoria>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramitesPromotoria item = new prop.TramitesPromotoria()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"].ToString()),
                    FolioCompuesto = reader["FolioCompuesto"].ToString(),
                    NumeroOrden = reader["NumeroOrden"].ToString(),
                    Operacion = reader["Operacion"].ToString(),
                    Contratante = reader["Contratante"].ToString(),
                    FechaSolicitud = Convert.ToDateTime(reader["FechaSolicitud"].ToString()),
                    Estatus = reader["Estatus"].ToString(),
                    PrimaTotal = reader["PrimaTotal"].ToString(),
                    PrimaExcedente = reader["PrimaExcedente"].ToString(),
                    PrimaDiferencia = reader["PrimaDiferencia"].ToString(),
                    IdSisLegados = reader["IdSisLegados"].ToString(),
                    kwik = reader["kwik"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}
