using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades;

namespace WFO.AccesoDatos.Sistema
{
    public class Menu
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.Menu> Seleccionar()
        {
            b.ExecuteCommandSP("Menu_Seleccionar");
            List<prop.Menu> resultado = new List<prop.Menu>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Menu item = new prop.Menu()
                {
                    IdMenu = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdMenu"].ToString()),
                    Descripcion = reader["Descripcion"].ToString(),
                    URL = reader["URL"].ToString(),
                    PerteneceA = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["PerteneceA"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.Menu> SeleccionarPertenencia()
        {
            b.ExecuteCommandSP("Menu_Seleccionar_Pertenencias");
            List<prop.Menu> resultado = new List<prop.Menu>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Menu item = new prop.Menu()
                {
                    IdMenu = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdMenu"].ToString()),
                    Descripcion = reader["Descripcion"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public prop.Menu SeleccionarPorId(int id)
        {
            b.ExecuteCommandSP("Menu_Seleccionar_PorId");
            b.AddParameter("@idmenu", id, SqlDbType.Int);
            prop.Menu resultado = new prop.Menu();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Descripcion = reader["Descripcion"].ToString();
                resultado.URL = reader["URL"].ToString();
                resultado.PerteneceA = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["PerteneceA"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.PermisosMenuRol> SeleccionarAsignadosPorRol(int idrol)
        {
            b.ExecuteCommandSP("Menu_Seleccionar_AsignadosPorRol");
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            List<prop.PermisosMenuRol> resultado = new List<prop.PermisosMenuRol>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.PermisosMenuRol item = new prop.PermisosMenuRol()
                {
                    IdMenu = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdMenu"].ToString()),
                    Descripcion = reader["Descripcion"].ToString(),
                    Url = reader["Url"].ToString(),
                    Pertenecea = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Pertenecea"].ToString()),
                    Idrol = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdRol"].ToString()),
                    Activo = bool.Parse(reader["Activo"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int SeleccionarHijos(string idrol)
        {

            b.ExecuteCommandSP("Menu_seleccionar_ContarHijos");
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            int totoal = 0;
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                totoal = Convert.ToInt32(reader["TOTAL"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return totoal;



            //b.ExecuteCommandSP("Menu_seleccionar_ContarHijos");
            //b.AddParameter("@idrol", idrol, SqlDbType.Int);

            //return int.Parse(b.Select().Rows[0][0].ToString());
        }

        /// <summary>
        /// Obtiene los datos para armar un menú desde una base de datos
        /// </summary>
        /// <param name="modulo">Número de módulo</param>
        /// <param name="grupo">Número de grupo</param>
        /// <returns>Lista con los datos</returns>
        public List<prop.Menu> MenuDinamicoObtener(int idrol)
        {
            List<prop.Menu> menutotal = new List<prop.Menu>();
            b.ExecuteCommandSP("Menu_Seleccionar_Armado");
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Menu menuitems = new prop.Menu()
                {
                    IdMenu = Funciones.Numeros.ConvertirTextoANumeroEntero(reader[0].ToString()),
                    PerteneceA = int.Parse(reader[4].ToString()),
                    icono = reader[3].ToString(),
                    Descripcion = reader[1].ToString(),
                    URL = reader[2].ToString() ?? "#"
                };
                menutotal.Add(menuitems);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return menutotal;
        }

        public int Agregar(string descripcion, string url, int pertenecea)
        {
            b.ExecuteCommandSP("Menu_Agregar");
            b.AddParameter("@descripcion", descripcion, SqlDbType.VarChar, 150);
            b.AddParameter("@url", url, SqlDbType.VarChar, 150);
            b.AddParameter("@pertenecea", pertenecea, SqlDbType.Int);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public int Modificar(string descripcion, string url, int pertenecea, int idmenu)
        {
            b.ExecuteCommandSP("Menu_Modificar");
            b.AddParameter("@descripcion", descripcion, SqlDbType.VarChar, 150);
            b.AddParameter("@url", url, SqlDbType.VarChar, 150);
            b.AddParameter("@pertenecea", pertenecea, SqlDbType.Int);
            b.AddParameter("@idmenu", idmenu, SqlDbType.Int);
            return b.InsertUpdateDeleteWithTransaction();
        }

       

    }
}
