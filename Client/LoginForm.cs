using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class LoginForm : Form
    {
        public Socket clientSocket;
        public string strName;
        public string character;
        public int incomingSize;
        private List<string> serverAddresses = new List<string>();
        private byte[] byteData = new byte[1024];
        private bool favorites = false;

        public LoginForm()
        {
            InitializeComponent();
            background.Controls.Add(btn_PublicServers);
            background.Controls.Add(btn_FavoriteServers);
            background.Controls.Add(btn_Refresh);
            background.Controls.Add(btn_AddFav);
            background.Controls.Add(btn_Connect);
            background.Controls.Add(versionLabel);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipAddress = IPAddress.Parse("129.138.39.18");

            //Masterserver is listening on port 1002
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 1002);

            //Connect to the masterserver
            clientSocket.BeginConnect(ipEndPoint, new AsyncCallback(OnConnectMaster), null);
        }

        private void OnConnect(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndConnect(ar);
                //btnOK.Enabled = false;

                //We are connected so we login into the server
                Data msgToSend = new Data ();
                //msgToSend.cmdCommand = Command.Login;
                //msgToSend.strName = txtName.Text;
                //msgToSend.strMessage = charList.SelectedItem.ToString();

                //msgToSend.cmdCommand = Command.PacketSize;

                byte[] b = msgToSend.ToByte ();

                //Send the message to the server
                clientSocket.BeginSend(b, 0, b.Length, SocketFlags.None, new AsyncCallback(OnSend), null);

                clientSocket.BeginReceive(byteData,
                                       0,
                                       byteData.Length,
                                       SocketFlags.None,
                                       new AsyncCallback(OnReceive),
                                       null);
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message, "AODXClient", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void OnConnectMaster(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndConnect(ar);
                //btnOK.Enabled = false;

                byte[] b = new byte[1];
                b[0] = 102;

                //Send the message to the server
                clientSocket.BeginSend(b, 0, b.Length, SocketFlags.None, new AsyncCallback(OnSend), null);

                clientSocket.BeginReceive(byteData,
                                       0,
                                       byteData.Length,
                                       SocketFlags.None,
                                       new AsyncCallback(OnReceiveServers),
                                       null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AODXClient", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AODXClient", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndReceive(ar);

                Data msgReceived = new Data(byteData);

                if (msgReceived.cmdCommand == Command.PacketSize)
                {
                    byteData = new byte[Convert.ToInt32(msgReceived.strMessage)];
                    incomingSize = Convert.ToInt32(msgReceived.strMessage);
                    //strName = txtName.Text;
                    DialogResult = DialogResult.OK;
                    Data msgToSend = new Data();
                    //msgToSend.cmdCommand = Command.Login;
                    //msgToSend.strName = txtName.Text;
                    //msgToSend.strMessage = charList.SelectedItem.ToString();
                    msgToSend.cmdCommand = Command.DataInfo;

                    byte[] b = msgToSend.ToByte();

                    //Send the message to the server
                    clientSocket.BeginSend(b, 0, b.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
                    Close();
                }
                else
                    clientSocket.BeginReceive(byteData,
                                       0,
                                       byteData.Length,
                                       SocketFlags.None,
                                       new AsyncCallback(OnReceive),
                                       null);

            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AODXClient: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnReceiveServers(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndReceive(ar);

                string allServerData = Encoding.UTF8.GetString(byteData);
                string[] servers = allServerData.Split('/');
                foreach (string server in servers)
                {
                    serverList.Items.Add(server.Split('|')[0]); // + server.Split('|')[2] + server.Split('|')[3]);
                    editServerDescTB(server.Split('|')[1]);
                }

            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AODXClient: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editServerDescTB(string txt)
        {
            if (serverDescTextBox.InvokeRequired)
            {
                serverDescTextBox.Invoke(new Action(() => serverDescTextBox.Text = txt));
                return;
            }
            serverDescTextBox.Text = txt;
        }

        private void btn_PublicServers_Click(object sender, EventArgs e)
        {
            if (favorites == true)
            {
                favorites = false;
                btn_PublicServers.Image = Properties.Resources.b1_on;
                btn_FavoriteServers.Image = Properties.Resources.b2_off;
            }
        }

        private void btn_FavoriteServers_Click(object sender, EventArgs e)
        {
            if (favorites == false)
            {
                favorites = true;
                btn_FavoriteServers.Image = Properties.Resources.b2_on;
                btn_PublicServers.Image = Properties.Resources.b1_off;
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {

        }

        private void btn_AddFav_Click(object sender, EventArgs e)
        {

        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //IPAddress ipAddress = IPAddress.Parse(txtServerIP.Text);

                //Server is listening on port 1000
                //IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 1000);

                //Connect to the server
                //clientSocket.BeginConnect(ipEndPoint, new AsyncCallback(OnConnect), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AODXClient", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}