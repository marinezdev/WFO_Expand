using System;
using System.Drawing;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.Operacion;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using prosu = WFO.Propiedades.Procesos.SupervisionGeneral;
using System.Web.UI;

namespace WFO.Procesos.SupervisionGeneral
{
    public partial class ReporteTramitesPorMesa : Utilerias.Comun
    {
        
        WFO.Negocio.Procesos.Operacion.UsuariosFlujo usuariosFlujo = new Negocio.Procesos.Operacion.UsuariosFlujo();

        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (IU.ManejadorSesion)Session["Sesion"];

            if (!IsPostBack)
            {
                CargaFlujos(manejo_sesion.Usuarios.IdUsuario);
            }
        }

        protected void CargaFlujos(int Id)
        {
            List<prop.UsuariosFlujo> Flujos = usuariosFlujo.SelecionarFlujo(Id);
            cboFlujos.DataSource = Flujos;
            cboFlujos.DataBind();
            cboFlujos.DataTextField = "Nombre";
            cboFlujos.DataValueField = "Id";
            cboFlujos.DataBind();
        }

        protected void CargaFlujos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdFlujo = Convert.ToInt32(cboFlujos.SelectedValue.ToString());

            string Tabla = "<table Id='datatableAsignar' class='table table-striped table-bordered jambo_table bulk_action text-center'  style='width:100%'>";
            DataTable Tramites =  supervisiongeneraltramites.Tramite_PorMesas(IdFlujo);

            if (Tramites.Rows.Count > 0)
            {
                int Num = 1;
                // CABECERA DE TABLA
                Tabla = Tabla + "<thead class='thead -dark'>";
                Tabla = Tabla + "<tr>";
                foreach (DataColumn column in Tramites.Columns)
                {
                    if (Num == 0)
                    {
                        Tabla = Tabla + "<th scope='col'>";
                        Tabla = Tabla + column.ColumnName;
                        Tabla = Tabla + "</th>";
                        Num = 1;
                    }
                    else
                    {
                        Num = 0;
                    }
                }
                Tabla = Tabla + "<th>";
                Tabla = Tabla +     "TOTAL";
                Tabla = Tabla + "</th>";
                Tabla = Tabla + "</tr>";
                Tabla = Tabla + "</thead>";

                //FILAS 
                for (int i = 0; i < Tramites.Rows.Count; i++)
                {
                    Tabla = Tabla + "<tr>";

                    Tabla = Tabla + "<td>";
                    Tabla = Tabla + Tramites.Rows[i]["ESTATUS"].ToString();
                    Tabla = Tabla + "</td>";

                    for (int c = 3; c < Tramites.Columns.Count;)
                    {
                        if (Tramites.Rows[i]["IdEstatus"].ToString() == "0")
                        {
                            Tabla = Tabla + "<td>";
                        }
                        else
                        {
                            Tabla = Tabla + "<td onclick='MuestraTramites(" + Tramites.Rows[i]["IdEstatus"].ToString() + "," + Tramites.Rows[i][c - 1].ToString() + ")' style='cursor: pointer'>";
                        }
                        Tabla = Tabla + "<label >" + Tramites.Rows[i][c].ToString()+ "</label>";
                        Tabla = Tabla + "</td>";

                        c = c + 2;
                    }

                    Tabla = Tabla + "<td>";
                    Tabla = Tabla + "<label >" + Tramites.Rows[i]["TOTAL"].ToString() + "</label>";
                    Tabla = Tabla + "</td>";

                    Tabla = Tabla + "</tr>";
                }
            }

            Tabla = Tabla + "</table>";
            MesasLiteral.Text = Tabla;

            string script = "";
            script = "$('#datatableAsignar').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); ";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

        }


        [WebMethod]
        public static ConsultasTramitesStatus BuscaTramites_Detalle(int IdEstatus, int IdMesa)
        {
            WFO.Negocio.Procesos.SupervisionGeneral.Tramite supervisionGeneral = new Negocio.Procesos.SupervisionGeneral.Tramite();
            List<prosu.TramiteDetalle> tramites = supervisionGeneral.Tramite_PorMesas_Detalle(IdMesa, IdEstatus);

            /* LLENAR JSON PARA RETORNAR */
            ConsultasTramitesStatus jsonObject = new ConsultasTramitesStatus();
            jsonObject.tramiteConsultaDetalle = new List<TramiteConsultaDetalle>();

            foreach (prosu.TramiteDetalle item in tramites)
            {
                jsonObject.tramiteConsultaDetalle.Add(new TramiteConsultaDetalle()
                {
                    Folio = item.Folio.ToString(),
                    FechaRegistro = item.FechaRegistro.ToString(),
                    NumeroOrden = item.NumeroOrden.ToString(),
                    Operacion = item.Operacion.ToString(),
                    Producto = item.Producto.ToString(),
                    Contratante = item.Contratante.ToString(),
                    //RFC = item.RFC.ToString(),
                    //Titular = item.Titular.ToString(),
                    FechaSolicitud = item.FechaSolicitud.ToString(),
                    IdSisLegados = item.IdSisLegados.ToString(),
                    //Kwik = item.Kwik.ToString(),
                    //TipoTramite = item.Institucion.ToString(),
                });
            }

            return jsonObject;
        }
    }

    public class TramiteConsultaDetalle
    {
        public string Folio { get; set; }
        public string FechaRegistro { get; set; }
        public string NumeroOrden { get; set; }
        public string Operacion { get; set; }
        public string Producto { get; set; }
        public string Contratante { get; set; }
        public string RFC { get; set; }
        public string Titular { get; set; }
        public string FechaSolicitud { get; set; }
        public string IdSisLegados { get; set; }
        public string Kwik { get; set; }
        public string TipoTramite { get; set; }
    }

    public class ConsultasTramitesStatus
    {
        public List<TramiteConsultaDetalle> tramiteConsultaDetalle { get; set; }
    }
}