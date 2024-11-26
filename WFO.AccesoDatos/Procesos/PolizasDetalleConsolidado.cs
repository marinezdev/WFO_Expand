using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class PolizasDetalleConsolidado
    {
        ManejoDatos b = new ManejoDatos();

        public DataTable Seleccionar()
        {
            string consulta = "DELETE FROM PolizasDetalleConsolidado; " +
                "INSERT INTO PolizasDetalleConsolidado(Dependencia, Poliza, Titulares_2TB, Conyuge_2TB, Hijos_2TB, Ascendientes_2TB, C333_2TB, C295_2TB, C259_2TB, C222_2TB, C185_2TB, C148_2TB, C111_2TB, C74_2TB, Otras_2TB, BasicaMonto_2TB, BasicaAjuste_2TB) " +
                "SELECT Dependencia, Poliza, " +
                "SUM(CASE tasegurado WHEN 'T'  THEN 1 ELSE 0 end) AS [Titulares(2T)], " +
                "SUM(CASE tasegurado WHEN 'C' THEN 1 ELSE 0 end) AS [Conyuges(2T)],  " +
                "SUM(CASE tasegurado WHEN 'H' THEN 1 ELSE 0 end) AS [Hijos(2T)], " +
                "SUM(CASE tasegurado WHEN 'A' THEN 1 ELSE 0 end) AS [Ascendientes(2T)], " +
                "SUM(CASE SABasica WHEN '333' THEN 1 ELSE 0 end) AS [333(2T)], " +
                "SUM(CASE SABasica WHEN '295' THEN 1 ELSE 0 end) AS [295(2T)], " +
                "SUM(CASE SABasica WHEN '259' THEN 1 ELSE 0 end) AS [259(2T)], " +
                "SUM(CASE SABasica WHEN '222' THEN 1 ELSE 0 end) AS [222(2T)], " +
                "SUM(CASE SABasica WHEN '185' THEN 1 ELSE 0 end) AS [185(2T)], " +
                "SUM(CASE SABasica WHEN '148' THEN 1 ELSE 0 end) AS [148(2T)], " +
                "SUM(CASE SABasica WHEN '111' THEN 1 ELSE 0 end) AS [111(2T)], " +
                "SUM(CASE SABasica WHEN '74' THEN 1 ELSE 0 end) AS [74(2T)], " +
                "SUM(CASE when SABasica <> '74' AND SABasica <> '111' AND SABasica <> '148' AND SABasica <> '185' AND SABasica <> '222' AND SABasica <> '259' AND SABasica <> '295' AND SABasica <> '333' THEN 1 ELSE 0 END) AS [Otras SA's(2T)], " +
                "SUM(convert(decimal,PBTReportar)) AS BasicaMonto, " +
                "SUM(convert(decimal,MAPBasica)) AS [PrimaAjustes(2T)] " +
                "FROM polizasdetalle26_30 " +
                //"--WHERE Trimestre = 2 " +
                "GROUP BY Dependencia, Poliza " +
                "CREATE TABLE #DetallePolizaTemporal ( " +
                "	poliza nvarchar(10), " +
                "	titulares int, " +
                "	conyuges int,  " +
                "	hijos int, " +
                "	ascendientes int, " +
                "	c74 int, " +
                "	c111 int, " +
                "	c148 int, " +
                "	c185 int, " +
                "	c222 int, " +
                "	c259 int, " +
                "	c295 int, " +
                "	c333 int, " +
                "	c444 int, " +
                "	c592 int, " +
                "	c740 int, " +
                "	c850 int, " +
                "	c1000 int, " +
                "	c34219 int, " +
                "	otras int,  " +
                "	primapotenciada int, " +
                "	primaajustes int " +
                ") " +
                "UPDATE PolizasDetalle26_30 SET PrimaPotenciadaQR=NULL WHERE IsNull(PrimaPotenciadaQR, '')='' " +
                "UPDATE PolizasDetalle26_30 SET MontoAjustePrimaP=NULL WHERE IsNull(MontoAjustePrimaP, '')='' " +
                "INSERT INTO #DetallePolizaTemporal " +
                "SELECT Poliza, " +
                "SUM(CASE tasegurado WHEN 'T' THEN 1 ELSE 0 END) AS [Titulares(2T)], " +
                "SUM(CASE tasegurado WHEN 'C' THEN 1 ELSE 0 END) AS [Conyuges(2T)],  " +
                "SUM(CASE tasegurado WHEN 'H' THEN 1 ELSE 0 END) AS [Hijos(2T)], " +
                "SUM(CASE tasegurado WHEN 'A' THEN 1 ELSE 0 END) AS [Ascendientes(2T)], " +
                "SUM(CASE SAT WHEN '74' THEN 1 ELSE 0 END) AS [74(2T)], " +
                "SUM(CASE SAT WHEN '111' THEN 1 ELSE 0 END) AS [111(2T)], " +
                "SUM(CASE SAT WHEN '148' THEN 1 ELSE 0 END) AS [148(2T)], " +
                "SUM(CASE SAT WHEN '185' THEN 1 ELSE 0 END) AS [185(2T)], " +
                "SUM(CASE SAT WHEN '222' THEN 1 ELSE 0 END) AS [222(2T)], " +
                "SUM(CASE SAT WHEN '259' THEN 1 ELSE 0 END) AS [259(2T)], " +
                "SUM(CASE SAT WHEN '295' THEN 1 ELSE 0 END) AS [295(2T)], " +
                "SUM(CASE SAT WHEN '333' THEN 1 ELSE 0 END) AS [333(2T)], " +
                "SUM(CASE SAT WHEN '444' THEN 1 ELSE 0 END) AS [444(2T)], " +
                "SUM(CASE SAT WHEN '592' THEN 1 ELSE 0 END) AS [592(2T)], " +
                "SUM(CASE SAT WHEN '740' THEN 1 ELSE 0 END) AS [740(2T)], " +
                "SUM(CASE SAT WHEN '850' THEN 1 ELSE 0 END) AS [850(2T)], " +
                "SUM(CASE SAT WHEN '1000' THEN 1 ELSE 0 END) AS [1000(2T)], " +
                "SUM(CASE SAT WHEN '34219' THEN 1 ELSE 0 END) AS [34219(2T)], " +
                "SUM(CASE WHEN SAT <> '74' AND SAT <> '111' AND SAT <> '148' AND SAT <> '185' AND SAT <> '222' AND SAT <> '259' AND SAT <> '295' AND SAT <> '333' AND SAT <> '444' AND SAT <> '592'  AND SAT <> '740' AND SAT <> '850' AND SAT <> '1000' AND SAT <> '34219' THEN 1 ELSE 0 END) AS [Otras SA's(2T)], " +
                "SUM(CONVERT(DECIMAL(8,2), ISNULL(primapotenciadaqr, 0))), " +
                "SUM(CONVERT(DECIMAL(8,2), ISNULL(MontoAjustePrimaP, 0))) " +
                "FROM polizasdetalle26_30 " +
                "WHERE quincena=7 OR Quincena=8 OR quincena=9 OR quincena=10 OR quincena=11 OR quincena=12 " +
                "GROUP BY Dependencia, Poliza " +
                "UPDATE PolizasDetalleConsolidado " +
                "SET polizasdetalleconsolidado.titulares_2tp=titulares, " +
                "    polizasdetalleconsolidado.conyuge_2tp=conyuges, " +
                "    polizasdetalleconsolidado.hijos_2tp=hijos, " +
                "    polizasdetalleconsolidado.ascendientes_2tp=ascendientes, " +
                "    polizasdetalleconsolidado.c74_2tp=c74, " +
                "    polizasdetalleconsolidado.c111_2tp=c111, " +
                "    polizasdetalleconsolidado.c148_2tp=c148, " +
                "    polizasdetalleconsolidado.c185_2tp=c185, " +
                "    polizasdetalleconsolidado.c222_2tp=c222, " +
                "    polizasdetalleconsolidado.c259_2tp=c259, " +
                "    polizasdetalleconsolidado.c295_2tp=c295, " +
                "    polizasdetalleconsolidado.c333_2tp=c333, " +
                "    polizasdetalleconsolidado.c444_2tp=c444, " +
                "    polizasdetalleconsolidado.c592_2tp=c592, " +
                "    polizasdetalleconsolidado.c740_2tp=c740, " +
                "    polizasdetalleconsolidado.c850_2tp=c850, " +
                "    polizasdetalleconsolidado.c1000_2tp=c1000, " +
                "    polizasdetalleconsolidado.c34219_2tp=c34219, " +
                "    polizasdetalleconsolidado.otras_2tp=otras, " +
                "    polizasdetalleconsolidado.potenciadamonto_2tp=primapotenciada, " +
                "    polizasdetalleconsolidado.potenciadaajuste_2tp=primaajustes " +
                "FROM polizasdetalleconsolidado " +
                "    INNER JOIN #DetallePolizaTemporal " +
                "	ON polizasdetalleconsolidado.poliza =  #DetallePolizaTemporal.poliza " +
                "SELECT Dependencia, Poliza,	" +
                "Titulares_1VB, Conyuge_1VB, Hijos_1VB, Ascendientes_1VB, convert(int, Titulares_1VB) + convert(int, Conyuge_1VB) + convert(int, Hijos_1VB) + convert(int, Ascendientes_1VB) AS TotalAsegurados_1VB, C333_1VB,	C295_1VB,	[C266.4_1VB] AS C2664_1VB,	C259_1VB,	C222_1VB,	C185_1VB,	C148_1VB,	C111_1VB,	C74_1VB,	Otras_1VB,	convert(int, C333_1VB) + convert(int, C295_1VB) + convert(int, [C266.4_1VB]) + convert(int, C259_1VB) + convert(int, C222_1VB) + convert(int, C185_1VB) + convert(int, C148_1VB) + convert(int, C111_1VB) + convert(int, C74_1VB) + convert(int, Otras_1VB) AS TotalAsegurados_1VB2, BasicaMonto_1VB,	BasicaAjuste_1VB, convert(decimal, BasicaMonto_1VB) + convert(decimal, BasicaAjuste_1VB) as TotalPrimaBasica_1VB,	" +
                "Titulares_1VP,	Conyuge_1VP, Hijos_1VP,	Ascendientes_1VP, convert(int, Titulares_1VP) + convert(int, Conyuge_1VP) + convert(int, Hijos_1VP) + convert(int, Ascendientes_1VP) AS TotalAsegurados_1VP, C74_1VP,	C111_1VP,	C148_1VP,	C185_1VP,	C222_1VP,	C259_1VP,	C295_1VP,	C333_1VP,	C444_1VP,	C592_1VP,	C740_1VP,	C850_1VP,	C1000_1VP,	C34219_1VP,	Otras_1VP, convert(int, C74_1VP) + convert(int, C111_1VP) + convert(int, C148_1VP) + convert(int, C185_1VP) + convert(int, C222_1VP) + convert(int, C259_1VP) + convert(int, C295_1VP) + convert(int, C333_1VP) + convert(int, C444_1VP) + convert(int, C592_1VP) + convert(int, C740_1VP) + convert(int, C850_1VP) + convert(int, C1000_1VP) + convert(int, C34219_1VP) + convert(int, Otras_1VP) as TotalAsegurados_1VP2, PotenciadaMonto_1VP, PotenciadaAjuste_1VP, convert(decimal, PotenciadaMonto_1VP) + convert(decimal, PotenciadaAjuste_1VP) AS TotalPrimaPotenciada_1VP, 	convert(decimal, BasicaMonto_1VB) + convert(decimal, BasicaAjuste_1VB) + convert(decimal, PotenciadaMonto_1VP) + convert(decimal, PotenciadaAjuste_1VP) AS PrimaTotalPeriodo_1V, " +
                "Titulares_1TB,	Conyuge_1TB, Hijos_1TB,	Ascendientes_1TB, convert(int, Titulares_1TB) + convert(int, Conyuge_1TB) + convert(int, Hijos_1TB) + convert(int, Ascendientes_1TB) AS TotalAsegurados_1TB, C333_1TB,	C295_1TB,	[C266.4_1TB] AS C2664_1TB,	C259_1TB,	C222_1TB,	C185_1TB,	C148_1TB,	C111_1TB,	C74_1TB,	Otras_1TB,	convert(int, C333_1TB) + convert(int, C295_1TB) + convert(int, [C266.4_1TB]) + convert(int, C259_1TB) + convert(int, C222_1TB) + convert(int, C185_1TB) + convert(int, C148_1VB) + convert(int, C111_1TB) + convert(int, C74_1TB) + convert(int, Otras_1TB) AS TotalAsegurados_1TB2, BasicaMonto_1TB,	BasicaAjuste_1TB, convert(decimal, BasicaMonto_1TB) + convert(decimal, BasicaAjuste_1TB) as TotalPrimaBasica_1TB,	" +
                "Titulares_1TP,	Conyuge_1TP, Hijos_1TP,	Ascendientes_1TP, convert(int, Titulares_1TP) + convert(int, Conyuge_1TP) + convert(int, Hijos_1TP) + convert(int, Ascendientes_1TP) AS TotalAsegurados_1TP, C74_1TP,	C111_1TP,	C148_1TP,	C185_1TP,	C222_1TP,	C259_1TP,	C295_1TP,	C333_1TP,	C444_1TP,	C592_1TP,	C740_1TP,	C850_1TP,	C1000_1TP,	C34219_1TP,	Otras_1TP, convert(int, C74_1TP) + convert(int, C111_1TP) + convert(int, C148_1TP) + convert(int, C185_1TP) + convert(int, C222_1TP) + convert(int, C259_1TP) + convert(int, C295_1TP) + convert(int, C333_1TP) + convert(int, C444_1TP) + convert(int, C592_1TP) + convert(int, C740_1TP) + convert(int, C850_1TP) + convert(int, C1000_1TP) + convert(int, C34219_1TP) + convert(int, Otras_1TP) as TotalAsegurados_1TP2, PotenciadaMonto_1TP, PotenciadaAjuste_1TP, convert(decimal, PotenciadaMonto_1TP) + convert(decimal, PotenciadaAjuste_1TP) AS TotalPrimaPotenciada_1TP,	convert(decimal, BasicaMonto_1TB) + convert(decimal, BasicaAjuste_1TB) + convert(decimal, PotenciadaMonto_1TP) + convert(decimal, PotenciadaAjuste_1TP) AS PrimaTotalPeriodo_1T, " +
                "Titulares_2TB,	Conyuge_2TB, Hijos_2TB,	Ascendientes_2TB, convert(int, Titulares_2TB) + convert(int, Conyuge_2TB) + convert(int, Hijos_2TB) + convert(int, Ascendientes_2TB) AS TotalAsegurados_2TB, C333_2TB,	C295_2TB,	[C266.4_2TB] AS C2664_2TB,	C259_2TB,	C222_2TB,	C185_2TB,	C148_2TB,	C111_2TB,	C74_2TB,	Otras_2TB,	convert(int, isnull(C333_2TB,0)) + convert(int, isnull(C295_2TB,0)) + convert(int, isnull([C266.4_2TB],0)) + convert(int, isnull(C259_2TB,0)) + convert(int, isnull(C222_2TB,0)) + convert(int, isnull(C185_2TB,0)) + convert(int, isnull(C148_2TB,0)) + convert(int, isnull(C111_2TB,0)) + convert(int, C74_2TB) + convert(int, isnull(Otras_2TB,0)) AS TotalAsegurados_2TB2, CONVERT(DECIMAL, BasicaMonto_2TB) AS BasicaMonto_2TB, CONVERT(DECIMAL, BasicaAjuste_2TB) AS BasicaAjuste_2TB, convert(decimal, BasicaMonto_2TB) + convert(decimal, BasicaAjuste_2TB) as TotalPrimaBasica_2TB,	" +
                "Titulares_2TP,	Conyuge_2TP, Hijos_2TP,	Ascendientes_2TP, convert(int, Titulares_2TP) + convert(int, Conyuge_2TP) + convert(int, Hijos_2TP) + convert(int, Ascendientes_2TP) AS TotalAsegurados_2TP, C74_2TP,	C111_2TP,	C148_2TP,	C185_2TP,	C222_2TP,	C259_2TP,	C295_2TP,	C333_2TP,	C444_2TP,	C592_2TP,	C740_2TP,	C850_2TP,	C1000_2TP,	C34219_2TP,	Otras_2TP, convert(int, C74_2TP) + convert(int, C111_2TP) + convert(int, C148_2TP) + convert(int, C185_2TP) + convert(int, C222_2TP) + convert(int, C259_2TP) + convert(int, C295_2TP) + convert(int, C333_2TP) + convert(int, C444_2TP) + convert(int, C592_2TP) + convert(int, C740_2TP) + convert(int, C850_2TP) + convert(int, C1000_2TP) + convert(int, C34219_2TP) + convert(int, Otras_2TP) as TotalAsegurados_2TP2, convert(decimal,PotenciadaMonto_2TP) AS PotenciadaMonto_2TP, CONVERT(DECIMAL,PotenciadaAjuste_2TP) AS PotenciadaAjuste_2TP, convert(decimal, PotenciadaMonto_2TP) + convert(decimal, PotenciadaAjuste_2TP) AS TotalPrimaPotenciada_2TP, convert(decimal, BasicaMonto_2TB) + convert(decimal, BasicaAjuste_2TB)  + convert(decimal, PotenciadaMonto_2TP) + convert(decimal, PotenciadaAjuste_2TP) AS PrimaTotalPeriodo_2T, " +
                "(CONVERT(DECIMAL, ISNULL(BasicaMonto_1VB,0)) + CONVERT(DECIMAL, ISNULL(BasicaAjuste_1VB,0)) + CONVERT(DECIMAL, ISNULL(PotenciadaMonto_1VP,0)) + CONVERT(DECIMAL, ISNULL(PotenciadaAjuste_1VP, 0)) + CONVERT(DECIMAL, ISNULL(BasicaMonto_1TB, 0)) + CONVERT(DECIMAL, ISNULL(BasicaAjuste_1TB, 0)) + CONVERT(DECIMAL, ISNULL(PotenciadaMonto_1TP, 0)) + CONVERT(DECIMAL, ISNULL(PotenciadaAjuste_1TP, 0)) + convert(decimal, BasicaMonto_2TB) + convert(decimal, BasicaAjuste_2TB)  + convert(decimal, PotenciadaMonto_2TP) + convert(decimal, PotenciadaAjuste_2TP)) AS TotalGeneral " +
                "FROM PolizasDetalleConsolidado";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }
    }
}
