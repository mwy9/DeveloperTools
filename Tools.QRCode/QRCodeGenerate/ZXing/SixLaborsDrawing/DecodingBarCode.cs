using FileConvert;
using System.Drawing;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace QRCodeGenerate.SixLaborsDrawing
{

    public class ZxingHelper
    {
        ///<summary>
        ///解码 
        ///</summary>
        ///<paramname="pictureBox1"></param>
        public static string Decode()
        {

            string imgPath = "";
            Bitmap bitmapQR = new Bitmap(imgPath);

            var byteFileData = FileConvertHelper.FileToByteBinary1(imgPath);

            using (var image = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(byteFileData))
            {
                var reader11 = new ZXing.ImageSharp.BarcodeReader<SixLabors.ImageSharp.PixelFormats.Rgba32>();
                var result11 = reader11.Decode(image);
                var text = result11.Text;
            }


            LuminanceSource luminanceSource2 = new RGBLuminanceSource(BitmapConvertHelper.ToByteArray(bitmapQR),
               bitmapQR.Width, bitmapQR.Height);

            Binarizer binarizer2 = new HybridBinarizer(luminanceSource2);
            BinaryBitmap binaryBitmap2 = new BinaryBitmap(binarizer2);


            //RGBLuminanceSource rgbLuminanceSource3 = new RGBLuminanceSource(byteFileData, 360, 360); 
            // (new HybridBinarizer(rgbLuminanceSource3)
            LuminanceSource luminanceSource = new RGBLuminanceSource(byteFileData, 360, 360); //(bitmapQR);
            Binarizer binarizer = new HybridBinarizer(luminanceSource);
            BinaryBitmap binaryBitmap = new BinaryBitmap(binarizer);
            QRCodeReader reader = new QRCodeReader();
            Result result = reader.decode(binaryBitmap);



            var strContent = result.Text;
            return strContent;



            //LuminanceSource source;
            //source = new BitmapLuminanceSource(image);
            //BinaryBitmap bitmap = new BinaryBitmap(new HybridBinarizer(source));
            //Result result = new MultiFormatReader().decode(bitmap);


        }




    }
}
