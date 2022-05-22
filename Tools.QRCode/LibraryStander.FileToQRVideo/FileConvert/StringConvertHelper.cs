using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConvert
{
    internal class StringConvertHelper
    {
        
        //byte 和 char 
        // C#  byte 和 char 可以认为是等价的。但是在文本显示的时候有差异。 
        public void mmm()
        {
             
            //c# 使用的是unicode字符集，应该和为ascii相互转换 只能转换到字符的unicode编码，或者由unicode编码转换为字符
            //转换方法如一楼所写
            //字符变数字

            char a = 'a';

            //char转int
            int ua = (int)a;

            //int转char
            a = (char)ua;

        }


        /*C# byte[]转char
        问题描述
        在做TCP传输的时候，需要在C#中接到到C++中处理得到的unsigned char类型（范围0~255）数据。
        C#中接收到的数据类型是byte[]，直接在C#中用BitConverter.ToChar函数发现得到的数据与发送的并不一致。

        解决方法
        调试后发现，C++中char类型占的空间是1字节，8位；而C#中ToChar是4字节，32位。
        直访问byte[]即可，比如要访问的char数据位于byte[]中的第i个位置，直接用byte[i-1]即可访问该数据，
        并可直接参与float运算。
         */










        #region byte[] 字符串
        //如byte[] b = new byte[255]; b[1] = 255到字符串

        //System.Text.Encoding.Default
        //System.Text.Encoding.ASCII
        //System.Text.Encoding.UTF8.GetString(bytes).TrimEnd('\0')给字符串加上结束标识

        /// <summary>
        /// byte[] 转成string
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="isWrite"></param>
        /// <returns></returns>
        public static string ByteBinaryToString(byte[] byteArray, bool isWrite = false)
        {
            string str = System.Text.Encoding.Default.GetString(byteArray);
            if (isWrite)
            {
                File.WriteAllText("myFile.txt", str);
            }
            return str;
        }

        /// <summary>
        ///  string类型转成byte[]
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isWrite"></param>
        /// <returns></returns>
        public static byte[] StringToByteBinary(string str, bool isWrite = false)
        {
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(str);
            if (isWrite)
            {
                File.WriteAllBytes("myFile2.txt", byteArray);
            }
            return byteArray;
        }



        //string类型转成ASCII byte[]：
        //（"01" 转成 byte[] = new byte[]{ 0x30,0x31}）
        public static string ByteBinaryASCIIToString(byte[] byteArraye)
        {
            string str = System.Text.Encoding.ASCII.GetString(byteArraye);
            return str;
        }

       //ASCIIbyte[]转成string：
       //（byte[] = new byte[]{ 0x30, 0x31} 转成"01"）
        public static byte[] StringToByteBinaryASCII(string str)
        {
            byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(str);
            return byteArray;
        }



        //byte[]转16进制格式string：
        //new byte[]{ 0x30, 0x31}转成"3031":
        public static string ByteBinaryToStringHex(byte[] bytes)
        {
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                }

                hexString = strB.ToString();
            }
            return hexString;
        }

        //16进制格式string 转byte[]：
        public static byte[] StringHexToByteBinary(string hexString)
        {
            byte[] byteArray = System.Text.Encoding.ASCII.GetBytes("");
            return byteArray;
        }
        #endregion 



        #region byte[] Base64
        //需要注意:转换出来的string可能会包含 '+'，'/' ， '=' 所以如果作为url地址的话，需要进行encode。
        public static string ByteBinaryToBase64(byte[] bytes)
        {
            string str = Convert.ToBase64String(bytes);
            return str;
        }
        public static byte[] Base64ToByteBinary(string str)
        {
            byte[] decBytes = Convert.FromBase64String(str);
            return decBytes;
        }
        #endregion 


    }
}
