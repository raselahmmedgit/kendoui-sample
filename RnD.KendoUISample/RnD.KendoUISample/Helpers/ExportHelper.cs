using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;

namespace RnD.KendoUISample.Helpers
{
    public class ExportHelper
    {
        private string fileName;
        private bool isInExcel;

        public ExportHelper(string exportFileName, bool isExportInExcel)
        {
            fileName = exportFileName;
            isInExcel = isExportInExcel;
        }

        public void ExportData(DataTable dataTobeExported, HttpResponse response)
        {
            string content = string.Empty;
            if (this.isInExcel == true)
            {
                content = BuildExportContent(dataTobeExported, "\t", "\t");
                ExportToExcel(content, response);
            }
            else
            {
                content = BuildExportContent(dataTobeExported, "'", ",");
                ExportToCSV(content, response);
            }


        }

        private void ExportToExcel(string exportContent, HttpResponse response)
        {
            //string style = @"<style> .SIMNo { mso-number-format:\@; } .prom {text-align:left; } "; 

            response.Clear();

            response.AddHeader("content-disposition", "attachment;filename=" + fileName);

            response.Charset = "";

            response.ContentType = "application/vnd.xls";

            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            response.Write(exportContent);
            //response.Write(style);

            response.End();

        }

        private void ExportToCSV(string exportContent, HttpResponse response)
        {

            response.Clear();

            response.AddHeader("content-disposition", "attachment;filename=" + fileName);

            response.Charset = "";

            response.ContentType = "application/octet-stream";

            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            response.Write(exportContent);

            response.End();

        }

        private string BuildExportContent(DataTable dataToExport, string delimiter, string seperator)
        {
            StringBuilder contentBuilder = new StringBuilder();

            foreach (DataColumn dc in dataToExport.Columns)
            {
                contentBuilder.Append(dc.ColumnName);
                contentBuilder.Append(seperator);
            }

            contentBuilder.Append(Environment.NewLine);
            contentBuilder.Append(Environment.NewLine);

            foreach (DataRow dr in dataToExport.Rows)
            {
                foreach (DataColumn vc in dataToExport.Columns)
                {
                    contentBuilder.Append(dr[vc.ColumnName]);
                    contentBuilder.Append(seperator);
                }
                contentBuilder.Length = contentBuilder.Length - 1;
                contentBuilder.Append(Environment.NewLine);
            }

            return contentBuilder.ToString();
        }
    }
}