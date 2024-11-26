using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion.Captura;
using WFO.Funciones;

namespace WFO.Negocio.Procesos.Operacion.CapturaMasiva
{
    public class Asegurados
    {
        AccesoDatos.Procesos.Operacion.Captura.Asegurados asegurados = new AccesoDatos.Procesos.Operacion.Captura.Asegurados();
        AccesoDatos.Procesos.Operacion.Captura.Tramite_KWIK Tramite_KWIK = new AccesoDatos.Procesos.Operacion.Captura.Tramite_KWIK();

        public prop.Asegurados Consulta_Asegurados_PorRFC(string RFC, int IdTramite)
        {
            return asegurados.Consulta_Asegurados_PorRFC(RFC, IdTramite);
        }

        public prop.Asegurados GM_UMAN_Seleccionar_PorRFC(string RFC, int IdTramite)
        {
            return asegurados.GM_UMAN_Seleccionar_PorRFC(RFC, IdTramite);
        }

        public prop.Asegurados Asegurados_Selecionar_PorIdTramite(int IdTramite)
        {
            return asegurados.Asegurados_Selecionar_PorIdTramite(IdTramite);
        }

        public List<prop.cat_catastroficos> Cat_Catastroficos_Selecionar(string Nombre, string APaterno, string AMaterno, DateTime Fecha)
        {
            return asegurados.Cat_Catastroficos_Selecionar(Nombre, APaterno, AMaterno, Fecha);
        }

        public int Asegurados_CotizacionExamen_Actualizar(int Idtramite, int Examen)
        {
            return asegurados.Asegurados_CotizacionExamen_Actualizar(Idtramite, Examen);
        }

        public int Asegurados_Cotizador_Actualizar(prop.Cotizador cotizador)
        {
            return asegurados.Asegurados_Cotizador_Actualizar(cotizador);
        }

        public int Asegurados_Actualiza_UMAN_Agente(int IdTramite, string RFC, string agente, string UMAN, string FechaFirmaSolicitud, int IdEstado)
        {
            return asegurados.Asegurados_Actualiza_UMAN_Agente(IdTramite, RFC, agente, UMAN, FechaFirmaSolicitud,IdEstado);
        }
        
        public int Asegurados_Actualizar_Datos(int IdTramite, string RFC, string Telefono, string Email, DateTime Fecha)
        {
            return asegurados.Asegurados_Actualizar_Datos(IdTramite, RFC, Telefono, Email, Fecha);
        }

        public int Asegurado_Captura_Registrar(int IdTramite, int IdPlan, int IdDeducible, int IdCausaSeguro, int IdTipoProducto, int IdRegiones)
        {
            return asegurados.Asegurado_Captura_Registrar(IdTramite, IdPlan, IdDeducible, IdCausaSeguro, IdTipoProducto, IdRegiones);
        }

        public int Asegurado_Captura_Actualizar(int IdTramite, int IdPlan, int IdDeducible, int IdCausaSeguro, int IdTipoProducto, int IdRegiones)
        {
            return asegurados.Asegurado_Captura_Actualizar(IdTramite, IdPlan, IdDeducible, IdCausaSeguro, IdTipoProducto, IdRegiones);
        }

        public prop.AseguradoCaptura Asegurado_Captura_Seleccionar(int IdTramite)
        {
            return asegurados.Asegurado_Captura_Seleccionar(IdTramite);
        }

        public prop.Tramite_KWIK Tramite_Consulta_KWIK(int IdTramite)
        {
            return Tramite_KWIK.Tramite_Consulta_KWIK(IdTramite);
        }

        public prop.Asegurados Asegurado_Seleccionar_IdTRamite(int IdTramite)
        {
            return asegurados.Asegurado_Seleccionar_IdTRamite(IdTramite);
        }

        public prop.AseguradoCaptura Asegurado_Captura_Seleccionar_IdTramite(int IdTramite)
        {
            return asegurados.Asegurado_Captura_Seleccionar_IdTramite(IdTramite);
        }
    }
}
