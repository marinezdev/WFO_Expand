using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using captura = WFO.Propiedades.Procesos.Operacion.Captura;

namespace WFO.Procesos.Operador
{
    public partial class ConsultaCaptura : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Operacion.CapturaMasiva.Asegurados asegurados = new Negocio.Procesos.Operacion.CapturaMasiva.Asegurados();
        WFO.Negocio.Procesos.Operacion.CapturaMasiva.CoAsegurados coAsegurados = new Negocio.Procesos.Operacion.CapturaMasiva.CoAsegurados();
        WFO.Negocio.Procesos.Operacion.CapturaMasiva.Tarjetas tarjetas = new Negocio.Procesos.Operacion.CapturaMasiva.Tarjetas();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
                hfIdTramite.Value = Request.QueryString["Id"].ToString();

            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.Parse(hfIdTramite.Value) > 0)
            {
                if (!IsPostBack)
                {
                    PostBack(int.Parse(hfIdTramite.Value));
                }
            }
        }

        private void PostBack(int pIdTramite)
        {
            // MUESTRA INFORMACION DEL TRÁMITE 
            
            PintaDatosTramite(pIdTramite);
        }

        private void PintaDatosTramite(int pIdTramite)
        {
            captura.Asegurados asegurdo = asegurados.Asegurado_Seleccionar_IdTRamite(pIdTramite);
            
            if (!string.IsNullOrEmpty(asegurdo.RFC))
            {
                LabelFechaFirmasolicitud.Text = asegurdo.FechaFirmaSolicitud;
                LabelSucursal.Text = asegurdo.EstadoFirma;
                LabelUMAN.Text = asegurdo.UMAN;
                LabelClaveAgente.Text = asegurdo.Clave_agente;
                LabelAPaterno.Text = asegurdo.APaterno;
                LabelAMaterno.Text = asegurdo.AMaterno;
                LabelNombre.Text = asegurdo.Nombre;
                LabelFechaNacimiento.Text = asegurdo.FechaNacimiento;
                LabelAntiguedad.Text = asegurdo.Antiguedad;
                LabelTelefono.Text = asegurdo.Telefono;
                LabelEmail.Text = asegurdo.Email;

                captura.AseguradoCaptura captura = asegurados.Asegurado_Captura_Seleccionar_IdTramite(pIdTramite);
                LabelPlanMedicaLife.Text = captura.Plan;
                LabelDeducibleEnPesos.Text = captura.Deducible;
                LabelCausaSeguro.Text = captura.CausaSeguro;
                LabelRegion.Text = captura.Riesgo;
                LabelTipoProducto.Text = captura.TipoProducto;

                CargaCoasegurados(pIdTramite, asegurdo.RFC);
                CargaTarjetas(pIdTramite);
            }
        }


        protected void CargaCoasegurados(int pIdTramite, string RFC)
        {
            int IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            List<captura.Coasegurados> coasegurados = coAsegurados.CoAsegurados_Selecionar_PorIdTramite_RFC(IdTramite, RFC);
            RepeterCoasegurados.DataSource = coasegurados;
            RepeterCoasegurados.DataBind();
        }

        protected void CargaTarjetas(int IdTramite)
        {
            // CARGA REGISTRO DE TARJETAS
            List<captura.Tarjetas> TotalTarjetas = tarjetas.Tarjetas_Selecionar_PorIdTramite(IdTramite);

            RepeterTarjetasMesaCaptura.DataSource = TotalTarjetas;
            RepeterTarjetasMesaCaptura.DataBind();
        }
    }
}