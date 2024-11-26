<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Priorizar.aspx.cs" Inherits="WFO.Procesos.SupervisionGeneral.Priorizar" MasterPageFile="~/Utilerias/Site.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
<style>
    th, td { padding: 3px
    }
</style>


    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Priorizar Trámite</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <table>
                        <tr><td colspan="4"><h3>Buscar por:</h3></td></tr>
                        <tr><td>Folio</td><td colspan="3"><asp:TextBox ID="txtFolio" runat="server" class="form-control" Width="150px"></asp:TextBox></td><td rowspan="5"><asp:Button ID="BtnBuscar" runat="server" CssClass="btn btn-primary" Text="Buscar" OnClick="BtnBuscar_Click" /></td></tr>
                        <tr>
                            <td>Fecha Registro del&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtRegistroDel" runat="server" class="form-control" Width="100px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRegistroDel" />
                            </td>
                            <td>&nbsp;Al&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtRegistroAl" runat="server" class="form-control" Width="100px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtRegistroAl" />
                            </td>
                        </tr>
                        <tr>
                            <td>Fecha Solicitud del&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtSolicitudDel" runat="server" class="form-control" Width="100px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtSolicitudDel" />
                            </td>
                            <td>&nbsp;Al&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtSolicitudAl" runat="server" class="form-control" Width="100px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSolicitudAl" />
                            </td>
                        </tr>
                        <tr><td>Promotoría</td><td colspan="3"><asp:DropDownList ID="DDLCatPromotoria" runat="server" class="form-control" EnableViewState="true"></asp:DropDownList></td></tr>
                        <tr><td>Estado</td><td colspan="3"><asp:DropDownList ID="DDLEstados" runat="server" class="form-control"></asp:DropDownList>  </td></tr>
                        <tr><td colspan="5" align="center">&nbsp;</td></tr>
                    </table>





                    <asp:GridView ID="GVTramites" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered jambo_table bulk_action" SelectedRowStyle-BackColor="Green">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LigaActualizar" CssClass="btn btn-primary text-center" runat="server" CommandName="Select" Text="Priorizar" OnClick="LigaActualizar_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IdTramite" ItemStyle-Width="0" ItemStyle-Font-Size="0" />
                            <asp:BoundField DataField="IdUsuario" ItemStyle-Width="0" ItemStyle-Font-Size="0" />
                            <asp:BoundField DataField="IdPrioridad" ItemStyle-Width="0" ItemStyle-Font-Size="0" />
                            <asp:BoundField DataField="FolioCompuesto" HeaderText="Folio" />
                            <asp:BoundField DataField="TipoTramite" HeaderText="Tipo Trámite" />
                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                            <asp:BoundField DataField="FechaSolicitud" HeaderText="Fecha Solicitud" />
                            <asp:BoundField DataField="Promotoria" HeaderText="Promotoría" />
                            <asp:BoundField DataField="Estatus" HeaderText="Estado" />
                            <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" />
                        </Columns>
                    </asp:GridView>                    



                 </div>
                </div>
            </div>
        </div>

</asp:Content>