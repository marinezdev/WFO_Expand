using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Propiedades.Procesos.Promotoria
{
    public class directorio
    {
        private string mAbuelo = String.Empty;
        public string abuelo { get { return mAbuelo; } set { mAbuelo = value; } }
        private string mPadre = String.Empty;
        public string padre { get { return mPadre; } set { mPadre = value; } }
        private string mHijo = String.Empty;
        public string hijo { get { return mHijo; } set { mHijo = value; } }
    }
}
