using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion.Captura;

namespace WFO.AccesoDatos.Procesos.Operacion.Captura
{
    public class Asegurados
    {
        ManejoDatos b = new ManejoDatos();
        public prop.Asegurados Consulta_Asegurados_PorRFC(string RFC, int IdTramite)
        {
            b.ExecuteCommandSP("GM_Asegurados_Seleccionar_PorRFC");
            b.AddParameter("@RFC", RFC, SqlDbType.NVarChar);
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);

            prop.Asegurados resultado = new prop.Asegurados();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Registro = Convert.ToInt32(reader["Registro"].ToString());
                resultado.Respuesta = reader["Respuesta"].ToString();
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public prop.Asegurados GM_UMAN_Seleccionar_PorRFC(string RFC, int IdTramite)
        {
            b.ExecuteCommandSP("GM_UMAN_Seleccionar_PorRFC");
            b.AddParameter("@RFC", RFC, SqlDbType.NVarChar);
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            prop.Asegurados resultado = new prop.Asegurados();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.SA_Total = Convert.ToInt32(reader["SA_Total"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public prop.Asegurados Asegurados_Selecionar_PorIdTramite(int Id)
        {
            b.ExecuteCommandSP("Asegurados_Selecionar_PorIdTramite");
            b.AddParameter("@Idtramite", Id, SqlDbType.Int);
            prop.Asegurados resultado = new prop.Asegurados();
            var reader = b.ExecuteReader();


            while (reader.Read())
            {
                resultado.Id = Convert.ToInt32(reader["Id"].ToString());
                resultado.Examen = Convert.ToInt32(reader["Examen"].ToString());
                resultado.UMAN = reader["UMAN"].ToString();
                resultado.Clave_agente = reader["Calve_Agente"].ToString();
                resultado.FechaFirmaSolicitud = reader["FechaFirmaSolicitud"].ToString();
                resultado.EstadoFirma = reader["Estado"].ToString();
                resultado.Telefono = reader["Telefono"].ToString();
                resultado.Email = reader["Email"].ToString();
                resultado.Antiguedad = reader["Antiguedad"].ToString();
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.cat_catastroficos> Cat_Catastroficos_Selecionar(string Nombre, string APaterno, string AMaterno, DateTime Fecha)
        {
            b.ExecuteCommandSP("Cat_Catastroficos_Selecionar");
            b.AddParameter("@Nombre", Nombre, SqlDbType.NVarChar);
            b.AddParameter("@APaterno", APaterno, SqlDbType.NVarChar);
            b.AddParameter("@AMaterno", AMaterno, SqlDbType.NVarChar);
            b.AddParameter("@Fecha", Fecha, SqlDbType.Date);
            List<prop.cat_catastroficos> resultado = new List<prop.cat_catastroficos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.cat_catastroficos item = new prop.cat_catastroficos()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    FechaNacimiento = reader["FechaNacimiento"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int Asegurados_Cotizador_Actualizar(prop.Cotizador cotizador)
        {
            b.ExecuteCommandSP("Asegurados_Cotizador_Actualizar");
            b.AddParameter("@IdTramite", cotizador.Id, SqlDbType.Int);
            b.AddParameter("@Examen", cotizador.Excamen, SqlDbType.Int);
            b.AddParameter("@Edad", cotizador.edad, SqlDbType.Int);
            b.AddParameter("@Peso", cotizador.peso, SqlDbType.Float);
            b.AddParameter("@Altura", cotizador.estatura, SqlDbType.Float);
            return b.InsertUpdateDelete();
        }

        public int Asegurados_CotizacionExamen_Actualizar(int IdTramite, int Examen)
        {
            b.ExecuteCommandSP("Asegurados_CotizacionExamen_Actualizar");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@Examen", Examen, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int Asegurados_Actualiza_UMAN_Agente(int IdTramite, string RFC, string agente, string UMAN, string FechaFirmaSolicitud, int IdEstado)
        {
            b.ExecuteCommandSP("Asegurados_Actualiza_UMAN_Agente");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@RFC", RFC, SqlDbType.NVarChar);
            b.AddParameter("@UMAN", UMAN , SqlDbType.NVarChar);
            b.AddParameter("@Agente", agente, SqlDbType.NVarChar);
            b.AddParameter("@FechaFirmaSolicitud", string.Format("{0:yyyy/MM/dd}", DateTime.Parse(FechaFirmaSolicitud)), SqlDbType.DateTime);
            b.AddParameter("@IdEstado", IdEstado, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int Asegurados_Actualizar_Datos(int IdTramite, string RFC, string Telefono, string Email, DateTime Fecha)
        {
            b.ExecuteCommandSP("Asegurados_Actualizar_Datos_PorIdtramiteRFC");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@RFC", RFC, SqlDbType.NVarChar);
            b.AddParameter("@Telefono", Telefono, SqlDbType.NVarChar);
            b.AddParameter("@Email", Email, SqlDbType.NVarChar);
            b.AddParameter("@Fecha", Fecha, SqlDbType.Date); 
            return b.InsertUpdateDelete();
        }

        public int Asegurado_Captura_Registrar(int IdTramite, int IdPlan, int IdDeducible, int IdCausaSeguro, int IdTipoProducto, int IdRegiones)
        {
            b.ExecuteCommandSP("Asegurado_Captura_Registrar");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@IdPlan", IdPlan, SqlDbType.Int);
            b.AddParameter("@IdDeducible", IdDeducible, SqlDbType.Int);
            b.AddParameter("@IdCausaSeguro", IdCausaSeguro, SqlDbType.Int);
            b.AddParameter("@IdTipoProducto", IdTipoProducto, SqlDbType.Int);
            b.AddParameter("@IdRegiones", IdRegiones, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int Asegurado_Captura_Actualizar(int IdTramite, int IdPlan, int IdDeducible, int IdCausaSeguro, int IdTipoProducto, int IdRegiones)
        {
            b.ExecuteCommandSP("Asegurado_Captura_Actualizar");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@IdPlan", IdPlan, SqlDbType.Int);
            b.AddParameter("@IdDeducible", IdDeducible, SqlDbType.Int);
            b.AddParameter("@IdCausaSeguro", IdCausaSeguro, SqlDbType.Int);
            b.AddParameter("@IdTipoProducto", IdTipoProducto, SqlDbType.Int);
            b.AddParameter("@IdRegiones", IdRegiones, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public prop.AseguradoCaptura Asegurado_Captura_Seleccionar(int IdTramite)
        {
            b.ExecuteCommandSP("Asegurado_Captura_Seleccionar");
            b.AddParameter("@Idtramite", IdTramite, SqlDbType.Int);
            prop.AseguradoCaptura resultado = new prop.AseguradoCaptura();
            var reader = b.ExecuteReader();
            
            while (reader.Read())
            {
                resultado.Id = Convert.ToInt32(reader["Id"].ToString());
                resultado.IdPlan = Convert.ToInt32(reader["IdPlan"].ToString());
                resultado.IdDeducible = Convert.ToInt32(reader["IdDeducible"].ToString());
                resultado.IdCausaSeguro = Convert.ToInt32(reader["IdCausaSeguro"].ToString());
                resultado.IdTipoProducto = Convert.ToInt32(reader["IdTipoProducto"].ToString());
                resultado.IdRegiones = Convert.ToInt32(reader["IdRegiones"].ToString());
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public prop.Asegurados Asegurado_Seleccionar_IdTRamite(int IdTramite)
        {
            b.ExecuteCommandSP("Asegurado_Seleccionar_IdTRamite");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            prop.Asegurados resultado = new prop.Asegurados();
            var reader = b.ExecuteReader();

            while (reader.Read())
            {
                resultado.FechaFirmaSolicitud = reader["FechaFirmaSolicitud"].ToString();
                resultado.EstadoFirma = reader["Estado"].ToString();
                resultado.UMAN = reader["UMAN"].ToString();
                resultado.Clave_agente = reader["ClaveAgente"].ToString();
                resultado.APaterno = reader["ApPaterno"].ToString();
                resultado.AMaterno = reader["ApMaterno"].ToString();
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.FechaNacimiento = reader["FechaNacimiento"].ToString();
                resultado.Antiguedad = reader["Antiguedad"].ToString();
                resultado.Telefono = reader["Telefono"].ToString();
                resultado.Email = reader["Email"].ToString();
                resultado.RFC = reader["RFC"].ToString();
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public prop.AseguradoCaptura Asegurado_Captura_Seleccionar_IdTramite(int IdTramite)
        {
            b.ExecuteCommandSP("Asegurado_Captura_Seleccionar_IdTramite");
            b.AddParameter("@Idtramite", IdTramite, SqlDbType.Int);
            prop.AseguradoCaptura resultado = new prop.AseguradoCaptura();
            var reader = b.ExecuteReader();

            while (reader.Read())
            {
                resultado.Plan = reader["Plan"].ToString();
                resultado.Deducible = reader["deducible"].ToString();
                resultado.CausaSeguro = reader["causa_seguro"].ToString();
                resultado.TipoProducto = reader["tipo_producto"].ToString();
                resultado.Riesgo = reader["region"].ToString();
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}