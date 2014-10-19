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
                    var record = ChangeOrder(br.ReadInt32());//第一个记录  记录号

                    var contentLength = ChangeOrder(br.ReadInt32());//记录长度

                    var shapeType = br.ReadInt32();//几何类型

                    if (shapeType == 3)
                    {
                        var con1 = GetPolyLine(br);
                    }
                }
            }
        }
        /// <summary>
        /// 读取头
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        ShpHead GetHead(BinaryReader br)
        {
            ShpHead head = new ShpHead();
            var read = br.ReadInt32();//读取FileCode  
            head.FileCode = ChangeOrder(read);
            //var str = Encoding.Default.GetString(bytes);
            //System.Windows.Forms.MessageBox.Show(str);

            for (int i = 0; i < 5; i++)//五个没有使用的情况
            {
                br.ReadInt32();
            }
            read = br.ReadInt32();
            head.FileLength = ChangeOrder(read);//文件长度

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
        /// <summary>
        /// 位序转换
        /// </summary>
        /// <param name="lbt"></param>
        /// <returns></returns>
        int ChangeOrder(byte[] lbt)
        {
            //int a = 9994;
            //byte[] lbt = BitConverter.GetBytes(a);  //将int转变为byte
            byte[] bbt = new byte[4];             //用于存放big byte，维数为4
            bbt[0] = lbt[3];                       //0
            bbt[1] = lbt[2];                       //0
            bbt[2] = lbt[1];                       //39
            bbt[3] = lbt[0];                       //10
            int a = BitConverter.ToInt32(bbt, 0);
            return a;
        }
        /// <summary>
        /// 位序转换
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        int ChangeOrder(int b)
        {
            return ChangeOrder(BitConverter.GetBytes(b));
        }
    }
}
