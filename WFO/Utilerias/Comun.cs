using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;


namespace WFO.Utilerias
{
    /// <summary>
    /// Clase para integrar todos los procesos y facilitar la accesibilidad
    /// </summary>
    public class Comun : System.Web.UI.Page
    {
        //Tablas
       
        public WFO.Negocio.Sistema.Configuracion sisConfig;
        public WFO.Negocio.Sistema.Menu sisMenu;
        public WFO.Negocio.Sistema.PermisosMenu sisPM;
        public WFO.Negocio.Sistema.Roles sisRols;
        public WFO.Negocio.Sistema.RolAcceso sisRA;
        public WFO.Negocio.Sistema.Usuarios sisUsrs;
        public WFO.Negocio.Sistema.Autentificar sisAutenti;

        //Utilerias/Servicios
        public WFO.IU.Comun comun;
        public WFO.IU.ManejadorSesion manejo_sesion;
        public Mensajes mensajes;
        public WFO.RegistraLog.RegistraLog log;

        //Tablas de procesos
        public WFO.Negocio.Cobranza.Default def;
        public WFO.Negocio.Cobranza.Cobranza cob;
        public WFO.Negocio.Cobranza.ListaCobranza lco;
        public WFO.Negocio.Cobranza.ReporteColectividad rco;
        public WFO.Negocio.Cobranza.ReporteConsolidado rcl;
        public WFO.Negocio.Procesos.Operacion.Mesas mesas;
        public WFO.Negocio.Procesos.Supervision.AlmacenamientoTramites atr;
        public WFO.Negocio.Procesos.Supervision.Default sup;
        public WFO.Negocio.Procesos.Supervision.MapaSupervisor msup;
        public WFO.Negocio.Procesos.Supervision.ReporteGeneralTotales rgt;
        public WFO.Negocio.Procesos.Supervision.ReporteGeneralMesa rgm;
        public WFO.Negocio.Procesos.Supervision.ReporteGeneralTop10 rgt10;
        public WFO.Negocio.Procesos.Supervision.DetallePromotoria dp;
        public WFO.Negocio.Procesos.Supervision.ReportePorcientoSuspension rps;
        public WFO.Negocio.Procesos.Supervision.ReporteEstatusTramite ret;
        public WFO.Negocio.Procesos.Supervision.ReporteProductividad rep;
        public WFO.Negocio.Procesos.Supervision.ReporteProductividadPromotoria rpp;
        public WFO.Negocio.Procesos.Supervision.ReporteCaducados rec;
        public WFO.Negocio.Procesos.Supervision.ReporteSelProcesado rpr;
        public WFO.Negocio.Procesos.Supervision.ReporteTramitesAnuales rta;
        public WFO.Negocio.Procesos.Supervision.ReporteTramitesReingresosV2 rprV2;
        public WFO.Negocio.Procesos.Supervision.TotalTramiteEstatus tts;
        public WFO.Negocio.Procesos.Supervision.TramitesFechaMov tfm;
        public WFO.Negocio.Procesos.Supervision.BuscarTramites bt;
        public WFO.Negocio.Procesos.Supervision.ReporteGeneralFranja rgf;
        public WFO.Negocio.Procesos.Supervision.DetalleHoras dh;
        public WFO.Negocio.Procesos.Supervision.TiemposAtencion ta;
        public WFO.Negocio.Procesos.Supervision.RelojChecador rch;
        public WFO.Negocio.Procesos.Supervision.StatusTramite statustramite;
        public WFO.Negocio.Procesos.Supervision.TramitesPorMesa tramitespormesa;

        public WFO.Negocio.Procesos.Supervision.TAT rtat;
        public WFO.Negocio.Procesos.Supervision.Sabana rs;

        public WFO.Negocio.Procesos.Promotoria.Catalogos catalogos;

        //Supervisión General
        public WFO.Negocio.Procesos.SupervisionGeneral.Usuarios supervisiongeneralusuarios;
        public WFO.Negocio.Procesos.SupervisionGeneral.Tramite supervisiongeneraltramites;
        public WFO.Negocio.Procesos.SupervisionGeneral.TramiteMesa supervisiongeneraltramitemesa;
        public WFO.Negocio.Procesos.SupervisionGeneral.cat_promotoria supervisiongeneralcatpromotoria;
        public WFO.Negocio.Procesos.SupervisionGeneral.Asignar supervisiongeneralasignar;
        public WFO.Negocio.Procesos.SupervisionGeneral.Productividad productividad;

        public Comun()
        {
            sisConfig = new WFO.Negocio.Sistema.Configuracion();
            sisMenu = new WFO.Negocio.Sistema.Menu();
            sisPM = new WFO.Negocio.Sistema.PermisosMenu();
            sisRols = new WFO.Negocio.Sistema.Roles();
            sisRA = new WFO.Negocio.Sistema.RolAcceso();
            sisUsrs = new WFO.Negocio.Sistema.Usuarios();
            sisAutenti = new WFO.Negocio.Sistema.Autentificar();

            comun = new WFO.IU.Comun();
            manejo_sesion = new WFO.IU.ManejadorSesion();
            mensajes = new Mensajes();
            log = new WFO.RegistraLog.RegistraLog("Log", HttpContext.Current.Server.MapPath("~"), "WFO Error");

            manejo_sesion.EsperaBloqueo = WebConfigurationManager.AppSettings["EsperaLoginBloqueado"];

            //Procesos
            def = new WFO.Negocio.Cobranza.Default();
            cob = new WFO.Negocio.Cobranza.Cobranza();
            lco = new WFO.Negocio.Cobranza.ListaCobranza();
            rco = new WFO.Negocio.Cobranza.ReporteColectividad();
            rcl = new WFO.Negocio.Cobranza.ReporteConsolidado();
            mesas = new Negocio.Procesos.Operacion.Mesas();
            atr = new Negocio.Procesos.Supervision.AlmacenamientoTramites();
            sup = new WFO.Negocio.Procesos.Supervision.Default();
            msup = new Negocio.Procesos.Supervision.MapaSupervisor();
            rgt = new WFO.Negocio.Procesos.Supervision.ReporteGeneralTotales();
            rgm = new WFO.Negocio.Procesos.Supervision.ReporteGeneralMesa();
            rgt10 = new WFO.Negocio.Procesos.Supervision.ReporteGeneralTop10();
            dp = new WFO.Negocio.Procesos.Supervision.DetallePromotoria();
            rps = new WFO.Negocio.Procesos.Supervision.ReportePorcientoSuspension();
            ret = new WFO.Negocio.Procesos.Supervision.ReporteEstatusTramite();
            rep = new WFO.Negocio.Procesos.Supervision.ReporteProductividad();
            rpp = new Negocio.Procesos.Supervision.ReporteProductividadPromotoria();
            statustramite = new WFO.Negocio.Procesos.Supervision.StatusTramite();
            tramitespormesa = new WFO.Negocio.Procesos.Supervision.TramitesPorMesa();
            rec = new WFO.Negocio.Procesos.Supervision.ReporteCaducados();
            rpr = new WFO.Negocio.Procesos.Supervision.ReporteSelProcesado();
            rta = new Negocio.Procesos.Supervision.ReporteTramitesAnuales();
            rprV2 = new Negocio.Procesos.Supervision.ReporteTramitesReingresosV2();
           tts = new WFO.Negocio.Procesos.Supervision.TotalTramiteEstatus();
            bt = new WFO.Negocio.Procesos.Supervision.BuscarTramites();
            tfm = new WFO.Negocio.Procesos.Supervision.TramitesFechaMov();
            rgf = new WFO.Negocio.Procesos.Supervision.ReporteGeneralFranja();
            dh = new WFO.Negocio.Procesos.Supervision.DetalleHoras();
            ta = new WFO.Negocio.Procesos.Supervision.TiemposAtencion();
            rch = new WFO.Negocio.Procesos.Supervision.RelojChecador();
            rtat = new WFO.Negocio.Procesos.Supervision.TAT();
            rs = new WFO.Negocio.Procesos.Supervision.Sabana();
            catalogos = new WFO.Negocio.Procesos.Promotoria.Catalogos();

            supervisiongeneralusuarios = new WFO.Negocio.Procesos.SupervisionGeneral.Usuarios();
            supervisiongeneraltramites = new WFO.Negocio.Procesos.SupervisionGeneral.Tramite();
            supervisiongeneraltramitemesa = new WFO.Negocio.Procesos.SupervisionGeneral.TramiteMesa();
            supervisiongeneralcatpromotoria = new WFO.Negocio.Procesos.SupervisionGeneral.cat_promotoria();
            supervisiongeneralasignar = new WFO.Negocio.Procesos.SupervisionGeneral.Asignar();

            productividad = new WFO.Negocio.Procesos.SupervisionGeneral.Productividad();
        }



    }
}