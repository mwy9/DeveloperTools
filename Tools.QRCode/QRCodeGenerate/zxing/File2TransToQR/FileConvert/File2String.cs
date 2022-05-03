using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
#region 将文件转化为字符串
string fileStr = FileToBinary(@"C:\Users\wjh\Desktop\321.xlsx");
MessageBox.Show(fileStr);
#endregion
#region 将字符串转为文件
BinaryToFile(@"E:\123\123.txt", fileStr);
#endregion
*/
namespace FileConvert
{
    public class File2String
    {
        /// <summary>  
        /// 将传进来的文件转换成字符串  
        /// </summary>  
        /// <param name="FilePath">待处理的文件路径(本地或服务器)</param>  
        /// <returns></returns>
        public static string FileToBinary(string filePath)
        {
            //利用新传来的路径实例化一个FileStream对像  
            System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //得到对象的大小
            int fileLength = Convert.ToInt32(fs.Length);
            //声明一个byte数组 
            byte[] fileByteArray = new byte[fileLength];
            //声明一个读取二进流的BinaryReader对像
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            for (int i = 0; i < fileLength; i++)
            {
                //将数据读取出来放在数组中 
                br.Read(fileByteArray, 0, fileLength);
            }
            //装数组转换为String字符串
            string strData = Convert.ToBase64String(fileByteArray);
            br.Close();
            fs.Close();
            return strData;
        }

        /// <summary>  
        /// 将传进来的字符串保存为文件  
        /// </summary>  
        /// <param name="path">需要保存的位置路径</param>  
        /// <param name="binary">需要转换的字符串</param>  
        public static void BinaryToFile(string path, string binary)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            //利用新传来的路径实例化一个FileStream对像  
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
            //实例化一个用于写的BinaryWriter  
            bw.Write(Convert.FromBase64String(binary));
            bw.Close();
            fs.Close();
        }
    }
}