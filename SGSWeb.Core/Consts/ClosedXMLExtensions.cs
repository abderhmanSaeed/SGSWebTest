using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSWeb.Core.Consts
{
    public static class ClosedXMLExtensions
    {
        public static FileStreamResult Deliver(this XLWorkbook workbook, string fileName, string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            var memoryStream = new MemoryStream();

            workbook.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new FileStreamResult(memoryStream, contentType)
            {
                FileDownloadName = fileName
            };
        }
    }
}
