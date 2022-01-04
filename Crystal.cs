using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaProyecto
{
    public class Crystal
    {
        //atributos
        public ReportDocument Reporte { get; set; }

        public DataTable Datos { get; set; }

        //constructor
        public Crystal(ReportDocument pRpt)
        {
            Reporte = pRpt;

        }

        public Crystal()
        {

        }

        //genera el reporte
        public ReportDocument GenerarReporte()
        {
            if (Datos.Rows.Count > 0)
            {
                Reporte.SetDataSource(Datos);

                return Reporte;
            }
            else
            {
                return null;
            }


        }


    }
}
