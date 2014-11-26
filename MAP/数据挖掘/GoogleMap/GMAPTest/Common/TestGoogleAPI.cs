using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using NPOI.HSSF.UserModel;

namespace GMAPTest.Common
{
    public class TestGoogleAPI
    {
        public static void GetPoint()
        {
            //var list = File.ReadAllLines("D:\\simiao.txt").ToList();
            var l1 = new List<string>();
            var l2 = new List<string>();
            string path = @"C:\Users\JSJZX\Desktop\宗教\藏传佛教\宗教场所_按地市州查询_定西_20140528144843.xls";
            ReadXLS(path, 2, 6, out l1, out l2);
            var line1 = GetLatLngList(l1);
            var line2 = GetLatLngList(l2);
            WriteXLS(path, line1, line2);
        }

        static List<double[]> GetLatLngList(List<string> list)
        {
            List<double[]> lines = new List<double[]>();
            var lists = PlaceHelper.GetPoints(list.ToList());
            foreach (var s in lists)
            {
                if (s == null || s.Geometry == null)
                    lines.Add(new double[] {0, 0});
                else
                    lines.Add(new double[] {s.Geometry.Location.Lat, s.Geometry.Location.Lng});
            }
            return lines;
        }

        private static void WriteXLS(string path, List<double[]> l1, List<double[]> l2)
        {
            HSSFWorkbook book;
            using(Stream stream = File.Open(path, FileMode.Open))
            {
                book = new HSSFWorkbook(stream);
                var sheet = book.GetSheetAt(0);
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    var cell = row.CreateCell(0);
                    cell.SetCellValue(l1[i][0]);
                    cell = row.CreateCell(1);
                    cell.SetCellValue(l1[i][1]);

                    cell = row.CreateCell(2);
                    cell.SetCellValue(l2[i][0]);
                    cell = row.CreateCell(3);
                    cell.SetCellValue(l2[i][1]);
                }
            }
            using (Stream stream2 = File.OpenWrite(path))
            {
                book.Write(stream2);
            }
        }

        static void ReadXLS(string path, int column1, int column2, out List<string> list1, out List<string> list2 )
        {
            list1 = new List<string>();
            list2 = new List<string>();
            using (Stream stream = File.OpenRead(path))
            {
                HSSFWorkbook book = new HSSFWorkbook(stream);
                var sheet = book.GetSheetAt(0);
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    var cell = row.GetCell(column1);
                    string s1 = cell.StringCellValue;
                    list1.Add(string.IsNullOrWhiteSpace(s1) ? "" : s1);
                    cell = row.GetCell(column2);
                    string s2 = cell.StringCellValue;
                    list2.Add(string.IsNullOrWhiteSpace(s2) ? "" : s2);
                }
            }
        }

        public static void GetAddress()
        {
            List<PointLatLng> list = new List<PointLatLng>();
            for (int i = 0; i < 20; i++)
            {
                list.Add(new PointLatLng(30 + i, 40 + i));
            }
            var adds = PlaceHelper.GetAddresses(list);
            XLSHelper.Write2Xls("D:\\2.xls", adds, list, true);
        }
    }
}
