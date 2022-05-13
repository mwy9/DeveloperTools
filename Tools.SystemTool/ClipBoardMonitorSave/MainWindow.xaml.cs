using System;
using System.Text;
using System.Windows;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Interop;
using ClipBoardMonitorSave.Win32API;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ClipBoardMonitorSave.Util;

namespace ClipBoardMonitorSave
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private HwndSource source;
        public static IntPtr mNextClipBoardViewerHWnd;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.SourceInitialized += new EventHandler(WSInitialized);






            //var dlg = new Microsoft.Win32.SaveFileDialog();
            //dlg.Filter = "PNG图片(*.png)|*.png|JPEG图片(*.jpg)|*.jpg|所有文件(*.*)|*.*";
            //var rst = dlg.ShowDialog();
            //if (rst == true)
            {
                //var fileName = dlg.FileName;
            }



            //设置数据类型
            bool flag = Clipboard.ContainsImage();


            //将图片放置到剪切板中
            //Image 
            //BitmapSource img = new Bitmap("file.png");
            // Clipboard.SetDataObject(imageItem, false);
            //Clipboard.SetImage(img);


            //判断剪切板中是否包含图片
            flag = Clipboard.ContainsImage();





            string data = "天王盖地虎!";
            Clipboard.SetDataObject(data);

            string text = Clipboard.GetText();
            Console.WriteLine(text);





            //设置自定义类型
            MyItem item = new MyItem();
            item.CopyToClipboard();



        }


        private void Window_Closed(object sender, EventArgs e)
        {
            //从观察链中删除本观察窗口（第一个参数：将要删除的窗口的句柄；第二个参数：观察链中下一个窗口的句柄 ）
            LibrayWindow32API.ChangeClipboardChain(source.Handle, mNextClipBoardViewerHWnd);
            //将变动消息WM_CHANGECBCHAIN消息传递到下一个观察链中的窗口
            LibrayWindow32API.SendMessage(mNextClipBoardViewerHWnd, LibrayWindow32API.WM_CHANGECBCHAIN, source.Handle, mNextClipBoardViewerHWnd);

        }

       
        //重写OnSourceInitialized
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            source = PresentationSource.FromVisual(this) as HwndSource;

            //挂消息钩子 //获得观察链中下一个窗口句柄
            mNextClipBoardViewerHWnd = LibrayWindow32API.SetClipboardViewer(source.Handle);
            
            source.AddHook(WndProc);
        }


        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            try
            {
                switch (msg)
                {
                    case LibrayWindow32API.WM_DRAWCLIPBOARD:

                        //将WM_DRAWCLIPBOARD消息传递到下一个观察链中的窗口
                        LibrayWindow32API.SendMessage(mNextClipBoardViewerHWnd, msg, wParam, lParam);

                        //获取剪切板中的数据
                        IDataObject iData = Clipboard.GetDataObject();



                        //var m1 = iData.GetFormats();



                       

                        //测试获取的数据的类型
                        if (
                           //iData.GetDataPresent(DataFormats.Dib) ||
                           //iData.GetDataPresent(DataFormats.MetafilePict) ||
                           //iData.GetDataPresent(DataFormats.PenData) ||
                           //iData.GetDataPresent(DataFormats.FileDrop) ||
                           //iData.GetDataPresent(DataFormats.WaveAudio) ||
                           //iData.GetDataPresent(DataFormats.CommaSeparatedValue) 
                           //iData.GetDataPresent(DataFormats.EnhancedMetafile) ||
                           //iData.GetDataPresent(DataFormats.Html  )
                           // iData.GetDataPresent(DataFormats.Serializable) ||
                           iData.GetDataPresent(DataFormats.Tiff)
                           )
                        {
                            Console.WriteLine("Tiff");
                        }

                        
                        //检测文本
                        if (iData.GetDataPresent(DataFormats.Text) | iData.GetDataPresent(DataFormats.OemText))
                        {
                            //判断剪切板中是否包含文本数据
                            //bool flag = Clipboard.ContainsText();

                            this.tb1.Text = (String)iData.GetData(DataFormats.Text);
                        }

                        //检测图像
                        if (iData.GetDataPresent(DataFormats.Bitmap))
                        {
                            bool flag = Clipboard.ContainsImage();

                            BitmapSource bitmapSource = Clipboard.GetImage();


                            this.image1.Source = bitmapSource;

                            string imagename = Guid.NewGuid() + ".jpg";

                            ImageHelper.SaveImageToFile(bitmapSource, imagename);
                        }

                        //检测图文
                        if (iData.GetDataPresent(DataFormats.Html))
                        {
                            if (Clipboard.ContainsText(TextDataFormat.Html))
                            {
                                //获取含HTML格式的文本时，内容有汉字时乱码。通过剪贴板工具分析
                                //HTML格式的文本是UTF8编码方式,Clipboard.GetText()解码出了问题
                                //var strText = Clipboard.GetText(TextDataFormat.Html);

                                //修复乱码
                                MemoryStream vMemoryStream =
                                    Clipboard.GetData("Html Format") as MemoryStream;
                                vMemoryStream.Position = 0;
                                byte[] vBytes = new byte[vMemoryStream.Length];
                                vMemoryStream.Read(vBytes, 0, (int)vMemoryStream.Length);
                                var str2 = Encoding.UTF8.GetString(vBytes);


                                string logPath = Guid.NewGuid() + ".txt";
                                System.IO.StreamWriter sw = new System.IO.StreamWriter(logPath, true, Encoding.UTF8);
                                sw.WriteLine(str2);
                                sw.Close();
                            }
                        }


                        //复制多个文件，获取文件名集合
                        if (iData.GetDataPresent(DataFormats.FileDrop))
                        {
                            //因为复制的只能复制一张图片。多文件复制，复制的是文件的路径而不是文件对象
                            StringCollection sc = Clipboard.GetFileDropList();

                            //遍历获取
                            foreach (var item in sc)
                            {
                                Console.WriteLine(item.ToString());
                            }
                        }


                        //检测自定义类型
                        if (iData.GetDataPresent(typeof(MyItem).FullName))
                        {
                            // MyItem item = (MyItem)iData.GetData(typeof(MyItem).FullName);
                            MyItem item = GetFromClipboard();
                            if (item != null)
                            {
                                //this.richTextBox1.Text = item.ItemName;
                            }
                        }
                        break;
                    default:

                        break;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return IntPtr.Zero;

        }





        protected static MyItem GetFromClipboard()
        {
            MyItem item = null;
            IDataObject dataObj = Clipboard.GetDataObject();
            string format = typeof(MyItem).FullName;

            if (dataObj.GetDataPresent(format))
            {
                item = dataObj.GetData(format) as MyItem;
            }
            return item;
        }





        


    }



    [Serializable]
    public class MyItem
    {
        public MyItem()
        {
            itemName = "This is a Custom Item";
        }
        public string ItemName
        {
            get { return itemName; }
        }
        private string itemName;

        public void CopyToClipboard()
        {
            //DataFormats.Format format = DataFormats.GetFormat(typeof(MyItem).FullName);
            //IDataObject dataObj = new DataObject();
            //dataObj.SetData(format.Name, false, this);
            //Clipboard.SetDataObject(dataObj, false);
        }
    }
}
