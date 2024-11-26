<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmListaCobranza.aspx.cs" Inherits="WFO.Procesos.Cobranza.frmListaCobranza" MasterPageFile="~/Utilerias/Site.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <br />
    <div style="width:100%">
        <div style="width: 90%; margin:auto;">
            <fieldset>
                <legend>LISTA COBRANZA <asp:Label ID="Label2" runat="server"></asp:Label> </legend>
                <div id="dvCajaTramite" style="width: 100%;">

                    <div id="cajaRptTramite" style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                        <asp:Repeater ID="rptTramite" runat="server" OnItemDataBound="rptTramite_ItemDataBound" OnItemCommand="rptTramite_ItemCommand">
                            <HeaderTemplate>
                                <table id="tblTramite" style="width:100%" class="display" >
                                    <thead>
                                        <%--<th scope="col"></th>--%>
                                        <th scope="col">Folio</th>
                                        <th scope="col">Poliza</th>
                                        <th scope="col">Fecha</th>
                                        <th scope="col">Archivo</th>
                                        <th scope="col">Formato</th>
                                        <th scope="col">Estado</th>
                                        <th scope="col"></th>
                                        <th scope="col"></th>
                                        <th scope="col"></th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <%--<td style="text-align: center;"><%# Eval("Fecha","{0:dd/MM/yyyy HH:mm:ss}")%></td>--%>
<%--                                    <td>
                                        <asp:Panel ID="PanelArchivos" runat="server">
                                        <asp:Repeater id="rptArchivos" runat="server">
                                            <HeaderTemplate>
                                                Archivos Anexados
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <ul style="list-style-type: no">
                                                    <li><%# Eval("Nombre") %></li>
                                                    <li><%# Eval("ArchivoPDF") %></li>
                                                    <li><%# Eval("ArchivoXLS") %></li>
                                                    <li><%# Eval("Archivo100PosPagos") %></li>
                                                    <li><%# Eval("Archivo100PosCanc") %></li>
                                                    <li><%# Eval("CartaPDF") %></li>
                                                    <li><%# Eval("Documento") %></li>
                                                    <li><%# Eval("ArchivosPotenciacion") %></li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        </asp:Panel>
                                    </td>--%>
                                    <td align="center"><%#Eval("Folio")%></td>
                                    <td align="center"><%#Eval("Poliza")%></td>
                                    <td align="center"><%#Eval("Fecha")%></td>
                                    <td align="center">
                                        <a href="./../../ArchivosTemporales/<%#Eval("Nombre")%>"><%#Eval("Nombre")%></a><br />
                                        <%# (String.IsNullOrEmpty(Eval("ArchivoPDF").ToString()) ? string.Empty : string.Format("<a href='../../ArchivosTemporales/{0}' target='_blank'>{0}</a><br />", Eval("ArchivoPDF"))) %>
                                        <%# (String.IsNullOrEmpty(Eval("ArchivoXLS").ToString()) ? string.Empty : string.Format("<a href='../../ArchivosTemporales/{0}' target='_blank'>{0}</a><br />", Eval("ArchivoXLS"))) %>
                                        <%# (String.IsNullOrEmpty(Eval("Archivo100PosPagos").ToString()) ? string.Empty : string.Format("<a href='../../ArchivosTemporales/{0}' target='_blank'>{0}</a><br />", Eval("Archivo100PosPagos"))) %>
                                        <%# (String.IsNullOrEmpty(Eval("Archivo100PosCanc").ToString()) ? string.Empty : string.Format("<a href='../../ArchivosTemporales/{0}' target='_blank'>{0}</a><br />", Eval("Archivo100PosCanc"))) %>
                                        <%# (String.IsNullOrEmpty(Eval("Documento").ToString()) ? string.Empty : string.Format("<a href='../../ArchivosTemporales/{0}' target='_blank'>{0}</a><br />", Eval("Documento"))) %>
                                        <%# (String.IsNullOrEmpty(Eval("CartaPDF").ToString()) ? string.Empty : string.Format("<a href='../../ArchivosTemporales/{0}' target='_blank'>{0}</a><br />", Eval("CartaPDF"))) %>
                                    <td align="center"><%#Eval("Cobertura")%></td>
                                    <td align="center"><%# Estados(Eval("Estado").ToString(), Eval("Cobertura").ToString()) %></td>
                                    <td style="width: 20px; text-align:center"><asp:Image ID="imgEstado" runat="server" ImageUrl="../../Imagenes/bolaGris.png" ToolTip="Estado del proceso"  /></td>
                                    <td style="width: 20px; text-align:center"><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="../../Imagenes/Folder.png" ToolTip="Ver el proceso" CommandName ="Consultar" CommandArgument='<%# Eval("folio") + "," + Eval("cobertura") + "," + Eval("estado")%>' /></td>
                                    <td><asp:LinkButton ID="lnkReasignar" runat="server" Visible="false" CommandArgument='<%#Eval("Folio")%>' OnClick="lnkReasignar_Click" ToolTip="Cambia el analista que procesará este trámite">Reasignar Trámite</asp:LinkButton></td>
                                </tr>
                                
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>

                    <asp:Label ID="lblMensajes" runat="server"></asp:Label>

                </div>
            </fieldset>
        </div>
    </div>



</asp:Content>