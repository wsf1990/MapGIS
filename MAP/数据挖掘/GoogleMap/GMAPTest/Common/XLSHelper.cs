using System.IO;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace GMAPTest.Common
{
    /// <summary>
    /// xls帮助类
    /// </summary>
    public class XLSHelper
    {
        public static void Write2Xls(string path)
        {
            using (Stream stream = File.OpenWrite(path))
            {
                HSSFWorkbook book = new HSSFWorkbook();
                var sheet = book.CreateSheet("Sheet1");
                SetHeader(sheet, "");
                var row = sheet.CreateRow(0);


                book.Write(stream);
            }
            
        }
        /// <summary>
        /// 设置XLS头
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="headers"></param>
        public static void SetHeader(ISheet sheet, params string[] headers)
        {
            var row = sheet.CreateRow(0);
            int count = headers.Count();
            for (int i = 0; i < count; i++)
            {
                var cell = row.CreateCell(i);
                cell.SetCellType(CellType.STRING);
                cell.SetCellValue(headers[i]);
            }
        }
    }
}
