namespace WFO.Negocio.Procesos.SupervisionGeneral
{
    public class SupervisionGeneral
    {
        public AccesoDatos.Procesos.Mesa mesa;
        public AccesoDatos.SupervisionGeneral.Tramite tramite;
        public AccesoDatos.SupervisionGeneral.TramiteBitacora tramitebitacora;
        public AccesoDatos.Procesos.TramiteMesa tramitemesa;
        public AccesoDatos.Procesos.TramiteMesaBitacora tramitemesabitacora;
        public AccesoDatos.Procesos.Tramite_Asigna_Futuro tramiteasignafuturo;
        public AccesoDatos.Procesos.Promotoria.cat_promotoria catpromotoria;
        public AccesoDatos.SupervisionGeneral.Usuarios usuarios;
        public AccesoDatos.SupervisionGeneral.Productividad productividad;

        public SupervisionGeneral()
        {
            mesa = new AccesoDatos.Procesos.Mesa();
            tramite = new AccesoDatos.SupervisionGeneral.Tramite();
            tramitebitacora = new AccesoDatos.SupervisionGeneral.TramiteBitacora();
            tramitemesa = new AccesoDatos.Procesos.TramiteMesa();
            tramitemesabitacora = new AccesoDatos.Procesos.TramiteMesaBitacora();
            tramiteasignafuturo = new AccesoDatos.Procesos.Tramite_Asigna_Futuro();
            catpromotoria = new AccesoDatos.Procesos.Promotoria.cat_promotoria();
            usuarios = new AccesoDatos.SupervisionGeneral.Usuarios();
            productividad = new AccesoDatos.SupervisionGeneral.Productividad();
        }
    }
}
