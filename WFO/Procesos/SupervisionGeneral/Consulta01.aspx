<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consulta01.aspx.cs" Inherits="WFO.Procesos.SupervisionGeneral.Consulta01" MasterPageFile="~/Utilerias/Site.Master" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server" EnableViewState="true">

    <script>
        function carga() {
            $('#myModal').modal({backdrop: 'static', keyboard: false});
        }
        function retirar() {
            $('#myModal').modal('toggle'); 
        }
    </script>

    <!-- MODAL DE  OPERACIONES -->
    <div class="modal fade bs-example-modal-sm" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel2">
                        <asp:label ID="TituloModal" runat="server" Text="Cargando ... ">
                        </asp:label>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="image view view-first">
                        <img style="width: 100%; display: block;" src="../../Imagenes/default-loader.gif" alt="image">
                    </div>
                </div>
                <div class="modal-footer">
                        
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Captura Masiva</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                        <table id="" class="table table-striped table-bordered ">
                            <thead>
                                <tr>
                                    <th>Descarga reporte captura masiva </th>
                                </tr>
                            </thead>
                            <tr>
                                <td style="text-align:center">
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Exportar a Excel"  CausesValidation="False" OnClientClick="carga()" OnClick="lnkExportar_Click">
                                        <img src="../../Imagenes/images.png" style="vertical-align:top"/>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--  
                    <asp:LinkButton ID="lnkExportarResumen" runat="server" Text="Exportar a Excel"  CausesValidation="False" OnClick="lnkExportar_Click">
                        <img src="../../Imagenes/ExcelIcon.png" style="vertical-align:top"/>
                    </asp:LinkButton>
                    --%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
