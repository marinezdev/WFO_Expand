using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFO.AccesoDatos.Procesos
{
    public class PolizasDetalle
    {
        ManejoDatos b = new ManejoDatos();

        public int Agregar(
        string poliza,
        string dependencia,
        string apaterno,
        string amaterno,
        string nombres,
        string fnacimiento,
        string rfc,
        string curp,
        string sexo,
        string centidadfederativa,
        string cmunicipio,
        string niveltabular,
        string mpercepcionordinariabrutamensual,
        string eventual,
        string apasegurado,
        string amasegurado,
        string nasegurado,
        string fnasegurado,
        string curpasegurado,
        string sasegurado,
        string faasegurado,
        string tasegurado,
        string ficolectividad,
        string sabasica,
        string pbtreportar,
        string mapbasica,
        string itpdependencia, string estado, string folio, string trimestre, string ann
        )
        {
            string consulta = "INSERT INTO polizasdetalle26_30 (Poliza, Dependencia, APaterno, AMaterno, Nombres, FNacimiento, RFC, CURP, Sexo, CEntidadFederativa, " +
            "CMunicipio, NivelTabular, MPercepcionOBM, Eventual, APAsegurado, AMAsegurado, NAsegurado, FNAsegurado, CURPAsegurado, SAsegurado, FAAsegurado, " +
            "TAsegurado, FIColectividad, SABasica, PBTReportar, MAPBasica, ITPDependencia, Estado, FolioAsignado, Formato, Trimestre, Ann) VALUES(" +
            "@poliza," +
            "@dependencia," +
            "@apaterno," +
            "@amaterno," +
            "@nombres," +
            "@fnacimiento," +
            "@rfc," +
            "@curp," +
            "@sexo," +
            "@centidadfederativa," +
            "@cmunicipio," +
            "@niveltabular," +
            "@mpercepcionordinariabrutamensual," +
            "@eventual," +
            "@apasegurado," +
            "@amasegurado," +
            "@nasegurado," +
            "@fnasegurado," +
            "@curpasegurado," +
            "@sasegurado," +
            "@faasegurado," +
            "@tasegurado," +
            "@ficolectividad," +
            "@sabasica," +
            "@pbtreportar," +
            "@mapbasica," +
            "@itpdependencia, @estado, @folio, 1, @trimestre, @ann" +
            ")";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@poliza", poliza, SqlDbType.NVarChar, 8);
            b.AddParameter("@dependencia", dependencia, SqlDbType.NVarChar, 200);
            b.AddParameter("@apaterno", apaterno, SqlDbType.NVarChar, 200);
            b.AddParameter("@amaterno", amaterno, SqlDbType.NVarChar, 200);
            b.AddParameter("@nombres", nombres, SqlDbType.NVarChar, 200);
            b.AddParameter("@fnacimiento", fnacimiento, SqlDbType.NVarChar, 8);
            b.AddParameter("@rfc", rfc, SqlDbType.NVarChar, 20);
            b.AddParameter("@curp", curp, SqlDbType.NVarChar, 20);
            b.AddParameter("@sexo", sexo, SqlDbType.NVarChar, 2);
            b.AddParameter("@centidadfederativa", centidadfederativa, SqlDbType.NVarChar, 5);
            b.AddParameter("@cmunicipio", cmunicipio, SqlDbType.NVarChar, 5);
            b.AddParameter("@niveltabular", niveltabular, SqlDbType.NVarChar, 2);
            b.AddParameter("@mpercepcionordinariabrutamensual", mpercepcionordinariabrutamensual, SqlDbType.NVarChar, 10);
            b.AddParameter("@eventual", eventual, SqlDbType.NVarChar, 2);
            b.AddParameter("@apasegurado", apasegurado, SqlDbType.NVarChar, 200);
            b.AddParameter("@amasegurado", amasegurado, SqlDbType.NVarChar, 200);
            b.AddParameter("@nasegurado", nasegurado, SqlDbType.NVarChar, 200);
            b.AddParameter("@fnasegurado", fnasegurado, SqlDbType.NVarChar, 8);
            b.AddParameter("@curpasegurado", curpasegurado, SqlDbType.NVarChar, 20);
            b.AddParameter("@sasegurado", sasegurado, SqlDbType.NVarChar, 2);
            b.AddParameter("@faasegurado", faasegurado, SqlDbType.NVarChar, 8);
            b.AddParameter("@tasegurado", tasegurado, SqlDbType.NVarChar, 2);
            b.AddParameter("@ficolectividad", ficolectividad, SqlDbType.NVarChar, 8);
            b.AddParameter("@sabasica", sabasica, SqlDbType.NVarChar, 10);
            b.AddParameter("@pbtreportar", pbtreportar, SqlDbType.NVarChar, 10);
            b.AddParameter("@mapbasica", mapbasica, SqlDbType.NVarChar, 10);
            b.AddParameter("@itpdependencia", itpdependencia, SqlDbType.NVarChar, 10);
            b.AddParameter("@estado", estado, SqlDbType.NVarChar, 1);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar, 20);
            b.AddParameter("@trimestre", trimestre, SqlDbType.Int);
            b.AddParameter("@ann", ann, SqlDbType.NChar, 4);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public int AgregarPotenciacion(string poliza,
         string dependencia,
         string apaterno,
         string amaterno,
         string nombres,
         string fnacimiento,
         string rfc,
         string curp,
         string sexo,
         string centidadfederativa,
         string cmunicipio,
         string niveltabular,
         string mpercepcionordinariabrutamensual,
         string eventual,
         string apasegurado,
         string amasegurado,
         string nasegurado,
         string fnasegurado,
         string curpasegurado,
         string sasegurado,
         string faasegurado,
         string tasegurado,
         string ficolectividad,
         string sabasica,
         string sapotenciada,
         string sat,
         string primapqr,
         string formapago,
         string montoappabpc,
         string importetpppfrsp,
         string faaseguradosap,
         string estado, string folio, string quincena, string ann)
        {
            string consulta = "INSERT INTO PolizasDetalle26_30 (Poliza, Dependencia, APaterno, AMaterno, Nombres, FNacimiento, RFC, CURP, Sexo, CEntidadFederativa, " +
            "CMunicipio, NivelTabular, MPercepcionOBM, Eventual, APAsegurado, AMAsegurado, NAsegurado, FNAsegurado, CURPAsegurado, SAsegurado, FAAsegurado, " +
            "TAsegurado, FIColectividad, SABasica, SAPotenciada, SAT, PrimaPotenciadaQR, FormaPago, MontoAjustePrimaP, ImporteTotalPagar, FechaAASA, Estado, " +
            "FolioAsignado, Formato, Quincena, Ann2) VALUES(" +
            "@poliza," +
            "@dependencia," +
            "@apaterno," +
            "@amaterno," +
            "@nombres," +
            "@fnacimiento," +
            "@rfc," +
            "@curp," +
            "@sexo," +
            "@centidadfederativa," +
            "@cmunicipio," +
            "@niveltabular," +
            "@mpercepcionordinariabrutamensual," +
            "@eventual," +
            "@apasegurado," +
            "@amasegurado," +
            "@nasegurado," +
            "@fnasegurado," +
            "@curpasegurado," +
            "@sasegurado," +
            "@faasegurado," +
            "@tasegurado," +
            "@ficolectividad," +
            "@sabasica," +
            "@sapotenciada," +
            "@sat," +
            "@primapqr," +
            "@formapago," +
            "@montoappabpc," +
            "@importetpppfrsp," +
            "@faaseguradosap, @estado, @folio,2, @quincena, @ann" +
            ")";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@poliza", poliza, SqlDbType.NVarChar, 8);
            b.AddParameter("@dependencia", dependencia, SqlDbType.NVarChar, 200);
            b.AddParameter("@apaterno", apaterno, SqlDbType.NVarChar, 200);
            b.AddParameter("@amaterno", amaterno, SqlDbType.NVarChar, 200);
            b.AddParameter("@nombres", nombres, SqlDbType.NVarChar, 200);
            b.AddParameter("@fnacimiento", fnacimiento, SqlDbType.NVarChar, 8);
            b.AddParameter("@rfc", rfc, SqlDbType.NVarChar, 20);
            b.AddParameter("@curp", curp, SqlDbType.NVarChar, 20);
            b.AddParameter("@sexo", sexo, SqlDbType.NVarChar, 2);
            b.AddParameter("@centidadfederativa", centidadfederativa, SqlDbType.NVarChar, 5);
            b.AddParameter("@cmunicipio", cmunicipio, SqlDbType.NVarChar, 5);
            b.AddParameter("@niveltabular", niveltabular, SqlDbType.NVarChar, 2);
            b.AddParameter("@mpercepcionordinariabrutamensual", mpercepcionordinariabrutamensual, SqlDbType.NVarChar, 10);
            b.AddParameter("@eventual", eventual, SqlDbType.NVarChar, 2);
            b.AddParameter("@apasegurado", apasegurado, SqlDbType.NVarChar, 200);
            b.AddParameter("@amasegurado", amasegurado, SqlDbType.NVarChar, 200);
            b.AddParameter("@nasegurado", nasegurado, SqlDbType.NVarChar, 200);
            b.AddParameter("@fnasegurado", fnasegurado, SqlDbType.NVarChar, 8);
            b.AddParameter("@curpasegurado", curpasegurado, SqlDbType.NVarChar, 20);
            b.AddParameter("@sasegurado", sasegurado, SqlDbType.NVarChar, 2);
            b.AddParameter("@faasegurado", faasegurado, SqlDbType.NVarChar, 8);
            b.AddParameter("@tasegurado", tasegurado, SqlDbType.NVarChar, 2);
            b.AddParameter("@ficolectividad", ficolectividad, SqlDbType.NVarChar, 8);
            b.AddParameter("@sabasica", sabasica, SqlDbType.NVarChar, 10);
            b.AddParameter("@sapotenciada", sapotenciada, SqlDbType.NVarChar, 10);
            b.AddParameter("@sat", sat, SqlDbType.NVarChar, 10);
            b.AddParameter("@primapqr", primapqr == " " ? "NULL" : primapqr, SqlDbType.NVarChar, 10);
            b.AddParameter("@formapago", formapago, SqlDbType.NVarChar, 10);
            b.AddParameter("@montoappabpc", montoappabpc, SqlDbType.NVarChar, 10);
            b.AddParameter("@importetpppfrsp", importetpppfrsp == " " ? "NULL" : importetpppfrsp, SqlDbType.NVarChar, 10);
            b.AddParameter("@faaseguradosap", faaseguradosap, SqlDbType.NVarChar, 8);
            b.AddParameter("@estado", estado, SqlDbType.NVarChar, 1);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar, 20);
            b.AddParameter("@quincena", quincena, SqlDbType.Int);
            b.AddParameter("@ann", ann, SqlDbType.NChar, 4);
            return b.InsertUpdateDeleteWithTransaction();
        }

        //Procesos 100 posiciones

        public DataTable SeleccionarCienPosicionesPagos(string folio, string periodo, string retenedor)
        {
            string consulta = "SELECT '10' + @retenedor " + 
            " + '      ' + '" + periodo + "075P' + " +
            "dbo.ConvertirImporte(REPLACE(CAST(SUM(CONVERT(DECIMAL, ISNULL(REPLACE(ImporteTotalPagar, ',', ''), 0))) AS varchar), '.', '')) + " +
            "RFC + " +
            "CASE " +
            "WHEN LEN(SUBSTRING((APaterno +' ' + AMaterno + ' ' + Nombres), 1, 29)) = 29 " +
            "   THEN SUBSTRING((APaterno +' ' + AMaterno + ' ' + Nombres), 1, 29)  " +
            "WHEN LEN(SUBSTRING((APaterno +' ' + AMaterno + ' ' + Nombres), 1, 29)) = 28 " +
            "   THEN SUBSTRING((APaterno +' ' + AMaterno + ' ' + Nombres), 1, 28) + REPLICATE(' ', 29 - LEN(SUBSTRING((APaterno + ' ' + AMaterno + ' ' + Nombres), 1, 29))) " +
            "ELSE " +
            "   SUBSTRING((APaterno + ' ' + AMaterno + ' ' + Nombres), 1, 29) + REPLICATE(' ', 29 - LEN(SUBSTRING((APaterno + ' ' + AMaterno + ' ' + Nombres), 1, 29))) " +
            "END + " +
            "'000000000000000000000000000' " +
            "FROM PolizasDetalle26_30 " +
            "WHERE folioAsignado=@folio AND CONVERT(DECIMAL, ISNULL(REPLACE(ImporteTotalPagar, ',', ''), 0)) > 0 " +
            "GROUP BY RFC, APaterno, AMaterno, Nombres, Dependencia ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            b.AddParameter("@retenedor", retenedor, SqlDbType.VarChar, 10);
            return b.Select();
        }

        public string CienPosicionesSumaPagos(string folio)
        {
            string consulta = "SELECT SUM(CONVERT(FLOAT, ISNULL(REPLACE(ImporteTotalPagar, ',', ''), 0))) " +
            "FROM PolizasDetalle26_30 " +
            "WHERE folioAsignado=@folio AND CONVERT(FLOAT, ISNULL(REPLACE(ImporteTotalPagar, ',', ''), 0)) > 0";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            return b.SelectString();
        }

        public DataTable SeleccionarCienPosicionesCancelaciones(string folio, string periodo, string retenedor)
        {
            string consulta = "SELECT '10' + @retenedor + '      ' + '" + periodo + "075C' + " +
            "dbo.ConvertirImporte(REPLACE(CAST(SUM(CONVERT(FLOAT, ISNULL(REPLACE(ImporteTotalPagar, ',', ''), 0))) AS varchar), '.', '')) + " +
            "RFC + " +
            "CASE " +
            "WHEN LEN(SUBSTRING((APaterno +' ' + AMaterno + ' ' + Nombres), 1, 29)) = 29 " +
            "   THEN SUBSTRING((APaterno +' ' + AMaterno + ' ' + Nombres), 1, 29)  " +
            "WHEN LEN(SUBSTRING((APaterno +' ' + AMaterno + ' ' + Nombres), 1, 29)) = 28 " +
            "   THEN SUBSTRING((APaterno +' ' + AMaterno + ' ' + Nombres), 1, 28) +REPLICATE(' ', 29 - LEN(SUBSTRING((APaterno + ' ' + AMaterno + ' ' + Nombres), 1, 29))) " +
            "ELSE " +
            "   SUBSTRING((APaterno + ' ' + AMaterno + ' ' + Nombres), 1, 29) + REPLICATE(' ', 29 - LEN(SUBSTRING((APaterno + ' ' + AMaterno + ' ' + Nombres), 1, 29))) " +
            "END + " +
            "'000000000000000000000000000' " +
            "FROM PolizasDetalle26_30 " +
            "WHERE folioAsignado=@folio AND CONVERT(FLOAT, ISNULL(REPLACE(ImporteTotalPagar, ',', ''), 0)) < 0 " +
            "GROUP BY RFC, APaterno, AMaterno, Nombres, Dependencia ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            b.AddParameter("@retenedor", retenedor, SqlDbType.NVarChar);
            return b.Select();
        }

        public string CienPosicionesSumaCancelaciones(string folio)
        {
            string consulta = "SELECT SUM(CONVERT(FLOAT, ISNULL(REPLACE(ImporteTotalPagar, ',', ''), 0))) " +
            "FROM PolizasDetalle26_30 " +
            "WHERE folioAsignado=@folio AND CONVERT(FLOAT, ISNULL(REPLACE(ImporteTotalPagar, ',', ''), 0)) < 0";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            return b.SelectString();
        }

        public DataTable SeleccionarPolizasColectividadExcel()
        {
            DataTable dt = new DataTable();

            try
            {
                string consulta = "DELETE FROM PolizasDetalle " +

                "INSERT INTO PolizasDetalle (Poliza, Dependencia, Certificado, APaterno, AMaterno, Nombres, FNacimiento, RFC, CURP, Sexo, CEntidadFederativa, CMunicipio, NivelTabular, MPercepcionOBM, Eventual , APAsegurado, AMAsegurado, NAsegurado, FNAsegurado, CURPAsegurado, SAsegurado, FAAsegurado, TAsegurado, FIColectividad,  " +
                "Trimestre, SAB2T, pab2t)  " +
                "SELECT c.Poliza, c.Dependencia, b.Certificado, c.APaterno, c.AMaterno, c.Nombres, c.FNacimiento, c.RFC, c.CURP, c.Sexo, c.CEntidadFederativa, c.CMunicipio,  " +
                "c.NivelTabular, c.MPercepcionOBM, c.Eventual, b.ApellidoMaterno, b.ApellidoMaterno, b.Nombres, b.FechaNacimiento, b.CURP, b.Sexo,  " +
                "b.FechaAfiliacion, b.Tipo, b.FechaIngresoColectividad, c.Trimestre, c.SABasica, c.ITPDependencia  " +
                "FROM AseguradosTitulares a, Coasegurados b, PolizasDetalle26_30 c  " +
                "WHERE a.Curp=b.CURPTitular  " +
                //"AND c.Trimestre=2  " +
                "AND c.FNAsegurado=b.FechaNacimiento  " +

                "UPDATE PolizasDetalle  " +
                "SET SAP7Q=PolizasDetalle.SAPotenciada, PAP7Q=PolizasDetalle.ImporteTotalPagar  " +
                "FROM PolizasDetalle " +
                "INNER JOIN PolizasDetalle26_30 " +
                "ON (PolizasDetalle.CURPAsegurado=PolizasDetalle26_30.CURPAsegurado) " +
                "WHERE PolizasDetalle26_30.Quincena=7 " +

                "UPDATE PolizasDetalle " +
                "SET SAP8Q=PolizasDetalle.SAPotenciada, PAP8Q=PolizasDetalle.ImporteTotalPagar " +
                "FROM PolizasDetalle " +
                "INNER JOIN PolizasDetalle26_30 " +
                "ON (PolizasDetalle.CURPAsegurado=PolizasDetalle26_30.CURPAsegurado) " +
                "WHERE PolizasDetalle26_30.Quincena=8 " +

                "UPDATE PolizasDetalle " +
                "SET SAP9Q=PolizasDetalle.SAPotenciada, PAP9Q=PolizasDetalle.ImporteTotalPagar  " +
                "FROM PolizasDetalle  " +
                "INNER JOIN PolizasDetalle26_30  " +
                "ON (PolizasDetalle.CURPAsegurado=PolizasDetalle26_30.CURPAsegurado)  " +
                "WHERE PolizasDetalle26_30.Quincena=9  " +

                "UPDATE PolizasDetalle  " +
                "SET SAP10Q=PolizasDetalle.SAPotenciada, PAP10Q=PolizasDetalle.ImporteTotalPagar  " +
                "FROM PolizasDetalle  " +
                "INNER JOIN PolizasDetalle26_30  " +
                "ON (PolizasDetalle.CURPAsegurado=PolizasDetalle26_30.CURPAsegurado)  " +
                "WHERE PolizasDetalle26_30.Quincena=10  " +

                "UPDATE PolizasDetalle  " +
                "SET SAP11Q=PolizasDetalle.SAPotenciada, PAP11Q=PolizasDetalle.ImporteTotalPagar  " +
                "FROM PolizasDetalle  " +
                "INNER JOIN PolizasDetalle26_30  " +
                "ON (PolizasDetalle.CURPAsegurado=PolizasDetalle26_30.CURPAsegurado)  " +
                "WHERE PolizasDetalle26_30.Quincena=11  " +

                "UPDATE PolizasDetalle  " +
                "SET SAP12Q=PolizasDetalle.SAPotenciada, PAP12Q=PolizasDetalle.ImporteTotalPagar  " +
                "FROM PolizasDetalle  " +
                "INNER JOIN PolizasDetalle26_30  " +
                "ON (PolizasDetalle.CURPAsegurado=PolizasDetalle26_30.CURPAsegurado)  " +
                "WHERE PolizasDetalle26_30.Quincena=12  " +

                "UPDATE PolizasDetalle SET PAB1PV = REPLACE(PAB1PV, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP1PV = REPLACE(PAP1PV, ',', '')  " +
                "UPDATE PolizasDetalle SET PAB1T = REPLACE(PAB1T, ',', '')  " +
                "UPDATE PolizasDetalle SET PAB2T = REPLACE(PAB2T, ',', '')  " +
                "UPDATE PolizasDetalle SET PAB3T = REPLACE(PAB3T, ',', '')  " +
                "UPDATE PolizasDetalle SET PAB4T = REPLACE(PAB4T, ',', '')  " +
                "UPDATE PolizasDetalle SET PAB1T19 = REPLACE(PAB1T19, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP6Q19 = REPLACE(PAP6Q19, ',', '')  " +
                "UPDATE PolizasDetalle SET PAB2T19 = REPLACE(PAB2T19, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP12Q19 = REPLACE(PAP12Q19, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP2Q = REPLACE(PAP2Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP1Q = REPLACE(PAP1Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP3Q = REPLACE(PAP3Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP4Q = REPLACE(PAP4Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP5Q = REPLACE(PAP5Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP6Q = REPLACE(PAP6Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP7Q = REPLACE(PAP7Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP8Q = REPLACE(PAP8Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP9Q = REPLACE(PAP9Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP10Q = REPLACE(PAP10Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP11Q = REPLACE(PAP11Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP12Q = REPLACE(PAP12Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP13Q = REPLACE(PAP13Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP14Q = REPLACE(PAP14Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP15Q = REPLACE(PAP15Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP16Q = REPLACE(PAP16Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP17Q = REPLACE(PAP17Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP18Q = REPLACE(PAP18Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP19Q = REPLACE(PAP19Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP20Q = REPLACE(PAP20Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP21Q = REPLACE(PAP21Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP22Q = REPLACE(PAP22Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP23Q = REPLACE(PAP23Q, ',', '')  " +
                "UPDATE PolizasDetalle SET PAP24Q = REPLACE(PAP24Q, ',', '')  " +

                "SELECT Poliza, Dependencia, Certificado, APaterno, AMaterno, Nombres, FNacimiento, RFC, CURP, Sexo, CEntidadFederativa, CMunicipio, NivelTabular, " +
                "MPercepcionOBM, Eventual, APAsegurado, AMAsegurado, NAsegurado, FNAsegurado, CURPAsegurado, SAsegurado, FAAsegurado, TAsegurado, FIColectividad, '' AS Estatus, '' AS FechaBaja, " +
                "ISNULL(SAB1PV, '0') AS SAB1PV, ISNULL(SAP1PV, '0') AS SAP1PV, ISNULL(SUM(CONVERT(decimal, SAP1PV) + CONVERT(decimal, SAP1PV)), '0') AS SAT1PV, " +
                "ISNULL(SAB1T, '0') AS SAB1T, ISNULL(SAP6Q, '0') AS SAP6Q, ISNULL(SUM(CONVERT(decimal, SAB1T) + CONVERT(decimal, SAP6Q)), '0') AS SAT1T, " +
                "ISNULL(SAB2T, '0') AS SAB2T, ISNULL(SAP12Q, '0') AS SAP12Q, ISNULL(SUM(CONVERT(decimal, SAB2T) + CONVERT(decimal, SAP12Q)), '0') AS SAT2T, " +
                "ISNULL(SAB3T, '0') AS SAB3T, ISNULL(SAP18Q, '0') AS SAP18Q, ISNULL(SUM(CONVERT(decimal, SAB3T) + CONVERT(decimal, SAP18Q)), '0') AS SAT3T, " +
                "ISNULL(SAB4T, '0') AS SAB4T, ISNULL(SAP24Q, '0') AS SAP24Q, ISNULL(SUM(CONVERT(decimal, SAB4T) + CONVERT(decimal, SAP24Q)), '0') AS SAT3T, " +
                "ISNULL(SAB1T19, '0') AS SAB1T19, ISNULL(SAP6Q19, '0') AS SAP6Q19, ISNULL(SUM(CONVERT(decimal, SAB1T19) + CONVERT(decimal, SAP6Q19)), '0') AS SAT1T19, " +
                "ISNULL(SAB2T19, '0') AS SAB2T19, ISNULL(SAP12Q19, '0') AS SAP12Q19, ISNULL(SUM(CONVERT(decimal, SAB2T19) + CONVERT(decimal, SAP12Q19)), '0') AS SAT2T19, " +
                "ISNULL(CONVERT(decimal,PAB1PV), '0') AS PAB1PV, ISNULL(PAP1PV, '0') AS PAP1PV, ISNULL(SUM(CONVERT(decimal, PAB1PV) + CONVERT(decimal, PAP1PV)), '0') AS PT1PV, " +
                "ISNULL(CONVERT(decimal, PAB1T), '0') AS PAB1T, ISNULL(SUM(CONVERT(decimal, PAP1Q) + CONVERT(decimal, PAP2Q) + CONVERT(decimal, PAP3Q) + CONVERT(decimal, PAP4Q) + CONVERT(decimal, PAP5Q) + CONVERT(decimal, PAP6Q)), '0') AS PAP1T, ISNULL(CONVERT(decimal, PAB1T) + SUM(CONVERT(decimal, PAP1Q) + CONVERT(decimal, PAP2Q) + CONVERT(decimal, PAP3Q) + CONVERT(decimal, PAP4Q) + CONVERT(decimal, PAP5Q) + CONVERT(decimal, PAP6Q)), '0') AS PT1T, " +
                "ISNULL(CONVERT(decimal,PAB2T), '0') AS PAB2T, ISNULL(SUM(CONVERT(decimal, PAP7Q) + CONVERT(decimal, PAP8Q) + CONVERT(decimal, PAP9Q) + CONVERT(decimal, PAP10Q) + CONVERT(decimal, PAP11Q) + CONVERT(decimal, PAP12Q)), '0') AS PAP2T, ISNULL(CONVERT(decimal, PAB2T) + SUM(CONVERT(decimal, PAP7Q) + CONVERT(decimal, PAP8Q) + CONVERT(decimal, PAP9Q) + CONVERT(decimal, PAP10Q) + CONVERT(decimal, PAP11Q) + CONVERT(decimal, PAP12Q)), '0') AS PT2T, " +
                "ISNULL(CONVERT(decimal, PAB3T), '0') AS PAB3T, ISNULL(SUM(CONVERT(decimal, PAP13Q) + CONVERT(decimal, PAP14Q) + CONVERT(decimal, PAP15Q) + CONVERT(decimal, PAP16Q) + CONVERT(decimal, PAP17Q) + CONVERT(decimal, PAP18Q)), '0') AS PAP3T, ISNULL(CONVERT(decimal, PAB3T) + SUM(CONVERT(decimal, PAP13Q) + CONVERT(decimal, PAP14Q) + CONVERT(decimal, PAP15Q) + CONVERT(decimal, PAP16Q) + CONVERT(decimal, PAP17Q) + CONVERT(decimal, PAP18Q)), '0') AS PT3T, " +
                "ISNULL(CONVERT(decimal, PAB4T), '0') AS PAB4T, ISNULL(SUM(CONVERT(decimal, PAP19Q) + CONVERT(decimal, PAP20Q) + CONVERT(decimal, PAP21Q) + CONVERT(decimal, PAP22Q) + CONVERT(decimal, PAP23Q) + CONVERT(decimal, PAP24Q)), '0') AS PAP4T, ISNULL(CONVERT(decimal, PAB4T) + SUM(CONVERT(decimal, PAP19Q) + CONVERT(decimal, PAP20Q) + CONVERT(decimal, PAP21Q) + CONVERT(decimal, PAP22Q) + CONVERT(decimal, PAP23Q) + CONVERT(decimal, PAP24Q)), '0') AS PT4T, " +
                "ISNULL(CONVERT(decimal, PAB1T19), '0') AS PAB1T19, ISNULL(PAP6Q19, '0') AS PAP6Q19, ISNULL(SUM(CONVERT(decimal, PAB1T19) + CONVERT(decimal, PAP6Q19)), '0') AS PT1T19, " +
                "ISNULL(CONVERT(decimal, PAB2T19), '0') AS PAB2T19, ISNULL(PAP12Q19, '0') AS PAP12Q19, ISNULL(SUM(CONVERT(decimal, PAB2T19) + CONVERT(decimal, PAP12Q19)), '0') AS PT2T19, " +

                "ISNULL(CONVERT(decimal, PAB1PV), '0') + ISNULL(CONVERT(decimal, PAB1T), '0') + ISNULL(CONVERT(decimal, PAB2T), '0') + ISNULL(CONVERT(decimal, PAB3T), '0') + " +
                "ISNULL(CONVERT(decimal, PAB4T), '0') + ISNULL(CONVERT(decimal, PAB1T19), '0') + ISNULL(CONVERT(decimal, PAB2T19), '0') AS PABT, " +
                "ISNULL(CONVERT(decimal, PAP1PV), '0') +ISNULL(CONVERT(decimal, PAP1Q), '0') + ISNULL(CONVERT(decimal, PAP2Q), '0') + ISNULL(CONVERT(decimal, PAP3Q), '0') + ISNULL(CONVERT(decimal, PAP4Q), '0') + ISNULL(CONVERT(decimal, PAP5Q), '0') + ISNULL(CONVERT(decimal, PAP6Q), '0') + " +
                "ISNULL(CONVERT(decimal, PAP7Q), '0') + ISNULL(CONVERT(decimal, PAP8Q), '0') + ISNULL(CONVERT(decimal, PAP9Q), '0') + ISNULL(CONVERT(decimal, PAP10Q), '0') + ISNULL(CONVERT(decimal, PAP11Q), '0') + ISNULL(CONVERT(decimal, PAP12Q), '0') + " +
                "ISNULL(CONVERT(decimal, PAP13Q), '0') + ISNULL(CONVERT(decimal, PAP14Q), '0') + ISNULL(CONVERT(decimal, PAP15Q), '0') + ISNULL(CONVERT(decimal, PAP16Q), '0') + ISNULL(CONVERT(decimal, PAP17Q), '0') + ISNULL(CONVERT(decimal, PAP18Q), '0') + " +
                "ISNULL(CONVERT(decimal, PAP19Q), '0') + ISNULL(CONVERT(decimal, PAP20Q), '0') + ISNULL(CONVERT(decimal, PAP21Q), '0') + ISNULL(CONVERT(decimal, PAP22Q), '0') + ISNULL(CONVERT(decimal, PAP23Q), '0') + ISNULL(CONVERT(decimal, PAP24Q), '0') + " +
                "ISNULL(CONVERT(decimal, PAB1T19), '0') + ISNULL(CONVERT(decimal, PAP6Q19), '0') + " +
                "ISNULL(CONVERT(decimal, PAB2T19), '0') + ISNULL(CONVERT(decimal, PAP12Q19), '0') AS PPT, " +

                "ISNULL(CONVERT(decimal, PAB1PV), '0') + ISNULL(CONVERT(decimal, PAB1T), '0') + ISNULL(CONVERT(decimal, PAB2T), '0') + ISNULL(CONVERT(decimal, PAB3T), '0') + ISNULL(CONVERT(decimal, PAB4T), '0') + ISNULL(CONVERT(decimal, PAB1T19), '0') + ISNULL(CONVERT(decimal, PAB2T19), '0') + " +
                "ISNULL(SUM(CONVERT(decimal, PAB1PV) + CONVERT(decimal, PAP1PV)), '0') + " +
                "ISNULL(SUM(CONVERT(decimal, PAP1Q) + CONVERT(decimal, PAP2Q) + CONVERT(decimal, PAP3Q) + CONVERT(decimal, PAP4Q) + CONVERT(decimal, PAP5Q) + CONVERT(decimal, PAP6Q)), '0') + " +
                "ISNULL(SUM(CONVERT(decimal, PAP7Q) + CONVERT(decimal, PAP8Q) + CONVERT(decimal, PAP9Q) + CONVERT(decimal, PAP10Q) + CONVERT(decimal, PAP11Q) + CONVERT(decimal, PAP12Q)), '0') + " +
                "ISNULL(SUM(CONVERT(decimal, PAP13Q) + CONVERT(decimal, PAP14Q) + CONVERT(decimal, PAP15Q) + CONVERT(decimal, PAP16Q) + CONVERT(decimal, PAP17Q) + CONVERT(decimal, PAP18Q)), '0') + " +
                "ISNULL(SUM(CONVERT(decimal, PAP19Q) + CONVERT(decimal, PAP20Q) + CONVERT(decimal, PAP21Q) + CONVERT(decimal, PAP22Q) + CONVERT(decimal, PAP23Q) + CONVERT(decimal, PAP24Q)), '0') + " +
                "ISNULL(SUM(CONVERT(decimal, PAB1T19) + CONVERT(decimal, PAP6Q19)), '0') + " +
                "ISNULL(SUM(CONVERT(decimal, PAB2T19) + CONVERT(decimal, PAP12Q19)), '0') AS PT " +

                "FROM PolizasDetalle " +
                "GROUP BY Poliza, Dependencia, Certificado, APaterno, AMaterno, Nombres, FNacimiento, RFC, CURP, Sexo, CEntidadFederativa, CMunicipio, NivelTabular, " +
                "MPercepcionOBM, Eventual, APAsegurado, AMAsegurado, NAsegurado, FNAsegurado, CURPAsegurado, SAsegurado, FAAsegurado, TAsegurado, FIColectividad, " +
                "SAB1PV, SAP1PV, SAB1T, SAP6Q, SAB2T, SAP12Q, SAB3T, SAP18Q, SAB4T, SAP24Q, SAB1T19, SAP6Q19, SAB2T19, SAP12Q19, " +
                "PAB1PV, PAP1PV, PAB1T, PAB2T, PAB3T, PAB4T, PAB1T19, PAP6Q19,  PAB2T19, PAP12Q19, PAP2Q, " +
                "PAP1Q, PAP3Q, PAP4Q, PAP5Q, PAP6Q, PAP7Q, PAP8Q, PAP9Q, PAP10Q, PAP11Q, PAP12Q, PAP13Q, PAP14Q, PAP15Q, PAP16Q, PAP17Q, PAP18Q, PAP19Q, PAP20Q, PAP21Q, PAP22Q, PAP23Q, PAP24Q ";

                b.ExecuteCommandQuery(consulta);

                b.ConnectionOpenToTransaction();
                b.BeginTransaction();

                dt = b.SelectExecuteFunctionsToTransaction().Tables[0];
                b.CommitTransaction();
            }
            catch 
            {
                b.RollBackTransaction();
            }
            finally
            {
                b.ConnectionCloseToTransaction();
            }

            return dt;
        }

    }
}
