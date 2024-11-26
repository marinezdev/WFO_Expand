<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WFO.Procesos.Promotoria.EsperaPromotoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

    <!-- Modal : Ayuda Trámites Status -->
    <div class="modal fade mTramitesStatusInfo" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel3">Información Status Trámites</h4>
                </div>
                <div class="modal-body" >
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <ul>
                                <li><strong>Registro. </strong>El trámite es ingresado por la promotoría.</li>
                                <li><strong>Proceso. </strong>El trámite se encuentra en proceso (se encuentra en operación por los participantes del proceso).</li>
                                <li><strong>Ejecución. </strong>El trámite fue ejecutado exitosamente. Ya cuenta con la póliza para el solicitante.</li>
                                <li><strong>Hold. </strong>El trámite se encuentra en pausa para que la promotoría anexe información requerida.</li>
                                <li><strong>Suspendido. </strong>El trámite se encuentra suspendido para que la promotoría anexe información requerida.</li>
                                <li><strong>PCI. </strong>El trámite se encuentra suspendido para que la promotoría vuelva a cargar la solicitud sin información confidencial del solicitante.</li>
                                <li><strong>Promotoría Cancela. </strong>La promotoría cancela el trámite.</li>
                                <li><strong>Cancelado. </strong>El supervisor u operador realiza la cancelación del trámite.</li>
                                <li><strong>Caducado. </strong>El trámite cuenta con más de 30 días naturales, el sistema automáticamente cambia el trámite por caducado.</li>
                            </ul>

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
        <script src="../../JS/Chart.js/dist/Chart.bundle.min.js"></script>
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Indicador General <small>Trámites</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li>
                            <!--<a id="link" download="Indicador General.jpg"><i class="fa fa-cloud-download"></i> jpg</a>-->
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <p class="text-muted font-13 m-b-30">
                        Indicador general correspondiente a trámites registrados en el rango de 4 meses, a partir de la fecha del día de hoy. <asp:Label runat="server" ID="Label2" Text=""></asp:Label>
                    </p>
                    <div class="col-md-4 col-sm-4 col-xs-12 form-group has-feedback">
                        <asp:DropDownList ID="cboMonthFilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Month_SelectedIndexChanged" class="form-control" Visible="true">
                            <asp:ListItem Value="1">Enero</asp:ListItem>
                            <asp:ListItem Value="2">Febrero</asp:ListItem>
                            <asp:ListItem Value="3">Marzo</asp:ListItem>
                            <asp:ListItem Value="4">Abril</asp:ListItem>
                            <asp:ListItem Value="5">Mayo</asp:ListItem>
                            <asp:ListItem Value="6">Junio</asp:ListItem>
                            <asp:ListItem Value="7">Julio</asp:ListItem>
                            <asp:ListItem Value="8">Agosto</asp:ListItem>
                            <asp:ListItem Value="9">Septiembre</asp:ListItem>
                            <asp:ListItem Value="10">Octubre</asp:ListItem>
                            <asp:ListItem Value="11">Noviembre</asp:ListItem>
                            <asp:ListItem Value="12">Diciembre</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LisEStatusTramite" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4 col-sm-4 col-xs-12 form-group has-feedback">
                        <asp:DropDownList ID="LisEstatusTramite" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisEstatusTramite_SelectedIndexChanged" class="form-control" Visible="false">
                            <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="LisEStatusTramite" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div style="text-align:right;">
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-3">
                            <a href="#" data-toggle="modal" title="Información Trámites" data-target=".mTramitesStatusInfo">
                                <img src="../../Imagenes/helpmodal.png" width="32px" />
                            </a>
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>

                    <div id="dvGrafUno" style="width: 700px; margin: auto;">
                        <asp:Chart ID="grfGrupoUno" runat="server" Width="700px" Height="300px" BackColor="211, 223, 240" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="#1A3B69" BorderlineDashStyle="Solid" BorderWidth="2px" OnClick="grfGrupoUno_Click" >
                            <ChartAreas>
                                <asp:ChartArea Name="GrupoUno" BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" ShadowColor="Transparent">
                                    <%--<Area3DStyle Enable3D="True" IsRightAngleAxes="False" LightStyle="Realistic" Inclination="20" Rotation="15"/>--%>
                                    <AxisY LineColor="64, 64, 64, 64" IsMarginVisible="false">
                                        <LabelStyle Font="Trebuchet MS, 8pt" />
                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                    </AxisY>
                                    <AxisX LineColor="64, 64, 64, 64" IsMarginVisible="false">
                                        <LabelStyle Font="Trebuchet MS, 8pt" />
                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:Literal ID="ltTemp" runat="server"></asp:Literal><br />
                    </div>


                    <!-- NUEVA GRAFICA  -->
                    
                    <asp:Literal ID="ltMuestraGrafica" runat="server" Visible="false"></asp:Literal>
                    

                </div>
            </div>
        </div>
    </div>

    <asp:panel ID="ListaTramitesEstatus" runat="server" Visible="false">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Trámites - <asp:Label runat="server" ID="LabelEstado" Text=""></asp:Label> </h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <p class="text-muted font-13 m-b-30">
                        Listado total de trámites gráfica. <asp:Label runat="server" ID="LabRespyuesta" Text=""></asp:Label>
                    </p>
                    <asp:Repeater ID="rptTramite" runat="server" OnItemCommand="rptTramite_ItemCommand">
                        <HeaderTemplate>
                            <table id="example" class="table table-striped table-bordered jambo_table bulk_action">
                                <thead>
                                    <tr>
                                        <th>Fecha envío</th>
                                        <th>Número de trámite</th>
                                        <th>Orden de Trabajo</th>
                                        <th>Operación</th>
                                        <th>Producto</th>
                                        <th>Información del Contratante</th>
                                        <th>Fecha Firma de Solicitud </th>
                                        <th>Estado</th>
                                        <th>Número De Póliza De Los Sistemas Legados</th>
                                        <th>KWIK</th>
                                        <th></th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                <td><%#Eval("FolioCompuesto")%></td>
                                <td><%#Eval("NumeroOrden")%></td>
                                <td><%#Eval("Operacion")%></td>
                                <td><%#Eval("Producto")%></td>
                                <td><strong>Nombre: </strong><%#Eval("Contratante")%> <br /><strong>RFC: </strong><%#Eval("RFC")%><br /><%#Eval("Titular")%></td>
                                <td><%#Eval("FechaSolicitud","{0:dd/MM/yyyy }")%></td>
                                <td><%#Eval("Estatus")%></td>
                                <td><%#Eval("IdSisLegados")%></td>
                                <td><%#Eval("kwik")%></td>
                                <td><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/Imagenes/folder.png" CommandName ="Consultar" CommandArgument='<%# Eval("Id")%>' /></td>
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
    </asp:panel>
    
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
