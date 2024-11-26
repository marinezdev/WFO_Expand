<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="TramitesListadosAll.aspx.cs" Inherits="WFO.Procesos.Promotoria.TramitesListadosAll" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <script>
        function carga() {
            $('#myModal').modal({ backdrop: 'static', keyboard: false });
        }
        function retirar() {
            $('#myModal').modal('toggle');
        }
    </script>

    <!-- MODAL DE  OPERACIONES -->
    <div class="modal fade bs-example-modal-sm" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel2">
                        <asp:label ID="TituloModal" runat="server" Text="Cargando ... ">
                        </asp:label>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="image view view-first">
                        <img style="width: 100%; display: block;" src="../../Imagenes/default-loader.gif" alt="image">
                    </div>
                </div>
                <div class="modal-footer">
                        
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Trámites por rango de fechas </h2>

                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <p class="text-muted font-13 m-b-30">
                            Listado total de trámites registrados en el rango de 3 meses a partir de la fecha inicial, filtro realizado en base a fecha de registro al sistema.
                        </p>
                        <div class="row">
                            <asp:Label runat="server" ID="label1"  Font-Bold="True" Text="* Desde" class="control-label col-md-2 col-sm-2 col-xs-6"></asp:Label>
                            <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                <dx:ASPxDateEdit ID="dtFechaInicio" runat="server" Theme="Material" EditFormat="Custom" Width="100%" Caption="" >
                                    <TimeSectionProperties>
                                        <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                    </TimeSectionProperties>
                                    <CalendarProperties>
                                        <FastNavProperties DisplayMode="Inline" />
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="dtFechaInicio" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                            </div>

                            <asp:Label runat="server" ID="labelFechaSolicitud"  Font-Bold="True" Text="* Hasta" class="control-label col-md-2 col-sm-2 col-xs-6"></asp:Label>
                            <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                <dx:ASPxDateEdit ID="dtFechaTermino" runat="server" Theme="Material" EditFormat="Custom" Width="100%" Caption="">
                                    <TimeSectionProperties>
                                        <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                    </TimeSectionProperties>
                                    <CalendarProperties>
                                        <FastNavProperties DisplayMode="Inline" />
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator4" controltovalidate="dtFechaTermino" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                            </div>
                            <div class="col-md-1 col-sm-1 col-xs-12 text-center">
                            </div>
                            <div class="col-md-1 col-sm-1 col-xs-12 text-center">
                                
                            </div>
                        </div>
                        <p class="text-muted font-13 m-b-30">
                            <hr />
                            Consulta por filtro.
                        </p>
                        <div class="row">
                            <asp:Label runat="server" ID="Label2" Text="Folio: " Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                            <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                <asp:TextBox ID="TextFolio" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
								<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                            </div>
                            <asp:Label runat="server" ID="Label3" Text="RFC: " Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                            <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                <asp:TextBox ID="TextRFC" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
								<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                            </div>
                        </div>
                        <p class="text-muted font-13 m-b-30">
                            Contratante
                        </p>
                        <div class="row">
                            <asp:Label runat="server" ID="lblNombre" Text="Nombre(s)" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                            <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                <asp:TextBox ID="txNombre" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
								<ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                            </div>
                            <asp:Label runat="server" ID="lblAPaterno" Text="Apellido Paterno" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                            <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                <asp:TextBox ID="txApPat" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApPat" runat="server" FilterMode="ValidChars" TargetControlID="txApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                            </div>
                            <asp:Label runat="server" ID="lblAMaterno" Text="Apellido Materno" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                            <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                <asp:TextBox ID="txApMat" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApMat" runat="server" FilterMode="ValidChars" TargetControlID="txApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                            </div>
                        </div>
                        <div class="row">
                            <hr />
                            <div class="col-md-1 col-sm-1 col-xs-12 text-center">
                                <asp:Button ID="BtnContinuar" runat="server"  AutoPostBack="True" Text="Consultar" Class="btn btn-success" OnClientClick="carga()"  OnClick="BtnConsultar_Click" />
                            </div>
                            <div class="col-md-4 col-sm-4 col-xs-12 text-center">
                                <code><asp:Label ID="Mensajes" runat="server"></asp:Label></code>
                            </div>
                            <div class="col-md-7 col-sm-7 col-xs-12 text-right">
                                <div class="col-md-12 col-sm-12 col-xs-12 text-right">
                                    <asp:Button ID="Button2" runat="server"  AutoPostBack="True" Text="Limpiar" Class="btn btn-primary"  OnClick="BtnLimpiar_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <br />
                            <hr />
                            <asp:Repeater ID="RepeaterFechas" runat="server" OnItemCommand="rptTramite_ItemCommand" Visible="false">
                                <HeaderTemplate>
                                    <table id="example" class="table table-striped table-bordered table-bordered jambo_table bulk_action" style='width:100%'>
                                        <thead>
                                            <tr>
                                                <th>&nbsp;</th>
                                                <th>Fecha envío</th>
                                                <th>Folio de trámite</th>
                                                <th>Número de Solicitud</th>
                                                <th>Información del Contratante</th>
                                                <th>Fecha Firma de Solicitud </th>
                                                <th>Estado</th>
                                                <th>Número de Póliza</th>
                                                <th>Promotoria</th>
                                                <th>GA</th>
                                                <th>GU</th>
                                                <th>Zona</th>
                                                <th>Tipo Servicio</th>
                                                <th>Prima Total</th>
                                                <th>Prima Excedente</th>
                                                <th>Prima Diferencia</th>
                                                <th>&nbsp;</th>
                                            </tr>
                                        </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><asp:ImageButton ID="imbtnConsultar2" runat="server" ImageUrl="~/Imagenes/folder.png" CommandName ="Consultar" CommandArgument='<%# Eval("IdTramite")%>' /></td>

                                        <td><%#Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                        <td><%#Eval("FolioCompuesto")%></td>
                                        <td><%#Eval("NumeroOrden")%></td>
                                        <td><strong></strong><%#Eval("Contratante")%>
                                        <td><%#Eval("FechaSolicitud","{0:dd/MM/yyyy }")%></td>
                                        <td><%#Eval("Estatus")%></td>
                                        <td><%#Eval("IdSisLegados")%></td>
                                        

                                        <td><%#Eval("Promotoria")%></td>
                                        <td><%#Eval("GA")%></td>
                                        <td><%#Eval("GU")%></td>
                                        <td><%#Eval("PromotoriaZona")%></td>
                                        <td><%#Eval("TipoServicio")%></td>
                                        
                                        <td><%#Eval("PrimaTotal")%></td>
                                        <td><%#Eval("PrimaExcedente")%></td>
                                        <td><%#Eval("PrimaDiferencia")%></td>

                                        <td><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/Imagenes/folder.png" CommandName ="Consultar" CommandArgument='<%# Eval("IdTramite")%>' /></td>
                                        
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>