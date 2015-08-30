using System;
using System.Windows.Forms;

namespace Server
{
    static class Program
    {
        public static bool debug = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if (DEBUG)
            debug = true;
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServerForm());
        }
    }
}