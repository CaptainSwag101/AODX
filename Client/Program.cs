using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Client
{
	internal static class Program
	{
		/// <summary>
		///     The main entry point for the application.
		/// </summary>
		public static List<string> charList;
		public static List<string> musicList;
		public static List<Evidence> eList;
		public static bool debug;
		//public static Socket connection;

		[STAThread]
		private static void Main()
		{
#if (DEBUG)
			debug = true;
#endif

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var loginForm = new LoginForm();

			Application.Run(loginForm);
			if (loginForm.DialogResult == DialogResult.OK)
			{
				charList = loginForm.charList;
				musicList = loginForm.musicList;
				eList = loginForm.eviList;

				var CharSelect = new CharForm();
				CharSelect.clientSocket = loginForm.clientSocket;
				//CharSelect.clientSocket = connection;
				CharSelect.charList = charList;

				Application.Run(CharSelect);
				if (CharSelect.DialogResult == DialogResult.OK)
				{
					var AODXClientForm = new ClientForm();
					AODXClientForm.clientSocket = CharSelect.clientSocket;
					AODXClientForm.eviList = eList;
					AODXClientForm.songs = musicList;
					AODXClientForm.strName = CharSelect.strName;
					try
					{
						Application.Run(AODXClientForm);
						//AODXClientForm.gameEntry = new GameRenderer(AODXClientForm.renderPB.Handle, AODXClientForm, AODXClientForm.renderPB);
						//AODXClientForm.gameEntry.Run();
						//AODXClientForm.Show();
						
					} catch (Exception ex)
					{
						if (debug)
							MessageBox.Show(ex.Message + ".\r\n" + ex.StackTrace, "AODXClient", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}
	}
}