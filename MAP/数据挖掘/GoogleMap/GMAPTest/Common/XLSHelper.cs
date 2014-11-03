using System.IO;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using GMap.NET;

namespace GMAPTest.Common
{
    /// <summary>
    /// xls帮助类
    /// </summary>
    public class XLSHelper
    {
        /// <summary>
        /// 将结果写到xls中
        /// </summary>
        /// <param name="path"></param>
        /// <param name="addresses"></param>
        /// <param name="points"></param>
        public static void Write2Xls(string path, List<GoogleAddress> addresses, List<PointLatLng> points)
        {
            using (Stream stream = File.OpenWrite(path))
            {
                HSSFWorkbook book = new HSSFWorkbook();
                var sheet = book.CreateSheet("Sheet1");
                var head = new List<string>() { "纬度", "经度", "全称", "简称", "类型","格式化地址" , "区域坐标范围", "坐标类型", "视角范围", "整体类型" };
                SetHeader(sheet, head.ToArray());
                foreach (var item in addresses)
                {
                    var row = sheet.CreateRow(sheet.LastRowNum);

                }
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
