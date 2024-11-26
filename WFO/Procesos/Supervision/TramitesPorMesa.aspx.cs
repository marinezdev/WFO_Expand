using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Export;
using DevExpress.XtraPrinting;

namespace WFO.Procesos.Supervision
{
    public partial class TramitesPorMesa : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                statustramite.StatusTramite_DropDownList(ref DDLStatusTramite);
                mesas.Mesas_DropDownList(ref DDLMesa);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            gvTramitesPorMesa.Visible = true;
            tramitespormesa.ObtenerTramitesPorMesaLlenar(ref gvTramitesPorMesa,
                Funciones.Fechas.PrepararFechaInicialParaConsulta(dtFechaInicio.Text),
                Funciones.Fechas.PrepararFechaFinalParaConsulta(dtFechaFin.Text), DDLStatusTramite.SelectedValue, DDLMesa.SelectedValue);
        }

        protected void lnkExportarResumen_Click(object sender, EventArgs e)
        {
            gvTramitesPorMesa.ExportXlsxToResponse("ASAEConsultores.xlsx", new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }
    }
}