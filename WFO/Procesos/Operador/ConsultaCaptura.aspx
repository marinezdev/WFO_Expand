<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaCaptura.aspx.cs" Inherits="WFO.Procesos.Operador.ConsultaCaptura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <!-- Campos Ocultos -->
    <div>
        <asp:HiddenField ID="hfIdTramite" runat="server" Value="0" />
        <asp:HiddenField ID="hfIdTipoTramite" runat="server" Value="0" />
    </div>


    <!-- DATOS DE MESA KWIK -->
        <asp:Panel ID="PanelDatos" runat="server" Visible="true" Enabled="False">
        <div class="row" >
            <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Datos de captura </small> </h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li>
                                <asp:Label runat="server" ID="Label53" Text="" Font-Bold="True" ></asp:Label>
                                <br />
                                <asp:Label runat="server" ID="Label56" Text="" Font-Bold="True" ></asp:Label>
                      	    </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content text-left">
                        <!-- SECCION DE CAPTURA -->
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <table id="datatable" class="table table-striped table-bordered table-responsive">
                                    <tbody>
                                        <tr>
                                            <td><asp:Label runat="server" ID="Label62" Text="Fecha firma de solicitud" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label58" Text="Sucursal" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label1" Text="UMAN" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label73" Text="Clave del agente" Font-Bold="True" class="control-label"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="LabelFechaFirmasolicitud" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelSucursal" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelUMAN" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelClaveAgente" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align:center; background-color:gainsboro"><asp:Label runat="server" ID="Label3" Text="Asegurado" Font-Bold="True" class="control-label"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="Label63" Text="Apellido Paterno" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label65" Text="Apellido Materno " Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label67" Text="Nombre" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label5" Text="Fecha Nacimiento" Font-Bold="True" class="control-label"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="LabelAPaterno" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelAMaterno" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelNombre" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelFechaNacimiento" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="Label6" Text="Antigüedad" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label8" Text="Teléfono" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label10" Text="Email" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label4" Text="Plan MedicaLife" Font-Bold="True" class="control-label"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="LabelAntiguedad" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelTelefono" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelEmail" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelPlanMedicaLife" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="Label15" Text="Deducible en pesos" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label18" Text="Causa seguro" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label19" Text="Región" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label20" Text="Tipo producto" Font-Bold="True" class="control-label"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td width="25%"><asp:Label runat="server" ID="LabelDeducibleEnPesos" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td width="25%"><asp:Label runat="server" ID="LabelCausaSeguro" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td width="25%"><asp:Label runat="server" ID="LabelRegion" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td width="25%"><asp:Label runat="server" ID="LabelTipoProducto" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <asp:Repeater ID="RepeterCoasegurados" runat="server" >
                                    <HeaderTemplate>
                                        <table id="datatable" class="table table-striped table-bordered table-responsive">
                                            <thead>
                                                <tr>
                                                    <th>Nombre</th>
                                                    <th>Apellido Paterno</th>
                                                    <th>Apellido Materno</th>
                                                    <th>Prentesco</th>
                                                    <th>Sexo </th>
                                                    <th>Fecha Nacimiento </th>
                                                    <th>Edad </th>
                                                    <th>Peso </th>
                                                    <th>Altura </th>
                                                    <th>Cotización </th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("Nombre")%></td>
                                            <td><%#Eval("APaterno")%></td>
                                            <td><%#Eval("AMaterno")%></td>
                                            <td><%#Eval("Interprestacion_larga")%></td>
                                            <td><%#Eval("Sexo")%></td>
                                            <td><%#Eval("FechaNacimiento","{0:dd/MM/yyyy }")%></td>
                                            <td><%#Eval("Edad")%></td>
                                            <td><%#Eval("Peso")%></td>
                                            <td><%#Eval("Altura")%></td>
                                            <td><%#Eval("ExamenEvaluacion")%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <asp:Repeater ID="RepeterTarjetasMesaCaptura" runat="server" >
                                    <HeaderTemplate>
                                        <table id="datatable" class="table table-striped table-bordered table-responsive">
                                            <thead>
                                                <tr>
                                                    <th>Periodicidad</th>
                                                    <th>Modo de Pago</th>
                                                    <th>Banco</th>
                                                    <th>Token</th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("periodicidad")%></td>
                                            <td><%#Eval("modo_pago")%></td>
                                            <td><%#Eval("banco")%></td>
                                            <td><%#Eval("Token")%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
