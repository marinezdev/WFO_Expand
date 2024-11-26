using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;


namespace WFO.Negocio.Cobranza
{
    public class ListaCobranza
    {
        AccesoDatos.Procesos.ArchivosDependencias ad = new AccesoDatos.Procesos.ArchivosDependencias();

        public void MostrarTramites(string rol, string estado, string cobertura, string usuario, ref Repeater repeater)
        {
            if (rol == "2" && string.IsNullOrEmpty(estado)) //Todos para el supervisor
            {
                repeater.DataSource = ad.Seleccionar();
            }
            else if (rol == "2" && !string.IsNullOrEmpty(estado)) //Por estado para el supervisor
            {
                repeater.DataSource = ad.SeleccionarPorEstadoCobertura(estado, cobertura);
            }

            else if (rol=="6" && string.IsNullOrEmpty(estado)) //Dependencias
            {
                repeater.DataSource = ad.SeleccionarPorDependencia(rol);
            }
            else if (rol == "6" && !string.IsNullOrEmpty(estado)) //Por estado para Dependencia
            {
                repeater.DataSource = ad.SeleccionarPorDependenciaEstadoCobertura(estado, cobertura, usuario);
            }
            
            //else if (!string.IsNullOrEmpty(rd.SeleccionarRolEnDependencia(rol)) && !string.IsNullOrEmpty(estado)) //Todos los del operador/dependencia
            //{
            //    repeater.DataSource = ad.SeleccionarPorDependencia(rd.SeleccionarRolEnDependencia(rol));
            //}
            //else if (!string.IsNullOrEmpty(rd.SeleccionarRolEnDependencia(rol)) && !string.IsNullOrEmpty(estado)) //Por estado para el operador/dependencia
            //{
            //    repeater.DataSource = ad.SeleccionarPorDependenciaEstadoCobertura(estado, cobertura, rd.SeleccionarRolEnDependencia(rol));
            //}
            else if (rol == "3") //Tramites por analista
            {
                repeater.DataSource = ad.SeleccionarTramitesPorUsuario(usuario);
            }
            else if (rol == "3" && !string.IsNullOrEmpty(estado)) //Tramites por estado por analista
            {
                repeater.DataSource = ad.SeleccionarTramitesPorUsuarioPorEstadoPorCobertura(estado, cobertura, usuario);
            }
            repeater.DataBind();
        }


    }
}
