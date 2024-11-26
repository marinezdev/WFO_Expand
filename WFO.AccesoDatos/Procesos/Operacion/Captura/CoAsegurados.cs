using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion.Captura;

namespace WFO.AccesoDatos.Procesos.Operacion.Captura
{
    public class CoAsegurados
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.Coasegurados> CoAsegurados_Selecionar_PorIdTramite_RFC(int IdTramite, string RFC)
        {
            b.ExecuteCommandSP("CoAsegurados_Selecionar_PorIdTramite_RFC");
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            b.AddParameter("@RFC", RFC, SqlDbType.NVarChar);

            List<prop.Coasegurados> resultado = new List<prop.Coasegurados>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.Coasegurados item = new prop.Coasegurados()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString(),
                    APaterno = reader["APaterno"].ToString(),
                    AMaterno = reader["AMaterno"].ToString(),
                    Interprestacion_larga = reader["Interprestacion_larga"].ToString(),
                    Sexo = reader["Sexo"].ToString(),
                    FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"].ToString()),
                    Edad = reader["Edad"].ToString(),
                    Peso = reader["Peso"].ToString(),
                    Altura = reader["Altura"].ToString(),
                    ExamenEvaluacion = reader["Examen"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public prop.Coasegurados CoAsegurado_Seleccionar_PorID(int Id, int IdTramite)
        {
            b.ExecuteCommandSP("CoAsegurado_Seleccionar_PorID");
            b.AddParameter("@Id", Id, SqlDbType.Int);
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);

            prop.Coasegurados resultado = new prop.Coasegurados();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = Convert.ToInt32(reader["Id"].ToString());
                resultado.Nombre = reader["Nombre"].ToString();
                resultado.APaterno = reader["APaterno"].ToString();
                resultado.AMaterno = reader["AMaterno"].ToString();
                resultado.FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"].ToString());
                resultado.IdTipoAsegurado = Convert.ToInt32(reader["IdTipoAsegurado"].ToString());
                resultado.Sexo = reader["Sexo"].ToString();
                resultado.Edad = reader["Edad"].ToString();
                resultado.Peso = reader["Peso"].ToString();
                resultado.Altura = reader["Altura"].ToString();
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int AgregarCoasegurado(prop.Coasegurados coasegurados)
        {
            b.ExecuteCommandSP("CoAsegurados_Agregar");
            b.AddParameter("@RFC", coasegurados.RFC, SqlDbType.NVarChar);
            b.AddParameter("@Nombre", coasegurados.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@APaterno", coasegurados.APaterno, SqlDbType.NVarChar);
            b.AddParameter("@AMaterno", coasegurados.AMaterno, SqlDbType.NVarChar);
            b.AddParameter("@FechaNacimiento", coasegurados.FechaNacimiento, SqlDbType.DateTime);
            b.AddParameter("@IdTipoAsegurado", coasegurados.IdTipoAsegurado, SqlDbType.Int);
            b.AddParameter("@Sexo", coasegurados.Sexo, SqlDbType.Int);
            b.AddParameter("@Edad", coasegurados.Edad, SqlDbType.Int);
            b.AddParameter("@Peso", coasegurados.Peso, SqlDbType.Float);
            b.AddParameter("@Altura", coasegurados.Altura, SqlDbType.Float);
            b.AddParameter("@Examen", coasegurados.Examen, SqlDbType.Int);
            b.AddParameter("@IdTramite", coasegurados.IdTramite, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int ActualizarCoasegurado(prop.Coasegurados coasegurados)
        {
            b.ExecuteCommandSP("CoAsegurados_Actualizar");
            b.AddParameter("@Id", coasegurados.Id, SqlDbType.Int);
            b.AddParameter("@Nombre", coasegurados.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@APaterno", coasegurados.APaterno, SqlDbType.NVarChar);
            b.AddParameter("@AMaterno", coasegurados.AMaterno, SqlDbType.NVarChar);
            b.AddParameter("@FechaNacimiento", coasegurados.FechaNacimiento, SqlDbType.DateTime);
            b.AddParameter("@IdTipoAsegurado", coasegurados.IdTipoAsegurado, SqlDbType.Int);
            b.AddParameter("@Sexo", coasegurados.Sexo, SqlDbType.Int);
            b.AddParameter("@Edad", coasegurados.Edad, SqlDbType.Int);
            b.AddParameter("@Peso", coasegurados.Peso, SqlDbType.Float);
            b.AddParameter("@Altura", coasegurados.Altura, SqlDbType.Float);
            b.AddParameter("@Examen", coasegurados.Examen, SqlDbType.Int);
            b.AddParameter("@IdTramite", coasegurados.IdTramite, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int ActualizarCoaseguradoMesaCaptura(prop.Coasegurados coasegurados)
        {
            b.ExecuteCommandSP("CoAsegurados_Actualizar_MesaCaptura");
            b.AddParameter("@Id", coasegurados.Id, SqlDbType.Int);
            b.AddParameter("@Nombre", coasegurados.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@APaterno", coasegurados.APaterno, SqlDbType.NVarChar);
            b.AddParameter("@AMaterno", coasegurados.AMaterno, SqlDbType.NVarChar);
            b.AddParameter("@FechaNacimiento", coasegurados.FechaNacimiento, SqlDbType.DateTime);
            b.AddParameter("@IdTipoAsegurado", coasegurados.IdTipoAsegurado, SqlDbType.Int);
            b.AddParameter("@Sexo", coasegurados.Sexo, SqlDbType.Int);
            b.AddParameter("@IdTramite", coasegurados.IdTramite, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int ModificarCoasegurado(int Id, int IdTramite)
        {
            b.ExecuteCommandSP("CoAsegurados_Modificar");
            b.AddParameter("@IdCoAsegurado", Id, SqlDbType.Int);
            b.AddParameter("@IdTramite", IdTramite, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
