using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prop = WFO.Propiedades.Procesos.Operacion;

namespace WFO.Negocio.Procesos.Operacion
{
    public class Tramites
    {
        AccesoDatos.Procesos.Operacion.Tramites tramites = new AccesoDatos.Procesos.Operacion.Tramites();

        public List<prop.Tramites> TramiteOperadorSelecionar(int Id, DateTime Fecha_Inicio, DateTime Fecha_Termino, string Folio, string RFC, string Nombre, string ApPaterno, string ApMaterno)
        {
            return tramites.TramiteOperadorSelecionar(Id, Fecha_Inicio, Fecha_Termino,Folio, RFC, Nombre, ApPaterno, ApMaterno);
        }

        public List<prop.Tramites> TramiteOperadorSelecionarBusqueda(int Id, string Folio, string RFC, string Nombre, string ApPaterno, string ApMaterno)
        {
            return tramites.TramiteOperadorSelecionarBusqueda(Id, Folio, RFC, Nombre, ApPaterno, ApMaterno);
        }
        public prop.Tramites Tramite_Selecionar_Id(int Id)
        {
            return tramites.Tramite_Selecionar_Id(Id);
        }
    }
}
