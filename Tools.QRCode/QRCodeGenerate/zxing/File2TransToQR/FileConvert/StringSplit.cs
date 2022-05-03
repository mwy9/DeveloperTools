using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileConvert
{
    public class StringSplit
    {
        /// <summary>
        /// 按长度分割字符串，汉字按一个字符算
        /// </summary>
        /// <param name="SourceString"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static ArrayList SplitLength(string SourceString, int Length)
        {
            //List<string> DestString = new List<string>();
            ArrayList list = new ArrayList();
            for (int i = 0; i < SourceString.Trim().Length; i += Length)
            {
                if ((SourceString.Trim().Length - i) >= Length)
                    list.Add(SourceString.Trim().Substring(i, Length));
                else
                    list.Add(SourceString.Trim().Substring(i, SourceString.Trim().Length - i));
            }
            return list;
        }
    }
}
