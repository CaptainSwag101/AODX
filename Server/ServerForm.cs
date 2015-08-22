using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    //The commands for interaction between the server and the client
    enum Command
    {
        Login,      //Log into the server
        Logout,     //Logout of the server
        Message,    //Send a text message to all the chat clients
        List,       //Get a list of users in the chat room from the server
        Null        //No command
    }

    public partial class ServerForm : Form
    {
        //The ClientInfo structure holds the required information about every
        //client connected to the server
        struct ClientInfo
        {
            public Socket socket;   //Socket of the client
            public string strName;  //Name by which the user logged into the chat room
            public string character; //Character (file)name that the user is playing as
        }

        //The collection of all clients logged into the room (an array of type ClientInfo)
        ArrayList clientList;

        //The main socket on which the server listens to the clients
        Socket serverSocket;

        byte[] byteData = new byte[1024];

        public ServerForm()
        {
            clientList = new ArrayList();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            userNumStat.Text = "Users Online: " + clientList.Count;

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIPLabel.Text = "Server IP Address: " + ip.ToString();
                    break;
                }
                //throw new Exception("Local IP Address Not Found!");
            }
            

            try
            {
                //We are using TCP sockets
                serverSocket = new Socket(AddressFamily.InterNetwork, 
                                          SocketType.Stream, 
                                          ProtocolType.Tcp);
                
                //Assign the any IP of the machine and listen on port number 1000
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 1000);

                //Bind and listen on the given address
                serverSocket.Bind(ipEndPoint);
                serverSocket.Listen(4);

                //Accept the incoming clients
                serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message, "AODXServer",                     
                    MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }            
        }

        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = serverSocket.EndAccept(ar);

                //Start listening for more clients
                serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);

                //Once the client connects then start receiving the commands from her
                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, 
                    new AsyncCallback(OnReceive), clientSocket);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AODXServer", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                clientSocket.EndReceive(ar);

                //Transform the array of bytes received from the user into an
                //intelligent form of object Data
                Data msgReceived = new Data(byteData);

                //We will send this object in response the users request
                Data msgToSend = new Data();

                byte[] message;
                
                //If the message is to login, logout, or simple text message
                //then when send to others the type of the message remains the same
                msgToSend.cmdCommand = msgReceived.cmdCommand;
                msgToSend.strName = msgReceived.strName;

                switch (msgReceived.cmdCommand)
                {
                    case Command.Login:
                        
                        //When a user logs in to the server then we add her to our
                        //list of clients

                        ClientInfo clientInfo = new ClientInfo();
                        clientInfo.socket = clientSocket;      
                        clientInfo.strName = msgReceived.strName;
                        clientInfo.character = msgReceived.strMessage;

                        clientList.Add(clientInfo);
                        appendLstUsersSafe(msgReceived.strName + " - " + msgReceived.strMessage);
                        
                        //Set the text of the message that we will broadcast to all users
                        msgToSend.strMessage = "<<<" + msgReceived.strName + " has entered the courtroom>>>";
                        userNumStat.Text = "Users Online: " + clientList.Count; 
                        break;

                    case Command.Logout:                    
                        
                        //When a user wants to log out of the server then we search for her 
                        //in the list of clients and close the corresponding connection

                        int nIndex = 0;
                        foreach (ClientInfo client in clientList)
                        {
                            if (client.socket == clientSocket)
                            {
                                clientList.RemoveAt(nIndex);
                                removeLstUsersSafe(client.strName + " - " + client.character);
                                break;
                            }
                            ++nIndex;
                        }
                        
                        clientSocket.Close();

                        msgToSend.strMessage = "<<<" + msgReceived.strName + " has left the courtroom>>>";
                        userNumStat.Text = "Users Online: " + clientList.Count;
                        break;

                    case Command.Message:

                        //Set the text of the message that we will broadcast to all users
                        msgToSend = msgReceived;
                        msgToSend.strMessage = msgReceived.strName + ": " + msgReceived.strMessage;
                        break;

                    case Command.List:

                        //Send the names of all users in the chat room to the new user
                        msgToSend.cmdCommand = Command.List;
                        msgToSend.strName = null;
                        msgToSend.strMessage = null;

                        //Collect the names of the user in the chat room
                        foreach (ClientInfo client in clientList)
                        {
                            //To keep things simple we use asterisk as the marker to separate the user names
                            msgToSend.strMessage += client.strName + "*";   
                        }

                        message = msgToSend.ToByte();

                        //Send the name of the users in the chat room
                        clientSocket.BeginSend(message, 0, message.Length, SocketFlags.None,
                                new AsyncCallback(OnSend), clientSocket);                        
                        break;
                }

                if (msgToSend.cmdCommand != Command.List)   //List messages are not broadcasted
                {
                    message = msgToSend.ToByte();

                    foreach (ClientInfo clientInfo in clientList)
                    {
                        if (clientInfo.socket != clientSocket || msgToSend.cmdCommand != Command.Login)
                        {
                            //Send the message to all users
                            clientInfo.socket.BeginSend(message, 0, message.Length, SocketFlags.None,
                                new AsyncCallback(OnSend), clientInfo.socket);                            
                        }
                    }

                    //txtLog.Text += msgToSend.strMessage + "\r\n";
                    appendTxtLogSafe(msgToSend.strMessage + "\r\n");
                    
                }

                //If the user is logging out then we need not listen from her
                if (msgReceived.cmdCommand != Command.Logout)
                {
                    //Start listening to the message send by the user
                    clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), clientSocket);
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message, "AODXServer", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void appendTxtLogSafe(string txt)
        {
            if (txtLog.InvokeRequired) {
                txtLog.Invoke(new Action(() => txtLog.Text += txt));
                return;
            }
            txtLog.Text += txt;
        }

        private void appendLstUsersSafe(string txt)
        {
            if (lstUsers.InvokeRequired)
            {
                lstUsers.Invoke(new Action(() => lstUsers.Items.Add(txt)));
                return;
            }
            lstUsers.Items.Add(txt);
        }

        private void removeLstUsersSafe(string txt)
        {
            if (lstUsers.InvokeRequired)
            {
                lstUsers.Invoke(new Action(() => lstUsers.Items.Remove(txt)));
                return;
            }
            lstUsers.Items.Remove(txt);
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
                MessageBox.Show(ex.Message, "AODXServer", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }
    }

    //The data structure by which the server and the client interact with 
    //each other
    class Data
    {
        //Default constructor
        public Data()
        {
            cmdCommand = Command.Null;
            strMessage = null;
            strName = null;
            charName = null;
            preAnim = null;
            textColor = Color.PeachPuff;
            anim = null;
        }

        //Converts the bytes into an object of type Data
        public Data(byte[] data)
        {
            //The first four bytes are for the Command
            cmdCommand = (Command)BitConverter.ToInt32(data, 0);

            //The next four store the length of the name
            int nameLen = BitConverter.ToInt32(data, 4);

            int charNameLen = BitConverter.ToInt32(data, 8);

            int preAnimLen = BitConverter.ToInt32(data, 12);

            int animLen = BitConverter.ToInt32(data, 16);

            int textColorLen = BitConverter.ToInt32(data, 20);

            //The next four store the length of the message
            int msgLen = BitConverter.ToInt32(data, 24);

            //This check makes sure that strName has been passed in the array of bytes
            if (nameLen > 0)
                strName = Encoding.UTF8.GetString(data, 28, nameLen);
            else
                strName = null;

            if (charNameLen > 0)
                charName = Encoding.UTF8.GetString(data, 28 + nameLen, charNameLen);
            else
                charName = null;

            if (preAnimLen > 0)
                preAnim = Encoding.UTF8.GetString(data, 28 + nameLen + charNameLen, preAnimLen);
            else
                preAnim = null;

            if (animLen > 0)
                anim = Encoding.UTF8.GetString(data, 28 + nameLen + charNameLen + preAnimLen, animLen);
            else
                anim = null;

            if (textColorLen > 0)
                textColor = Color.FromArgb(BitConverter.ToInt32(data, 28 + nameLen + charNameLen + preAnimLen + animLen));
            else
                textColor = Color.White;

            //This checks for a null message field
            if (msgLen > 0)
                strMessage = Encoding.UTF8.GetString(data, 28 + nameLen + charNameLen + preAnimLen + animLen + textColorLen, msgLen);
            else
                strMessage = null;
        }

        //Converts the Data structure into an array of bytes
        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();

            //First four are for the Command
            result.AddRange(BitConverter.GetBytes((int)cmdCommand));

            //Add the length of the name
            if (strName != null)
                result.AddRange(BitConverter.GetBytes(strName.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            if (charName != null)
                result.AddRange(BitConverter.GetBytes(charName.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            if (preAnim != null)
                result.AddRange(BitConverter.GetBytes(preAnim.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            if (anim != null)
                result.AddRange(BitConverter.GetBytes(anim.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            //Add the color length
            result.AddRange(BitConverter.GetBytes(4));

            //Length of the message
            if (strMessage != null)
                result.AddRange(BitConverter.GetBytes(strMessage.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));


            //Add the name
            if (strName != null)
                result.AddRange(Encoding.UTF8.GetBytes(strName));

            if (charName != null)
                result.AddRange(Encoding.UTF8.GetBytes(charName));

            if (preAnim != null)
                result.AddRange(Encoding.UTF8.GetBytes(preAnim));

            if (anim != null)
                result.AddRange(Encoding.UTF8.GetBytes(anim));

            //if (textColor != Color.PeachPuff)
            result.AddRange(BitConverter.GetBytes(textColor.ToArgb()));

            //And, lastly we add the message text to our array of bytes
            if (strMessage != null)
                result.AddRange(Encoding.UTF8.GetBytes(strMessage));

            return result.ToArray();
        }

        public string strName;      //Name by which the client logs into the room
        public string charName;
        public string preAnim;
        public string anim;
        public Color textColor;
        public string strMessage;   //Message text
        public Command cmdCommand;  //Command type (login, logout, send message, etcetera)
    }
}