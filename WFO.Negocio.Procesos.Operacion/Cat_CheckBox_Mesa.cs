using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Negocio.Procesos.Operacion
{
    public class Cat_CheckBox_Mesa
    {
        AccesoDatos.Procesos.Operacion.Cat_CheckBox_Mesa checkBox_Mesa = new AccesoDatos.Procesos.Operacion.Cat_CheckBox_Mesa();

        public List<prop.Cat_CheckBox_Mesa> CheckBox_Mesa_Seleccionar_PorIdMesa(int IdMesa)
        {
            return checkBox_Mesa.CheckBox_Mesa_Seleccionar_PorIdMesa(IdMesa);
        }

        public int Agregar_Check(prop.Cat_CheckBox_Mesa _CheckBox_Mesa)
        {
            return checkBox_Mesa.Agregar_Check(_CheckBox_Mesa);
        }
    }
}
