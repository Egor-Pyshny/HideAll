using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoIt;

namespace AutoIt
{
    class Program
    {
        [StructLayout(LayoutKind.Sequential)]
        struct POINT
        {
            public Int32 X;
            public Int32 Y;
        }

        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        const int MOUSEEVENTF_MOVE = 0x0001;
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        const int KEYEVENTF_KEYUP = 0x0002;
        const int VK_LEFT = 0x25;
        const int VK_UP = 0x26;
        const int VK_RIGHT = 0x27;
        const int VK_DOWN = 0x28;
        const int VK_RETURN	= 0x0D;

        static void Main(string[] args)
        {
            var resolution = Screen.PrimaryScreen.Bounds.Size;
            Console.WindowHeight = 1;
            Console.WindowWidth = 1;
            AutoItX.WinMinimizeAll();
            POINT current_pos = new POINT();
            GetCursorPos(out current_pos);
            int deltaX = 1240 - current_pos.X, deltaY = 600 - current_pos.Y;
            mouse_event(MOUSEEVENTF_MOVE, deltaX, deltaY, 0, 0);
            right_click();
            press_key(VK_DOWN);
            press_key(VK_RIGHT);
            press_key(VK_UP);
            press_key(VK_RETURN);
            Thread.Sleep(100);
            mouse_event(MOUSEEVENTF_MOVE, 0, 850, 0, 0);
            right_click();
            press_key(VK_UP);
            press_key(VK_RETURN);
            Thread.Sleep(500);
            var hndle = FindWindow(null, "Параметры");
            ShowWindow(hndle, 3);
            GetCursorPos(out current_pos);
            deltaX = 355 - current_pos.X;
            deltaY = 214 - current_pos.Y;
            mouse_event(MOUSEEVENTF_MOVE, deltaX, deltaY, 0, 0);
            Thread.Sleep(200);
            AutoItX.MouseClick();
            AutoItX.WinClose("Параметры");
            AutoItX.WinMinimizeAllUndo();
        }

        private static void press_key(byte code) {
            keybd_event(code, 0, 0, 0);
            keybd_event(code, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void right_click()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        [DllImport("user32.dll")]
        static extern bool CloseWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr GetWindowText(IntPtr hwndParent, StringBuilder Wintxt, int txtsize);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData,int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT point);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
    }
}
