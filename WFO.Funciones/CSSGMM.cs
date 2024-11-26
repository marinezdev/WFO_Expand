using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion.Captura;
namespace WFO.Funciones
{
    public class CSSGMM
    {
        public static string Cotizador(prop.Cotizador cotizador)
        {
            string respuesta = "";

            // TOMA CINCO CRITERIOS DE EVALUACION, EN EL EXCEL MUESTRA 6, UN CRITEIRO NO LO TOMAREMOS - CRITERIO AD6
            // CRITEIRO 1 - EVALUACION DE LAS 2 PRIMERAS PREGUNTAS,
            string Evaluacion1 = validacion1(cotizador.pregunta1, cotizador.pregunta2);
            // CRITERIO 2 EVALUACION DE ALTURA Y PESO
            string Evaluacion2 = validacion2(cotizador.estatura, cotizador.peso, cotizador.edad);
            // CRITERIO 3 EVALUACION PREGUNTA 3
            string Evaluacion3 = validacion3(cotizador.pregunta3, cotizador.IdPadecimiento);


            if (Evaluacion1== "ACC" & Evaluacion2 == "ACC" & Evaluacion3 == "ACC")
            {
                respuesta = "ACEPTADO";
            }
            else
            {
                respuesta = "DECLINADO";
            }

            return respuesta;
        }

        public static string validacion1(int p1, int p2)
        {
            string respuesta = "";
            if (p1 == 1 || p2 == 1)
            {
                respuesta = "DCL";
            }
            else
            {
                respuesta = "ACC";
            }
            return respuesta;
        }

        public static string validacion2(double estatura, double peso, int edad)
        {
            string respuesta = "";
            string EvaluacionEdad = "";

            double IMC = peso/(estatura * estatura);

            // EVALUA LA EDAD DEL CONTRATANTE
            switch (edad)
            {
                case int n when (n > 15 & n<30):
                    EvaluacionEdad = "1";
                    break;
                case int n when (n > 29 & n < 46):
                    EvaluacionEdad = "2";
                    break;
                case int n when (n > 45 & n < 60):
                    EvaluacionEdad = "3";
                    break;
                case int n when (n > 59 & n < 101):
                    EvaluacionEdad = "4";
                    break;
                default:
                    EvaluacionEdad = "ACC";
                    break;
            }

            if (EvaluacionEdad == "ACC")
            {
                respuesta = "ACC";
            }
            else
            {
                int EdadEvaluacion = Convert.ToInt32(EvaluacionEdad);
                respuesta = "DCL";

                switch (EdadEvaluacion)
                {
                    case 1:
                        if (IMC>15.5 & IMC<33.6)
                        {
                            respuesta = "ACC";
                        }
                        break;
                    case 2:
                        if (IMC > 17.5 & IMC < 35.6)
                        {
                            respuesta = "ACC";
                        }
                        break;
                    case 3:
                        if (IMC > 18.5 & IMC < 35.6)
                        {
                            respuesta = "ACC";
                        }
                        break;
                    case 4:
                        if (IMC > 17.5 & IMC < 38.6)
                        {
                            respuesta = "ACC";
                        }
                        break;
                    default:
                        respuesta = "DCL";
                        break;
                }
            }
            return respuesta;
        }

        public static string validacion3(int p3, int IdPadecimiento)
        {
            string respuesta = "";

            if (p3 == 1)
            {
                respuesta = "DCL";
            }
            else
            {
                respuesta = "ACC";
            }

            return respuesta;
        }
    }
}
