<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCobranza.aspx.cs" Inherits="WFO.Procesos.Cobranza.frmCobranza" MasterPageFile="~/Utilerias/Site.Master" ValidateRequest="false" %> 

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <script>        
        function NegritaToggle(item) {
            if (item === 1) {
                $('#tdNombre').css('font-weight', 'bold');
                $('#tdAPaterno').css('font-weight', 'normal');
                $('#tdAMaterno').css('font-weight', 'normal');
            }
            else if (item === 2) {
                $('#tdNombre').css('font-weight', 'normal');
                $('#tdAPaterno').css('font-weight', 'bold');
                $('#tdAMaterno').css('font-weight', 'normal');
            }
            else if (item === 3) {
                $('#tdNombre').css('font-weight', 'normal');
                $('#tdAPaterno').css('font-weight', 'normal');
                $('#tdAMaterno').css('font-weight', 'bold');
            }
            else {
                $('#tdNombre').css('font-weight', 'normal');
                $('#tdAPaterno').css('font-weight', 'normal');
                $('#tdAMaterno').css('font-weight', 'normal');
            }
        }

        function Confirmar() {
                pnlProcesando.Show();
            return continuar;
        }

        function HacerEncabezadoEstatico(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }

        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }

         function ConfirmarProceso(mensaje) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(mensaje)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <style>
        #mydiv {
          height: 0;
          position: absolute;
          background-color: white; /* for demonstration */
          z-index: 100;
        }
        .ajax-loader {
          position: absolute;
          left: 0;
          top: -350px;
          right: 0;
          bottom: 0;
          margin: auto; /* presto! */
        }
    </style>
    
    <fieldset>
        <legend>COBRANZA</legend>

        <div style="padding: 20px">
            <div style="width: 90%; margin: auto">
                <asp:UpdatePanel ID="upCaptura" runat="server">
                    <ContentTemplate>

                            <table align="center">
                                <tr>
                                    <td colspan="4"><asp:Label ID="lblFolio" runat="server"></asp:Label></td>
                                </tr>
                                <tr id="trNoFolioExiste" runat="server" visible="false">
                                    <td>Ingrese su no. de folio: </td>
                                    <td colspan="3"><asp:TextBox ID="txtFoliovalidar" runat="server" AutoPostBack="True" OnTextChanged="txtFoliovalidar_TextChanged"></asp:TextBox></td>
                                </tr>
                                <tr><td>Número de Póliza:</td>
                                    <td>
                                        <asp:TextBox ID="txtNoPoliza" runat="server" AutoPostBack="True" OnTextChanged="txtNoPoliza_TextChanged"></asp:TextBox>
                                    </td>
                                    <td align="right">Fecha:</td><td align="left"><asp:TextBox ID="txtFecha" runat="server" Width="70px"></asp:TextBox><asp:Image ID="ImgCalendar" runat="server" ImageUrl="~/Imagenes/Calendar.png" />
                                    <ajaxToolkit:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" BehaviorID="txtFecha_CalendarExtender" TargetControlID="txtFecha" PopupButtonID="ImgCalendar"  Format="dd/MM/yyyy" />
                                    </td>
                                    
                                </tr>
                                <tr><td>Nombre del Contratante:</td><td colspan="3"><asp:TextBox ID="txtCliente" runat="server" Width="330px"></asp:TextBox></td></tr>
                                <tr>
                                    <td>Cobertura:</td>
                                    <td colspan="2" align="center">
                                        <asp:RadioButtonList ID="rdbCobertura" runat="server" TextAlign="Left" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbCobertura_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="1" Text="Básica"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Potenciación"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr id="trTrimestre" runat="server" visible="false">
                                    <td><asp:Label ID="lblTrimestreQuincena" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlTrimestreQuincena" runat="server"></asp:DropDownList>
                                    </td>
                                    <td align="right">Año:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlAnn" runat="server">
                                            <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trAnn2" runat="server" visible="false">
                                    <td>Folio:</td>
                                    <td>
                                        <asp:TextBox ID="txtPeriodoReporte" runat="server"></asp:TextBox> 
                                    </td>
                                    <td align="right">Movimiento:</td>
                                    <td><asp:TextBox ID="txtMovimiento" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Nombre:</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNombreSolicitante" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right">Correo:</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSolicitanteCorreo" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSolicitanteCorreo" ErrorMessage="*" Font-Size="12pt"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSolicitanteCorreo" ErrorMessage="E-mail inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Width="131px"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">Asunto:</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtAsunto" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                </tr>
                                <tr id="trArchivoExcel" runat="server" visible ="false">
                                    <td valign="top">Archivo(s) adjunto(s):</td>
                                    <td valign="top" colspan="3" align="left">

                                        <asp:GridView ID="gvArchivos" runat="server" Font-Names="Tahoma" Font-Size="Small"
                                            AutoGenerateColumns="false" OnRowDataBound="gvArchivos_RowDataBound" ShowHeader="false" BorderStyle="None" BorderWidth="0">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlkNombresarchivos" runat="server" Text='<%# Eval("Archivo") %>' Target="_blank"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                            
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkQuitarArchivo" runat="server" Text="Quitar" CommandArgument='<%# Eval("Archivo") %>' OnClick="lnkQuitarArchivo_Click" ></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                            </Columns>
                                        </asp:GridView>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4"></td>
                                </tr>
                            </table>
                            
                            <div id="dvEspacioPDF" runat="server" visible="false" style="width: 100%; height: 550px; vertical-align: top" tabindex="0">
                                <asp:HiddenField ID="hfIdArchivo" runat="server" Value="9999" />
                                <asp:Literal ID="ltMuestraPdf" runat="server"></asp:Literal>
                            </div>

                            <table align="center">
                                <tr>
                                    <td colspan="4">

                                        <asp:UpdatePanel ID="upSubirExcel" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <table>

                                                    <tr id="trSubirExcelDependencia" runat="server" visible="false">
                                                        <td>Seleccionar Archivo:</td>
                                                        <td>
                                                            <ajaxToolkit:AsyncFileUpload id="AsyncFileUpload1" runat="server" PersistFile="true" ClientIDMode="AutoID" UploaderStyle="Modern"/>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnSubirExcel" runat="server" Text="Subir" CssClass="boton" ClientIDMode="Static" OnClientClick="return Confirmar();" OnClick="btnSubirExcel_Click" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trSubirExcelParaValidar" runat="server" visible="false">
                                                        <td>Archivo Excel para Validar:</td>
                                                        <td>
                                                            <ajaxToolkit:AsyncFileUpload id="AsyncFileUpload2" runat="server" PersistFile="true" ClientIDMode="AutoID" UploaderStyle="Modern"/>
                                                        </td>
                                                        <td align="left">
                                                            <table>
                                                                <tr>
                                                                    <td>

                                                                        <asp:Button ID="BtnSubirExcelParaValidar" runat="server" Text="Subir" CssClass="boton" ClientIDMode="Static" OnClientClick="return Confirmar();" OnClick="BtnSubirExcelParaValidar_Click" />

                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                            </table>
                                                           
                                                        </td>
                                                    </tr>
                                                </table>

                                                <span id="Span1" runat="server" style="color:Red; font-size:X-large; font-style:italic; font-weight:bold;"></span>
                                                                                        
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnSubirExcel" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnSubirExcelParaValidar" />
                                                <asp:AsyncPostBackTrigger ControlID="gvArchivos" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                            
                                        
                                                
                                    </td>
                                </tr>
                            </table>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:UpdatePanel ID="upBotones" runat="server" UpdateMode="Always">
            <ContentTemplate>

                <div id="dvBotones" style="text-align: right">   
                    <!-- Basica -->
                    <asp:Button ID="BtnContinuar"   runat="server" Text="Solicitar Recibo Fiscal"           CssClass="boton" OnClick="BtnContinuar_Click"   OnClientClick="return Confirmar();" Visible="false" />&nbsp;&nbsp;
                    <asp:Button ID="BtnTerminar"    runat="server" Text="Enviar a Dependencia"              CssClass="boton" OnClick="BtnTerminar_Click"    OnClientClick="return Confirmar();" Visible="false" />
                    
                    <!-- potenciacion --> 
                    <asp:Button ID="BtnValidacion"  runat="server" Text="Generar Archivo 100 Posiciones"    CssClass="boton" OnClick="BtnValidacion_Click"  OnClientClick="ConfirmarProceso('Hay muchos errores para la generación de este archivo ¿Desea generar de todas formas?'); return Confirmar();" Visible="false" />&nbsp;&nbsp;
                    <asp:Button ID="BtnAceptar01"   runat="server" Text="Aceptar"                           CssClass="boton" OnClick="BtnAceptar01_Click"   Visible="false" />
                    <asp:Button ID="BtnAceptar02"   runat="server" Text="Aceptar"                           CssClass="boton" OnClick="BtnAceptar02_Click"   Visible="false" />
                    <asp:Button ID="BtnCancelar02"  runat="server" Text="Cancelar"                          CssClass="boton" OnClick="BtnCancelar02_Click"  Visible="false" />
                    <asp:Button ID="BtnTerminar02"  runat="server" Text="Enviar a Dependencia"              CssClass="boton" OnClick="BtnTerminar02_Click"  Visible="false" />
                    
                    <!-- Cancela porceso basica o potenciacion-->
                    <asp:Button ID="BtnCancelar"    runat="server" Text="Suspender"                         CssClass="boton" OnClick="BtnCancelar_Click"    OnClientClick="ConfirmarProceso('¿Desea eliminar los datos? Se borrarán permamentemente'); return Confirmar();" Visible="false" />

                </div>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnValidacion" />
                <asp:PostBackTrigger ControlID="BtnAceptar01" />
                <asp:PostBackTrigger ControlID="BtnAceptar02" />
                <asp:PostBackTrigger ControlID="BtnCancelar02" />
                <asp:PostBackTrigger ControlID="BtnTerminar02" />
            </Triggers>
        </asp:UpdatePanel>

    </fieldset>

    <br />

    <div id="DivRoot" style="margin:auto">

            <div style="overflow: hidden;" id="DivHeaderRow"></div>

            <div style="overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">

            <asp:UpdatePanel ID="upGVAgregado" runat="server">
                <ContentTemplate>

                    <asp:GridView ID="gvBasica" runat="server"></asp:GridView>

                    <asp:GridView ID="gvPotenciacion" runat="server"></asp:GridView>
                        
                </ContentTemplate>
            </asp:UpdatePanel>

            </div>

            <div id="DivFooterRow" style="overflow:hidden"></div>

    </div>





</asp:Content>