using System;
using System.Windows.Forms;

namespace Music_Player
{
    internal static class Program
    {
        public static int user_id { get; set; }
        public static string username { get; set; }
        public static string sbit { get; set; }
        public static bool OpenForm1OnClose { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            OpenForm1OnClose = false;

            Application.Run(new loginForm());

            if (OpenForm1OnClose)
            {
                Application.Run(new MusicPlayer());
            }
        }
    }
}
