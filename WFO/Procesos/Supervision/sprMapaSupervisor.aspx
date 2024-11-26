<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="sprMapaSupervisor.aspx.cs" Inherits="WFO.Procesos.Supervision.sprMapaSupervisor" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="text-center text-warning">DETALLE DE MOVIMIENTOS </div>
    <br />
    <br />
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <br /><br />
                <dx:ASPxLabel runat="server" ID="lblMesa" Font-Bold="true" />
                <br /><br />
            </div>
        </div>
        <div class="col-md-12 col-sm-12 col-xs-12">
            <dx:ASPxGridView ID="dvgdTramites" ClientInstanceName="dvgdTramites" runat="server"
                AutoGenerateColumns="False" Width="100%" EnableTheming="True" Theme="iOS"
                Font-Size="10px" KeyFieldName="idMesa">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="FLUJO" FieldName="flujo" VisibleIndex="1" Width="250" />
                    <dx:GridViewDataTextColumn Caption="NUM MESA" FieldName="idMesa" VisibleIndex="2" Visible="false" />
                    <dx:GridViewDataTextColumn Caption="USUARIOS CONECTADOS" FieldName="Usuarios" VisibleIndex="3" Width="180" />
                    <dx:GridViewDataTextColumn Caption="TRAMITES NUEVOS" FieldName="TramitesNuevos" VisibleIndex="4" Width="180" />
                    <dx:GridViewDataTextColumn Caption="# REINGRESOS" FieldName="Reingresos" VisibleIndex="5" Width="180" />
                    <dx:GridViewDataTextColumn Caption="TOTAL TRAMITES" FieldName="TramitesTotal" VisibleIndex="6" Width="180" />
                </Columns>
                <SettingsDetail ShowDetailRow="true" />
                <SettingsExport ExcelExportMode="WYSIWYG"></SettingsExport>
                <Templates>
                    <DetailRow>
                        <dx:ASPxGridView ID="dvgdDetalleTramite" runat="server" ClientInstanceName="dvgdDetalleTramite" OnInit="dvgdDetalleTramite_Init" KeyFieldName="idTramite" Width="100%" EnablePagingGestures="False" AutoGenerateColumns="False">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="FolioCompuesto" Caption="FOLIO" VisibleIndex="1" Width="180px" />
                                <dx:GridViewDataColumn FieldName="Reingreso" Caption="REINGRESOS" VisibleIndex="2" />
                                <dx:GridViewDataColumn FieldName="Fecha" Caption="FECHA" VisibleIndex="3"  />
                                <dx:GridViewDataColumn FieldName="Usuario" Caption="USUARIO" VisibleIndex="4" />
                                <dx:GridViewDataColumn FieldName="tAtencion" Caption="TIEMPO ATENCION" VisibleIndex="5" />
                                <dx:GridViewDataColumn FieldName="tMesa" Caption="TIEMPO MESA" VisibleIndex="6" />
                                <dx:GridViewDataColumn FieldName="Contratante" Caption="CONTRATANTE" VisibleIndex="7" />
                                <dx:GridViewDataColumn FieldName="Solicitante" Caption="SOLICITANTE" VisibleIndex="8" />
                            </Columns>
                            <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupedColumns="True" ShowGroupPanel="True" />
                            <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                            <SettingsPager EnableAdaptivity="true" Mode="ShowPager" />
                            <SettingsSearchPanel Visible="true" />
                            <Styles Header-Wrap="True" />
                        </dx:ASPxGridView>
                    </DetailRow>
                </Templates>
                <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" AllowEllipsisInText="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsDetail ShowDetailRow="true" />
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="600" />
                <Settings HorizontalScrollBarMode="Visible" />
                <SettingsSearchPanel Visible="true" />
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdTramites"></dx:ASPxGridViewExporter>
        </div>
    </div>
</asp:Content>
