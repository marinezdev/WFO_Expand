using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using prop = WFO.Propiedades.Procesos.Promotoria;
using WFO.Funciones;
using DevExpress.Web;

namespace WFO.Negocio.Procesos.Promotoria
{
    public class Catalogos
    {
        AccesoDatos.Procesos.Promotoria.Catalogos catalogos = new AccesoDatos.Procesos.Promotoria.Catalogos();

        public List<prop.cat_DocRecEmicion> cat_DocRecEmicion_Selecionar_Persona(int IdTipoPersona, int IdTramiteTipo)
        {
            return catalogos.cat_DocRecEmicion_Selecionar_Persona(IdTipoPersona, IdTramiteTipo);
        }
        public List<prop.cat_estados> Cat_Direcciones_Estados()
        {
            return catalogos.Cat_Direcciones_Estados();
        }

        public List<prop.cat_producto> Cat_productos(int TipoTramite)
        {
            return catalogos.Cat_Productos(TipoTramite);
        }

        public List<prop.cat_subproducto> Cat_subproductos(int Id)
        {
            return catalogos.Cat_SubProductos(Id);
        }

        public List<prop.cat_riesgos> Cat_Riesgos()
        {
            return catalogos.Cat_Riesgos();
        }

        public List<prop.cat_moneda> Cat_Monedas()
        {
            return catalogos.Cat_Monedas();
        }
        public List<prop.cat_Instituciones> cat_instituciones()
        {
            return catalogos.cat_instituciones();
        }

        public List<prop.cat_pais> Cat_Paises()
        {
            return catalogos.Cat_Paises();
        }

        public List<prop.promotoria_usuario> Promotoria_Usuarios(int Id)
        {
            return catalogos.Promotoria_Usuarios(Id);
        }

        public List<prop.promotoria_usuario> Promotoria_Seleccionar_PorIdTramite(int Id_Promotoria, int Id_Tramite)
        {
            return catalogos.Promotoria_Seleccionar_PorIdTramite(Id_Promotoria, Id_Tramite);

        }
        public List<prop.agente_promotoria_usuario> Agente_Promotoria_Usuarios(int Id, string Clave)
        {
            return catalogos.agente_Promotoria_Usuarios(Id, Clave);
        }
        public List<prop.cat_pais> cat_Pais_Sancionado(int Id)
        {
            return catalogos.cat_Pais_Sancionado(Id);
        }

        public List<prop.TramiteN1> BustatramiteN1RFC(string RFC)
        {
            return catalogos.BustatramiteN1RFC(RFC);
        }

        public List<prop.cat_moneda> BuscaMonedaId(int Id)
        {
            return catalogos.BuscaMonedaId(Id);
        }
        
        public List<prop.cat_statusTramite> cat_StatusTramites()
        {
            return catalogos.SeleccionaEstatusTramite();
        }

        public void cat_StatusTramites_DropDownList(ref DropDownList dropdownlist)
        {
            Funciones.LlenarControles.LlenarDropDownList<prop.cat_statusTramite>(ref dropdownlist, catalogos.SeleccionaEstatusTramite(), "Nombre", "Id");
        }

        public void cat_StatusTramites_ASPxComboBox(ref ASPxComboBox aSPxComboBox)
        {
            Funciones.LlenarControles.LlenarASPxComboBox<prop.cat_statusTramite>(ref aSPxComboBox, catalogos.SeleccionaEstatusTramite(), "Nombre", "Id");
        }

        public List<prop.cat_tipo_servicio> cat_tipo_servicio_Seleccionar_PorTipo_tramite(int TipoTramite)
        {
            return catalogos.cat_tipo_servicio_Seleccionar_PorTipo_tramite(TipoTramite);
        }

        public List<prop.cat_PromotoriaZonas> cat_PromotoriaZonas_Seleccionar(int idUsuario)
        {
            return catalogos.cat_PromotoriaZonas_Seleccionar(idUsuario);
        }
    }
}
