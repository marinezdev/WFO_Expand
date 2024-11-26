<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Archivos.ascx.cs" Inherits="WFO.Utilerias.Archivos" %>


<fieldset>
    <legend>ARCHIVOS</legend>
    <br />
    <asp:FileUpload ID="fileUpDocumento" runat="server" />
    <asp:Button ID="btnSubirDocumento" runat="server" Text="Subir" CssClass="boton" OnClick="btnSubirDocumento_Click" /><br />
    <asp:ListBox ID="lstArchivos" runat="server" Height="150px" Width="95%" SelectionMode="Single" >
    </asp:ListBox>
    <br />
    <asp:Button ID="btnEliminaDocumento" runat="server" Text="Eliminar" CssClass="boton" OnClick="btnEliminaDocumento_Click" />
</fieldset>