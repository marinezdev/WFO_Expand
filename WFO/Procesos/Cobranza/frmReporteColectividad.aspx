<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmReporteColectividad.aspx.cs" Inherits="WFO.Procesos.Cobranza.frmReporteColectividad" MasterPageFile="~/Utilerias/Site.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <fieldset>
        <legend>REPORTE DE COLECTIVIDAD</legend>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="BtnGenerarExcel" runat="server" Text="Generar Reporte" CssClass="boton" ClientIDMode="Static" OnClientClick="return Confirmar();" OnClick="BtnGenerarExcel_Click" />&nbsp;
        <asp:Button ID="BtnExportar" runat="server" Text="Exportar a Excel" CssClass="boton" ClientIDMode="Static" OnClick="BtnExportar_Click"  Visible="false" />
        <br /><br />    
        <div id="DivRoot" style="margin:auto">

                    <div style="overflow: hidden;" id="DivHeaderRow"></div>

                    <div style="overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">

                        <asp:UpdatePanel ID="upGVAgregado" runat="server">
                            <ContentTemplate>

                        <asp:GridView ID="gvAgregado" runat="server"
                            AutoGenerateColumns="False" 
                            BackColor="White" 
                            BorderColor="Yellow" 
                            BorderStyle="None" 
                            BorderWidth="0" GridLines="Both" 
                            HeaderStyle-Font-Bold="true"
                            HeaderStyle-Font-Size="XX-Small"
                            RowStyle-Font-Size="Small"
                            CellPadding="4" 
                            CellSpacing="1"  
                            RowStyle-Wrap="false" 
                            HeaderStyle-Wrap="true"  
                            ShowFooter="true" PageSize="25" AllowPaging="true" 
                            Width="100%" OnRowCreated="gvAgregado_RowCreated" OnPageIndexChanging="gvAgregado_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Poliza" HeaderText="Poliza" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="Dependencia" HeaderText="Dependencia" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="Certificado" HeaderText="No. Certificado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="APaterno" HeaderText="Apellido Paterno" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="AMaterno" HeaderText="Apellido Materno" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="Nombres" HeaderText="Nombre(s)" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="FNacimiento" HeaderText="Fecha de Nacimiento" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="RFC" HeaderText="RFC" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="CURP" HeaderText="CURP" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="Sexo" HeaderText="Sexo" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="CEntidadFederativa" HeaderText="Código Entidad Federativa" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="CMunicipio" HeaderText="Código Municipio" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="NivelTabular" HeaderText="Nivel Tabular" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="MPercepcionOBM" HeaderText="Monto Percepción Ordinaria Bruta" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="Eventual" HeaderText="Eventual" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="APAsegurado" HeaderText="Apellido Paterno Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="AMAsegurado" HeaderText="Apellido Materno Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="NAsegurado" HeaderText="Nombre(s) Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="FNAsegurado" HeaderText="Fecha Nacimiento Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="CURPAsegurado" HeaderText="CURP Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="SAsegurado" HeaderText="Sexo Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="FAAsegurado" HeaderText="Fecha Afliación Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="TAsegurado" HeaderText="Tipo Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="FIColectividad" HeaderText="Fecha Ingreso Colectividad" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="Estatus" HeaderText="Estatus" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="FechaBaja" HeaderText="Fecha de Baja" HeaderStyle-BackColor="Aqua" />

                                <asp:BoundField DataField="SAB1PV" HeaderText="Suma Asegurada Básica del 16 de noviembre al 31 de diciembre de 2017" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAP1PV" HeaderText="Suma Asegurada Potenciada del 16 de noviembre al 31 de diciembre de 2017" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAT1PV" HeaderText="Suma Asegurada Total del 16 de noviembre al 31 de diciembre de 2017" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAB1T" HeaderText="Suma Asegurada Básica del 1° de enero al 31 de marzo del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAP6Q" HeaderText="Suma Asegurada Potenciada del 1° de enero al 31 de marzo del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAT1T" HeaderText="Suma Asegurada Total del 1° de enero al 31 de marzo del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAB2T" HeaderText="Suma Asegurada Básica del 1° de abril al 30 de junio del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAP12Q" HeaderText="Suma Asegurada Potenciada del 1° de abril al 30 de junio del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAT2T" HeaderText="Suma Asegurada Total del 1° de abril al 30 de junio del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAB3T" HeaderText="Suma Asegurada Básica del 1° de julio al 30 de septiembre del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAP18Q" HeaderText="Suma Asegurada Potenciada del 1° de julio al 30 de septiembre del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAT3T" HeaderText="Suma Asegurada Total del 1° de julio al 30 de septiembre del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAB4T" HeaderText="Suma Asegurada Básica del 1° de octubre al 31 de diciembre del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAP24Q" HeaderText="Suma Asegurada Potenciada del 1° de octubre al 31 de diciembre del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAT3T" HeaderText="Suma Asegurada Total del 1° de octubre al 31 de diciembre del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAB1T19" HeaderText="Suma Asegurada Básica del 1° de enero al 31 de marzo del 2019" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAP6Q19" HeaderText="Suma Asegurada Potenciada del 1° de enero al 31 de marzo del 2019" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAT1T19" HeaderText="Suma Asegurada Total del 1° de enero al 31 de marzo del 2019" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAB2T19" HeaderText="Suma Asegurada Básica del 1° de abril al 15 de mayo del 2019" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAP12Q19" HeaderText="Suma Asegurada Potenciada del 1° de abril al 15 de mayo del 2019" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAT2T19" HeaderText="Suma Asegurada Total del 1° de abril al 15 de mayo del 2019" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                                      
                                <asp:BoundField DataField="PAB1PV" HeaderText="Prima Básica del 16 de noviembre al 31 de diciembre de 2017" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAP1PV" HeaderText="Prima Potenciada del 16 de noviembre al 31 de diciembre de 2017" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT1PV" HeaderText="Prima Total del 16 de noviembre al 31 de diciembre de 2017" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAB1T" HeaderText="Prima Básica del 1° de enero al 31 de marzo del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAP1T" HeaderText="Prima Potenciada del 1° de enero al 31 de marzo del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT1T" HeaderText="Prima Total del 1° de enero al 31 de marzo del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAB2T" HeaderText="Prima Básica del 1° de abril al 30 de junio del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAP2T" HeaderText="Prima Potenciada del 1° de abril al 30 de junio del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT2T" HeaderText="Prima Total del 1° de abril al 30 de junio del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAB3T" HeaderText="Prima Básica del 1° de julio al 30 de septiembre del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAP3T" HeaderText="Prima Potenciada del 1° de julio al 30 de septiembre del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT3T" HeaderText="Prima Total del 1° de julio al 30 de septiembre del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAB4T" HeaderText="Prima Básica del 1° de octubre al 31 de diciembre del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAP4T" HeaderText="Prima Potenciada del 1° de octubre al 31 de diciembre del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT4T" HeaderText="Prima Total del 1° de octubre al 31 de diciembre del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAB1T19" HeaderText="Prima Básica del 1° de enero al 31 de marzo del 2019" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAP6Q19" HeaderText="Prima Potenciada del 1° de enero al 31 de marzo del 2019" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT1T19" HeaderText="Prima Total del 1° de enero al 31 de marzo del 2019" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAB2T19" HeaderText="Prima Básica del 1° de abril al 15 de mayo del 2019" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAP12Q19" HeaderText="Prima Potenciada del 1° de abril al 15 de mayo del 2019" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT2T19" HeaderText="Prima Total del 1° de abril al 15 de mayo del 2019" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>

                                <asp:BoundField DataField="PABT" HeaderText="Prima Básica Total del 16 de noviembre 2017 al 15 de mayo del 2019" HeaderStyle-BackColor="Navy" HeaderStyle-ForeColor="White" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PPT" HeaderText="Prima Potenciada total del 16 de noviembre 2017 al 15 de mayo del 2019" HeaderStyle-BackColor="Navy" HeaderStyle-ForeColor="White" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT" HeaderText="Prima Potenciada total del 16 de noviembre 2017 al 15 de mayo del 2019" HeaderStyle-BackColor="Navy" HeaderStyle-ForeColor="White" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                            </Columns>
                            <HeaderStyle ForeColor="Black" />
                            <RowStyle Font-Size="XX-Small" />
                        </asp:GridView>                
                            
                            </ContentTemplate>
                        </asp:UpdatePanel>
        
                    </div>

                    <div id="DivFooterRow" style="overflow:hidden"></div>

            </div>
        
        </fieldset>    
</asp:Content>
