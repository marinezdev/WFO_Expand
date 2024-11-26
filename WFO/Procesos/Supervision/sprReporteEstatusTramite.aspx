<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="sprReporteEstatusTramite.aspx.cs" Inherits="WFO.Procesos.Supervision.sprReporteEstatusTramite" %>
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
    <div class="text-center text-warning">ESTATUS TRÁMITE </div>
    <br />
    <br />
    <div class="container-fluid">
        <div class="form-horizontal">
            <div class="form-group">
                <label for="cmbFlujo" class="col-md-1 col-sm-1 col-xs-1 control-label">Flujo:</label>
                <div class="col-md-11 col-sm-11 col-xs-11">
                    <asp:DropDownList runat="server" ID="cmbFlujo" CssClass="form-control">
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
                    <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="cmbEstatus" Width="285px" runat="server" AnimationType="None" Theme="iOS" Caption="Estatus">
                        <DropDownWindowStyle BackColor="#A5CE4E" />
                        <DropDownWindowTemplate>
                            <dx:ASPxListBox Width="100%" ID="listaEstatus" ClientInstanceName="checkListBox" SelectionMode="CheckColumn" runat="server" Height="200" EnableSelectAll="true" OnInit="listaEstatus_Init">
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
        </div>
        <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <dx:ASPxGridView ID="dvgdEstatusTramite" ClientInstanceName="dvgdEstatusTramite" runat="server" AutoGenerateColumns="False" Style="margin-top: 0px" EnableTheming="True" Theme="iOS" Font-Size="10px" Width="100%">
                            <Columns>
                                <dx:GridViewDataDateColumn Caption="FECHA INGRESO" FieldName="FechaIngreso" VisibleIndex="1" Width="110px">
                                    <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn Caption="FOLIO" FieldName="FolioTramite" VisibleIndex="2" Width="110px" />
                                <dx:GridViewDataTextColumn Caption="RAMO" FieldName="Ramo" VisibleIndex="3" Width="110px" />
                                <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="Producto" VisibleIndex="4" Width="110px" />
                                <dx:GridViewDataTextColumn Caption="ESTATUS" FieldName="Estatus" VisibleIndex="5" Width="110px" />
                                <dx:GridViewDataDateColumn Caption="FECHA ESTATUS" FieldName="FechaEstatus" VisibleIndex="6" Width="110px">
                                    <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn Caption="TIEMPO ESTATUS ACTUAL (HORAS)" FieldName="Tiempo" VisibleIndex="7" Width="110px" />
                                <dx:GridViewDataTextColumn Caption="NUM. PROMOTORIA" FieldName="IdPromotoria" VisibleIndex="8" Visible="false" />
                                <dx:GridViewDataTextColumn Caption="CVE. PROMOTORIA" FieldName="PromotoriaClave" VisibleIndex="9" Width="110px" />
                                <dx:GridViewDataTextColumn Caption="PROMOTORIA" FieldName="Promotoria" VisibleIndex="10" Width="110px" />
                                <dx:GridViewDataTextColumn Caption="CLAVE AGENTE" FieldName="ClaveAgente" VisibleIndex="11" Width="110px" />
                                <dx:GridViewDataTextColumn Caption="POLIZA" FieldName="Poliza" VisibleIndex="12" Width="110px" />
                                <dx:GridViewDataTextColumn Caption="IDENTIFICADOR" FieldName="Prioridad" VisibleIndex="13" Width="110px" />
                            </Columns>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="600" ShowFooter="True" ShowGroupFooter="VisibleAlways" HorizontalScrollBarMode="Visible" />
                            <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                            <SettingsPager EnableAdaptivity="true" />
                            <SettingsSearchPanel Visible="true" />
                            <Styles Header-Wrap="True" />
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
