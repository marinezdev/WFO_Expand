<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="ListadoTramitesOperacionN3.aspx.cs" Inherits="WFO.Procesos.SupervisionGeneral.ListadoTramitesOperacionN3" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <script>
        function carga() {
            $('#myModal').modal({backdrop: 'static', keyboard: false});
        }
        function retirar() {
            $('#myModal').modal('toggle'); 
        }
    </script>
    <asp:HiddenField ID="hfIdTramite" runat="server" Value="0" />

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
                    <h2>Listado de Trámites Operados [ N3 ]</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                            <table id="" class="table table-striped table-bordered ">
                                <thead>
                                    <tr>
                                        <th>Descarga reporte captura masiva </th>
                                    </tr>
                                </thead>
                                <tr>
                                    <td style="text-align:center">
                                        <asp:LinkButton ID="lnkExportarResumen" runat="server" Text="Exportar a Excel"  CausesValidation="False" OnClientClick="carga()" OnClick="lnkExportar_Click">
                                            <img src="../../Imagenes/images.png" style="vertical-align:top"/>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                            <table id="" class="table table-striped table-bordered ">
                                <thead>
                                    <tr>
                                        <th>Lista tramites incompletos </th>
                                    </tr>
                                </thead>
                                <tr>
                                    <td style="text-align:center;">
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Exportar a Excel"  CausesValidation="False" OnClientClick="carga()" OnClick="ListaTramitesIncompletos">
                                            <img src="../../Imagenes/post_NUEVAS_SOLIC.jpg" style="vertical-align:top"/>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    
                            <hr /> 
                    <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="upPnlTraMesa" runat="server" Visible="false">
                                <div class="row" style="text-align:center;">
                                    <h3><asp:Label runat="server" ID="Folio" Text ="Número Folio"></asp:Label></h3>
                                </div>
                        <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:Repeater ID="rptObservacionesMesa" runat="server" >
                                <HeaderTemplate>
                                    <table id="" class="table table-striped table-bordered">
                                        <thead>
                                            <tr>
                                                <th>Mesa</th>
                                                <th>Estatus Mesa</th>
                                                <th>Nombre operador</th>
                                                <th>Fecha Inicio</th>
                                                <th>Fecha Inicio</th>
                                                <th>Observaciones públicas </th>
                                                <th>Observaciones privadas </th>
                                            </tr>
                                        </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Mesa")%></td>
                                        <td><%#Eval("Stsatus")%></td>
                                        <td><%#Eval("Nombre")%></td>
                                        <td><%#Eval("FechaInicio")%></td>
                                        <td><%#Eval("FechaFin")%></td>
                                        <td><%#Eval("ObservacionPublica")%></td>
                                        <td><%#Eval("ObservacionPrivada")%></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="row">
                        <div class="row control-label col-md-4 col-sm-6 col-xs-12 text-center">
                            <asp:Label runat="server" ID="label1" Text="Número De Póliza De Los Sistemas Legados" Font-Bold="True" class="control-label"></asp:Label>
                            <asp:HiddenField ID="HiddenField3" runat="server" />
                            <asp:TextBox ID="TextNumPolizaSisLegado" runat="server" MaxLength="20" AutoPostBack="false" class="form-control"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterMode="ValidChars" TargetControlID="TextNumPolizaSisLegado" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ@.=$%*_0123456789-" />
                        </div>

                        <div class="row control-label col-md-4 col-sm-6 col-xs-12 text-center">
                            <br />
                            <asp:Button ID="btnReiniciarTramite" Visible="false" runat="server" Text="Reiniciar tramite Admisión" class="btn btn-success" OnClientClick="carga()" OnClick="ListaTramitesIncompletos"/>
                            <br />
                            <code><asp:Label ID="LabelRespuestaReinicioTramite" runat="server" Text ="" Visible="true"></asp:Label></code>
                        </div>
                    </div>
                    <hr />
                    </asp:Panel>
                    
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <p>Listado de trámites  con registro en ejecución, sin importar el estatus del trámite o estatus de mesa</p>
                            <asp:Repeater ID="rptListadoTramites" runat="server" OnItemCommand="rptTramite_ItemCommand" >
                                <HeaderTemplate>
                                    <table id="datatable" class="table table-striped table-bordered">
                                        <thead>
                                            <tr>
                                                <th>Número de Folio</th>
                                                <th>Número de póliza</th>
                                                <th>Contratante</th>
                                                <th>Mesa</th>
                                                <th>Estatus mesa</th>
                                                <th>Estatus trámite</th>
                                                <th>Observaciones</th>
                                            </tr>
                                        </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Folio")%></td>
                                        <td><%#Eval("NumeroPoliza")%></td>
                                        <td><strong>Nombre: </strong><%#Eval("Contratante")%> <br /><strong>RFC: </strong><%#Eval("RFC")%><br /><%#Eval("Titular")%></td>
                                        <td><%#Eval("Mesa")%></td>
                                        <td><%#Eval("EstatusMesa")%></td>
                                        <td><%#Eval("EstatusTramite")%></td>
                                        <td style="text-align:center"><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/Imagenes/note-2389227_960_720.png" CommandName ="Consultar" CommandArgument='<%# Eval("Id")%>' /></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    
                    
                  

                        </ContentTemplate>
                    </asp:UpdatePanel>
                                   
                </div>
            </div>
        </div>
    </div> 
</asp:Content>