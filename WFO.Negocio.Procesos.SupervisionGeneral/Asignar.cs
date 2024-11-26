using System.Collections.Generic;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.SupervisionGeneral;

namespace WFO.Negocio.Procesos.SupervisionGeneral
{
    public class Asignar : SupervisionGeneral
    {
        public void MostrarMesasDisponibles(ref GridView gridview, string idflujo)
        {
            Funciones.LlenarControles.LlenarGridView(ref gridview, mesa.SeleccionarPorFlujo(idflujo));
        }

        public void MostrarMesasDisponiblesAsiganr(ref Repeater repeater, Label labelFolio, int IdTramite)
        {
            List<prop.TramiteMesaAsignar> tramiteMesaAsignar = tramitemesa.SeleccionarMesasAsignar(IdTramite);
            if (tramiteMesaAsignar.Count > 0)
            {
                labelFolio.Text = tramiteMesaAsignar[0].Folio;

                Funciones.LlenarControles.LlenarRepeaterLista(ref repeater, tramiteMesaAsignar);
            }
            

            
        }


        public int ActualizarUsuarioMesa(int IdTramitemesa, int IdUsuario)
        {
            return tramitemesa.ActualizarUsuarioMesa(IdTramitemesa, IdUsuario);
        }


        public void CambiarUsuarioAnterior(string idtramitemesa, string idusuario)
        {
            tramitemesa.ModificarUsuarioAnterior(idtramitemesa, idusuario);
        }

        public void AgregarTramiteMesaBitacoraCambios(string idusuarioanterior, string idusuarionuevo, string idusuariocambio, string idtramitemesa)
        {
            tramitemesabitacora.Agregar(idusuarioanterior, idusuarionuevo, idusuariocambio, idtramitemesa);
        }

        public void AgregarUsuarioFuturo(string idusuario, string idusuarioasigna, string idtramite)
        {
            tramiteasignafuturo.AgregarUsuarioFuturo(idusuario, idusuarioasigna, idtramite);
        }

    }
}
