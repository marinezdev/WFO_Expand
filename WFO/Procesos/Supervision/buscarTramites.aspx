<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="buscarTramites.aspx.cs" Inherits="WFO.Procesos.Supervision.buscarTramites" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <script>
        $(document).keypress(
            function (event) {
                if (event.which == '13') {
                    event.preventDefault();
                }
            });
    </script>
    <div class="text-center text-warning">BÚSQUEDA DE TRÁMITES</div>
    <br />
    <br />
    <div class="container-fluid">
        <div class="form-horizontal">
            <div class="form-group">
                <label for="txtTramite" class="col-md-1 col-sm-1 col-xs-12 control-label">TRÁMITE</label>
                <div class="col-md-2 col-sm-2 col-xs-12">
                    <asp:TextBox ID="txtTramite" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <label for="txtRFC" class="col-md-1 col-sm-1 col-xs-12 control-label">RFC</label>
                <div class="col-md-2 col-sm-2 col-xs-12">
                    <asp:TextBox ID="txtRFC" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <label for="txtContratante" class="col-md-2 col-sm-2 col-xs-12 control-label">CONTRATANTE</label>
                <div class="col-md-4 col-sm-4 col-xs-12">
                    <asp:TextBox ID="txtContratante" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label for="txtAsegurado" class="col-md-1 col-sm-1 col-xs-12 control-label">ASEGURADO</label>
                <div class="col-md-4 col-sm-4 col-xs-12">
                    <asp:TextBox ID="txtAsegurado" runat="server" CssClass="form-control"></asp:TextBox>
                    <br />
                </div>
                <div class="col-md-7 col-sm-7 col-xs-12 text-xs-right">
                    <asp:Button ID="btnFiltroMesa" CssClass="btn btn-success" runat="server" Text="Buscar" />
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
        <asp:UpdatePanel ID="DetalleTramites" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <br />
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <dx:ASPxGridView ID="dvgdTramites" ClientInstanceName="dvgdTramites" runat="server"
                            AutoGenerateColumns="False" Width="100%" Style="margin-top: 0px" EnableTheming="True" Theme="iOS"
                            Font-Size="10px" KeyFieldName="IdTramite">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="TRÁMITE" FieldName="FolioCompuesto" VisibleIndex="1" Width="100" />
                                <dx:GridViewDataTextColumn Caption="CONTRATANTE" FieldName="Contratante" VisibleIndex="2" width="250"/>
                                <dx:GridViewDataTextColumn Caption="TITULAR" FieldName="Titular" VisibleIndex="3" Width="250" />
                                <dx:GridViewDataTextColumn Caption="NUM. TRÁMITE" FieldName="IdTramite" Visible="false" VisibleIndex="6" />
                                <dx:GridViewDataTextColumn Caption="MESA" FieldName="MesaNombre" VisibleIndex="7" width="200"/>
                                <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="UsuarioNombre" VisibleIndex="8" width="250"/>
                                <dx:GridViewDataTextColumn Caption="IDENTIFICADOR" FieldName="prioridad" VisibleIndex="9" />
                            </Columns>
                            <SettingsBehavior AllowSelectByRowClick="true" AllowSelectSingleRowOnly="false" EnableRowHotTrack="False" />
                            <SettingsPager EnableAdaptivity="true" />
                            <SettingsSearchPanel Visible="true" />
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="600" HorizontalScrollBarMode="Visible" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExportMotivos" runat="server" GridViewID="dvgdMotivosSuspension"></dx:ASPxGridViewExporter>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFiltroMesa" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

