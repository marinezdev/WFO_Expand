using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion.Captura;
using WFO.Funciones;

namespace WFO.Negocio.Procesos.Operacion.CapturaMasiva
{
    public class CoAsegurados
    {
        AccesoDatos.Procesos.Operacion.Captura.CoAsegurados coAsegurados = new AccesoDatos.Procesos.Operacion.Captura.CoAsegurados();
        
        public List<prop.Coasegurados> CoAsegurados_Selecionar_PorIdTramite_RFC(int IdTramite, string RFC)
        {
            return coAsegurados.CoAsegurados_Selecionar_PorIdTramite_RFC(IdTramite, RFC);
        }

        public prop.Coasegurados CoAsegurado_Seleccionar_PorID(int Id, int IdTramite)
        {
            return coAsegurados.CoAsegurado_Seleccionar_PorID(Id, IdTramite);
        }

        public int AgregarCoasegurado(prop.Coasegurados coasegurados)
        {
            return coAsegurados.AgregarCoasegurado(coasegurados);
        }

        public int ActualizarCoasegurado(prop.Coasegurados coasegurados)
        {
            return coAsegurados.ActualizarCoasegurado(coasegurados);
        }

        public int ActualizarCoaseguradoMesaCaptura(prop.Coasegurados coasegurados)
        {
            return coAsegurados.ActualizarCoaseguradoMesaCaptura(coasegurados);
        }

        public int ModificarCoasegurado(int Id, int IdTramite)
        {
            return coAsegurados.ModificarCoasegurado(Id, IdTramite);
        }
    }
}
