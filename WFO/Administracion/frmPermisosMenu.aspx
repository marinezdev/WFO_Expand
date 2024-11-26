<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPermisosMenu.aspx.cs" Inherits="WFO.Administracion.frmPermisosMenu" MasterPageFile="~/Utilerias/Site.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Permisos Menú</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <table>
                        <tr><td>Seleccione el Rol:</td><td><asp:DropDownList ID="ddlRoles" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" class="form-control"></asp:DropDownList></td></tr>
                        <tr id="trOpciones" runat="server" visible="false">
                            <td colspan="2">
                                Elija las opciones del menú a las que tendrá acceso el rol seleccionado.<br />
                                <asp:TreeView ID="tvwMenu" runat="server" ShowLines="true" ShowCheckBoxes="All"></asp:TreeView>
                                <br />
                                <asp:Button ID="BtnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="BtnGuardar_Click" />
                            </td>
                        </tr>
                    </table>    

                </div>
            </div>
        </div>
    </div>

</asp:Content>