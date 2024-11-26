<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="TramiteTarea.aspx.cs" Inherits="WFO.Procesos.Operador.TramiteTarea" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v17.2" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register assembly="DevExpress.Web.ASPxTreeList.v17.2" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <script>
        // Evitar doble click
        var isSubmitted = false;
        function preventMultipleSubmissions(itemHtmlClientId) {
            if (!isSubmitted) {
                $(itemHtmlClientId).val('procesnado...');
                isSubmitted = true;
                return true;
            }
            else {
                return false;
            }
        }


        function soloLetras(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ@,. = $%*_0123456789-/&?¿¡!";
            especiales = "8-37-39-46";

            tecla_especial = false
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }

            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }

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

        function treeList_CustomDataCallbackHold(s, e) {
            document.getElementById('treeListCountCell').innerHTML = e.result;
        }
        function treeList_SelectionChangedHold(s, e) {
            window.setTimeout(function () { s.PerformCustomDataCallback(''); }, 0)
        }

        function treeList_CustomDataCallbackSuspender(s, e) {
            document.getElementById('treeListCountCell').innerHTML = e.result;
        }
        function treeList_SelectionChangedSuspender(s, e) {
            window.setTimeout(function () { s.PerformCustomDataCallback(''); }, 0)
        }

        function treeList_CustomDataCallbackRechazar(s, e) {
            document.getElementById('treeListCountCell').innerHTML = e.result;
        }
        function treeList_SelectionChangedRechazar(s, e) {
            window.setTimeout(function () { s.PerformCustomDataCallback(''); }, 0)
        }

        function ShowPausa() {
            var observacionesPrivadas = document.getElementById('<%=txtObservacionesPrivadas.ClientID%>').value;
            if (observacionesPrivadas.length > 0) {
                pnlPopPausaTramite.Show();
            }
            else {
                AlertaObservaciones();
                //alert('Las observaciones son Requeridas. Por favor agregue observaciones.');
            }
        }

        function ShowHold() {
            var observacionesPrivadas = document.getElementById('<%=txtObservacionesPrivadas.ClientID%>').value;
            if (observacionesPrivadas.length > 0) {
                var foliollamada = document.getElementById('<%=TextNumPolizaSisLegado.ClientID%>').value;
                if (foliollamada.length > 0) {
                    var numerollamada = document.getElementById('<%=txtNumeroLlamada.ClientID%>').value;

                    if (numerollamada.length > 0) {
                        pnlPopMotivosHold.Show();
                        pnlCallbackMotHold.PerformCallback();
                    }
                    else
                    {
                        AlertaNumeroLlamada();
                    }
                }
                else
                {
                    AlertaFolioLlamada();
                }
            }
            else {
                AlertaObservaciones();
                //alert('Las observaciones son Requeridas. Por favor agregue observaciones.');
            }
        }

        function ShowSuspender() {
            var observacionesPrivadas = document.getElementById('<%=txtObservacionesPrivadas.ClientID%>').value;
            if (observacionesPrivadas.length > 0)
            {
                pnlPopMotivosSuspender.Show();
                pnlCallbackMotSuspender.PerformCallback();
            }
            else {
                AlertaObservaciones();
                //alert('Las observaciones son Requeridas. Por favor agregue observaciones.');
            }
        }

        function ShowRechazar() {
            var observacionesPrivadas = document.getElementById('<%=txtObservacionesPrivadas.ClientID%>').value;
            if (observacionesPrivadas.length > 0)
            {
                pnlPopMotivosRechazar.Show();
                pnlCallbackMotRechazar.PerformCallback();
            }
            else {
                AlertaObservaciones();
                //alert('Las observaciones son Requeridas. Por favor agregue observaciones.');
            }
        }
        
        function ShowCancelar() {
            var observacionesPrivadas = document.getElementById('<%=txtObservacionesPrivadas.ClientID%>').value;
            if (observacionesPrivadas.length > 0)
            {
                pnlPopCancelar.Show();
                pnlCallbackCancelar.PerformCallback();
            }
            else {
                AlertaObservaciones();
                //alert('Las observaciones son Requeridas. Por favor agregue observaciones.');
            }
        }

        function ShowEnviarMesa() {
            var observacionesPrivadas = document.getElementById('<%=txtObservacionesEnviarMesa.ClientID%>').value;
            if (observacionesPrivadas.length == 0) {
                AlertaObservaciones();
            }
        }

        function CheckRequerido(Documento) {
            new PNotify({
                title: 'Documento requerido !',
                text: 'Debe contener el documento: ' + Documento,
                type: 'error',
                styling: 'bootstrap3'
            });
        }

        function CheckRequeridoDocumentacion() {
            new PNotify({
                title: 'Documento requerido !',
                text: 'Realizada la validación de documentos',
                type: 'error',
                styling: 'bootstrap3'
            });
        }

        function AlertaNumeroLlamada() {
            new PNotify({
                title: 'Número Llamada!',
                text: 'Por favor agregue el número de intento de la llamada.',
                type: 'error',
                styling: 'bootstrap3'
            });
        }

        function AlertaFolioLlamada() {
            new PNotify({
                title: 'Folio Llamada!',
                text: 'Por favor agregue el folio de la llamada.',
                type: 'error',
                styling: 'bootstrap3'
            });
        }

        function AlertaObservaciones() {
            new PNotify({
                title: 'Las observaciones son Requeridas !',
                text: 'Por favor agregue observaciones.',
                type: 'error',
                styling: 'bootstrap3'
            });
        }

        function AlertaObservacionesPublicas() {
            new PNotify({
                title: 'Las observaciones publicas son requeridas !',
                text: 'Por favor agregue observaciones.',
                type: 'error',
                styling: 'bootstrap3'
            });
        }

        function AlertaFolioLlamada() {
            new PNotify({
                title: 'El folio de llamada es requerido !',
                text: 'Por favor agregue el folio de la llamada.',
                type: 'error',
                styling: 'bootstrap3'
            });
        }

        function AlertaSeleccionCompleta() {
            new PNotify({
                title: 'Estatus de selección completa requerido !',
                text: 'Por favor seleccione una opción.',
                type: 'error',
                styling: 'bootstrap3'
            });
        }

        function AlertaMotivosHold() {
            new PNotify({
                title: 'Debe seleccionar al menos un motivo Hold. !',
                text: 'Por favor agregue motivos.',
                type: 'error',
                styling: 'bootstrap3'
            });
        }

        function AlertaMotivosSeleccionCompleta() {
            new PNotify({
                title: 'Debe seleccionar al menos una mesa. !',
                text: 'Por favor seleciona una mesa.',
                type: 'error',
                styling: 'bootstrap3'
            });
        }


        function AlertaMotivosSuspencion() {
            new PNotify({
                title: 'Debe seleccionar al menos un motivo de suspensión. !',
                text: 'Por favor agregue motivos.',
                type: 'error',
                styling: 'bootstrap3'
            });
        }

        function AlertaMotivosRechazo() {
            new PNotify({
                title: 'Debe seleccionar al menos un motivo de rechazo. !',
                text: 'Por favor agregue motivos.',
                type: 'error',
                styling: 'bootstrap3'
            });
        }

        function AlertaMotivosCancelacion() {
            new PNotify({
                title: 'Debe seleccionar al menos un motivo de cancelación. !',
                text: 'Por favor agregue motivos.',
                type: 'error',
                styling: 'bootstrap3'
            });
        }

        function ValidaCheck() {
            alert("validacion");
        }

    </script>
    <style>
        .btn-danger {
            background: linear-gradient(-45deg,#d32929,#b70000)!important;
        }
    </style>

    <!-- Campos Ocultos -->
    <div>
        <asp:HiddenField ID="hfIdArchivo" runat="server" Value="0" />
        <asp:HiddenField ID="hfIdTramite" runat="server" Value="0" />
        <asp:HiddenField ID="hfIdProceso"    runat="server" Value="0" />
        <asp:HiddenField ID="hfNombreProceso" runat="server" Value="0" />
        <asp:HiddenField ID="hfAutomatico" runat="server" Value="1" />
        <asp:HiddenField ID="hfTipoTramite" runat="server" Value="0" />
    </div>

    <!-- Modal : Enviar trámite a Mesa -->
    <div class="modal fade mSendToMesa" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel3">Enviar a Mesa</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:Label runat="server" ID="lblSendToMesa" Visible="false" Text=""></asp:Label>
                            <asp:DropDownList runat="server" AutoPostBack="false" ID="cboToSend" Visible="false" class="form-control"></asp:DropDownList><br />
                            <strong>Observaciones Públicas</strong>
                            <asp:TextBox ID="txtObservacionesEnviarMesa" runat="server" TextMode="MultiLine" Width="98%" Height="50px" ></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterMode="ValidChars" TargetControlID="txtObservacionesEnviarMesa" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ@. = $%*_0123456789-/&" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSendToMesa" ValidationGroup="EnvioMesas" runat="server" Text="Enviar a Mesa" class="btn btn-success" OnClick="btnSendToMesa_Click"/>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- MODAL DE BITACORA PÚBLICA -->
    <div class="modal fade BitacoraPublica" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel3">Bitácora Pública </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:Repeater ID="rptBitacoraPublica" runat="server" >
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

    <!-- MODAL DE BITACORA PRIVADA  -->
    <div class="modal fade BitacoraPrivada" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel3">Bitácora Privada </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:Repeater ID="rptBitacoraPrivada" runat="server" >
                                <HeaderTemplate>
                                    <table id="datatableBitacora" class="table table-striped table-bordered jambo_table bulk_action">
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

    
    <!-- MODAL DE CONFIRMACION TAREA REALIZA (SIN EXITO - REPROGRAMACIÓN) -->
    <div class="modal fade confirmacion" id="screenTaskDoModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h4 class="modal-title" id="screenTitleTaskDoModal">
                        <asp:label ID="lblTaskDoModal1" runat="server" Text="Confirmación">
                        </asp:label>
                    </h4>
                </div>
                <div class="modal-body text-center">
                    <h5 class="modal-title" id="lblTaskDoModalTitle">
                        <asp:Label runat="server" ID="lblTaskDoModal2" Text="¿ Deseas indicar indicar la tarea realizada ?"></asp:Label>
                        <asp:Label runat="server" ID="lblTaskDoModal3" Text="La llamada será reprogramada para su atención nuevamente"></asp:Label>
                    </h5>
                </div>
                <div class="modal-footer text-center">
                    <div class="row text-center">
                        <button type="button" class="btn btn-default col-md-6 col-sm-6 col-xs-12" data-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnTaskDoModal" runat="server" Text="Aceptar" class="btn btn-primary col-md-5 col-sm-5 col-xs-12" CausesValidation="False" OnClick="btnTaskDoModal_Click"  />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- MODAL DE CONFIRMACION TAREA REALIZA OK (DAR RESPUES) -->
    <div class="modal fade confirmacion" id="screenTaskOkModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h4 class="modal-title" id="screenTitleTaskOkModal">
                        <asp:label ID="lblTaskOkModal1" runat="server" Text="Confirmación">
                        </asp:label>
                    </h4>
                </div>
                <div class="modal-body text-center">
                    <h5 class="modal-title" id="lblTaskOkModalTitle">
                        <asp:Label runat="server" ID="lblTaskOkModal2" Text="¿ Deseas indicar la respuesta ?"></asp:Label>
                    </h5>
                </div>
                <div class="modal-footer text-center">
                    <div class="row text-center">
                        <button type="button" class="btn btn-default col-md-6 col-sm-6 col-xs-12" data-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnTaskOkModal" runat="server" Text="Aceptar" class="btn btn-primary col-md-5 col-sm-5 col-xs-12" CausesValidation="False" OnClick="btnTaskOkModal_Click"  />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- MODAL DE CONFIRMACION ACEPTAR
    <div class="modal fade confirmacion" id="ContinuarTramite" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h4 class="modal-title" id="myModalLabel2">
                        <asp:label ID="Label4" runat="server" Text="Confirmación de movimiento">
                        </asp:label>
                    </h4>
                </div>
                <div class="modal-body text-center">
                    <h5 class="modal-title" id="myModalLabel2">
                        <asp:Label runat="server" ID="Label5" Text="¿ Deseas aceptar el tramite ?"></asp:Label>
                    </h5>
                </div>
                <div class="modal-footer text-center">
                    <div class="row text-center">
                        <button type="button" class="btn btn-default col-md-6 col-sm-6 col-xs-12" data-dismiss="modal">Cancelar</button>
                        <asp:Button ID="Button1" runat="server" Text="Aceptar" class="btn btn-primary col-md-5 col-sm-5 col-xs-12" CausesValidation="False" OnClick="btnAceptar_Click"  />
                    </div>
                </div>
            </div>
        </div>
    </div-->

    <!-- MODAL DE CONFIRMACION PCI -->
    <div class="modal fade confirmacionPCI" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h4 class="modal-title" id="myModalLabel2">
                        <asp:label ID="Label6" runat="server" Text="Confirmación de movimiento">
                        </asp:label>
                    </h4>
                </div>
                <div class="modal-body text-center">
                    <h5 class="modal-title" id="myModalLabel2">
                        <asp:Label runat="server" ID="Label7" Text="¿ Deseas mandar tramite a PCI ?"></asp:Label>
                    </h5>
                </div>
                <div class="modal-footer text-center">
                    <div class="row text-center">
                        <button type="button" class="btn btn-default col-md-6 col-sm-6 col-xs-12" data-dismiss="modal">Cancelar</button>
                        <asp:Button ID="Button3" runat="server" Text="Aceptar" class="btn btn-primary col-md-5 col-sm-5 col-xs-12" CausesValidation="False" OnClick="btnPCI_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- MODAL DE CONFIRMACION SELECCIÓN COMPLETA -->
    <div class="modal fade confirmacionSeleccionCompleta" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h4 class="modal-title" id="myModalLabel2">
                        <asp:label ID="lblTituloSeleccionCompleta" runat="server" Text="Confirmación de movimiento">
                        </asp:label>
                    </h4>
                </div>
                <div class="modal-body text-center">
                    <h5 class="modal-title" id="myModalLabel2">
                        <asp:Label runat="server" ID="lblTituloSeleccionCompletaConfirma" Text="¿ Deseas mandar tramite a Selección Completa ?"></asp:Label>
                    </h5>
                </div>
                <div class="modal-footer text-center">
                    <div class="col-md-12 col-sm-12 col-xs-12 text-center">
                        <button type="button" class="btn btn-default col-md-6 col-sm-6 col-xs-6" data-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnAceptarSeleccionCompleta" runat="server" Text="Aceptar" class="btn btn-primary col-md-5 col-sm-5 col-xs-6" CausesValidation="False" OnClick="btnAceptarSeleccionCompleta_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- MODAL DE  OPERACIONES -->
    <div class="modal fade bs-example-modal-sm" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel2">
                        <asp:label ID="TituloModal" runat="server" Text="Actualización de trámite">
                        </asp:label>
                    </h4>
                </div>
                <div class="modal-body">
                    <p><asp:Label runat="server" ID="MensajeModal" Text="Trámite procesado"></asp:Label></p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="Button2" runat="server" Text="Aceptar" class="btn btn-primary" CausesValidation="False" OnClick="TramiteActualizado"  />
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
    

    <!-- MODAL DE ARCHIVOS EXPEDIENTE  -->
    <div class="modal fade Expediente" id="Expediente" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel3">Expedientes </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:Repeater ID="rptExpedientes" runat="server" >
                                <HeaderTemplate>
                                    <table id="" class="table table-striped table-bordered jambo_table bulk_action">
                                        <thead>
                                            <tr>
                                                <th>Nombre Archivo</th>
                                                <th>Fecha Carga</th>
                                                <th>Unidad</th>
                                                <th>Consultar</th>
                                            </tr>
                                        </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("NmArchivo")%></td>
                                        <td><%#Eval("Fecha_Registro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                        <td><%#Eval("FusionTexto")%></td>
                                        <td><asp:ImageButton ID="imbtnExpedienteFlot" CausesValidation="false" runat="server" ImageUrl="../../Imagenes/folder-abrir.jpg" CommandName ="Consultar" CommandArgument='<%#Eval("Id")%>' OnCommand="CargaExpedienteID" /></td>
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

    <!-- MODAL DE AGREGACION DE DIRECCIONES  -->
    <div class="modal fade bs-example-modal-sm" id="AgreDirecciones" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h4 class="modal-title" id="myModalLabel2">
                        <asp:label ID="LabelTituloDirecciones" runat="server" Text="">
                        </asp:label>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="control-label col-md-12 col-sm-12 col-xs-12">
                        <asp:Label runat="server" ID="LabelCampo" Text="" Font-Bold="True" class="control-label"></asp:Label>
                        <asp:TextBox ID="TextBoxDato" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" FilterMode="ValidChars" TargetControlID="TextBoxDato" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ 1234567890&/," />
                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator64" validationgroup="AterecionDireciones" controltovalidate="TextBoxDato" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                    </div>
                    <br />
                    <hr />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default col-md-6 col-sm-6 col-xs-6" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="ButtonAgregarPoblacion" Visible="false" runat="server" Text="Aceptar" class="btn btn-primary col-md-5 col-sm-5 col-xs-6" validationgroup="AterecionDireciones" CausesValidation="True"  OnClick="BtnAgregarPoblacion_Click"  />
                    <asp:Button ID="ButtonAgregarCP" Visible="false" runat="server" Text="Aceptar" class="btn btn-primary col-md-5 col-sm-5 col-xs-6" validationgroup="AterecionDireciones" CausesValidation="True"  OnClick="BtnAgregarCP_Click"  />
                    <asp:Button ID="ButtonAgregarColonia" Visible="false" runat="server" Text="Aceptar" class="btn btn-primary col-md-5 col-sm-5 col-xs-6" validationgroup="AterecionDireciones" CausesValidation="True"  OnClick="BtnAgregarColonia_Click"  />
                </div>
            </div>
        </div>
    </div>

    <!-- ELEMENTOS DE CAPTURA ADMISION -->
    <asp:Panel ID="PanelEtitar" runat="server" Visible="false" Enabled="false">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>ADMISIÓN</small> </h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content text-left">
                        <div class="form-group">
                            <h2><small>Póliza / Seguro</small></h2>
                        </div>
                        <hr />
                        <!-- SECCIÓN CANTIDADES -->
                        <div class="row">
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="Label2" Text="Fecha registro:" Font-Bold="True" class="control-label"></asp:Label><br />
                                <asp:Label runat="server" ID="FechaRegistroAdmicion" Text="Fecha registro:" Font-Bold="False" class="control-label"></asp:Label>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="SumaAsegurada" Text="* Prima Total de Acuerdo a Cotización" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:TextBox ID="txtPrimaTotalGMM"  onChange="MASK('ContenidoPrincipal_txtPrimaTotalGMM','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" AutoPostBack="true" class="form-control"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterMode="ValidChars" TargetControlID="txtPrimaTotalGMM" ValidChars="0123456789.," />
                                <asp:RequiredFieldValidator id="RequiredFieldValidator27" InitialValue="0.00"  ControlToValidate="txtPrimaTotalGMM" ErrorMessage="*" runat="server" ForeColor="Red"/>
                            </div>
                            <asp:Panel ID="PanelDatosN3" runat="server" Visible="False">
                                <asp:Label ID="LabelRiesgos" Visible="false" runat="server" Text="*Riesgos" Font-Bold="true" class="control-label col-md-2 col-sm-2 col-xs-6 "></asp:Label>
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group has-feedback">
                                    <asp:DropDownList ID="LisRiesgos" runat="server" AutoPostBack="True" class="form-control" Visible="false">
                                        <asp:ListItem Value="-1">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="LisRiesgos" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </asp:Panel>
                        </div>

                        <asp:Panel ID="PanelDatosN1C4" runat="server" Visible="False">
                            <div class="row">
                                <div class="col-md-4 col-sm-4 col-xs-6">
                                    <asp:Label runat="server" ID="Label74" Text="* Suma Asegurada Básica" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="txtSumaAseguradaBasica"  onChange="MASK('ContenidoPrincipal_txtSumaAseguradaBasica','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" AutoPostBack="true" class="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterMode="ValidChars" TargetControlID="txtSumaAseguradaBasica" ValidChars="0123456789.," />
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator72" InitialValue="0.00"  ControlToValidate="txtSumaAseguradaBasica" ErrorMessage="*" runat="server" ForeColor="Red"/>
                                </div>
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="Moneda" Text="*Moneda" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:DropDownList ID="cboMoneda" runat="server" AutoPostBack="True" TabIndex="1"  class="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="cboMoneda" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <asp:Label runat="server" ID="Label76" Text="Hombres clave" Font-Bold="True" class="control-label col-md-2 col-sm-2 col-xs-6"></asp:Label>
                                <br />
                                <div class="col-md-2 col-sm-2 col-xs-6 form-group has-feedback">
                                    <asp:CheckBox ID="CheckBoxHombreClave"  runat="server" AutoPostBack="True" Text="Si"  />
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="PanelInstitucionesNC" runat="server" Visible="False">
                            <div class="row">
                                <asp:Label ID="Label80" runat="server" Text="* Tipo de trámite" Font-Bold="true" class="control-label col-md-1 col-sm-1 col-xs-6 "></asp:Label>
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group has-feedback">
                                    <asp:DropDownList ID="DropDownListTipoTramite" runat="server" AutoPostBack="True" class="form-control">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator74" runat="server" ControlToValidate="DropDownListTipoTramite" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="form-group">
                            <h2><small>Información de la póliza</small></h2>
                        </div>
                        <hr />
                        <!-- INFORMACIÓN DE PÓLIZA  -->
                        <div class="row">
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="labelClavePromotoria" Text="Clave Promontoría" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <!-- <asp:TextBox ID="texClavePromotoria" runat="server" MaxLength="5" AutoPostBack="false" Enabled="false" Visible="false"></asp:TextBox> -->
                                <asp:TextBox ID="texClave" runat="server" MaxLength="5" AutoPostBack="false" class="form-control" disabled="disabled"></asp:TextBox>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="labelRegion" Text="Región" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                <asp:TextBox ID="texRegion" runat="server" MaxLength="5" AutoPostBack="false" class="form-control" disabled="disabled" TextMode="MultiLine" Rows="1"></asp:TextBox>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="labelGerenteComercial" Text="Gerente comercial" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:HiddenField ID="HiddenField4" runat="server" />
                                <asp:TextBox ID="texGerenteComercial" runat="server" class="form-control" TextMode="MultiLine" Rows="1" AutoPostBack="false" disabled="disabled" ></asp:TextBox>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="labelEjecutivoComercial" Text="Ejecutivo comercial" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:HiddenField ID="HiddenField5" runat="server" />
                                <asp:TextBox ID="texEjecuticoComercial" runat="server" class="form-control" TextMode="MultiLine" Rows="1"  AutoPostBack="false" disabled="disabled"></asp:TextBox>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="LabelEjecutivoFront" Text="Ejecutivo Front" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:TextBox ID="texEjecuticoFront" runat="server" class="form-control" TextMode="MultiLine" Rows="1"  AutoPostBack="false" disabled="disabled"></asp:TextBox>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="labelSolicituNumeroOrden" Text="Solicitud / Número de Orden" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:TextBox ID="textNumeroOrden" runat="server" MaxLength="15" class="form-control" AutoPostBack="false" ></asp:TextBox>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="labelFechaSolicitud"  Font-Bold="True" Text="* Fecha Firma Solicitud" class="control-label"></asp:Label>
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
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="lblTipoContratante" Text="* Tipo de Contratante" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:DropDownList ID="cboTipoContratante" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="cboTipoContratante_SelectedIndexChanged">
                                    <asp:ListItem Value="0">SELECCIONAR</asp:ListItem>
                                    <asp:ListItem Value=1>PERSONA FÍSICA</asp:ListItem>
                                    <asp:ListItem Value=2>PERSONA MORAL</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTipoContratante" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" Font-Size="16px"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <!-- PERSONA FISICA -->
                        <asp:Panel ID="pnPrsFisica" runat="server" Visible="False">
                            <div class="form-group">
                                <h2><small>Información contratante </small></h2>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="lblNombre" Text="*Nombre(s)" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="txNombre" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
								    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txNombre" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="lblAPaterno" Text="*Apellido Paterno" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="txApPat" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApPat" runat="server" FilterMode="ValidChars" TargetControlID="txApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator22" controltovalidate="txApPat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="lblAMaterno" Text="*Apellido Materno" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="txApMat" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApMat" runat="server" FilterMode="ValidChars" TargetControlID="txApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator11" controltovalidate="txApMat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                            </div>
                            <div class="row">
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="Label8" Text="* Sexo" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:DropDownList ID="txSexo" runat="server" class="form-control">
                                        <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                        <asp:ListItem Value="M">MASCULINO</asp:ListItem>
                                        <asp:ListItem Value="F">FEMENINO</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="txSexo" ForeColor="Red" InitialValue="" Font-Size="16px"></asp:RequiredFieldValidator>
                                </div>
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="lblRFCPFisica" Text="* RFC" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class="col-md-11 col-sm-11 col-xs-12 form-group has-feedback">
                                        <div class="input-group col-xs-10">
                                            <asp:TextBox ID="txRfc" runat="server" MaxLength="13" class="form-control" ></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:Button  runat="server" CausesValidation="False" Text="Calcular" class="btn btn-primary" ToolTip="RFC" OnClick="dtFechaNacimiento_OnChanged" />
                                            </span>
                                                
                                        </div>
                                    </div>
                                </div>
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txRfc" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                    <asp:RegularExpressionValidator ID="rev_txRfc" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvRfc" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    <code><asp:Label runat="server" ID="LabelRespuestaRFCFisico" Text=""></asp:Label></code>
                                </div>
                            </div>

                            <div class="row">
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="LabelFechaNacimiento" Text="Fecha Nacimiento" class="control-label"></asp:Label>
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
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="LabelNacionalidadFisica" Text="Nacionalidad" class="control-label"></asp:Label>
                                    <dx:ASPxComboBox ID="txNacionalidad" runat="server" SelectedIndex="136" AutoPostBack="true" Theme="Material" EditFormat="Custom" Width="100%" OnSelectedIndexChanged="LisNacionalidad_SelectedIndexChanged">
                                    </dx:ASPxComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txNacionalidad" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                    
                                <div class="control-label col-md-4 col-sm-4 col-xs-12 text-center">
                                    <code><asp:Label runat="server" ID="LabelRespuestaNacionalidadFisico" Text=""></asp:Label></code>
                                </div>
                            </div>
                        </asp:Panel>
                        <!-- PERSONA MORAL -->
                        <asp:Panel ID="pnPrsMoral" runat="server" Visible="False">
                            <div class="form-group">
                                <h2><small>Información contratante </small></h2>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="control-label col-md-8 col-sm-8 col-xs-12">
                                    <asp:Label runat="server" ID="lblNombrePMoral" Text="*Nombre" Font-Bold="True" class="control-label"/>
                                    <asp:TextBox ID="txNomMoral" runat="server" MaxLength="100" class="form-control" ></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteNomMoral" runat="server" FilterMode="ValidChars" TargetControlID="txNomMoral" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZáéíóúÁÉÍÓÚ&" />
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" controltovalidate="txNomMoral" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                            </div>
                            <div class="row">
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="LabelMoralFechaConstitucion" Text="*Fecha de Constitución" Font-Bold="True" class="control-label"></asp:Label>
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
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="lblRFCPMoral" Text="*RFC" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class="col-md-11 col-sm-11 col-xs-12 form-group has-feedback">
                                        <div class="input-group col-xs-10">
                                        <asp:TextBox ID="txRfcMoral" runat="server" MaxLength="12" class="form-control" ></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:Button  runat="server" CausesValidation="False" Text="Calcular" class="btn btn-primary" ToolTip="RFC" OnClick="dtFechaConstitucion_OnChanged" />
                                        </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteRfcMoral" runat="server" FilterMode="ValidChars" TargetControlID="txRfcMoral" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="revRfcMoral" runat="server" ControlToValidate="txRfcMoral" ErrorMessage="*" Font-Size="16px" ForeColor="Red" ValidationExpression="^[a-zA-Z]{3,4}(\d{6})((\D|\d){3})?$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="txRfcMoral" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    <code><asp:Label runat="server" ID="LabelRespuestaRFCMoral" Text=""></asp:Label></code>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Label runat="server" ID="LabelMismoContratante" Text="¿El solicitante es el mismo que el contratante?" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                        <asp:CheckBox ID="CheckBox2"  runat="server" AutoPostBack="True" oncheckedchanged="CheckBox2_CheckedChanged" Text="Si" Checked="true" />
                        <asp:CheckBox ID="CheckBox1"  runat="server" AutoPostBack="True" oncheckedchanged="CheckBox1_CheckedChanged" Text="No" /> 
                        <!-- DIFERENTE CONTRATANTE -->
                        <asp:Panel ID="DiferenteContratante" runat="server" Visible="False">
                            <div class="form-group">
                                <h2><small>Información titular</small></h2>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="LabelTitularNombre" Text="*Nombre(s)" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="txTiNombre" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" TargetControlID="txTiNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator13" controltovalidate="txTiNombre" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="LabelTitularApellidoPaterno" Text="*Apellido paterno" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="txTiApPat" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars" TargetControlID="txTiApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator14" controltovalidate="txTiApPat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="LabelApellidoMaterno" Text="*Apellido materno" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="txTiApMat" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterMode="ValidChars" TargetControlID="txTiApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator15" controltovalidate="txTiApMat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                    
                            </div>
                            <div class="row">
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="LabelTitularNacionalidad" Text="*Nacionalidad" Font-Bold="True" class="control-label"></asp:Label>
                                    <dx:ASPxComboBox ID="txTiNacionalidad" runat="server" Theme="Material" EditFormat="Custom" Width="100%" SelectedIndex="136" AutoPostBack="true" OnSelectedIndexChanged="LisTitNacionalidad_SelectedIndexChanged">
                                    </dx:ASPxComboBox>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator16" controltovalidate="txTiNacionalidad" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="LabelTitularSexo" Text="*Sexo" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:DropDownList ID="txtSexoM" runat="server" class="form-control">
                                        <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                        <asp:ListItem Value="M">MASCULINO</asp:ListItem>
                                        <asp:ListItem Value="F">FEMENINO</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="txtSexoM" ForeColor="Red" InitialValue="" Font-Size="16px"></asp:RequiredFieldValidator>
                                </div>
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Label runat="server" ID="LabelTitularFechaNacimiento" Text="*Fecha Nacimiento" Font-Bold="True" class="control-label"></asp:Label>
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
                                <div class="control-label col-md-4 col-sm-4 col-xs-12 text-center">
                                    <code><asp:Label runat="server" ID="LabelRespuestaNacionalidadTitular" Text=""></asp:Label></code>
                                </div>
                            </div>
                        </asp:Panel>
                        <hr />
                        <!-- REGISTRAR O CANCELAR ACCION -->
                        <div class="row">
                            <asp:Button ID="GuardarCaptura" runat="server" Text="Guardar" class="btn btn-success col-md-4 col-sm-4 col-xs-12" Visible="false" OnClick="BtnGuardarCaptura_Click"/>
                            <asp:Button ID="CancelarCaptura" runat="server" Text="Cancelar" class="btn btn-danger col-md-4 col-sm-4 col-xs-12" CausesValidation="false"  Visible="false" OnClick="BtnCancelar_Click"/>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12 text-center">
                                <code><asp:Label runat="server" ID="LabelRegistrarCaptura" Text=""></asp:Label></code>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- ELEMENTOS DE CAPTURA MASIVA COASEGURADOS - MESA ADMISCION -->
    <asp:Panel ID="PanelCapturaMasiva" runat="server" Visible="False" Enabled="False">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Captura</small> </h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content text-left">
                        <!-- SECCION DE CAPTURA -->
                        <p>Por favor coloca la suma asegura en UMAN antes de iniciar toda la captura.</p>
                        <div class="row">
                            <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                <asp:Label runat="server" ID="Label19" Text="* Fecha Firma Solicitud" Font-Bold="True" class="control-label"></asp:Label>
                                <dx:ASPxDateEdit ID="cpoFechaFirmaSolicitud" runat="server" Theme="Material" EditFormat="Custom" Width="100%" Caption="" >
                                    <TimeSectionProperties>
                                        <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                    </TimeSectionProperties>
                                    <CalendarProperties>
                                        <FastNavProperties DisplayMode="Inline"/>
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator30" validationgroup="RegistroAsegurado" controltovalidate="cpoFechaFirmaSolicitud" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="Label44" Font-Bold="True" Visible="True" Text="* Lugar de firma" class="control-label"></asp:Label>
                                <dx:ASPxComboBox ID="cboCatEstados" Visible="True" runat="server" SelectedIndex="-1" AutoPostBack="true" Theme="Material" EditFormat="Custom" Width="100%">
                                </dx:ASPxComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" validationgroup="RegistroAsegurado" ControlToValidate="cboCatEstados" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="Label20" Text="*Suma asegura en UMAN" Font-Bold="True" class="control-label"></asp:Label>
                                <div class="col-md-11 col-sm-11 col-xs-12 form-group has-feedback">
                                    <div class="input-group col-xs-10">
                                        <asp:TextBox ID="txtSumaUMAN" runat="server" MaxLength="5" class="form-control" ></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:Button  runat="server" validationgroup="PersonalInfoGroup" CausesValidation="True" Text="Buscar" class="btn btn-primary" ToolTip="RFC" OnClick="BtnSumaUMAN_click" />
                                        </span>  
                                    </div>
                                </div>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterMode="ValidChars" TargetControlID="txtSumaUMAN" ValidChars="0123456789" />
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator31" validationgroup="PersonalInfoGroup" controltovalidate="txtSumaUMAN" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="Label22" Text="*Agente" Font-Bold="True" class="control-label"></asp:Label>
                                <div class="col-md-11 col-sm-11 col-xs-12 form-group has-feedback">
                                    <div class="input-group col-xs-10">
                                        <asp:TextBox ID="txtAgente" runat="server" MaxLength="13" class="form-control" ></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:Button  runat="server" validationgroup="agente" CausesValidation="True" Text="Buscar" class="btn btn-primary" ToolTip="RFC" OnClick="BtnAgente_click" />
                                        </span>
                                    </div>
                                </div>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterMode="ValidChars" TargetControlID="txtAgente" ValidChars="0123456789 /" />
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator32" validationgroup="agente" controltovalidate="txtAgente" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                            </div>
                            <div class="col-md-4 col-sm-4 col-xs-12 form-group has-feedback">
                                <asp:Label runat="server" ID="LabelFiltroAgente" Text="* Filtro Agente" Visible="false" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:DropDownList ID="cboFiltroAgente" runat="server" Visible="false" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="cboFiltroAgente_SelectedIndexChanged">
                                    <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator43" validationgroup="RegistroAsegurado" runat="server" ControlToValidate="cboFiltroAgente" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row">
                            <div class="control-label col-md-4 col-sm-4 col-xs-12 text-center">
                                <code><asp:Label runat="server" ID="LabelUMAN" Text=""></asp:Label></code>
                                <code style="background-color:aquamarine; text-decoration-color:blue"><asp:Label runat="server" ID="LabelUMANAcptable" Text=""></asp:Label></code>
                                <code style="background-color:aquamarine; text-decoration-color:chartreuse"><asp:Label runat="server" ID="LabelUMANANoencontrado" Text=""></asp:Label></code>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12 text-center">
                                <code><asp:Label runat="server" ID="LabelAgente" Text=""></asp:Label></code>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="control-label col-md-2 col-sm-2 col-xs-12">
                                <asp:Button ID="btnExamenCotizacion" Visible="false" runat="server" Text="Realizar Cotizacion" class="btn btn-primary" OnClick="BtnMostrarExamenCotizador"/>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12 text-center">
                                <code><asp:Label runat="server" ID="Label54" Text=""></asp:Label></code>
                            </div>
                        </div>
                        <hr />
                         <asp:Panel ID="PanelCapturaExamen" runat="server" Visible="False" Enabled="False">
                            <div class="row">
                                <div class="control-label col-md-2 col-sm-2 col-xs-12">
                                    <asp:Label runat="server" ID="Label32" Text="* Edad" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="TextBoxEdad" runat="server" MaxLength="3" AutoPostBack="true" class="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" FilterMode="ValidChars" TargetControlID="TextBoxEdad" ValidChars="0123456789" />
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator45" validationgroup="Cotizador" InitialValue=""  ControlToValidate="TextBoxEdad" ErrorMessage="*" runat="server" ForeColor="Red"/>
                                </div>
                                <div class="control-label col-md-2 col-sm-2 col-xs-12">
                                    <asp:Label runat="server" ID="Label34" Text="* Altura" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="TextBoxAltura" runat="server" MaxLength="15" AutoPostBack="true" class="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" FilterMode="ValidChars" TargetControlID="TextBoxAltura" ValidChars="0123456789." />
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator47" validationgroup="Cotizador" InitialValue=""  ControlToValidate="TextBoxAltura" ErrorMessage="*" runat="server" ForeColor="Red"/>
                                </div>
                                <div class="control-label col-md-2 col-sm-2 col-xs-12">
                                    <asp:Label runat="server" ID="Label33" Text="* Peso" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="TextBoxPeso"  runat="server" MaxLength="15" AutoPostBack="true" class="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" FilterMode="ValidChars" TargetControlID="TextBoxPeso" ValidChars="0123456789." />
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator46" validationgroup="Cotizador" InitialValue=""  ControlToValidate="TextBoxPeso" ErrorMessage="*" runat="server" ForeColor="Red"/>
                                </div>
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label35" Text="* Pregunta 1" Font-Bold="True" Visible ="true" class="control-label"></asp:Label>
                                    <asp:DropDownList ID="DropDownList1" runat="server"  AutoPostBack="True" class="form-control" Visible="true">
                                        <asp:ListItem Value="-1">SELECCIONAR</asp:ListItem>
                                        <asp:ListItem Value=1>SI</asp:ListItem>
                                        <asp:ListItem Value=0>NO</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator48" validationgroup="Cotizador" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="DropDownList1" ForeColor="Red" InitialValue="-1" Font-Size="16px"></asp:RequiredFieldValidator>
                                </div>
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label36" Text="* Pregunta 2" Font-Bold="True" Visible ="true" class="control-label"></asp:Label>
                                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" class="form-control" Visible="true">
                                        <asp:ListItem Value="-1">SELECCIONAR</asp:ListItem>
                                        <asp:ListItem Value=1>SI</asp:ListItem>
                                        <asp:ListItem Value=0>NO</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator49" validationgroup="Cotizador" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="DropDownList2" ForeColor="Red" InitialValue="-1" Font-Size="16px"></asp:RequiredFieldValidator>
                                </div>
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label37" Text="* Pregunta 3" Font-Bold="True" Visible ="true" class="control-label"></asp:Label>
                                    <asp:DropDownList ID="cboPregunta3" runat="server" AutoPostBack="True" class="form-control" Visible="true" OnSelectedIndexChanged="cboPregunta3_SelectedIndexChanged">
                                        <asp:ListItem Value="-1">SELECCIONAR</asp:ListItem>
                                        <asp:ListItem Value=1>SI</asp:ListItem>
                                        <asp:ListItem Value=0>NO</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator50" validationgroup="Cotizador" runat="server" Text="*" ControlToValidate="cboPregunta3" ForeColor="Red" InitialValue="-1" Font-Size="16px"></asp:RequiredFieldValidator>
                                </div>
                                <div class="control-label col-md-8 col-sm-8 col-xs-12">
                                    <asp:Label runat="server" ID="LabelPadecimiento" Font-Bold="True" Visible="false" Text="* Padecimientos" class="control-label"></asp:Label>
                                    <dx:ASPxComboBox ID="ASPxComboBoxPadecimiento" Visible="false" runat="server" SelectedIndex="-1" AutoPostBack="true" Theme="Material" EditFormat="Custom" Width="100%">
                                    </dx:ASPxComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" validationgroup="Cotizador" ControlToValidate="ASPxComboBoxPadecimiento" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                             <!-- REGISTRAR O CANCELAR ACCION -->
                            <div class="row">
                                <asp:Button ID="Button10" runat="server" Text="Evaluar" class="btn btn-success col-md-2 col-sm-2 col-xs-12" validationgroup="Cotizador" Visible="True" OnClick="BtnEvaluarCotizadorSolicitudSimplificadaGMM_Click"/>
                                <asp:Button ID="btnExamenCotizadorCancelar" runat="server" Text="Cancelar" class="btn btn-danger col-md-2 col-sm-2 col-xs-12" CausesValidation="false"  Visible="True" OnClick="BtnCancelarCotizadorSolicitudSimplificadaGMM_Click"/>
                            </div>
                            <hr />
                         </asp:Panel>
                        <!-- CAPTURA DE  -->
                        <div class="row">
                            <div class="control-label col-md-2 col-sm-2 col-xs-12">
                                <asp:Button ID="btnCoasegurados" runat="server" Text="Nuevo CoAsegurado" class="btn btn-primary" OnClick="NuevoCoasegurado"/>
                            </div>
                        </div>
                        <asp:Panel ID="PanelCapturaCoasegurados" runat="server" Visible="False" Enabled="False">
                            <div class="row">
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label13" Text="*Nombre(s)" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="TextCoAseNombre" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
								    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    <asp:RequiredFieldValidator runat="server" validationgroup="CoAsegurado" id="RequiredFieldValidator33" controltovalidate="TextCoAseNombre" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label23" Text="*Apellido Paterno" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="TextCoAseApaterno" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterMode="ValidChars" TargetControlID="txApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    <asp:RequiredFieldValidator runat="server" validationgroup="CoAsegurado" id="RequiredFieldValidator34" controltovalidate="TextCoAseApaterno" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label24" Text="*Apellido Materno" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="TextCoAseMaterno" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterMode="ValidChars" TargetControlID="txApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    <asp:RequiredFieldValidator runat="server" validationgroup="CoAsegurado" id="RequiredFieldValidator35" controltovalidate="TextCoAseMaterno" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                    <asp:Label runat="server" ID="Label30" Text="* Parentesco" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:DropDownList ID="cboCat_Parentesco" runat="server" AutoPostBack="True" class="form-control">
                                        <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" validationgroup="CoAsegurado"  runat="server" ControlToValidate="cboCat_Parentesco" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label25" Text="* Sexo" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:DropDownList ID="cboCoAsegSexo" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="cboTipoContratante_SelectedIndexChanged">
                                        <asp:ListItem Value="0">SELECCIONAR</asp:ListItem>
                                        <asp:ListItem Value=1>MASCULINO</asp:ListItem>
                                        <asp:ListItem Value=2>FEMENINO</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator37" validationgroup="CoAsegurado" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="cboCoAsegSexo" ForeColor="Red" InitialValue="0" Font-Size="16px"></asp:RequiredFieldValidator>
                                </div>
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label26" Text="*Fecha Nacimiento" Font-Bold="True" class="control-label"></asp:Label>
                                    <dx:ASPxDateEdit ID="dtCoAsFechaNacimiento" runat="server" Theme="Material" EditFormat="Custom" Width="100%" Caption="" >
                                            <TimeSectionProperties>
                                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                            </TimeSectionProperties>
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline"/>
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator38" validationgroup="CoAsegurado" controltovalidate="dtCoAsFechaNacimiento" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                <div class="control-label col-md-2 col-sm-2 col-xs-12">
                                    <asp:Label runat="server" ID="Label27" Text="* Edad" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="TextCoAseEdad"  onfocus="if(this.value == '0') {this.value=''}" onblur="if(this.value == ''){this.value ='0'}" value="0" runat="server" MaxLength="3" AutoPostBack="true" class="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" FilterMode="ValidChars" TargetControlID="TextCoAseEdad" ValidChars="0123456789" />
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator39" validationgroup="CoAsegurado" InitialValue="0.00"  ControlToValidate="TextCoAseEdad" ErrorMessage="*" runat="server" ForeColor="Red"/>
                                </div>
                                <div class="control-label col-md-2 col-sm-2 col-xs-12">
                                    <asp:Label runat="server" ID="Label28" Text="* Peso" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="TextCoAasegPeso"  onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" AutoPostBack="true" class="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" FilterMode="ValidChars" TargetControlID="TextCoAasegPeso" ValidChars="0123456789." />
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator40" validationgroup="CoAsegurado" InitialValue="0.00"  ControlToValidate="TextCoAasegPeso" ErrorMessage="*" runat="server" ForeColor="Red"/>
                                </div>
                                <div class="control-label col-md-2 col-sm-2 col-xs-12">
                                    <asp:Label runat="server" ID="Label29" Text="* Altura" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="TextCoAasegAltura"  onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" AutoPostBack="true" class="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" FilterMode="ValidChars" TargetControlID="TextCoAasegAltura" ValidChars="0123456789." />
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator41" validationgroup="CoAsegurado" InitialValue="0.00"  ControlToValidate="TextCoAasegAltura" ErrorMessage="*" runat="server" ForeColor="Red"/>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label38" Text="* Pregunta 1" Font-Bold="True" Visible ="true" class="control-label"></asp:Label>
                                    <asp:DropDownList ID="DropDownList3" runat="server"  AutoPostBack="True" class="form-control" Visible="true">
                                        <asp:ListItem Value="-1">SELECCIONAR</asp:ListItem>
                                        <asp:ListItem Value=1>SI</asp:ListItem>
                                        <asp:ListItem Value=0>NO</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator44" validationgroup="CoAsegurado" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="DropDownList3" ForeColor="Red" InitialValue="-1" Font-Size="16px"></asp:RequiredFieldValidator>
                                </div>
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label39" Text="* Pregunta 2" Font-Bold="True" Visible ="true" class="control-label"></asp:Label>
                                    <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" class="form-control" Visible="true">
                                        <asp:ListItem Value="-1">SELECCIONAR</asp:ListItem>
                                        <asp:ListItem Value=1>SI</asp:ListItem>
                                        <asp:ListItem Value=0>NO</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator52" validationgroup="CoAsegurado" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="DropDownList4" ForeColor="Red" InitialValue="-1" Font-Size="16px"></asp:RequiredFieldValidator>
                                </div>
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label40" Text="* Pregunta 3" Font-Bold="True" Visible ="true" class="control-label"></asp:Label>
                                    <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" class="form-control" Visible="true" OnSelectedIndexChanged="cboPregunta4_SelectedIndexChanged">
                                        <asp:ListItem Value="-1">SELECCIONAR</asp:ListItem>
                                        <asp:ListItem Value=1>SI</asp:ListItem>
                                        <asp:ListItem Value=0>NO</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator53" validationgroup="CoAsegurado" runat="server" Text="*" ControlToValidate="DropDownList5" ForeColor="Red" InitialValue="-1" Font-Size="16px"></asp:RequiredFieldValidator>
                                </div>
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label41" Font-Bold="True" Visible="False" Text="* Padecimientos" class="control-label"></asp:Label>
                                    <dx:ASPxComboBox ID="ASPxComboBoxPadecimiento2" Visible="False" runat="server" SelectedIndex="-1" AutoPostBack="true" Theme="Material" EditFormat="Custom" Width="100%">
                                    </dx:ASPxComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" validationgroup="CoAsegurado" ControlToValidate="ASPxComboBoxPadecimiento2" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Button ID="ButtonCoasegurados" validationgroup="CoAsegurado" runat="server" Text="Registrar CoAsegurado" class="btn btn-primary" OnClick="BtnRegistrarCoasegurado"/>
                                    <asp:Button ID="ButtonActualizarCoasegurados" Visible="false" validationgroup="CoAsegurado" runat="server" Text="Actualizar CoAsegurado" class="btn btn-primary" OnClick="BtnActualizarCoasegurado"/>
                                </div>
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Button ID="btnCancelarCoasegurado" runat="server" Text="Cancelar" class="btn btn-danger col-md-3 col-sm-3 col-xs-12" CausesValidation="false"  Visible="True" OnClick="BtnCancelarCoasegurado_Click"/>
                                </div>
                                <div class="control-label col-md-5 col-sm-5 col-xs-12">
                                    <code><asp:Label runat="server" ID="LabelRespuestaCoasegurados" Text=""></asp:Label></code>
                                </div>
                            </div>
                            <hr />
                        </asp:Panel>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <asp:Repeater ID="RepeterCoasegurados" runat="server" >
                                    <HeaderTemplate>
                                        <table id="datatable" class="table table-striped table-bordered table-responsive jambo_table bulk_action">
                                            <thead>
                                                <tr>
                                                    <th>Nombre</th>
                                                    <th>Apellido Paterno</th>
                                                    <th>Apellido Materno</th>
                                                    <th>Prentesco</th>
                                                    <th>Sexo </th>
                                                    <th>Fecha Nacimiento </th>
                                                    <th>Edad </th>
                                                    <th>Peso </th>
                                                    <th>Altura </th>
                                                    <th>Cotización </th>
                                                    <th>Acción </th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("Nombre")%></td>
                                            <td><%#Eval("APaterno")%></td>
                                            <td><%#Eval("AMaterno")%></td>
                                            <td><%#Eval("Interprestacion_larga")%></td>
                                            <td><%#Eval("Sexo")%></td>
                                            <td><%#Eval("FechaNacimiento","{0:dd/MM/yyyy }")%></td>
                                            <td><%#Eval("Edad")%></td>
                                            <td><%#Eval("Peso")%></td>
                                            <td><%#Eval("Altura")%></td>
                                            <td><%#Eval("ExamenEvaluacion")%></td>
                                            <td style="text-align:center;">
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../Imagenes/notepad-icon.png" CommandName ="Consultar" CommandArgument='<%#Eval("Id")%>' OnCommand="ModificarCoaegurado" />
                                                <asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="../../Imagenes/boton-eliminar-png-1.png" CommandName ="Consultar" CommandArgument='<%#Eval("Id")%>' OnCommand="EliminaCoasegurado" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <hr />
                        <asp:Panel ID="PanelTarjeta" runat="server" Visible="False" Enabled="False">
                        <div class="row">
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="Label9" Text="*Periodicidad de pago" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:DropDownList ID="cboCatPerioricidad" runat="server" AutoPostBack="True" class="form-control">
                                    <asp:ListItem Value="0">SELECCIONAR</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" validationgroup="Targeta"  runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="cboCatPerioricidad" ForeColor="Red" InitialValue="-1" Font-Size="16px"></asp:RequiredFieldValidator>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="Label10" Text="*Modo de Pago / Información Bancaria" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:DropDownList ID="cboCatModoPago" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="ccboCatModoPago_SelectedIndexChanged">
                                    <asp:ListItem Value="0">SELECCIONAR</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" validationgroup="Targeta" runat="server" Text="*" ControlToValidate="cboCatModoPago" ForeColor="Red" InitialValue="-1" Font-Size="16px"></asp:RequiredFieldValidator>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="Label11" Text="*Banco Emisor de Tarjeta / CLABE" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:DropDownList ID="cboCatBancos" runat="server" AutoPostBack="True" class="form-control">
                                    <asp:ListItem Value="0">SELECCIONAR</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" validationgroup="Targeta" runat="server" Text="*" ControlToValidate="cboCatBancos" ForeColor="Red" InitialValue="-1" Font-Size="16px"></asp:RequiredFieldValidator>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="Label12" Text="*Número de TOKEN / CLABE" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:TextBox ID="TextToken" runat="server" MaxLength="18" class="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterMode="ValidChars" TargetControlID="TextToken" ValidChars="1234567890" />
                                <asp:RequiredFieldValidator  runat="server" id="RequiredFieldValidator17" validationgroup="Targeta" controltovalidate="TextToken" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                            </div>
                        </div>
                        <div class="row">
                            <div class="control-label col-md-2 col-sm-2 col-xs-12">
                                <asp:Button ID="Button4" validationgroup="Targeta" runat="server" Text="Registrar tarjeta" class="btn btn-primary" OnClick="BtnRegistrarTarjeta_click"/>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <code><asp:Label runat="server" ID="LabelRespuestaTarjeta" Text=""></asp:Label></code>
                            </div>
                        </div>
                        </asp:Panel>
                        <div class="control-label col-md-2 col-sm-2 col-xs-12">
                            <asp:Button ID="ButtonNuevaTarjeta" validationgroup="Targeta" runat="server" Text="Nueva tarjeta" class="btn btn-primary" OnClick="BtnNuevaTarjeta_click"/>
                        </div>
                        <br /><br /><br />
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <asp:Repeater ID="RepeterTarjetas" runat="server" >
                                    <HeaderTemplate>
                                        <table id="datatable" class="table table-striped table-bordered table-responsive">
                                            <thead>
                                                <tr>
                                                    <th>Periodicidad</th>
                                                    <th>Modo de Pago</th>
                                                    <th>Banco</th>
                                                    <th>Token</th>
                                                    <th>Acción </th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("periodicidad")%></td>
                                            <td><%#Eval("modo_pago")%></td>
                                            <td><%#Eval("banco")%></td>
                                            <td><%#Eval("Token")%></td>
                                            <td style="text-align:center;"><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="../../Imagenes/boton-eliminar-png-1.png" CommandName ="Consultar" CommandArgument='<%# Eval("Id")%>' OnCommand="EliminaTarjeta" /></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <hr />
                        <!-- REGISTRAR O CANCELAR ACCION -->
                        <div class="row">
                            <asp:Button ID="GuardarCapturaMasiva" validationgroup="RegistroAsegurado" runat="server" Text="Guardar" class="btn btn-success col-md-3 col-sm-3 col-xs-12" Visible="False"  OnClick="BtnGuardarCaptura_click"/>
                            <asp:Button ID="CancelarCapturaMasiva" runat="server" Text="Cancelar" class="btn btn-danger col-md-3 col-sm-3 col-xs-12" CausesValidation="false"  Visible="False" OnClick="BtnCancelarCapturaMasiva_Click"/>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12 text-center">
                                <code><asp:Label runat="server" ID="Label14" Text="" Visible="false"></asp:Label></code>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    
    <!-- ELEMENTOS DE CAPTURA MASIVA - MESA CAPTURA -->
    <asp:Panel ID="PanelCapturaMasivaMesaCaptura" runat="server" Visible="False" Enabled="False">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Captura</small> </h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content text-left">
                        <!-- SECCION DE CAPTURA -->
                        <div class="row">
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <table id="datatable" class="table table-striped table-bordered table-responsive jambo_table bulk_action">
                                    <thead>
                                        <tr>
                                            <th>Fecha firma solicitud</th>
                                            <th>Lugar de firma </th>
                                            <th>Suma asegura en UMAN</th>
                                            <th>Agente</th>
                                            <th>Cotización</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td><asp:Label runat="server" ID="LabelFechaFirmaSolicitudtxt" Text="No Encontrado" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelLugarFirma" Text="No Encontrado" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelUMANtxt" Text="No Encontrado" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelAgentetxt" Text="No Encontrado" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelCotizaciontxt" Text="No Encontrado" Font-Bold="True" class="control-label"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        
                        <div class="row">
                            <div class="control-label col-md-2 col-sm-2 col-xs-12">
                                <asp:Button ID="btnCoaseguradosMesaCaptura" runat="server" Text="Nuevo CoAsegurado" class="btn btn-primary" OnClick="NuevoCoaseguradoMesaCaptura"/>
                            </div>
                        </div>
                        <asp:Panel ID="PanelCapturaCoaseguradoMesaCaptura" runat="server" Visible="False" Enabled="False">
                            <div class="row">
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label57" Text="*Nombre(s)" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="TextCoAseNombreMesaCaptura" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
								    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server" FilterMode="ValidChars" TargetControlID="TextCoAseNombreMesaCaptura" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    <asp:RequiredFieldValidator runat="server" validationgroup="CoAsegurado" id="RequiredFieldValidator65" controltovalidate="TextCoAseNombreMesaCaptura" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label59" Text="*Apellido Paterno" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="TextCoAseApaternoMesaCaptura" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server" FilterMode="ValidChars" TargetControlID="TextCoAseApaternoMesaCaptura" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    <asp:RequiredFieldValidator runat="server" validationgroup="CoAsegurado" id="RequiredFieldValidator66" controltovalidate="TextCoAseApaternoMesaCaptura" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label61" Text="*Apellido Materno" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:TextBox ID="TextCoAseMaternoMesaCaptura" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server" FilterMode="ValidChars" TargetControlID="TextCoAseMaternoMesaCaptura" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    <asp:RequiredFieldValidator runat="server" validationgroup="CoAsegurado" id="RequiredFieldValidator67" controltovalidate="TextCoAseMaternoMesaCaptura" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                    <asp:Label runat="server" ID="Label64" Text="* Parentesco" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:DropDownList ID="cboCat_ParentescoMesaCaptura" runat="server" AutoPostBack="True" class="form-control">
                                        <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator68" validationgroup="CoAsegurado"  runat="server" ControlToValidate="cboCat_ParentescoMesaCaptura" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label66" Text="* Sexo" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:DropDownList ID="cboCoAsegSexoMesaCaptura" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="cboTipoContratante_SelectedIndexChanged">
                                        <asp:ListItem Value="0">SELECCIONAR</asp:ListItem>
                                        <asp:ListItem Value=1>MASCULINO</asp:ListItem>
                                        <asp:ListItem Value=2>FEMENINO</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator69" validationgroup="CoAsegurado" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="cboCoAsegSexoMesaCaptura" ForeColor="Red" InitialValue="0" Font-Size="16px"></asp:RequiredFieldValidator>
                                </div>
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label68" Text="*Fecha Nacimiento" Font-Bold="True" class="control-label"></asp:Label>
                                    <dx:ASPxDateEdit ID="dtCoAsFechaNacimientoMesaCaptura" runat="server" Theme="Material" EditFormat="Custom" Width="100%" Caption="" >
                                            <TimeSectionProperties>
                                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                            </TimeSectionProperties>
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline"/>
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator70" validationgroup="CoAsegurado" controltovalidate="dtCoAsFechaNacimientoMesaCaptura" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                            </div>
                            <hr />
                            
                            <div class="row">
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Button ID="ButtonCoaseguradosMesaCaptura" validationgroup="CoAsegurado" runat="server" Text="Registrar CoAsegurado" class="btn btn-primary" OnClick="BtnRegistrarCoaseguradoMesaCaptura"/>
                                    <asp:Button ID="ButtonActualizarCoaseguradosMesaCaptura"  validationgroup="CoAsegurado" runat="server" Text="Actualizar CoAsegurado" class="btn btn-primary" OnClick="BtnActualizarCoaseguradoMesaCaptura"/>
                                </div>
                                <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                    <asp:Button ID="btnCancelarCoaseguradoMesaCaptura" runat="server" Text="Cancelar" class="btn btn-danger col-md-3 col-sm-3 col-xs-12" CausesValidation="false"  Visible="True" OnClick="BtnCancelarCoaseguradoMesaCaptura_Click"/>
                                </div>
                                <div class="control-label col-md-5 col-sm-5 col-xs-12">
                                    <code><asp:Label runat="server" ID="LabelRespuestaCoaseguradosMesaCaptura" Text=""></asp:Label></code>
                                </div>
                            </div>
                            <hr />
                        </asp:Panel>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <asp:Repeater ID="RepeterCoaseguradosMesaCaptura" runat="server" >
                                    <HeaderTemplate>
                                        <table id="datatable" class="table table-striped table-bordered table-responsive jambo_table bulk_action">
                                            <thead>
                                                <tr>
                                                    <th>Nombre</th>
                                                    <th>Apellido Paterno</th>
                                                    <th>Apellido Materno</th>
                                                    <th>Prentesco</th>
                                                    <th>Sexo </th>
                                                    <th>Fecha Nacimiento </th>
                                                    <th>Acción </th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("Nombre")%></td>
                                            <td><%#Eval("APaterno")%></td>
                                            <td><%#Eval("AMaterno")%></td>
                                            <td><%#Eval("Interprestacion_larga")%></td>
                                            <td><%#Eval("Sexo")%></td>
                                            <td><%#Eval("FechaNacimiento","{0:dd/MM/yyyy }")%></td>
                                            <td style="text-align:center;">
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../Imagenes/notepad-icon.png" CommandName ="Consultar" CommandArgument='<%#Eval("Id")%>' OnCommand="ModificarCoaeguradoMesaCaptura" />
                                                <asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="../../Imagenes/boton-eliminar-png-1.png" CommandName ="Consultar" CommandArgument='<%#Eval("Id")%>' OnCommand="EliminaCoasegurado" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <asp:Repeater ID="RepeterTarjetasMesaCaptura" runat="server" >
                                    <HeaderTemplate>
                                        <table id="datatable" class="table table-striped table-bordered table-responsive jambo_table bulk_action">
                                            <thead>
                                                <tr>
                                                    <th>Periodicidad</th>
                                                    <th>Modo de Pago</th>
                                                    <th>Banco</th>
                                                    <th>Token</th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("periodicidad")%></td>
                                            <td><%#Eval("modo_pago")%></td>
                                            <td><%#Eval("banco")%></td>
                                            <td><%#Eval("Token")%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="Label31" Text="*Busca de CP" Font-Bold="True" class="control-label"></asp:Label>
                                <div class="col-md-11 col-sm-11 col-xs-12 form-group has-feedback">
                                    <div class="input-group col-xs-10">
                                        <asp:TextBox ID="TextBoxBCP" runat="server" MaxLength="5" class="form-control" ></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:Button  runat="server" validationgroup="BusquedaCP" CausesValidation="True" Text="Buscar" class="btn btn-primary" ToolTip="RFC" OnClick="BtnBuscaCP_click" />
                                        </span>  
                                    </div>
                                </div>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" FilterMode="ValidChars" TargetControlID="TextBoxBCP" ValidChars="0123456789" />
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator19" validationgroup="BusquedaCP" controltovalidate="TextBoxBCP" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                            </div>
                        </div>
                        <div class="row">
                            <div class="control-label col-md-4 col-sm-4 col-xs-12 text-center">
                                <code><asp:Label runat="server" ID="LabelRespuestaCP" Text=""></asp:Label></code>
                                <code style="background-color:aquamarine; text-decoration-color:blue"><asp:Label ID="LabelRespuestaCPSucceful" runat="server" Text ="" Visible="true"></asp:Label></code>
                            </div>
                        </div>
                        <div class="row">
                            <!--  -->
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="Label15" Font-Bold="True" Visible="True" Text="* Estado" class="control-label"></asp:Label>
                                <dx:ASPxComboBox ID="cboCatDireccionesEstados" Visible="True" runat="server" SelectedIndex="-1" AutoPostBack="true" Theme="Material" EditFormat="Custom" Width="100%" OnSelectedIndexChanged="cboCatDireccionesEstados_SelectedIndexChanged">
                                </dx:ASPxComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" validationgroup="Direciones" ControlToValidate="cboCatDireccionesEstados" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="Label17" Font-Bold="True" Visible="True" Text="* Poblacion" class="control-label"></asp:Label> <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButtonPoblacion_Click"> -  Agregar</asp:LinkButton>
                                <dx:ASPxComboBox ID="cboCatDireccionesPoblacion" Visible="True" runat="server" SelectedIndex="-1" AutoPostBack="true" Theme="Material" EditFormat="Custom" Width="100%" OnSelectedIndexChanged="cboCatDireccionesPoblacion_SelectedIndexChanged">
                                </dx:ASPxComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" validationgroup="Direciones" ControlToValidate="cboCatDireccionesPoblacion" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="Label43" Text="*Código Póstal" Font-Bold="True" class="control-label"></asp:Label> <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButtonCP_Click"> -  Agregar</asp:LinkButton>
                                <dx:ASPxComboBox ID="cboCatDireccionesCP" Visible="True" runat="server" SelectedIndex="-1" AutoPostBack="true" Theme="Material" EditFormat="Custom" Width="100%" OnSelectedIndexChanged="cboCatDireccionesCP_SelectedIndexChanged">
                                </dx:ASPxComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ErrorMessage="" Text="*" ControlToValidate="cboCatDireccionesCP" ForeColor="Red" InitialValue="-1" Font-Size="16px"></asp:RequiredFieldValidator>
                            </div>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12">
                                <asp:Label runat="server" ID="Label18" Font-Bold="True" Visible="True" Text="* Colonia" class="control-label"></asp:Label> <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButtonColonia_Click"> -  Agregar</asp:LinkButton>
                                <dx:ASPxComboBox ID="cboCatDireccionesColonia" Visible="True" runat="server" SelectedIndex="-1" AutoPostBack="true" Theme="Material" EditFormat="Custom" Width="100%">
                                </dx:ASPxComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" validationgroup="Direciones" ControlToValidate="cboCatDireccionesColonia" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="control-label col-md-6 col-sm-6 col-xs-12">
                                <asp:Label runat="server" ID="Label16" Text="*Calle / Avenida, Numero Exterior, Numero Interior" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:TextBox ID="TextBox4" runat="server" MaxLength="64" class="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterMode="ValidChars" TargetControlID="TextBox4" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ 1234567890&/," />
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator20" validationgroup="Direciones" controltovalidate="TextBox4" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                            </div>
                        </div>

                        <!-- REGISTRAR O CANCELAR ACCION -->
                        <div class="row">
                            <asp:Button ID="GuardarDireccion" validationgroup="Direciones" runat="server" Text="Guardar Direccion" class="btn btn-primary col-md-3 col-sm-3 col-xs-12" Visible="False" OnClick="BtnGuardarDirecion_Click"/>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12 text-center">
                                <code><asp:Label runat="server" ID="Label21" Text=""></asp:Label></code>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <asp:Repeater ID="RepeaterDireciones" runat="server" >
                                    <HeaderTemplate>
                                        <table id="datatable" class="table table-striped table-bordered table-responsive jambo_table bulk_action">
                                            <thead>
                                                <tr>
                                                    <th>Estado</th>
                                                    <th>Población </th>
                                                    <th>CP</th>
                                                    <th>Colonia</th>
                                                    <th>Calle / Avenida, Numero Exterior, Numero Interior </th>
                                                    <th>Accion</th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("EstadoNombre")%></td>
                                            <td><%#Eval("PoblacionNombre")%></td>
                                            <td><%#Eval("CP")%></td>
                                            <td><%#Eval("ColoniaNombre")%></td>
                                            <td><%#Eval("Direccion")%></td>
                                            <td><asp:ImageButton ID="imbtnEditarDireccion" runat="server" ImageUrl="../../Imagenes/boton-eliminar-png-1.png" CommandName ="Consultar" CommandArgument='<%#Eval("Id")%>' OnCommand="EliminaDireccion" /></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                <asp:Label runat="server" ID="Label45" Text="*Teléfono" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:TextBox ID="TextBoxAseguradoTelefono" runat="server" MaxLength="15" class="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" FilterMode="ValidChars" TargetControlID="TextBoxAseguradoTelefono" ValidChars="1234567890" />
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator57" validationgroup="DatosExtraAsegurado" controltovalidate="TextBoxAseguradoTelefono" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                            </div>
                            <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                <asp:Label runat="server" ID="Label47" Text="*Correo electrónico " Font-Bold="True" class="control-label"></asp:Label>
                                <asp:TextBox ID="TextBoxAseguradoEmail" runat="server" MaxLength="64" class="form-control"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" FilterMode="ValidChars" TargetControlID="TextBoxAseguradoEmail" ValidChars="@abcdefghijklmnñopqrstuvwxyz-_&ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ 1234567890&/," />
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator58" validationgroup="DatosExtraAsegurado" controltovalidate="TextBoxAseguradoEmail" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                <asp:Label runat="server" ID="Label48" Text="* Plan MedicaLife" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:DropDownList ID="cbCatPlanMedicalife" runat="server" AutoPostBack="True" class="form-control">
                                    <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator59" validationgroup="DatosExtraAsegurado"  runat="server" ControlToValidate="cbCatPlanMedicalife" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                <asp:Label runat="server" ID="Label49" Text="* Deducible en pesos" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:DropDownList ID="cbCatDeduciblePesos" runat="server" AutoPostBack="True" class="form-control">
                                    <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator60" validationgroup="DatosExtraAsegurado"  runat="server" ControlToValidate="cbCatDeduciblePesos" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                <asp:Label runat="server" ID="Label50" Text="* Causa seguro" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:DropDownList ID="cbCatCausaSeguro" runat="server" AutoPostBack="True" class="form-control">
                                    <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator61" validationgroup="DatosExtraAsegurado"  runat="server" ControlToValidate="cbCatCausaSeguro" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                <asp:Label runat="server" ID="Label51" Text="* Región" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:DropDownList ID="cbCatRegion" runat="server" AutoPostBack="True" class="form-control">
                                    <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator62" validationgroup="DatosExtraAsegurado"  runat="server" ControlToValidate="cbCatRegion" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                <asp:Label runat="server" ID="Label52" Text="* Tipo producto" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:DropDownList ID="cbCatTipoProducto" runat="server" AutoPostBack="True" class="form-control">
                                    <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator63" validationgroup="DatosExtraAsegurado"  runat="server" ControlToValidate="cbCatTipoProducto" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                                <div class="control-label col-md-3 col-sm-3 col-xs-12">
                                    <asp:Label runat="server" ID="Label70" Text="*Fecha de antigüedad" Font-Bold="True" class="control-label"></asp:Label>
                                    <dx:ASPxDateEdit ID="FechaAntiguedad" runat="server" Theme="Material" EditFormat="Custom" Width="100%" Caption="" >
                                            <TimeSectionProperties>
                                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                            </TimeSectionProperties>
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline"/>
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator71" validationgroup="DatosExtraAsegurado" controltovalidate="FechaAntiguedad" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                            </div>
                        <div class="row">
                            <asp:Button ID="GuardarMesaCapturaMasiva" validationgroup="DatosExtraAsegurado" runat="server" Text="Guardar" class="btn btn-success col-md-3 col-sm-3 col-xs-12" Visible="False" OnClick="BtnGuardarDatosAsegurado_Click"/>
                            <asp:Button ID="CancelarMesaCapturaMasiva" runat="server" Text="Cancelar" class="btn btn-danger col-md-3 col-sm-3 col-xs-12" CausesValidation="false"  Visible="False" OnClick="BtnCancelarMesaCapturaMasiva_Click"/>
                            <div class="control-label col-md-4 col-sm-4 col-xs-12 text-center">
                                <code><asp:Label runat="server" ID="Label46" Text=""></asp:Label></code>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- DATOS MOSTRADOS EN TODAS LAS MESAS -->  
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 text-left">
            <div class="x_panel">
                <div class="x_title">
                    <h2><small> Información del trámite  </small> </h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li>
                            <asp:Label runat="server" ID="LabelFlujo" Text="" Font-Bold="True" ></asp:Label>
                            <br />
                            <asp:Label runat="server" ID="LabelNombreMesa" Text="" Font-Bold="True" ></asp:Label>
                      	</li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content text-left" >
                    <div class="row">
                        <!-- IMFORMAICON DEL TRAMITE -->
<!-- =================================================================================================================================================================================================== -->
                        <div class="col-md-8 col-sm-8 col-xs-12">
                            <asp:UpdatePanel id="DatosTramiteInformacion" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table border="0" style="width: 100%;">    
                                        <tr>
                                            <td style="text-align:center; border-bottom: 1px solid #ddd; color:black; background-color:#8EBB53;"><asp:Label ID="Label55" runat="server" Font-Names="Britannic Bold" Font-Size="12px" > Fecha de Registro: </asp:Label></td>
                                            <td colspan="2" style="text-align:center; border-bottom: 1px solid #ddd; color:black; background-color:#8EBB53;">
                                                <asp:Label ID="InfoFechaRegistro" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                            </td>
                                        </tr>
                                        <asp:Panel ID="TramiteInformacionCPDES" runat="server" Visible="false" >
                                            <tr>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Folio CPDES</td>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Estatus CPDES</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoFolioCPDES" runat="server" ></asp:Label>
                                                </td>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoEstatusCPDES" runat="server" ></asp:Label>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="color:#244f02; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                       
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="CantidadesVida" runat="server" Visible="false" >
                                            <tr>
                                                <td style="width:35%; background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Moneda
                                                </td>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Suma Asegurada Básica
                                                </td>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Suma Asegurada de Pólizas Vigentes
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoMoneda" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                </td>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoSumaAseguradaBasica" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                </td>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoSumaAseguradaPolizasVigentes" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Prima Total de Acuerdo a Cotización
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoPrimaTotal" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="CantidadesGastosMedicos" runat="server" Visible="false" >
                                            <tr>
                                                <td style="width:35%; background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Moneda
                                                </td>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Suma Asegurada Básica
                                                </td>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Prima Total de Acuerdo a Cotización
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoMonedaGM" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                </td>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoSumaAseguradaBasicaGM" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                </td>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoPrimaTotalGM" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="PanelInstitucionesInfo" runat="server" Visible="False">
                                            <tr>
                                                <td style="color:#000000; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="Label81" Text="Tipo de Servicio" runat="server" ></asp:Label>
                                                </td>
                                                <td colspan="2" style="color:#000000; font-size:smaller;  font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="lblTipoServicioValue" runat="server" Font-Bold="true" ></asp:Label>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="HombresClave" runat="server" Visible="false">
                                            <tr>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    Hombre Clave
                                                </td>
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoHobreClave" Text="NO" runat="server" ></asp:Label>
                                                </td>
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="color:#244f02; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoGrandeSumas" runat="server" ></asp:Label>
                                                </td>
                                            </tr>
                                        
                                            <tr>
                                                <td style="width:35%; background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Moneda
                                                </td>
                                                <td colspan="2" style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Prima Total de Acuerdo a Cotización
                                                </td>
                                            </tr>
                                        </asp:Panel>        
                                        <asp:Panel ID="monedadisplay" runat="server" Visible="false">
                                            <tr>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoGMMoneda" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                </td>
                                                <td colspan="2" style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoGMPrimaTotal" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="PanelRiesgo" runat="server" Visible="false">
                                        <tr>
                                            <td style="width:35%; background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Riesgo
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                <asp:Label ID="InfoRiesgo" Text="daot" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                            </td>
                                        </tr>
                                        <!--------- ------------->
                                        </asp:Panel>
                                        <tr>
                                            <td colspan="3" style="text-align:center; border-bottom: 1px solid #ddd; color:black; background-color:#8EBB53;"> 
                                                INFORMACIÓN DE PÓLIZA
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                Clave Promotoria
                                            </td>
                                            <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                Zona Promotoria
                                            </td>
                                            <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                <asp:Label ID="InfoClave" runat="server" ></asp:Label>
                                            </td>
                                            <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                <asp:Label ID="InfoZona" runat="server" ></asp:Label>
                                            </td>
                                            <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr style="display:none">
                                            <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                Ejecutivo Comercial 
                                            </td>
                                            <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                Ejecutivo Front 
                                            </td>
                                            <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                Número de Póliza o Solicitud
                                            </td>
                                                    
                                        </tr>
                                        <tr style="display:none">
                                            <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                <asp:Label ID="InfoEjecutivo" runat="server" ></asp:Label>
                                            </td>
                                            <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                <asp:Label ID="InfoEjecutivoFront" runat="server" ></asp:Label>
                                            </td>
                                            <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                <asp:Label ID="InfoNumero" runat="server" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                Fecha Firma solicitud 
                                            </td>
                                            <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                
                                            </td>
                                            <td id="tipoContratanteTitulo" runat="server" style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                &nbsp;
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                <asp:Label ID="InfoFechaSolicitud" runat="server" ></asp:Label>
                                            </td>
                                            <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                &nbsp;
                                            </td>
                                            <td id="tipoContratanteValue" runat="server" style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                <asp:Label ID="InfoContratante" runat="server" Visible="false" ></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align:center; border-bottom: 1px solid #ddd; color:black; background-color:#8EBB53;"> 
                                                INFORMACIÓN CONTRATANTE 
                                            </td>
                                        </tr>
                                        <asp:Panel ID="InfoPrsFisica" runat="server" Visible="false" >
                                            <tr>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    Apellido Paterno
                                                </td>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    Apellido Materno
                                                </td>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    Nombre(s) 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoFApellidoP" runat="server" ></asp:Label>
                                                </td>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoFApellidoM" runat="server" ></asp:Label>
                                                </td>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoFNombre" runat="server" ></asp:Label>
                                                </td>
                                            </tr>
                                            <asp:Panel ID="datosContratanteGral" runat="server" Visible="false">
                                                <tr>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Sexo
                                                    </td>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        RFC
                                                    </td>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Nacionalidad
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoFSexo" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoFRFC" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoFNacionalidad" runat="server" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Fecha Nacimiento
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoFFechaNa" runat="server" ></asp:Label>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                            </asp:Panel>
                                        </asp:Panel>
                                        <asp:Panel ID="InfoPrsMoral" runat="server" Visible="false" >
                                            <tr>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    Nombre
                                                </td>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    Fecha de Constitución
                                                </td>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    RFC
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoMNombre" runat="server" ></asp:Label>
                                                </td>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoMFechaConsti" runat="server" ></asp:Label>
                                                </td>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoMRFC" runat="server" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="color:#244f02; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="InfoDiContratante" runat="server" Visible="false">
                                            <tr>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                ¿El solicitante es el <br />mismo que el contratante?
                                                </td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoFContratante" runat="server" ></asp:Label>
                                                </td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="color:#244f02; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    INFORMACIÓN TITULAR 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    Nombre(s) 
                                                </td>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    Apellido Paterno
                                                </td>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    Apellido Materno
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoTNombre" runat="server" ></asp:Label>
                                                </td>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoTApellidoP" runat="server" ></asp:Label>
                                                </td>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoTApellidoM" runat="server" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    Nacionalidad
                                                </td>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    Sexo
                                                </td>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    Fecha Nacimiento
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoTNacionalidad" runat="server" ></asp:Label>
                                                </td>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoTSexo" runat="server" ></asp:Label>
                                                </td>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoTNacimiento" runat="server" ></asp:Label>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                    </table>
                                    <br />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <asp:Panel id="PanelCheckBoxList" Visible="false" runat="server">
                                        <table class="table table-hover table-bordered">
                                            <thead>
                                                <tr>
                                                    <th style="text-align:center">DOCUMENTOS REQUERIDOS </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="DocRequerid" runat="server" AutoPostBack="true">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
<!-- =================================================================================================================================================================================================== -->
                        
                        <!-- INFORMACION PRODUCTO Y SUB PRODUCTO -->
                        <div class="col-md-4 col-sm-4 col-xs-12">
                            <table class="table table-hover table-bordered">
                                <thead>
                                    <tr>
                                        <th colspan="2" style="text-align:center; border-bottom: 1px solid #ddd; background-color:#8EBB53; color:black;">Folio: <asp:Label runat="server" ID="LabelFolio" Text="Folio" Font-Bold="False" class="control-label"></asp:Label></th>
                                    </tr>
                                    <asp:Panel runat="server" ID="tituloProducto" Visible="false"> 
                                        <tr>
                                            <th colspan="2" style="text-align:center; border-bottom: 1px solid #ddd; background-color:#EADB20; color:black;">Número de póliza: <asp:Label runat="server" ID="LabelNumeroPoliza" Text="-" Font-Bold="False" class="control-label"></asp:Label></th>
                                        </tr>
                                        <tr>
                                            <th style="background-color:#1572B7; color:white; text-align:center;">Producto </th>
                                            <th style="background-color:#1572B7; color:white; text-align:center;">Sub Producto </th>
                                        </tr>
                                    </asp:Panel>
                                </thead>
                                <tbody>
                                    <asp:Panel runat="server" ID="Panel1" Visible="false"> 
                                        <tr>
                                            <td class="text-center"><asp:Label runat="server" ID="LabelProducto" Text="Producto" Font-Bold="False" class="control-label text-center"></asp:Label></td>
                                            <td class="text-center"><asp:Label runat="server" ID="LabelSubProducto" Text="Sub Producto" Font-Bold="False" class="control-label text-center"></asp:Label></td>
                                        </tr>
                                    </asp:Panel>
                                 </tbody>
                            </table>
                            <div class="row" style="display:none">
                                <asp:Repeater ID="StatusMesa" runat="server" >
                                    <HeaderTemplate>
                                        <table id="#" class="table table-striped table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Mesa</th>
                                                    <th>Status</th>
                                                    <th>  </th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("Mesa")%></td>
                                            <td><%#Eval("StatusMesa")%></td>
                                            <td><a href="#"><i class="fa fa-flag fa-2x" style="color:<%#Eval("Color")%>"></i></a></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    
                    <asp:Panel ID="botonesEdicionDatos" runat="server" Visible="false">
                        <!-- BOTONES ADMISION EDITAR INFORMACION -->
                        <asp:Button ID="EditarCaptura" runat="server" Text="Editar" class="btn btn-default col-md-4 col-sm-4 col-xs-12" CausesValidation="false"  Visible="false" OnClick="BtnEditar_Click"/>
                        <asp:Button ID="Captura" runat="server" Text="Captura" class="btn btn-default col-md-4 col-sm-4 col-xs-12" CausesValidation="false"  Visible="false" OnClick="BtnCaptura_Click"/>
                        <asp:Button ID="MesaCapturaMasiva" runat="server" Text="Captura" class="btn btn-default col-md-4 col-sm-4 col-xs-12" CausesValidation="false"  Visible="false" OnClick="BtnCapturaMasiva_Click"/>
                        <code><asp:Label ID="LabelConsultaCaptura" runat="server" Text ="" Visible="false"></asp:Label></code>
                        <code style="background-color:aquamarine; text-decoration-color:blue"><asp:Label ID="Label42" runat="server" Text ="" Visible="true"></asp:Label></code>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    
  

    <!-- ACCIONES -->
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 text-left">
            <div class="x_panel">
                <div class="x_title">
                    <h2><small>Acciones </small> </h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li>
                            <a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      	</li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content text-left" >
                    <div class="row">
                        <div class="control-label col-md-5 col-sm-5 col-xs-12">
                            <strong>OBSERVACIONES PRIVADAS</strong>
                            <asp:TextBox ID="txtObservacionesPrivadas" runat="server" class="form-control" TextMode="MultiLine" Rows="5" AutoPostBack="false" onkeypress="return soloLetras(event)" ></asp:TextBox>
                            <br />
                        </div>

                        <asp:UpdatePanel ID="PolizaSisLegados" runat="server" Visible="false" UpdateMode="Conditional" >
                            <ContentTemplate>
                               <div class="row control-label col-md-4 col-sm-4 col-xs-12 text-center">
                                    <asp:Label runat="server" ID="label1" Text="Folio de llamada telefónica" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                    <asp:TextBox ID="TextNumPolizaSisLegado" runat="server" class="form-control" MaxLength="20" AutoPostBack="false"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterMode="ValidChars" TargetControlID="TextNumPolizaSisLegado" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ@. = $%*_0123456789-" />
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="TextNumPolizaSisLegado" ForeColor="Crimson" errormessage="Coloca el número de póliza " Font-Size="16px"/>

                                   <br />

                                   <asp:Label runat="server" ID="lblNumIntentoLlamada" Text="Número de llamada (intento)" Font-Bold="True" class="control-label"></asp:Label>
                                    <asp:HiddenField ID="HiddenField7" runat="server" />
                                    <asp:TextBox ID="txtNumeroLlamada" runat="server" MaxLength="2" class="form-control" AutoPostBack="false"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" FilterMode="ValidChars" TargetControlID="txtNumeroLlamada" ValidChars="0123456789" />
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator75" controltovalidate="txtNumeroLlamada" ForeColor="Crimson" errormessage="coloca el número de intento de llamada" Font-Size="16px"/>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:Panel ID="KWIK" runat="server" Visible="false">
                            <div class="row control-label col-md-4 col-sm-4 col-xs-12 text-center">
                                <asp:Label runat="server" ID="label3" Text="Número KWIK" Font-Bold="True" class="control-label"></asp:Label>
                                <asp:HiddenField ID="HiddenField6" runat="server" />
                                <asp:TextBox ID="TextNumKwik" runat="server" MaxLength="22" AutoPostBack="false" class="form-control"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterMode="ValidChars" TargetControlID="TextNumKwik" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ@. = $%*_0123456789-" />
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" controltovalidate="TextNumKwik" ForeColor="Crimson" errormessage="Coloca el número de KWIK" Font-Size="16px"/>
                            </div>
                        </asp:Panel>

                        <div class="control-label col-md-7 col-sm-12 col-xs-12">
                            <div class="row">
                                <code><asp:Label runat="server" ID="Mensajes" Text=""></asp:Label></code>
                            </div>
                            <asp:Panel ID="PanelSeleccionCompleta" runat="server" Visible="false">
                                <div class="row">
                                    <div class="control-label col-md-5 col-sm-5 col-xs-12">
                                        <asp:Label runat="server" ID="Label78" Text="* Selección completa" Font-Bold="True" Visible ="true" class="control-label"></asp:Label>
                                        <asp:DropDownList ID="DropDownListSeleccionCompleta" runat="server" AutoPostBack="True" class="form-control" Visible="true">
                                            <asp:ListItem Value="-1">SELECCIONAR</asp:ListItem>
                                            <asp:ListItem Value=1>SELECCIÓN COMPLETA</asp:ListItem>
                                            <asp:ListItem Value=0>SIN SELECCIÓN COMPLETA</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator73" ValidationGroup="Observaciones" runat="server" Text=" " ControlToValidate="DropDownList5" ForeColor="Red" InitialValue="-1" Font-Size="16px"></asp:RequiredFieldValidator>
                                        <br />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="PanelSeleccionCompletaChecs" runat="server" Visible="false">
                                <div class="x_panel">
                                    <div class="x_title">
                                        <h3><small>Enviar a mesas </small> </h3>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content text-left" >
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <asp:CheckBox ID="CheckBoxRevisioTecnica"  runat="server" AutoPostBack="True" Text=" REVISIÓN TÉCNICA" />
                                                <br />
                                                <asp:CheckBox ID="CheckBoxRevisionMedica"  runat="server" AutoPostBack="True" Text=" REVISIÓN MÉDICA" /> 
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                 <asp:Button ID="Button5" Visible="true" ValidationGroup="Observaciones" runat="server" Text="Enviar" class="btn btn-success col-md-6 col-sm-6 col-xs-12" title="Aceptar" OnClick="btnSeleccionCompleta_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row" style="display:none;">
                                <asp:Button ID="btnTaskOk" Visible="true" ValidationGroup="Observaciones" runat="server" Text="Dar Respuesta" class="btn btn-success col-md-5 col-sm-5 col-xs-12" title="Tarea realizada con éxito" OnClick="btnTaskOk_Click" />
                                <asp:Button ID="btnTaskDo" Visible="true" ValidationGroup="Observaciones" runat="server" Text="Realizada" class="btn btn-info col-md-5 col-sm-5 col-xs-12" title="Tarea Realizada sin éxito" OnClick="btnTaskDo_Click" />
                            </div>
                            <div class="row">

                                <asp:Button ID="btnHold" Visible="false"  runat="server" Text="Contacto Efectivo" class="btn btn-info col-md-4 col-sm-4 col-xs-12"/>
                                <asp:Button ID="btnSuspender" Visible="false"  runat="server" Text="Contacto no Efectivo" class="btn btn-danger col-md-4 col-sm-4 col-xs-12"/>


                                <asp:Button ID="btnAceptar" Visible="false" ValidationGroup="Observaciones" runat="server" Text="Aceptar" class="btn btn-success col-md-2 col-sm-2 col-xs-12" title="Aceptar" OnClick="btnAceptarValida_Click" />
                                <asp:Button ID="btnPCI" Visible="false"  runat="server" Text="P C I" class="btn btn-danger col-md-2 col-sm-2 col-xs-12" data-toggle="modal" data-target=".confirmacionPCI"/>
                                <asp:Button ID="btnRechazar" Visible="false" runat="server" Text="Rechazar" class="btn btn-danger col-md-2 col-sm-2 col-xs-12"/>
                                <asp:Button ID="btnSelccionCompleta" Visible="false"  runat="server" Text="Selección" class="btn btn-dark col-md-2 col-sm-2 col-xs-12" data-toggle="modal" data-target=".confirmacionSeleccionCompleta"/>
                                <asp:Button ID="btnEnviaMesa" Visible="false" runat="server" Text="Enviar a Mesa" class="btn btn-info col-md-3 col-sm-3 col-xs-12" data-toggle="modal" title="Enviar a Mesa" data-target=".mSendToMesa"/>
                                <asp:Button ID="btnCancelacion" Visible ="false" runat="server" Text="Cancelación" class="btn btn-danger col-md-2 col-sm-2 col-xs-12"/>
                                <!-- <button type="button" class="btn btn-info col-md-3 col-sm-3 col-xs-12" data-toggle="modal" title="Enviar a Mesa" data-target=".mSendToMesa">Enviar a Mesa</i></button>-->
                            </div>
                            <div class="row" style="display:none;">
                                <asp:Button ID="btnPausa" Visible="false"  runat="server" Text="Pausa de Trámite" title="Se realiza pausa del trámite para su revisión"  class="btn btn-danger col-md-5 col-sm-5 col-xs-12"/>
                                <asp:Button ID="btnDetener" Visible="false"  runat="server" Text="Pausa de Usuario" title="Detiene la recepción de trámites automáticos por el WFO" class="btn btn-warning col-md-5 col-sm-5 col-xs-12" OnClick="btnDetener_Click"/>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="PanelObservacionesPubicas" runat="server" Visible="false">
                        <div class="row">
                            <div class="control-label col-md-5 col-sm-5 col-xs-12">
                                <strong>OBSERVACIONES PUBLICAS</strong>
                                <asp:TextBox ID="txtObservacionesPublicas" runat="server" ValidationGroup="Observaciones" class="form-control" TextMode="MultiLine" Rows="5" AutoPostBack="false" onkeypress="return soloLetras(event)" ></asp:TextBox>
                                <br />
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="row">
                        <div class="control-label col-md-5 col-sm-5 col-xs-12">

                            <div style="display:none;">
                                <button type="button" class="btn btn-default col-md-2 col-sm-2 col-xs-12" data-toggle="modal" title="Bitácora publica" data-target=".BitacoraPublica"><i class="fa fa-unlock-alt"></i></button>
                                <button type="button" class="btn btn-default col-md-2 col-sm-2 col-xs-12" data-toggle="modal" title="Bitácora privada" data-target=".BitacoraPrivada"><i class="fa fa-lock"></i> </button>
                            
                            </div>

                            <asp:LinkButton ID="LinkButtonAgregarArchivos" CausesValidation="false" runat="server" class="btn btn-default col-md-2 col-sm-2 col-xs-12"  title="Agregar archivos" Text="" OnClick="BtnExpediente_click" Visible="false"><i class="fa fa-file-archive-o"></i></asp:LinkButton>
                            <asp:LinkButton ID="LinkButtonCancelarArchivos" CausesValidation="false" runat="server" class="btn btn-default col-md-2 col-sm-2 col-xs-12"  title="Cerrar carga de archivos" Text="" OnClick="BtnExpedienteOcultarFrom_click" Visible="false"><i class="fa fa-external-link"></i></asp:LinkButton>
                            <asp:LinkButton ID="LinkButtonAbrirExpediente"  CausesValidation="false" runat="server" class="btn btn-default col-md-2 col-sm-2 col-xs-12"  title="Abrir documento" Text="" OnClick="BtnExpedienteAbrir_click" Visible="false"><i class="fa fa-desktop"></i></asp:LinkButton>
                            <asp:LinkButton ID="LinkButtonOcultarExpediente" CausesValidation="false" runat="server" class="btn btn-default col-md-2 col-sm-2 col-xs-12" title="Ocultar documento" Text="" OnClick="BtnExpedienteOcultar_click" visible="false"><i class="fa fa-ban"></i></asp:LinkButton>
                            <asp:LinkButton ID="LinkButtonMostrarExpediente" CausesValidation="false" runat="server" class="btn btn-default col-md-2 col-sm-2 col-xs-12" title="Mostrar documento" Text="" OnClick="BtnExpedienteMostrar_click" Visible="false"><i class="fa fa-file-text"></i></asp:LinkButton>
                            
                            <!--button type="button" class="btn btn-default col-md-2 col-sm-2 col-xs-12" data-toggle="modal" title="Expedientes" data-target=".Expediente"><i class="fa fa-archive"></i> </!--button-->
                           
                            <asp:LinkButton ID="LinkButtonMuestraCaptura" Visible ="false" CausesValidation="false" runat="server" class="btn btn-default col-md-2 col-sm-2 col-xs-12"  title="Abrir documento" Text="" OnClick="BtnCapturaAbrir_click"><i class="fa fa-external-link-square"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    
        <!-- DATOS DE MESA KWIK -->
        <asp:Panel ID="PanelDatosKwik" runat="server" Visible="False" Enabled="False">
        <div class="row" >
            <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small> Datos especiales  </small> </h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li>
                                <asp:Label runat="server" ID="Label53" Text="" Font-Bold="True" ></asp:Label>
                                <br />
                                <asp:Label runat="server" ID="Label56" Text="" Font-Bold="True" ></asp:Label>
                      	    </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content text-left">
                        <!-- SECCION DE CAPTURA -->
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <table id="datatable" class="table table-striped table-bordered table-responsive">
                                    <tbody>
                                        <tr>
                                            <td><asp:Label runat="server" ID="Label62" Text="Fecha firma de solicitud" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelInfFechaFirmaSolicitud" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label58" Text="Sucursal" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelInfSucursal" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="Label60" Text="RFC del asegurado" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelInfRFC" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="Label63" Text="Apellido Paterno Asegurado" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelInfAP" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label65" Text="Apellido Materno Asegurado" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelInfAM" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label67" Text="Nombre del Asegurado" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelInfNombre" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="Label69" Text="Numero de póliza" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelInfNPoliza" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label71" Text="No. De solicitud" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelInfNoSolicitud" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label73" Text="Clave del agente" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelInfClaveAgente" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="Label75" Text="Clave de promotoría" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelInfClavePromotoria" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="Label77" Text="Nombre de la promotoría" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td colspan="3"><asp:Label runat="server" ID="LabelInfNombrePromotoria" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="Label79" Text="Solicitud NV" Font-Bold="True" class="control-label"></asp:Label></td>
                                            <td><asp:Label runat="server" ID="LabelInfSolicitudNV" Text="No Encontrado" Font-Bold="False" class="control-label"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    

    <!-- CARGA DE ARCHIVOS A EXPEDIENTE HE INSUMOS -->
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
                                    <div class="col-md-6 col-sm-6 col-xs-12 well profile_view">
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

    <!-- EXPEDIENTE PDF -->
    <asp:Panel ID="Expediente" runat="server" Visible="true">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 text-left">
            <div class="x_panel">
                <div class="x_title">
                    <h2><small>Expediente</small> </h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li>
                            <a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      	</li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content text-left" style="height: 550px;">
                    <asp:Literal ID="ltMuestraPdf" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
    </asp:Panel>
    

    <!-- BOTNONES EMERGENTES CATALOGOS DE RECHASOS -->
    <!-- Botones Emergentes HOLD -->
    <dx:ASPxPopupControl ID="pnlPopMotivosHold" 
					    runat="server" 
					    CloseAction="CloseButton" 
					    HeaderText="Contacto Efectivo" 
					    ShowFooter="True" 
					    Theme="iOS" 
					    Width="350px" 
					    ClientInstanceName="pnlPopMotivosHold" 
					    Modal="True" 
					    PopupHorizontalAlign="WindowCenter" 
					    PopupVerticalAlign="WindowCenter" 
					    FooterText="">
	    <ContentStyle>
		    <Paddings Padding="5px" />
	    </ContentStyle>
	    <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxCallbackPanel ID="pnlCallbackMotHold" 
								    runat="server" 
					                ClientInstanceName="pnlCallbackMotHold" 
				                    Width="100%" 
								    OnCallback="pnlCallbackMotHold_Callback">
				    <PanelCollection>
	                    <dx:PanelContent runat="server">
						    <dx:ASPxTreeList ID="treeListHold" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" OnCustomDataCallback="treeList_CustomDataCallbackHold" OnDataBound="treeList_DataBoundHold" ParentFieldName="idParent" Width="100%">
                                <Columns>
                                    <dx:TreeListDataColumn AutoFilterCondition="Default" Caption="Motivos" FieldName="motivoRechazo" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="0"></dx:TreeListDataColumn>
                                </Columns>
                                <settingsbehavior allowautofilter="True" expandcollapseaction="NodeDblClick"></settingsbehavior>
                                <settingscustomizationwindow caption="" popuphorizontalalign="RightSides" popupverticalalign="BottomSides"></settingscustomizationwindow>
                                <settingsselection enabled="True"></settingsselection>
                                <settingspopupeditform verticaloffset="-1"></settingspopupeditform>
                                <settingspopup>
                                    <editform verticaloffset="-1"></editform>
                                </settingspopup>
                                <clientsideevents customdatacallback="treeList_CustomDataCallbackHold" selectionchanged="treeList_SelectionChangedHold"></clientsideevents>
                            </dx:ASPxTreeList>
                            <br />
                            <asp:Label ID="lblObservacionesPublicasHold" runat="server" Text="OBSERVACIONES PÚBLICAS" Font-Bold="True"></asp:Label>
                            <asp:TextBox ID="txtObservacionesPublicasHold" runat="server" TextMode="MultiLine" Width="98%" Height="50px" ></asp:TextBox>
					    </dx:PanelContent>
				    </PanelCollection>
	            </dx:ASPxCallbackPanel>
		    </dx:PopupControlContentControl>
        </ContentCollection>
	    <FooterTemplate>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>

                <div style="text-align:right;">
                    <br />&nbsp;
                    <asp:Button ID="btnAplicarHold" runat="server" CausesValidation="false" Text="Aplicar Contacto Efectivo" class="btn btn-info" OnClick="btnAplicarContactoEfectivo_Click"  OnClientClick="return preventMultipleSubmissions('#ContenidoPrincipal_pnlPopMotivosHold_TPCFm1_btnAplicarHold');"   />
                    <br />&nbsp;
		        </div>
                    </ContentTemplate>
            </asp:UpdatePanel>
	    </FooterTemplate>
    </dx:ASPxPopupControl>

    <!-- Botones Emergentes SUSPENDER -->
    <dx:ASPxPopupControl ID="pnlPopMotivosSuspender" 
					    runat="server" 
					    CloseAction="CloseButton" 
					    HeaderText="Contacto no Efectivo" 
					    ShowFooter="True" 
					    Theme="iOS" 
					    Width="350px" 
					    ClientInstanceName="pnlPopMotivosSuspender" 
					    Modal="True" 
					    PopupHorizontalAlign="WindowCenter" 
					    PopupVerticalAlign="WindowCenter" 
					    FooterText="">
	    <ContentStyle>
		    <Paddings Padding="5px" />
	    </ContentStyle>
	    <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxCallbackPanel ID="pnlCallbackMotSuspender" 
								    runat="server" 
					                ClientInstanceName="pnlCallbackMotSuspender" 
				                    Width="100%" 
								    OnCallback="pnlCallbackMotSuspender_Callback">
				    <PanelCollection>
	                    <dx:PanelContent runat="server">
						    <dx:ASPxTreeList ID="treeListSuspender" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" OnCustomDataCallback="treeList_CustomDataCallbackSuspender" OnDataBound="treeList_DataBoundSuspender" ParentFieldName="idParent" Width="100%">
                                <Columns>
                                    <dx:TreeListDataColumn AutoFilterCondition="Default" Caption="Motivos" FieldName="motivoRechazo" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="0"></dx:TreeListDataColumn>
                                </Columns>
                                <settingsbehavior allowautofilter="True" expandcollapseaction="NodeDblClick"></settingsbehavior>
                                <settingscustomizationwindow caption="" popuphorizontalalign="RightSides" popupverticalalign="BottomSides"></settingscustomizationwindow>
                                <settingsselection enabled="True"></settingsselection>
                                <settingspopupeditform verticaloffset="-1"></settingspopupeditform>
                                <settingspopup>
                                    <editform verticaloffset="-1"></editform>
                                </settingspopup>
                                <clientsideevents customdatacallback="treeList_CustomDataCallbackSuspender" selectionchanged="treeList_SelectionChangedSuspender"></clientsideevents>
                            </dx:ASPxTreeList>
                            <br />
                            <asp:Label ID="lblObservacionesPublicasSuspender" runat="server" Text="OBSERVACIONES PÚBLICAS" Font-Bold="True"></asp:Label>
                            <asp:TextBox ID="txtObservacionesPublicasSuspender" runat="server" TextMode="MultiLine" Width="98%" Height="50px" onkeypress="return soloLetras(event)" ></asp:TextBox>
					    </dx:PanelContent>
				    </PanelCollection>
	            </dx:ASPxCallbackPanel>
		    </dx:PopupControlContentControl>
        </ContentCollection>
	    <FooterTemplate>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <div style="text-align:right;">
                        <br />&nbsp;
                        <asp:Button ID="btnAplicarSuspender" runat="server" CausesValidation="false" Text="Aplicar Contacto no Efectivo" class="btn btn-danger"  OnClick="btnAplicarContactoNoEfectivo_Click" OnClientClick="return preventMultipleSubmissions('#ContenidoPrincipal_pnlPopMotivosSuspender_TPCFm1_btnAplicarSuspender');"/>
                        <br />&nbsp;
		            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
	    </FooterTemplate>
    </dx:ASPxPopupControl>
    
    <!-- Botones Emergentes RECHAZAR -->
    <dx:ASPxPopupControl ID="pnlPopMotivosRechazar" 
					    runat="server" 
					    CloseAction="CloseButton" 
					    HeaderText="Motivos de Rechazo" 
					    ShowFooter="True" 
					    Theme="iOS" 
					    Width="350px" 
					    ClientInstanceName="pnlPopMotivosRechazar" 
					    Modal="True" 
					    PopupHorizontalAlign="WindowCenter" 
					    PopupVerticalAlign="WindowCenter" 
					    FooterText="">
	    <ContentStyle>
		    <Paddings Padding="5px" />
	    </ContentStyle>
	    <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxCallbackPanel ID="pnlCallbackMotRechazar" 
								    runat="server" 
					                ClientInstanceName="pnlCallbackMotRechazar" 
				                    Width="100%" 
								    OnCallback="pnlCallbackMotRechazar_Callback">
				    <PanelCollection>
	                    <dx:PanelContent runat="server">
                            <dx:ASPxTreeList ID="treeListRechazar" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" OnCustomDataCallback="treeList_CustomDataCallbackRechazo" OnDataBound="treeList_DataBoundRechazo" ParentFieldName="idParent" Width="100%">
                                <Columns>
                                    <dx:TreeListDataColumn AutoFilterCondition="Default" Caption="Motivos de rechazo" FieldName="motivoRechazo" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="0"></dx:TreeListDataColumn>
                                </Columns>
                                <settingsbehavior allowautofilter="True" expandcollapseaction="NodeDblClick"></settingsbehavior>
                                <settingscustomizationwindow caption="" popuphorizontalalign="RightSides" popupverticalalign="BottomSides"></settingscustomizationwindow>
                                <settingsselection enabled="True"></settingsselection>
                                <settingspopupeditform verticaloffset="-1"></settingspopupeditform>
                                <settingspopup>
                                    <editform verticaloffset="-1"></editform>
                                </settingspopup>
                                <clientsideevents customdatacallback="treeList_CustomDataCallbackSuspender" selectionchanged="treeList_SelectionChangedSuspender"></clientsideevents>
                            </dx:ASPxTreeList>
						    <br />
                            <asp:Label ID="lblObservacionesPublicasRechazar" runat="server" Text="OBSERVACIONES PÚBLICAS" Font-Bold="True"></asp:Label>
                            <asp:TextBox ID="txtObservacionesPublicasRechazara" runat="server" TextMode="MultiLine" Width="98%" Height="50px" onkeypress="return soloLetras(event)" ></asp:TextBox>
					    </dx:PanelContent>
				    </PanelCollection>
	            </dx:ASPxCallbackPanel>
		    </dx:PopupControlContentControl>
        </ContentCollection>
	    <FooterTemplate>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <div style="text-align:right;">
                        <br />&nbsp;
                        <asp:Button ID="btnAplicarRechazar" CausesValidation="false" runat="server" Text="Aplicar Recahzo" class="btn btn-warning" OnClick="btnAplicarRechazar_Click"/>
                        <br />&nbsp;
		            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
	    </FooterTemplate>
    </dx:ASPxPopupControl>

    <!-- Botón de Pausa -->
    <dx:ASPxPopupControl ID="pnlPopPausaTramite" 
	        runat="server" 
	        CloseAction="CloseButton" 
	        HeaderText="Pausar Trámite" 
	        ShowFooter="True" 
	        Theme="iOS" 
	        Width="350px" 
	        ClientInstanceName="pnlPopPausaTramite" 
	        Modal="True" 
	        PopupHorizontalAlign="WindowCenter" 
	        PopupVerticalAlign="WindowCenter" 
	        FooterText="">
	    <ContentStyle>
		    <Paddings Padding="5px" />
	    </ContentStyle>
	    <ContentCollection>
		    <dx:PopupControlContentControl runat="server">
                <p>
                    <strong>OBSERVACIONES PÚBLICAS</strong>
                </p>
                <asp:TextBox ID="txtObservacionesPublicasPausar" runat="server" TextMode="MultiLine" Width="95%" Height="50px" onkeypress="return soloLetras(event)" > </asp:TextBox>
		    </dx:PopupControlContentControl>
	    </ContentCollection>
	    <FooterTemplate>
		    <div style="text-align:right;">
                <br />
                <asp:Button ID="btnPausaTramite" runat="server" Text="Pausar" CausesValidation="false" class="btn btn-warning" OnClientClick="pnlPopPausaTramite.Hide();" OnClick="btnCtrlPausarTramite_Click"/>
			    <br />&nbsp;
		    </div>
	    </FooterTemplate>
    </dx:ASPxPopupControl>
    
   <!-- Botones Emergentes Cancelar -->
    <dx:ASPxPopupControl ID="pnlPopCancelar" runat="server" CloseAction="CloseButton" HeaderText="Motivos cancelación" ShowFooter="True" Theme="Aqua"  Width="350px" ClientInstanceName="pnlPopCancelar" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" FooterText="">
        <ContentStyle>
            <Paddings Padding="5px" />
        </ContentStyle>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" >
                <dx:ASPxCallbackPanel ID="pnlCallbackCancelar" runat="server" ClientInstanceName="pnlCallbackCancelar" Width="100%" OnCallback="pnlCallbackCancelar_Callback">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxTreeList ID="treeListCancelar" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" OnCustomDataCallback="treeList_CustomDataCallbackCancelar" OnDataBound="treeList_DataBoundCancelar" ParentFieldName="IdParent" Width="100%">
                            <Columns>
                                <dx:TreeListDataColumn AutoFilterCondition="Default" Caption="Motivos de cancelación" FieldName="motivoRechazo" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="0"></dx:TreeListDataColumn>
                            </Columns>
                            <settingsbehavior allowautofilter="True" expandcollapseaction="NodeDblClick"></settingsbehavior>
                            <settingscustomizationwindow caption="" popuphorizontalalign="RightSides" popupverticalalign="BottomSides"></settingscustomizationwindow>
                            <settingsselection enabled="True"></settingsselection>
                            <settingspopupeditform verticaloffset="-1"></settingspopupeditform>
                            <settingspopup>
                                <editform verticaloffset="-1"></editform>
                            </settingspopup>
                            <clientsideevents customdatacallback="treeList_CustomDataCallbackSuspender" selectionchanged="treeList_SelectionChangedSuspender"></clientsideevents>
                            </dx:ASPxTreeList>
                            <br />
                            <asp:Label ID="Label72" runat="server" Text="OBSERVACIONES PÚBLICAS" Font-Bold="True"></asp:Label>
                            <asp:TextBox ID="txObservacionesCancelacion" runat="server" TextMode="MultiLine" Width="98%" Height="50px" onkeypress="return soloLetras(event)" ></asp:TextBox>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <FooterTemplate>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>

                <div style="text-align:right;">
                    <br />&nbsp;
                    <asp:Button ID="btnAplicarCancelar" runat="server" CausesValidation="false" Text="Aplicar Cancelación" class="btn btn-warning" OnClick="btnAplicarCancelar_Click"/>
                    <br />&nbsp;
		        </div>
                    </ContentTemplate>
            </asp:UpdatePanel>
        </FooterTemplate>
    </dx:ASPxPopupControl>
</ContentTemplate>
</asp:UpdatePanel>
        
        
</asp:Content>