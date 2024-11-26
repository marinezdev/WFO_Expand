using DevExpress.Web;

namespace WFO.Negocio.Procesos.Supervision
{
    public class TramitesPorMesa
    {
        AccesoDatos.Procesos.Tramite tramite = new AccesoDatos.Procesos.Tramite();

        public void ObtenerTramitesPorMesaLlenar(ref ASPxGridView aspxgridview, string fechainicial, string fechafinal, string statustramite, string mesa)
        {
             Funciones.LlenarControles.LlenaraAspxGridView(ref aspxgridview, tramite.TramitesPorMesa(fechainicial, fechafinal, statustramite, mesa));
        }

        public void ObtenerTramitesPorMesaLista(ref ASPxGridView aspxgridview, string fechainicial, string fechafinal, string statustramite, string mesa)
        {
            Funciones.LlenarControles.LlenarAspxGridView(ref aspxgridview, tramite.TramitesPorMesaLista(fechainicial, fechafinal, statustramite, mesa));
        }
    }
}
