using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using promotoria = WFO.Propiedades.Procesos.Promotoria;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Procesos.Operador
{
    public partial class BuscarTramite2 : Utilerias.Comun
    {
        WFO.Negocio.Procesos.Operacion.Tramites tramites = new Negocio.Procesos.Operacion.Tramites();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];

            }
        }

        protected void BtnConsultar_Click(object sender, EventArgs e)
        {
            string ListadoTramites = "";
            // NUEVOS PARAMETROS DE BUSQUEDA
            string Folio = TextFolio.Text.ToString().Trim();
            string RFC = TextRFC.Text.ToString().Trim();
            string Nombre = txNombre.Text.ToString().Trim();
            string ApPaterno = txApPat.Text.ToString().Trim();
            string ApMaterno = txApMat.Text.ToString().Trim();

            Mensajes.Text = "";

            manejo_sesion = (WFO.IU.ManejadorSesion)Session["Sesion"];
            List<prop.Tramites> Tramites = tramites.TramiteOperadorSelecionarBusqueda(manejo_sesion.Usuarios.IdUsuario, Folio, RFC, Nombre, ApPaterno, ApMaterno);

            ListadoTramites += "<div class='table-responsive'>" +
                                "<table  id='example' class='table table-striped table-bordered jambo_table bulk_action' style='width:100%'>" +
                                    "<thead>" +
                                        "<tr>" +
                                            "<th>Fecha envío</th>" +
                                            "<th>Número de trámite</th>" +
                                            "<th>Operación</th>" +
                                            "<th>Información del Contratante</th>" +
                                            "<th>Estado</th>" +
                                            "<th></th>" +
                                        "</tr>" +
                                    "</thead>" +
                                    "<tbody>";

            foreach (var ds in Tramites)
            {
                ListadoTramites += "<tr>" +
                                    "<td class=' '>" + ds.FechaRegistro + "</td>" +
                                    "<td class=' '>" + ds.FolioCompuesto + "</td>" +
                                    "<td class=' '>" + ds.Operacion + "</td>" +
                                    "<td class=' '><strong>Nombre: </strong>" + ds.Contratante + " </td>" +
                                    "<td class=' '>" + ds.Estatus + "</td>" +
                                    "<td class=' '><img src='../../Imagenes/folder.png' OnClick='Consultar(" + ds.IdTramite + ")' /></td>" +
                            "</tr>";

            }

            ListadoTramites += "</tbody>" +
                "</table>" +
            "</div>";


            //RepeaterFechas.DataSource = Tramites;
            //RepeaterFechas.DataBind();
            //RepeaterFechas.Visible = true;


            ListBusqueda.Text = ListadoTramites;

            string script2 = "";
            script2 = "$('#example').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '300px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); retirar();";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);

        }


        [WebMethod]
        public static prop.Tramites DetalleIdTramite(int Id)
        {
            prop.Tramites tramiteProcesado = new prop.Tramites();

            WFO.Negocio.Procesos.Operacion.Tramites tramites = new Negocio.Procesos.Operacion.Tramites();
            tramiteProcesado = tramites.Tramite_Selecionar_Id(Id);

            return tramiteProcesado;
        }

        [WebMethod]
        public static List<promotoria.bitacora> DetalleBitacora(int Id)
        {
            WFO.Negocio.Procesos.Promotoria.Bitacora bitacora = new Negocio.Procesos.Promotoria.Bitacora();
            List<promotoria.bitacora> bitacoras = bitacora.Bitacora_Seleccionar_IdTramite(Id);
            return bitacoras;
        }

        [WebMethod]
        public static string DetalleExpediente(int Id, int IdTipo)
        {
            //WFO.Negocio.Procesos.Promotoria.Archivos _archivo = new Negocio.Procesos.Promotoria.Archivos();
            //List<promotoria.expediente> expediente = _archivo.ConsultaExpediente(Id, IdTipo);

            //string strDoctoWeb = "";
            string result = "";
            //strDoctoWeb = "..\\..\\ArchivosDefinitivos\\404.pdf";

            //if (expediente.Count > 0)
            //{
            //    foreach (promotoria.expediente oArchivo in expediente)
            //    {
            //        if (!string.IsNullOrEmpty(oArchivo.NmArchivo))
            //        {
            //            strDoctoWeb = "..\\..\\DocsUp\\" + oArchivo.NmArchivo;
            //        }
            //        else
            //        {
            //            // AGREGAR ARCHIVO NO ENCONTRADO
            //            strDoctoWeb = "..\\..\\ArchivosDefinitivos\\404.pdf";
            //        }
            //    }
            //}

            string parametros = "IdTramite=" + Id + ",IdTipoTramite=" + IdTipo;
            string Encrypt = Negocio.Sistema.UrlCifrardo.Encrypt(parametros);
            string url = "Displaypdf.aspx" + "?data=" + Encrypt;
            result = "<iframe src='" + url + "' style='width:100%; height:450px' style='border: none;'></iframe>";
            //result = "<iframe src='" + strDoctoWeb + "' style='width:100%; height:450px' style='border: none;'></iframe>";
            return result;
        }



        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            // Inicialización de Variables
            //RepeaterFechas.Visible = false;

            TextFolio.Text = "";
            TextRFC.Text = "";
            txNombre.Text = "";
            txApPat.Text = "";
            txApMat.Text = "";
            Mensajes.Text = "";

            ListBusqueda.Text = "";

        }

        
    }
}