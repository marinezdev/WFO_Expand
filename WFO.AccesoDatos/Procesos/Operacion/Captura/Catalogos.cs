using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion.Captura;

namespace WFO.AccesoDatos.Procesos.Operacion.Captura
{
    public class Catalogos
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.cat_bancos> Cat_Bancos(int IdModoPago)
        {
            b.ExecuteCommandSP("Cat_Bancos_Seleccionar");
            b.AddParameter("@IdModoPago", IdModoPago, SqlDbType.Int);
            List<prop.cat_bancos> resultado = new List<prop.cat_bancos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_bancos item = new prop.cat_bancos()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    banco = reader["banco"].ToString(),
                    LargoToken = Convert.ToInt32(reader["LargoToken"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_modo_pago> Cat_Modo_Pago()
        {
            b.ExecuteCommandSP("Cat_Modo_Pago_Seleccionar");
            List<prop.cat_modo_pago> resultado = new List<prop.cat_modo_pago>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_modo_pago item = new prop.cat_modo_pago()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    modo_pago = reader["modo_pago"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_periodicidad> Cat_Periodicidad()
        {
            b.ExecuteCommandSP("Cat_Periodicidad_Seleccionar");
            List<prop.cat_periodicidad> resultado = new List<prop.cat_periodicidad>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_periodicidad item = new prop.cat_periodicidad()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    periodicidad = reader["periodicidad"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public prop.cat_direcciones Cat_CP_Selecionar_PorCP(string CP)
        {
            b.ExecuteCommandSP("Cat_CP_Selecionar_PorCP");
            b.AddParameter("@CP", CP, SqlDbType.NVarChar);

            prop.cat_direcciones resultado = new prop.cat_direcciones();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Estado = Convert.ToInt32(reader["Estado"].ToString());
                resultado.Poblacion = Convert.ToInt32(reader["Poblacion"].ToString());
                resultado.CP = Convert.ToInt32(reader["CP"].ToString());
                resultado.IdColonia = Convert.ToInt32(reader["IdColonia"].ToString());
                resultado.Colonia = reader["Colonia"].ToString();
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }


        public List<prop.cat_estados> Cat_Direcciones_Estados()
        {
            b.ExecuteCommandSP("Cat_Direcion_Estados");
            List<prop.cat_estados> resultado = new List<prop.cat_estados>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_estados item = new prop.cat_estados()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Estado = reader["Estado"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        
        public List<prop.cat_poblaciones> Cat_Direcciones_Poblacion(int IdEstado)
        {
            b.ExecuteCommandSP("Cat_Direcion_Poblacion_PorIdEstado");
            b.AddParameter("@IdEstado", IdEstado, SqlDbType.Int);
            List<prop.cat_poblaciones> resultado = new List<prop.cat_poblaciones>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_poblaciones item = new prop.cat_poblaciones()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Poblacion = reader["Poblacion"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_cp> Cat_Direcciones_CP_Selecionar_PorIdPoblacion(int IdPoblacion)
        {
            b.ExecuteCommandSP("Cat_Direccion_CP_PorIdPoblacion");
            b.AddParameter("@IdPoblacion", IdPoblacion, SqlDbType.Int);
            List<prop.cat_cp> resultado = new List<prop.cat_cp>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_cp item = new prop.cat_cp()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    CP = reader["CP"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_colonia> Cat_Direcion_Colonia_PorIdCP(int IdCP)
        {
            b.ExecuteCommandSP("Cat_Direcion_Colonia_PorIdCP");
            b.AddParameter("@IdCP", IdCP, SqlDbType.Int);
            List<prop.cat_colonia> resultado = new List<prop.cat_colonia>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_colonia item = new prop.cat_colonia()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Colonia = reader["Colonia"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
        
        public List<prop.cat_TipoAsegurados> Cat_TipoAsegurados_Seleccionar()
        {
            b.ExecuteCommandSP("Cat_TipoAsegurados_Seleccionar");
            List<prop.cat_TipoAsegurados> resultado = new List<prop.cat_TipoAsegurados>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_TipoAsegurados item = new prop.cat_TipoAsegurados()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Interprestacion_larga = reader["Interprestacion_larga"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_padecimiento> Cat_Padecimiento()
        {
            b.ExecuteCommandSP("Cat_Padecimiento_Seleccionar");
            List<prop.cat_padecimiento> resultado = new List<prop.cat_padecimiento>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_padecimiento item = new prop.cat_padecimiento()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }




        public List<prop.cat_PlanMedicalife> Cat_PlanMedicalife_Seleccionar()
        {
            b.ExecuteCommandSP("Cat_PlanMedicalife_Seleccionar");
            List<prop.cat_PlanMedicalife> resultado = new List<prop.cat_PlanMedicalife>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_PlanMedicalife item = new prop.cat_PlanMedicalife()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Plan = reader["CPlan"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_deducible> Cat_Deducible_Seleccionar()
        {
            b.ExecuteCommandSP("Cat_Deducible_Seleccionar");
            List<prop.cat_deducible> resultado = new List<prop.cat_deducible>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_deducible item = new prop.cat_deducible()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    deducible = reader["deducible"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_causa_seguro> Cat_Causa_Seguro_Seleccionar()
        {
            b.ExecuteCommandSP("Cat_Causa_Seguro_Seleccionar");
            List<prop.cat_causa_seguro> resultado = new List<prop.cat_causa_seguro>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_causa_seguro item = new prop.cat_causa_seguro()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    causa_seguro = reader["causa_seguro"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_regiones> Cat_Region_Seleccionar()
        {
            b.ExecuteCommandSP("Cat_Region_Seleccionar");
            List<prop.cat_regiones> resultado = new List<prop.cat_regiones>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_regiones item = new prop.cat_regiones()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    region = reader["region"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_tipo_producto> Cat_Tipo_Producto_Seleccionar()
        {
            b.ExecuteCommandSP("Cat_Tipo_Producto_Seleccionar");
            List<prop.cat_tipo_producto> resultado = new List<prop.cat_tipo_producto>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_tipo_producto item = new prop.cat_tipo_producto()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    tipo_producto = reader["tipo_producto"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }


        public int AgregarPoblacion(int IdEstado, string Poblacion)
        {
            b.ExecuteCommandSP("Cat_Direccion_Poblacion_Agregar");
            b.AddParameter("@IdEstado", IdEstado, SqlDbType.Int);
            b.AddParameter("@Poblacion", Poblacion, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        public int AgregarCP(int IdPoblacion, string CP)
        {
            b.ExecuteCommandSP("Cat_Direccion_CP_Agregar");
            b.AddParameter("@IdPoblacion", IdPoblacion, SqlDbType.Int);
            b.AddParameter("@CP", CP, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }

        public int AgregarColonia(int IdCP, string Colonia)
        {
            b.ExecuteCommandSP("Cat_Direccion_Colonia_Agregar");
            b.AddParameter("@IdCP", IdCP, SqlDbType.Int);
            b.AddParameter("@Colonia", Colonia, SqlDbType.NVarChar);
            return b.InsertUpdateDelete();
        }
    }
}
