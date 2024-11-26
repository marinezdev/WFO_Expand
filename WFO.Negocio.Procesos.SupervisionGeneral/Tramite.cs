using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using WFO.Funciones;
using prop = WFO.Propiedades.Procesos.SupervisionGeneral;
using DevExpress.Web;

namespace WFO.Negocio.Procesos.SupervisionGeneral
{
    public class Tramite : SupervisionGeneral
    {
        public void Seleccionar(ref GridView gridview, int IdUsuario)
        {
            LlenarControles.LlenarGridView(ref gridview, tramite.Seleccionar(IdUsuario));
        }

        public void SelecionarRepeater(ref Repeater repeater, int IdUsuario)
        {
            LlenarControles.LlenarRepeaterLista(ref repeater, tramite.Seleccionar(IdUsuario));
        }

        public prop.Tramite SeleccionarPorId(string id)
        {
            return tramite.SeleccionarPorId(id);
        }

        public void ModificarPrioridad(string id, string usuariocambio, string usuarioanterior, string idtramite, string idprioridadanterior)
        {
            tramite.ModificarPrioridad(id);
            tramitebitacora.Agregar(usuariocambio, usuarioanterior, idtramite, idprioridadanterior);
        }

        public List<prop.TramitesIncompletos> TramitesIncompletos()
        {
            return tramite.TramitesIncompletos();
        }

        public List<prop.TramiteMesa> tramiteMesas(int IdTramite)
        {
            return tramite.tramiteMesas(IdTramite);
        }

        public int EnviarTramiteMesaAdmisionNumeroPoliza(int IdTramite, string NumeroPoliza)
        {
            return tramite.EnviarTramiteMesaAdmisionNumeroPoliza(IdTramite, NumeroPoliza);
        }


        public void Buscar(ref GridView gridview, string foliocompuesto, string fecharegistrodel, string fecharegistroal, string fechasolicituddel, string fechasolicitudal, string idpromotoria, string estado, int IdUsuario)
        {
            LlenarControles.LlenarGridView(ref gridview, tramite.Buscar(foliocompuesto, fecharegistrodel, fecharegistroal, fechasolicituddel, fechasolicitudal, idpromotoria, estado, IdUsuario));

        }
        public void Buscar(ref Repeater gridview, string foliocompuesto, string fecharegistrodel, string fecharegistroal, string fechasolicituddel, string fechasolicitudal, string idpromotoria, string estado, int IdUsuario)
        {
            LlenarControles.LlenarRepeaterLista(ref gridview, tramite.Buscar(foliocompuesto, fecharegistrodel, fecharegistroal, fechasolicituddel, fechasolicitudal, idpromotoria, estado, IdUsuario)); 
        }

        //public void Tramite_LlenarGridView(ref GridView gridview, string fase)
        //{
        //    LlenarControles.LlenarGridView(ref gridview, tramite.SeleccionarTramitesPorMesa(fase));
        //}

        public DataTable Tramite_PorMesas(int IdTipoTramite)
        {
            return tramite.SeleccionarTramitesPorMesa(IdTipoTramite);
        }

        public List<prop.TramiteDetalle> Tramite_PorMesas_Detalle(int IdMesa, int IdEstatus)
        {
            return tramite.Tramite_EstadosMesa_Detalle(IdMesa, IdEstatus);
        }

        public void TramiteConsultaX_LlenarGridView(ref GridView gridview)
        {
            LlenarControles.LlenarGridView(ref gridview, tramite.SeleccionarTramiteConsultaX());
        }

        public void ListadoTramitesOperacion(ref ASPxGridView aspxgridview)
        {
            LlenarControles.LlenaraAspxGridView(ref aspxgridview, tramite.ListadoTramitesOperacion());
        }


        public void TramiteConsultaX_LlenarAspxGridView(ref ASPxGridView aspxgridview)
        {
            LlenarControles.LlenaraAspxGridView(ref aspxgridview, tramite.SeleccionarTramiteConsultaX());
        }

        public DataSet qryListadoTramitesOperacion()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(tramite.ListadoTramitesOperacion());
            return ds;

        }

        public DataSet qryListadoTramitesOperacionN3()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(tramite.ListadoTramitesOperacionN3());
            return ds;

        }

        public DataSet TramiteConsultaX()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(tramite.SeleccionarTramiteConsultaX());
            return ds;

        }

        public List<prop.Tramite> TramiteSupervicionGeneralSelecionarFechas(int Id, DateTime Fecha_Inicio, DateTime Fecha_Termino, string Folio, string RFC, string Nombre, string ApPaterno, string ApMaterno)
        {
            return tramite.TramiteSupervicionGeneralSelecionarFechas(Id, Fecha_Inicio, Fecha_Termino, Folio, RFC, Nombre, ApPaterno, ApMaterno);
        }

        public List<prop.Tramite> TramiteSupervicionGeneralSelecionar(int Id, string Folio, string RFC, string Nombre, string ApPaterno, string ApMaterno)
        {
            return tramite.TramiteSupervicionGeneralSelecionar(Id, Folio, RFC, Nombre, ApPaterno, ApMaterno);
        }
    }
}
