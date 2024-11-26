using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades;

namespace WFO.AccesoDatos.Sistema
{
    public class PermisosMenu
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.PermisosMenu> Seleccionar()
        {
            b.ExecuteCommandSP("PermisosMenu_Seleccionar");
            List<prop.PermisosMenu> resultado = new List<prop.PermisosMenu>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.PermisosMenu item = new prop.PermisosMenu()
                {
                    IdRol = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdRol"].ToString()),
                    IdMenu = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdMenu"].ToString()),
                    Activo = bool.Parse(reader["Activo"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int Agregar(prop.PermisosMenu permisosmenu)
        {
            b.ExecuteCommandSP("PermisosMenu_Agregar");
            b.AddParameter("@idrol", permisosmenu.IdRol, SqlDbType.Int);
            b.AddParameter("@idmenu", permisosmenu.IdMenu, SqlDbType.Int);
            b.AddParameter("@activo", permisosmenu.Activo, SqlDbType.Bit);
            return b.InsertUpdateDelete();
        }

        public int Modificar(int idrol, int idmenu, int activo)
        {
            b.ExecuteCommandSP("PermisosMenu_Actualizar");
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            b.AddParameter("@idmenu", idmenu, SqlDbType.Int);
            b.AddParameter("@activo", activo, SqlDbType.Bit);
            return b.InsertUpdateDelete();
        }

    }
}
