using CreateQRCode;
using System.IO;
using System.Threading;
using System.Windows;

namespace QRCodeGenerate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            QRCodeGenerate.ThoughtWorks.MainWindow mainWindow = new ThoughtWorks.MainWindow();
            mainWindow.Show();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            string pathFile = @"images6.7z";
            string dirOutput = @"qrcode_image\";

            //已存在则删除后创建
            if(Directory.Exists(dirOutput))
            {
                Directory.Delete(dirOutput,true);
            }
            Thread.Sleep(30);
            Directory.CreateDirectory(dirOutput);

            File2QRHelper.Run(pathFile, dirOutput);
        }

        private void Button21_Click(object sender, RoutedEventArgs e)
        {
            WPFBatch.MainWindow mainWindow = new WPFBatch.MainWindow();
               mainWindow.Show();
        }

        private void Button22_Click(object sender, RoutedEventArgs e)
        {
            WpfGeneration.MainWindow mainWindow = new WpfGeneration.MainWindow();
            mainWindow.Show();
        }
    }
}
