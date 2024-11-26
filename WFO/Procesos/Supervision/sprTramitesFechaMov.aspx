<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="sprTramitesFechaMov.aspx.cs" Inherits="WFO.Procesos.Supervision.sprTramitesFechaMov" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="text-warning text-center">TRÁMITES POR FECHA DE MOVIMIENTO</div>
    <br />
    <br />
    <div class="container-fluid" id="contenedor">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-md-3 col-sm-4 col-xs-12">
                    <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS" EditFormat="Custom" EditFormatString="f" Width="190" Caption="Desde:">
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
                    <dx:ASPxDateEdit ID="CalHasta" runat="server" Theme="iOS" EditFormat="Custom" EditFormatString="f" Width="190" Caption="Hasta:">
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
                    <asp:Button ID="btnFiltrar" CssClass="btn btn-success" runat="server" Text="Filtrar" ClientIDMode="Static" />
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12,col-sm-12 col-xs-12 text-right">
                    <asp:LinkButton ID="lnkExportar" runat="server" CausesValidation="False" OnClick="lnkExportar_Click">
                   <img src="../../Imagenes/ExcelIcon.png" style="vertical-align:top"/>
                    </asp:LinkButton>
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
                        <br />
                        <dx:ASPxGridView ID="dvgdEstatusTramite" ClientInstanceName="dvgdEstatusTramite" runat="server" AutoGenerateColumns="False" Width="100%" Style="margin-top: 0px" EnableTheming="True" Theme="iOS" Font-Size="10px">
                            <Columns>
                                <dx:GridViewDataDateColumn Caption="FECHA" FieldName="FECHA" VisibleIndex="0">
                                      <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn Caption="CADUCADO" FieldName="CADUCADO" VisibleIndex="1" Width="150"/>
                                <dx:GridViewDataTextColumn Caption="CANCELADO" FieldName="CANCELADO" VisibleIndex="2" Width="150" />
                                <dx:GridViewDataTextColumn Caption="EJECUCION" FieldName="EJECUCION" VisibleIndex="3" />
                                <dx:GridViewDataTextColumn Caption="HOLD" FieldName="HOLD" VisibleIndex="4" />
                                <dx:GridViewDataTextColumn Caption="PCI" FieldName="PCI" VisibleIndex="5" />
                                <dx:GridViewDataTextColumn Caption="PROCESO" FieldName="PROCESO" VisibleIndex="6" />
                                <dx:GridViewDataTextColumn Caption="PROMOTORIA CANCELA" FieldName="PROMOTORIA CANCELA" VisibleIndex="7" Width="150"/>
                                <dx:GridViewDataTextColumn Caption="RECHAZO" FieldName="RECHAZO" VisibleIndex="8" />
                                <dx:GridViewDataTextColumn Caption="REGISTRO" FieldName="REGISTRO" VisibleIndex="9" />
                                <dx:GridViewDataTextColumn Caption="REVISIÓN PROMOTORIA" FieldName="REVISIÓN PROMOTORIA" VisibleIndex="10" Width="150" />
                                <dx:GridViewDataTextColumn Caption="SUSPENDIDO" FieldName="SUSPENDIDO" VisibleIndex="11"  Width="150"/>
                                <dx:GridViewDataTextColumn Caption="TOTALES" FieldName="TOTALES" VisibleIndex="12" />
                            </Columns>
                            <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" AllowEllipsisInText="true" />
                            <SettingsPager EnableAdaptivity="true" />
                            <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
                            <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" VerticalScrollBarMode="Visible" VerticalScrollableHeight="450"  HorizontalScrollBarMode="Visible"/>
                            <SettingsSearchPanel Visible="true" />
                            <Styles Header-Wrap="True"/>
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
