using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.Funciones
{
    public class VariablesGlobales
    {
        public enum StatusTramite
        {
            Registro = 1,
	        Proceso = 2,
	        PCI = 3,
	        Hold = 4,
	        Suspendido = 5, 
	        Ejecucion = 6,
	        Rechazo = 7,
	        Cancelado = 8,
	        Caducado = 9,
	        RevPromotoria = 10,
	        PromReconsidera = 11,
	        PromCancela = 12
        }

        public enum StatusMesa
        {
            SeleccionCompleta= -200,
            Registro = 1,
            Atrapado = 2,
            PCI = 3,
            RevisionPCI = 4,
            ReingresoPCI = 5,
            Hold = 6,
            RevisionHold = 7,
            ReingresoHold = 8,
            Suspendido = 9,
            RevisionSuspencion = 10,
            ReingresoSuspencion = 11,
            SolicitudApoyo = 12,
            ReingresoApoyo = 13,
            Procesable = 14,
            NoProcesable = 15,
            Pausa = 16,
            Procesado = 17,
            Rechazo = 18,
            Cancelado = 19,
            Caducado = 20,
            RevPromotoría = 21,
            RevPromotoriaKO = 22,
            RevPromotoriaOK = 23,
            PromoCancela = 24,
            EnviaMesa = 25,
            RespuestaMesa = 26,
            SistemaAutomatico = 27,
            ContactoEfectivo = 28,
            ContactoNoEfectivo = 29
        }
    }
}
