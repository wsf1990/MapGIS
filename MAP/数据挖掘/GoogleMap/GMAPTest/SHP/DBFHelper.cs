using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.SHP
{
    /// <summary>
    /// 读取SHP的属性文件  .dbf
    /// </summary>
    public class DBFHelper
    {
        public static void ImportDBFFromFile()
        {
            using(Stream stream = File.OpenRead("shp/bou2_4l.dbf"))
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    var b = br.ReadByte();//表示当前的版本信息   FoxBASE+/Dbase III plus, no memo 

                    var yy = br.ReadByte();//YY

                    var mm = br.ReadByte();//MM

                    var dd = br.ReadByte();//DD

                    var count = br.ReadInt32();//记录条数

                    var headcount = br.ReadInt16();//头中的字节数

                    var length = br.ReadInt16();//一条记录中的字节数

                    br.ReadInt16();

                    var action = br.ReadByte();//未完成的操作

                    var bj = br.ReadByte();//dBASE IV编密码标记。

                    for (int i = 0; i < 12; i++)//保留字节，用于多用户处理时使用。
                    {
                        br.ReadByte();
                    }

                    var mdx = br.ReadByte();//DBF文件的MDX标识

                    var language = br.ReadByte();//Language driver ID.

                    br.ReadInt16();

                    int FieldCount = (headcount - 33) / 8;//字段个数
                    for (int i = 0; i < FieldCount; i++)//开始读取字段信息
                    {
                        
                    }
                }
            }
        }
    }
}
