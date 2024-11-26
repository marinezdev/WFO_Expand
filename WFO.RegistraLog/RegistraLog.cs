using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WFO.RegistraLog
{
    public class RegistraLog
    {
        #region Private Attributes
        private string nombreArchivo = "";
        private string carpeta = "";
        private string ruta = "";
        protected string BRINCO = "\n";
        protected List<string> lErrores = new List<string>();

        #endregion

        #region Propierties
        public string FullPath
        {
            get { return ruta + "/" + carpeta; }
        }
        #endregion

        public RegistraLog(string folder_, string path, string nombreArchivoLog)
        {
            nombreArchivo = nombreArchivoLog;
            carpeta = folder_;
            this.ruta = path;
        }

        public bool Agregar(string sLog)
        {
            try
            {
                LimpiarErrores();

                //verificamos si existe directorio
                if (!CrearDirectorio()) return false;

                string nombreArchivo = ObtenerNombreArchivo();//obtenemos nombre de archivo del día
                string cadena = ""; //obtenemos el contenido del archivo


                cadena += DateTime.Now + " - " + sLog + Environment.NewLine;

                //creamos el archivo y guardamos
                StreamWriter sw = new StreamWriter(FullPath + "/" + nombreArchivo, true);
                sw.Write(cadena);
                sw.Close();

                return true;
            }
            catch (DirectoryNotFoundException ex)
            {
                AgregarError("Directorio invalido: " + FullPath);
                return false;
            }
            catch (FileNotFoundException ex)
            {
                AgregarError("Ruta de archivo invalida");
                return false;
            }
            catch (Exception ex)
            {
                AgregarError("Error: " + ex.Message);
                return false;
            }
        }

        #region Helpers

        private string ObtenerNombreArchivo()
        {
            string nombre = "";
            nombre = nombreArchivo + "_Log_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + ".txt";
            return nombre;
        }

        private bool CrearDirectorio()
        {
            try
            {
                //si no existe el directorio lo creamos
                if (!Directory.Exists(FullPath))
                    Directory.CreateDirectory(FullPath);

                return true;

            }
            catch (DirectoryNotFoundException)
            {
                AgregarError("Directorio invalido: " + FullPath);
                return false;
            }
        }


        public void AgregarError(string error)
        {
            lErrores.Add(error);
        }

        public string ObtenerErrores()
        {
            string error = "";
            foreach (string err in lErrores)
            {
                error += err + BRINCO;
            }
            return error;
        }

        protected void LimpiarErrores()
        {
            lErrores = new List<string>();
        }

        #endregion
    }
}
