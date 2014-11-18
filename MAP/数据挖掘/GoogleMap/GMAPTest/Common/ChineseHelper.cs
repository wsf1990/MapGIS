using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.International.Converters.PinYinConverter;

namespace GMAPTest.Common
{
    public class ChineseHelper
    {
        /// <summary>
        /// 获取单个汉字拼音
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static ReadOnlyCollection<string> GetOneCharPinYin(char word)
        {
            var chineseChar = new ChineseChar(word);
            return chineseChar.Pinyins;
        }

        /// <summary>
        /// 判断字符串是否包含中文
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool CheckContainChinese(string word)
        {
            bool ch = false;
            word.ToList().ForEach(s =>
                {
                    if (ChineseChar.IsValidChar(s))
                        ch = true;
                });
            return ch;
        }
    }
}
