using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion.Captura;
using WFO.Funciones;


namespace WFO.Negocio.Procesos.Operacion.CapturaMasiva
{
    public class Asegurado_direciones
    {
        AccesoDatos.Procesos.Operacion.Captura.Asegurado_direciones direciones = new AccesoDatos.Procesos.Operacion.Captura.Asegurado_direciones();

        public int Asegurado_Agregar_Direccion(prop.Asegurado_direciones asegurado_Direciones)
        {
            return direciones.Asegurado_Agregar_Direccion(asegurado_Direciones);
        }

        public List<prop.Asegurado_direciones> Asegurado_Direcion_Selecionar_PorIdTramite(int IdTramite)
        {
            return direciones.Asegurado_Direcion_Selecionar_PorIdTramite(IdTramite);
        }

        public int Asegurado_Direcion_Desactivar_PorIdAseguradoDireccion(int Id, int IdTramite)
        {
            return direciones.Asegurado_Direcion_Desactivar_PorIdAseguradoDireccion(Id, IdTramite);
        }
    }
}
