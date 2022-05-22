// See https://aka.ms/new-console-template for more information
using LibraryStander.FileToQRVideo;

Console.WriteLine("Hello, World!");

string strJsonConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"/qrcodegenerate.config";
List<string> strJsonConfig = System.IO.File.ReadLines(strJsonConfigPath).ToList();

ConfigModel configModel = new ConfigModel();
var configObject = QRVideoEncoding.JsonToObject(strJsonConfig[0], configModel);
configModel = configObject as ConfigModel;

string dirOutput = configModel.OutputDirPath;
string dirOutputTemp = dirOutput + @"temp/";


//已存在则删除后创建
if (Directory.Exists(dirOutput))
{
    Directory.Delete(dirOutput, true);
}
Thread.Sleep(10);
Directory.CreateDirectory(dirOutput);
Thread.Sleep(10);
Directory.CreateDirectory(dirOutputTemp);

QRVideoEncoding.Run(configModel);

//创建后删除临时目录
if (Directory.Exists(dirOutputTemp))
{
    Directory.Delete(dirOutputTemp, true);
}