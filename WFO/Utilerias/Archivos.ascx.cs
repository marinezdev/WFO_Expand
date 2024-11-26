using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WFO.Utilerias
{
    public partial class Archivos : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubirDocumento_Click(object sender, EventArgs e)
        {
            if (fileUpDocumento.HasFile)
            {
                lstArchivos.Items.Add(fileUpDocumento.FileName);
            }
        }

        protected void btnEliminaDocumento_Click(object sender, EventArgs e)
        {
            lstArchivos.Items.RemoveAt(lstArchivos.SelectedIndex);
        }
    }
}