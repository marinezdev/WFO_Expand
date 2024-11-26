<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmConfiguracionGeneral.aspx.cs" Inherits="WFO.Administracion.frmConfiguracionGeneral"  MasterPageFile="~/Utilerias/Site.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

     <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Configuración General del Sistema</h2>
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
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LigaEditar" runat="server" CommandName="Select" Text="Editar" OnClick="LigaEditar_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id" HeaderText="Id" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="Valor" HeaderText="Valor" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td valign="top">
                                <asp:Button ID="BtnAgregar" runat="server" Text="Nuevo" CssClass="btn btn-primary" OnClick="BtnAgregar_Click" />
                                <br /><br />
                                <fieldset id="Fieldset01" runat="server" visible="false">
                                    <legend id="Legend01" runat="server"></legend>
                    
                                    <table id="TablaAgregarModificar" runat="server" visible="false">
                                        <tr><td>Nombre:</td><td><asp:TextBox ID="txtNombre" runat="server" class="form-control"></asp:TextBox></td></tr>
                                        <tr><td>Valor:</td><td><asp:TextBox ID="txtValor" runat="server" class="form-control"></asp:TextBox></td></tr>
                                        <tr><td></td><td></td></tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="BtnAceptar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="BtnAceptar_Click" />
                                                <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="BtnCancelar_Click" />
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