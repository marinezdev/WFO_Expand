<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteTramitesPorMesa.aspx.cs" Inherits="WFO.Procesos.SupervisionGeneral.ReporteTramitesPorMesa" MasterPageFile="~/Utilerias/Site.Master" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server" EnableViewState="true">
    
<style>
    th { font-size: 12px; font-weight: normal }
    th, td { padding: 5px; border:1px solid black }
    td a { text-decoration: underline; color: blue }
</style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Reporte de Trámites por Mesa</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        <div class="row">
                            <div class="col-md-4 col-sm-4 col-xs-12 form-group has-feedback">
                                <asp:DropDownList ID="cboFlujos" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="CargaFlujos_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cboFlujos" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <asp:Literal id="MesasLiteral" runat=server  text=""/>
                            </div>
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                 <hr />
                            </div>
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div  id="DatosConsulta" class="text-center">
                            </div>
                        </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>
            </div>
        </div>
    </div>


    <script>
        function MuestraTramites(estatus, mesa) {

            $.ajax({
                type: "POST",
                url: "ReporteTramitesPorMesa.aspx/BuscaTramites_Detalle",
                data: '{IdEstatus: ' + estatus + ', IdMesa:' + mesa + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: resultado,
                error: errores
            });
        }

        
        function resultado(data) {
            console.log(data);
            $('#DatosConsulta').html("");

            var tabla = "<table id='Infomacion' class='table table-striped table-bordered jambo_table bulk_action text-center' style='width:100%'>" +
                "<thead>" +
                "<tr>" +
                "<td>Fecha envío</td>" +
               /* "<td>Tipo trámite</td>" +*/
                "<td>Número de trámite</td>" +
                "<td>Orden de Trabajo</td>" +
                "<td>Operación</td>" +
                "<td>Producto </td>" +
                "<td>Información del Contratante</td>" +
                "<td>Número De Póliza De Los Sistemas Legados </td>" +
               /* "<td>KWIK </td>" +*/
                "</tr>" +
                "</thead>" +
                "<tbody>";
            for (var b = 0; b < data.d.tramiteConsultaDetalle.length; b++) {
                tabla += "<tr>" +
                    "<td>" + data.d.tramiteConsultaDetalle[b].FechaRegistro + "</td>" +
                    /*"<td>" + data.d.tramiteConsultaDetalle[b].TipoTramite + "</td>" +*/
                    "<td>" + data.d.tramiteConsultaDetalle[b].Folio + "</td>" +
                    "<td>" + data.d.tramiteConsultaDetalle[b].NumeroOrden + "</td>" +
                    "<td>" + data.d.tramiteConsultaDetalle[b].Operacion + "</td>" +
                    "<td>" + data.d.tramiteConsultaDetalle[b].Producto + "</td>" +
                    "<td>" + data.d.tramiteConsultaDetalle[b].Contratante + "</td>" +
                    "<td>" + data.d.tramiteConsultaDetalle[b].IdSisLegados + "</td>" +
                    /*"<td>" + data.d.tramiteConsultaDetalle[b].Kwik + "</td>" +*/
                    "</tr>";
            }
            tabla += "</tbody>" +
                "</table>";

            $('#DatosConsulta').html(tabla);

            $("#Infomacion").dataTable().fnDestroy();

            $('#Infomacion').DataTable({
                "order": [[0, "asc"]],
                'language': { 'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json' },
                dom: 'Blfrtip',
                buttons: [{ extend: 'copy', className: 'btn-sm' }, { extend: 'csv', className: 'btn-sm' }, { extend: 'excel', className: 'btn-sm' }, { extend: 'pdfHtml5', className: 'btn-sm' }, { extend: 'print', className: 'btn-sm' }]
            });
        }

        function errores(data) {
            //msg.responseText tiene el mensaje de error enviado por el servidor
            alert('Error: ' + msg.responseText);
        }
    </script>
</asp:Content>