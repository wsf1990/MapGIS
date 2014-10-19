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
        public void ImportShapeFileData()
        {
            //读Shp文件头开始  
            using (Stream stream = File.OpenRead("shp/bou2_4l.shp"))
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    var head = GetHead(br);//读取头
                    //读取正文
                    var record = CommonHelper.ChangeOrder(br.ReadInt32());//第一个记录  记录号

                    var contentLength = CommonHelper.ChangeOrder(br.ReadInt32());//记录长度

                    var shapeType = br.ReadInt32();//几何类型

                    if (shapeType == 3)
                    {
                        var con1 = GetPolyLine(br);
                    }

                    if(br.BaseStream.Position == br.BaseStream.Length)
                    {
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// 读取shp头
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        ShpHead GetHead(BinaryReader br)
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
        /// 读取一个记录项  直线
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        PolyLine GetPolyLine(BinaryReader br)
        {
            PolyLine line = new PolyLine();
            line.Box = new double[4];
            line.Box[0] = br.ReadDouble();
            line.Box[1] = br.ReadDouble();
            line.Box[2] = br.ReadDouble();
            line.Box[3] = br.ReadDouble();

            line.NumParts = br.ReadInt32();

            line.NumPoints = br.ReadInt32();

            line.Parts = new int[line.NumParts];
            int l = 0;
            while(l < line.NumParts)
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
        
    }
}
