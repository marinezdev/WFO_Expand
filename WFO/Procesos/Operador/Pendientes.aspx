<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="Pendientes.aspx.cs" Inherits="WFO.Procesos.Operador.Pendientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <script>
        function Cantidades(id) {
            var boton = document.getElementById('<%=btnConsultar.ClientID%>');
            $("#<%= hfIdPendiente.ClientID %>").val(id);
            boton.click();
        }
    </script>

    <asp:UpdatePanel ID="ObservacionesCartaEjecucion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
             <!-- Campos Ocultos -->
            <div>
                <asp:HiddenField ID="hfIdPendiente" runat="server" />
            </div>

            <!-- MODAL DE LISTADO DE TRAMITES -->
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title" id="myModalLabel2">
                                <asp:label ID="TituloModal" runat="server" Text="">
                                </asp:label>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <asp:Repeater ID="rptTramite" runat="server" OnItemCommand="rptTramite_ItemCommand">
                                        <HeaderTemplate>
                                            <table id="datatable" class="table table-striped table-bordered jambo_table bulk_action">
                                                <thead>
                                                    <tr>
                                                        <th>Trámite</th>
                                                        <th>Tipo de Servicio</th>
                                                        <th>Información del contratante</th>
                                                        <th>Mesa</th>
                                                        <th>Estado</th>
                                                        <th>Fecha envío</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("FolioCompuesto")%></td>
                                                <td><%#Eval("TipoServicio")%></td>
                                                <td><strong></strong><%#Eval("Contratante")%></td>
                                                <td><%#Eval("NombreMesa")%></td>
                                                <td><%#Eval("EstatusTramite")%></td>
                                                <td><%#Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                                <td><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/Imagenes/folder.png" CausesValidation="false" CommandName ="Consultar" CommandArgument='<%#Eval("IdTramite") + ";" +Eval("IdMesa")%>' /></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4 col-sm-4 col-xs-12 form-group has-feedback">
                    <asp:DropDownList ID="cbFlujos" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="CargaFlujos_SelectedIndexChanged">
                        <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cbFlujos" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row">
                <asp:Button ID="btnConsultar" Style="display: none" runat="server" Text="Cerrar" class="btn btn-default" CausesValidation="False" OnClick="btnConsultar_Click"  />
                <asp:Literal id="MesasLiteral" runat=server  text=""/>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
