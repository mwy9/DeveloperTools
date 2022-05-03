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

        //Auto run time Second
        private int SecondTimerInterval = 0;

        //Left Top
        private int PointScreenStart = 0;
        //Right Buttom
        private int PointScreenEnd = 100;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnTimerStart_Click(object sender, RoutedEventArgs e)
        {
            //Range
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


        private void movementTimer_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            var flagx = random.Next(PointScreenStart, PointScreenEnd);
            var flagy = random.Next(PointScreenStart, PointScreenEnd);

            int stepx = PointScreenStart + flagx;
            int stepy = PointScreenStart + flagy;

            //SetCurso
            Win32API.SetCursorPos(stepx, stepy);
            Win32API.mouse_event(Win32API.MouseEventFlag.Move, stepx, stepy, 0, UIntPtr.Zero);
            Win32API.mouse_event(Win32API.MouseEventFlag.RightDown, 0, 0, 0, UIntPtr.Zero);
            Win32API.mouse_event(Win32API.MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);

        }




    }
}