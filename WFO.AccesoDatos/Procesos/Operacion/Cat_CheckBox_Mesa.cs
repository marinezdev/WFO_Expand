using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.AccesoDatos.Procesos.Operacion
{
    public class Cat_CheckBox_Mesa
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.Cat_CheckBox_Mesa> CheckBox_Mesa_Seleccionar_PorIdMesa(int IdMesa)
        {
            b.ExecuteCommandSP("CheckBox_Mesa_Seleccionar_PorIdMesa");
            b.AddParameter("@IdMesa", IdMesa, SqlDbType.Int);
            List<prop.Cat_CheckBox_Mesa> resultado = new List<prop.Cat_CheckBox_Mesa>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Cat_CheckBox_Mesa item = new prop.Cat_CheckBox_Mesa()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    IdMesa = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdMesa"].ToString()),
                    Documentos = reader["Documentos"].ToString(),
                    Requerido = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Requerido"].ToString()),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int Agregar_Check(prop.Cat_CheckBox_Mesa _CheckBox_Mesa)
        {
            b.ExecuteCommandSP("Tramite_DocReq_Agragar");
            b.AddParameter("@IdTramite", _CheckBox_Mesa.IdTramite, SqlDbType.Int, 150);
            b.AddParameter("@IdMesa", _CheckBox_Mesa.IdMesa, SqlDbType.Int, 150);
            b.AddParameter("@IdCheck", _CheckBox_Mesa.Id, SqlDbType.VarChar, 150);

            return b.InsertUpdateDeleteWithTransaction();
        }
    }
}
