<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="MapaGeneralDetalle.aspx.cs" Inherits="WFO.Procesos.Supervision.MapaGeneralDetalle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <asp:HiddenField ID="hfIdMesa" runat="server" Value="0" />
    <asp:HiddenField ID="hfIdFlujo" runat="server" Value="0" />

    <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <asp:Label ID="lblTitulo" runat="server" Text="" Font-Bold="True" ForeColor="#73879C" Font-Size="30px" Font-Italic="false"></asp:Label>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <p class="text-muted font-13 m-b-30">
                            <div class="row">
                                <br />
                                <asp:Repeater ID="MesaResumen" runat="server">
                                    <HeaderTemplate>
                                        <table id="tMesaResumen" class="table table-striped table-bordered jambo_table bulk_action">
                                            <thead>
                                                <tr>
                                                    <th style="text-align: center;">Mesa</th>
                                                    <th style="text-align: center;">Mesa</th>
                                                    <th style="text-align: center;">Usuarios Conectados</th>
                                                    <th style="text-align: center;">Trámites Nuevos</th>
                                                    <th style="text-align: center;">Trámites Reingresos</th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;">
                                                <img src="<%#Eval("Icono")%>" width="20%">
                                            </td>
                                            <td style="text-align: center; font-size:20px; "><%#Eval("Mesa")%></td>
                                            <td style="text-align: center; font-size:20px; "><%#Eval("UsuariosConectados")%></td>
                                            <td style="text-align: center; font-size:20px; "><%#Eval("TramitesDisponibles")%></td>
                                            <td style="text-align: center; font-size:20px; "><%#Eval("TramitesReingresos")%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>

                            <div class="row">
                            <br />
                            <hr />
                            <asp:Repeater ID="RepeaterFechas" runat="server" OnItemCommand="rptTramite_ItemCommand" Visible="false">
                                <HeaderTemplate>
                                    <table id="datatable" class="table table-striped table-bordered jambo_table bulk_action">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center;">Registro</th>
                                                <th style="text-align: center;">Folio</th>
                                                <th style="text-align: center;">Reingresos</th>
                                                <th style="text-align: center;">Status Mesa</th>
                                                <th style="text-align: center;">Usuario</th>
                                                <th style="text-align: center;">Tiempo de Atención</th>
                                                <th style="text-align: center;">Tiempo en Mesa</th>
                                                <th style="text-align: center;">Contratante</th>
                                                <th style="text-align: center;">Titular</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Registro")%><s/td>
                                        <td><%#Eval("Folio")%></td>
                                        <td><%#Eval("Reingresos")%></td>
                                        <td><%#Eval("statusMesa")%></td>
                                        <td><%#Eval("Usuario")%></td>
                                        <td><%#Eval("TiempoAtencion")%></td>
                                        <td><%#Eval("TiempoMesa")%></td>
                                        <td><%#Eval("Contratante")%></td>
                                        <td><%#Eval("Titular")%></td>
                                        <td><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/Imagenes/folder.png" CommandName ="Consultar" CommandArgument='<%# Eval("IdTramite")%>' /></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>

                        </p>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>