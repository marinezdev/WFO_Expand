using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.SupervisionGeneral;

namespace WFO.Procesos.SupervisionGeneral
{
    public partial class ListarTramites : Utilerias.Comun
    {
        WFO.Negocio.Procesos.SupervisionGeneral.Tramite tramite = new Negocio.Procesos.SupervisionGeneral.Tramite();

        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];

            if (!IsPostBack)
            {
                FormatosFechas();
            }
        }
        
        protected void BtnConsultar_Fechas(object sender, EventArgs e)
        {
            RepeaterFechas.Visible = false;
            DateTime Fecha1 = Convert.ToDateTime("2000/01/01 00:00:00");
            DateTime Fecha2 =  DateTime.Now.AddDays(+1);
            string Folio = "";
            string RFC = "";
            string Nombre = "";
            string ApPaterno = "";
            string ApMaterno = "";
            string script2 = "";

            try
            {
                if (dtFechaInicio.Text.Length != 0 && dtFechaTermino.Text.Length != 0)
                {
                    try
                    {
                        Fecha1 = Convert.ToDateTime(dtFechaInicio.Text);
                        Fecha2 = Convert.ToDateTime(dtFechaTermino.Text);
                    }
                    catch (Exception ex)
                    {
                        script2 = "";
                        script2 = "retirar();";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
                        RepeaterFechas.Visible = false;
                        Mensajes.Text = "Formato de Fechas Inválido.";
                        return;
                    }

                    if (DateTime.Compare(Fecha2, Fecha1) < 0)
                    {
                        script2 = "";
                        script2 = "retirar();";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
                        RepeaterFechas.Visible = false;
                    }
                    else
                    {
                        if (Fecha1.AddMonths(-3).AddDays(-1) > Fecha2)
                        {
                            script2 = "";
                            script2 = "retirar();";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
                            RepeaterFechas.Visible = false;
                            Mensajes.Text = "El rango de fechas supera los 3 meses ";
                        }
                    }
                }

                // Inicialización de Variables
                Folio = TextFolio.Text.ToString().Trim();
                RFC = TextRFC.Text.ToString().Trim();
                Nombre = txNombre.Text.ToString().Trim();
                ApPaterno = txApPat.Text.ToString().Trim();
                ApMaterno = txApMat.Text.ToString().Trim();
                Mensajes.Text = "";

                // Generación de reportes...
                RepeaterFechas.DataSource = supervisiongeneraltramites.TramiteSupervicionGeneralSelecionarFechas(manejo_sesion.Usuarios.IdUsuario, Fecha1, Fecha2, Folio, RFC, Nombre, ApPaterno, ApMaterno);
                RepeaterFechas.DataBind();
                RepeaterFechas.Visible = true;

                // Formato de tabla
                script2 = "";
                script2 = "$('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); retirar();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
            }
            catch (Exception ex)
            {
                string MensajeError = ex.Message;

                script2 = "";
                script2 = "retirar();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
                RepeaterFechas.Visible = false;
            }
        }

        protected void BtnConsultar_Click(object sender, EventArgs e)
        {
            // Inicialización de Variables
            RepeaterFechas.Visible = false;

            string Folio = TextFolio.Text.ToString().Trim();
            string RFC = TextRFC.Text.ToString().Trim();
            string Nombre = txNombre.Text.ToString().Trim();
            string ApPaterno = txApPat.Text.ToString().Trim();
            string ApMaterno = txApMat.Text.ToString().Trim();
            string script2 = "";
            string cade = Folio + RFC + Nombre+ ApPaterno+ ApMaterno;
            Mensajes.Text = "";
            if (cade.Length>0)
            {
                // Generación de reportes...
                RepeaterFechas.DataSource = supervisiongeneraltramites.TramiteSupervicionGeneralSelecionar(manejo_sesion.Usuarios.IdUsuario, Folio, RFC, Nombre, ApPaterno, ApMaterno);
                RepeaterFechas.DataBind();
                RepeaterFechas.Visible = true;

                // Formato de tabla
                script2 = "";
                script2 = "$('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); retirar();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
            }
            else
            {
                script2 = "";
                script2 = "retirar();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
                Mensajes.Text = "Colca algún parámetro de filtración.";
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
                Response.Redirect("ConsultaTramite.aspx?Procesable=" + IdTramite);
            }
        }

        private void FormatosFechas()
        {
            // INICIO DE FECHAS
            //DateTime Fecha = DateTime.Today;

            //dtFechaInicio.MaxDate = Fecha;
            //dtFechaInicio.UseMaskBehavior = true;
            //dtFechaInicio.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");
            //dtFechaInicio.Date = Fecha;

            //dtFechaTermino.MaxDate = Fecha;
            //dtFechaTermino.UseMaskBehavior = true;
            //dtFechaTermino.EditFormatString = Funciones.Fechas.GetFormatString("dd/MM/yyyy");
            //dtFechaTermino.Date = Fecha;

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
    }
}