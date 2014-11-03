using System.IO;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var row = sheet.CreateRow(0);


                book.Write(stream);
            }
            
        }
    }
}
