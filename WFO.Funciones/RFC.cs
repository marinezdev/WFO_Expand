using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RFC;

namespace WFO.Funciones
{
    public static class RFC
    {
        public static string ValidaContinuidadRFC(string TipoPersona, string RFC)
        {
            string resultado = "";
            if (RFC != "" && RFC != null)
            {
                switch (TipoPersona)
                {
                    case "Fisica":
                        if (RFC.Length == 13)
                        {
                            Regex Val = new Regex(@"[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?");
                            if (Val.IsMatch(RFC))
                            {
                                resultado = "ok";
                            }
                            else
                            {
                                resultado = "RFC Persona Física Inválido ";
                            }
                        }
                        else
                        {
                            resultado = "El RFC No Contiene 13 Caracteres ";
                        }
                        break;
                    case "Moral":
                        if (RFC.Length == 12)
                        {
                            Regex Val = new Regex(@"^[a-zA-Z]{3,4}(\d{6})((\D|\d){3})?$");
                            if (Val.IsMatch(RFC))
                            {
                                resultado = "ok";
                            }
                            else
                            {
                                resultado = "RFC Persona Moral Inválido ";
                            }
                        }
                        else
                        {
                            resultado = "El RFC No Contiene 12 Caracteres ";
                        }
                        break;
                    default:
                        resultado = "Coloca el RFC";
                        break;
                }
            }
            else
            {
                resultado = "Coloca el RFC";
            }

            return resultado;
        }

        public static string CalcularRFCFisico(string FechaNacimiento, string Nombre, string ApellidoPaterno, string ApellidoMaterno)
        {
            string RFC = "";
            string strValMsj = "";
            DateTime dtValor = DateTime.Today;
            if (FechaNacimiento.Length > 0)
            {
                if (IsDate(FechaNacimiento.Trim(), ref strValMsj, ref dtValor))
                {
                    try
                    {
                        string strNombre = "";
                        strNombre = removerAcentos(Nombre.Trim());

                        if (strNombre.Equals("MARIA") || strNombre.Equals("JOSE"))
                        {
                            strNombre += "A";
                        }
                        ObtieneRFC rfc = new ObtieneRFC();
                        RFC = rfc.RFC13Pocisiones(removerAcentos(ApellidoPaterno.Trim()), removerAcentos(ApellidoMaterno.Trim()), removerAcentos(strNombre), dtValor.ToString("yy/MM/dd"));
                    }
                    catch
                    {
                        RFC = "ERR000000AAA";
                    }
                }
                else
                {
                    RFC = "ERROR FECHA";
                }
            }
            return RFC;
        }

        public static string CalcularRFCMoral(string FechaConstitucion, string NombreMoral)
        {
            string RFC = "";
            string strValMsj = "";
            DateTime dtValor = DateTime.Today;
            if (FechaConstitucion.Length > 0)
            {
                if (IsDate(FechaConstitucion.Trim(), ref strValMsj, ref dtValor))
                {
                    try
                    {
                        string strMoral = removerAcentos(NombreMoral.Trim());
                        String[] arrPalabrasNo = { " EL ", " S DE RL ", " DE ", " LAS ", " DEL ", " COMPAÑÍA ", " SOCIEDAD ", " COOPERATIVA ", " S EN C POR A ", " S EN NC ", " PARA ", " POR ", " AL ", " E ", " SCL ", " SNC ", " OF ", " COMPANY ", " MC ", " VON ", " MI ", " SRL CV ", " SA MI ", " LA ", " SA DE CV ", " LOS ", " Y ", " SA ", " CIA ", " SOC ", " COOP ", " A EN P ", " S EN C ", " EN ", " CON ", " SUS ", " SC ", " SCS ", " THE ", " AND ", " CO ", " MAC ", " VAN ", " A ", " SA DE CV ", " COMPAÑÍA ", " COMPANÍA ", " DE ", " LA ", " LAS ", " MC ", " VON ", " DEL ", " LOS ", " Y ", " MAC ", " VAN ", " MI ", " SRL CV MI ", " SRL MI" };
                        foreach (string strPalabra in arrPalabrasNo)
                        {
                            strMoral = strMoral.Replace(strPalabra, " ");
                        }

                        String[] arrPalabras = strMoral.Split(' ');
                        if (arrPalabras.Length > 3)
                        {
                            strMoral = "";
                            strMoral += arrPalabras[0].ToString() + " ";
                            strMoral += arrPalabras[1].ToString() + " ";
                            strMoral += arrPalabras[2].ToString() + " ";
                        }

                        PersonaMoral moral = new PersonaMoral();
                        RFC = moral.RetornaLetrasFinalesRFC(strMoral, dtValor.ToString("yy/MM/dd"));
                    }
                    catch
                    {
                        RFC = "ERR000000AAA";
                    }
                }
                else
                {
                    RFC = "ERROR FECHA";
                }
            }

            return RFC;
        }

        public static bool IsDate(string inputDate, ref string strResultado, ref DateTime FechaValue)
        {
            bool isDate = true;
            try
            {
                FechaValue = DateTime.ParseExact(inputDate, "dd/MM/yyyy", null);

                if (FechaValue > DateTime.Today)
                {
                    strResultado = "La fecha no puede ser mayor al día de hoy.";
                    isDate = false;
                }
                else if (FechaValue < FechaValue.AddDays(-60))
                {
                    strResultado = "La fecha no puede ser menor a 60 días a partir del día de hoy.";
                    isDate = false;
                }
                else
                {
                    isDate = true;
                }

            }
            catch (Exception ex)
            {
                isDate = false;
                strResultado = ex.Message;
                Console.Write("Error al Validar la Fecha: " + ex.Message);
            }
            return isDate;
        }

        private static string removerAcentos(String texto)
        {
            string consignos = "áàäéèëíìïóòöúùuÁÀÄÉÈËÍÌÏÓÒÖÚÙÜçÇñÑ";
            string sinsignos = "aaaeeeiiiooouuuAAAEEEIIIOOOUUUcCnN";

            StringBuilder textoSinAcentos = new StringBuilder(texto.Length);
            int indexConAcento;
            foreach (char caracter in texto)
            {
                indexConAcento = consignos.IndexOf(caracter);
                if (indexConAcento > -1)
                    textoSinAcentos.Append(sinsignos.Substring(indexConAcento, 1));
                else
                    textoSinAcentos.Append(caracter);
            }
            return textoSinAcentos.ToString();
        }
    }
}
