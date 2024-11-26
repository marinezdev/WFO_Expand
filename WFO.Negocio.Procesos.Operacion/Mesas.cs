using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Negocio.Procesos.Operacion
{
    public class Mesas
    {
        AccesoDatos.Procesos.Operacion.Mesas mesas = new AccesoDatos.Procesos.Operacion.Mesas();

        public List<prop.Mesa> SelecionarMesas(int Id_Usuario, int IdFlujo)
        {
            return mesas.SelecionarMesas(Id_Usuario, IdFlujo);
        }

        public List<prop.Mesa> SelecionarMesasFlujo(int IdFlujo)
        {
            return mesas.SelecionarMesasFlujo(IdFlujo);
        }


        public List<prop.Mesa> ObtenerMesasToSend(int Id_Tramite, int Id_Usuario, int Id_Mesa)
        {
            return mesas.ObtenerMesasToSend(Id_Tramite, Id_Usuario, Id_Mesa);
        }

        
        public List<prop.Procesos> SelecionarUsuariosProcesos(int IdUsuario, int IdProceso)
        {
            return mesas.SelecionarUsuariosProcesos(IdUsuario, IdProceso);
        }


        public List<prop.Mesa> SelecionarMesasUsuarioMesa(int Id_Usuario, int Id_Mesa)
        {
            return mesas.SelecionarMesasUsuarioMesa(Id_Usuario, Id_Mesa);
        }

        public void Mesas_DropDownList(ref DropDownList dropdownlist)
        {
            Funciones.LlenarControles.LlenarDropDownList(ref dropdownlist, mesas.Seleccionar(), "Nombre", "Id");
        }
    }
}
