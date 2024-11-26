<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="detalleMesa.aspx.cs" Inherits="WFO.Procesos.Supervision.detalleMesa" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="text-warning text-center">DETALLE POR MESA (SÁBANA) </div>
    <br />
    <br />
    <div class="container-fluid">
        <div class="row>">
            <div class="col-sm-4; text-right">
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
            <div class="col-sm-4; text-right">
                <dx:ASPxDateEdit ID="CalHasta" runat="server" Theme="iOS" EditFormat="Custom" Width="190" Caption="Hasta">
                    <TimeSectionProperties Adaptive="true">
                        <TimeEditProperties EditFormatString="hh:mm tt" />
                    </TimeSectionProperties>
                    <CalendarProperties>
                        <FastNavProperties DisplayMode="Inline" />
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </div>
            <div class="col-sm-4; text-right">
                <asp:Button ID="btnFiltrar" CssClass="btn btn-success" runat="server" Text="Filtrar" ClientIDMode="Static" />
            </div>
        </div>
        <asp:UpdatePanel ID="DetalleTramites" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12">
                        <dx:ASPxGridView ID="dvgdTramites" ClientInstanceName="dvgdTramites" runat="server" AutoGenerateColumns="False" Width="95%" EnableTheming="True" Theme="iOS" Font-Size="10px" KeyFieldName="IdTramite">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Tramite" FieldName="IdTramite" VisibleIndex="1" Visible="false" />
                                <dx:GridViewDataTextColumn Caption="FOLIO" FieldName="NumPoliza" VisibleIndex="2" Width="150px" />
                                <dx:GridViewDataTextColumn Caption="CVE PROMOTORIA" FieldName="PromotoriaClave" VisibleIndex="3" Width="150px" />
                                <dx:GridViewDataTextColumn Caption="PROMOTORIA" FieldName="PromotoriaNombre" VisibleIndex="4" Width="300px">
                                    <CellStyle Wrap="True"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="CONTRATANTE" FieldName="Contratante" VisibleIndex="5" Width="300px">
                                    <CellStyle Wrap="True"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ESTATUS" FieldName="EstatusActual" VisibleIndex="6" />
                                <dx:GridViewDataTextColumn Caption="FECHA ULTIMO MOV" FieldName="FechaEstatusActual" VisibleIndex="7" Width="200px" />
                            </Columns>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxLabel runat="server" Font-Bold="true" Text="Trámite: " />
                                    <dx:ASPxLabel runat="server" Text='<%#Eval("NumPoliza") %>' Font-Bold="true" />
                                    <br />
                                    <br />
                                    <dx:ASPxGridView ID="dvgdDetalleTramite" runat="server" ClientInstanceName="dvgdDetalleTramite" OnInit="dvgdDetalleTramite_Init" KeyFieldName="idTramite" Width="95%" EnablePagingGestures="False" AutoGenerateColumns="False">
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="idTramite" Caption="TRAMITE" VisibleIndex="1" Visible="false" />
                                            <dx:GridViewDataColumn FieldName="idMesa" Caption="NUM MESA" VisibleIndex="2" Visible="false" />
                                            <dx:GridViewDataColumn FieldName="MesaNombre" Caption="MESA" VisibleIndex="3" />
                                            <dx:GridViewDataColumn FieldName="usuario" Caption="USUARIO" VisibleIndex="4" />
                                            <dx:GridViewDataTextColumn FieldName="FechaInicio" Caption="FECHA INICIO" VisibleIndex="5" />
                                            <dx:GridViewDataTextColumn FieldName="FechaTermino" Caption="FECHA TERMINO" VisibleIndex="6" />
                                            <dx:GridViewDataColumn FieldName="EstadoNombre" Caption="ESTATUS MESA" VisibleIndex="7" />
                                            <dx:GridViewDataColumn FieldName="Observacion" Caption="COMENTARIO PÚBLICO" VisibleIndex="8" />
                                            <dx:GridViewDataColumn FieldName="ObservacionPrivada" Caption="COMENTARIO PRIVADO" VisibleIndex="9" />
                                        </Columns>
                                        <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupedColumns="True" ShowGroupPanel="True" />
                                        <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" HorizontalScrollBarMode="Visible" />
                                        <SettingsPager EnableAdaptivity="true" Mode="ShowAllRecords" />
                                        <Styles Header-Wrap="True" />
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" AllowEllipsisInText="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <SettingsDetail ShowDetailRow="true" />
                            <Settings HorizontalScrollBarMode="Visible" />
                            <SettingsSearchPanel Visible="true" />
                        </dx:ASPxGridView>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFiltrar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
