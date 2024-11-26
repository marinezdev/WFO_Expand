<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLog.aspx.cs" Inherits="WFO.Administracion.frmLog" MasterPageFile="~/Utilerias/Site.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Log de errores</h2>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <strong>Dé click en el archivo de errores a revisar.</strong><br /><br />

                    <asp:GridView ID="gvArchivos" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered">
                        <Columns>
                            <asp:TemplateField HeaderText="Archivos">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkNombresarchivos" runat="server" Text='<%# Eval("Nombre") %>' NavigateUrl='<%# "~//Log//" + Eval("Nombre") %>' Target="_blank"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>     
                        </Columns>
                    </asp:GridView>

                 </div>
                </div>
            </div>
        </div>

</asp:Content>