using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.AccesoDatos.Procesos.Promotoria
{
    public class NuevoTramite
    {
        ManejoDatos b = new ManejoDatos();

        //public List<prop.RespuestaNuevoTramiteN1> NuevoTramiteN1(int IdTipoTramite, int IdPromotoria, int IdUsuario, int IdStatus, int idPrioridad, string FechaSolicitud, int IdAgente, string NumeroOrden, int idRamo, string IdSisLegados, string kwik, int IdMoneda, int TipoPersona, string Nombre, string ApPaterno, string ApMaterno, string Sexo, string FechaNacimiento, string RFC, string FechaConst, int IdNacionalidad, string TitularNombre, string TitularApPat, string TitularApMat, int IdTitularNacionalidad, string TitularSexo, string TitularFechaNacimiento, double PrimaCotizacion, int TitularContratante, string Observaciones, int IdProducto, int IdSubProducto)
        public List<prop.RespuestaNuevoTramiteN1> NuevoTramiteN1(prop.TramiteN1 tramiteN1)
        {
            tramiteN1.PrimaTotal = tramiteN1.PrimaTotal == null ? "0" : tramiteN1.PrimaTotal;
            tramiteN1.PrimaExcedente = tramiteN1.PrimaExcedente == null ? "0" : tramiteN1.PrimaExcedente;
            tramiteN1.PrimaDiferencia = tramiteN1.PrimaDiferencia == null ? "0" : tramiteN1.PrimaDiferencia;

#if DEBUG
            System.Diagnostics.Debug.WriteLine("exec spTramiteNuevo ");
            System.Diagnostics.Debug.WriteLine("@IdTipoTramite = " + tramiteN1.IdTipoTramite);
            System.Diagnostics.Debug.WriteLine(", @IdTipoServicio = " + tramiteN1.IdTipoServicio);
            System.Diagnostics.Debug.WriteLine(", @IdPromotoria = " + tramiteN1.IdPromotoria);
            System.Diagnostics.Debug.WriteLine(", @IdPromotoriaZona = " + tramiteN1.IdPromotoriaZona);
            System.Diagnostics.Debug.WriteLine(", @IdUsuario = " + tramiteN1.IdUsuario);
            System.Diagnostics.Debug.WriteLine(", @IdStatus = " + tramiteN1.IdStatus);
            System.Diagnostics.Debug.WriteLine(", @idPrioridad = " + tramiteN1.idPrioridad);
            System.Diagnostics.Debug.WriteLine(", @FechaSolicitud = " + string.Format("{0:yyyy/MM/dd}", DateTime.Parse(tramiteN1.FechaSolicitud)));
            System.Diagnostics.Debug.WriteLine(", @IdAgente = " + tramiteN1.IdAgente);
            System.Diagnostics.Debug.WriteLine(", @NumeroOrden = " + tramiteN1.NumeroOrden);
            System.Diagnostics.Debug.WriteLine(", @TipoPersona = " + tramiteN1.TipoPersona);
            System.Diagnostics.Debug.WriteLine(", @Nombre = " + tramiteN1.Nombre);
            System.Diagnostics.Debug.WriteLine(", @ApPaterno = " + tramiteN1.ApPaterno);
            System.Diagnostics.Debug.WriteLine(", @ApMaterno = " + tramiteN1.ApMaterno);
            System.Diagnostics.Debug.WriteLine(", @Observaciones = " + tramiteN1.Observaciones);
            System.Diagnostics.Debug.WriteLine(", @Telefono1 = " + tramiteN1.Telefono1);
            System.Diagnostics.Debug.WriteLine(", @Telefono2 = " + tramiteN1.Telefono2);
            System.Diagnostics.Debug.WriteLine(", @PrimaTotal = " + tramiteN1.PrimaTotal);
            System.Diagnostics.Debug.WriteLine(", @PrimaExcedente = " + tramiteN1.PrimaExcedente);
            System.Diagnostics.Debug.WriteLine(", @PrimaDiferencia = " + tramiteN1.PrimaDiferencia);
#endif


            b.ExecuteCommandSP("spTramiteNuevo");
            b.AddParameter("@IdTipoTramite", tramiteN1.IdTipoTramite, SqlDbType.Int);
            b.AddParameter("@IdTipoServicio", tramiteN1.IdTipoServicio, SqlDbType.Int);
            b.AddParameter("@IdPromotoria", tramiteN1.IdPromotoria, SqlDbType.Int);
            b.AddParameter("@IdPromotoriaZona", tramiteN1.IdPromotoriaZona, SqlDbType.Int);
            b.AddParameter("@IdUsuario", tramiteN1.IdUsuario, SqlDbType.Int);
            b.AddParameter("@IdStatus", tramiteN1.IdStatus, SqlDbType.Int);
            b.AddParameter("@idPrioridad", tramiteN1.idPrioridad, SqlDbType.Int);
            b.AddParameter("@FechaSolicitud", string.Format("{0:yyyy/MM/dd}", DateTime.Parse(tramiteN1.FechaSolicitud)), SqlDbType.Date);
            //b.AddParameter("@FechaSolicitud", FechaSolicitud, SqlDbType.Date);
            b.AddParameter("@IdAgente", tramiteN1.IdAgente, SqlDbType.Int);
            b.AddParameter("@NumeroOrden", tramiteN1.NumeroOrden, SqlDbType.NVarChar);
            //b.AddParameter("@idRamo", tramiteN1.idRamo, SqlDbType.Int);
            //b.AddParameter("@IdSisLegados", tramiteN1.IdSisLegados, SqlDbType.NVarChar);
            //b.AddParameter("@kwik", tramiteN1.kwik, SqlDbType.NVarChar);
            //b.AddParameter("@IdMoneda", tramiteN1.IdMoneda, SqlDbType.Int);
            b.AddParameter("@TipoPersona", tramiteN1.TipoPersona, SqlDbType.Int);
            b.AddParameter("@Nombre", tramiteN1.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@ApPaterno", tramiteN1.ApPaterno, SqlDbType.NVarChar);
            b.AddParameter("@ApMaterno", tramiteN1.ApMaterno, SqlDbType.NVarChar);
            //b.AddParameter("@Sexo", tramiteN1.Sexo, SqlDbType.NVarChar);
            //b.AddParameter("@FechaNacimiento", string.Format("{0:yyyy/MM/dd}", DateTime.Parse(tramiteN1.FechaNacimiento)), SqlDbType.Date);
            //b.AddParameter("@RFC", tramiteN1.RFC, SqlDbType.NVarChar);
            //b.AddParameter("@FechaConst",  string.Format("{0:yyyy/MM/dd}", DateTime.Parse(tramiteN1.FechaConst)), SqlDbType.Date);
            //b.AddParameter("@IdNacionalidad", tramiteN1.IdNacionalidad, SqlDbType.Int);
            //b.AddParameter("@TitularNombre", tramiteN1.TitularNombre, SqlDbType.NVarChar);
            //b.AddParameter("@TitularApPat", tramiteN1.TitularApPat, SqlDbType.NVarChar);
            //b.AddParameter("@TitularApMat", tramiteN1.TitularApMat, SqlDbType.NVarChar);
            //b.AddParameter("@IdTitularNacionalidad", tramiteN1.IdTitularNacionalidad, SqlDbType.Int);
            //b.AddParameter("@TitularSexo", tramiteN1.TitularSexo, SqlDbType.NVarChar);
            //b.AddParameter("@TitularFechaNacimiento", string.Format("{0:yyyy/MM/dd}", DateTime.Parse(tramiteN1.TitularFechaNacimiento)), SqlDbType.Date);
            //b.AddParameter("@PrimaCotizacion", tramiteN1.PrimaTotal, SqlDbType.Float);
            //b.AddParameter("@SumaBasica", tramiteN1.SumaBasica, SqlDbType.Float);
            //b.AddParameter("@TitularContratante", tramiteN1.TitularContratante, SqlDbType.Int);
            b.AddParameter("@Observaciones", tramiteN1.Observaciones, SqlDbType.NVarChar);
            b.AddParameter("@Telefono1", tramiteN1.Telefono1, SqlDbType.NVarChar);
            b.AddParameter("@Telefono2", tramiteN1.Telefono2, SqlDbType.NVarChar);
            //b.AddParameter("@IdProducto", tramiteN1.IdProducto, SqlDbType.Int);
            //b.AddParameter("@IdSubProducto", tramiteN1.IdSubProducto, SqlDbType.Int);
            //b.AddParameter("@IdRiesgo", tramiteN1.IdRiesgo, SqlDbType.Int);
            //b.AddParameter("@HombreClave", tramiteN1.HombreClave, SqlDbType.Int);
            //b.AddParameter("@IdInstitucion", tramiteN1.IdInstitucion, SqlDbType.Int);

            // NUEVOS DATOS

            //b.AddParameter("@CPDES", tramiteN1.CPDES, SqlDbType.NVarChar);
            //b.AddParameter("@FolioCPDES", tramiteN1.FolioCPDES, SqlDbType.NVarChar);
            //b.AddParameter("@EstatusCPDES", tramiteN1.EstatusCPDES, SqlDbType.NVarChar);
            //b.AddParameter("@SumaPolizas", tramiteN1.SumaPolizas, SqlDbType.Float);
            //b.AddParameter("@OneShot", tramiteN1.OneShot, SqlDbType.NVarChar);

            b.AddParameter("@PrimaTotal", tramiteN1.PrimaTotal.ToString(), SqlDbType.NVarChar);
            b.AddParameter("@PrimaExcedente", tramiteN1.PrimaExcedente.ToString(), SqlDbType.NVarChar);
            b.AddParameter("@PrimaDiferencia", tramiteN1.PrimaDiferencia.ToString(), SqlDbType.NVarChar);

            List<prop.RespuestaNuevoTramiteN1> resultado = new List<prop.RespuestaNuevoTramiteN1>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.RespuestaNuevoTramiteN1 item = new prop.RespuestaNuevoTramiteN1()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Folio = reader["Folio"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}
