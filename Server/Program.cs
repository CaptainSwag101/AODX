using System;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    static class Program
    {
        public static bool debug = false;
        static Mutex mutex = new Mutex(true, "{b6b25bcfcee262c292f838d6e5befa5b}");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if (DEBUG)
            debug = true;
#endif
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ServerForm());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("An instance of the server is already running.","AODXServer");
            }
        }
    }
}