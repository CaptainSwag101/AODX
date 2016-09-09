using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Xml;

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
            updateCheck(true);

            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIPLabel.Text = "Masterserver IP Address: " + ip.ToString();
                        break;
                    }
                    //throw new Exception("Local IP Address Not Found!");
                }

                //We are using TCP sockets
                masterSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

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
                if (Program.debug)
                    MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace.ToString(), "AODXMasterServer", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                connectingSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), connectingSocket);
            }
            catch (Exception ex)
            {
                if (Program.debug)
                    MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace.ToString(), "AODXMasterServer", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    ServerInfo newServer = new ServerInfo {socket = connectingSocket};
                    int len = BitConverter.ToInt32(byteData, 1);
                    string infoString = Encoding.UTF8.GetString(byteData, 5, len);
                    newServer.name = infoString.Split('|')[0];
                    newServer.desc = infoString.Split('|')[1];
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

                    appendServerListBox(newServer.name + " - " + newServer.socket.RemoteEndPoint.ToString());

                    connectingSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), connectingSocket);
                }
                else if (byteData[0] == 102) //client
                {
                    List<byte> msgToSend = new List<byte>();
                    string data = "";
                    foreach (ServerInfo server in serverList)
                    {
                        data += server.name + "|";
                        data += server.desc + "|";
                        data += ((IPEndPoint)server.socket.RemoteEndPoint).Address.ToString() + ":";
                        data += ((IPEndPoint)server.socket.RemoteEndPoint).Port.ToString() + "/";
                    }
                    msgToSend.AddRange(BitConverter.GetBytes(data.Length));
                    msgToSend.AddRange(Encoding.UTF8.GetBytes(data));
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
                            removeServerListBox(server.name + " - " + server.socket.RemoteEndPoint.ToString());
                            break;
                        }
                        ++nIndex;
                    }
                    //connectingSocket.Close();
                }
            }
            catch (SocketException) { }
            catch (ObjectDisposedException) { }
            catch (Exception ex)
            {
                if (Program.debug)
                    MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace.ToString(), "AODXMasterServer", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (Program.debug)
                    MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace.ToString(), "AODXMasterServer", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (Program.debug)
                    MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace.ToString(), "AODXMasterServer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void refreshStatsTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (serverList != null && serverList.Count > 0)
                {
                    int nIndex = 0;
                    foreach (ServerInfo server in serverList)
                    {
                        Socket tempSocket = server.socket;
                        removeServerListBox(server.name + " - " + server.socket.RemoteEndPoint.ToString());
                        if (tempSocket != null)
                        {
                            byte[] msg = new byte[1024];
                            msg[0] = 101;
                            tempSocket.BeginSend(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(OnSend), tempSocket);
                            //tempSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), tempSocket);
                            ++nIndex;
                        }
                        else
                        {
                            serverList.RemoveAt(nIndex);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                if (Program.debug)
                    MessageBox.Show(ex.Message + ".\r\n" + ex.StackTrace.ToString(), "AODXMasterServer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateMenuItem_Click(object sender, EventArgs e)
        {
            updateCheck();
        }

        private void updateCheck(bool silent = false)
        {
            // in newVersion variable we will store the  
            // version info from xml file  
            Version newVersion = null;
            // and in this variable we will put the url we  
            // would like to open so that the user can  
            // download the new version  
            // it can be a homepage or a direct  
            // link to zip/exe file  
            string url = "";
            XmlTextReader reader;
            try
            {
                // provide the XmlTextReader with the URL of  
                // our xml document  
                string xmlURL = "https://raw.githubusercontent.com/jpmac26/AODX/master/version.xml";
                reader = new XmlTextReader(xmlURL);
                // simply (and easily) skip the junk at the beginning  
                reader.MoveToContent();
                // internal - as the XmlTextReader moves only  
                // forward, we save current xml element name  
                // in elementName variable. When we parse a  
                // text node, we refer to elementName to check  
                // what was the node name  
                string elementName = "";
                // we check if the xml starts with a proper  
                // "AODX" element node  
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "AODX"))
                {
                    bool done = false;
                    while (reader.Read() && !done)
                    {
                        // when we find an element node,  
                        // we remember its name  
                        if (reader.NodeType == XmlNodeType.Element)
                            elementName = reader.Name;
                        else
                        {
                            if (elementName == "Masterserver")
                            {
                                bool done2 = false;
                                while (reader.Read() && !done2)
                                {
                                    if (reader.NodeType == XmlNodeType.Element)
                                        elementName = reader.Name;
                                    // for text nodes...  
                                    else if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                                    {
                                        // we check what the name of the node was  
                                        switch (elementName)
                                        {
                                            case "version":
                                                // thats why we keep the version info  
                                                // in xxx.xxx.xxx.xxx format  
                                                // the Version class does the  
                                                // parsing for us  
                                                newVersion = new Version(reader.Value);
                                                break;
                                            case "url":
                                                url = reader.Value;
                                                done2 = true;
                                                done = true;
                                                break;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                if (reader != null)
                    reader.Close();
            }
            catch (Exception)
            {

            }

            // get the running version  
            Version curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            // compare the versions  
            if (curVersion.CompareTo(newVersion) < 0)
            {
                // ask the user if he would like  
                // to download the new version  
                string title = "Update Check";
                string question = "New masterserver version available: " + newVersion.ToString() + ".\r\n (You have version " + curVersion.ToString() + "). \r\n Download the new version?";
                if (MessageBox.Show(this, question, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // navigate the default web  
                    // browser to our app  
                    // homepage (the url  
                    // comes from the xml content)  
                    System.Diagnostics.Process.Start(url);
                }
            }
            else if (!silent)
                MessageBox.Show(this, "You have the latest version: " + curVersion.ToString(), "Update Check", MessageBoxButtons.OK);
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
