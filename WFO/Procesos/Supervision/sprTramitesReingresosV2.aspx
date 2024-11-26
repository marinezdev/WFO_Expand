<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sprTramitesReingresosV2.aspx.cs" Inherits="WFO.Procesos.Supervision.sprTramitesReingresosV2" MasterPageFile="~/Utilerias/Site.Master"%>
<%@ Register Assembly="DevExpress.Web.v17.2" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblTramitesEspera').DataTable({
                "paging": true,
                "language": {                    
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "All",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": { "sFirst": "Primero", "sLast": "Último", "sNext": "Siguiente", "sPrevious": "Anterior" },
                    "oAria": { "sSortAscending": ": Activar para ordenar la columna de manera ascendente", "sSortDescending": ": Activar para ordenar la columna de manera descendente" }
                }
            });
        });
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>REPORTE DE TRAMITES REINGRESOS</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <table>
                        <tr>
                            <td></td>
                            <td>
                                <dx:ASPxDateEdit ID="DXFechaInicio" runat="server" Theme="iOS" EditFormat="Custom" Width="190" Caption="Fecha Inicio:">
                                    <TimeSectionProperties Adaptive="true">
                                        <TimeEditProperties EditFormatString="hh:mm tt" />
                                    </TimeSectionProperties>
                                    <CalendarProperties>
                                        <FastNavProperties DisplayMode="Inline" />
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <dx:ASPxDateEdit ID="DXFechaTermino" runat="server" Theme="iOS" EditFormat="Custom" Width="190" Caption="Fecha Termino:">
                                    <TimeSectionProperties Adaptive="true">
                                        <TimeEditProperties EditFormatString="hh:mm tt" />
                                    </TimeSectionProperties>
                                    <CalendarProperties>
                                        <FastNavProperties DisplayMode="Inline" />
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="cbFlujos" runat="server" AutoPostBack="false" class="form-control">
                                    <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td valign="top"><asp:Button ID="BtnAceptar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="BtnAceptar_Click" /></td>
                        </tr>
                    </table>
                    <br />

                    <table>
                        <tr>
                            <td valign="top">
                                <asp:GridView ID="GV01" runat="server" CellPadding="7" CellSpacing="5" HeaderStyle-BackColor="#deedf7" HeaderStyle-ForeColor="#2779aa" BorderColor="#aed0ea" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="ProcesosRealizados" HeaderText="Procesos Realizados" />
                                        <asp:BoundField DataField="TotaldeReingresos" HeaderText="Total de Reingresos" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td valign="top">
                                <asp:Chart ID="Chart1" runat="server" Width="600px" Height="500px" BackSecondaryColor="White" BorderColor="#1A3B69" BorderlineDashStyle="Solid" BorderWidth="4px" OnClick="Chart1_Click">
                                    <Titles>
                                        <asp:Title Name="Titulo1" Text=""></asp:Title>
                                    </Titles>
                                    <Legends>
                                        <asp:Legend Alignment="Center" IsTextAutoFit="False" Name="Default" ShadowColor="DarkGray" />
                                    </Legends>
                                    <ChartAreas>
                                        <asp:ChartArea Name="GrupoUno" BorderColor="64, 64, 64, 64"> 
                                            <AxisX IsReversed="true"><LabelStyle Interval="1" /></AxisX>
                                            <AxisY Title="ProcesosRealizados"></AxisY>
                                            <AxisY></AxisY>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </td>
                        </tr>
                    </table>

                    <br />

                    <center>
                        <asp:GridView ID="GV02" runat="server" CellPadding="15" CellSpacing="10" HeaderStyle-BackColor="#deedf7" HeaderStyle-ForeColor="#2779aa" BorderColor="#aed0ea" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="IdTRamite" HeaderText="IdTrámite" />
                                <asp:BoundField DataField="Mesa" HeaderText="Mesa" />
                                <asp:BoundField DataField="TotalProcesos" HeaderText="Procesos Totales" />
                            </Columns>
                        </asp:GridView>
                    </center>

                    <asp:GridView ID="GV03" runat="server" AutoGenerateColumns="false" DataKeyNames="Id" Width="100%" CellPadding="7" CellSpacing="5" HeaderStyle-BackColor="#deedf7" HeaderStyle-ForeColor="#2779aa" BorderColor="#aed0ea">
                        <Columns>
                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Envío" />
                            <asp:BoundField DataField="FolioCompuesto" HeaderText="Número de Trámite" />
                            <asp:BoundField DataField="Contratante" HeaderText="Información del Contratante" />
                            <asp:BoundField DataField="Estatus" HeaderText="Estado" />
                            <asp:BoundField DataField="Fechasolicitud" HeaderText="Fecha Firma de Solicitud" />
                            <asp:BoundField DataField="IdsisLegados" HeaderText="Número de Póliza" />
                            <%--<asp:BoundField DataField="Prioridad" HeaderText="Identificador" />--%>
                            <asp:TemplateField HeaderText="Mostrar">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/Folder.png" CommandName='Consultar' CommandArgument='<%# Eval("Id")%>' OnClick="imbtnConsultar_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>


                </div>
            </div>
        </div>
    </div>
</asp:Content>