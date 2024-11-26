using DevExpress.Web;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Procesos.SupervisionGeneral
{
    public partial class Asignar : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (IU.ManejadorSesion)Session["Sesion"];
            if (!IsPostBack)
            {
                //supervisiongeneralcatpromotoria.Seleccionar_DropdownList(ref DDLCatPromotoria);
                supervisiongeneralcatpromotoria.Seleccionar_ASPxComboBox(ref ComboCatPromotoria);
                //catalogos.cat_StatusTramites_DropDownList(ref DDLEstados);
                catalogos.cat_StatusTramites_ASPxComboBox(ref ComboCatEstado);
                supervisiongeneraltramites.SelecionarRepeater(ref rptTramite, manejo_sesion.Usuarios.IdUsuario);
                //supervisiongeneraltramites.Seleccionar(ref GVTramites);
            }
        }
        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            string Estado = "0";
            string promotoria = "0";

            if(ComboCatEstado.SelectedIndex.ToString() == "-1")
                Estado = "0";
            else
                Estado = ComboCatEstado.SelectedItem.Value.ToString();

            if (ComboCatPromotoria.SelectedIndex.ToString() == "-1")
                promotoria = "0";
            else
                promotoria = ComboCatPromotoria.SelectedItem.Value.ToString();


            supervisiongeneraltramites.Buscar(ref rptTramite, txtFolio.Text, txtRegistroDel.Text, txtRegistroAl.Text, txtSolicitudDel.Text, txtSolicitudAl.Text, promotoria, Estado, manejo_sesion.Usuarios.IdUsuario);
            string script = "";
            script = "$('#datatableAsignar').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); ";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }

        protected void LigaAsignar_Click(object sender, EventArgs e)
        {
            tablaBusqueda.Visible = false;
            rptTramite.Visible = false;
            tablaMesas.Visible = true;

            LinkButton btn = sender as LinkButton;
            string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
            var IdTramite = commandArgs[0];
            var IdUsuario = commandArgs[1];

            // ELIMINA CONTENIDO DE BUSQUEDA TRAMITES
            rptTramite.DataSource = null;
            rptTramite.DataBind();

            // PINTA LAS MESAS DISPONIBLES DEL TRAMITE
            supervisiongeneralasignar.MostrarMesasDisponiblesAsiganr(ref RepeaterMesas, labelFolio, Convert.ToInt32(IdTramite.ToString().Trim()));

            string script = "";
            script = "$('#datatable').dataTable({'order': [[0, 'desc']],'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'}});";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            /*
            if (sender is LinkButton)
            {
                try
                {
                    LinkButton btn = sender as LinkButton;
                    string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
                    var id = commandArgs[0];

                    ViewState["Id"] = id;
                    ViewState["IdUsuario"] = commandArgs[1];
                    GridViewRow row = btn.NamingContainer as GridViewRow;
                    //Procesar el registro (obtener las mesas disponibles para ese trámite)
                    tablaBusqueda.Visible = false;
                    rptTramite.DataSource = null;
                    rptTramite.DataBind();

                    
                    rptTramite.Visible = false;
                    tablaMesas.Visible = true;

                    // obtener el detalle del registro
                    // var detalle = supervisiongeneraltramites.SeleccionarPorId(id);

                    // Obtener las mesas disponibles para este trámite
                    supervisiongeneralasignar.MostrarMesasDisponibles(ref GVMesas, id);
                    
                }
                catch (Exception ex)
                {
                    log.Agregar("Error al intentar actualizar el usuario: " + ex.Message);
                }
            }
            */
        }

        protected void BtnAsignar_Click(object sender, EventArgs e)
        {
            int IdTramiteMesa = Convert.ToInt32(hfIdTM.Value.ToString());
            int IdTramite = Convert.ToInt32(hfIdTramite.Value.ToString());
            int IdUsuario = Convert.ToInt32(ASPxCombo_Usuarios.SelectedItem.Value);
            string script = "";

            if (supervisiongeneralasignar.ActualizarUsuarioMesa(IdTramiteMesa, IdUsuario) == 1)
            {
                //Response.Redirect(Request.RawUrl);
                //script = "UsuarioSuccess(); $('#myModal').modal('hide'); $('body').removeClass('modal-open'); $('.modal-backdrop').remove();";
                supervisiongeneralasignar.MostrarMesasDisponiblesAsiganr(ref RepeaterMesas, labelFolio, IdTramite);

                script = "UsuarioSuccess(); $('#myModal').modal('hide');" +
                        "if ($('.modal-backdrop').is (':visible')) {" +
                            "$('body').removeClass('modal-open'); " +
                            "$('.modal-backdrop').remove();" +
                        "};" +
                        "$('#datatable').dataTable({'order': [[0, 'desc']],'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'}});";
                

            }
            else
            {
                script = "ErrorAsiganacion('Asignación no disponible'); $('#datatable').dataTable({'order': [[0, 'desc']],'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'}});";
            }
            

            // script = "$('#myModal').modal('hide'); $('body').removeClass('modal-open'); $('.modal-backdrop').remove(); ";

            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }

        protected void AsiganarTramite_Click(object sender, EventArgs e)
        {
            try
            {
                hfIdTM.Value = "0";
                hfIdTramite.Value = "0";

                LinkButton btn = sender as LinkButton;
                string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });

                var IdTramiteMesa = commandArgs[0];
                var Mesa = commandArgs[1];
                var IdTramite = commandArgs[2];

                labelNombreMesa.Text = Mesa.ToString();
                hfIdTM.Value = IdTramiteMesa;
                hfIdTramite.Value = IdTramite;

                sisUsrs.SeleccionarUsuarios_ASPxComboBox(ref ASPxCombo_Usuarios);

                string script = "";
                script = "carga(); $('#datatable').dataTable({'order': [[0, 'desc']],'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'}});";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            catch
            {
                string mensajes = "Asignación no disponible";
                string script = "";
                script = "ErrorAsiganacion('" + mensajes + "'); $('#datatable').dataTable({'order': [[0, 'desc']],'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'}});";

                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Asignar.aspx");
        }
        
        protected void GVMesas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVMesas.EditIndex = e.NewEditIndex;
            supervisiongeneralasignar.MostrarMesasDisponibles(ref GVMesas, ViewState["Id"].ToString());
        }

        protected void GVMesas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVMesas.EditIndex = -1;
            supervisiongeneralasignar.MostrarMesasDisponibles(ref GVMesas, ViewState["Id"].ToString());
        }

        protected void GVMesas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string idtramitemesa = GVMesas.DataKeys[e.RowIndex].Values["_IdTramiteMesa"].ToString();
            DropDownList valorseleccionado = (DropDownList)GVMesas.Rows[e.RowIndex].Cells[2].Controls[1];
            GVMesas.EditIndex = -1;

            if (valorseleccionado.SelectedValue != "0")
            {
                //Modificaciones
                if (GVMesas.Rows[e.RowIndex].Cells[1].Text != "")
                {
                    //Usuarios existentes
                    supervisiongeneralasignar.CambiarUsuarioAnterior(idtramitemesa, valorseleccionado.SelectedValue);
                    supervisiongeneralasignar.AgregarTramiteMesaBitacoraCambios(ViewState["IdUsuario"].ToString(), valorseleccionado.SelectedValue, manejo_sesion.Usuarios.IdUsuario.ToString(), idtramitemesa);
                }
                else
                {
                    //agregado usuario nuevo a futuro
                    supervisiongeneralasignar.AgregarUsuarioFuturo(valorseleccionado.SelectedValue, manejo_sesion.Usuarios.IdUsuario.ToString(), idtramitemesa);
                }
                string script = "";
                script = "UsuarioSuccess();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                //supervisiongeneralasignar.MostrarMesasDisponibles(ref GVMesas, ViewState["Id"].ToString());
            }
            else
            {
                string script = "";
                script = "UsuarioRequerido();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            
            supervisiongeneralasignar.MostrarMesasDisponibles(ref GVMesas, ViewState["Id"].ToString());
        }

        protected void GVMesas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GVMesas.EditIndex == e.Row.RowIndex)
            {
                //ASPxComboBox aSPxComboBox = (ASPxComboBox)e.Row.FindControl("ddlUsuarios");
                //sisUsrs.SeleccionarUsuarios_ASPxComboBox(ref aSPxComboBox);
                DropDownList ddlUs = (DropDownList)e.Row.FindControl("ddlUsuarios");
                sisUsrs.SeleccionarUsuarios_DropDownList(ref ddlUs);
            }
        }

        protected void GVMesas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sin hacer nada pero necesario para que todo funcione en esta tabla
        }   
        
    }
}