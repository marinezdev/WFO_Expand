using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Procesos.Supervision
{
    public class ReporteProductividad
    {
        AccesoDatos.Procesos.BitacoraDos BD = new AccesoDatos.Procesos.BitacoraDos();
        AccesoDatos.Procesos.Usuarios U = new AccesoDatos.Procesos.Usuarios();

        public void DatosProductividad(ref DevExpress.Web.ASPxGridView aSPxGridView, ref DevExpress.Web.ASPxDropDownEdit DropdownEdit, string fechaDesde, string fechaHasta, string idFlujo)
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
            dt = BD.DatosProductividad(fechaDesde, fechaHasta, dUsuarios, idFlujo);
            aSPxGridView.DataSource = dt;
            aSPxGridView.DataBind();
        }
        public DataTable DatosDetalleProductividad(string fechaDesde, string fechaHasta,string usuario,string mesa, string idFlujo)
        {
            DataTable dt = new DataTable();
            dt = BD.DatosDetalleProductividad(fechaDesde, fechaHasta, usuario, mesa, idFlujo);
            return dt;
        }
        public DataTable DatosUsuarios(string idFlujo)
        {
            DataTable dt= new DataTable();
            dt = U.NombreUsuarios(idFlujo);
            return dt;
        }
    }
}
