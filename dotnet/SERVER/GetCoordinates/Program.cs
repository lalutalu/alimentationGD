using System;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;

namespace GetCoordinates
{
    class Program
    {

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int
 X;
            public int Y;
        }

        static void
 Main(string[] args)
        {
            Console.WriteLine("Move cursor to desired location within 5 seconds... ");
            Thread.Sleep(5000);

            POINT cursorPosition;
            if (GetCursorPos(out cursorPosition))
            {
                Console.WriteLine($"Current mouse position: {cursorPosition.X}, {cursorPosition.Y}");
            }
            else
            {
                Console.WriteLine("Failed to get cursor position.");
            }
        }
    }

}
