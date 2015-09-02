using System;
using System.Threading;
using System.Windows.Forms;

namespace MasterServer
{
    static class Program
    {
        public static bool debug = false;
        static Mutex mutex = new Mutex(true, "{8e8ea4f005667dcea1218ccc951ce918}");

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
                Application.Run(new MasterForm());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("An instance of the masterserver is already running.", "AODXMasterserver");
            }
        }
    }
}
