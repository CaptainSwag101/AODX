using System;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            

            LoginForm loginForm = new LoginForm();

            Application.Run(loginForm);
            if (loginForm.DialogResult == DialogResult.OK)
            {
                ClientForm AODXClientForm = new ClientForm();
                AODXClientForm.clientSocket = loginForm.clientSocket;
                AODXClientForm.strName = loginForm.strName;

                AODXClientForm.ShowDialog();
            }

        }
    }
}