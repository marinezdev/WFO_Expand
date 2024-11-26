using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.AccesoDatos.Procesos.Promotoria
{
    public class Catalogos
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.cat_producto> Cat_Productos(int TipoTramite)
        {
            b.ExecuteCommandSP("Cat_Producto_Seleccionar_PorTipo_tramite");
            b.AddParameter("@TipoTramite", TipoTramite, SqlDbType.Int);
            List<prop.cat_producto> resultado = new List<prop.cat_producto>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_producto item = new prop.cat_producto()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Id_TipoTramite = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id_TipoTramite"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Descripcion = reader["Descripcion"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_subproducto> Cat_SubProductos(int Id)
        {
            b.ExecuteCommandSP("Cat_SubProducto_Seleccionar_PorIdProducto");
            b.AddParameter("@Id_cat_producto", Id, SqlDbType.Int);
            List<prop.cat_subproducto> resultado = new List<prop.cat_subproducto>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_subproducto item = new prop.cat_subproducto()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Id_CatProducto = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id_CatProducto"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Descripcion = reader["Descripcion"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_riesgos> Cat_Riesgos()
        {
            b.ExecuteCommandSP("Cat_Riesgo_selecionar");
            List<prop.cat_riesgos> resultado = new List<prop.cat_riesgos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_riesgos item = new prop.cat_riesgos()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Riesgo = reader["Riesgo"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_moneda> Cat_Monedas()
        {
            b.ExecuteCommandSP("Cat_Monedas_Seleccionar");
            List<prop.cat_moneda> resultado = new List<prop.cat_moneda>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_moneda item = new prop.cat_moneda()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Valor = Funciones.Numeros.ConvertirTextoANumeroDecimal(reader["Valor"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_Instituciones> cat_instituciones()
        {
            b.ExecuteCommandSP("Cat_Instituciones_Seleccionar");
            List<prop.cat_Instituciones> resultado = new List<prop.cat_Instituciones>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_Instituciones item = new prop.cat_Instituciones()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Banco = reader["Banco"].ToString(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_pais> Cat_Paises()
        {
            b.ExecuteCommandSP("Cat_Pais_Seleccionar");
            List<prop.cat_pais> resultado = new List<prop.cat_pais>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_pais item = new prop.cat_pais()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    PaisNombre = reader["PaisNombre"].ToString().Trim()
                    //Sancionado = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Sancionado"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.promotoria_usuario> Promotoria_Usuarios(int Id)
        {
            b.ExecuteCommandSP("[Promotoria_Usuario_Seleccionar]");
            b.AddParameter("@IdUsuario", Id, SqlDbType.Int);
            List<prop.promotoria_usuario> resultado = new List<prop.promotoria_usuario>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.promotoria_usuario item = new prop.promotoria_usuario()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Clave = reader["Clave"].ToString(),
                    Clave_Region = reader["clave_region"].ToString(),
                    Region = reader["region"].ToString(),
                    Clave_Gerente = reader["clave_gerente"].ToString(),
                    Gerente = reader["gerente"].ToString(),
                    Clave_Ejecutivo = reader["clave_ejecutivo"].ToString(),
                    Ejecutivo = reader["ejecutivo"].ToString(),
                    Clave_Front = reader["clave_front"].ToString(),
                    Front = reader["front"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.promotoria_usuario> Promotoria_Seleccionar_PorIdTramite(int Id_Promotoria, int Id_Tramite)
        {
            b.ExecuteCommandSP("Promotoria_Seleccionar_PorIdTramite");
            b.AddParameter("@Id_Promotoria", Id_Promotoria, SqlDbType.Int);
            b.AddParameter("@Id_Tramite", Id_Tramite, SqlDbType.Int);
            List<prop.promotoria_usuario> resultado = new List<prop.promotoria_usuario>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.promotoria_usuario item = new prop.promotoria_usuario()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Clave = reader["Clave"].ToString(),
                    Zona = reader["Zona"].ToString(),
                    Clave_Region = reader["clave_region"].ToString(),
                    Region = reader["region"].ToString(),
                    Clave_Gerente = reader["clave_gerente"].ToString(),
                    Gerente = reader["gerente"].ToString(),
                    Clave_Ejecutivo = reader["clave_ejecutivo"].ToString(),
                    Ejecutivo = reader["ejecutivo"].ToString(),
                    Clave_Front = reader["clave_front"].ToString(),
                    Front = reader["front"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.agente_promotoria_usuario> agente_Promotoria_Usuarios(int Id, string Clave)
        {
            b.ExecuteCommandSP("[Agente_Promotoria_Usuario_Seleccionar_PorClaveAgente]");
            b.AddParameter("@IdUsuario", Id, SqlDbType.Int);
            b.AddParameter("@ClavePromotoria", Clave, SqlDbType.NVarChar);
            List<prop.agente_promotoria_usuario> resultado = new List<prop.agente_promotoria_usuario>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.agente_promotoria_usuario item = new prop.agente_promotoria_usuario()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Correo = reader["Correo"].ToString(),
                    Telefono = reader["Telefono"].ToString(),
                    Extencion = reader["Extencion"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_pais> cat_Pais_Sancionado (int Id)
        {
            b.ExecuteCommandSP("[Cat_Pais_Seleccionar_Sancionado]");
            b.AddParameter("@Id_pais", Id, SqlDbType.Int);
            List<prop.cat_pais> resultado = new List<prop.cat_pais>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_pais item = new prop.cat_pais()
                {
                    Sancionado = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Sancionado"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.TramiteN1> BustatramiteN1RFC(string RFC)
        {
            b.ExecuteCommandSP("[Tramite_det_N1_Seleccionar_ExistenteRFC]");
            b.AddParameter("@RFC", RFC, SqlDbType.NVarChar);
            List<prop.TramiteN1> resultado = new List<prop.TramiteN1>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.TramiteN1 item = new prop.TramiteN1()
                {
                    RFC = reader["RFC"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_moneda> BuscaMonedaId(int Id)
        {
            b.ExecuteCommandSP("[Cat_moneda_seleccionar_PorId]");
            b.AddParameter("@Id_moneda", Id, SqlDbType.Int);
            List<prop.cat_moneda> resultado = new List<prop.cat_moneda>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_moneda item = new prop.cat_moneda()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    Valor = Funciones.Numeros.ConvertirTextoANumeroDecimal(reader["Valor"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_statusTramite> SeleccionaEstatusTramite()
        {
            b.ExecuteCommandSP("Estatus_Tramite_Selecionar");
            List<prop.cat_statusTramite> resultado = new List<prop.cat_statusTramite>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_statusTramite item = new prop.cat_statusTramite()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    BackgroundColor = reader["BackgroundColor"].ToString(),
                    BorderColor = reader["BorderColor"].ToString(),
                    HoverBackgroundColor = reader["HoverBackgroundColor"].ToString()
                };
                resultado.Add(item);
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

        public List<prop.cat_DocRecEmicion> cat_DocRecEmicion_Selecionar_Persona(int IdTipoPersona,int IdTramiteTipo)
        {
            b.ExecuteCommandSP("cat_DocRecEmicion_Selecionar_Persona");
            b.AddParameter("@IdTipoPersona", IdTipoPersona, SqlDbType.Int);
            b.AddParameter("@IdTramiteTipo", IdTramiteTipo, SqlDbType.Int);
            List<prop.cat_DocRecEmicion> resultado = new List<prop.cat_DocRecEmicion>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_DocRecEmicion item = new prop.cat_DocRecEmicion()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Documentos = reader["Documentos"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        
        public List<prop.cat_tipo_servicio> cat_tipo_servicio_Seleccionar_PorTipo_tramite(int TipoTramite)
        {
            b.ExecuteCommandSP("cat_tipo_servicio_Seleccionar_PorTipo_tramite");
            b.AddParameter("@TipoTramite ", TipoTramite, SqlDbType.Int);
            List<prop.cat_tipo_servicio> resultado = new List<prop.cat_tipo_servicio>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_tipo_servicio item = new prop.cat_tipo_servicio()
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


        public List<prop.cat_PromotoriaZonas> cat_PromotoriaZonas_Seleccionar(int IdUsuario)
        {
            b.ExecuteCommandSP("cat_PromotoriaZonas_Seleccionar");
            b.AddParameter("@idUsuario ", IdUsuario, SqlDbType.Int);
            List<prop.cat_PromotoriaZonas> resultado = new List<prop.cat_PromotoriaZonas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_PromotoriaZonas item = new prop.cat_PromotoriaZonas()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Clave = reader["Clave"].ToString(),
                    Nombre = reader["Nombre"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}
