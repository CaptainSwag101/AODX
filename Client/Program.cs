using System;
using System.Collections.Generic;
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

        [STAThread]
        static void Main()
        {
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
                CharSelect.charList = charList;

                Application.Run(CharSelect);
                if (CharSelect.DialogResult == DialogResult.OK)
                {
                    ClientForm AODXClientForm = new ClientForm();
                    AODXClientForm.clientSocket = CharSelect.clientSocket;
                    AODXClientForm.strName = CharSelect.strName;
                    AODXClientForm.ShowDialog();
                }
            }
        }
    }
}