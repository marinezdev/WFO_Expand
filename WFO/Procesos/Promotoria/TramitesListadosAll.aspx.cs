using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Procesos.Promotoria
{
    public partial class TramitesListadosAll : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Operacion.Tramites tramites = new Negocio.Procesos.Operacion.Tramites();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
                FormatosFechas();
            }
        }

        protected void BtnConsultar_Click(object sender, EventArgs e)
        {
            RepeaterFechas.Visible = false;
            DateTime FechaInicial = Convert.ToDateTime(dtFechaInicio.Text + " 00:00:00");
            DateTime FechaTermino = Convert.ToDateTime(dtFechaTermino.Text + " 23:59:59");

            // NUEVOS PARAMETROS DE BUSQUEDA
            string Folio = TextFolio.Text.ToString().Trim();
            string RFC = TextRFC.Text.ToString().Trim();
            string Nombre = txNombre.Text.ToString().Trim();
            string ApPaterno = txApPat.Text.ToString().Trim();
            string ApMaterno = txApMat.Text.ToString().Trim();

            //Fecha1 = Fecha1.AddDays(+1);

            DateTime FechaCalculada = FechaInicial;

            Mensajes.Text = "";

            if (DateTime.Compare(FechaTermino, FechaInicial) < 0)
            {
                string script2 = "";
                script2 = "retirar();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
                RepeaterFechas.Visible = false;
            }
            else
            {
                if (FechaCalculada.AddMonths(-3).AddDays(-1) <= FechaInicial)
                {
                    manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
                    List<prop.Tramites> Tramites = tramites.TramiteOperadorSelecionar(manejo_sesion.Usuarios.IdUsuario, FechaInicial, FechaTermino, Folio, RFC, Nombre, ApPaterno, ApMaterno);
                    RepeaterFechas.DataSource = Tramites;
                    RepeaterFechas.DataBind();
                    RepeaterFechas.Visible = true;

                    //string script3 = "";
                    //script3 = "retirar();";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script3, true);

                    string script2 = "";
                    script2 = "retirar(); $('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
                }
                else
                {
                    string script2 = "";
                    script2 = "retirar();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
                    RepeaterFechas.Visible = false;
                    Mensajes.Text = "El rango de fechas supera los 3 meses ";
                }
            }
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            // Inicialización de Variables
            RepeaterFechas.Visible = false;

            TextFolio.Text = "";
            TextRFC.Text = "";
            txNombre.Text = "";
            txApPat.Text = "";
            txApMat.Text = "";
            Mensajes.Text = "";
        }


        protected void rptTramite_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Consultar"))
            {
                string IdTramite = e.CommandArgument.ToString();
                //Response.Redirect("ConsultaTramite.aspx?Procesable=" + IdTramite);
                Response.Redirect(EncripParametros("Id=" + IdTramite, "ConsultaTramite.aspx").URL, true);
            }
        }

        private void FormatosFechas()
        {
            // INICIO DE FECHAS
            DateTime Fecha = DateTime.Today;

            dtFechaInicio.MaxDate = Fecha.AddDays(+1);
            //dtFechaInicio.MinDate = validateFechaSolicitud.AddDays(-30);
            dtFechaInicio.UseMaskBehavior = true;
            dtFechaInicio.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");
            dtFechaInicio.Date = new DateTime(Fecha.Year, Fecha.Month, 1);

            //dtFechaTermino.MinDate = Fecha.AddMonths(-4);
            dtFechaTermino.MaxDate = Fecha;
            dtFechaTermino.UseMaskBehavior = true;
            dtFechaTermino.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");
            dtFechaTermino.Date = Fecha;
        }

        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();

            string Encrypt = Negocio.Sistema.UrlCifrardo.Encrypt(parametros);

            urlCifrardo.URL = Direccion + "?data=" + Encrypt;

            return urlCifrardo;
        }

    }
}