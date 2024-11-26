using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.Procesos.Promotoria
{
    public partial class MisTramites : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Promotoria.TramitesPromotoria tramitesPromotoria = new Negocio.Procesos.Promotoria.TramitesPromotoria();
        WFO.Negocio.Procesos.Promotoria.Catalogos catalogos = new Negocio.Procesos.Promotoria.Catalogos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];

                List<prop.TramitesPromotoria> Tramites = tramitesPromotoria.ListaTramitesPromotoria(manejo_sesion.Usuarios.IdUsuario);

                rptTramite.DataSource = Tramites;
                rptTramite.DataBind();

                //ListaCat_StatusTramite();
                //FormatosFechas();
            }
        }

        protected void rptTramite_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Consultar"))
            {
                string IdTramite = e.CommandArgument.ToString();
                //Response.Redirect("ConsultaTramite.aspx?Id=" + IdTramite);
                Response.Redirect(EncripParametros("Id=" + IdTramite, "ConsultaTramite.aspx").URL, true);

            }
        }

        //private void ListaCat_StatusTramite()
        //{
        //    List<prop.cat_statusTramite> cat_StatusTramites = catalogos.cat_StatusTramites();
        //    LisCat_StatusTramite.DataSource = cat_StatusTramites;
        //    LisCat_StatusTramite.DataBind();
        //    LisCat_StatusTramite.DataTextField = "Nombre";
        //    LisCat_StatusTramite.DataValueField = "Id";
        //    LisCat_StatusTramite.DataBind();
        //}

        //protected void BtnConsultar_Click(object sender, EventArgs e)
        //{
        //    TimeSpan ti = new TimeSpan(00, 00, 00);
        //    TimeSpan ts = new TimeSpan(23, 59, 59);

        //    DateTime Fecha1 = Convert.ToDateTime(dtFechaInicio.Text);
        //    DateTime Fecha2 = Convert.ToDateTime(dtFechaTermino.Text);

        //    Fecha1 = Fecha1 + ti;
        //    Fecha2 = Fecha2 + ts;

        //    int IdStatus = Convert.ToInt32(LisCat_StatusTramite.SelectedValue);

        //    DateTime FechaCalculada = Fecha1;

        //    Mensajes.Text = "";

        //    if (Fecha1 <= Fecha2)
        //    {
        //        Mensajes.Text = "La fecha 'Desde' debe ser mayor a la fecha 'Hasta'";
        //        RepeaterFechas.Visible = false;
        //    }
        //    else
        //    {
        //        //if (Fecha2 >= FechaCalculada.AddMonths(-3))
        //        //{
        //            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
        //            List<prop.TramitesPromotoria> Tramites = tramitesPromotoria.ListaTramitesPromotoriaFechas(manejo_sesion.Usuarios.IdUsuario, IdStatus, Fecha1, Fecha2);
        //            RepeaterFechas.DataSource = Tramites;
        //            RepeaterFechas.DataBind();
        //            RepeaterFechas.Visible = true;
        //            string script = "";
        //            script = "$('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); ";
        //            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

        //        //}
        //        //else
        //        //{
        //        //    Mensajes.Text = "La fecha termino es mayor a los 3 meses permitidos";
        //        //    RepeaterFechas.Visible = false;
        //        //}
        //    }
        //}


        //protected void ActualizaFechaTermino(object sender, EventArgs e)
        //{
        //    DateTime Fecha = Convert.ToDateTime(dtFechaInicio.Value.ToString());
        //    dtFechaTermino.MinDate = Fecha.AddMonths(-3);
        //    dtFechaTermino.MaxDate = Fecha;
        //    dtFechaTermino.UseMaskBehavior = true;
        //    dtFechaTermino.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");
        //    dtFechaTermino.Date = Fecha.AddMonths(-3);
        //}

        //private void FormatosFechas()
        //{
        //    // INICIO DE FECHAS
        //    DateTime Fecha = DateTime.Today;

        //    dtFechaInicio.MaxDate = Fecha.AddDays(+1);
        //    //dtFechaInicio.MinDate = validateFechaSolicitud.AddDays(-30);
        //    dtFechaInicio.UseMaskBehavior = true;
        //    dtFechaInicio.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");
        //    dtFechaInicio.Date = Fecha;

        //    //dtFechaTermino.MinDate = Fecha.AddMonths(-4);
        //    dtFechaTermino.MaxDate = Fecha;
        //    dtFechaTermino.UseMaskBehavior = true;
        //    dtFechaTermino.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");
        //    dtFechaTermino.Date = Fecha.AddMonths(-1);
        //}

        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();

            string Encrypt = Negocio.Sistema.UrlCifrardo.Encrypt(parametros);

            urlCifrardo.URL = Direccion + "?data=" + Encrypt;

            return urlCifrardo;
        }
    }
}