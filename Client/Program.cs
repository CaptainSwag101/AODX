using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static List<string> charList;
        public static List<string> musicList;
        public static bool debug = false;
        //public static Socket connection;

        [STAThread]
        static void Main()
        {
#if (DEBUG)
            debug = true;
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            

            LoginForm loginForm = new LoginForm();

            Application.Run(loginForm);
            if (loginForm.DialogResult == DialogResult.OK)
            {
                charList = loginForm.charList;
                musicList = loginForm.musicList;

                CharForm CharSelect = new CharForm();
                CharSelect.clientSocket = loginForm.clientSocket;
                //CharSelect.clientSocket = connection;
                CharSelect.charList = charList;

                Application.Run(CharSelect);
                if (CharSelect.DialogResult == DialogResult.OK)
                {
                    ClientForm AODXClientForm = new ClientForm();
                    AODXClientForm.clientSocket = CharSelect.clientSocket;
                    AODXClientForm.strName = CharSelect.strName;
                    try
                    {
                        AODXClientForm.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        if (debug)
                            MessageBox.Show(ex.Message + ".\r\n" + ex.StackTrace.ToString(), "AODXClient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}