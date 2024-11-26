using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Funciones
{
    public static class Numeros
    {
        public static int ConvertirTextoANumeroEntero(string texto)
        {
            return int.Parse(texto);
        }

        public static decimal ConvertirTextoANumeroDecimal(string texto)
        {
            return decimal.Parse(texto);
        }
    }
}
