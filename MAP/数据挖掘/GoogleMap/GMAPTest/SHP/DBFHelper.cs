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
            using (Stream stream = File.OpenRead("shp/bou2_4l.dbf"))
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

                    br.ReadBytes(12);//保留字节，用于多用户处理时使用。

                    var mdx = br.ReadByte();//DBF文件的MDX标识

                    var language = br.ReadByte();//Language driver ID.

                    br.ReadInt16();

                    int FieldCount = (headcount - 33) / 32;//字段个数
                    List<DBFField> Fields = new List<DBFField>();
                    for (int i = 0; i < FieldCount; i++)//开始读取字段信息
                    {
                        DBFField field = new DBFField();
                        var bytes = br.ReadBytes(11);
                        field.FieldName = Encoding.ASCII.GetString(bytes); //字段名称
                        field.FieldType = Encoding.ASCII.GetString(new byte[] { br.ReadByte() });//字段类型
                        br.ReadBytes(4);
                        field.FieldLength = br.ReadByte();//长度
                        field.FieldPricision = br.ReadByte();//精度
                        br.ReadBytes(2);
                        field.FieldID = br.ReadByte();//工作区ID
                        br.ReadBytes(10);
                        field.FieldMdx = br.ReadByte();
                        Fields.Add(field);
                    }
                    var last = br.ReadByte();//文件头最后一个字节
                    //读取记录
                    while (br.BaseStream.Position != br.BaseStream.Length)
                    {
                        var tt = br.ReadByte();//第一个字节不读
                        for (int i = 0; i < Fields.Count; i++)
                        {
                            var field = Fields[i];
                            var con = br.ReadBytes(field.FieldLength);
                            var s = Encoding.GetEncoding("gb2312").GetString(con);//读取字段内容，任何格式均可以以此种读法
                        }
                    }
                }
            }
        }

        public static void WriteDBF()
        {
            using (Stream stream = new FileStream("shp/test.dbf", FileMode.Create, FileAccess.Write))
            {
                using(BinaryWriter bw = new BinaryWriter(stream))
                {
                    byte tempByte = 0x03;
                    byte[] tempBytes;
                    bw.Write(tempByte);

                    tempBytes = new byte[3];
                    tempBytes[0] = Convert.ToByte(14);
                    tempBytes[1] = Convert.ToByte(10);
                    tempBytes[2] = Convert.ToByte(20);
                    bw.Write(tempBytes);

                    int rowCount = 1000;
                    bw.Write(rowCount);

                    int tempInt = 33 + 2 * 32;//2列   文件头中的字节数。
                    bw.Write((Int16)tempInt);

                    List<DBFField> Fields = new List<DBFField>();
                    Fields.Add(new DBFField() { FieldID = 0, FieldLength = (byte)10, FieldMdx = 0, FieldName = "BOU2_4M_", FieldType = "N", FieldPricision = 0 });
                    Fields.Add(new DBFField() { FieldID = 0, FieldLength = (byte)10, FieldMdx = 0, FieldName = "BOU2_4M_ID", FieldType = "N", FieldPricision = 0 });
                    //一条记录中的字节长度。
                    int length = 0;
                    Fields.ForEach(s => length += s.FieldLength);
                    bw.Write((Int16)length);

                    tempBytes = new byte[20]; 
                    bw.Write(tempBytes);

                    Fields.ForEach(s =>
                    {
                        var bytes = ConvertStringToBytes(s.FieldName, 11);
                        bw.Write(bytes);

                        bw.Write((byte)(s.FieldType[0]));

                        bw.Write(new byte[4]);

                        bw.Write(s.FieldLength);

                        bw.Write(s.FieldPricision);

                        bw.Write(new byte[14]);
                    });
                    tempByte = 0x0D;
                    bw.Write(tempByte);

                    //记录
                    List<Test> list = new List<Test>();
                    for (int i = 0; i < 10000; i++)
                    {
                        list.Add(new Test() { BOU2_4M_ = i, BOU2_4M_ID = i });
                    }

                    list.ForEach(s => 
                        {
                            //每一行第一个字节默认为20
                            tempByte = (byte)32;
                            bw.Write(tempByte);//记录第一个为空
                            bw.Write(ConvertStringToBytes(s.BOU2_4M_.ToString(), 10));
                            bw.Write(ConvertStringToBytes(s.BOU2_4M_ID.ToString(), 10));
                        });
                    //tempByte = 0x1A;
                    //bw.Write(tempByte);
                }
            }
        }
        static byte[] ConvertStringToBytes(string str, int length)
        {
            //var bytes = Encoding.GetEncoding("gb2312").GetBytes(str);
            //if(bytes.Length == length)
            //    return bytes;
            //byte[] temp = new byte[length];
            //for (int i = 0; i < length; i++)
            //{
            //    temp[i] = i >= bytes.Length ? (byte)0 : bytes[i];
            //}
            //return temp;
            byte[] result = null;
            byte[] tempBytes = UTF8Encoding.GetEncoding("gb2312").GetBytes(str);
            if (tempBytes.Length == length)
            {
                result = tempBytes;
            }
            else if (tempBytes.Length > length)
            {
                result = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    result[i] = tempBytes[i];
                }
            }
            else if (tempBytes.Length < length)
            {
                result = new byte[length];
                for (int i = 0; i < tempBytes.Length; i++)
                {
                    result[i] = tempBytes[i];
                }
            }
            return result;
        }
    }
    /// <summary>
    /// DBF测试
    /// </summary>
    public class Test
    {
        public int BOU2_4M_ { get; set; }
        public int BOU2_4M_ID { get; set; }
    }
}
