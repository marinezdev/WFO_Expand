using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
    public class DetalleHoras
    {
        AccesoDatos.Procesos.TramiteMesa TM = new AccesoDatos.Procesos.TramiteMesa();
        AccesoDatos.Procesos.Usuarios U = new AccesoDatos.Procesos.Usuarios();

        public void DatosDetalleHoras(ref DevExpress.Web.ASPxGridView aSPxGridView, ref DevExpress.Web.ASPxDropDownEdit DropdownEdit, string fechaDesde, string fechaHasta, string idFlujo)
        {
            DataTable dt = new DataTable();
            string dUsuarios = string.Empty;
            string usuarios = DropdownEdit.Text;
            if (string.IsNullOrEmpty(usuarios))
            {
                dUsuarios = "' '";
            }
            else
            {
                string[] listaUsuarios = usuarios.Split(';');
                foreach (string usuario in listaUsuarios)
                {
                    dUsuarios += "'" + usuario.Trim() + "',";

                }
                dUsuarios = dUsuarios.Trim(',');
            }


            dt = TM.DetalleHoras(fechaDesde,fechaHasta, dUsuarios,idFlujo);
            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();
        }
        public DataTable DatosUsuarios(string idFlujo)
        {
            DataTable dt = new DataTable();
            dt = U.NombreUsuarios(idFlujo);
            return dt;
        }
    }
}
