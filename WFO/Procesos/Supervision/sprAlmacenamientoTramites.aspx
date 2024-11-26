<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sprAlmacenamientoTramites.aspx.cs" Inherits="WFO.Procesos.Supervision.sprAlmacenamientoTramites" MasterPageFile="~/Utilerias/Site.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="text-center text-warning">ALMACENAMIENTO DE TRAMITES</div>
    <br /><br />
    <div class="container-fluid">
        <div class="row">
            <br />
            <asp:GridView ID="GVPromedio" runat="server" AutoGenerateColumns="false" CellPadding="15" CellSpacing="10" HeaderStyle-BackColor="#deedf7" HeaderStyle-ForeColor="#2779aa" BorderColor="#aed0ea" Width="100%">
                <Columns>
                    <asp:HyperLinkField DataTextField="1" HeaderText="0 a 30" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=1" DataNavigateUrlFields="1" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField DataTextField="2" HeaderText="31 a 60" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=2" DataNavigateUrlFields="2" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField DataTextField="3" HeaderText="61 a 90" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=3" DataNavigateUrlFields="3" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField DataTextField="4" HeaderText="91 a 120" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=4" DataNavigateUrlFields="4" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField DataTextField="5" HeaderText="121 a 150" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=5" DataNavigateUrlFields="5" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField DataTextField="6" HeaderText="151 a 180" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=6" DataNavigateUrlFields="6" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField DataTextField="7" HeaderText="181 a 210" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=7" DataNavigateUrlFields="7" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField DataTextField="8" HeaderText="211 a 240" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=8" DataNavigateUrlFields="8" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField DataTextField="9" HeaderText="241 a 270" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=9" DataNavigateUrlFields="9" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField DataTextField="10" HeaderText="271 a 300" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=10" DataNavigateUrlFields="10" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField DataTextField="11" HeaderText="301 a 330" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=11" DataNavigateUrlFields="11" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField DataTextField="12" HeaderText="331 a 360" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=12" DataNavigateUrlFields="12" ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField DataTextField="13" HeaderText="Más de 360" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=13" DataNavigateUrlFields="13" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="row">
            <br /><hr />
            <asp:Repeater ID="rptTramitesAlmacen" runat="server" OnItemCommand="rptTramitesAlmacen_ItemCommand">
                <HeaderTemplate>
                    <table id="example" class="table table-striped table-bordered">
                        <thead>
                            <tr>
                                <th scope="col">Fecha envío</th>
				                <th scope="col">Folio</th>
				                <th scope="col">Estado</th>
				                <th scope="col">Número Póliza</th>
				                <th scope="col">DCN KWIK</th>
				                <th scope="col">Identificador</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("FechaRegistro")%></td>
			            <td><%# Eval("Folio")%></td>
			            <td><%# Eval("Estatus")%></td>
			            <td><%# Eval("IdSisLegados")%></td>
			            <td><%# Eval("kwik")%></td>
			            <td><%# Eval("Prioridad")%></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>