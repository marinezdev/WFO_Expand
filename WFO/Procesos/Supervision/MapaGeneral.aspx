<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="MapaGeneral.aspx.cs" Inherits="WFO.Procesos.Supervision.MapaGeneral" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                        <div class="x_panel">
                            <div class="x_title">
                                <h2>Mesas flujo </h2>
                                <div class="clearfix"></div>
                            </div>
                            <div class="x_content">
                                <p class="text-muted font-13 m-b-30">
                                    Las mesas se mostraran a partir del flujo seleccionado.
                                </p>
                                <div class="row">
                                    <div class="col-md-4 col-sm-4 col-xs-12 form-group has-feedback">
                                        <asp:DropDownList ID="cbFlujos" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="CargaFlujos_SelectedIndexChanged">
                                            <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cbFlujos" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                <asp:Literal id="MesasLiteral" runat=server  text=""/>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
