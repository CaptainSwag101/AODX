using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class CharForm : Form
    {
        public Socket clientSocket;
        public string strName;
        public List<string> charList;
        private byte[] byteData = new byte[1024];

        public CharForm()
        {
            InitializeComponent();
        }

        private void CharForm_Load(object sender, EventArgs e)
        {
            /*foreach (string charName in charList)
            {
                lb_CharList.Items.Add(charName);
            }
            btn_Login.Enabled = false; */
            lb_CharList.DataSource = charList;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            Data msgToSend = new Data();
            msgToSend.cmdCommand = Command.Login;
            msgToSend.strName = strName;

            DialogResult = DialogResult.OK;

            byte[] b = msgToSend.ToByte();

            //Send the message to the server
            clientSocket?.BeginSend(b, 0, b.Length, SocketFlags.None, new AsyncCallback(OnSend), null);

            //clientSocket?.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void lb_CharList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_CharList.SelectedValue != null)
            {
                strName = (string)lb_CharList.SelectedValue;
                btn_Login.Enabled = true;
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch (SocketException)
            {
                if (MessageBox.Show("You have been kicked from the server.", "AODXClient", MessageBoxButtons.OK) == DialogResult.OK | MessageBox.Show("You have been kicked from the server.", "AODXClient", MessageBoxButtons.OK) == DialogResult.Cancel)
                    Close();
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AODXClient", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CharForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                byte[] b = new byte[1];
                b[0] = 103;

                //Send the message to the server
                clientSocket.BeginSend(b, 0, b.Length, SocketFlags.None, new AsyncCallback(OnSend), null);

                if (Directory.Exists("base/cases"))
                    Directory.Delete("base/cases", true);
            }
        }
    }
}
