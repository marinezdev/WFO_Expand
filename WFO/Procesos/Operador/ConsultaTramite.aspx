<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaTramite.aspx.cs" Inherits="WFO.Procesos.Operador.ConsultaTramite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <!-- Campos Ocultos -->
    <div>
        <asp:HiddenField ID="hfIdTramite" runat="server" Value="0" />
        <asp:HiddenField ID="hfIdTipoTramite" runat="server" Value="0" />
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
                                    <table id="datatable" class="table table-striped table-bordered  jambo_table bulk_action">
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

    <!-- MODAL DE ARCHIVOS EXPEDIENTE  -->
    <div class="modal fade Expediente" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
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
                                        <td><asp:ImageButton ID="imbtnExpedienteFlot" runat="server" ImageUrl="../../Imagenes/folder-abrir.jpg" CommandName ="Consultar" CommandArgument='<%#Eval("Id")%>' OnCommand="CargaExpedienteID" /></td>
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>

    <!-- DATOS MOSTRADOS EN TODAS LAS MESES --> 
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 text-left">
            <div class="x_panel">
                <div class="x_title">
                    <h2><small> Información del trámite  </small> </h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li>
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
                            <div class="row">
                                <table class="table table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <th colspan="2" style="text-align:center; border-bottom: 1px solid #ddd; background-color:#8EBB53; color:black;">Folio: <asp:Label runat="server" ID="LabelFolio" Text="Folio" Font-Bold="False" class="control-label"></asp:Label></th>
                                        </tr>
                                        <tr>
                                            <th colspan="2" style="text-align:center; border-bottom: 1px solid #ddd; background-color:#EADB20; color:black;">
                                                <asp:Label runat="server" ID="LabelNumeroPoliza" Text="-" Font-Bold="false" class="control-label"></asp:Label>
                                                <asp:Label runat="server" ID="LabelNumeroPoliza2" Text="-" Font-Bold="true" class="control-label"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr style="display:none;">
                                            <th style="background-color:#1572B7; color:white; text-align:center;">Producto </th>
                                            <th style="background-color:#1572B7; color:white; text-align:center;">Sub Producto </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="text-center"><asp:Label runat="server" ID="LabelProducto" Text="Producto" Font-Bold="False" class="control-label text-center"></asp:Label></td>
                                            <td class="text-center"><asp:Label runat="server" ID="LabelSubProducto" Text="Sub Producto" Font-Bold="False" class="control-label text-center"></asp:Label></td>
                                        </tr>
                                     </tbody>
                                </table>
                            </div>
                            <br /><br />
                            <div class="row">
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
                    <div class="clearfix"></div>
                </div>
                <div class="x_content text-left" >

                </div>
                    <div class="row">
                        <div class="control-label col-md-5 col-sm-5 col-xs-12">
                            <button type="button" class="btn btn-default col-md-2 col-sm-2 col-xs-12" data-toggle="modal" title="Bitácora publica" data-target=".BitacoraPublica"><i class="fa fa-unlock-alt"></i></button>
                            <button type="button" class="btn btn-default col-md-2 col-sm-2 col-xs-12" data-toggle="modal" title="Bitácora privada" data-target=".BitacoraPrivada"><i class="fa fa-lock"></i> </button>
                            <asp:Panel ID="panelAccionesOcultas" runat="server" Visible="false">
                                <asp:LinkButton ID="LinkButtonAbrirExpediente" runat="server" class="btn btn-default col-md-2 col-sm-2 col-xs-12"  title="Abrir documento" OnClick="BtnExpedienteAbrir_click" Text="" ><i class="fa fa-desktop"></i></asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonOcultarExpediente" runat="server" class="btn btn-default col-md-2 col-sm-2 col-xs-12" title="Ocultar documento" OnClick="BtnExpedienteOcultar_click" Text=""><i class="fa fa-ban"></i></asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonMostrarExpediente" runat="server" class="btn btn-default col-md-2 col-sm-2 col-xs-12" title="Mostrar documento" OnClick="BtnExpedienteMostrar_click" Text="" Visible="false"><i class="fa fa-file-text"></i></asp:LinkButton>
                                <button type="button" class="btn btn-default col-md-2 col-sm-2 col-xs-12" data-toggle="modal" title="Expedientes" data-target=".Expediente"><i class="fa fa-archive"></i> </button>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    <!-- EXPEDIENTE PDF -->
    <asp:Panel ID="Expediente" runat="server" Visible="true">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><small>Expediente</small> </h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content text-left" style="height: 550px;">
                        <asp:Literal ID="ltMuestraPdf" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
