// See https://aka.ms/new-console-template for more information
using QRCodeGenerate.SixLaborsDrawing;

Console.WriteLine("Hello, World!");


string strFilePath = @"C:\Users\Administrator\Desktop\111111\output\temp\images1.png"; 
ZxingHelper.Decode(strFilePath);

