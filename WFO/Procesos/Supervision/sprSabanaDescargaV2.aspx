<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sprSabanaDescargaV2.aspx.cs" Inherits="WFO.Procesos.Supervision.sprSabanaDescargaV2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../CSS/vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../CSS/vendors/pnotify/dist/pnotify.css" rel="stylesheet" />
    <link href="../../CSS/vendors/pnotify/dist/pnotify.buttons.css" rel="stylesheet" />
    <link href="../../CSS/vendors/build/css/custom.css" rel="stylesheet" />
    <link href="../../CSS/cssOperador.css" rel="stylesheet" />
</head>
<body>
 <form id="form1" runat="server">
    
    <nav class="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
  <a class="navbar-brand" href="#">DESCARGA</a>

  <div class="collapse navbar-collapse" id="navbarCollapse">
    
  </div>
</nav>

<main role="main" class="container">
    <div class="jumbotron">
        <h1 style="color:#2C2C2C">Reporte sabana</h1>
        <asp:Panel ID="InformacionFin" runat="server" Visible="false">
             <p>Descarga Finalizada.</p>
        </asp:Panel>
        <asp:Panel ID="Informacion" runat="server">
            <p style="color:#515151">Tu reporte está siendo procesado, no cierras la ventana de tu navegador hasta aparecer tu descarga.</p>
            <div class="row">
                <div class="col-md-4 col-sm-4">
                    <h4 style="color:#2C2C2C">
                        <strong>Fecha Inicio: </strong>
                        <asp:Label runat="server" ID="LabelFechaInicio" Text="" Font-Bold="True" ></asp:Label>
                    </h4>
                </div>
                <div class="col-md-4 col-sm-4">
                    <h4 style="color:#2C2C2C">
                        <strong>Fecha Fin:</strong>
                        <asp:Label runat="server" ID="LabelFechaFin" Text="" Font-Bold="True" ></asp:Label>
                    </h4>
                </div>
        </div> 
        </asp:Panel>
        <div style="visibility:collapse">
            <asp:Button ID="BtnContinuar" runat="server"  AutoPostBack="True" Text="Iniciar Descarga" Class="btn btn-success" OnClick="BtnDescargar_Click"/>
        </div>
    </div>
</main>
     

     <script src="../../JS/jquery-1.12.4.min.js"></script>
    <script>
        $(document).ready(function () {
            setTimeout(function () {
                document.getElementById("<%= BtnContinuar.ClientID %>").click();
                //alert(1);
            },1000); // el tiempo a que pasara antes de ejecutar el cod
        });
    </script>
     </form>
</body>
</html>