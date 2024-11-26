<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="NuevoPamExpedienteFisico.aspx.cs" Inherits="WFO.Procesos.Promotoria.NuevoPamExpedienteFisico" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
        <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="modal fade bd-example-modal-sm" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModal" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                      <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLongTitle">Confirmación</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                        </button>
                      </div>
                      <div class="modal-body">
                        ¿Deseas continuar con el trámite?
                      </div>
                      <div class="modal-footer">
                          <asp:Button ID="Button1" runat="server"  AutoPostBack="True" Text="Continuar" CssClass="btn btn-info" OnClick="Continuar" />&nbsp;&nbsp;
                          <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                      </div>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="x_panel">
                    <br />
                    <div class="container">
                        <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                            <div class="card">
                                <div class="card-header">
                                <h2><small style="color:#007CC3"> TRAMITE NUEVO - EXPAND EXPEDIENTE FÍSICO    </small></h2>
                                </div>
                                <div class="card-body">
                                    <div class="form-group">
                                        <h2><small style="color:#007CC3">PÓLIZA /SEGURO</small></h2>
                                    </div>
                                    <asp:Panel ID="Limpiar" runat="server" Visible="false">
                                        <asp:Button ID="btnlimpiar" runat="server" CssClass="btn btn-info col-md-2 col-sm-12" AutoPostBack="True" CausesValidation="false" OnClick="BtnLimpiar" Text="Limpiar" Visible="true"/>&nbsp;&nbsp;
                                    </asp:Panel>
                                    <hr />
                                    <!-- TOPO DE SERVICIO. -->
                                    <div class="row">
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <asp:Panel ID="Panel1" runat="server" Visible="true">
                                                <asp:Panel ID="Panel2" runat="server" Visible="true">
                                                    <asp:Label ID="Label1" runat="server" Text="* Tipo Servicio" Font-Bold="true" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                                    <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                                        <asp:DropDownList ID="LisTipoServicio" runat="server" AutoPostBack="True"  class="form-control">
                                                            <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                                        </asp:DropDownList>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LisTipoServicio" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                   </div>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <h2><small style="color:#007CC3">INFORMACIÓN DE LA PÓLIZA</small></h2>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <asp:Label runat="server" ID="LabelClave" Text="Clave promotoria" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                            <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                                <asp:TextBox ID="texClave" runat="server" MaxLength="5" AutoPostBack="false" class="form-control"></asp:TextBox>
                                            </div>
                                            <asp:Label runat="server" ID="lblFechaSolicitud" Text="* Fecha Solicitud" Font-Bold="True" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                            <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                                <dx:ASPxDateEdit ID="dtFechaSolicitud" runat="server" Theme="Material" EditFormat="Custom" Width="100%" Caption="" AutoPostBack="true">
                                                    <TimeSectionProperties>
                                                        <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                                    </TimeSectionProperties>
                                                    <CalendarProperties>
                                                        <FastNavProperties DisplayMode="Inline" />
                                                    </CalendarProperties>
                                                </dx:ASPxDateEdit>
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator4" controltovalidate="dtFechaSolicitud" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            </div>
                                            <asp:Label runat="server" ID="Label6" Text="* Número de póliza o solicitud" Font-Bold="True" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                            <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                                <asp:TextBox ID="textNumeroOrden" runat="server" MaxLength="15" AutoPostBack="false" class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="textNumeroOrden" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                             <asp:Label runat="server" ID="Label4" Text="Región" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                             <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                                 <asp:TextBox ID="texRegion" runat="server" MaxLength="5" AutoPostBack="false" TextMode="MultiLine" Rows="1" class="form-control"></asp:TextBox>
                                             </div>
                                            <asp:Label runat="server" ID="Label5" Text="Gerente comercial" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                            <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                                <asp:TextBox ID="texGerenteComercial" runat="server" TextMode="MultiLine" Rows="1" AutoPostBack="false"  class="form-control"></asp:TextBox>
                                            </div>
                                            <asp:Label runat="server" ID="Label7" Text="Ejecutivo comercial" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                            <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                                <asp:TextBox ID="texEjecuticoComercial" runat="server" TextMode="MultiLine" Rows="1"  AutoPostBack="false" class="form-control"></asp:TextBox>
                                            </div>
                                            <asp:Label runat="server" ID="Label8" Text="Ejecutivo Front" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                            <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                                <asp:TextBox ID="texEjecuticoFront" runat="server" TextMode="MultiLine" Rows="1"  AutoPostBack="false" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnPrsFisica" runat="server" Visible="true">
                                    <div class="form-group">
                                        <h2><small style="color:#007CC3">INFORMACIÓN CONTRATANTE</small></h2>
                                    </div>
                                    <hr />   
                                    <div class="row">
                                        <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label runat="server" ID="lblNombre" Text="*Nombre(s)" Font-Bold="True" class="control-label"></asp:Label>
                                            <div class="form-group has-feedback">
                                                <asp:TextBox ID="txNombre" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
								                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txNombre" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label runat="server" ID="lblAPaterno" Text="*Apellido Paterno" Font-Bold="True" class="control-label"></asp:Label>
                                            <div class="form-group has-feedback">
                                                <asp:TextBox ID="txApPat" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApPat" runat="server" FilterMode="ValidChars" TargetControlID="txApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator22" controltovalidate="txApPat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label runat="server" ID="lblAMaterno" Text="*Apellido Materno" Font-Bold="True" class="control-label"></asp:Label>
                                            <div class="form-group has-feedback">
                                                <asp:TextBox ID="txApMat" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApMat" runat="server" FilterMode="ValidChars" TargetControlID="txApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator11" controltovalidate="txApMat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            </div>
                                        </div>
                                    </div>
                                    </asp:Panel>
                                    <div class="form-group">
                                        <h2><small style="color:#007CC3">OBSERVACIONES</small></h2>
                                    </div>
                                    <hr /> 
                                    <div class="col-md-9 col-sm-9 col-xs-12 form-group has-feedback">
                                        <asp:TextBox ID="txDetalle" runat="server" Font-Size="14px" TextMode="MultiLine" class="form-control" ></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteDetalle" runat="server" FilterMode="ValidChars" TargetControlID="txDetalle" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ = $%*_0123456789-,.:+*/?¿+¡\/][{};" />
                                        <br />
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <asp:Button ID="BtnContinuar" runat="server"  AutoPostBack="True" Text="Continuar" CssClass="btn btn-info col-md-12 col-sm-12" OnClick="BtnContinuar_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger col-md-12 col-sm-12" OnClick="BtnCancelar_Click" CausesValidation="false" />
                                        <asp:Label ID="Mensajes" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>
                                        <asp:Label ID="SumaBasica" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function calcula(idForm) {

            var valor = parseFloat($("#" + idForm).val());
            var n = new Intl.NumberFormat("es-MX").format(valor);
            var s = "0.00";

            $("#" + idForm).val(n);
        }

        function MASK(idForm, mask, format) {
            var n = $("#" + idForm).val();
            if (format === "undefined") format = false;
            if (format || NUM(n)) {
                dec = 0, point = 0;
                x = mask.indexOf(".") + 1;
                if (x) { dec = mask.length - x; }

                if (dec) {
                    n = NUM(n, dec) + "";
                    x = n.indexOf(".") + 1;
                    if (x) { point = n.length - x; } else { n += "."; }
                } else {
                    n = NUM(n, 0) + "";
                }
                for (var x = point; x < dec; x++) {
                    n += "0";
                }
                x = n.length, y = mask.length, XMASK = "";
                while (x || y) {
                    if (x) {
                        while (y && "#0.".indexOf(mask.charAt(y - 1)) === -1) {
                            if (n.charAt(x - 1) !== "-")
                                XMASK = mask.charAt(y - 1) + XMASK;
                            y--;
                        }
                        XMASK = n.charAt(x - 1) + XMASK, x--;
                    } else if (y && "$0".indexOf(mask.charAt(y - 1)) + 1) {
                        XMASK = mask.charAt(y - 1) + XMASK;
                    }
                    if (y) { y-- }
                }
            } else {
                XMASK = "";
            }
            $("#" + idForm).val(XMASK);
            return XMASK;
        }

        // Convierte una cadena alfanumérica a numérica (incluyendo formulas aritméticas)
        //
        // s   = cadena a ser convertida a numérica
        // dec = numero de decimales a redondear
        //
        // La función devuelve el numero redondeado

        function NUM(s, dec) {
            for (var s = s + "", num = "", x = 0; x < s.length; x++) {
                c = s.charAt(x);
                if (".-+/*".indexOf(c) + 1 || c !== " " && !isNaN(c)) { num += c; }
            }
            if (isNaN(num)) { num = eval(num); }
            if (num === "") { num = 0; } else { num = parseFloat(num); }
            if (dec !== undefined) {
                r = .5; if (num < 0) r = -r;
                e = Math.pow(10, (dec > 0) ? dec : 0);
                return parseInt(num * e + r) / e;
            } else {
                return num;
            }
        }

    </script>

    <script type="text/javascript"> 
        function WebForm_OnSubmit() {
            if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) {
                for (var i in Page_Validators) {
                    try {
                        var control = document.getElementById(Page_Validators[i].controltovalidate);
                        if (!Page_Validators[i].isvalid) {
                            control.className = "form-control-error";
                        } else {
                            control.className = "form-control";
                        }
                    } catch (e) { }
                }
                return false;
            }
            return true;
        }
    </script> 

</asp:Content>
