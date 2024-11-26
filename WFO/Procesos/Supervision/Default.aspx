<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WFO.Procesos.Supervision.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v17.2" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.XtraCharts.Web" Assembly="DevExpress.XtraCharts.v17.2.Web" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.XtraCharts" Assembly="DevExpress.XtraCharts.v17.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="container-fluid" id="contenedor">
        <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <dx:ASPxPageControl ID="carTabPage" Width="100%" runat="server" CssClass="dxtcFixed" ActiveTabIndex="0" EnableHierarchyRecreation="True">
                    <TabPages>
                        <dx:TabPage Text="INDICADORES">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server">
                                    <fieldset>
                                        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" AllowCollapsingByHeaderClick="True" HeaderText="MAPA GENERAL" Theme="Aqua">
                                            <ContentPaddings Padding="5px" />
                                            <PanelCollection>
                                                <dx:PanelContent runat="server">
                                                    <div class="container-fluid">
                                                        <br />
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <label for="cmbFlujoM" class="col-md-1 col-sm-1 col-xs-12 control-label">Flujo:</label>
                                                                <div class="col-md-3 col-sm-3 col-xs-12">
                                                                    <asp:DropDownList runat="server" ID="cmbFlujoM" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                </div>
                                                                <div class="col-md-8 col-sm-8 col-xs-12 text-xs-right">
                                                                    <asp:Button ID="btnFiltrarM" CssClass="btn btn-success" runat="server" Text="Ver" ClientIDMode="Static" />
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                                        <br />
                                                                        <hr />
                                                                        <br /><br />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 col-md-12" runat="server">
                                                                <asp:Literal runat="server" ID="IndicadoresMapa" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxRoundPanel>
                                    </fieldset>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                    <TabPages>
                        <dx:TabPage Text="RESUMEN PROMOTORÍAS">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl2" runat="server">
                                    <fieldset>
                                        <div class="container-fluid">
                                            <br />
                                            <br />
                                            <div class="form-horizontal">
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                        <dx:ASPxGridView ID="dvgdEstatusTramite" ClientInstanceName="dvgdEstatusTramite" runat="server" AutoGenerateColumns="False" KeyFieldName="ESTADO" Width="100%" Style="margin-top: 0px" EnableTheming="True" Theme="iOS" Font-Size="10px">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" VisibleIndex="0" Visible="false" />
                                                                <dx:GridViewDataTextColumn Caption="ESTATUS" FieldName="ESTATUS" VisibleIndex="1" />
                                                                <dx:GridViewDataTextColumn Caption="TOTAL" FieldName="TOTAL" VisibleIndex="2" />
                                                            </Columns>
                                                            <Templates>
                                                                <DetailRow>
                                                                    <dx:ASPxLabel runat="server" Text='<%# Eval("ESTATUS") %>' Font-Bold="true" />
                                                                    <br />
                                                                    <br />
                                                                    <dx:ASPxGridView ID="dvgdDetallePromotoria" runat="server" ClientInstanceName="dvgdDetallePromotoria" OnInit="dvgdDetallePromotoria_Init" KeyFieldName="TRAMITE" Width="100%" EnablePagingGestures="False" AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <dx:GridViewDataColumn FieldName="RAMO" Caption="CVERAMO" VisibleIndex="0" Visible="false" />
                                                                            <dx:GridViewDataColumn FieldName="NRAMO" Caption="RAMO" VisibleIndex="1" />
                                                                            <dx:GridViewDataColumn FieldName="TRAMITE" Caption="TRAMITE" VisibleIndex="2" Width="180" />
                                                                            <dx:GridViewDataColumn FieldName="CVEPROMOTORIA" Caption="NUM PROMOTORIA" VisibleIndex="3" />
                                                                            <dx:GridViewDataColumn FieldName="PROMOTORIA" Caption="PROMOTORIA" VisibleIndex="4" />
                                                                            <dx:GridViewDataColumn FieldName="CLAVEAGENTE" Caption="NUM AGENTE" VisibleIndex="5" />
                                                                            <dx:GridViewDataColumn FieldName="AGENTE" Caption="AGENTE" VisibleIndex="6" />
                                                                            <dx:GridViewDataColumn FieldName="DIAS" Caption="DIAS" VisibleIndex="7" />
                                                                        </Columns>
                                                                        <Settings ShowFooter="True" HorizontalScrollBarMode="Visible" />
                                                                        <SettingsPager EnableAdaptivity="true" />
                                                                        <Styles Header-Wrap="True" />
                                                                        <SettingsSearchPanel Visible="true" />
                                                                    </dx:ASPxGridView>
                                                                </DetailRow>
                                                            </Templates>
                                                            <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" />
                                                            <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" AllowEllipsisInText="true" />
                                                            <SettingsPager Mode="ShowAllRecords" />
                                                            <SettingsDetail ShowDetailRow="true" />
                                                            <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
                                                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="200" />
                                                            <SettingsSearchPanel Visible="true" />
                                                        </dx:ASPxGridView>
                                                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdEstatusTramite"></dx:ASPxGridViewExporter>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 col-md-12">
                                                        <br />
                                                        <br />
                                                        <div style="display: flex; justify-content: center">
                                                            <dx:WebChartControl ID="dxChtTotales" runat="server" CrosshairEnabled="True" Height="300px" Width="550px" Theme="SoftOrange">
                                                                <Titles>
                                                                    <dx:ChartTitle Font="Arial,12pt" Text="ESTATUS" />
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
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <asp:HiddenField ClientIDMode="Static" ID="Ancho" runat="server" Value="400" />
                                                    </div>
                                                </div>
                                            </div>
                                    </fieldset>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFiltrar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Button ID="btnFiltrar" runat="server" ClientIDMode="Static" />
    </div>
</asp:Content>



