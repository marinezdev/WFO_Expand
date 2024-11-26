using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace WFO.IU
{
    public class Comun
    {
        public void CargaInicialddlAnnQuincena(ref DropDownList dropdownlist)
        {
            dropdownlist.Items.Clear();
            dropdownlist.Items.Insert(0, new ListItem("Seleccione", "0"));
            dropdownlist.Items.Insert(1, new ListItem("2018/01", "1"));
            dropdownlist.Items.Insert(2, new ListItem("2018/02", "2"));
            dropdownlist.Items.Insert(3, new ListItem("2018/03", "3"));
            dropdownlist.Items.Insert(4, new ListItem("2018/04", "4"));
            dropdownlist.Items.Insert(5, new ListItem("2018/05", "5"));
            dropdownlist.Items.Insert(6, new ListItem("2018/06", "6"));
            dropdownlist.Items.Insert(7, new ListItem("2018/07", "7"));
            dropdownlist.Items.Insert(8, new ListItem("2018/08", "8"));
            dropdownlist.Items.Insert(9, new ListItem("2018/09", "9"));
            dropdownlist.Items.Insert(10, new ListItem("2018/10", "10"));
            dropdownlist.Items.Insert(11, new ListItem("2018/11", "11"));
            dropdownlist.Items.Insert(12, new ListItem("2018/12", "12"));
            dropdownlist.Items.Insert(13, new ListItem("2018/13", "13"));
            dropdownlist.Items.Insert(14, new ListItem("2018/14", "14"));
            dropdownlist.Items.Insert(15, new ListItem("2018/15", "15"));
            dropdownlist.Items.Insert(16, new ListItem("2018/16", "16"));
            dropdownlist.Items.Insert(17, new ListItem("2018/17", "17"));
            dropdownlist.Items.Insert(18, new ListItem("2018/18", "18"));
            dropdownlist.Items.Insert(19, new ListItem("2018/19", "19"));
            dropdownlist.Items.Insert(20, new ListItem("2018/20", "20"));
            dropdownlist.Items.Insert(21, new ListItem("2018/21", "21"));
            dropdownlist.Items.Insert(22, new ListItem("2018/22", "22"));
            dropdownlist.Items.Insert(23, new ListItem("2018/23", "23"));
            dropdownlist.Items.Insert(24, new ListItem("2018/24", "24"));
        }




    }
}
