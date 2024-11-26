using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades;
using DevExpress.Web;
using System.Drawing;

namespace WFO.Funciones
{
    /// <summary>
    /// Llena un control con datos
    /// </summary>
    public static class LlenarControles
    {
        public static void LlenarRepeater(ref Repeater Tabla, DataTable Datos)
        {
            Tabla.DataSource = Datos;
            Tabla.DataBind();
        }
        
        public static void LlenarRepeaterLista<T>(ref Repeater Tabla, List<T> list)
        {
            Tabla.DataSource = list;
            Tabla.DataBind();
        }
        
        /// <summary>
        /// Llena un gridview con una lista
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridview"></param>
        /// <param name="list"></param>
        public static void LlenarGridView<T>(ref GridView gridview, List<T> list)
        {
            gridview.DataSource = list;
            gridview.CellPadding = 5;
            gridview.CellSpacing = 5;
            gridview.HeaderStyle.Font.Bold = true;
            gridview.EmptyDataText = "Ningún dato para mostrar";
            gridview.DataBind();
            //gridview.SelectedRowStyle.BackColor = Color.Gray;
        }

        /// <summary>
        /// Llena un gridview con un datatable
        /// </summary>
        /// <param name="gridview"></param>
        /// <param name="datatable"></param>
        public static void LlenarGridView(ref GridView gridview, DataTable datatable)
        {
            gridview.DataSource = datatable;
            gridview.CellPadding = 15;
            gridview.CellSpacing = 10;
            //gridview.BorderStyle = BorderStyle.None;
            //gridview.BorderWidth = 0;
            //gridview.HeaderStyle.Font.Bold = true;
            gridview.EmptyDataText = "Ningún dato para mostrar";
            //gridview.SelectedRowStyle.BackColor = Color.Gray;
            gridview.DataBind();
        }

        public static void LlenarAspxGridView<T>(ref ASPxGridView aspxgridview, List<T> list)
        {
            aspxgridview.DataSource = list;
            aspxgridview.DataBind();
        }

        public static void LlenaraAspxGridView(ref ASPxGridView aspxgridview, DataTable datatable)
        {
            aspxgridview.DataSource = datatable;
            aspxgridview.DataBind();
        }

        public static void LlenarDropDownList<T>(ref DropDownList dropdownlist, List<T> list, string text, string value)
        {
            dropdownlist.DataSource = list;
            dropdownlist.DataTextField = text;
            dropdownlist.DataValueField = value;
            dropdownlist.DataBind();
            dropdownlist.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        public static void LlenarASPxComboBox<T>(ref ASPxComboBox aSPxComboBox, List<T> list, string text, string value)
        {
            int lista = list.Count;
            aSPxComboBox.DataSource = list;
            aSPxComboBox.TextField = text;
            aSPxComboBox.ValueField = value;
            aSPxComboBox.DataBind();
            //aSPxComboBox.SelectedIndex = 0;
        }

        public static void LlenarDropDownList(ref DropDownList dropdownlist, DataTable datatable, string texto, string valor)
        {
            dropdownlist.DataSource = datatable;
            dropdownlist.DataTextField = texto;
            dropdownlist.DataValueField = valor;
            dropdownlist.DataBind();
            dropdownlist.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        public static void LlenarCheckBoxList<T>(ref CheckBoxList checkboxlist, List<T> lista, string texto, string valor)
        {
            checkboxlist.DataSource = lista;
            checkboxlist.DataTextField = texto;
            checkboxlist.DataValueField = valor;
            checkboxlist.DataBind();
        }

        /// <summary>
        /// Llena un treeview de forma recursiva e infita para menú
        /// </summary>
        /// <param name="lista">List<> con los datos</param>
        /// <param name="nodoPadre">Al inicio con null</param>
        /// <param name="treeview">control referenciado</param>
        public static void LlenarTreeViewMenu(IEnumerable<prop.PermisosMenuRol> lista, TreeNode nodoPadre, ref TreeView treeview)
        {
            var nodos = lista.Where(nodosInternos => nodoPadre == null ? nodosInternos.Pertenecea == 0 : nodosInternos.Pertenecea == int.Parse(nodoPadre.Value));
            foreach (var node in nodos)
            {
                TreeNode nuevoNodo = new TreeNode(node.Descripcion, node.IdMenu.ToString());
                if (nodoPadre == null)
                {
                    nuevoNodo.Checked = node.Activo;
                    treeview.Nodes.Add(nuevoNodo);
                }
                else
                {
                    nuevoNodo.Checked = node.Activo;
                    nodoPadre.ChildNodes.Add(nuevoNodo);
                }
                LlenarTreeViewMenu(lista, nuevoNodo, ref treeview);
            }
        }

        

    }
}
