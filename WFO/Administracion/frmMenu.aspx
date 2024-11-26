<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMenu.aspx.cs" Inherits="WFO.Administracion.frmMenu" MasterPageFile="~/Utilerias/Site.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Menú</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <table>
                        <tr>
                            <td valign="top">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                    <asp:LinkButton ID="LigaEditar" runat="server" CommandName="Select" Text="Editar" OnClick="LigaEditar_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td valign="top">
                                <asp:Button ID="BtnNuevo" runat="server" CssClass="btn btn-primary" Text="Nuevo" OnClick="BtnNuevo_Click" />
                                <br /><br />
                                <fieldset id="fieldset01" runat="server" visible="false">
                                    <legend id="Legend01" runat="server"></legend>
                        
                                    <table id="TablaAgregarModificar" runat="server" visible="false">
                                        <tr><td align="right">Nombre de la opción:</td><td><asp:TextBox ID="txtNombre" runat="server" class="form-control"></asp:TextBox></td></tr>
                                        <tr><td align="right">Nombre de la página:</td><td><asp:TextBox ID="txtUrl" runat="server" class="form-control"></asp:TextBox></td></tr>
                                        <tr><td align="right">Pertenece a:</td><td><asp:DropDownList ID="ddlPerteneceA" runat="server" class="form-control"></asp:DropDownList></td></tr>
                                        <tr><td></td><td></td></tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="BtnAceptar" runat="server" CssClass="btn btn-primary" Text="Aceptar" OnClick="BtnAceptar_Click" />
                                                <asp:Button ID="BtnCancelar" runat="server" CssClass="btn btn-primary" Text="Cancelar" OnClick="BtnCancelar_Click" />
                                            </td>
                                        </tr>
                                    </table>

                                </fieldset>

                            </td>
                        </tr>
                    </table>

                </div>
            </div>
        </div>
    </div>


</asp:Content>
