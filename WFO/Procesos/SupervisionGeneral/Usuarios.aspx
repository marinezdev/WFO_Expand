<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="WFO.Procesos.SupervisionGeneral.Usuarios" MasterPageFile="~/Utilerias/Site.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Desbloqueo de Usuarios</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <table>
                        <tr><td>Nombre de usuario:</td><td><asp:TextBox ID="txtUsuario" runat="server" class="form-control"></asp:TextBox></td><td><asp:Button ID="BtnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="BtnBuscar_Click" /></td></tr>
                    </table>
                    <br />

                    <asp:GridView ID="GVUsuarios" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LigaDesbloquear" runat="server" CommandName="Select" Text="Desbloquear" OnClick="LigaDesbloquear_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IdUsuario" HeaderText="Id" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="FechaCambioClave" HeaderText="Fecha Válida Acceso" />
                            <asp:BoundField DataField="Clave" HeaderText="Clave" />
                            <asp:BoundField DataField="RolNombre" HeaderText="Rol" />
                            <asp:CheckBoxField DataField="Activo" HeaderText="Activo" ItemStyle-HorizontalAlign="Center" />
                            <asp:CheckBoxField DataField="Conectado" HeaderText="Conectado" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>



</asp:Content>