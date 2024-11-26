<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="sprReporteProductividadOperacion.aspx.cs" Inherits="WFO.Procesos.Supervision.sprReporteProductividadOperacion" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
<script src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>
<link href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" rel="stylesheet" />

<script src="https://cdn.datatables.net/buttons/1.6.0/js/dataTables.buttons.min.js"></script>
<script src="https://cdn.datatables.net/buttons/1.6.0/js/buttons.flash.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
<script src="https://cdn.datatables.net/buttons/1.6.0/js/buttons.html5.min.js"></script>
<script src="https://cdn.datatables.net/buttons/1.6.0/js/buttons.print.min.js"></script>
    <script>
    function DetalleMesaGraf(IdOrden, IdFlujo) {
         var recipient = $("#<%=CalDesde.ClientID%>").val();
         var FachaIn = $("#ContenidoPrincipal_CalDesde_I").val();
         var FechaFin = $("#ContenidoPrincipal_CalHasta_I").val(); 

         $.ajax({
                type: "POST",
                url: "sprReporteProductividadOperacion.aspx/DetalleMesaGraf",
                data: '{IdOrden: ' + IdOrden + ', IdFlujo: ' + IdFlujo + ', FechaIn: "'+FachaIn+'", FechaFin: "'+FechaFin+'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: resultadoGraf,
                error: errores
         });
     }

     function resultadoGraf(data) {
         //console.log(data);
         $("#DetalleMesaGraf").modal("show");
         pintaGrafica(data);
         
        }

        function DetalleMesa(IdOrden, IdFlujo) {

         var recipient = $("#<%=CalDesde.ClientID%>").val();
         var FachaIn = $("#ContenidoPrincipal_CalDesde_I").val();
         var FechaFin = $("#ContenidoPrincipal_CalHasta_I").val(); 

         $.ajax({
                type: "POST",
                url: "sprReporteProductividadOperacion.aspx/DetalleMesa",
                data: '{IdOrden: ' + IdOrden + ', IdFlujo: ' + IdFlujo + ', FechaIn: "'+FachaIn+'", FechaFin: "'+FechaFin+'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: resultado,
                error: errores
         });

         //$("#Titulo").text('Productividad por mesa: '+ Mesa);
     }

     function resultado(data) {
        console.log(data);
         $("#DetalleMesa").modal("show");
         $('#DatosConsulta').html("");
         var tabla = "<table id='InfomacionMesas' class='table table-striped table-bordered table-hover jambo_table bulk_action' style='width:100%'>" +
                "<thead>" +
                "<tr>" +
                        "<th scope='col'>Operador</th>" +
                        "<th scope='col'>Total</th>" +
                        "<th scope='col'>Reingresos</th>" +
                        "<th scope='col'>Productividad</th>" +
                        "<th scope='col'>Calidad</th>" +
                        "<th scope='col'></th>" +
                    "</tr>" +
                "</thead>" +
                "<tbody>";
            for (var b = 0; b < data.d.consulta.length; b++) {
                var img = "";
                if (Total = 0) {
                    img = "<img src='../img/bolaGrisObscuro.png' />";
                } else {
                    var pro = (((data.d.consulta[b].Reingreso * 100) / (data.d.consulta[b].Total)) - 100) * -1
                    if (pro >= 90) {
                        img = "<img src='../../Imagenes/bolaVerde.png' /> ";
                    } else if (pro >= 80) {
                        img = "<img src='../../Imagenes/bolaAzul.png' />";
                    }else if (pro >= 60) {
                        img = "<img src='../../Imagenes/bolaAmarilla.png' />"; 
                    }else if (pro >= 50) {
                        img = "<img src='../../Imagenes/bolaNaranja.png' />";
                    }else {
                        img = "<img src='../../Imagenes/bolaRoja.png' />";
                    }
                }

                tabla += "<tr>" +
                            "<td>" + data.d.consulta[b].Nombre + "</td>" +
                            "<td>" + data.d.consulta[b].Total + "</td>" +
                            "<td>" + data.d.consulta[b].Reingreso + "</td>" +
                            "<td>" + data.d.consulta[b].Productividad + "</td>" +
                            "<td>" + data.d.consulta[b].Calidad + "</td>" +
                            "<td align='center'>" + img + "</td>" +

                        "</tr>";
            }
            tabla += "</tbody>" +
                "</table>";

            $('#DatosConsulta').html(tabla);

            $("#InfomacionMesas").dataTable().fnDestroy();
            $('#InfomacionMesas').DataTable({
                "order": [[1, "DESC"]],
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                },
                dom: "Blfrtip", 
                buttons: [{ extend: 'copy', className: 'btn-sm' }, { extend: 'csv', className: 'btn-sm' }, { extend: 'excel', className: 'btn-sm' }, { extend: 'pdfHtml5', className: 'btn-sm' }, { extend: 'print', className: 'btn-sm' }]
            });
     }

     function errores(data) {
        //msg.responseText tiene el mensaje de error enviado por el servidor
        alert('Error: ' + msg.responseText);
     }
    </script>

<div class="modal fade bd-example-modal-lg" id="DetalleMesa" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="NumeroFolio">Tramite</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                
                <div  id="DatosConsulta" class="table-responsive">

                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
            </div>
        </div>
    </div>
</div>

<div class="modal fade bd-example-modal-lg" id="DetalleMesaGraf" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalTitle">Se recomiendo visualizar la gráfica en periodos de 30  a 31 días</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body text-justify">
                <p id ="TituloGraf"></p>
                
                 <div id="pieChartContent">
                    <h2>Sin Datos </h2>
                    <canvas id="pieChart" width="300" height="1"></canvas>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" id="CloseModal" class="btn btn-secondary" data-dismiss="modal">CERRAR</button>
            </div>
        </div>
    </div>
</div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Productividad operación.</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <p class="text-muted font-13 m-b-30">
                        Tramites finalizados en mesas con estatus: PCI, Hold, Suspendido, Procesable, No Procesable, Procesado, Rechazo, Cancelado, Caducad y  EnviaMesa.
                    </p>
                    <hr />
                    <div class="row">
                        <asp:Label runat="server" ID="label1"  Font-Bold="True" Text="* Desde" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                        <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                            <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="Material" EditFormat="Custom" Width="100%" Caption="" >
                                <TimeSectionProperties>
                                    <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                </TimeSectionProperties>
                                <CalendarProperties>
                                    <FastNavProperties DisplayMode="Inline" />
                                </CalendarProperties>
                            </dx:ASPxDateEdit>
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="CalDesde" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                        </div>

                        <asp:Label runat="server" ID="labelFechaSolicitud"  Font-Bold="True" Text="* Hasta" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                        <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                            <dx:ASPxDateEdit ID="CalHasta" runat="server" Theme="Material" EditFormat="Custom" Width="100%" Caption="">
                                <TimeSectionProperties>
                                    <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                </TimeSectionProperties>
                                <CalendarProperties>
                                    <FastNavProperties DisplayMode="Inline" />
                                </CalendarProperties>
                            </dx:ASPxDateEdit>
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator4" controltovalidate="CalHasta" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                        </div>
                        <asp:Label runat="server" ID="label2"  Font-Bold="True" Text="* Flujo" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                        <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                            <dx:ASPxComboBox ID="cbFlujos" runat="server" Theme="Material" EditFormat="Custom" Width="100%">
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="cbFlujos" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1 col-sm-1 col-xs-12 text-center">
                            <asp:Button ID="Button1" runat="server"  AutoPostBack="True" Text="Consultar" Class="btn btn-success" OnClick="btnFiltroMes_Click"/>
                        </div>
                        <div class="col-md-4 col-sm-4">
                            <asp:Label runat="server" ForeColor="Red" ID="Mensaje" Text =""></asp:Label>
                        </div>
                    </div>
                    <hr />
                </div>
            </div>
        </div>
    </div>

     <div class="row">
         <asp:Literal id="MesasLiteral" runat=server  text=""/>
     </div>
<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.2/Chart.js"></script>
<script>
    function pintaGrafica(data) {

        var pieChartContent = document.getElementById('pieChartContent');
        pieChartContent.innerHTML = '&nbsp;';
        $('#pieChartContent').append('<canvas id="pieChart" width="300" height="150"><canvas>');
        ctx = $("#pieChart").get(0).getContext("2d");


        var tiempos = new Array();
        for (var b = 1; b < data.d.tiempos.length; b++) {
            tiempos.push(data.d.tiempos[b].tiempo);
        }
        //console.log(tiempos);
        var datasets = [];

        for (var a = 0; a < data.d.tablaModels.length; a++) {
            var obj = {};
            obj['label'] = data.d.tablaModels[a].label;

            var cantidades = new Array();
            for (var c = 1; c < data.d.tablaModels[a].tablaDatos.length; c++) {
                var re = /,/g;
                cantidades.push(data.d.tablaModels[a].tablaDatos[c].cantidad.replace(re, ''));
            };

            //console.log(cantidades);

            //cantidades = [10, 20, 30, 40, 50, 60];
            obj['data'] = cantidades;
            //obj['lineTension'] =  0,
            obj['fill'] = false,
            obj['borderColor'] =   getRandomColorHex(),
            obj['backgroundColor'] = 'transparent',
            //obj['borderDash'] = [5, 5],
             
            obj['pointBorderColor'] = 'rgba(0, 153, 194 ,0.5)',
                obj['pointBackgroundColor'] = 'rgba(0, 153, 194 ,0.5)',

            /*
            obj['pointRadius'] = 5,
            obj['pointHoverRadius'] = 10,
            obj['pointHitRadius'] = 30,
            obj['pointBorderWidth'] = 2,
            */
            //obj['pointStyle'] = 'rectRounded'
            //obj[' backgroundColor'] = 'transparent',
            /*
            obj['backgroundColor'] = getRandomColorHex();
            */
            datasets.push(obj);
        }
        
        var TiempoGraf = tiempos;
        console.log(tiempos);
        console.log(datasets);
        var config = {
            type: 'line',
            data: {
                labels: TiempoGraf,
                datasets: datasets
            },
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: 'Productividad'
                },
                tooltips: {
                    mode: 'index',
                    intersect: false,
                },
                hover: {
                    mode: 'nearest',
                    intersect: true
                },
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Periodos de tiempo'
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                        },
                    }]
                }
            }
        };
        var myLine = new Chart(ctx, config);
    }

    function shuffle(array) {
      var m = array.length, t, i;

      // While there remain elements to shuffle…
      while (m) {

        // Pick a remaining element…
        i = Math.floor(Math.random() * m--);

        // And swap it with the current element.
        t = array[m];
        array[m] = array[i];
        array[i] = t;
      }

      return array;
    }

    function getRandomColorHex() {
        var colours = shuffle(["rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)", "rgb(153, 102, 255)", "rgb(231,233,237)","rgb(4, 118, 127)","rgb(50, 4, 127 )","rgb(149, 9, 88 )","rgb(182, 57, 83 )"]);

        color = "";
        
        for (var n = 1; n < colours.length-1; n++) {
            color =  colours[n];
        }
       
        return color;
    }
    
</script>
</asp:Content>
