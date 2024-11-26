<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCambiarClave.aspx.cs" Inherits="WFO.Administracion.frmCambiarClave" MasterPageFile="~/Utilerias/Site.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Cambiar Contraseña</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <table>
                        <tr><td colspan="3">Longitud miníma: 8 caracteres, debe incluir por lo<br /> menos una letra mayúscula, un número<br /> y caracteres especiales(#$%&?).</td></tr>
                        <tr>
                            <td>Contraseña actual:</td><td><asp:TextBox ID="txtActual" runat="server" TextMode="Password" class="form-control"></asp:TextBox></td><td></td>
                        </tr>
                        <tr>
                            <td>Nueva contraseña:</td>
                            <td>
                                <asp:TextBox ID="txtNueva" runat="server" TextMode="Password" class="form-control"></asp:TextBox>
                                <ajaxToolkit:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="txtNueva"                             
                                    PreferredPasswordLength="8" 
                                    MinimumNumericCharacters="1" 
                                    MinimumSymbolCharacters="1" 
                                    RequiresUpperAndLowerCaseCharacters="true" 
                                    HelpStatusLabelID="Lblayuda" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txClave" runat="server" TargetControlID="txtNueva" FilterMode="ValidChars" ValidChars="1234567890._&$#?=/*abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ"></ajaxToolkit:FilteredTextBoxExtender>

                            </td>
                            <td></td>
                        </tr>
                        <tr><td colspan="3"><asp:Label ID="LblAyuda" runat="server"></asp:Label></td></tr>
                        <tr>
                            <td>Repetir contraseña:</td>
                            <td>
                                <asp:TextBox ID="txtRepetir" runat="server" TextMode="Password" class="form-control"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRepetir" FilterMode="ValidChars" ValidChars="1234567890._&$#?=/*abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ"></ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                            <td></td>

                        </tr>
                        <tr><td colspan="3" align="center"><asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="BtnAceptar_Click" /></td></tr>
                        <tr><td colspan="3"><asp:Label ID="LblMensajes" runat="server"></asp:Label> </td></tr>
                    </table>    
                </div>
            </div>
        </div>
     </div>

</asp:Content>


