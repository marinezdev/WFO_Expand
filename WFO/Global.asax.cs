using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WFO
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            string sessionId = Session.SessionID;

            if (Session["Sesion"] == null)
            {
                string strpPathApp = string.Empty;
                if (Request.ApplicationPath.Length == 1)
                {
                    strpPathApp = "";
                }
                else
                {
                    strpPathApp = Request.ApplicationPath;
                }
                Response.Redirect(strpPathApp + "/Default.aspx");
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Desconectado de la sesión en caso de error
            WFO.Negocio.Sistema.Usuarios sisusr = new WFO.Negocio.Sistema.Usuarios();
            if (HttpContext.Current.Session != null)
            {
                IU.ManejadorSesion manejo_sesion = (IU.ManejadorSesion)HttpContext.Current.Session["Sesion"];
                if (manejo_sesion != null)
                    sisusr.ActualizarDesconectarSesion(manejo_sesion.Usuarios.IdUsuario, 0);
            }
            Exception ex = Server.GetLastError();
            WFO.RegistraLog.RegistraLog log = new WFO.RegistraLog.RegistraLog("Log", HttpContext.Current.Server.MapPath("~"), "WFO Error System");
            log.Agregar("Error: " + ex);
        }

        protected void Session_End(object sender, EventArgs e)
        {
            WFO.Negocio.Sistema.Usuarios usrs = new Negocio.Sistema.Usuarios();
            usrs.ActualizarDesconectarSesion(int.Parse(Session["idusuario"].ToString()), 0);
            
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Server.ClearError();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Server.ClearError();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        }
    }
}