using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO
{
    public partial class Default : WFO.Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
            //Eliminar una sola
            Session.Remove("Sesion");
            Session["Sesion"] = null;
            Session.Remove("idusuario");
            Session["idusuario"] = null;
            Session.Remove("IdSesion");
            Session["IdSesion"] = null;
            Session.Remove("Inicio");
            Session["Inicio"] = null;
            //Eliminar todas las variables
            Session.Contents.RemoveAll();

#if DEBUG
#else
            ///////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////
            // COMENTAR TODAS LAS LINEAS PARA DEJAR ENTRAR POR LOGION
            
            //PARAMETROS DE AUTENTITFIACION POR WEB SERVICE EN EL CASO DE NO EXISTIR REDIRIGIR AL LISTADO DE APLIACIONES.
            if (Request.Params["numlife"] != null && Request.Params["us"] != null && Request.Params["ap"] != null)
            {
                // VALIDACION POR WEB SERVICE. - RETORNARA EL TOKEN Y EL NOMBRE DEL USUARIO.
                string token = Request.Params["numlife"].ToString();
                int IdUsuario = Convert.ToInt32(Request.Params["us"]);
                int IdAplicacion = Convert.ToInt32(Request.Params["ap"]);

                // COLOCA AUTENTITIFACION POR WS
                WFO.Negocio.Sistema.CredencialesWS credenciales = sisAutenti.Autentificarws(IdUsuario, IdAplicacion, token);

                if (credenciales.Token != "0")
                {
                    manejo_sesion.Inicializar();
                    manejo_sesion.Usuarios = sisUsrs.SeleccionarPorId(credenciales.Id);
                    manejo_sesion.Token = credenciales.Token;
                    Propiedades.Configuracion conf = new Propiedades.Configuracion();
                    //manejo_sesion.DiasAvisoCambioContraseña = sisConfig.SeleccionarPorId(3).Valor;
                    manejo_sesion.Menu = sisMenu.Seleccionar(manejo_sesion.Usuarios.IdRol);

                    Session["Sesion"] = manejo_sesion;
                    Session["idusuario"] = manejo_sesion.Usuarios.IdUsuario;
                    Session["IdSesion"] = HttpContext.Current.Session.SessionID;
                    Session["Inicio"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                    sisUsrs.RegistroLog(Session["IdSesion"].ToString(), Session["idusuario"].ToString(), Session["Inicio"].ToString(), 1);

                    Response.Redirect(sisRA.SeleccionarPorRol(manejo_sesion.Usuarios.IdRol), false);
                }
                else
                {
                    string host = HttpContext.Current.Request.Url.Host;
                    Response.Redirect("https://" + host + "/MetLife/");
                    //Response.Redirect("http://localhost:51634/Default.aspx");
                }
            }
            else
            {
                string host = HttpContext.Current.Request.Url.Host;
                Response.Redirect("https://" + host + "/MetLife/");
                //Response.Redirect("http://localhost:51634/Default.aspx");
            }


            //if (!SM1.IsInAsyncPostBack)
            //    Session["timeout"] = DateTime.Now.AddMinutes(double.Parse(manejo_sesion.EsperaBloqueo)).ToString();
            ///////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////*/
#endif
        }

        protected string getIP()
        {
            string strIP = "";

            try
            {
                strIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[2].ToString();
            }
            catch (Exception ex)
            {
                strIP = "<< no asignada >>";
            }

            return strIP;
        }


        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string localIP = getIP();
            lnkRecuperarClave.Visible = false;

            if (sisUsrs.Validar(txUsuario.Text, txClave.Text))
            {
                manejo_sesion.Inicializar();
                manejo_sesion.Usuarios = sisUsrs.SeleccionarDetalle(txUsuario.Text, txClave.Text);
                Propiedades.Configuracion conf = new Propiedades.Configuracion();
                manejo_sesion.DiasAvisoCambioContraseña = sisConfig.SeleccionarPorId(3).Valor;
                manejo_sesion.Menu = sisMenu.Seleccionar(manejo_sesion.Usuarios.IdRol);

                // ACCESO DIRECTO
                Session["Sesion"] = manejo_sesion;
                Session["idusuario"] = manejo_sesion.Usuarios.IdUsuario;
                Session["IdSesion"] = HttpContext.Current.Session.SessionID;
                Session["Inicio"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                sisUsrs.RegistroLog(Session["IdSesion"].ToString(), Session["idusuario"].ToString(), Session["Inicio"].ToString(), 1);

                Response.Redirect(sisRA.SeleccionarPorRol(manejo_sesion.Usuarios.IdRol), false);
                
                /*
                if (sisUsrs.ActualizarSesion(manejo_sesion.Usuarios.IdUsuario, 1) == 1)
                {
                    //************** Validación del cambio de contraseña
                    if (sisUsrs.ValidarAcceso(manejo_sesion.Usuarios.IdUsuario.ToString()))
                    {
                        Session["Sesion"] = manejo_sesion;
                        Session["idusuario"] = manejo_sesion.Usuarios.IdUsuario;
                        Session["IdSesion"] = HttpContext.Current.Session.SessionID;
                        Session["Inicio"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        
                        sisUsrs.RegistroLog(Session["IdSesion"].ToString(), Session["idusuario"].ToString(), Session["Inicio"].ToString() , 1);

                        Response.Redirect(sisRA.SeleccionarPorRol(manejo_sesion.Usuarios.IdRol), false);
                    }
                      else
                    {
                        log.Agregar(txUsuario.Text + " intenta ingresar al sistema pero la contraseña ha caducado.");
                        LblMensajes.Text = "Por favor contacte al Administrador para reestablecer su contraseña.";
                        lnkRecuperarClave.Visible = true;
                        LoginButton.Attributes["disabled"] = "enabled";
                        //LoginButton.Enabled = false;
                        //LoginButton.CssClass = "btn-block";
                        //Label1.Visible = true;
                        Session["idusuario"] = manejo_sesion.Usuarios.IdUsuario;
                    }
                }
                else
                {
                    log.Agregar(txUsuario.Text + " intenta ingresar al sistema pero está bloqueado. IP: " + localIP);
                    LblMensajes.Text = "Acceso del usuario se encuentra bloqueado. Intente de nuevo después de " + manejo_sesion.EsperaBloqueo + " minutos. Si continúa bloqueado contacte al administrador.";
                    lnkRecuperarClave.Visible = true;
                    LoginButton.Attributes["disabled"] = "enabled";
                    //LoginButton.Enabled = false;
                    //LoginButton.CssClass = "btn-block";
                    //Label1.Visible = true;
                    Session["idusuario"] = manejo_sesion.Usuarios.IdUsuario;
                }
                */
            }
            else
            {
                log.Agregar(txUsuario.Text + " ha intentado ingresar al sistema, ha equivocado su clave o intenta accesar sin autorización. IP: " + localIP);
                LblMensajes.Text = "Clave o contraseña no existen en el sistema.";
                lnkRecuperarClave.Visible = true;
            }
        }

        //private void UpdateTimer()
        //{
        //    Label1.Text = DateTime.Now.ToLongTimeString();
        //}

        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    if (0 > DateTime.Compare(DateTime.Now, Funciones.Fechas.ConvertirTextoAFecha(Session["timeout"].ToString())))
        //        Label1.Text = "Quedan " + ((Int32)Funciones.Fechas.ConvertirTextoAFecha(Session["timeout"].ToString()).Subtract(DateTime.Now).TotalMinutes).ToString() + " minutos para desbloquear. <br /> No cierre el navegador.";
        //    else
        //    {
        //        try
        //        {
        //            sisUsrs.ActualizarDesconectarSesion(Funciones.Numeros.ConvertirTextoANumeroEntero(Session["idusuario"].ToString()), 0);
        //            Session["idusuario"] = null;
        //            LblMensajes.Text = "";
        //            Label1.Text = "";
        //            Response.Redirect("Default.aspx", false);
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
    }
}