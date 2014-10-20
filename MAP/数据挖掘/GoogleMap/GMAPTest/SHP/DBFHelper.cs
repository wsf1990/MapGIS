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
                    //读取记录
                    while (br.BaseStream.Position != br.BaseStream.Length)
                    {
                        for (int i = 0; i < Fields.Count; i++)
                        {
                            var field = Fields[i];
                            var con = br.ReadBytes(field.FieldLength);
                            var str = Encoding.ASCII.GetString(con);
                        }
                    }
                }
            }
        }
    }
}
