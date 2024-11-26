<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Asignar.aspx.cs" Inherits="WFO.Procesos.SupervisionGeneral.Asignar" MasterPageFile="~/Utilerias/Site.Master" EnableEventValidation="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server" EnableViewState="true">
<style>
    th, td { padding: 3px }
</style>
    <script>
        function carga() {
            $('#myModal').modal({backdrop: 'static', keyboard: false});
        }
        function retirar() {
            $('#myModal').modal('toggle'); 
        }
    </script>
    <script>
        function UsuarioRequerido() {
            new PNotify({
                    title:'Usuario requerido !',
                    text: 'Selecciona un operador',
                    type: 'error',
                    styling: 'bootstrap3'
                });
        }
        function UsuarioSuccess() {
            new PNotify({
                    title:'Acción exitosa !',
                    text: 'Tramite asignado correctamente',
                    type: 'success',
                    styling: 'bootstrap3'
            });
        }

        function ErrorAsiganacion(tramite) {
            new PNotify({
                    title:'Advertencia !',
                    text: tramite,
                    type: 'warning',
                    styling: 'bootstrap3'
                });
        }

    </script>

    
    

     <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
     <!-- Modal : Enviar trámite a Mesa -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel3">Asignación de trámite</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <label> Mesa : </label> <asp:Label runat="server" ID="labelNombreMesa"  Font-Bold="True" Text="NumeroFolio" ></asp:Label>
                            <p>Selecciona un operador</p>
                            <br /><br />
                            <asp:HiddenField ID="hfIdTM" runat="server" Value="0" />
                            <asp:HiddenField ID="hfIdTramite" runat="server" Value="0" />

                            <dx:ASPxComboBox ID="ASPxCombo_Usuarios" AutoPostBack="false" runat="server" Theme="Material" EditFormat="Custom" Width="100%">
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator64" validationgroup="AterecionDireciones" controltovalidate="ASPxCombo_Usuarios" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSendToMesa" runat="server" validationgroup="AterecionDireciones" CausesValidation="True"  OnClick="BtnAsignar_Click" Text="Asignar" class="btn btn-success"/>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Asignar</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <table id="tablaBusqueda" runat="server">
                            <tr><td colspan="4"><h3>Buscar por:</h3></td></tr>
                            <tr>
                                <td>Folio</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtFolio" runat="server" class="form-control" Width="150px"></asp:TextBox>
                                </td>
                                <td rowspan="5">
                                    
                                    <asp:Button ID="BtnBuscar" runat="server" CssClass="btn btn-primary" Text="Buscar" OnClick="BtnBuscar_Click" />
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Limpiar" OnClick="BtnLimpiar_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>Fecha Registro del&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtRegistroDel" runat="server" class="form-control" Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRegistroDel" />
                                </td>
                                <td>&nbsp;Al&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtRegistroAl" runat="server" class="form-control" Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtRegistroAl" />
                                </td>
                            </tr>
                            <tr>
                                <td>Fecha Solicitud del&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtSolicitudDel" runat="server" class="form-control" Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtSolicitudDel" />
                                </td>
                                <td>&nbsp;Al&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtSolicitudAl" runat="server" class="form-control" Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSolicitudAl" />
                                </td>
                            </tr>
                            <tr><td>Promotoría</td>
                                <td colspan="3" Width="70%">
                                <dx:ASPxComboBox ID="ComboCatPromotoria" runat="server" Theme="Material" EditFormat="Custom" Width="100%">
                                </dx:ASPxComboBox>
                                                   </td></tr>
                            <tr><td>Estado</td><td colspan="3">
                                <dx:ASPxComboBox ID="ComboCatEstado" runat="server" Theme="Material" EditFormat="Custom" Width="100%">
                                </dx:ASPxComboBox>  </td></tr>
                            <tr><td colspan="5" align="center">&nbsp;</td></tr>
                        </table>
                    </div>
                    <hr />

                    <div class="row">
                        <asp:Repeater ID="rptTramite" runat="server">
                            <HeaderTemplate>
                                <table id="datatableAsignar" class="table table-striped table-bordered jambo_table bulk_action" style='width:100%'>
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Folio</th>
                                            <th>Tipo Trámite</th>
                                            <th>Fecha Registro</th>
                                            <th>Fecha Solicitud</th>
                                            <th>Promotoría</th>
                                            <th>Estado</th>
                                        </tr>
                                    </thead>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><asp:LinkButton ID="LigaAsignar" runat="server" CssClass="btn btn-app" CommandName="Select" CommandArgument='<%# Eval("IdTramite") + "," + Eval("IdUsuario")+ "," + Eval("FolioCompuesto") %>' Text="" OnClick="LigaAsignar_Click"> <i class="fa fa-edit"></i> Asignar </asp:LinkButton></td>
                                    <td><%#Eval("FolioCompuesto")%></td>
                                    <td><%#Eval("TipoTramite")%></td>
                                    <td><%#Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                    <td><%#Eval("FechaSolicitud","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                    <td><%#Eval("Promotoria")%></td>
                                    <td><%#Eval("Estatus")%></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <!-- gridview de asignación de mesas disponibles -->
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:Panel ID="tablaMesas" runat="server" Visible="False">
                                
                                <asp:Button ID="ButtonRegresar" Visible="True" runat="server" Text="Regresar" class="btn btn-primary col-md-1 col-sm-1 col-xs-6" CausesValidation="False"  OnClick="BtnRegresar_Click"  />
                                <br /><br /><br />
                                <p>La asignación de los trámites consultados solo aplica para las mesas con algún estatus de reingresos o pausas.</p>
                                <label> Folio : </label> <asp:Label runat="server" ID="labelFolio"  Font-Bold="True" Text="NumeroFolio" ></asp:Label>
                                <p>Cambio de usuario en mesa. </p>
                                
                                <asp:GridView ID="GVMesas" Visible="false"  runat="server" AutoGenerateColumns="false" DataKeyNames="_IdTramiteMesa" OnRowEditing="GVMesas_RowEditing" OnRowCancelingEdit="GVMesas_RowCancelingEdit" OnRowUpdating="GVMesas_RowUpdating" OnSelectedIndexChanged="GVMesas_SelectedIndexChanged" OnRowDataBound="GVMesas_RowDataBound" class="table table-striped table-bordered">
                                    <Columns>
                                        <asp:BoundField DataField="_Mesa" HeaderText="Mesa" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                        <asp:BoundField DataField="_Estado" HeaderText="Estado" ReadOnly="true" />
                                        <asp:TemplateField HeaderText="Usuario">
                                            <ItemTemplate>
                                                <asp:Label ID="LblUsuario" runat="server" Text='<%# Eval("_Usuario") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <div class="form-group">
                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                        <asp:DropDownList ID="ddlUsuarios" CssClass="select2_single form-control" runat="server"></asp:DropDownList>
                                                    </div>
                                               </div>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="true" ButtonType="Link" EditText="Modificar" CancelText="Cancelar" UpdateText="Cambiar" />
                                    </Columns>
                                </asp:GridView>


                                <asp:Repeater ID="RepeaterMesas" runat="server">
                                    <HeaderTemplate>
                                        <table id="datatable" class="table table-striped table-bordered jambo_table bulk_action" style='width:100%'>
                                            <thead>
                                                <tr>
                                                    <th>Mesa</th>
                                                    <th>Estado</th>
                                                    <th>Usuario</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("Mesa")%></td>
                                            <td><%#Eval("Estatus")%></td>
                                            <td><%#Eval("Usuario")%></td>
                                            <td><asp:LinkButton ID="LinkButton2" Enabled='<%#Eval("Accion").ToString() == "1" ? true : false %>' runat="server" CssClass='<%#Eval("Accion").ToString() == "1" ? "col-md-12 col-sm-12 col-xs-12 btn btn-success" : "col-md-12 col-sm-12 col-xs-12 btn btn-dark" %>' CommandName="Select" CommandArgument='<%# Eval("IdTramiteMesa") + "," + Eval("Mesa") + "," + Eval("IdTramite")%>' Text='<%#Eval("Accion").ToString() == "1" ? "Asignar" : "No aplica" %>' OnClick="AsiganarTramite_Click">  </asp:LinkButton></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>

                            </asp:Panel>
                        </div>
                   </div>
                </div>
            </div>
        </div>
    </div>

    </ContentTemplate>
</asp:UpdatePanel>
    
</asp:Content>