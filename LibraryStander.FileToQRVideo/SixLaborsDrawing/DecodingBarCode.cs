using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System.IO;
using ZXing.Common;

namespace QRCodeGenerate.SixLaborsDrawing
{
    public class ZxingHelper
    {
        ///<summary>
        ///解码 
        ///</summary>
        public static string Decode(string imgPath = "")
        {
            //图片路径
            string inputImagePath = imgPath;
            //保存图片路径
            string outputImagePath = "C:\\Users\\Administrator\\Downloads\\output\\new.png";
            int size = 300;


            string strContent = string.Empty;

            //SkiaSharp 调用
            //若在Linux出现依赖问题，可以使用包SkiaSharp.NativeAssets.Linux.NoDependencies使用
            using (var imageSkiaSharp = SkiaSharp.SKBitmap.Decode(inputImagePath))
            {
                //设置图片新的size
                var newImg = imageSkiaSharp.Resize(new SkiaSharp.SKSizeI(50, 50), SkiaSharp.SKFilterQuality.Medium);
                using var fs = new FileStream(outputImagePath, FileMode.Create);
                newImg.Encode(fs, SkiaSharp.SKEncodedImageFormat.Png, 100);
                fs.Flush();
            }

            //SkiaSharp + ZXing.Net.Bindings.SkiaSharp 解析二维码图片
            var stream = System.IO.File.Open(inputImagePath, System.IO.FileMode.Open);
            using (var skiaImage = SkiaSharp.SKBitmap.Decode(stream))
            {
                var skiaReader = new ZXing.SkiaSharp.BarcodeReader();
                var skiaResult = skiaReader.Decode(skiaImage);
                strContent = skiaResult?.Text ?? "";
            }




            //ImageSharp 调用
            using (var imageImageSharp = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(inputImagePath))
            {
                //01 缩放
                //其中调用 Resize(width,height) 方法时，如果设置了宽或者高，然后另一个参数设置为 0
                //那么 ImageSharp 将会保持图片纵横比来进行调整大小
                imageImageSharp.Mutate(x => x.Resize(size, 0));
                imageImageSharp.Save(outputImagePath);


                //02 创建
                var barcodeWriter = new ZXing.ImageSharp.BarcodeWriter<SixLabors.ImageSharp.PixelFormats.Rgba32>
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new EncodingOptions
                    {
                        Height = 500,
                        Width = 500,
                        Margin = 10
                    }
                };



                using (var imageZXingImageSharp2 = barcodeWriter.Write(outputImagePath))
                {
                    //output.TagName = "img";
                    //output.Attributes.Clear();
                    //output.Attributes.Add("width", image.Width);
                    //output.Attributes.Add("height", image.Height);
                    //output.Attributes.Add("alt", alt);
                    //output.Attributes.Add("src", string.Format(image.ToBase64String(ImageFormats.Png)));
                    imageZXingImageSharp2.Save(outputImagePath);
                }  
            }



            //ImageSharp + ZXing.Net.Bindings.ImageSharp 解析二维码图片
            using (var imageZXingImageSharp = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(inputImagePath))
            {
                // create a barcode reader instance
                var reader = new ZXing.ImageSharp.BarcodeReader<SixLabors.ImageSharp.PixelFormats.Rgba32>();
                
                var result = reader.Decode(imageZXingImageSharp);
                if (result != null)
                {
                    var textBarcodeFormat = result.BarcodeFormat.ToString();
                    var textResult = result.Text;
                }

                // detect and decode the barcode inside the bitmap
                var luminanceSource = new ZXing.ImageSharp.ImageSharpLuminanceSource<SixLabors.ImageSharp.PixelFormats.Rgba32>(imageZXingImageSharp);
                result = reader.Decode(luminanceSource);
                if (result != null)
                {
                    var textBarcodeFormat = result.BarcodeFormat.ToString();
                    var textResult = result.Text;
                }

                strContent = result.Text;
            }

            return strContent;
        }
    }
}