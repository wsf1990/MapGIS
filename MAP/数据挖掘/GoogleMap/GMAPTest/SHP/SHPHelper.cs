using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.SHP
{
    /// <summary>
    /// shp帮助类
    /// </summary>
    public class SHPHelper
    {
        /// <summary>
        /// 读取SHP文件
        /// </summary>
        public static ShpFileContent ImportShapeFileData(string fileName)
        {
            //读Shp文件头开始  
            using (Stream stream = File.OpenRead(fileName))
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    ShpFileContent Content = new ShpFileContent();
                    Content.Head = GetHead(br);//读取头
                    //读取正文

                    Content.PolyLines = new List<ShpPolyLine>();
                    Content.Points = new List<ShpPoint>();
                    Content.Polygons = new List<ShpPolygon>();
                    //循环读取记录  
                    while (br.BaseStream.Position != br.BaseStream.Length)
                    {
                        var record = CommonHelper.ChangeOrder(br.ReadInt32());//记录号

                        var contentLength = CommonHelper.ChangeOrder(br.ReadInt32());//记录长度
                        var shapeType = br.ReadInt32();//几何类型
                        switch (shapeType)//根据不同几何类型进行处理
                        {
                            case  1:
                                var point = GetPoint(br, record);
                                Content.Points.Add(point);
                                break;
                            case 3:
                                var line = GetPolyLine(br, record);
                                Content.PolyLines.Add(line);
                                break;
                            case 5:
                                var polygon = GetPolygon(br, record);
                                Content.Polygons.Add(polygon);
                                break;
                            default:
                                Console.WriteLine("未知类型");
                                break;
                        }
                    }
                    return Content;
                }
            }
        }
        /// <summary>
        /// 写内容到SHP文件
        /// </summary>
        /// <param name="fileName"></param>
        public static void WriteShapeToFile(ShpFileContent Content, string fileName)
        {
            using(Stream stream = File.OpenWrite(fileName))
            {
                using(BinaryWriter bw = new BinaryWriter(stream))
                {
                    WriteShpHead(Content.Head, bw);
                    //先插入Point
                    WriteShpPoint(Content.Points, bw, 0);
                    WriteShpPolyLine(Content.PolyLines, bw, Content.Points.Count);
                    WriteShpPolygon(Content.Polygons, bw, Content.Points.Count + Content.PolyLines.Count);
                }
            }
        }

        #region 1. Read Shp
        /// <summary>
        /// 读取shp头
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public static ShpHead GetHead(BinaryReader br)
        {
            ShpHead head = new ShpHead();
            var read = br.ReadInt32();//读取FileCode  
            head.FileCode = CommonHelper.ChangeOrder(read);
            //var str = Encoding.Default.GetString(bytes);
            //System.Windows.Forms.MessageBox.Show(str);

            for (int i = 0; i < 5; i++)//五个没有使用的情况
            {
                br.ReadInt32();
            }
            read = br.ReadInt32();
            head.FileLength = CommonHelper.ChangeOrder(read);//文件长度

            head.Version = br.ReadInt32();//版本号

            head.ShpType = br.ReadInt32();//几何类型

            head.Xmin = br.ReadDouble();
            head.Ymin = br.ReadDouble();
            head.Xmax = br.ReadDouble();
            head.Ymax = br.ReadDouble();
            head.Zmin = br.ReadDouble();
            head.Zmax = br.ReadDouble();
            head.Mmin = br.ReadDouble();//所谓Measure值，是用于存储需要的附加数据，可以用来记录各种数据，例如权值、道路长度等信息
            head.Mmax = br.ReadDouble();
            return head;
        }

        /// <summary>
        /// 读取点状目标
        /// </summary>
        /// <param name="br"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        static ShpPoint GetPoint(BinaryReader br, int record)
        {
            ShpPoint point = new ShpPoint();
            point.RecordNumber = record;
            PointLatLng p = new PointLatLng();
            p.Lng = br.ReadDouble();
            p.Lat = br.ReadDouble();
            point.Point = p;
            return point;
        }

        /// <summary>
        /// 读取一个记录项  直线
        /// </summary>
        /// <param name="br"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        static ShpPolyLine GetPolyLine(BinaryReader br, int record)
        {
            ShpPolyLine line = new ShpPolyLine();
            line.RecordNumber = record;//保存记录号
            line.Box = new double[4];
            line.Box[0] = br.ReadDouble();
            line.Box[1] = br.ReadDouble();
            line.Box[2] = br.ReadDouble();
            line.Box[3] = br.ReadDouble();

            line.NumParts = br.ReadInt32();

            line.NumPoints = br.ReadInt32();

            line.Parts = new int[line.NumParts];
            int l = 0;
            while (l < line.NumParts)
            {
                line.Parts[l++] = br.ReadInt32();
            }
            l = 0;
            line.Points = new PointLatLng[line.NumPoints];
            while (line.NumPoints > l)
            {
                PointLatLng point = new PointLatLng();
                point.Lng = br.ReadDouble();
                point.Lat = br.ReadDouble();
                line.Points[l++] = point;
            }
            return line;
        }
        /// <summary>
        /// 读取一个记录项  直线
        /// </summary>
        /// <param name="br"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        static ShpPolygon GetPolygon(BinaryReader br, int record)
        {
            var polygon = new ShpPolygon();
            polygon.RecordNumber = record;//保存记录号
            polygon.Box = new double[4];
            polygon.Box[0] = br.ReadDouble();
            polygon.Box[1] = br.ReadDouble();
            polygon.Box[2] = br.ReadDouble();
            polygon.Box[3] = br.ReadDouble();

            polygon.NumParts = br.ReadInt32();

            polygon.NumPoints = br.ReadInt32();

            polygon.Parts = new int[polygon.NumParts];
            int l = 0;
            while (l < polygon.NumParts)
            {
                polygon.Parts[l++] = br.ReadInt32();
            }
            l = 0;
            polygon.Points = new PointLatLng[polygon.NumPoints];
            while (polygon.NumPoints > l)
            {
                var point = new PointLatLng();
                point.Lng = br.ReadDouble();
                point.Lat = br.ReadDouble();
                polygon.Points[l++] = point;
            }
            return polygon;
        } 
        #endregion

        #region 2. Write Shp
        /// <summary>
        /// 写SHP头
        /// </summary>
        /// <param name="head"></param>
        /// <param name="bw"></param>
        static void WriteShpHead(ShpHead head, BinaryWriter bw)
        {
            bw.Write(CommonHelper.ChangeOrder(head.FileCode));
            bw.Write(0); bw.Write(0); bw.Write(0); bw.Write(0); bw.Write(0);
            bw.Write(CommonHelper.ChangeOrder(head.FileLength));
            bw.Write(head.Version);
            bw.Write(head.ShpType);
            bw.Write(head.Xmin);
            bw.Write(head.Ymin);
            bw.Write(head.Xmax);
            bw.Write(head.Ymax);
            bw.Write(head.Zmin);
            bw.Write(head.Zmax);
            bw.Write(head.Mmin);
            bw.Write(head.Xmax);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="bw"></param>
        /// <param name="index">起始记录号</param>
        static void WriteShpPoint(List<ShpPoint> points, BinaryWriter bw, int index)
        {
            for (int i = 0; i < points.Count; i++)
            {
                //一条记录
                bw.Write(CommonHelper.ChangeOrder(index + i));
                int contentLength = 0;
                bw.Write(CommonHelper.ChangeOrder(contentLength));
                bw.Write(1);
                bw.Write(points[i].Point.Lng);
                bw.Write(points[i].Point.Lat);
            }
        }
        /// <summary>
        /// 写PolyLine
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="bw"></param>
        /// <param name="index"></param>
        static void WriteShpPolyLine(List<ShpPolyLine> lines, BinaryWriter bw, int index)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                //一条记录
                bw.Write(CommonHelper.ChangeOrder(index + i));
                int contentLength = 0;
                bw.Write(CommonHelper.ChangeOrder(contentLength));
                bw.Write(3);
                var line = lines[i];
                foreach (var item in line.Box)
                {
                    bw.Write(item);
                }
                bw.Write(line.NumParts);
                bw.Write(line.NumPoints);
                for (int j = 0; j < line.NumParts; j++)
                {
                    bw.Write(line.Parts[j]);
                }
                for (int j = 0; j < line.Points.Length; j++)
                {
                    bw.Write(line.Points[j].Lng);
                    bw.Write(line.Points[j].Lat);
                }
            }
        }
        /// <summary>
        /// 写面状目标
        /// </summary>
        /// <param name="polygons"></param>
        /// <param name="bw"></param>
        /// <param name="index"></param>
        static void WriteShpPolygon(List<ShpPolygon> polygons, BinaryWriter bw, int index)
        {
            for (int i = 0; i < polygons.Count; i++)
            {
                //一条记录
                bw.Write(CommonHelper.ChangeOrder(index + i));
                int contentLength = 0;
                bw.Write(CommonHelper.ChangeOrder(contentLength));
                bw.Write(5);
                var polygon = polygons[i];
                foreach (var item in polygon.Box)
                {
                    bw.Write(item);
                }
                bw.Write(polygon.NumParts);
                bw.Write(polygon.NumPoints);
                for (int j = 0; j < polygon.NumParts; j++)
                {
                    bw.Write(polygon.Parts[j]);
                }
                for (int j = 0; j < polygon.Points.Length; j++)
                {
                    bw.Write(polygon.Points[j].Lng);
                    bw.Write(polygon.Points[j].Lat);
                }
            }
        }

        #endregion
    }
}
