using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Funciones
{
    /// <summary>
    /// Manejo de fechas
    /// </summary>
    public static class Fechas
    {
        public static DateTime ConvertirTextoAFecha(string texto)
        {
            return DateTime.Parse(texto);
        }

        /// <summary>
        /// Convierte una fecha para guardarla en la bd y le agrega la hora
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string PrepararFechaParaAgregar(string fecha)
        {
            return string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Parse(fecha + " " + DateTime.Now.ToLongTimeString()));
        }

        public static string PrepararFechaParaBusqueda(string fecha)
        {
            return string.Format("{0:yyyyMMdd}", DateTime.Parse(fecha));
        }

        public static string GetFormatString(object value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        public static string PrepararFechaInicialParaConsulta(string fecha)
        {
            string strFechaI = fecha + " 00:00:00";
            DateTime dt1 = DateTime.ParseExact(strFechaI, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            return strFechaI = dt1.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public static string PrepararFechaFinalParaConsulta(string fecha)
        {
            string strFechaF = fecha + " 23:59:59";
            DateTime dt1 = DateTime.ParseExact(strFechaF, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            return strFechaF = dt1.ToString("yyyy/MM/dd HH:mm:ss");
        }

    }
}
