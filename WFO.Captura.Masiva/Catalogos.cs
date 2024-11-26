using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion.Captura;
using WFO.Funciones;

namespace WFO.Negocio.Procesos.Operacion.CapturaMasiva
{
    public class Catalogos
    {
        AccesoDatos.Procesos.Operacion.Captura.Catalogos catalogos = new AccesoDatos.Procesos.Operacion.Captura.Catalogos();

        public List<prop.cat_bancos> Cat_Bancos(int IdModoPago)
        {
            return catalogos.Cat_Bancos(IdModoPago);
        }

        public List<prop.cat_modo_pago> Cat_Modo_Pago()
        {
            return catalogos.Cat_Modo_Pago();
        }

        public List<prop.cat_periodicidad> Cat_Periodicidad()
        {
            return catalogos.Cat_Periodicidad();
        }

        public prop.cat_direcciones Cat_CP_Selecionar_PorCP(string CP)
        {
            return catalogos.Cat_CP_Selecionar_PorCP(CP);
        }
        
        public List<prop.cat_estados> Cat_Direcciones_Estados()
        {
            return catalogos.Cat_Direcciones_Estados();
        }
        
        public List<prop.cat_poblaciones> Cat_Direcciones_Poblacion(int IdEstado)
        {
            return catalogos.Cat_Direcciones_Poblacion(IdEstado);
        }

        public List<prop.cat_cp> Cat_Direcciones_CP_Selecionar_PorIdPoblacion(int IdPoblacion)
        {
            return catalogos.Cat_Direcciones_CP_Selecionar_PorIdPoblacion(IdPoblacion);
        }

        public List<prop.cat_colonia> Cat_Direcion_Colonia_PorIdCP(int IdCP)
        {
            return catalogos.Cat_Direcion_Colonia_PorIdCP(IdCP);
        }
        
        public List<prop.cat_TipoAsegurados> Cat_TipoAsegurados_Seleccionar()
        {
            return catalogos.Cat_TipoAsegurados_Seleccionar();
        }

        public List<prop.cat_padecimiento> Cat_Padecimientos()
        {
            return catalogos.Cat_Padecimiento();
        }

        public List<prop.cat_PlanMedicalife> Cat_PlanMedicalife_Seleccionar()
        {
            return catalogos.Cat_PlanMedicalife_Seleccionar();
        }

        public List<prop.cat_deducible> Cat_Deducible_Seleccionar()
        {
            return catalogos.Cat_Deducible_Seleccionar();
        }

        public List<prop.cat_causa_seguro> Cat_Causa_Seguro_Seleccionar()
        {
            return catalogos.Cat_Causa_Seguro_Seleccionar();
        }

        public List<prop.cat_regiones> Cat_Region_Seleccionar()
        {
            return catalogos.Cat_Region_Seleccionar();
        }

        public List<prop.cat_tipo_producto> Cat_Tipo_Producto_Seleccionar()
        {
            return catalogos.Cat_Tipo_Producto_Seleccionar();
        }
        
        public int AgregarPoblacion(int IdEstado, string Poblacion)
        {
            return catalogos.AgregarPoblacion(IdEstado, Poblacion);
        }

        public int AgregarCP(int IdPoblacion, string CP)
        {
            return catalogos.AgregarCP(IdPoblacion, CP);
        }

        public int AgregarColonia(int IdCP, string Colonia)
        {
            return catalogos.AgregarColonia(IdCP, Colonia);
        }
    }
}
