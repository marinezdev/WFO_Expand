<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sprReporteTramitesAnuales.aspx.cs" Inherits="WFO.Procesos.Supervision.sprReporteTramitesAnuales" MasterPageFile="~/Utilerias/Site.Master"%>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <div class="text-center text-warning">REPORTE DE TRAMITES ANUALES</div>
    <br />
    <br />
    <div class="container-fluid">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <br /><br />
                    <dx:ASPxGridView ID="dvgdEstatusTramite" ClientInstanceName="dvgdEstatusTramite" runat="server" 
                    AutoGenerateColumns ="True" 
                    style="margin-top: 0px" 
                    EnableTheming="false" 
                    Font-Size ="10px" 
                    Width="100%" SettingsDetail-ExportMode="All" SettingsExport-ExcelExportMode="Default">           
                    <Settings HorizontalScrollBarMode="Hidden" VerticalScrollBarMode="Hidden" ShowFooter="false" ShowGroupFooter="Hidden" />
                    <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                    <SettingsPager  Mode="ShowAllRecords"/>
                    <SettingsSearchPanel Visible="false" />
                    <Styles Header-Wrap="false" Cell-Wrap="true" />
                    </dx:ASPxGridView>
                    <br />

                    <asp:Chart ID="Chart1" runat="server" BackGradientStyle="LeftRight" Height="350px" Palette="None" Width="1100px">   
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="true" Name="Default" LegendStyle="Row" AutoFitMinFontSize="5"></asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Name="Titulo1" Text="Gráfico de Trámites por Año"></asp:Title>
                        </Titles>
                        <Series>  
                            <asp:Series Name="Series1" LegendText="2018" Color="Red" BorderWidth="5" ChartArea="ChartArea1" IsValueShownAsLabel="true" MarkerSize="2" Font="Arial"></asp:Series>  
                            <asp:Series Name="Series2" LegendText="2019" Color="Blue" BorderWidth="5" ChartArea="ChartArea1" IsValueShownAsLabel="true"  MarkerSize="2" Font="Arial"></asp:Series>  
                        </Series>  
                        <ChartAreas>  
                            <asp:ChartArea Name="ChartArea1">
                                <AxisX Title="Meses"></AxisX>
                                <AxisY Title="Trámites"></AxisY>
                            </asp:ChartArea>  
                            <%--<asp:ChartArea Name="ChartArea2"></asp:ChartArea> --%> 
                        </ChartAreas>
                    
                        <BorderSkin BackColor=""  />  
                    </asp:Chart>  



                </div>
            </div>
        </div>
    </div>

</asp:Content>