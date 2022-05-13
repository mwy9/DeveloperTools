using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipBoardMonitorSave.Win32API
{
    public class LibrayWindow32API
    {
        #region 消息钩子预定义参数
        public const int WM_DRAWCLIPBOARD = 0x308;
        public const int WM_CHANGECBCHAIN = 0x30D;


        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern IntPtr SetClipboardViewer(IntPtr hwnd);
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern IntPtr ChangeClipboardChain(IntPtr hwnd, IntPtr hWndNext);
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);



        /*
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool ChangeClipboardChain(IntPtr HWnd, IntPtr HWndNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        */
        #endregion
    }
}
