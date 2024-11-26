<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="sprReporteGeneralTotales.aspx.cs" Inherits="WFO.Procesos.Supervision.sprReporteGeneralTotales" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="text-warning text-center">REPORTE GENERAL DE TOTALES</div>
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
                    <br />
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
                        <dx:ASPxGridView ID="dvgdTotales" ClientInstanceName="dvgdTotales" runat="server" AutoGenerateColumns="False" Width="100%" Style="margin-top: 0px" EnableTheming="True" Theme="iOS" Font-Size="10px">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="DESCRIPCIÓN" FieldName="Descripcion" VisibleIndex="1" />
                                <dx:GridViewDataTextColumn Caption="TOTALES" FieldName="Totales" VisibleIndex="2" />
                                <dx:GridViewDataTextColumn Caption="%" FieldName="Porcentaje" VisibleIndex="3" />
                            </Columns>
                            <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" />
                            <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                            <SettingsPager Mode="ShowAllRecords" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdTotales"></dx:ASPxGridViewExporter>
                        <br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div style="display: flex; justify-content: center">
                            <dx:WebChartControl ID="dxChtTotales" ClientInstanceName="chart" runat="server" CrosshairEnabled="True" Height="300px" Width="800px" Theme="SoftOrange">
                                <Titles>
                                    <dx:ChartTitle Font="Arial,12pt" Text="PORCENTAJES OPERACION" />
                                </Titles>
                                <DiagramSerializable>
                                    <dx:XYDiagram>
                                        <AxisX VisibleInPanesSerializable="-1">
                                        </AxisX>
                                        <AxisY VisibleInPanesSerializable="-1">
                                        </AxisY>
                                    </dx:XYDiagram>
                                </DiagramSerializable>
                                <Legend Name="Default Legend" Visibility="False"></Legend>
                                <SeriesSerializable>
                                    <dx:Series Name="dxSreTotales"></dx:Series>
                                </SeriesSerializable>
                            </dx:WebChartControl>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:HiddenField ClientIDMode="Static" ID="Ancho" runat="server" Value="400" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFiltrar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <!--<asp:LinkButton ID="lnkControl" runat="server" CausesValidation="False" OnClick="lnkControl_Click">Operativo</asp:LinkButton>-->
</asp:Content>
