using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WFO.Administracion
{
    public partial class frmLog : WFO.Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                manejo_sesion = (IU.ManejadorSesion)Session["Sesion"];

                ObtenerArchivosDirectorio();
            }
            catch (Exception ex)
            {
                log.Agregar("Problemas en la carga inicial/Carga de archivos de error Administracion/frmLog: " + ex.Message);
                mensajes.MostrarMensaje(this, "Ha habido un error al inicio de la página/carga de archivos de error, revise el log para ver los detalles. Fin de la operación.", "Default.aspx");
            }

        }

        protected void ObtenerArchivosDirectorio()
        {
            DataTable dt = new DataTable();
            //Columnas para la nueva tabla
            dt.Columns.Add("Nombre");

            //Fila para la tabla
            string directorioArchivosTemporales = Server.MapPath("~/Log");
            DirectoryInfo d = new DirectoryInfo(directorioArchivosTemporales);

            foreach (var file in d.GetFiles("*.*"))
            {
                dt.Rows.Add(file.Name);
            }

            gvArchivos.DataSource = dt;
            gvArchivos.DataBind();
        }


    }
}