<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUsuarios.aspx.cs" Inherits="WFO.Administracion.frmUsuarios" MasterPageFile="~/Utilerias/Site.Master"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Usuarios</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
    
                    <asp:GridView ID="gvPrueba" runat="server" OnRowCommand="gvPrueba_RowCommand1"></asp:GridView>

                    <table>
                        <tr>
                            <td valign="top">

                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LigaEditar" runat="server" CommandName="Select" Text="Editar" OnClick="LigaEditar_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IdUsuario" HeaderText="Id" />
                                        <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Registro" />
                                        <asp:BoundField DataField="RolNombre" HeaderText="Rol" />
                                        <asp:CheckBoxField DataField="Activo" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>

                            </td>
                            <td valign="top">

                                <asp:Button ID="BtnAgregar" runat="server" Text="Nuevo" OnClick="BtnAgregar_Click" CssClass="btn btn-primary" />

                                <br /><br />

                                <asp:UpdatePanel ID="upFieldSet01" runat="server">
                                    <ContentTemplate>

                                        <fieldset id="Fieldset01" runat="server" visible="false">
                                            <legend id="Legend01" runat="server"></legend>
                                            <table id="tablaAgregarModificar" runat="server" visible="false">
                                                <tr><td align="right">Nombre:</td><td><asp:TextBox ID="txtNombre" runat="server" class="form-control"></asp:TextBox></td></tr>
                                                <tr><td align="right">Clave:</td><td><asp:TextBox ID="txtClave" runat="server" class="form-control"></asp:TextBox></td></tr>
                                                <tr><td align="right">Contraseña:</td><td><asp:TextBox ID="txtContra" runat="server" TextMode="Password" class="form-control"></asp:TextBox></td></tr>
                                                <tr><td align="right">Rol:</td><td><asp:DropDownList ID="ddlRoles" runat="server" class="form-control"></asp:DropDownList></td></tr>
                                                <tr><td align="right">Fecha Cambio Clave:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtCambio" runat="server" class="form-control"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCambio" Format="dd/MM/yyyy"  />
                                                    </td>
                                                </tr>
                                                <tr><td align="right">Correo:</td><td><asp:TextBox ID="txtCorreo" runat="server" class="form-control"></asp:TextBox></td></tr>
                                                <tr><td align="right">Estado:</td><td><asp:CheckBox ID="chkActivo" runat="server" class="form-control" /></td></tr>
                                                <tr><td></td><td></td></tr>
                                                <tr><td colspan="2" align="center">
                                                    <asp:Button ID="BtnGuardar" runat="server" Text="Aceptar" OnClick="BtnGuardar_Click" CssClass="btn btn-primary" />
                                                    <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" CssClass="btn btn-primary" />

                                                    </td>
                                                </tr>
                                            </table>
                            
                            
                                        </fieldset>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="BtnGuardar" />
                                    </Triggers>
                                </asp:UpdatePanel>
                        

                            </td>
                        </tr>
                    </table>

                    <asp:Panel ID="PanelControles" runat="server" ClientIDMode="Static"></asp:Panel>

                </div>
            </div>
        </div>
    </div>


</asp:Content>