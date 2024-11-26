<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="sprReporteGeneralTop10.aspx.cs" Inherits="WFO.Procesos.Supervision.sprReporteGeneralTop10" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="text-warning text-center">TOP 10 </div>
    <br />
    <br />
    <div class="container-fluid" id="contenedor">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-md-3 col-sm-4 col-xs-12">
                    <dx:ASPxDateEdit ID="CalDesdeTE" runat="server" Theme="iOS" EditFormat="Custom" Width="190" Caption="Desde">
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
                    <dx:ASPxDateEdit ID="CalHastaTE" runat="server" Theme="iOS" EditFormat="Custom" Width="190" Caption="Hasta">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                </div>
                <label for="cmbFlujoTE" class="col-md-1 col-sm-1 col-xs-12 control-label">Flujo:</label>
                <div class="col-md-3 col-sm-3 col-xs-12">
                    <asp:DropDownList runat="server" ID="cmbFlujoTE" CssClass="form-control">
                    </asp:DropDownList>
                    <br />
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 text-xs-right">
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-success" ClientIDMode="Static" />
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
        <asp:UpdatePanel ID="UPTopProm" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <dx:ASPxGridView ID="dvgdPromotorias" ClientInstanceName="dvgdPromotorias" runat="server" AutoGenerateColumns="False" Width="100%" Style="margin-top: 0px" EnableTheming="True" Theme="iOS" Font-Size="10px">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="CVE PROMOTORIA" FieldName="Promotoria" VisibleIndex="1" Width="400" />
                                <dx:GridViewDataTextColumn Caption="PROMOTORIA" FieldName="Nombre" VisibleIndex="2" Width="400" />
                                <dx:GridViewDataTextColumn Caption="ZONA" FieldName="Zona" VisibleIndex="3" Width="400" />
                                <dx:GridViewDataTextColumn Caption="EJECUTADOS" FieldName="NumTramitesEje" VisibleIndex="4" Width="400" />
                                <dx:GridViewDataTextColumn Caption="SUSPENDIDOS" FieldName="NumTramitesSus" VisibleIndex="5" Width="400" />
                            </Columns>
                            <Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto" />
                            <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <SettingsSearchPanel Visible="true" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdPromotorias"></dx:ASPxGridViewExporter>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div style="display: flex; justify-content: center">
                            <dx:WebChartControl ID="dxChtTotales" runat="server" CrosshairEnabled="True" Height="300px" Width="600px" Theme="SoftOrange">
                                <Titles>
                                    <dx:ChartTitle Font="Arial,12pt" Text="TOP 10 EJECUTADOS" />
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
                        <br />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFiltrar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-md-3 col-sm-4 col-xs-12">
                    <dx:ASPxDateEdit ID="CalDesdeTS" runat="server" Theme="iOS" EditFormat="Custom" Width="190" Caption="Desde">
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
                    <dx:ASPxDateEdit ID="CalHastaTS" runat="server" Theme="iOS" EditFormat="Custom" Width="190" Caption="Hasta">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                    <br />
                </div> <label for="cmbFlujoTR" class="col-md-1 col-sm-1 col-xs-12 control-label">Flujo:</label>
                <div class="col-md-3 col-sm-3 col-xs-12">
                    <asp:DropDownList runat="server" ID="cmbFlujoTR" CssClass="form-control">
                    </asp:DropDownList>
                    <br />
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 text-xs-right">
                    <asp:Button ID="btnFiltroTR" runat="server" Text="Filtrar" CssClass="btn btn-success" ClientIDMode="Static" />
                    <br /><br />
                </div>
                <div class="row">
                    <div class="col-md-12,col-sm-12 col-xs-12 text-right">
                        <asp:LinkButton ID="lnkExportSuspend" runat="server" CausesValidation="False" OnClick="lnkExportar_Click"><img src="../../Imagenes/ExcelIcon.png" style="vertical-align:top"/>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UPTopSus" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <dx:ASPxGridView ID="dgvsuspendidos" ClientInstanceName="dgvsuspendidos" runat="server" AutoGenerateColumns="False" Width="100%" Style="margin-top: 0px" EnableTheming="True" Theme="iOS" Font-Size="10px">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="CVE PROMOTORIA" FieldName="Promotoria" VisibleIndex="1" Width="400" />
                                <dx:GridViewDataTextColumn Caption="PROMOTORIA" FieldName="Nombre" VisibleIndex="2" Width="400" />
                                <dx:GridViewDataTextColumn Caption="ZONA" FieldName="Zona" VisibleIndex="3" Width="400" />
                                <dx:GridViewDataTextColumn Caption="SUSPENDIDOS" FieldName="NumTramitesSus" VisibleIndex="4" Width="400" />
                                <dx:GridViewDataTextColumn Caption="EJECUTADOS" FieldName="NumTramitesEje" VisibleIndex="5" Width="400" />
                            </Columns>
                            <Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto" />
                            <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <SettingsSearchPanel Visible="true" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExportSuspendidos" runat="server" GridViewID="dgvsuspendidos"></dx:ASPxGridViewExporter>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:HiddenField ClientIDMode="Static" ID="Ancho" runat="server" Value="400" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div style="display: flex; justify-content: center">
                            <dx:WebChartControl ID="dxChtSuspendidos" runat="server" CrosshairEnabled="True" Height="300px" Width="600px" Theme="RedWine">
                                <Titles>
                                    <dx:ChartTitle Font="Arial,12pt" Text="TOP 10 SUSPENDIDOS" />
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
                                    <dx:Series Name="dxsreSuspendidos"></dx:Series>
                                </SeriesSerializable>
                            </dx:WebChartControl>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFiltroTR" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
