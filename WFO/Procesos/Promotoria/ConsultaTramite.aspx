<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaTramite.aspx.cs" Inherits="WFO.Procesos.Promotoria.ConsultaTramite" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <!-- Campos Ocultos -->
    <div>
        <asp:HiddenField ID="hfTipoTramite" runat="server" Value="0" />
    </div>

    <!-- MODAL DE CARTAS -->
    <div class="modal fade Carta" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel2">PDF <asp:Label ID="LabelTipoCarta" runat="server" ></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="Label4" runat="server" ></asp:Label>
                    <asp:Literal ID="ltMuestraCarta" runat="server"></asp:Literal>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- MODAL DE PDF -->
    <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel2">PDF Expediente</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="MensajePDF" runat="server" ></asp:Label>
                    <asp:Literal ID="ltMuestraPdf" runat="server"></asp:Literal>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- MODAL DE BITACORA -->
    <div class="modal fade bitacora" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel3">Bitácora trámite </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:Repeater ID="rptBitacora" runat="server" >
                                <HeaderTemplate>
                                    <table id="datatable" class="table table-striped table-bordered jambo_table bulk_action">
                                        <thead>
                                            <tr>
                                                <th>Numero</th>
                                                <th>Mesa</th>
                                                <th>Fecha inicio</th>
                                                <th>Fecha termino</th>
                                                <th>Usuario</th>
                                                <th>Estatus mesa</th>
                                                <th>Observación</th>
                                            </tr>
                                        </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Numero")%></td>
                                        <td><%#Eval("Mesa")%></td>
                                        <td><%#Eval("FechaInicio","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                        <td><%#Eval("FechaTermino","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                        <td><%#Eval("Usuario")%></td>
                                        <td><%#Eval("EstatusMesa")%></td>
                                        <td><%#Eval("Observacion")%></td>
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

    <!-- MODAL DE FOLIO -->
    <div class="modal fade bs-example-modal-sm" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel2">Actualización trámite </h4>
                </div>
                <div class="modal-body text-center">
                    <h4>Operación realizada </h4>
                    <br />
                    <h4><asp:Label ID="LabelActualizacionTramite" Text="" runat="server" ></asp:Label></h4>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="Button2" runat="server" Text="Aceptar" class="btn btn-primary" CausesValidation="False" OnClick="TramiteTerminado"  />
                </div>
            </div>
        </div>
    </div>


    <div class="row">
        <!-- INFORMAICON DEL TRAMITE -->
        <div class="col-md-6 col-sm-6 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2><small>Información de trámite</small></h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <asp:Label ID="Label0" runat="server" Text="Nombre" Font-Bold="true" class="control-label col-md-2 col-sm-2 col-xs-6 "></asp:Label>
                    <div class="col-md-10 col-sm-10 col-xs-6 form-group has-feedback">
                        <asp:Label ID="LabelNombre" runat="server" Text="-" Font-Bold="true" class="text-muted font-13 m-b-30"></asp:Label>
                    </div>
                    <%--<asp:Label ID="Label1" runat="server" Text="RFC" Font-Bold="true" class="control-label col-md-2 col-sm-2 col-xs-6 "></asp:Label>
                    <div class="col-md-10 col-sm-10 col-xs-6 form-group has-feedback">
                        <asp:Label ID="LabelRFC" runat="server" Text="-" Font-Bold="true" class="text-muted font-13 m-b-30"></asp:Label>
                    </div>--%>
                    <asp:Label ID="Label2" runat="server" Text="Folio" Font-Bold="true" class="control-label col-md-2 col-sm-2 col-xs-6 "></asp:Label>
                    <div class="col-md-10 col-sm-10 col-xs-6 form-group has-feedback">
                        <asp:Label ID="LabelFolio" runat="server" Text="-" Font-Bold="true" class="text-muted font-13 m-b-30"></asp:Label>
                    </div>
                    <asp:Label ID="Label3" runat="server" Text="Flujo" Font-Bold="true" class="control-label col-md-2 col-sm-2 col-xs-6 "></asp:Label>
                    <div class="col-md-10 col-sm-10 col-xs-6 form-group has-feedback">
                        <asp:Label ID="LabelFlujo" runat="server" Text="-" Font-Bold="true" class="text-muted font-13 m-b-30"></asp:Label>
                    </div>
                    <asp:Label ID="Label5" runat="server" Text="Fecha Registro" Font-Bold="true" class="control-label col-md-3 col-sm-3 col-xs-6 "></asp:Label>
                    <div class="col-md-9 col-sm-9 col-xs-6 form-group has-feedback">
                        <asp:Label ID="LabelFechaRegistro" runat="server" Text="-" Font-Bold="true" class="text-muted font-13 m-b-30"></asp:Label>
                    </div>
                    <button type="button" class="btn btn-default" data-toggle="modal" data-target=".bd-example-modal-lg">Muestra PDF</button>
                </div>
            </div>
        </div>
        <!-- BITACORA -->
        <div class="col-md-6 col-sm-6 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2><small>Estatus trámite</small></h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content text-center">
                    <br />
                    <h2><small><asp:Label ID="LabelEstatusTramite" runat="server" Text=""></asp:Label></small></h2>
                    <br /><br />
                    <button type="button" class="btn btn-default" data-toggle="modal" data-target=".bitacora">Bitácora</button>
                    <br /><br /><br />
                </div>
            </div>
        </div>
    </div>

    <div class="row">

    <asp:UpdatePanel ID="ObservacionesCartaEjecucion" runat="server" UpdateMode="Conditional" Visible="False">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Carta ejecución</small></h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="col-xs-6">
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target=".Carta">Mostrar carta</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="ObservacionesCartaHold" runat="server" UpdateMode="Conditional" Visible="False">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Observaciones</small></h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="col-md-9 col-sm-9 col-xs-6 form-group has-feedback">
                            <p class="text-muted well well-sm no-shadow" style="margin-top: 10px;">
                                <asp:Label ID="LabelObservacionesHold" Text="" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-6 form-group has-feedback">
                            <br />
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target=".Carta">Mostrar carta</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="ObservacionesCartaSuspendido" runat="server" UpdateMode="Conditional" Visible="False">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Observaciones</small></h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="col-md-9 col-sm-9 col-xs-6 form-group has-feedback">
                            <p class="text-muted well well-sm no-shadow" style="margin-top: 10px;">
                                <asp:Label ID="LabelObservacionesSuspendido" Text="" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-6 form-group has-feedback">
                            <br />
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target=".Carta">Mostrar carta</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="ObservacionesCartaRechazo" runat="server" UpdateMode="Conditional" Visible="False">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Observaciones</small></h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="col-md-3 col-sm-3 col-xs-6 form-group has-feedback">
                            <br />
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target=".Carta">Mostrar carta</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="ObservacionesCartaCancelado" runat="server" UpdateMode="Conditional" Visible="False">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Observaciones</small></h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="col-md-3 col-sm-3 col-xs-6 form-group has-feedback">
                            <br />
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target=".Carta">Mostrar carta</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="ObservacionesCartaPCI" runat="server" UpdateMode="Conditional" Visible="False">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Observaciones</small></h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="col-md-3 col-sm-3 col-xs-6 form-group has-feedback">
                            <br />
                            <button type="button" class="btn btn-default" data-toggle="modal" data-target=".Carta">Mostrar carta</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="AnexoArchivos" runat="server" UpdateMode="Conditional" Visible="False">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Archivos Anexos </small></h2>
                        <div class="clearfix"></div>
                        <div class="x_content text-left">
                            <br />
                            <div class="row">
                                <div class=" profile_details">
                                    <div class="col-md-6 col-sm-6 col-xs-12 well profile_view">
                                        <div class="col-xs-12 bottom text-center">
                                            <h4 class="brief">ARCHIVOS CON DOCUMENTOS REQUERIDOS</h4><br />
                                        </div>
                                        <div class="right col-xs-12 text-left">
                                            <asp:UpdatePanel ID="PnlArchivosAnexos" runat="server">
                                                <ContentTemplate>
                                                    <fieldset>
                                                    <asp:Label ID="lblDocumentosRequeridos" runat="server" Text="Archivos (*.PDF, *.JPG, *.PNG)"></asp:Label>
                                                    <asp:FileUpload ID="fileUpDocumento" runat="server"></asp:FileUpload>
                                                    <code><asp:Label ID="LabRespuestaArchivosCarga" runat="server" Text =""></asp:Label></code>
                                                    <br />
                                                    <asp:Button ID="btnSubirDocumento" runat="server" Text="Subir" class="btn btn-primary" CausesValidation="False" OnClick="btnSubirDocumento_Click"/><br />
                                                    <asp:ListBox ID="lstDocumentos" runat="server" Height="100px" Width="100%" SelectionMode="Single" class="select2_multiple form-control">
                                                    </asp:ListBox>
                                                    <br />
                                                    <asp:Button ID="btnEliminaDocumento" runat="server" Text="Eliminar" class="btn btn-danger" CausesValidation="False" OnClick="btnEliminaDocumento_Click" />
                                                </fieldset>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnSubirDocumento" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6 col-xs-12 well profile_view" style="display: none">
                                        <div class="col-xs-12 bottom text-center">
                                            <h4 class="brief">ARCHIVOS ADICIONALES</h4><br />
                                        </div> 
                                        <div class="right col-xs-12 text-left">
                                            <asp:CheckBox ID="CheckBoxInsumos"  runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_Habilita_Insumos" Text="¿Desea agregar archivos adicionales?" />
                                            <asp:UpdatePanel ID="PanelInsumos" runat="server" Visible="False">
                                                <ContentTemplate>
                                                    <fieldset>
                                                        <asp:FileUpload ID="fileUpInsumo" runat="server"></asp:FileUpload>
                                                        <code><asp:Label ID="MensajeInsumos" runat="server" Text =""></asp:Label></code>
                                                        <br />
                                                        <asp:Button ID="btnSubirInsumo" runat="server" Text="Subir" class="btn btn-primary" OnClick="btnSubirInsumo_Click" CausesValidation="False"/><br />
                                                        <asp:ListBox ID="lstInsumos" runat="server" Height="100px" Width="100%" class="select2_multiple form-control" SelectionMode="Single">
                                                        </asp:ListBox>
                                                        <br />
                                                        <asp:Button ID="btnEliminaInsumo" runat="server" Text="Eliminar" class="btn btn-danger" OnClick="btnEliminaInsumo_Click" CausesValidation="False"/>
                                                    </fieldset>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnSubirInsumo" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="RegistrarHold" runat="server" UpdateMode="Conditional" Visible="False">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Observaciones </small></h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content text-left">
                        <!-- OBSERVACIONES -->
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group has-feedback">
                            <asp:TextBox ID="txObervaciones" runat="server" Font-Size="14px" TextMode="MultiLine" Width="100%" class="form-control"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteDetalle" runat="server" FilterMode="ValidChars" TargetControlID="txObervaciones" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ = $%*_0123456789-,.:+@*/?¿+¡\/][{};" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txObervaciones" ErrorMessage="*" InitialValue="" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1 col-sm-1 col-xs-12 text-center">
                                <asp:Button ID="BtnContinuar" runat="server"  AutoPostBack="True" Text="Enviar" Class="btn btn-success" OnClick="BtnContinuar_Click"/>
                            </div>
                            <div class="col-md-4 col-sm-4 col-xs-12 text-center">
                                <code><asp:Label ID="Mensajes" runat="server"></asp:Label></code>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
            
    <asp:UpdatePanel ID="RegistrarSuspendido" runat="server" UpdateMode="Conditional" Visible="False">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Observaciones </small></h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content text-left">
                        <!-- OBSERVACIONES -->
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group has-feedback">
                                <asp:TextBox ID="txObervacionesSuspendido" runat="server" Font-Size="14px" TextMode="MultiLine" Width="100%" class="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" TargetControlID="txObervacionesSuspendido" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ = $%*_0123456789-,.:+*/?¿+¡\/][{};" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txObervacionesSuspendido" ErrorMessage="*" InitialValue="" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1 col-sm-1 col-xs-12 text-center">
                                <asp:Button ID="BtnContinuarSuspendido" runat="server"  AutoPostBack="True" Text="Enviar" Class="btn btn-success" OnClick="BtnContinuarSuspendido_Click"/>
                            </div>
                            <div class="col-md-4 col-sm-4 col-xs-12 text-center">
                                <code><asp:Label ID="MensajeSuspendido" runat="server"></asp:Label></code>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="RegistrarRevisionPromotoria" runat="server" UpdateMode="Conditional" Visible="False">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Observaciones </small></h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content text-left">
                        <!-- OBSERVACIONES -->
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group has-feedback">
                            <asp:TextBox ID="txObervacionesRevisionPromotoria" runat="server" Font-Size="14px" TextMode="MultiLine" Width="100%" class="form-control" ></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars" TargetControlID="txObervacionesRevisionPromotoria" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ = $%*_0123456789-,.:+*/?¿+¡\/][{};" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txObervacionesRevisionPromotoria" ErrorMessage="*" InitialValue="" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1 col-sm-1 col-xs-12 text-center">
                                <asp:Button ID="Button3" runat="server"  AutoPostBack="True" Text="Rechazar" Class="btn btn-danger" OnClick="BtnRechazarRevisionPromotoria_Click"/>
                            </div>
                            <div class="col-md-1 col-sm-1 col-xs-12 text-center">
                                <asp:Button ID="Button1" runat="server"  AutoPostBack="True" Text="Aceptar Póliza" Class="btn btn-success" OnClick="BtnContinuarRevisionPromotoria_Click"/>
                            </div>
                            <div class="col-md-4 col-sm-4 col-xs-12 text-center">
                                <code><asp:Label ID="MensajeRevisionPromotoria" runat="server"></asp:Label></code>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="RegistraPCI" runat="server" UpdateMode="Conditional" Visible="False">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Observaciones </small></h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content text-left">
                        <!-- OBSERVACIONES -->
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group has-feedback">
                            <asp:TextBox ID="txObervacionesPCI" runat="server" Font-Size="14px" TextMode="MultiLine" Width="100%" class="form-control" ></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterMode="ValidChars" TargetControlID="txObervacionesPCI" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ = $%*_0123456789-,.:+*/?¿+¡\/][{};" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txObervacionesPCI" ErrorMessage="*" InitialValue="" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1 col-sm-1 col-xs-12 text-center">
                                <asp:Button ID="Button4" runat="server"  AutoPostBack="True" Text="Enviar" Class="btn btn-success" OnClick="BtnContinuarPCI_Click"/>
                            </div>
                            <div class="col-md-4 col-sm-4 col-xs-12 text-center">
                                <code><asp:Label ID="MensajesPCI" runat="server"></asp:Label></code>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    </div>


</asp:Content>
