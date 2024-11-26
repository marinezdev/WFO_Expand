<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="NuevoINE.aspx.cs" Inherits="WFO.Procesos.Promotoria.NuevoTramiteN4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
<script>
        function MASK(idForm, mask, format) {
            var n = $("#" + idForm).val();
            if (format == "undefined") format = false;
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
                        while (y && "#0.".indexOf(mask.charAt(y - 1)) == -1) {
                            if (n.charAt(x - 1) != "-")
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
                if (".-+/*".indexOf(c) + 1 || c != " " && !isNaN(c)) { num += c; }
            }
            if (isNaN(num)) { num = eval(num); }
            if (num == "") { num = 0; } else { num = parseFloat(num); }
            if (dec != undefined) {
                r = .5; if (num < 0) r = -r;
                e = Math.pow(10, (dec > 0) ? dec : 0);
                return parseInt(num * e + r) / e;
            } else {
                return num;
            }
        }
        function nombreDeLaFuncion() {
            location.reload();
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
    <script>
        function ErrorArchvios() {
            new PNotify({
                title: 'Advertencia. !',
                text: 'Uno de tus archivos se encuentra dañado y no es posible procesarlo.',
                type: 'error',
                styling: 'bootstrap3'
            });
        }
        function FormularioIncompleto() {
            new PNotify({
                title: 'Formulario incompleto. !',
                text: 'No se han subido archivos al expediente.',
                type: 'error',
                styling: 'bootstrap3'
            });
        }
    </script>
    <!-- Campos Ocultos -->
    <div>
        <asp:HiddenField ID="hfTipoTramite" runat="server" Value="0" />
    </div>

     <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- MODAL DE FOLIO -->
            <div class="modal fade bs-example-modal-sm" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="myModalLabel2">Registro trámite </h4>
                        </div>
                        <div class="modal-body">
                            <h4>Trámite</h4>
                            <p>Trámite registrado exitosamente con el folio:</p>
                            <h4><asp:Label ID="LabelFolio" runat="server" ></asp:Label></h4>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="Button1" runat="server" Text="Cerrar" class="btn btn-default" CausesValidation="False" OnClick="TramiteTerminado"  />
                            <asp:Button ID="Button2" runat="server" Text="Aceptar" class="btn btn-primary" CausesValidation="False" OnClick="TramiteTerminado"  />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2><small>Nuevo tramite - INE</small></h2>
                            <ul class="nav navbar-right panel_toolbox">
                                <li>
                                    <a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      	        </li>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content text-left">
                            <div class="form-group">
                                <h2><small>Póliza / Seguro</small></h2>
                            </div>
                            <hr />
                            <!-- SECCIÓN PRODUCTO Y PLAN -->
                            <div class="row">
                                <asp:Panel ID="producto1" runat="server" Visible="true">
                                    <asp:Label ID="lblProducto" runat="server" Text="*Producto" Font-Bold="true" class="control-label col-md-1 col-sm-1 col-xs-6 "></asp:Label>
                                    <div class="col-md-4 col-sm-4 col-xs-12 form-group has-feedback">
                                        <asp:DropDownList ID="LisProducto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisProducto_SelectedIndexChanged" class="form-control">
                                            <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="LisProducto" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="subproducto" runat="server" Visible="true">
                                    <asp:Label ID="LabPlan" runat="server" Text="*Plan" Font-Bold="true" class="control-label col-md-2 col-sm-2 col-xs-6 "></asp:Label>
                                    <div class="col-md-4 col-sm-4 col-xs-12 form-group has-feedback">
                                        <asp:DropDownList ID="LisSubproducto" runat="server" AutoPostBack="True" class="form-control">
                                            <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LisSubproducto" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </asp:Panel>
                            </div>

                            <!-- SECCIÓN CANTIDADES -->
                            <div class="row">
                                <asp:Label runat="server" ID="Moneda" Text="*Moneda" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6" ></asp:Label>
                                <div class="col-md-2 col-sm-2 col-xs-6 form-group has-feedback">
                                    <asp:DropDownList ID="cboMoneda" runat="server" AutoPostBack="True" TabIndex="1"  class="form-control" ></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="cboMoneda" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <asp:Label runat="server" ID="SumaAsegurada" Text="* Prima Total de Acuerdo a Cotización" Font-Bold="True" class="control-label col-md-2 col-sm-2 col-xs-6"></asp:Label>
                                <div class="col-md-2 col-sm-2 col-xs-6 form-group has-feedback">
                                    <asp:TextBox ID="txtPrimaTotalGMM"  onChange="MASK('ContenidoPrincipal_txtPrimaTotalGMM','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" AutoPostBack="true" class="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterMode="ValidChars" TargetControlID="txtPrimaTotalGMM" ValidChars="0123456789.," />
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator27" InitialValue="0.00"  ControlToValidate="txtPrimaTotalGMM" ErrorMessage="*" runat="server" ForeColor="Red"/>
                                </div>
                                <asp:Label runat="server" ID="Label1" Text="* Suma Asegurada Básica" Font-Bold="True" class="control-label col-md-2 col-sm-2 col-xs-6"></asp:Label>
                                <div class="col-md-2 col-sm-2 col-xs-6 form-group has-feedback">
                                    <asp:TextBox ID="txtSumaAseguradaBasica"  onChange="MASK('ContenidoPrincipal_txtSumaAseguradaBasica','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" AutoPostBack="true" class="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" TargetControlID="txtSumaAseguradaBasica" ValidChars="0123456789.," />
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator2" InitialValue="0.00"  ControlToValidate="txtSumaAseguradaBasica" ErrorMessage="*" runat="server" ForeColor="Red"/>
                                </div>
                                
                            </div>
                            <div class="row">
                                <asp:Label runat="server" ID="Label2" Text="Hombres clave" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                <div class="col-md-2 col-sm-2 col-xs-6 form-group has-feedback">
                                <asp:CheckBox ID="HombresClave"  runat="server" AutoPostBack="True" Text="Si"  />
                                </div>
                             </div>

                            <div class="form-group">
                                    <h2><small>Información de la póliza</small></h2>
                                </div>
                            <hr />
                            <!-- INFORMACIÓN DE PÓLIZA  -->
                            <div class="row">
                                <asp:Label runat="server" ID="labelClavePromotoria" Text="Clave Promontoría" class="control-label col-md-2 col-sm-2 col-xs-6"></asp:Label>
                                <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <!-- <asp:TextBox ID="texClavePromotoria" runat="server" MaxLength="5" AutoPostBack="false" Enabled="false" Visible="false"></asp:TextBox> -->
                                    <asp:TextBox ID="texClave" runat="server" MaxLength="5" AutoPostBack="false" class="form-control" disabled="disabled"></asp:TextBox>
                                </div>
                            
                                <asp:Label runat="server" ID="labelRegion" Text="Región" class="control-label col-md-2 col-sm-2 col-xs-6"></asp:Label>
                                <div class="col-md-5 col-sm-5 col-xs-12 form-group has-feedback">
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                    <asp:TextBox ID="texRegion" runat="server" MaxLength="5" AutoPostBack="false" class="form-control" disabled="disabled" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Label runat="server" ID="labelFechaSolicitud"  Font-Bold="True" Text="* Fecha Solicitud" class="control-label col-md-2 col-sm-2 col-xs-6"></asp:Label>
                                <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
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
                                <asp:Label runat="server" ID="labelGerenteComercial" Text="Gerente comercial" class="control-label col-md-2 col-sm-2 col-xs-6"></asp:Label>
                                <div class="col-md-5 col-sm-5 col-xs-12 form-group has-feedback">
                                    <asp:HiddenField ID="HiddenField4" runat="server" />
                                    <asp:TextBox ID="texGerenteComercial" runat="server" class="form-control" TextMode="MultiLine" Rows="1" AutoPostBack="false" disabled="disabled" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Label runat="server" ID="labelSolicituNumeroOrden" Text="Solicitud / Número de Orden" class="control-label col-md-2 col-sm-2 col-xs-6"></asp:Label>
                                <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                    <asp:TextBox ID="textNumeroOrden" runat="server" MaxLength="15" class="form-control" AutoPostBack="false" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                </div>
                                <asp:Label runat="server" ID="labelEjecutivoComercial" Text="Ejecutivo comercial" class="control-label col-md-2 col-sm-2 col-xs-6"></asp:Label>
                                <div class="col-md-5 col-sm-5 col-xs-12 form-group has-feedback">
                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                    <asp:TextBox ID="texEjecuticoComercial" runat="server" class="form-control" TextMode="MultiLine" Rows="1"  AutoPostBack="false" disabled="disabled"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Label runat="server" ID="lblTipoContratante" Text="* Tipo de Contratante" Font-Bold="True" class="control-label col-md-2 col-sm-2 col-xs-6"></asp:Label>
                                <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                    <asp:DropDownList ID="cboTipoContratante" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipoContratante_SelectedIndexChanged" class="form-control">
                                        <asp:ListItem Value="0">SELECCIONAR</asp:ListItem>
                                        <asp:ListItem Value="Fisica">PERSONA FÍSICA</asp:ListItem>
                                        <asp:ListItem Value="Moral">PERSONA MORAL</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTipoContratante" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" Font-Size="16px"></asp:RequiredFieldValidator>
                                </div>
                                <asp:Label runat="server" ID="LabelEjecutivoFront" Text="Ejecutivo Front" class="control-label col-md-2 col-sm-2 col-xs-6"></asp:Label>
                                <div class="col-md-5 col-sm-5 col-xs-12 form-group has-feedback">
                                    <asp:TextBox ID="texEjecuticoFront" runat="server" class="form-control" TextMode="MultiLine" Rows="1"  AutoPostBack="false" disabled="disabled"></asp:TextBox>
                                </div>
                            </div>
                            <!-- PERSONA FISICA -->
                            <asp:Panel ID="pnPrsFisica" runat="server" Visible="False">
                                <div class="form-group">
                                    <h2><small>Información contratante </small></h2>
                                </div>
                                <hr />
                                <div class="row">
                                    <asp:Label runat="server" ID="lblNombre" Text="*Nombre(s)" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <asp:TextBox ID="txNombre" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
								        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txNombre" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                    <asp:Label runat="server" ID="lblAPaterno" Text="*Apellido Paterno" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <asp:TextBox ID="txApPat" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApPat" runat="server" FilterMode="ValidChars" TargetControlID="txApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator22" controltovalidate="txApPat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                    <asp:Label runat="server" ID="lblAMaterno" Text="*Apellido Materno" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <asp:TextBox ID="txApMat" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApMat" runat="server" FilterMode="ValidChars" TargetControlID="txApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator11" controltovalidate="txApMat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label runat="server" ID="Label8" Text="* Sexo" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <asp:DropDownList ID="txSexo" runat="server" class="form-control">
                                            <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                            <asp:ListItem Value="M">MASCULINO</asp:ListItem>
                                            <asp:ListItem Value="F">FEMENINO</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="txSexo" ForeColor="Red" InitialValue="" Font-Size="16px"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:Label runat="server" ID="lblRFCPFisica" Text="* RFC" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <div class="input-group col-xs-11">
                                        <asp:TextBox ID="txRfc" runat="server" MaxLength="13" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:Button  runat="server" CausesValidation="False" Text="Calcular" class="btn btn-primary" ToolTip="RFC" OnClick="dtFechaNacimiento_OnChanged" />
                                        </span>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txRfc" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="rev_txRfc" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfvRfc" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-4 col-xs-12 text-center">
                                        <code><asp:Label runat="server" ID="LabelRespuestaRFCFisico" Text=""></asp:Label></code>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label runat="server" ID="LabelFechaNacimiento" Text="Fecha Nacimiento" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <dx:ASPxDateEdit ID="dtFechaNacimiento" runat="server" Theme="Material" EditFormat="Custom" Width="100%" SelectedIndex="137" AutoPostBack="true" OnDateChanged="dtFechaNacimiento_OnChanged">
                                            <TimeSectionProperties>
                                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                            </TimeSectionProperties>
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline" />
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator23" controltovalidate="dtFechaNacimiento" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>

                                    <asp:Label runat="server" ID="LabelNacionalidadFisica" Text="Nacionalidad" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <dx:ASPxComboBox ID="txNacionalidad" runat="server" SelectedIndex="136" AutoPostBack="true" Theme="Material" EditFormat="Custom" Width="100%" OnSelectedIndexChanged="LisNacionalidad_SelectedIndexChanged">
                                        </dx:ASPxComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txNacionalidad" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-4 col-sm-4 col-xs-12 text-center">
                                        <code><asp:Label runat="server" ID="LabelRespuestaNacionalidadFisico" Text=""></asp:Label></code>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnPrsMoral" runat="server" Visible="False">
                                <div class="form-group">
                                    <h2><small>Información contratante </small></h2>
                                </div>
                                <hr />
                                <div class="row">
                                    <asp:Label runat="server" ID="lblNombrePMoral" Text="*Nombre" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"/>
                                    <div class="col-md-7 col-sm-7 col-xs-12 form-group has-feedback">
                                        <asp:TextBox ID="txNomMoral" runat="server" MaxLength="100" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteNomMoral" runat="server" FilterMode="ValidChars" TargetControlID="txNomMoral" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZáéíóúÁÉÍÓÚ&" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" controltovalidate="txNomMoral" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label runat="server" ID="LabelMoralFechaConstitucion" Text="*Fecha de Constitución" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <dx:ASPxDateEdit ID="dtFechaConstitucion" runat="server" Theme="Material" EditFormat="Custom" Width="100%" Caption="" AutoPostBack="true" OnDateChanged="dtFechaConstitucion_OnChanged">
                                            <TimeSectionProperties>
                                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                            </TimeSectionProperties>
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline"/>
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator10" controltovalidate="dtFechaConstitucion" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                    <asp:Label runat="server" ID="lblRFCPMoral" Text="*RFC" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <div class="input-group col-xs-11">
                                        <asp:TextBox ID="txRfcMoral" runat="server" MaxLength="12" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:Button  runat="server" CausesValidation="False" Text="Calcular" class="btn btn-primary" ToolTip="RFC" OnClick="dtFechaConstitucion_OnChanged" />
                                        </span>
                                        </div>
                                    
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteRfcMoral" runat="server" FilterMode="ValidChars" TargetControlID="txRfcMoral" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="revRfcMoral" runat="server" ControlToValidate="txRfcMoral" ErrorMessage="*" Font-Size="16px" ForeColor="Red" ValidationExpression="^[a-zA-Z]{3,4}(\d{6})((\D|\d){3})?$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator6" controltovalidate="txRfcMoral" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                    <div class="col-md-4 col-sm-4 col-xs-12 text-center">
                                        <code><asp:Label runat="server" ID="LabelRespuestaRFCMoral" Text=""></asp:Label></code>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Label runat="server" ID="LabelMismoContratante" Text="¿El solicitante es el mismo que el contratante?" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                            <asp:CheckBox ID="CheckBox2"  runat="server" AutoPostBack="True" oncheckedchanged="CheckBox2_CheckedChanged" Text="Si" Checked="true" />
                            <asp:CheckBox ID="CheckBox1"  runat="server" AutoPostBack="True" oncheckedchanged="CheckBox1_CheckedChanged" Text="No" /> 
                            <asp:Panel ID="DiferenteContratante" runat="server" Visible="False">
                                <div class="form-group">
                                    <h2><small>Información titular</small></h2>
                                </div>
                                <hr />
                                <div class="row">
                                    <asp:Label runat="server" ID="LabelTitularNombre" Text="*Nombre(s)" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <asp:TextBox ID="txTiNombre" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars" TargetControlID="txTiNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator13" controltovalidate="txTiNombre" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                    <asp:Label runat="server" ID="LabelTitularApellidoPaterno" Text="*Apellido paterno" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <asp:TextBox ID="txTiApPat" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterMode="ValidChars" TargetControlID="txTiApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator14" controltovalidate="txTiApPat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                    <asp:Label runat="server" ID="LabelApellidoMaterno" Text="*Apellido materno" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <asp:TextBox ID="txTiApMat" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterMode="ValidChars" TargetControlID="txTiApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator15" controltovalidate="txTiApMat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label runat="server" ID="LabelTitularNacionalidad" Text="*Nacionalidad" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <dx:ASPxComboBox ID="txTiNacionalidad" runat="server" Theme="Material" EditFormat="Custom" Width="100%" SelectedIndex="136" AutoPostBack="true" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()" OnSelectedIndexChanged="LisTitNacionalidad_SelectedIndexChanged">
                                        </dx:ASPxComboBox>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator16" controltovalidate="txTiNacionalidad" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                    <asp:Label runat="server" ID="LabelTitularSexo" Text="*Sexo" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <asp:DropDownList ID="txtSexoM" runat="server" class="form-control">
                                            <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                            <asp:ListItem Value="M">MASCULINO</asp:ListItem>
                                            <asp:ListItem Value="F">FEMENINO</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="txtSexoM" ForeColor="Red" InitialValue="" Font-Size="16px"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:Label runat="server" ID="LabelTitularFechaNacimiento" Text="*Fecha Nacimiento" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                        <dx:ASPxDateEdit ID="dtFechaNacimientoTitular" runat="server" Theme="Material" EditFormat="Custom" Width="100%" Caption="" >
                                                <TimeSectionProperties>
                                                    <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                                </TimeSectionProperties>
                                                <CalendarProperties>
                                                    <FastNavProperties DisplayMode="Inline"/>
                                                </CalendarProperties>
                                            </dx:ASPxDateEdit>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator26" controltovalidate="dtFechaNacimientoTitular" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 col-sm-4 col-xs-12 text-center">
                                        <code><asp:Label runat="server" ID="LabelRespuestaNacionalidadTitular" Text=""></asp:Label></code>
                                    </div>
                                </div>
                            </asp:Panel>
                            <hr />
                            <!-- DATOS DE AGENTE  -->
                            <div class="row">
                                <asp:Label runat="server" ID="LabelAgente" Text="Agente" class="control-label col-md-1 col-sm1 col-xs-12"></asp:Label>
                                <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                    <asp:TextBox ID="txClaveAgente" runat="server" MaxLength="10" class="form-control" AutoPostBack="true" OnTextChanged="DatotsDeAgente"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" FilterMode="ValidChars" TargetControlID="txClaveAgente" ValidChars="áéíóúabcdefghijklmnñopqrstuvwxyzÁÉÍÓÚABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789" />
                                </div>
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="x_panel">
                                        <div class="x_title">
                                            <h2><small>Datos Agente <asp:Label ID="LbAgenteRespuesta" runat="server" Text=""></asp:Label></small></h2>
                                            <ul class="nav navbar-right panel_toolbox ">
                                                <li><a class="collapse-link navbar-left"><i class="fa fa-chevron-up"></i></a>
                                                </li>
                                            </ul>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <table class="table table-striped">
                                                <thead>
                                                    <tr>
                                                        <th>Nombre</th>
                                                        <th>Correo Electrónico </th>
                                                        <th>Teléfono</th>
                                                        <th>Extensión</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <th scope="row"><asp:Label ID="lbNombreAgente" runat="server" Text=""></asp:Label></th>
                                                        <td><asp:Label ID="lbEmailAgente" runat="server" Text=""></asp:Label></td>
                                                        <td><asp:Label ID="lbTelefonoAgente" runat="server" Text=""></asp:Label></td>
                                                        <td><asp:Label ID="lbExtensionAgente" runat="server" Text=""></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2><small>Archivos Anexos </small></h2>
                            <ul class="nav navbar-right panel_toolbox">
                                <li>
                                    <a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      	        </li>
                            </ul>

                            <div class="clearfix"></div>
                            <div class="x_content text-left">
                                <br />
                                <div class="row">
                                    <div class=" profile_details">
                                        <div class="col-md-12 col-sm-12 col-xs-12 well profile_view">
                                            <div class="col-xs-12 bottom text-center">
                                                <h4 class="brief">CHECKLIST  DE DOCUMENTOS</h4><br />
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="right col-xs-12 text-left">
                                                    <asp:Label ID="TextIdTipoTramite" runat="server" Visible="false"></asp:Label>
                                                    <asp:Label ID="TextTipoPersona" runat="server" Visible="false"></asp:Label>
                                                    <asp:CheckBoxList ID="DocRequerid" runat="server" CssClass="cbl">
                                                        <asp:ListItem Selected="False" Text="Solicitud vigente" Value="2" />
                                                        <asp:ListItem Selected="False" Text="Acuse de CUSF" Value="1" />
                                                        <asp:ListItem Selected="False" Text="Identificación del contratante de acuerdo a las indicaciones de Metlife" Value="3" />
                                                        <asp:ListItem Selected="False" Text="Cotización" Value="4" />
                                                    </asp:CheckBoxList>
                                                    <dx:ASPxFormLayout ID="FORMULAIRO" runat="server" Visible="false"></dx:ASPxFormLayout>
                                                </div>
                                            </div>
                                        </div>
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
                                                        <span style="font-size: 9px">Tamaño máximo de archivo: <%= ArchivoMaximo1 %>&nbsp;MB</span><br />
                                                        <asp:Button ID="btnSubirDocumento" runat="server" Text="Subir" class="btn btn-primary" CausesValidation="False" OnClick="btnSubirDocumento_Click"/><br />
                                                        <asp:ListBox ID="lstDocumentos" runat="server" Height="100px" Width="100%" SelectionMode="Single" class="select2_multiple form-control">
                                                        </asp:ListBox>
                                                        <br />
                                                        <asp:Button ID="btnEliminaDocumento" runat="server" Text="Eliminar" class="btn btn-danger" CausesValidation="False"  OnClick="btnEliminaDocumento_Click"/>
                                                    </fieldset>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnSubirDocumento" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6 col-xs-12 well profile_view">
                                            <div class="col-xs-12 bottom text-center">
                                                <h4 class="brief">ARCHIVOS ADICIONALES</h4><br />
                                            </div> 
                                            <div class="right col-xs-12 text-left">
                                                <asp:CheckBox ID="CheckBoxInsumos"  runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_Habilita_Insumos" Text="¿Desea agregar archivos adicionales?" />
                                                <asp:UpdatePanel ID="PanelInsumos" runat="server" Visible="false">
                                                    <ContentTemplate>
                                                        <fieldset>
                                                            <asp:FileUpload ID="fileUpInsumo" runat="server"></asp:FileUpload>
                                                            <code><asp:Label ID="MensajeInsumos" runat="server" Text =""></asp:Label></code>
                                                            <br />
                                                            <asp:Button ID="btnSubirInsumo" runat="server" Text="Subir" class="btn btn-primary" OnClick="btnSubirInsumo_Click" CausesValidation="False"/><br />
                                                            <asp:ListBox ID="lstInsumos" runat="server" Height="100px" Width="100%" class="select2_multiple form-control" SelectionMode="Single">
                                                            </asp:ListBox>
                                                            <br />
                                                            <asp:Button ID="btnEliminaInsumo" runat="server" Text="Eliminar" class="btn btn-danger" CausesValidation="False" OnClick="btnEliminaInsumo_Click"  />
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

            <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Observaciones </small></h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li>
                                <a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      	    </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content text-left">
                        
                        <!-- OBSERVACIONES -->
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group has-feedback">
                            <asp:TextBox ID="txObervaciones" runat="server" Font-Size="14px" TextMode="MultiLine" Width="100%" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteDetalle" runat="server" FilterMode="ValidChars" TargetControlID="txObervaciones" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ = $%*_0123456789-,.:+*/?¿+¡\/][{};" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1 col-sm-1 col-xs-12 text-center">
                                <asp:Button ID="BtnContinuar" runat="server"  AutoPostBack="True" Text="Continuar" Class="btn btn-success" OnClick="BtnContinuar_Click"/>
                            </div>
                            <div class="col-md-4 col-sm-4 col-xs-12 text-center">
                                <code><asp:Label ID="Respuesta" runat="server" ></asp:Label><asp:Label ID="Mensajes" runat="server"></asp:Label></code>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
     </asp:UpdatePanel>


</asp:Content>
