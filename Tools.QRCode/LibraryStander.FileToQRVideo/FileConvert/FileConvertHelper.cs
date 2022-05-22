using System;
using System.IO;

namespace FileConvert
{
    public class FileConvertHelper
    {
        #region
        #endregion



        #region byte[] File

        /// <summary>
        /// 将文件转换成byte[] 数组
        /// </summary>
        /// <param name="fileUrl">文件路径文件名称</param>
        /// <returns>byte[]</returns>
        public static byte[] FileToByteBinary1(string fileUrl)
        {
            //文件转byte数组
            using (FileStream fs = new FileStream(fileUrl, FileMode.Open, FileAccess.Read))
            {
                byte[] buffur = new byte[fs.Length];

                try
                {
                    fs.Read(buffur, 0, (int)fs.Length);
                    return buffur;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (fs != null)
                    {
                        //关闭资源
                        fs.Close();
                    }
                }
            }
        }

        public static byte[] FileToByteBinary2(string fileUrl)
        {
            //利用新传来的路径实例化一个FileStream对像  
            System.IO.FileStream fs = new System.IO.FileStream(fileUrl, System.IO.FileMode.Open, System.IO.FileAccess.Read);
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

            br.Close();
            fs.Close();

            return fileByteArray;
        }




        /// <summary>
        /// 将文件转换成byte[] 数组  //无法取值
        /// </summary>
        /// <param name="fileUrl">文件路径文件名称</param>
        /// <returns>byte[]</returns>
        public static bool ByteBinaryToFile(byte[] buffur, string fileUrl)
        {
            //利用新传来的路径实例化一个FileStream对像  
            using (FileStream fs = new FileStream(fileUrl, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                //声明一个读取二进流的BinaryReader对像
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(buffur);
                    bw.Close();
                }
                return true;
            }
        }


        #endregion





        #region  byte[] 和 Stream
        /// <summary>
        /// byte[]转换成Stream
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// Stream转换成byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin); // 设置当前流的位置为流的开始
            return bytes;
        }
        #endregion


        #region  文件 和 Stream
        /// <summary>
        /// 从文件读取Stream，     思路=文件-字节-流
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Stream FileToStream(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read); // 打开文件
            byte[] bytes = new byte[fileStream.Length]; // 读取文件的byte[]
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            Stream stream = new MemoryStream(bytes); // 把byte[]转换成Stream
            return stream;
        }

        /// <summary>
        /// 将Stream写入文件    思路=流-字节，文件-写字节
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="path"></param>
        public void StreamToFile(Stream stream, string path)
        {
            byte[] bytes = new byte[stream.Length]; // 把Stream转换成byte[]
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin); // 设置当前流的位置为流的开始
            FileStream fs = new FileStream(path, FileMode.Create); // 把byte[]写入文件
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }
        #endregion
    }
}