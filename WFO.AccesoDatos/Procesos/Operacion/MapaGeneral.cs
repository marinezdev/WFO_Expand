using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.AccesoDatos.Procesos.Operacion
{
    public class MapaGeneral
    {
        ManejoDatos b = new ManejoDatos();

        public List<prop.MapaGeneral> getDashboard(int IdFlujo)
        {
            b.ExecuteCommandSP("spWFOMapaGeneral");
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            List<prop.MapaGeneral> resultado = new List<prop.MapaGeneral>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.MapaGeneral item = new prop.MapaGeneral()
                {
                    IdMesa = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Mesa = reader["Nombre"].ToString(),
                    Icono = reader["Icono"].ToString(),
                    Color = reader["Color"].ToString(),
                    UsuariosConectados = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["UsuariosDisponibles"].ToString()),
                    TramitesDisponibles = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["TramitesDisponibles"].ToString()),
                    TramitesReingresos = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["TramitesReingresos"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.MapaGeneral> getDashboardMesa(int IdFlujo, int IdMesa)
        {
            b.ExecuteCommandSP("spWFOMapaGeneralMesa");
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            b.AddParameter("@IdMesa", IdMesa, SqlDbType.Int);
            List<prop.MapaGeneral> resultado = new List<prop.MapaGeneral>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.MapaGeneral item = new prop.MapaGeneral()
                {
                    IdMesa = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Mesa = reader["Nombre"].ToString(),
                    Icono = reader["Icono"].ToString(),
                    UsuariosConectados = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["UsuariosDisponibles"].ToString()),
                    TramitesDisponibles = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["TramitesDisponibles"].ToString()),
                    TramitesReingresos = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["TramitesReingresos"].ToString()),
                    TotalTramites = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["TramitesDisponibles"].ToString()) + Funciones.Numeros.ConvertirTextoANumeroEntero(reader["TramitesReingresos"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.MapaGeneral> getDashboardMesaDetalle(int IdFlujo, int IdMesa)
        {
            b.ExecuteCommandSP("spWFOMapaGeneralMesa");
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            b.AddParameter("@IdMesa", IdMesa, SqlDbType.Int);
            List<prop.MapaGeneral> resultado = new List<prop.MapaGeneral>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.MapaGeneral item = new prop.MapaGeneral()
                {
                    IdMesa = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Id"].ToString()),
                    Mesa = reader["Nombre"].ToString(),
                    Icono = reader["Icono"].ToString(),
                    UsuariosConectados = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["UsuariosDisponibles"].ToString()),
                    TramitesDisponibles = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["TramitesDisponibles"].ToString()),
                    TramitesReingresos = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["TramitesReingresos"].ToString()),
                    TotalTramites = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["TramitesDisponibles"].ToString()) + Funciones.Numeros.ConvertirTextoANumeroEntero(reader["TramitesReingresos"].ToString())
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public List<prop.MapaGeneralMesaDetalleTramite> getDashboardMesaDetalleTramite(int IdFlujo, int IdMesa)
        {
            b.ExecuteCommandSP("spWFOMapaGeneralMesaTramite");
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            b.AddParameter("@IdMesa", IdMesa, SqlDbType.Int);
            List<prop.MapaGeneralMesaDetalleTramite> resultado = new List<prop.MapaGeneralMesaDetalleTramite>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                prop.MapaGeneralMesaDetalleTramite item = new prop.MapaGeneralMesaDetalleTramite()
                {
                    IdFlujo = IdFlujo,
                    IdMesa = IdMesa,
                    IdTramite = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdTramite"].ToString()),
                    IdTramiteMesa = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["IdTramiteMesa"].ToString()),
                    Folio = reader["Folio"].ToString(),
                    Reingresos = Funciones.Numeros.ConvertirTextoANumeroEntero(reader["Reingresos"].ToString()),
                    Registro = Convert.ToDateTime(reader["Registro"].ToString()),
                    Usuario = reader["Usuario"].ToString(),
                    TiempoAtencion = reader["TiempoAtencion"].ToString(),
                    TiempoMesa = reader["TiempoMesa"].ToString(),
                    Contratante = reader["Contratante"].ToString(),
                    Titular = reader["Titular"].ToString(),
                    StatusMesa = reader["StatusMesa"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}