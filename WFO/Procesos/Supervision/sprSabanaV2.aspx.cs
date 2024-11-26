using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;
using prop = WFO.Propiedades.Procesos.Operacion;
using prosu = WFO.Propiedades.Procesos.SupervisionGeneral;

namespace WFO.Procesos.Supervision
{
    public partial class sprSabanaV2 : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Operacion.UsuariosFlujo usuariosFlujo = new Negocio.Procesos.Operacion.UsuariosFlujo();

        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (IU.ManejadorSesion)Session["Sesion"];
            if (!IsPostBack)
            {
                CalDesde.EditFormatString = "yyyy-MM-dd";
                CalDesde.Date = DateTime.Now.AddDays(-1);
                CalDesde.MaxDate = DateTime.Today;
                CalHasta.EditFormatString = "yyyy-MM-dd";
                CalHasta.Date = DateTime.Today;
                CalHasta.MaxDate = DateTime.Today;

                CargaFlujos(manejo_sesion.Usuarios.IdUsuario);

            }
        }

        protected void CargaFlujos(int Id)
        {
            List<prop.UsuariosFlujo> Flujos = usuariosFlujo.SelecionarFlujoSabana(Id);
            cbFlujos.DataSource = Flujos;
            cbFlujos.DataBind();
            cbFlujos.TextField = "Nombre";
            cbFlujos.ValueField = "Id";
            cbFlujos.DataBind();
        }

        protected void btnFiltroMes_Click(object sender, EventArgs e)
        {
            String script;
            Mensaje.Text = "";
            if (CalDesde.Date <= CalHasta.Date)
            {
                int IdFlujo = Convert.ToInt32(cbFlujos.SelectedItem.Value.ToString());
                List<prosu.Tramite> tramites = rs.Tramite_UltimoEstatusTramite(CalDesde.Date, CalHasta.Date, IdFlujo);
                int num = tramites.Count;
                rptTramites.DataSource = tramites;
                rptTramites.DataBind();

                // Formato de tabla
                script = "";
                script = "$('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); retirar();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            else
            {
                Mensaje.Text = "La fecha 'Desde' debe ser menor a la fecha 'Hasta'";
                //rptTramitesEspera.Visible = false;
            }

        }

        [WebMethod]
        public static ConsultasMesasV2 Busqueda(int Id)
        {
            WFO.Negocio.Procesos.Supervision.Sabana sabana = new Negocio.Procesos.Supervision.Sabana();

            List<prosu.DetalleMesa> tramites = sabana.Tramite_InformacionBitacora(Id);

            /* LLENAR JSON PARA RETORNAR */
            ConsultasMesasV2 jsonObject = new ConsultasMesasV2();
            jsonObject.consulta = new List<ConsultaV2>();

            foreach (prosu.DetalleMesa item in tramites)
            {
                jsonObject.consulta.Add(new ConsultaV2()
                {
                    Orden = item.NORDENREPORTE.ToString(),
                    IdTramite = item.IdTramite.ToString(),
                    FechaRegistro = item.FechaRegistro.ToString(),
                    NMESA = item.NMESA.ToString(),
                    FechaInicio = item.FechaInicio.ToString(),
                    FechaTermino = item.FechaTermino.ToString(),
                    EstadoMesa = item.EstadoMesa.ToString(),
                    Observacion = item.Observacion.ToString(),
                    NombreUsuario = item.NombreUsuario.ToString(),
                });
            }

            return jsonObject;
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            Mensaje.Text = "";
            if (CalDesde.Date <= CalHasta.Date)
            {

                if (this.cbFlujos.SelectedItem == null || this.cbFlujos.SelectedIndex == -1)
                {
                    string script = "BitacoraSabana();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                else
                {
                    int IdFlujo = Convert.ToInt32(cbFlujos.SelectedItem.Value.ToString());
                    string script = "window.open('sprSabanaDescargaV2.aspx?In=" + CalDesde.Date + "&Fn=" + CalHasta.Date + "&Flu=" + IdFlujo + "','Expediente', 'width = 800, height = 400'); ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            else
            {
                Mensaje.Text = "La fecha 'Desde' debe ser menor a la fecha 'Hasta'";
                //rptTramitesEspera.Visible = false;
            }
        }

        [WebMethod]
        public static ConsultaBitacoraSabanaV2 BusquedaBitacoraDescraga()
        {
            WFO.Negocio.Procesos.Supervision.Sabana sabana = new Negocio.Procesos.Supervision.Sabana();
            List<prosu.BitacoraSabana> tramites = sabana.SabanaConsultaBitacoraDescarga();

            /* LLENAR JSON PARA RETORNAR */
            ConsultaBitacoraSabanaV2 jsonObject = new ConsultaBitacoraSabanaV2();
            jsonObject.bitacoraSabanas = new List<BitacoraSabanaV2>();

            foreach (prosu.BitacoraSabana item in tramites)
            {
                jsonObject.bitacoraSabanas.Add(new BitacoraSabanaV2()
                {
                    FechaRegistro = item.FechaRegistro.ToString(),
                    FechaInicio = item.FechaInicio.ToString(),
                    FechaFin = item.FechaFin.ToString(),
                    NumRegistros = item.NumRegistros.ToString(),
                    Usuario = item.Usuario.ToString(),
                    NumSolicitudes = item.NumSolicitudes.ToString(),
                });
            }

            return jsonObject;
        }
    }

    public class ConsultaV2
    {
        public string Orden { get; set; }
        public string IdTramite { get; set; }
        public string FechaRegistro { get; set; }
        public string NMESA { get; set; }
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
        public string EstadoMesa { get; set; }
        public string Observacion { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class ConsultasMesasV2
    {
        public List<ConsultaV2> consulta { get; set; }
    }

    public class BitacoraSabanaV2
    {
        public string FechaRegistro { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string NumRegistros { get; set; }
        public string Usuario { get; set; }
        public string NumSolicitudes { get; set; }
    }

    public class ConsultaBitacoraSabanaV2
    {
        public List<BitacoraSabanaV2> bitacoraSabanas { get; set; }
    }
}