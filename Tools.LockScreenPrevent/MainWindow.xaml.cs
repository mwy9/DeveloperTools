using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;

namespace LockScreenPrevent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer movementTimer = new DispatcherTimer();
        private int PointLocationX = 0;
        private int PointLocationY = 0;

        private int SecondTimerInterval = 0;//秒

        private int PointScreenStart = 0;//左上角
        private int PointScreenEnd = 100;//右下角


        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnTimerStart_Click(object sender, RoutedEventArgs e)
        {
            //自动点击范围
            Int32.TryParse(tbScreenStart.Text.ToString(), out PointScreenStart);
            Int32.TryParse(tbScreenEnd.Text.ToString(), out PointScreenEnd);
            Int32.TryParse(tbTimerSecond.Text.ToString(), out SecondTimerInterval);

            movementTimer.Tick += movementTimer_Tick;
            movementTimer.Interval = TimeSpan.FromSeconds(SecondTimerInterval);

            movementTimer.Start();
        }

        private void btnTimerStop_Click(object sender, RoutedEventArgs e)
        {
            movementTimer.Tick -= movementTimer_Tick;

            movementTimer.Stop();
        }


        //Tick:定时器,每当经过多少时间发生函数
        private void movementTimer_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            var flagx = random.Next(PointScreenStart, PointScreenEnd);
            var flagy = random.Next(PointScreenStart, PointScreenEnd);

            int stepx = PointScreenStart + flagx;
            int stepy = PointScreenStart + flagy;

            //设置焦点
            SetCursorPos(stepx, stepy);
            mouse_event(MouseEventFlag.Move, stepx, stepy, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.RightDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);
           
        }


        //结构体布局 本机位置
        [StructLayout(LayoutKind.Sequential)]
        struct NativeRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        //将枚举作为位域处理
        [Flags]
        enum MouseEventFlag : uint //设置鼠标动作的键值
        {
            Move = 0x0001,               //发生移动
            LeftDown = 0x0002,           //鼠标按下左键
            LeftUp = 0x0004,             //鼠标松开左键
            RightDown = 0x0008,          //鼠标按下右键
            RightUp = 0x0010,            //鼠标松开右键
            MiddleDown = 0x0020,         //鼠标按下中键
            MiddleUp = 0x0040,           //鼠标松开中键
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,              //鼠标轮被移动
            VirtualDesk = 0x4000,        //虚拟桌面
            Absolute = 0x8000
        }

        //设置鼠标位置
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        //设置鼠标按键和动作
        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy,
            uint data, UIntPtr extraInfo); //UIntPtr指针多句柄类型

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string strClass, string strWindow);

        //该函数获取一个窗口句柄,该窗口雷鸣和窗口名与给定字符串匹配 hwnParent=Null从桌面窗口查找
        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter,
            string strClass, string strWindow);

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(HandleRef hwnd, out NativeRECT rect);

        
    }
}