<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="sprReporteCaducados.aspx.cs" Inherits="WFO.Procesos.Supervision.sprReporteCaducados" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="text-center text-warning">TRÁMITES CADUCADOS</div>
    <br />
    <br />
    <div class="container-fluid">
        <div class="form-horizontal">

            <div class="form-group">
                <div class="col-md-3 col-sm-4 col-xs-12">
                    <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS" EditFormat="Custom" Width="190" Caption="Desde">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                    <br />
                </div>
                <div class="col-md-3 col-sm-4 col-xs-12">
                    <dx:ASPxDateEdit ID="CalHasta" runat="server" Theme="iOS" EditFormat="Custom" Width="190" Caption="Hasta">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                    <br />
                </div>
                <label for="cmbFlujo" class="col-md-1 col-sm-1 col-xs-12 control-label">Flujo:</label>
                <div class="col-md-3 col-sm-3 col-xs-12">
                    <asp:DropDownList runat="server" ID="cmbFlujo" CssClass="form-control">
                    </asp:DropDownList>
                    <br />
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 text-xs-right">
                    <asp:Button ID="btnFiltrar" CssClass="btn btn-success" runat="server" Text="Filtrar" />
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
        <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <dx:ASPxGridView ID="dvgdEstatusTramite" ClientInstanceName="dvgdEstatusTramite" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" Width="100%" Style="margin-top: 0px" EnableTheming="True" Theme="iOS" Font-Size="10px">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="IdTramite" FieldName="Id" VisibleIndex="0" Visible="false" />
                                <dx:GridViewDataDateColumn Caption="FECHA INGRESO" FieldName="FechaIngreso" VisibleIndex="1" Width="150">
                                    <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn Caption="FOLIO" FieldName="FolioTramite" VisibleIndex="2" Width="150" />
                                <dx:GridViewDataTextColumn Caption="RAMO" FieldName="Ramo" VisibleIndex="3" Width="150" />
                                <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="Producto" VisibleIndex="4" Width="150" />
                                <dx:GridViewDataTextColumn Caption="ESTATUS" FieldName="Estatus" VisibleIndex="5" Visible="false" Width="150" />
                                <dx:GridViewDataDateColumn Caption="FECHA ESTATUS" FieldName="FechaEstatus" VisibleIndex="6" Width="150">
                                    <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn Caption="TIEMPO (HORAS)" FieldName="Tiempo" VisibleIndex="7" Width="150" />
                                <dx:GridViewDataTextColumn Caption="PROMOTORIA" FieldName="Promotoria" VisibleIndex="8" Width="150" />
                                <dx:GridViewDataTextColumn Caption="CLAVE AGENTE" FieldName="ClaveAgente" VisibleIndex="9" Width="150" />
                                <dx:GridViewDataTextColumn Caption="POLIZA" FieldName="Poliza" VisibleIndex="10" Width="150" />
                                <dx:GridViewDataTextColumn Caption="IDENTIFICADOR" FieldName="prioridad" VisibleIndex="11" Width="150" />
                            </Columns>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxLabel runat="server" Text='<%# Eval("FolioTramite") %>' Font-Bold="true" />
                                    <br />
                                    <br />
                                    <dx:ASPxGridView ID="dvgdDetalleCaducados" runat="server" ClientInstanceName="dvgdEstatusTramite" OnInit="dvgdDetalleCaducados_Init" KeyFieldName="Id" Width="100%" EnablePagingGestures="False" AutoGenerateColumns="False">
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="IdTramite" Caption="TRAMITE" VisibleIndex="1" Visible="false" />
                                            <dx:GridViewDataColumn FieldName="MesaNombre" Caption="MESA" VisibleIndex="3" />
                                            <dx:GridViewDataColumn FieldName="UsuarioNombre" Caption="USUARIO" VisibleIndex="4" />
                                            <dx:GridViewDataDateColumn FieldName="FechaInicio" Caption="FECHA INICIO" VisibleIndex="5" PropertiesDateEdit-DisplayFormatString="G" />
                                            <dx:GridViewDataDateColumn FieldName="FechaTermino" Caption="FECHA TERMINO" VisibleIndex="6" PropertiesDateEdit-DisplayFormatString="G" />
                                        </Columns>
                                        <Settings ShowFooter="True" />
                                        <SettingsPager EnableAdaptivity="true" />
                                        <SettingsExport ExcelExportMode="WYSIWYG"></SettingsExport>
                                        <Styles Header-Wrap="True" />
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <Settings ShowFooter="True" VerticalScrollableHeight="600"  ShowGroupFooter="VisibleAlways" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" />
                            <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                            <SettingsPager EnableAdaptivity="true" />
                            <SettingsDetail ShowDetailRow="true" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdEstatusTramite"></dx:ASPxGridViewExporter>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFiltrar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
