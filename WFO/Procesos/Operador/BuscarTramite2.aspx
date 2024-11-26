<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuscarTramite2.aspx.cs" Inherits="WFO.Procesos.Operador.BuscarTramite2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<%@ Register Src="~/Utilerias/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="icon" href="../Imagenes/logo.ico" type="image/ico" />
    <title>Work Flow</title>
    <!-- Bootstrap -->
    <link href="../../CSS/vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link href="../../CSS/vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <!-- PNotify -->
    <link href="../../CSS/vendors/pnotify/dist/pnotify.css" rel="stylesheet" />
    <link href="../../CSS/vendors/pnotify/dist/pnotify.buttons.css" rel="stylesheet" />
    <!-- Custom Theme Style -->
    <link href="../../CSS/vendors/build/css/custom.css" rel="stylesheet" />
    <!-- Operador -->
    <link href="../../CSS/cssOperador.css" rel="stylesheet" />
    <style>
        @media (max-width: 991px) {
            .text-xs-right {
                text-align: right;
            }
        }
    </style>
    <style>
        body {
            color: #0163a2;
            background: #f7f7f7;
            font-family: "Helvetica Neue", Roboto, Arial, "Droid Sans", sans-serif;
            font-size: 13px;
            font-weight: 400;
            line-height: 1.471;
        }
        .nav.side-menu>li>a, .nav.child_menu>li>a {
            color: #fefefe;
            font-weight: 500;
        }

        .nav_title {
            width: 230px;
            float: left;
            background: #727f8d;
            border-radius: 0;
            height: 57px;
            background: linear-gradient(-45deg,#29D3C6,#0DAEDF)!important;
        }
        .nav li.current-page {
            background: linear-gradient(-45deg,#29D3C6,#0DAEDF)!important;
            box-shadow: 4px 4px 10px 0 rgba(0,0,0,.1),4px 4px 15px -5px rgba(21,114,232,.4)!important;
        }
        .profile_info span {
            font-size: 13px;
            line-height: 20px;
            color: #ffffff;
        }
        .nav-md ul.nav.child_menu li:before {
            background: #ffffff;
            bottom: auto;
            content: "";
            height: 8px;
            left: 23px;
            margin-top: 15px;
            position: absolute;
            right: auto;
            width: 8px;
            z-index: 1;
            border-radius: 50%;
        }
        .nav-md ul.nav.child_menu li:after {
            border-left: 1px solid #ffffff;
            bottom: 0;
            content: "";
            left: 27px;
            position: absolute;
            top: 0;
        }

        .left_col {
            background: #4a6778;
            box-shadow: 4px 4px 10px 0 rgba(0,0,0,.1),4px 4px 15px -5px rgba(21,114,232,.4)!important;
        }

        .nav_menu {
            float: left;
            background: #ffffff;
            border-bottom: 1px solid #eee;
            margin-bottom: 10px;
            width: 100%;
            position: relative;
            box-shadow: 4px 4px 10px 0 rgba(0,0,0,.1),4px 4px 15px -5px rgba(21,114,232,.4)!important;
        }

        .x_panel {
            position: relative;
            width: 100%;
            margin-bottom: 10px;
            padding: 10px 17px;
            display: inline-block;
            background: #fff;
            border: 1px solid #E6E9ED;
            -webkit-column-break-inside: avoid;
            -moz-column-break-inside: avoid;
            column-break-inside: avoid;
            opacity: 1;
            transition: all .2s ease;
            box-shadow: 4px 4px 10px 0 rgba(0,0,0,.1),4px 4px 15px -5px rgba(21,114,232,.4)!important;
        }

        .nav.side-menu>li.current-page, .nav.side-menu>li.active {
            border-right: 5px solid #eee;
        }
        .nav.side-menu>li.active>a {
            text-shadow: rgb(0 0 0 / 25%) 0 -1px 0;
            background: linear-gradient(-45deg,#29D3C6,#0DAEDF)!important;
            box-shadow: rgb(0 0 0 / 25%) 0 1px 0, inset rgb(255 255 255 / 16%) 0 1px 0;
        }
        .nav.side-menu>li>a:hover, .nav>li>a:focus {
            text-decoration: none;
            background: linear-gradient(-45deg,#29D3C6,#0DAEDF)!important;
            color:#eee;
            background: #0163a2;
        }

        .menu_section h3 {
            padding-left: 41px;
            color: #fff;
            text-transform: uppercase;
            letter-spacing: .5px;
            font-weight: bold;
            font-size: 11px;
            margin-bottom: 0;
            margin-top: 0;
            text-shadow: 0px 1px #000;
        }

        .Card_1{
            background: linear-gradient(-45deg,#29D3C6,#0DAEDF)!important;
            box-shadow: rgb(0 0 0 / 25%) 0 1px 0, inset rgb(255 255 255 / 16%) 0 1px 0;
            color:#eee;
        }

        .Card_2{
            background: linear-gradient(-45deg,#5E6B71,#8FA0A9)!important;
            box-shadow: rgb(0 0 0 / 25%) 0 1px 0, inset rgb(255 255 255 / 16%) 0 1px 0;
        }

        table.jambo_table thead {
           background: linear-gradient(-45deg,#29D3C6,#0DAEDF)!important;
            color: #ECF0F1;
        }
        .btn-info{
            background: linear-gradient(-45deg,#29D3C6,#0DAEDF)!important;
        }
        .btn-info:hover {
            background: linear-gradient(-45deg,#1f9f95,#0d91b9)!important;
            box-shadow: 4px 4px 10px 0 rgba(0,0,0,.1),4px 4px 15px -5px rgba(21,114,232,.4)!important;
        }
        .btn-danger{
                background: linear-gradient(-45deg,#d32929,#b700a0)!important;
        }

        .btn-danger:hover {
               background: linear-gradient(-45deg,#cb005e,#7e026f)!important;
                box-shadow: 4px 4px 10px 0 rgba(0,0,0,.1),4px 4px 15px -5px rgba(21,114,232,.4)!important;
        }

        .btn-default {
            background: linear-gradient(-45deg,#f7f7f7,#ccc)!important;
        }

        .btn-default:hover {
            background: linear-gradient(-45deg,#d3d2d2,#9f9e9e)!important;
            box-shadow: 4px 4px 10px 0 rgba(0,0,0,.1),4px 4px 15px -5px rgba(21,114,232,.4)!important;
        }

        .btn-success {
            background: linear-gradient(-45deg,#77a809,#3c763d)!important;
            box-shadow: 4px 4px 10px 0 rgba(0,0,0,.1),4px 4px 15px -5px rgba(21,114,232,.4)!important;
            border: 1px solid #77a809;
        }
        .btn-success:hover {
            background: linear-gradient(-45deg,#537800,#205421)!important;
            box-shadow: 4px 4px 10px 0 rgba(0,0,0,.1),4px 4px 15px -5px rgba(21,114,232,.4)!important;
        }
        table{
   width:100%;
   table-layout: fixed;
   overflow-wrap: break-word;
}

        /*table table-striped jambo_table bulk_action*/
    </style>
    <script>     
        function ocultarBloqueo() {
            document.getElementById("divPantallaBloqueo").style.display = "none";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnDisableEnter">
        <asp:Button ID="btnDisableEnter" runat="server" Text="" OnClientClick="return false;" Style="display: none;" />
        <div class="container body">
            <div class="main_container">
                
                <!-- page content -->
                <div class="right_col" role="main">

                    <!-- HERRAMIENTAS DE MODAL EXIT CARGA -->
                        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true" EnablePageMethods="true">
                        </asp:ScriptManager>

                    <div id="dvCajaAreaDeTrabajo">
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
                                            <h2>Buscar Trámites </h2>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content">
                                            <p class="text-muted font-13 m-b-30">
                                                <hr />
                                                Consulta por filtro.
                                            </p>
                                            <div class="row">
                                                <asp:Label runat="server" ID="Label2" Text="Folio: " Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                                <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                                    <asp:TextBox ID="TextFolio" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
								                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                </div>
                                                <asp:Label runat="server" ID="Label3" Text="RFC: " Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                                <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                                    <asp:TextBox ID="TextRFC" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
								                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                </div>
                                            </div>
                                            <p class="text-muted font-13 m-b-30">
                                                Contratante
                                            </p>
                                            <div class="row">
                                                <asp:Label runat="server" ID="lblNombre" Text="Nombre(s)" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                                <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                                    <asp:TextBox ID="txNombre" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
								                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                </div>
                                                <asp:Label runat="server" ID="lblAPaterno" Text="Apellido Paterno" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                                <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                                    <asp:TextBox ID="txApPat" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApPat" runat="server" FilterMode="ValidChars" TargetControlID="txApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                </div>
                                                <asp:Label runat="server" ID="lblAMaterno" Text="Apellido Materno" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                                <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                                    <asp:TextBox ID="txApMat" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
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
                                                <asp:Literal id="ListBusqueda" runat=server  text=""/>
                                                <hr />
                                            </div>
                                            <div class="row" id="DatosConsultaTramite">
                                                
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <!-- /page content -->


                <!-- footer content -->
                <footer>
                    <div class="pull-right">
                        Version: 1.1.0 - 2022 <a href="https://www.asae.com.mx/">Asae Consultores S.A de C.V.</a>
                    </div>
                    <div class="clearfix"></div>
                </footer>
                <!-- /footer content -->
            </div>
        </div>
    </form>

    <!-- jQuery -->
    <script src="../../JS/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap -->
    <script src="../../CSS/vendors/bootstrap/dist/js/bootstrap.min.js"></script>

    <script src="../../JS/nprogress/nprogress.js"></script>
    
    <!-- iCheck --
    <script src="vendors/iCheck/icheck.min.js"></script>
    
    <!-- PNotify -->
    <script src="../../CSS/vendors/pnotify/dist/pnotify.js"></script>
    <script src="../../CSS/vendors/pnotify/dist/pnotify.buttons.js"></script>

    
    <!-- Skycons -->
    <script src="../../JS/skycons/skycons.js"></script>
    
    <!-- Custom Theme Scripts -->
    <script src="../../CSS/vendors/build/js/custom.min.js"></script>
    <script src="../../JS/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="../../JS/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
    <script src="../../JS/datatables.net-buttons/js/dataTables.buttons.min.js"></script>
    <script src="../../JS/datatables.net-buttons-bs/js/buttons.bootstrap.min.js"></script>
    <script src="../../JS/datatables.net-buttons/js/buttons.flash.min.js"></script>
    <script src="../../JS/datatables.net-buttons/js/buttons.html5.min.js"></script>
    <script src="../../JS/datatables.net-buttons/js/buttons.print.min.js"></script>
    <script src="../../JS/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js"></script>
    <script src="../../JS/datatables.net-keytable/js/dataTables.keyTable.min.js"></script>
    <script src="../../JS/datatables.net-responsive/js/dataTables.responsive.min.js"></script>
    <script src="../../JS/datatables.net-responsive-bs/js/responsive.bootstrap.js"></script>
    <script src="../../JS/datatables.net-scroller/js/dataTables.scroller.min.js"></script>
    <script src="../../JS/sweetalert/sweetalert.min.js"></script>
    <script type="text/javascript">
        $(document).ready(Inicio);

        function Inicio() {
            $("#Ancho").val($("#contenedor").width() - 30)
            $("#btnFiltrar").click();
            $(window).resize(function () {
                $("#Ancho").val($("#contenedor").width() - 30)
                $("#btnFiltrar").click()
            });
        }

        function Consultar(num) {
            $('#DatosConsultaTramite').html("");

            $.ajax({
                type: "POST",
                url: "BuscarTramite2.aspx/DetalleIdTramite",
                data: '{ Id: ' + num + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: resultado,
                error: errores
            });
            }

        function resultado(data) {
            $('#DatosConsultaTramite').html("");
            var contenido = "";
            contenido +=
                "<div class='x_title'>" +
                    "<h2>Información del trámite <strong>" + data.d.FolioCompuesto + "</strong></h2>" +
                    "<div class='clearfix'></div>" +
                "</div>" +
                "<div class='col-md-5 col-sm-5 col-xs-5'>" +
                    "<table>" +
                        "<tr>" +
                            "<td style='background: linear-gradient(-45deg,#29D3C6,#0DAEDF)!important;color: #ECF0F1; width:110px'>Folio</td>" +
                            "<td>" + data.d.FolioCompuesto + "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style='background: linear-gradient(-45deg,#29D3C6,#0DAEDF)!important;color: #ECF0F1;'>Fecha de registro</td>" +
                            "<td>" + data.d.Fecha + "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style='background: linear-gradient(-45deg,#29D3C6,#0DAEDF)!important;color: #ECF0F1;'>Trámite</td>" +
                            "<td> " + data.d.Operacion  + "</td>" +
                        "</tr>" +
                    "</table>" +
                "</div>" +

                "<div class='col-md-7 col-sm-7 col-xs-12'>" +
                    "<div class='row'>" +
                        "<button type='button' class='btn btn-default col-md-5 col-sm-5 col-xs-12' onclick='ConsultaBitacora(" + data.d.IdTramite + "," + data.d.IdTipoTramite+ ");'><i class='fa fa-unlock-alt'></i> Bitácora</button>" +
                        "<button type='button' class='btn btn-default col-md-5 col-sm-5 col-xs-12' onclick='ConsultaExpediente(" + data.d.IdTramite + "," + data.d.IdTipoTramite + ");'><i class='fa fa-file-text'></i> Expediente</button>" +
                    "</div>" +
                    "<br>" +
                    "<div class='row' id='ContenidoBitacora'>" +
                    "</div>" +
                "</div>" +

                "<div class='col-md-12 col-sm-12 col-xs-12'>" +
                    "<div class='row' id='ContenidoExpediente'>" +
                    "</div>" +
                "</div>" +

                "<hr />";
            $('#DatosConsultaTramite').html(contenido);
        }

        function ConsultaBitacora(Id, IdTipo) {
            $.ajax({
                type: "POST",
                url: "BuscarTramite2.aspx/DetalleBitacora",
                data: '{ Id: ' + Id + ', IdTipo: ' + IdTipo +'}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: resulBitacora,
                error: errores
            });
        }

        function resulBitacora(data) {
                console.log(data);

            var contenido = "";
                contenido += "<div class='table-responsive'>" +
                    "<table  id='example2' class='table table-striped table-bordered jambo_table bulk_action' style='width:100%'>" +
                        "<thead>" +
                            "<tr>" +
                            "<th>Información</th>" +
                            "</tr>" +
                        "</thead>" +
                            "<tbody>";

                    for (var i = 0; i < data.d.length; i++) {
                        contenido +=
                            "<tr>" +
                                "<td>" +
                                    "<strong>" + 
                                        data.d[i].Mesa +
                                    "</strong> <br>" +
                                        data.d[i].FechaInicio2 + "<br>" +
                                        data.d[i].FechaTermino2 + "<br>" +
                                        data.d[i].EstatusMesa + "<br><br>" +
                                        "<i>Observaciones publicas </i><br>" +
                                        data.d[i].Observacion + 
                                        "<br><br>" +
                                        "<i>Observaciones privadas  </i><br>" +
                                        data.d[i].ObservacionPrivada +
                                "</td>" +
                            "</tr>";
                    }

            contenido +=  "</tbody>" +
                        "</table>";
                    "</div>";

            $('#ContenidoBitacora').html(contenido);

            $('#example2').DataTable({ 'language': { 'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json' }, scrollY: '300px', scrollX: true, scrollCollapse: true, fixedColumns: true, dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm' }, { extend: 'csv', className: 'btn-sm' }, { extend: 'excel', className: 'btn-sm' }, { extend: 'pdfHtml5', className: 'btn-sm' }, { extend: 'print', className: 'btn-sm' }] });

        }

        function ConsultaExpediente(Id, IdTipo) {
            $('#ContenidoExpediente').html("");

            $.ajax({
                type: "POST",
                url: "BuscarTramite2.aspx/DetalleExpediente",
                data: '{ Id: ' + Id + ', IdTipo: ' + IdTipo + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: resulExpediente,
                error: errores
            });
        }

        function resulExpediente(data) {
            var iframe = document.createElement("iframe");
            iframe.width = '100%';
            iframe.height = '700px';
            iframe.src = data.d; //Aqui iría el src de tu archivo .PDF
            $('#ContenidoExpediente').append(iframe);
        }

        function errores(data) {
            alert('Error: ');
        }

    </script>
    <script src="../JS/loader.js"></script>
</body>
</html>