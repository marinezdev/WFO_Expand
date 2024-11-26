using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace WFO.Negocio.Cobranza
{
    /// <summary>
    /// Metodos comunes de personalización para controles diversos
    /// </summary>
    class GridViewLocal 
    {
        /// <summary>
        /// Agrega los estilos comunes para usar en los gridviews
        /// </summary>
        /// <param name="gridview"></param>
        public void GridViewPersonalizado(ref GridView gridview)
        {
            gridview.AutoGenerateColumns = false;
            gridview.BackColor = System.Drawing.Color.White;
            gridview.BorderColor = System.Drawing.Color.Yellow;
            gridview.BorderStyle = BorderStyle.None;
            gridview.BorderWidth = 0;
            gridview.GridLines = GridLines.Both;
            gridview.HeaderStyle.Font.Bold = true;
            gridview.HeaderStyle.Font.Size = FontUnit.XXSmall;
            gridview.RowStyle.Font.Size = FontUnit.Small;
            gridview.CellPadding = 4;
            gridview.CellSpacing = 1;
            gridview.RowStyle.Wrap = false;
            gridview.HeaderStyle.Wrap = false;
            gridview.ShowFooter = true;
            gridview.Width = Unit.Percentage(100);
            gridview.SelectedRowStyle.BackColor = System.Drawing.Color.Green;
            gridview.FooterStyle.BackColor = System.Drawing.Color.White;
            gridview.FooterStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000066");
            gridview.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#deedf7");
            gridview.HeaderStyle.Font.Bold = true;
            gridview.HeaderStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2779aa");
            gridview.RowStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        }
    }
}
