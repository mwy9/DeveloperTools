using FileConvert;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace CreateQRCode
{
    public  class File2QRHelper
    {
        public static string FFMpegPath = @"ffmpeg\ffmpeg.exe";

        public static string QRImagePreName = "images";
        public static string QRImageType = ".png";
        public static string QRVideoType = ".mp4";
        public static string QRVideoFrame = "5";


        public static int LengthQRString = 500;
        public static int QRImageWidth = 500;
        public static int QRImageHeigth = 500;



        public static void Run(string nameFile,string dirOutput)
        {
            //File转Byte
            var byteFileData1 = FileConvertHelper.FileToByteBinary1(nameFile);
            var byteFileData2 = FileConvertHelper.FileToByteBinary2(nameFile);

            //Byte转File
            var fileData = FileConvertHelper.ByteBinaryToFile(byteFileData1,nameFile+".7z");

            //Byte转String
            var stringFileData1 = StringConvertHelper.ByteBinaryToString(byteFileData1);
            var stringFileData2 = StringConvertHelper.ByteBinaryASCIIToString(byteFileData2);

            //Byte转Base64
            var base64FileData1 = StringConvertHelper.ByteBinaryToBase64(byteFileData1);
            var base64FileData2 = StringConvertHelper.ByteBinaryToBase64(byteFileData2);


            //分割文件String
            var strFileDataList = StringSplit.SplitLength(base64FileData1, LengthQRString);


            int countQRCode = 0;
            //生成二维码
            foreach(var SplitedFile in strFileDataList)
            {
                //加编号
                var newSplitedFile = countQRCode++ + "&&&" + SplitedFile;

                //string转二维码
                Bitmap bitmapSplitedFile = CreateQRBitmap(newSplitedFile, QRImageWidth, QRImageHeigth);

                //保存到本地
                bitmapSplitedFile.Save(dirOutput + QRImagePreName + countQRCode + QRImageType, ImageFormat.Png);
            }

            string pathOutVideo = Guid.NewGuid().ToString() + QRVideoType;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" -r ");
            stringBuilder.Append(QRVideoFrame);
            stringBuilder.Append(" -f image2 -i ");
            stringBuilder.Append(Path.GetFullPath(dirOutput)+ QRImagePreName);
            stringBuilder.Append("%d.png -c:v libx264 ");
            stringBuilder.Append(Path.GetFullPath(dirOutput) + pathOutVideo);
            stringBuilder.Append("");


            string param = stringBuilder.ToString();

            TransImageToVideo(dirOutput, param);
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


        //FFMpeg合成视频
       
        public static void TransImageToVideo(string dirOutput, string strParam )
        {
            Process process = new Process();
            process.StartInfo.WorkingDirectory = Path.GetFullPath(dirOutput);
            process.StartInfo.FileName = FFMpegPath;
            process.StartInfo.Arguments = strParam;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }
    }
}