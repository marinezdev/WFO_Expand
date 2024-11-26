using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;
using promotoria = WFO.Propiedades.Procesos.Promotoria;
namespace WFO.Negocio.Procesos.Operacion
{
    public class TramiteProcesar
    {
        AccesoDatos.Procesos.Operacion.TramiteProcesar Tramites = new AccesoDatos.Procesos.Operacion.TramiteProcesar();
        AccesoDatos.Procesos.Operacion.PolizaSistemasLegados sistemasLegados = new AccesoDatos.Procesos.Operacion.PolizaSistemasLegados();
        AccesoDatos.Procesos.Operacion.kwik kwik = new AccesoDatos.Procesos.Operacion.kwik();
        AccesoDatos.Procesos.Operacion.SeleccionCompleta seleccionCompleta = new AccesoDatos.Procesos.Operacion.SeleccionCompleta();

        public List<prop.TramiteProcesar> ObtenerTarea(int pIdUsuario, int pIdMesa, int pIdTramite)
        {
            return Tramites.ObtenerTarea(pIdUsuario, pIdMesa, pIdTramite);
        }

        public List<prop.TramiteProcesar> ObtenerTramite(int pIdUsuario, int pIdMesa, int pIdTramite)
        {
            return Tramites.ObtenerTramite(pIdUsuario, pIdMesa, pIdTramite);
        }

        public List<prop.TramiteProcesar> ObtenerTramite(int pIdTramite)
        {
            return Tramites.ObtenerTramite(pIdTramite);
        }

        public List<prop.TramiteProcesado> PromotoriaAcepta(int IdTramite, bool StatusPoliza, int IdUsuario, string ObservacionPublica, string ObservacionPrivada)
        {
            return Tramites.PromotoriaAcepta(IdTramite, StatusPoliza, IdUsuario, ObservacionPublica.Replace("\n", " // "), ObservacionPrivada.Replace("\n", " // "));
        }

        public List<prop.TramiteProcesado> ReingresarTramite(int IdTramite, int IdUsuario, string ObservacionPublica, string ObservacionPrivada)
        {
            return Tramites.ReingresarTramite(IdTramite, IdUsuario, ObservacionPublica.Replace("\n", " // "), ObservacionPrivada.Replace("\n", " // "));
        }

        public List<prop.TramiteProcesado> ProcesarTramiteTarea(int IdTramite, int IdMesa, int IdUsuario, int IdStatusMesa, string ObsPublica, string ObsPrivada, string MotivosRechazo)
        {
            return Tramites.ProcesarTramiteTarea(IdTramite, IdMesa, IdUsuario, IdStatusMesa, ObsPublica.Replace("\n", " // "), ObsPrivada.Replace("\n", " // "), MotivosRechazo);
        }

        public List<prop.TramiteProcesado> ProcesarTramitePausarAtrapado(int IdMesa, int IdUsuario, Funciones.VariablesGlobales.StatusMesa IdStatusMesa, string ObsPublica, string ObsPrivada, string MotivosRechazo)
        {
            return Tramites.ProcesarTramitePausarAtrapado(IdMesa, IdUsuario, IdStatusMesa, ObsPublica.Replace("\n", " // "), ObsPrivada.Replace("\n", " // "), MotivosRechazo);
        }
        public List<prop.TramiteProcesado> ProcesarTramitePausarAtrapadoAsignado(int IdTramite, int IdMesa, int IdUsuario, Funciones.VariablesGlobales.StatusMesa IdStatusMesa, string ObsPublica, string ObsPrivada, string MotivosRechazo)
        {
            return Tramites.ProcesarTramitePausarAtrapadoAsignado(IdTramite, IdMesa, IdUsuario, IdStatusMesa, ObsPublica.Replace("\n", " // "), ObsPrivada.Replace("\n", " // "), MotivosRechazo);
        }

        public List<prop.TramiteProcesado> ProcesarTramite(int IdTramite, int IdMesa, int IdUsuario, Funciones.VariablesGlobales.StatusMesa IdStatusMesa, string ObsPublica, string ObsPrivada, string MotivosRechazo)
        {
            return Tramites.ProcesarTramite(IdTramite, IdMesa, IdUsuario, IdStatusMesa, ObsPublica.Replace("\n", " // "), ObsPrivada.Replace("\n", " // "), MotivosRechazo);
        }

        public List<prop.TramiteProcesado> ProcesarTramiteCallCenter(int IdTramite, int IdProceso, int IdUsuario, Funciones.VariablesGlobales.StatusMesa IdStatusMesa, string ObsPublica, string ObsPrivada, string MotivosRechazo, string LlamadaFolio, string LlamadaIntento)
        {
            return Tramites.ProcesarTramiteCallCenter(IdTramite, IdProceso, IdUsuario, IdStatusMesa, ObsPublica.Replace("\n", " // "), ObsPrivada.Replace("\n", " // "), MotivosRechazo, LlamadaFolio, LlamadaIntento);
        }

        public List<prop.TramiteProcesado> EnviarTramite(int IdTramite, int IdMesa, int IdMesaToSend, int IdUsuario, string observacionesPublicas, string observacionesPrivadas)
        {
            return Tramites.EnviarTramite(IdTramite, IdMesa, IdMesaToSend, IdUsuario, observacionesPublicas.Replace("\n", " // "), observacionesPrivadas.Replace("\n", " // "));
        }

        public List<prop.RespuestaTramite> ActualizarTramite(int IdUsuario, int IdTramite, promotoria.TramiteN1 tramite)
        {
            return Tramites.ActualizarTramite(IdUsuario,IdTramite,tramite);
        }

        public List<prop.TramiteProcesado> ProcesarTramiteSeleccionCompleta(int IdTramite, int IdUsuario, int Chec1, int Chec2)
        {
            return Tramites.ProcesarTramiteSeleccionCompleta(IdTramite, IdUsuario, Chec1, Chec2);
        }

        public int ActualizaPolizaSistemasLegados(int IdTramite, int IdUsuario, string IdSisLegados)
        {
            return sistemasLegados.ActualizaPolizaSistemasLegados(IdTramite, IdUsuario, IdSisLegados);
        }

        public int ActualizaKwik(int IdTramite, int IdUsuario, string IdSisLegados)
        {
            return kwik.ActualizaKwik(IdTramite, IdUsuario, IdSisLegados);
        }

        public int ActualizaSeleccionCompleta(int IdTramite, int IdUsuario, int SelecionCompleta)
        {
            return seleccionCompleta.ActualizaSeleccionCompleta(IdTramite, IdUsuario, SelecionCompleta);
        }

        public bool ConsultaSeleccionCompleta(int IdTramite)
        {
            return seleccionCompleta.ConsultaSeleccionCompleta(IdTramite);
        }
        
        public prop.TipoTramite ObtenerTipoTramite(int IdTramite)
        {
            return Tramites.ObtenerTipoTramite(IdTramite);
        }

        public promotoria.cat_riesgos ObtenerRiesgoTramite(int IdTramite)
        {
            return Tramites.ObtenerRiesgoTramite(IdTramite);
        }
    }
}