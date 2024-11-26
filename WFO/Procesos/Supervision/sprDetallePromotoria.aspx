<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="sprDetallePromotoria.aspx.cs" Inherits="WFO.Procesos.Supervision.sprDetallePromotoria" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="text-center text-warning">DETALLE PROMOTORIA </div>
    <br /><br />
    <div class="container-fluid" id="contenedor">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS" EditFormat="Custom" Width="190" Caption="Desde:">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                    <br />
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <dx:ASPxDateEdit ID="CalHasta" runat="server" Theme="iOS" EditFormat="Custom" Width="190" Caption="Hasta">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                </div>
                <div class="col-md-6 col-sm-12 col-xs-12 text-xs-right">
                    <asp:Button ID="btnFiltro" runat="server" Text="Filtrar" CssClass="btn btn-success" />
                    <br /><br />
                </div>
                <div class="row">
                    <div class="col-md-12,col-sm-12 col-xs-12 text-right">
                        <asp:LinkButton ID="lnkExportar" runat="server" CausesValidation="False" OnClick="lnkExportar_Click"><img src="../../Imagenes/ExcelIcon.png" style="vertical-align:top"/>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:UpdateProgress ID="updProgress" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.8;">
                        <div style="padding: 10px; position: relative; top: 45%; left: 50%; background-color: white; width: 170px; height: 67px">
                            <table style="background-size: 0">
                                <tr>
                                    <td>
                                        <img alt=" " src="../../Imagenes/spinner.gif" />
                                    </td>
                                    <td>
                                        <span style="font-size: 16px">&nbsp;Procesando...</span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <asp:UpdatePanel ID="DetallePromotorias" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <dx:ASPxGridView ID="dvgdResumenPromotoria" KeyFieldName="idPromotoria" ClientInstanceName="dvgdResumenPromotoria" runat="server" AutoGenerateColumns="False" Width="100%" EnableTheming="True" Theme="iOS" Font-Size="10px">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="NUM. PROMOTORÍA" FieldName="idPromotoria" VisibleIndex="0" Width="150" Visible="false" />
                                <dx:GridViewDataTextColumn Caption="CVE. PROMOTORÍA" FieldName="PromotoriaClave" VisibleIndex="1" Width="150" />
                                <dx:GridViewDataTextColumn Caption="PROMOTORÍA" FieldName="Promotoria" VisibleIndex="2" Width="200">
                                    <CellStyle Wrap="True"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="REGISTRO" FieldName="Registro" VisibleIndex="3" Width="150" />
                                <dx:GridViewDataTextColumn Caption="PROCESO" FieldName="Proceso" VisibleIndex="4" />
                                <dx:GridViewDataTextColumn Caption="HOLD" FieldName="Hold" VisibleIndex="5" />
                                <dx:GridViewDataTextColumn Caption="RECHAZO" FieldName="Rechazo" VisibleIndex="6" />
                                <dx:GridViewDataTextColumn Caption="SUSPENDIDO" FieldName="Suspendido" VisibleIndex="7" Width="150" />
                                <dx:GridViewDataTextColumn Caption="EJECUCIÓN" FieldName="Ejecucion" VisibleIndex="8" />
                                <dx:GridViewDataTextColumn Caption="TOTAL" FieldName="Total" VisibleIndex="9" />
                            </Columns>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxLabel runat="server" Text='<%# Eval("Promotoria") %>' Font-Bold="true" />
                                    <br />
                                    <br />
                                    <dx:ASPxGridView ID="dvgdDetallePromotoria" runat="server" ClientInstanceName="dvgdResumenPromotoria" OnInit="dvgdDetallePromotoria_Init" KeyFieldName="idTramite" Width="95%" EnablePagingGestures="False" AutoGenerateColumns="False">
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="idTramite" Caption="TRAMITE" VisibleIndex="1" Visible="false" />
                                            <dx:GridViewDataColumn FieldName="FolioCompuesto" Caption="FOLIO" VisibleIndex="2" />
                                            <dx:GridViewDataDateColumn FieldName="FechaRegistro" Caption="FECHA REGISTRO" VisibleIndex="3" PropertiesDateEdit-DisplayFormatString="G" />
                                            <dx:GridViewDataDateColumn FieldName="FechaInicio" Caption="FECHA INICIO" VisibleIndex="4" PropertiesDateEdit-DisplayFormatString="G" />
                                            <dx:GridViewDataColumn FieldName="UsuarioNombre" Caption="USUARIO" VisibleIndex="5" />
                                            <dx:GridViewDataColumn FieldName="MesaNombre" Caption="MESA" VisibleIndex="6" />
                                            <dx:GridViewDataColumn FieldName="EstadoTramite" Caption="ESTADO" VisibleIndex="7" />
                                        </Columns>
                                        <Settings ShowFooter="True" />
                                        <SettingsPager EnableAdaptivity="true" />
                                        <SettingsExport ExcelExportMode="WYSIWYG"></SettingsExport>
                                        <Styles Header-Wrap="True" />
                                        <SettingsSearchPanel Visible="true" />
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" AllowEllipsisInText="true" />
                            <SettingsPager EnableAdaptivity="true" />
                            <SettingsDetail ShowDetailRow="true" />
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="600" />
                            <SettingsSearchPanel Visible="true" />
                            <Settings HorizontalScrollBarMode="Visible" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdDetallePromotoria"></dx:ASPxGridViewExporter>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFiltro" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
