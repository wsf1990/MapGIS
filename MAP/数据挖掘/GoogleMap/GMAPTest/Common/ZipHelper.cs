using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

namespace GMAPTest.Common
{
    /// <summary>
    /// 压缩帮助类
    /// </summary>
    public class ZipHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFile"></param>
        /// <param name="strZip"></param>
        public static void ZipFile(string strFile, string strZip)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
                strFile += Path.DirectorySeparatorChar;
            using (var s = new ZipOutputStream(File.Create(strZip)))
            {
                s.SetLevel(6); // 0 - store only to 9 - means best compression
                Zip(strFile, s, strFile);
            }
        }

        /// <summary>
        /// 递归压缩文件和文件夹（空文件夹不进行压缩）
        /// </summary>
        /// <param name="strFile"></param>
        /// <param name="s"></param>
        /// <param name="staticFile"></param>
        private static void Zip(string strFile, ZipOutputStream s, string staticFile)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
                strFile += Path.DirectorySeparatorChar;
            Crc32 crc = new Crc32();
            string[] filenames = Directory.GetFileSystemEntries(strFile);
            foreach (string file in filenames)
            {
                //文件夹  递归压缩
                if (Directory.Exists(file))
                {
                    Zip(file, s, staticFile);
                }
                else // 否则直接压缩文件
                {
                    //打开压缩文件
                    using (FileStream fs = File.OpenRead(file))
                    {
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                        crc.Reset();
                        crc.Update(buffer);
                        var entry = new ZipEntry(tempfile) { DateTime = DateTime.Now, Size = fs.Length, Crc = crc.Value };
                        s.PutNextEntry(entry);

                        s.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="targetFile">解压缩文件</param>
        /// <param name="fileDir">解压缩文件</param>
        /// <returns></returns>
        public static void UnZip(string targetFile, string fileDir = "")
        {
            if (string.IsNullOrWhiteSpace(fileDir))
                fileDir = Path.GetDirectoryName(targetFile);
            try
            {
                //读取压缩文件(zip文件)，准备解压缩
                using (var s = new ZipInputStream(File.OpenRead(targetFile.Trim())))
                {
                    ZipEntry theEntry;
                    string path = fileDir;//解压出来的文件保存的路径
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string dir = Path.GetDirectoryName(theEntry.Name);
                        //根目录下的第一级子文件夹的下的文件夹的名称
                        string fileName = Path.GetFileName(theEntry.Name);
                        //根目录下的文件名称
                        path = Path.Combine(fileDir, dir ?? "");
                        if (!Directory.Exists(path))
                        {
                            //在指定的路径创建文件夹
                            Directory.CreateDirectory(path);
                        }
                        //以下为解压缩zip文件的基本步骤
                        //基本思路就是遍历压缩文件里的所有文件，创建一个相同的文件。
                        if (!string.IsNullOrWhiteSpace(fileName))
                        {
                            using (var streamWriter = File.Create(Path.Combine(path, fileName)))
                            {
                                int size = 2048;
                                var data = new byte[size];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
