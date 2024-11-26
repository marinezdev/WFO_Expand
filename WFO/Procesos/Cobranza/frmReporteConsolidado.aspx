<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmReporteConsolidado.aspx.cs" Inherits="WFO.Procesos.Cobranza.frmReporteConsolidado" MasterPageFile="~/Utilerias/Site.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
<script>
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
</script>

    <fieldset>
        <legend>REPORTE CONSOLIDADO</legend>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

        <asp:Button ID="BtnGenerar" runat="server" Text="Generar Reporte" OnClick="BtnGenerar_Click" CssClass="boton" />&nbsp;<asp:Button ID="BtnExportar" runat="server" Text="Exportar a Excel" Visible="false" OnClick="BtnExportar_Click" CssClass="boton" />

        <div id="DivRoot" style="margin:auto">

            <div style="overflow: hidden;" id="DivHeaderRow"></div>

                <div style="overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">

                    <asp:UpdatePanel ID="upGVAgregado" runat="server">
                        <ContentTemplate>

                        <asp:GridView ID="gvAgregado" runat="server"
                            AutoGenerateColumns="false" 
                            BackColor="White" 
                            BorderColor="Black" 
                            BorderStyle="None" 
                            BorderWidth="0" GridLines="Both" 
                            HeaderStyle-Font-Bold="true"
                            HeaderStyle-Font-Size="XX-Small"
                            RowStyle-Font-Size="Small"
                            CellPadding="4" 
                            CellSpacing="1"  
                            RowStyle-Wrap="false" 
                            HeaderStyle-Wrap="true"  
                            ShowFooter="true" 
                            Width="100%" OnRowCreated="gvAgregado_RowCreated">
                            <Columns>
                                <asp:BoundField DataField="Dependencia" HeaderText="Secretarías y Entidades Participantes de la Administración Pública Federal" HeaderStyle-BackColor="SteelBlue" HeaderStyle-ForeColor="White" />
                                <asp:BoundField DataField="Poliza" HeaderText="Poliza" HeaderStyle-BackColor="Aqua" />                                

                                <asp:BoundField DataField="Titulares_1VB" HeaderText="Titulares" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Conyuge_1VB" HeaderText="Conyuges" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Hijos_1VB" HeaderText="Hijos" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Ascendientes_1VB" HeaderText="Ascendientes" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalAsegurados_1VB" HeaderText="Total Asegurados" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                
                                <asp:BoundField DataField="C333_1VB" HeaderText="333" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C295_1VB" HeaderText="295" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C2664_1VB" HeaderText="266.4" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C259_1VB" HeaderText="259" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C222_1VB" HeaderText="222" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C185_1VB" HeaderText="185" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C148_1VB" HeaderText="148" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C111_1VB" HeaderText="111" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C74_1VB" HeaderText="74" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Otras_1VB" HeaderText="Otas SA's" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalAsegurados_1VB2" HeaderText="Total Asegurados" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="BasicaMonto_1VB" HeaderText="Básica" DataFormatString="{0:c}" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="BasicaAjuste_1VB" HeaderText="Ajuste" DataFormatString="{0:c}" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalPrimaBasica_1VB" HeaderText="Total Prima Basica" DataFormatString="{0:c}" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="Titulares_1VP" HeaderText="Titulares" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Conyuge_1VP" HeaderText="Conyuges" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Hijos_1VP" HeaderText="Hijos" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Ascendientes_1VP" HeaderText="Ascendientes" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalAsegurados_1VP" HeaderText="Total Asegurados" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="C74_1VP" HeaderText="74" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C111_1VP" HeaderText="111" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C148_1VP" HeaderText="148" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C185_1VP" HeaderText="185" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C222_1VP" HeaderText="222" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C259_1VP" HeaderText="259" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C295_1VP" HeaderText="295" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C333_1VP" HeaderText="333" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C444_1VP" HeaderText="444" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C592_1VP" HeaderText="592" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C740_1VP" HeaderText="740" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C850_1VP" HeaderText="850" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C1000_1VP" HeaderText="1000" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C34219_1VP" HeaderText="34219" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Otras_1VP" HeaderText="Otras SA's" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalAsegurados_1VP2" HeaderText="Total Asegurados" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                
                                <asp:BoundField DataField="PotenciadaMonto_1VP" HeaderText="Potenciada" DataFormatString="{0:c}" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PotenciadaAjuste_1VP" HeaderText="Ajustes" DataFormatString="{0:c}" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalPrimaPotenciada_1VP" HeaderText="Total Prima Potenciada" DataFormatString="{0:c}" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                
                                <asp:BoundField DataField="PrimaTotalPeriodo_1V" HeaderText="Prima Total del Período" DataFormatString="{0:c}" HeaderStyle-BackColor="SteelBlue" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="Titulares_1TB" HeaderText="Titulares" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Conyuge_1TB" HeaderText="Conyuges" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Hijos_1TB" HeaderText="Hijos" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Ascendientes_1TB" HeaderText="Ascendientes" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalAsegurados_1TB" HeaderText="Total Asegurados" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="C333_1TB" HeaderText="333" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C295_1TB" HeaderText="295" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C2664_1TB" HeaderText="266.4" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C259_1TB" HeaderText="259" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C222_1TB" HeaderText="222" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C185_1TB" HeaderText="185" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C148_1TB" HeaderText="148" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C111_1TB" HeaderText="111" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C74_1TB" HeaderText="74" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Otras_1TB" HeaderText="Otas SA's" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalAsegurados_1TB2" HeaderText="Total Asegurados" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="BasicaMonto_1TB" HeaderText="Básica" DataFormatString="{0:c}" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="BasicaAjuste_1TB" HeaderText="Ajuste" DataFormatString="{0:c}" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalPrimaBasica_1TB" HeaderText="Total Prima Basica" DataFormatString="{0:c}" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="Titulares_1TP" HeaderText="Titulares" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Conyuge_1TP" HeaderText="Conyuges" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Hijos_1TP" HeaderText="Hijos" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Ascendientes_1TP" HeaderText="Ascendientes" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right"  />
                                <asp:BoundField DataField="TotalAsegurados_1TP" HeaderText="Tota Asegurados" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="C74_1TP" HeaderText="74" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C111_1TP" HeaderText="111" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C148_1TP" HeaderText="148" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C185_1TP" HeaderText="185" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C222_1TP" HeaderText="222" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C259_1TP" HeaderText="259" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C295_1TP" HeaderText="295" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C333_1TP" HeaderText="333" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C444_1TP" HeaderText="444" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C592_1TP" HeaderText="592" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C740_1TP" HeaderText="740" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C850_1TP" HeaderText="850" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C1000_1TP" HeaderText="1000" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C34219_1TP" HeaderText="34219" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Otras_1TP" HeaderText="Otras SA's" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalAsegurados_1TP2" HeaderText="Tota Asegurados" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                
                                <asp:BoundField DataField="PotenciadaMonto_1TP" HeaderText="Potenciada" DataFormatString="{0:c}" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PotenciadaAjuste_1TP" HeaderText="Ajustes" DataFormatString="{0:c}" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalPrimaPotenciada_1TP" HeaderText="Total Prima Potenciada" DataFormatString="{0:c}" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="PrimaTotalPeriodo_1T" HeaderText="Prima Total del Período" DataFormatString="{0:c}" HeaderStyle-BackColor="SteelBlue" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="Titulares_2TB" HeaderText="Titulares" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Conyuge_2TB" HeaderText="Conyuges" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Hijos_2TB" HeaderText="Hijos" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Ascendientes_2TB" HeaderText="Ascendientes" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalAsegurados_2TB" HeaderText="Total Asegurados" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="C333_2TB" HeaderText="333" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C295_2TB" HeaderText="295" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C2664_2TB" HeaderText="266.4" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C259_2TB" HeaderText="259" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C222_2TB" HeaderText="222" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C185_2TB" HeaderText="185" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C148_2TB" HeaderText="148" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C111_2TB" HeaderText="111" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C74_2TB" HeaderText="74" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Otras_2TB" HeaderText="Otras SA's" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalAsegurados_2TB2" HeaderText="Total Asegurados" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="BasicaMonto_2TB" HeaderText="Básica" DataFormatString="{0:c}" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="BasicaAjuste_2TB" HeaderText="Ajuste" DataFormatString="{0:c}" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalPrimaBasica_2TB" HeaderText="Total Prima Basica" DataFormatString="{0:c}" HeaderStyle-BackColor="LightGray" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="Titulares_2TP" HeaderText="Titulares" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Conyuge_2TP" HeaderText="Conyuges" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Hijos_2TP" HeaderText="Hijos" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Ascendientes_2TP" HeaderText="Ascendientes" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalAsegurados_2TP" HeaderText="Total Asegurados" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="C74_2TP" HeaderText="74" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C111_2TP" HeaderText="111" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C148_2TP" HeaderText="148" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C185_2TP" HeaderText="185" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C222_2TP" HeaderText="222" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C259_2TP" HeaderText="259" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C295_2TP" HeaderText="295" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C333_2TP" HeaderText="333" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C444_2TP" HeaderText="444" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C592_2TP" HeaderText="592" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C740_2TP" HeaderText="740" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C850_2TP" HeaderText="850" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C1000_2TP" HeaderText="1000" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C34219_2TP" HeaderText="34219" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Otras_2TP" HeaderText="Otras SA's" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalAsegurados_2TP2" HeaderText="Total Asegurados" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="PotenciadaMonto_2TP" HeaderText="Potenciada" DataFormatString="{0:c}" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PotenciadaAjuste_2TP" HeaderText="Ajustes" DataFormatString="{0:c}" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalPrimaPotenciada_2TP" HeaderText="Total Prima Potenciada" DataFormatString="{0:c}" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="PrimaTotalPeriodo_2T" HeaderText="Prima Total del Período" DataFormatString="{0:c}" HeaderStyle-BackColor="SteelBlue" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField DataField="TotalGeneral" HeaderText="Total de Primas Pagadas" DataFormatString="{0:c}" HeaderStyle-BackColor="Orange" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" />
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
