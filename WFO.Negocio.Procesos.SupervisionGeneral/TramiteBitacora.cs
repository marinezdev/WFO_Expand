namespace WFO.Negocio.Procesos.SupervisionGeneral
{
    public class TramiteBitacora : SupervisionGeneral
    {
        public int Agregar(string usuariocambio, string usuarioanterior, string tramite, string idpriodidadanterior)
        {
            return tramitebitacora.Agregar(usuariocambio, usuarioanterior, tramite, idpriodidadanterior);
        }


    }
}
