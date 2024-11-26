<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="MisTramitesPendientes.aspx.cs" Inherits="WFO.Procesos.Promotoria.MisTramitesPendientes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Mis trámites </h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <p class="text-muted font-13 m-b-30">
                        Listado total de trámites registrados en el rango de 4 meses, a partir de la fecha del día de hoy. <asp:Label runat="server" ID="LabRespyuesta" Text=""></asp:Label>
                    </p>
                    <asp:Repeater ID="rptTramite" runat="server" OnItemCommand="rptTramite_ItemCommand">
                        <HeaderTemplate>
                            <table id="datatable-buttons" class="table table-striped table-bordered jambo_table bulk_action" style='width:100%'>
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>Fecha de envio</th>
                                        <th>Folio del Tramite</th>
                                        <th>Tipo Flujo</th>
                                        <th>Tipo Servicio</th>
                                        <th>Contratante</th>
                                        <th>Fecha Firma Solicitud</th>
                                        <th>Status Tramite</th>
                                        <th>Numero de Orden</th>
                                        <th>Numero de Poliza</th>
                                        <th>Clave Promotoria</th>
                                        <th>GA</th>
                                        <th>GU</th>
                                        <th>Clave Zona</th>
                                        <th>Prima Total</th>
                                        <th>Prima Excedente</th>
                                        <th>Prima Diferencia</th>
                                        <th>Motivo Suspension / Rechazo</th>
                                        <th>Observaciones Publicas</th>
                                        <th></th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><asp:ImageButton ID="imbtnConsultar2" runat="server" ImageUrl="~/Imagenes/folder.png" CausesValidation="false" CommandName ="Consultar" CommandArgument='<%# Eval("IdTramite")%>' /></td>
                                <td><%#Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                <td><%#Eval("Folio")%></td>
                                <td><%#Eval("TipoFlujo")%></td>
                                <td><%#Eval("TipoServicio")%></td>
                                <td><%#Eval("Contratante")%></td>
                                <td><%#Eval("FechaFirmaSolicitud","{0:dd/MM/yyyy}")%></td>
                                <td><%#Eval("StatusTramite")%></td>
                                <td><%#Eval("NumeroOrden")%></td>
                                <td><%#Eval("NumeroPoliza")%></td>
                                <td><%#Eval("PromotoriaClave")%></td>
                                <td><%#Eval("GA")%></td>
                                <td><%#Eval("GU")%></td>
                                <td><%#Eval("PromotoriaZonaClave")%></td>
                                <td><%#Eval("PrimaTotal")%></td>
                                <td><%#Eval("PrimaExcedente")%></td>
                                <td><%#Eval("PrimaDiferencia")%></td>
                                <td><%#Eval("SuspensionMotivos")%></td>
                                <td><%#Eval("ObservacionesPublicas")%></td>
                                <td><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/Imagenes/folder.png" CausesValidation="false" CommandName ="Consultar" CommandArgument='<%# Eval("IdTramite")%>' /></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>



    </div>
</asp:Content>

