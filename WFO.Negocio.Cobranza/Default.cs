using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Negocio.Cobranza
{
    public class Default
    {
        AccesoDatos.Procesos.ArchivosDependencias ad = new AccesoDatos.Procesos.ArchivosDependencias();
        AccesoDatos.Procesos.ArchivosDependenciasAsignados ada = new AccesoDatos.Procesos.ArchivosDependenciasAsignados();

        public DataTable ValidarRol(string rol, string idusuario, string cobertura)
        {
            DataTable dt = new DataTable();
            switch (rol)
            {
                case "6":   //Dependencias
                    if (cobertura == "1")
                        dt = ad.SeleccionarPorCoberturaBasicaDependencia(idusuario);
                    else if (cobertura == "2")
                        dt = ad.SeleccionarPorCoberturaPotenciadaDependencia(idusuario);
                    break;
                case "2":   //Supervisor (ve todos los tramites)
                    if (cobertura == "1")
                        dt = ad.SeleccionarPorCoberturaBasicaSupervisor();
                    else if (cobertura == "2")
                        dt = ad.SeleccionarPorCoberturaPotenciadaSupervisor();
                        break;
                case "3":   //Analista básica
                    if (cobertura == "1")
                        dt = ad.SeleccionarPorCoberturaBasicaUsuario(idusuario);
                    else if (cobertura == "2")
                        dt = ad.SeleccionarPorCoberturaPotenciadaUsuario(idusuario);
                    break;
                case "7":  //Analista back básica
                    if (cobertura == "1")
                        dt = ad.SeleccionarPorCoberturaBasicaUsuario(idusuario);
                    else if (cobertura == "2")
                        dt = ad.SeleccionarPorCoberturaPotenciadaUsuario(idusuario);
                    break;
                case "50":   //Analista potenciación
                    break;
                case "60":   //Analista back potenciación
                    break;  
                case "70":   //analista front potenciación
                    break;
            }
            return dt;
        }

        public bool ValidarAsignacionAnalista(string cobertura, string idusuario, string idrol, ref string cadena)
        {
            bool volver = false;
            if (idrol == "3") //sólo analistas
            {
                //Obtener el primer tramite disponible para asignarselo
                DataTable dt = new DataTable();
                dt = ad.SeleccionarPrimerTramiteDisponibleParaAnalista(cobertura);
                if (dt.Rows.Count == 0)
                {
                    if (ad.SeleccionarTramiteAsignadoUsuario(idusuario) != "")
                    {
                        string folioAsignado = ad.SeleccionarTramiteYaAsignadoAAnalista(idusuario, "6");
                        if (folioAsignado != "")
                        {
                            cadena = "folio=" + folioAsignado + "&cobertura=&estado=";
                            volver = true;
                        }
                        else
                            volver = false;
                    }
                    else
                    {
                        dt = ad.SeleccionarTramiteParaTerminarAsignadoAUsuarioBasica(idusuario);
                        if (dt.Rows.Count > 0)
                        {
                            cadena = "folio=" + dt.Rows[0][0].ToString() + "&cobertura=" + dt.Rows[0][1].ToString() + "&usuario=" + dt.Rows[0][2].ToString() + "&estado=" + dt.Rows[0][3].ToString();
                            volver = true;
                        }
                        else
                        {
                            dt = null;
                            dt = ad.SeleccionarTramiteParaTerminarAsignadoAUsuarioPotenciacion(idusuario);
                            cadena = "folio=" + dt.Rows[0][0].ToString() + "&cobertura=" + dt.Rows[0][1].ToString() + "&usuario=" + dt.Rows[0][2].ToString() + "&estado=" + dt.Rows[0][3].ToString();
                            volver = true;
                        }
                    }
                }
                else
                {
                    ad.AgregarUsuarioAsignadoATramite(idusuario, dt.Rows[0]["Folio"].ToString());
                    ada.Agregar(idusuario, dt.Rows[0]["Folio"].ToString());
                    cadena = "folio=" + dt.Rows[0][0].ToString() + "&cobertura=" + dt.Rows[0][1].ToString() +  "&estado=" + dt.Rows[0][2].ToString();
                    volver = true;
                }
                return volver;
            }
            else
                return false;
        }

        public bool ValidarAsignacionAnalistaBackBasicaMetLife(string idusuario, string idrol, ref string cadena)
        {
            bool estado = false;
            if (idrol == "7")
            {
                //Obtener el primer tramite disponible para asignarselo
                DataTable dt = new DataTable();
                dt = ad.SeleccionarPrimerTramiteDisponibleParaEjecutivoBasica();
                if (dt.Rows.Count > 0)
                {
                    ada.Agregar(idusuario, dt.Rows[0][0].ToString());
                    cadena = "folio=" + dt.Rows[0][0].ToString() + "&estado=" + dt.Rows[0][1].ToString();
                    estado = true;
                }
            }
            return estado;
        }

        public bool ValidarAsignacionAnalistaBackPotenciacionMetlife(ref string cadena)
        {
            //Obtener el primer tramite disponible para continuarlo, analista back Metlife
            DataTable dt = new DataTable();
            bool devolver = false;
            dt = ad.SeleccionarPrimerTramiteDisponibleParaAnalistaBackPotenciacion();
            if (dt.Rows.Count > 0)
            {
                devolver = true;
                cadena = "folio=" + dt.Rows[0][0].ToString() + "&estado=" + dt.Rows[0][1].ToString();
            }
            return devolver;
        }

        public bool ValidarAsignacionAnalistaFrontPotenciacionMetLife(ref string cadena)
        {
            //Obtener el primer tramite disponible para continuarlo, analista front metlife
            DataTable dt = new DataTable();
            bool devolver = false;
            dt = ad.SeleccionarPrimerTramiteDisponibleParaAnalistaFrontPotenciacion();
            if (dt.Rows.Count > 0)
            {
                devolver = true;
                cadena = "folio=" + dt.Rows[0][0].ToString() + "&estado=" + dt.Rows[0][1].ToString();
            }
            return devolver;
        }

    }
}
