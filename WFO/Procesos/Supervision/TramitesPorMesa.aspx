<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TramitesPorMesa.aspx.cs" Inherits="WFO.Procesos.Supervision.TramitesPorMesa" MasterPageFile="~/Utilerias/Site.Master"%>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">


    <div class="text-center text-warning">TRÁMITES POR MESA</div>
    <br />
    <br />
    <div class="container-fluid">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-md-3 col-sm-4 col-xs-12">

                    <table>
                        <tr>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td>
                                <asp:Label runat="server" ID="lblFechaInicio" Text="Fecha Inicio:"></asp:Label>
                                <dx:ASPxDateEdit ID="dtFechaInicio" runat="server" Theme="Material" EditFormat="Custom" Width="210" AutoPostBack="false" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy">
                                </dx:ASPxDateEdit>
                                <asp:RequiredFieldValidator runat="server" id="reqValidaFehcaInicio" controltovalidate="dtFechaInicio" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td>
                                <asp:Label runat="server" ID="lblFechaFin" Text="Fecha Fin:"></asp:Label>
                                <dx:ASPxDateEdit ID="dtFechaFin" runat="server" Theme="Material" EditFormat="Custom" Width="210" AutoPostBack="false" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy">
                                </dx:ASPxDateEdit>
                                <asp:RequiredFieldValidator runat="server" id="reqValidaFehcaFin" controltovalidate="dtFechaFin" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td valign="top">
                                Status Trámite:<br /> 
                                <asp:DropDownList ID="DDLStatusTramite" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td valign="top">
                                Nombre Mesa:<br />
                                <asp:DropDownList ID="DDLMesa" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td>
                                <asp:Button ID="btnBuscar" runat="server"  AutoPostBack="True" Text="Buscar" CssClass="btn btn-success" OnClick="btnBuscar_Click"  />
                                <asp:Label runat="server" ID="lblMensaje" ForeColor="Red" Font-Bold="true" Font-Size="Large" Text=""></asp:Label>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td>
                                <asp:LinkButton ID="lnkExportarResumen" runat="server"  CausesValidation="False" OnClick="lnkExportarResumen_Click" ToolTip="Exportar a Excel">
                                    <img src="../../Imagenes/excel.png"/>
                                </asp:LinkButton>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                            </td>
                        </tr>
                    </table>




                    <dx:ASPxGridView ID="gvTramitesPorMesa" runat="server" AutoGenerateColumns="False" Width="430%" Styles-Header-HorizontalAlign="Center" Visible="false">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Folio"                  FieldName="Folio"               VisibleIndex="1"    Width="110px" />
                                <dx:GridViewDataTextColumn Caption="Fecha Registro"         FieldName="FechaRegistro"       VisibleIndex="2"    Width="110px" />
                                <dx:GridViewDataTextColumn Caption="Fecha Término"          FieldName="FechaTermino"        VisibleIndex="3"    Width="110px" />
                                <dx:GridViewDataTextColumn Caption="Número Orden"           FieldName="NumeroOrden"         VisibleIndex="4"    Width="110px" />
                                <dx:GridViewDataTextColumn Caption="Fecha Solicitud"        FieldName="FechaSolicitud"      VisibleIndex="5"    Width="110px" />
                                <dx:GridViewDataTextColumn Caption="Status Trámite"         FieldName="StatusTramite"       VisibleIndex="6"    Width="110px" />
                                <dx:GridViewDataTextColumn Caption="Número Poliza"          FieldName="NumeroPoliza"        VisibleIndex="7"    Width="110px" />
                                <dx:GridViewDataTextColumn Caption="DCN Kwik"               FieldName="DCNKWIK"             VisibleIndex="8"    Width="110px" />
                                <dx:GridViewDataTextColumn Caption="Separado"               FieldName="Separado"            VisibleIndex="9"    Width="110px" />
                                <dx:GridViewDataTextColumn Caption="Operador"               FieldName="Operador"            VisibleIndex="10"   Width="110px" />
                                <dx:GridViewDataTextColumn Caption="Mesa"                   FieldName="Mesa"                VisibleIndex="11"   Width="110px" />
                                <dx:GridViewDataTextColumn Caption="Status Mesa"            FieldName="StatusMesa"          VisibleIndex="12"   Width="110px" />
                                <dx:GridViewDataTextColumn Caption="Observación Pública"    FieldName="ObservacionPublica"  VisibleIndex="13"   Width="110px" />
                                <dx:GridViewDataTextColumn Caption="Observación Privada"    FieldName="ObservacionPrivada"  VisibleIndex="14"   Width="110px" />
                                <dx:GridViewDataTextColumn Caption="Fin Archivo"            FieldName="FinArchivo"          VisibleIndex="15"   Width="110px" />
                            </Columns>
                            <Templates>
                                <EmptyDataRow>
                                    <div style="width: 300px;">
                                        No hay datos para desplegar...
                                    </div>
                                </EmptyDataRow>
                            </Templates> 
                            <SettingsBehavior AllowSelectByRowClick="true" />
                            <SettingsPager  Mode="ShowAllRecords"/>
                            <SettingsDetail ShowDetailRow="false" />
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" HorizontalScrollBarMode="Visible" />
                            <SettingsSearchPanel Visible="false" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="gvTramitesPorMesa"></dx:ASPxGridViewExporter>
                    

                
                </div>
            </div>
        </div>
    </div>


</asp:Content>