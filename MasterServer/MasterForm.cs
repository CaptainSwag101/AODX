﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace MasterServer
{
    public partial class MasterForm : Form
    {
        //The ServerInfo structure holds the required information about every
        //client connected to the server
        struct ServerInfo
        {
            public Socket socket;   //Socket of the server
            public string name;     //Name of the server
            public string desc;     //Server description
            public int users;       //Number of users currently on server
        }

        byte[] byteData = new byte[1024];

        //The collection of all servers connected to the masterserver (an array of type ServerInfo)
        ArrayList serverList;

        //The main socket on which the masterserver listens to the clients and servers
        Socket masterSocket;

        public MasterForm()
        {
            serverList = new ArrayList();
            InitializeComponent();
        }

        private void MasterForm_Load(object sender, EventArgs e)
        {
            try
            {
                //We are using TCP sockets
                masterSocket = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Stream,
                                          ProtocolType.Tcp);

                //Assign the any IP of the machine and listen on port number 1000
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 1002);

                //Bind and listen on the given address
                masterSocket.Bind(ipEndPoint);
                masterSocket.Listen(4);

                //Accept the incoming clients
                masterSocket.BeginAccept(new AsyncCallback(OnAccept), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AODXMasterServer",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket connectingSocket = masterSocket.EndAccept(ar);

                //Start listening for more clients
                masterSocket.BeginAccept(new AsyncCallback(OnAccept), null);

                //Once the client connects then start receiving the commands from her
                connectingSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None,
                    new AsyncCallback(OnReceive), connectingSocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AODXMasterServer",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OnReceive(IAsyncResult ar)
        {
            try
            {
                Socket connectingSocket = (Socket)ar.AsyncState;
                connectingSocket.EndReceive(ar);

                //Determine if the connection is from a server or client, respectively
                if (byteData[0] == 101) //server
                {
                    ServerInfo newServer = new ServerInfo();
                    newServer.socket = connectingSocket;
                    int len = BitConverter.ToInt32(byteData, 1);
                    string infoString = Encoding.UTF8.GetString(byteData, 5, len);
                    newServer.name = infoString.Split('|')[0];
                    newServer.desc = infoString.Split('|')[1];
                    newServer.users = Convert.ToInt32(infoString.Split('|')[2]);
                    bool alreadyConnected = false;
                    foreach (ServerInfo server in serverList)
                    {
                        if (server.socket == connectingSocket)
                        {
                            alreadyConnected = true;
                        }
                    }
                    if (alreadyConnected != true)
                        serverList.Add(newServer);

                    appendServerListBox(infoString);

                    connectingSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), connectingSocket);
                }
                else if (byteData[0] == 102) //client
                {
                    List<byte> msgToSend = new List<byte>();
                    foreach (ServerInfo server in serverList)
                    {
                        msgToSend.AddRange(Encoding.UTF8.GetBytes(server.name));
                        msgToSend.AddRange(Encoding.UTF8.GetBytes("|"));
                        msgToSend.AddRange(Encoding.UTF8.GetBytes(server.desc));
                        msgToSend.AddRange(Encoding.UTF8.GetBytes("|"));
                        msgToSend.AddRange(BitConverter.GetBytes(server.users));
                        msgToSend.AddRange(Encoding.UTF8.GetBytes("|"));
                        msgToSend.AddRange(Encoding.UTF8.GetBytes(((IPEndPoint)server.socket.RemoteEndPoint).Address.ToString()));
                        msgToSend.AddRange(Encoding.UTF8.GetBytes(":"));
                        msgToSend.AddRange(Encoding.UTF8.GetBytes(((IPEndPoint)server.socket.RemoteEndPoint).Port.ToString()));
                        msgToSend.AddRange(Encoding.UTF8.GetBytes("/"));
                    }
                    byte[] message = msgToSend.ToArray();
                    connectingSocket.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(OnSendClose), connectingSocket);
                }
                else if (byteData[0] == 103) //disconnect server
                {
                    int nIndex = 0;
                    foreach (ServerInfo server in serverList)
                    {
                        if (server.socket == connectingSocket)
                        {
                            serverList.RemoveAt(nIndex);
                            removeServerListBox(server.name + "|" + server.desc + "|" + server.users.ToString());
                            break;
                        }
                        ++nIndex;
                    }
                    connectingSocket.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AODXMasterServer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void appendServerListBox(string txt)
        {
            if (lb_Servers.InvokeRequired)
            {
                lb_Servers.Invoke(new Action(() => lb_Servers.Items.Add(txt)));
                return;
            }
            lb_Servers.Items.Add(txt);
        }

        private void removeServerListBox(string txt)
        {
            if (lb_Servers.InvokeRequired)
            {
                lb_Servers.Invoke(new Action(() => lb_Servers.Items.Remove(txt)));
                return;
            }
            lb_Servers.Items.Remove(txt);
        }

        public void OnSend(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AODXMasterServer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OnSendClose(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndSend(ar);
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AODXMasterServer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void refreshStatsTimer_Tick(object sender, EventArgs e)
        {
            if (serverList != null && serverList.Count > 0)
            {
                int nIndex = 0;
                foreach (ServerInfo server in serverList)
                {
                    //serverList.RemoveAt(nIndex);
                    removeServerListBox(server.name + "|" + server.desc + "|" + server.users.ToString());

                    byte[] msg = new byte[1024];
                    msg[0] = 101;
                    server.socket.BeginSend(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(OnSend), server.socket);

                    ++nIndex;
                }
            }
        }
    }
}