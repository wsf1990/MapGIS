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
        /// <param name="titles"></param>
        /// <param name="isPoint"></param>
        public static void Write2Xls(string path, List<GoogleAddress> addresses, object titles, bool isPoint)
        {
            using (Stream stream = File.OpenWrite(path))
            {
                var book = new HSSFWorkbook();
                var sheet = book.CreateSheet("Sheet1");
                var maxcount = addresses.Where(s=>s != null && s.Address_components != null && s.Address_components.Count > 0).Max(s => s.Address_components.Count);
                SetHeader(ref sheet, isPoint, maxcount);
                Write(ref sheet, addresses, titles, isPoint, maxcount);
                book.Write(stream);
            }
        }
        /// <summary>
        /// 写内容
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="addresses"></param>
        /// <param name="titles"></param>
        /// <param name="isPoint"></param>
        /// <param name="maxAddressComponentCount"></param>
        private static void Write(ref ISheet sheet, List<GoogleAddress> addresses, object titles, bool isPoint, int maxAddressComponentCount)
        {
            List<PointLatLng> points = null;
            List<string> adds = null;
            var count = 0;
            if (isPoint)
            {
                points = (List<PointLatLng>)titles;
                count = points.Count;
            }
            else
            {
                adds = (List<string>) titles;
                count = adds.Count;
            }
            for (int i = 0; i < count; i++)
            {
                var address = addresses[i];
                var row = sheet.CreateRow(sheet.LastRowNum + 1);
                var thiscount = 0;
                var cell = row.CreateCell(0);
                int rec = 0;
                if (isPoint)
                {
                    cell.SetCellValue(points[i].Lat);

                    cell = row.CreateCell(1);
                    cell.SetCellValue(points[i].Lng);
                    rec = 2;
                }
                else
                {
                    cell.SetCellValue(adds[i]);
                    rec = 1;
                }
                if (addresses[i] == null || addresses[i].Address_components == null)
                {
                    continue;
                }
                thiscount = addresses[i].Address_components.Count;
                for (int j = 0; j < thiscount; j++)
                {
                    cell = row.CreateCell(rec + 3 * j);
                    cell.SetCellValue(address.Address_components[j].Long_name);

                    cell = row.CreateCell(rec+ 1 + 3 * j);
                    cell.SetCellValue(address.Address_components[j].short_name);

                    cell = row.CreateCell(rec+ 2 + 3 * j);
                    cell.SetCellValue(address.Address_components[j].Types.GetString());
                }

                cell = row.CreateCell(rec + maxAddressComponentCount * 3);
                cell.SetCellValue(address.Formatted_address);

                cell = row.CreateCell(rec + 1 + maxAddressComponentCount * 3);
                cell.SetCellValue(address.Geometry.Bounds == null ? "" : address.Geometry.Bounds.ToString());

                cell = row.CreateCell(rec + 2 + maxAddressComponentCount * 3);
                cell.SetCellValue(address.Geometry.Location.Lat + "," + address.Geometry.Location.Lng);

                cell = row.CreateCell(rec + 3 + maxAddressComponentCount * 3);
                cell.SetCellValue(address.Geometry.Location_type.ToString());

                cell = row.CreateCell(rec + 4 + maxAddressComponentCount * 3);
                cell.SetCellValue(address.Geometry.Viewport.ToString());

                cell = row.CreateCell(rec + 5 + maxAddressComponentCount * 3);
                cell.SetCellValue(address.Partial_match);

                cell = row.CreateCell(rec + 6 + maxAddressComponentCount * 3);
                cell.SetCellValue(address.Types.GetString());
            }
        }
        /// <summary>
        /// 为两种地名解析方式设置XLS头
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="isPoint">是否是根据经纬度查找</param>
        /// <param name="maxAddressComponentCount">查出的地址最大AddressComponent数量</param>
        public static void SetHeader(ref ISheet sheet, bool isPoint, int maxAddressComponentCount)
        {
            List<string> head = isPoint ? new List<string>() {"纬度", "经度"} : new List<string>() {"名称"};
            for (int i = 0; i < maxAddressComponentCount; i++)
            {
                head.Add("全称" + (i + 1));
                head.Add("简称" + (i + 1));
                head.Add("类型" + (i + 1));
            }
            head.AddRange(new List<string>() { "格式化地址", "区域坐标范围", "坐标", "坐标类型", "可视区域范围", "部分匹配", "整体类型" });
            SetHeader(sheet, head.ToArray());
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
