using FileConvert;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace CreateQRCode
{
    public  class File2QRVideoHelper
    {
        public static void Run(string nameFile,string pathOutDir)
        {
            //文件转Byte
            var byteFileData = File2Byte.GetFileData(nameFile);

            //Byte转Base64
            string strFileData = Convert.ToBase64String(byteFileData);

            //分割文件String
            var strFileDataList = StringSplit.SplitLength(strFileData,500);

            int countQRCode = 0;
            //生成二维码
            foreach(var SplitedFile in strFileDataList)
            {
                //加编号
                var newSplitedFile = countQRCode++ + "&&&" + SplitedFile;

                //string转二维码
                Bitmap bitmapSplitedFile = CreateQRBitmap(newSplitedFile, 500, 500);

                //保存到本地
                bitmapSplitedFile.Save(pathOutDir + "images" + countQRCode + ".png", ImageFormat.Png);
            }


            //FFMpeg合成视频
            //ffmpeg  -r 15  -f image2 -i qrcode_image/%d.png -c:v libx264 out.mp4
            //ffmpeg  -r 15  -f image2 -i "C://AllWorkspace//3rdparty//0workspace//qrcode//qrcode_image//images%d.png' -c:v libx264 out.mp4
        }



        ///<summary>
        ///生成二维码  WriteToBitmapFile
        ///</summary>
        ///<paramname="pictureBox1"></param>
        ///<paramname="Contents"></param>
        public static Bitmap CreateQRBitmap(string Contents,int widthQR = 0, int heightQR = 0)
        {
            //创建QRCodeWriter对象
            QRCodeWriter writer = new QRCodeWriter();

            //生成的图片基本配置
            //参数:1.二维码信息 2.图片类型 3.图片宽度 4.图片长度
            BitMatrix matrix = writer.encode(Contents, BarcodeFormat.QR_CODE, widthQR, heightQR);

            Bitmap bitmap = BitMatrix2Bitmap(matrix);
            return bitmap;
        }


        public static Bitmap BitMatrix2Bitmap(BitMatrix matrix)
        {
            int widthQR = matrix.Width;
            int heightQR = matrix.Height;

            Bitmap bitmap = new Bitmap(widthQR, heightQR, PixelFormat.Format32bppArgb);

            for (int x = 0; x < widthQR; x++)
            {
                for (int y = 0; y < heightQR; y++)
                {
                    bitmap.SetPixel(x, y, matrix[x, y] ?
                        Color.FromArgb(0xFF, 0x00, 0x00, 0x00) : Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
                }
            }

            return bitmap;
        }



        public static BitMatrix Bitmap2BitMatrix(Bitmap bitmap)
        {
            int widthQR = bitmap.Width;
            int heightQR = bitmap.Height;

            return null;

        }

        ///<summary>
        ///解码
        ///</summary>
        ///<paramname="pictureBox1"></param>
        public static string Decode(Bitmap bitmapQR)
        {
            //QRCodeReader reader = new QRCodeReader();
            //Result result = reader.decode(bitmapQR);
            //var strContent = result.Text;
            //return strContent;

            return "";
        }
    }
}