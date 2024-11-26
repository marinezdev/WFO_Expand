<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WFO.Procesos.Cobranza.Default" MasterPageFile="~/Utilerias/Site.Master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick" Enabled="False"></asp:Timer>
            <div id="dvEjecutados" style="width: 100%;">
                <fieldset>
                    <legend>INDICADOR GENERAL</legend>
                    <table id="tblGraficaUno" style="width: 100%;">
                        <tr>
                            <td style="text-align:center">
                                <h3>
                                    <asp:Literal ID="ltTituloPromotoria" runat="server"></asp:Literal>
                                </h3>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                                <div id="dvGrafUno" style="width: 700px; margin: auto;">

                                    <asp:Chart ID="grfGrupoUno" runat="server" Width="700px" Height="300px" BackColor="211, 223, 240" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="#1A3B69" BorderlineDashStyle="Solid" BorderWidth="2px" OnClick="grfGrupoUno_Click" >
                                        <ChartAreas>
                                            <asp:ChartArea Name="GrupoUno" BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" ShadowColor="Transparent">
                                                <%--<Area3DStyle Enable3D="True" IsRightAngleAxes="False" LightStyle="Realistic" Inclination="20" Rotation="15"/>--%>
                                                <AxisY LineColor="64, 64, 64, 64" IsMarginVisible="false">
                                                    <LabelStyle Font="Trebuchet MS, 8pt" />
                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                </AxisY>
                                                <AxisX LineColor="64, 64, 64, 64" IsMarginVisible="false">
                                                    <LabelStyle Font="Trebuchet MS, 8pt" />
                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                    <asp:Literal ID="ltTemp" runat="server"></asp:Literal><br />
                                    <asp:RadioButtonList ID="RblCobertura" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RblCobertura_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Text="Básica"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Potenciada" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>

                                </div>

                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>