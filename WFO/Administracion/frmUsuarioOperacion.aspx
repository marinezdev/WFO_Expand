<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUsuarioOperacion.aspx.cs" Inherits="WFO.Administracion.frmUsuarioOperacion" MasterPageFile="~/Utilerias/Site.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Agregar Usuario Operación</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                
                    <table>
                        <tr><td>Nombre:</td><td><asp:TextBox ID="txtNombre" runat="server" class="form-control"></asp:TextBox> </td></tr>
                        <tr><td>Correo:</td><td><asp:TextBox ID="txtCorreo" runat="server" class="form-control"></asp:TextBox></td></tr>
                        <tr><td>Clave:</td><td><asp:TextBox ID="txtClave" runat="server" class="form-control"></asp:TextBox></td></tr>
                        <tr><td colspan="2" align="center"><asp:Button ID="BtnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="BtnAgregar_Click" />  </td></tr>
                        <tr><td colspan="2"><asp:Label id="LblMensajes" runat="server"></asp:Label> </td></tr>
                    </table>


                </div>
            </div>
        </div>
    </div>


</asp:Content>