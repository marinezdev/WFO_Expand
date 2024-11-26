<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sprReporteProductividadPromotorias.aspx.cs" Inherits="WFO.Procesos.Supervision.sprReporteProductividadPromotorias" MasterPageFile="~/Utilerias/Site.Master"%>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

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
            for(var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if(item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
    </script>


    <div class="text-center text-warning">REPORTE DE PRODUCTIVIDAD PROMOTORIAS</div>
    <br />
    <br />
    <div class="container-fluid">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <table style ="width:75%">
                        <tr>
                            <td>
                                <dx:ASPxComboBox ID="ASPxComboBoxAnn" runat="server"
                                    ClientInstanceName="SeparatorComboBox" EnableClientSideAPI="True" Theme="iOS"
                                    ValueType="System.String" Caption="Año:">
                                    <Items>
                                        <dx:ListEditItem Text="Seleccionar" Value="0" Selected="true"/>
                                        <dx:ListEditItem Text="2018" Value="2018" />
                                        <dx:ListEditItem Text="2019" Value="2019"/>
                                        <dx:ListEditItem Text="2020" Value="2020"/>
                                    </Items>                        
                                </dx:ASPxComboBox>

                            </td>
                            <td>
                            </td>
                            <td>
                                <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="cmbEstatus" Width="285px" runat="server" AnimationType="None" Theme="iOS" Caption="Promotorías:">
                                    <DropDownWindowStyle BackColor="#A5CE4E" />
                                    <DropDownWindowTemplate>
                                        <dx:ASPxListBox Width="100%" ID="LbxPromotorias" runat="server"
                                            ClientInstanceName="checkListBox" 
                                            SelectionMode="CheckColumn"
                                            Height="200" 
                                            EnableSelectAll="true" ValueField="Id" ValueType="System.Int32"
                                            TextField="Nombre"
                                            OnInit="LbxPromotorias_Init">
                                            <FilteringSettings ShowSearchUI="true"/>
                                            <Border BorderStyle="None" />
                                            <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                            <ClientSideEvents SelectedIndexChanged="updateText" />
                                        </dx:ASPxListBox>

                                        <table style="width: 100%">
                                            <tr>
                                                <td style="padding: 4px">
                                                    <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Cerrar" style="float: right">
                                                        <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>

                                    </DropDownWindowTemplate>
                                    <ClientSideEvents TextChanged="synchronizeListBoxValues" DropDown="synchronizeListBoxValues" />
                                </dx:ASPxDropDownEdit>
                            </td>
                           <td>
                               <asp:Button ID="btnFiltrar"  CssClass="boton" runat="server" Text="Buscar" OnClick="btnFiltrar_Click"/>
                            </td>
                       </tr>
                       <tr>
                             <td>
                                <asp:UpdateProgress ID="updProgress"  runat="server">
                                    <ProgressTemplate>           
                                     <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.8;">
                                        <div style="padding: 10px;position:fixed;top:45%;left:50%;background-color:white;width:170px;height:45px " > 
                                         <table style="background-size:0">
                                            <tr>
                                             <td>
                                                 <img alt=" " src="/img/spinner.gif" />
                                             </td>
                                            <td>
                                                <span style="font-size:16px">&nbsp;Procesando...</span>
                                            </td>
                                            </tr>
                                         </table>
                                       </div> 
                                     </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                             </td>
                            </tr>
                    </table>

                    <br /><br />
                    <table style="width:100%">
                        <tr>
                         <td style="text-align:right">
                            <asp:LinkButton ID="lnkExportar" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                               <img src="../../Imagenes/excel.png"/>
                            </asp:LinkButton>
                         </td>
                     </tr>
                    </table>
        
                    <table style="width:99%">
                        <tr>
                            <td style="width:100%;vertical-align:top">
                                <dx:ASPxGridView ID="dvgdEstatusTramite" ClientInstanceName="dvgdEstatusTramite" runat="server" 
                                    AutoGenerateColumns ="True" 
                                    style="margin-top: 0px" 
                                    EnableTheming="false" 
                                    Font-Size ="10px" 
                                    Width="100%" SettingsDetail-ExportMode="All" SettingsExport-ExcelExportMode="Default">           
                                <Settings HorizontalScrollBarMode="Hidden" VerticalScrollBarMode="Hidden" ShowFooter="false" ShowGroupFooter="Hidden" />
                                <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                                <SettingsPager  Mode="ShowAllRecords"/>
                                <SettingsSearchPanel Visible="false" />
                                <Styles Header-Wrap="false" Cell-Wrap="true" />
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdEstatusTramite"></dx:ASPxGridViewExporter>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>