using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest
{
    public class CommonHelper
    {
        public static string GetMd5(string str)
        {
            MD5CryptoServiceProvider.Create(str).Hash();
        }
    }
}
