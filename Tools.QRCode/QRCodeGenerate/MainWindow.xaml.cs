using CreateQRCode;
using LibraryStander.FileToQRVideo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
