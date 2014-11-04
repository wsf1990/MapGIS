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
                var head = new List<string>() { "纬度", "经度", "全称"};
                var maxcount = addresses.Max(s=>s.Address_components.Count);
                for (int i = 0; i < maxcount; i++)
			    {
			        head.Add("全称" + i + 1);
                    head.Add("简称" + i + 1);
                    head.Add("类型" + i + 1);
			    }
                head.AddRange(new List<string>(){"格式化地址" , "区域坐标范围", "坐标类型", "视角范围", "整体类型"});
                SetHeader(sheet, head.ToArray());
                for (int i = 0; i < points.Count; i++)
                {
                    var address = addresses[i];
                    var row = sheet.CreateRow(sheet.LastRowNum);
                    var thiscount = addresses[i].Address_components.Count;
                    var cell = row.CreateCell(0);
                    cell.SetCellValue(points[i].Lat);

                    cell = row.CreateCell(1);
                    cell.SetCellValue(points[i].Lng);

                    for (int j = 0; j < thiscount; j++)
                    {
                        cell = row.CreateCell(3 * j + 2);
                        cell.SetCellValue(address.Address_components[j].Long_name);

                        cell = row.CreateCell(3 * j + 3);
                        cell.SetCellValue(address.Address_components[j].short_name);

                        cell = row.CreateCell(3 * j + 4);
                        cell.SetCellValue(address.Address_components[j].Types.ToString());
                    }

                    cell = row.CreateCell(2 + maxcount * 3);
                    cell.SetCellValue(address.Formatted_address);

                    cell = row.CreateCell(3 + maxcount * 3);
                    cell.SetCellValue(address.Geometry.Bounds.ToString());

                    cell = row.CreateCell(4 + maxcount * 3);
                    cell.SetCellValue(address.Geometry.Location_type);

                    cell = row.CreateCell(5 + maxcount * 3);
                    cell.SetCellValue(address.Geometry.Viewport.ToString());

                    cell = row.CreateCell(6 + maxcount * 3);
                    cell.SetCellValue(address.Types.ToString());
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
