using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Utilerias
{
    public partial class Menu : System.Web.UI.UserControl
    {
        //WFO.Negocio.Sistema.Menu menu = new WFO.Negocio.Sistema.Menu();
        WFO.IU.ManejadorSesion manejo_sesion = new WFO.IU.ManejadorSesion();

        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
            LblMenu.Text = manejo_sesion.Menu;
        }
    }
}