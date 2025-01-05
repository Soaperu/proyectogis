using ArcGIS.Core.Data;
using CommonUtilities.ArcgisProUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities
{
    public class DataProcessorUtils
    {
        public static void ProcessorDataCaramIntersect(DataTable lodbtExisteCA)
        {
            //string lista_caramur;
            //string lista_caramre;
            if (lodbtExisteCA.Rows.Count >= 1)
            {
                var grupos = lodbtExisteCA.AsEnumerable()
                    .GroupBy(row => {
                        string vcodi = row["CODIGO"].ToString();
                        return (vcodi.Length >= 2 && (vcodi.Substring(0, 2) == "ZU" || vcodi.Substring(0, 2) == "U1")) ? "caramur" : "caramre";
                    });

                foreach (var grupo in grupos)
                {
                    if (grupo.Key == "caramur")
                    {
                        GlobalVariables.resultadoEvaluacion.listaCaramUrbana = string.Join(",", grupo.Select(row => row["NM_AREA"].ToString()));
                    }
                    else if (grupo.Key == "caramre")
                    {
                         GlobalVariables.resultadoEvaluacion.listaCaramReservada = string.Join(",", grupo.Select(row => row["NM_AREA"].ToString()));
                    }
                }
            } 
        }

        public static void ProcessorDataCforestalIntersect(DataTable lodbtExistecf)
        {
            if (lodbtExistecf.Rows.Count >= 1)
            {
                StringBuilder lista_forestalBuilder = new StringBuilder();

                for (int contador = 0; contador < lodbtExistecf.Rows.Count; contador++)
                {
                    string TP_CONCE = "";

                    DataRow row = lodbtExistecf.Rows[contador];

                    // Verificar si la columna "TP_CONCE" existe y no es nula
                    if (lodbtExistecf.Columns.Contains("TP_CONCE") && row["TP_CONCE"] != DBNull.Value)
                    {
                        TP_CONCE = row["TP_CONCE"].ToString();
                    }

                    if (contador == 0)
                    {
                        lista_forestalBuilder.Append(TP_CONCE);
                    }
                    else
                    {
                        lista_forestalBuilder.Append("," + TP_CONCE);
                    }
                }

                // Asignar el resultado final a la variable lista_forestal
                GlobalVariables.resultadoEvaluacion.listaCatastroforestal = lista_forestalBuilder.ToString();
            }
        }
        public static void ProcessorDataAreaAdminstrative(DataTable lodbtExisteDist)
        {
            try
            {
                if (lodbtExisteDist.Rows.Count >= 1)
                {
                    foreach (DataRow row in lodbtExisteDist.Rows)
                    {
                        string lostr_Join_Codigos_marcona;
                        string valida_urb_shp;
                        string lostr_Join_Codigos_AREA;
                        GlobalVariables.CurrentDepDm += FeatureProcessorUtils.ProcessDataRowsFields("Departamento",
                                                                                        row,
                                                                                        "",
                                                                                        out lostr_Join_Codigos_marcona,
                                                                                        out valida_urb_shp,
                                                                                        out lostr_Join_Codigos_AREA
                                    );
                        GlobalVariables.CurrentProvDm += FeatureProcessorUtils.ProcessDataRowsFields("Provincia",
                                                                                        row,
                                                                                        "",
                                                                                        out lostr_Join_Codigos_marcona,
                                                                                        out valida_urb_shp,
                                                                                        out lostr_Join_Codigos_AREA
                                    );
                        GlobalVariables.CurrentDistDm += FeatureProcessorUtils.ProcessDataRowsFields("Distrito",
                                                                                        row,
                                                                                        "",
                                                                                        out lostr_Join_Codigos_marcona,
                                                                                        out valida_urb_shp,
                                                                                        out lostr_Join_Codigos_AREA
                                    );
                    }
                }
            }
            catch(Exception ex) { }
            
        }
    }
}
