using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            string strDirPath = @"qrcode_image\";
            Directory.CreateDirectory(strDirPath);

            CreateQRCode.File2QRVideoHelper.Run(@"test.exe",strDirPath);
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
