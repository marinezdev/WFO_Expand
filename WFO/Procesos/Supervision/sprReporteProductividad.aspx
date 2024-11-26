<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="sprReporteProductividad.aspx.cs" Inherits="WFO.Procesos.Supervision.sprReporteProductividad" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <script type="text/javascript">
        var textSeparator = ";";
        function updateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(getSelectedItemsText(selectedItems));
        }
        function synchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = getValuesByTexts(texts);
            checkListBox.SelectValues(values);
            updateText(); // for remove non-existing texts
        }
        function getSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function getValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
    </script>
    <div class="text-center text-warning">PRODUCTIVIDAD </div>
    <br />
    <div class="container-fluid">
        <div class="form-horizontal">
            <div class="form-group">
                <label for="cmbFlujo" class="col-md-1 col-sm-1 col-xs-1 control-label">Flujo:</label>
                <div class="col-md-11 col-sm-11 col-xs-11">
                    <asp:DropDownList runat="server" ID="cmbFlujo" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                    <br />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-3 col-sm-4 col-xs-12">
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
                <div class="col-md-4 col-sm-4 col-xs-12">
                    <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="cmbUsuarios" Width="285px" runat="server" AnimationType="None" Theme="iOS" Caption="Usuario">
                        <DropDownWindowStyle BackColor="#A5CE4E" />
                        <DropDownWindowTemplate>
                            <dx:ASPxListBox Width="100%" ID="listUsuario" ClientInstanceName="checkListBox" SelectionMode="CheckColumn" runat="server" Height="200" EnableSelectAll="true" OnInit="listUsuario_Init">
                                <FilteringSettings ShowSearchUI="true" />
                                <Border BorderStyle="None" />
                                <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                <ClientSideEvents SelectedIndexChanged="updateText" />
                            </dx:ASPxListBox>
                            <table style="width: 100%">
                                <tr>
                                    <td style="padding: 4px">
                                        <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Cerrar" Style="float: right">
                                            <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DropDownWindowTemplate>
                        <ClientSideEvents TextChanged="synchronizeListBoxValues" DropDown="synchronizeListBoxValues" />
                    </dx:ASPxDropDownEdit>
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
                        <dx:ASPxGridView ID="dvgdProductividad" ClientInstanceName="dvgdProductividad" runat="server" AutoGenerateColumns="False" Width="100%" Style="margin-top: 0px" EnableTheming="True" Theme="iOS" Font-Size="10px" KeyFieldName="operador">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="OPERADOR" FieldName="operador" VisibleIndex="0" Width="150" />
                                <dx:GridViewDataTextColumn Caption="MESA" FieldName="mesaNombre" VisibleIndex="1" Width="120" />
                                <dx:GridViewDataTextColumn Caption="ACEPTADOS" FieldName="aceptado" VisibleIndex="2" Width="125" />
                                <dx:GridViewDataTextColumn Caption="RECHAZADOS" FieldName="rechazo" VisibleIndex="4" Width="125" />
                                <dx:GridViewDataTextColumn Caption="EN TRÁMITE" FieldName="tramite" VisibleIndex="5" Width="125" />
                                <dx:GridViewDataTextColumn Caption="EN PROCESO" FieldName="proceso" VisibleIndex="6" Width="125" />
                                <dx:GridViewDataTextColumn Caption="EN PAUSA" FieldName="pausa" VisibleIndex="7" Width="150" />
                                <dx:GridViewDataTextColumn Caption="TOTAL TRÁMITES" FieldName="totalTramites" VisibleIndex="8" Width="150" />
                                <dx:GridViewDataTextColumn Caption="TIEMPO TOTAL" FieldName="tiempo" VisibleIndex="9" Width="150" />
                            </Columns>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxLabel  runat="server" Text='<%# Eval("mesaNombre") %>' Font-Bold="true" />
                                    <br />
                                    <br />
                                    <dx:ASPxGridView ID="dvgdDetalleProducividad" runat="server" ClientInstanceName="dvgdProductividad" OnInit="dvgdDetalleProducividad_Init" KeyFieldName="IdTramite" Width="100%" EnablePagingGestures="False" AutoGenerateColumns="False">
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="IdTramite" Caption="TRAMITE" VisibleIndex="0" Visible="false" />
                                            <dx:GridViewDataColumn FieldName="FolioCompuesto" Caption="FOLIO" VisibleIndex="1" />
                                            <dx:GridViewDataColumn FieldName="EstadoNombre" Caption="ESTATUS" VisibleIndex="2" />
                                            <dx:GridViewDataDateColumn FieldName="FechaInicio" Caption="FECHA INICIO" VisibleIndex="3" PropertiesDateEdit-DisplayFormatString="G" />
                                            <dx:GridViewDataDateColumn FieldName="FechaTermino" Caption="FECHA TERMINO" VisibleIndex="4" PropertiesDateEdit-DisplayFormatString="G" />
                                            <dx:GridViewDataColumn FieldName="tiempo" Caption="TIEMPO" VisibleIndex="5" />
                                        </Columns>
                                        <Settings ShowFooter="True" />
                                        <SettingsPager EnableAdaptivity="true" />
                                        <SettingsExport ExcelExportMode="WYSIWYG"></SettingsExport>
                                        <Styles Header-Wrap="True" />
                                        <SettingsSearchPanel Visible="true" />
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  AllowEllipsisInText="true" />
                            <SettingsPager EnableAdaptivity="true" />
                            <SettingsDetail ShowDetailRow="true" />
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="600" HorizontalScrollBarMode="Visible" />
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


