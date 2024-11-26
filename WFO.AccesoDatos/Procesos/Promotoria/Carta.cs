using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Promotoria;

namespace WFO.AccesoDatos.Procesos.Promotoria
{
    public class Carta
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.carta> Consulta_Carta(int Id, string Nombre, int Rechazo)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("exec Carta_Datos_PorIdTramite_Estatus");
            System.Diagnostics.Debug.WriteLine("  @Id_Tramite = " + Id + "");
            System.Diagnostics.Debug.WriteLine(", @Nombre = '" + Nombre + "'");
            System.Diagnostics.Debug.WriteLine(", @Rechazo = '" + Rechazo + "'");
#endif

            b.ExecuteCommandSP("Carta_Datos_PorIdTramite_Estatus");
            b.AddParameter("@Id_Tramite", Id, SqlDbType.Int);
            b.AddParameter("@Nombre", Nombre, SqlDbType.NVarChar);
            b.AddParameter("@Rechazo", Rechazo, SqlDbType.Int);
            List<prop.carta> resultado = new List<prop.carta>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.carta item = new prop.carta()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    //FechaTermino = Convert.ToDateTime(reader["FechaTermino"].ToString()),
                    FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"].ToString()),
                    TipoTramite = reader["TipoTramite"].ToString(),
                    Operacion = reader["Operacion"].ToString(),
                    FolioCompuesto = reader["FolioCompuesto"].ToString(),
                    Contratante = reader["Contratante"].ToString(),
                    //Titular = reader["Titular"].ToString(),
                    IdSisLegados = reader["IdSisLegados"].ToString(),
                    kwik = reader["kwik"].ToString(),
                    //Producto = reader["Producto"].ToString(),
                    Agente = reader["Agente"].ToString(),
                    Promotoria = reader["Promotoria"].ToString(),
                    tipoServicio = reader["tipoServicio"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.motivosRechazo> Consulta_Motivos_Rechazo(int Id)
        {
            b.ExecuteCommandSP("Tramite_Rechazos_Seleccionar_PorIdTramite");
            b.AddParameter("@Id_Tramite", Id, SqlDbType.Int);
            List<prop.motivosRechazo> resultado = new List<prop.motivosRechazo>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.motivosRechazo item = new prop.motivosRechazo()
                {
                    MotivoRechazo = reader["RECHAZO"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.bitacora> Consulta_Observaciones_Bitacora(int Id)
        {
            b.ExecuteCommandSP("Tramite_Rechazos_Observaciones_Seleccionar_PorIdTramite");
            b.AddParameter("@Id_Tramite", Id, SqlDbType.Int);
            List<prop.bitacora> resultado = new List<prop.bitacora>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.bitacora item = new prop.bitacora()
                {
                    Observacion = reader["OBSERVACIONES"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.carta> Consulta_Carta_PCI(int Id)
        {
            b.ExecuteCommandSP("Carta_Datos_PorIdTramite_EstatusPCI");
            b.AddParameter("@Id_Tramite", Id, SqlDbType.Int);
            List<prop.carta> resultado = new List<prop.carta>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.carta item = new prop.carta()
                {
                    Id = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    FechaTermino = Convert.ToDateTime(reader["FechaTermino"].ToString()),
                    TipoTramite = reader["TipoTramite"].ToString(),
                    Operacion = reader["Operacion"].ToString(),
                    FolioCompuesto = reader["FolioCompuesto"].ToString(),
                    Contratante = reader["Contratante"].ToString(),
                    //Titular = reader["Titular"].ToString(),
                    IdSisLegados = reader["IdSisLegados"].ToString(),
                    //Producto = reader["Producto"].ToString(),
                    Agente = reader["Agente"].ToString(),
                    Promotoria = reader["Promotoria"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}
