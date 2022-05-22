using FileConvert;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace LibraryStander.FileToQRVideo
{
    public class QRVideoEncoding
    {
        public static string FFMpegPath = "";

        public static string QRImagePreName = "images";
        public static string QRImageType = ".png";
        public static string QRVideoType = ".mp4";
        public static string QRVideoFrame = "5";


        public static int LengthQRString = 500;
        public static int QRImageWidth = 500;
        public static int QRImageHeigth = 500;

        public static void Run(ConfigModel configModel)
        {
            string nameFile = configModel.InputFilePath;
            string dirOutput = configModel.OutputDirPath;
            string dirOutputTemp = dirOutput + @"temp\";


            FFMpegPath = configModel.FFmpegPath;
            LengthQRString = int.Parse(configModel.QRLengthPath);
            QRImageHeigth = int.Parse(configModel.QRWidthHeight);
            QRImageWidth = int.Parse(configModel.QRWidthHeight);
            QRVideoFrame = configModel.QRVideoFrame;



            //File转Byte
            var byteFileData1 = FileConvertHelper.FileToByteBinary1(nameFile);
            var byteFileData2 = FileConvertHelper.FileToByteBinary2(nameFile);

            //Byte转File
            //var fileData = FileConvertHelper.ByteBinaryToFile(byteFileData1, nameFile + ".7z");

            //Byte转String
            //var stringFileData1 = StringConvertHelper.ByteBinaryToString(byteFileData1);
            //var stringFileData2 = StringConvertHelper.ByteBinaryASCIIToString(byteFileData2);

            //Byte转Base64
            var base64FileData1 = StringConvertHelper.ByteBinaryToBase64(byteFileData1);


            //分割文件String
            var strFileDataList = StringSplit.SplitLength(base64FileData1, LengthQRString);


            int countQRCode = 0;
            //生成二维码
            foreach (var SplitedFile in strFileDataList)
            {
                //加编号
                var newSplitedFile = countQRCode++ + "&&&" + SplitedFile;

                //string转二维码01
                Bitmap bitmapSplitedFile = CreateQRBitmap(newSplitedFile, QRImageWidth, QRImageHeigth);


                //string转二维码02


                QrCodeEncodingOptions qr = new QrCodeEncodingOptions()
                {
                    //CharacterSet = "UTF-8",
                    //ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.H,
                    Height = 500,
                    Width = 500,
                    //Margin = 10
                };
                var barcodeWriterZXingImageSharp = new ZXing.ImageSharp.BarcodeWriter<SixLabors.ImageSharp.PixelFormats.Rgba32>
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = qr
                };


                //对数据量有限制 newSplitedFile
                var imageZXingImageSharpPng = barcodeWriterZXingImageSharp.WriteAsImageSharp<SixLabors.ImageSharp.PixelFormats.Rgba32>(newSplitedFile);
                using var fsZXingImageSharpPng = new FileStream("00000000001.png", FileMode.Create);
                var encoderPng = new SixLabors.ImageSharp.Formats.Png.PngEncoder();
                imageZXingImageSharpPng.Save(fsZXingImageSharpPng, encoderPng);

                var imageZXingImageSharpJpg = barcodeWriterZXingImageSharp.Write(newSplitedFile);
                using var fsZXingImageSharpJpg = new FileStream("00000000001.jpg", FileMode.Create);
                var encoderJpg = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder { Quality = 80 };
                imageZXingImageSharpJpg.Save(fsZXingImageSharpJpg, encoderJpg);
                //string转二维码02

                //保存到本地
                bitmapSplitedFile.Save(dirOutputTemp + QRImagePreName + countQRCode + QRImageType, ImageFormat.Png);
            }

            string pathOutVideo = Guid.NewGuid().ToString() + QRVideoType;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" -r ");
            stringBuilder.Append(QRVideoFrame);
            stringBuilder.Append(" -f image2 -i ");
            stringBuilder.Append(Path.GetFullPath(dirOutputTemp) + QRImagePreName);
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
        public static Bitmap CreateQRBitmap(string Contents, int widthQR = 0, int heightQR = 0)
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
        public static void TransImageToVideo(string dirOutput, string strParam)
        {
            Process process = new Process();
            process.StartInfo.WorkingDirectory = Path.GetFullPath(dirOutput);
            process.StartInfo.FileName = FFMpegPath;
            process.StartInfo.Arguments = strParam;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
        }




        // 从一个对象信息生成Json串
        public static string ObjectToJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
        }

        // 从一个Json串生成对象信息
        public static object JsonToObject(string jsonString, object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return serializer.ReadObject(mStream);
        }
    }


    public class ConfigModel
    {
        public string FFmpegPath { get; set; }
        public string InputFilePath { get; set; }
        public string OutputDirPath { get; set; }
        public string QRWidthHeight { get; set; }
        public string QRLengthPath { get; set; }
        public string QRVideoFrame { get; set; }
    }


}