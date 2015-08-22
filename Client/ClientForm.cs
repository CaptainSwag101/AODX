using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Client
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

    public partial class ClientForm : Form
    {
        public Socket clientSocket; //The main client socket
        public string strName;      //Name by which the user logs into the room
        public string character; //Character that the user is playing as
        private int selectedAnim = 3;
        private int colorIndex = 0;
        private Color selectedColor = Color.White;
        private string[] textToDisp = new string[3];
        private int textTicks = 1;
        private bool redraw = false;
        private Data latestMsg;
        private System.Media.SoundPlayer blipPlayer = new System.Media.SoundPlayer(Properties.Resources.sfx_blipmale);

        private byte[] byteData = new byte[1024];

        public ClientForm()
        {
            InitializeComponent();
            backgroundPB.BackColor = Color.Transparent;
            backgroundPB.Load("base/background/default/defenseempty.png");
            backgroundPB.Controls.Add(charLayerPB);
            charLayerPB.BackColor = Color.Transparent;
            charLayerPB.Image = Properties.Resources.phoenix_normal_a_;
            charLayerPB.Controls.Add(deskLayerPB);
            deskLayerPB.BackColor = Color.Transparent;
            deskLayerPB.Image = Properties.Resources.Defense_Bench_Overlay_resized;
            deskLayerPB.Controls.Add(chatBGLayerPB);
            chatBGLayerPB.Image = Properties.Resources.PW_Textbox_Trans;
            chatBGLayerPB.BackColor = Color.Transparent;
            chatBGLayerPB.Controls.Add(objectLayerPB);
            objectLayerPB.BackColor = Color.Transparent;
            objectLayerPB.Controls.Add(displayMsg1);
            objectLayerPB.Controls.Add(displayMsg2);
            objectLayerPB.Controls.Add(displayMsg3);
            displayMsg1.BackColor = Color.Transparent;
            displayMsg2.BackColor = Color.Transparent;
            displayMsg3.BackColor = Color.Transparent;
            setDispMsgColor(Color.White);

            blipPlayer.Load();
            //displayMsg.Text = "Sample Text";
            //Refresh();
        }

        private void clearDispMsg()
        {
            displayMsg1.Text = "";
            displayMsg2.Text = "";
            displayMsg3.Text = "";
        }

        private void setDispMsgColor(Color newColor)
        {
            displayMsg1.ForeColor = newColor;
            displayMsg2.ForeColor = newColor;
            displayMsg3.ForeColor = newColor;
        }

        //Broadcast the message typed by the user to everyone
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                //Fill the info for the message to be send
                Data msgToSend = new Data();

                msgToSend.strName = strName;
                msgToSend.charName = character;
                msgToSend.preAnim = iniParser.GetPreAnim(character, selectedAnim);
                msgToSend.anim = iniParser.GetAnim(character, selectedAnim);
                msgToSend.textColor = selectedColor;
                msgToSend.strMessage = txtMessage.Text;
                msgToSend.cmdCommand = Command.Message;

                byte[] byteData = msgToSend.ToByte();

                //prepWriteDispBoxes(msgToSend);

                //Send it to the server
                clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnSend), null);

                txtMessage.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to send message to the server.\r\n" + ex.Message, "AODXClient: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AODXClient: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndReceive(ar);

                Data msgReceived = new Data(byteData);
                //Accordingly process the message received
                switch (msgReceived.cmdCommand)
                {
                    case Command.Login:
                        lstUsers.Items.Add(msgReceived.strName); // + " - " + msgReceived.strMessage);
                        break;

                    case Command.Logout:
                        lstUsers.Items.Remove(msgReceived.strName); // + " - " + msgReceived.strMessage);
                        break;

                    case Command.Message:
                        //blipPlayer.PlayLooping();
                        latestMsg = msgReceived;
                        charLayerPB.Load("base/characters/" + msgReceived.charName + "/(b)" + msgReceived.anim + ".gif");
                        prepWriteDispBoxes(msgReceived, msgReceived.textColor);
                        //dispTextRedraw.Enabled = true;
                        break;

                    case Command.List:
                        lstUsers.Items.AddRange(msgReceived.strMessage.Split('*'));
                        lstUsers.Items.RemoveAt(lstUsers.Items.Count - 1);
                        appendTxtLogSafe("<<<" + strName + " has entered the courtroom>>>\r\n");
                        break;
                }

                if (msgReceived.strMessage != null && msgReceived.cmdCommand != Command.List)
                {
                    appendTxtLogSafe(msgReceived.strMessage + "\r\n");
                }

                byteData = new byte[1024];

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

        private void appendTxtLogSafe(string txt)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => txtLog.Text += txt));
                return;
            }
            txtLog.Text += txt;
        }

        private void prepWriteDispBoxes(Data msg, Color newColor)
        {
            string msgText = msg.strMessage;
            textToDisp = new string[3];
            textToDisp[0] = "";
            textToDisp[1] = "";
            textToDisp[2] = "";
            displayMsg1.Text = "";
            displayMsg2.Text = "";
            displayMsg3.Text = "";
            if (newColor.ToKnownColor() != KnownColor.PeachPuff)
                setDispMsgColor(newColor);

            msgText = msg.strMessage.Substring(msg.strName.Length + 2);

            for (int i = 0; i < 3; i++)
            {
                if (TextRenderer.MeasureText(msgText, displayMsg1.Font).Width > 240)
                {
                    string[] parts = msgText.Split(' ');
                    string combined = "";
                    for (int x = 0; x < parts.Length; x++)
                    {
                        /* if (i == 2) // test bit of code to try and help me test these stupid loops
                        {
                            string test = "";
                        } */

                        //TO DO: Add a handler in case a single word is too long, and measure it by characters
                        if (TextRenderer.MeasureText(parts[x], displayMsg1.Font).Width <= 246)
                        {
                            if (TextRenderer.MeasureText(combined + parts[x] + " ", displayMsg1.Font).Width <= 246)
                            {
                                combined = combined + parts[x] + " "; // TO DO: Don't add a space after the last word
                            }
                            else
                            {
                                textToDisp[i] = combined;
                                msgText = msgText.Substring(combined.Length);
                                break;
                            }
                        }
                        else
                        {
                            for (int y = 0; y < parts[x].Length; y++)
                            {
                                if (TextRenderer.MeasureText(combined + parts[x].Substring(0, y), displayMsg1.Font).Width <= 246)
                                {
                                    combined = combined + parts[x].Substring(0, y); // TO DO: Don't add a space after the last word
                                }
                                else
                                {
                                    textToDisp[i] = combined;
                                    msgText = msgText.Substring(combined.Length);
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    textToDisp[i] = msgText;
                    break;
                }
            }
            switch (iniParser.GetSide(latestMsg.charName))
            {
                case "def":
                    backgroundPB.Load("base/background/default/defenseempty.png");
                    deskLayerPB.Image = Properties.Resources.Defense_Bench_Overlay_resized;
                    break;
                case "pro":
                    backgroundPB.Load("base/background/default/prosecutorempty.png");
                    deskLayerPB.Image = Properties.Resources.Prosecutor_Bench_Overlay_resized;
                    break;
                case "jud":
                    backgroundPB.Load("base/background/default/judgestand.png");
                    deskLayerPB.Image = null;
                    break;
                case "wit":
                    backgroundPB.Load("base/background/default/witnessempty.png");
                    deskLayerPB.Image = Properties.Resources.PW_Witness_Stand_Overlay;
                    break;
            }
            //dispTextRedraw.Enabled = true;
            blipPlayer.PlayLooping();
            redraw = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Text = "AODXClient: " + strName;

            //The user has logged into the system so we now request the server to send
            //the names of all users who are in the chat room
            Data msgToSend = new Data();
            msgToSend.cmdCommand = Command.List;
            msgToSend.strName = strName;
            msgToSend.strMessage = null;

            byteData = msgToSend.ToByte();

            clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnSend), null);

            byteData = new byte[1024];
            //Start listening to the data asynchronously
            clientSocket.BeginReceive(byteData,
                                       0,
                                       byteData.Length,
                                       SocketFlags.None,
                                       new AsyncCallback(OnReceive),
                                       null);

        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            if (txtMessage.Text.Length == 0)
                btnSend.Enabled = false;
            else
                btnSend.Enabled = true;
        }

        private void AODXClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* if (MessageBox.Show("Are you sure you want to leave the courtroom?", "AODXClient: " + strName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            } */

            try
            {
                //Send a message to logout of the server
                Data msgToSend = new Data();
                msgToSend.cmdCommand = Command.Logout;
                msgToSend.strName = strName;
                msgToSend.strMessage = null;

                byte[] b = msgToSend.ToByte();
                clientSocket.Send(b, 0, b.Length, SocketFlags.None);
                clientSocket.Close();
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AODXClient: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend_Click(sender, null);
            }
        }

        private void dispTextRedraw_Tick(object sender, EventArgs e)
        {
            if (redraw == true)
            {
                for (int x = 0; x < textToDisp.Length; x++)
                {
                    if (textToDisp[x] == null)
                        textToDisp[x] = "";
                }

                if (textToDisp.Length >= 0 && displayMsg1.Text != textToDisp[0])
                {
                    displayMsg1.Text = textToDisp[0].Substring(0, textTicks);
                    if (displayMsg1.Text != textToDisp[0])
                    {
                        textTicks++;
                    }
                    else
                    {
                        textTicks = 1;
                    }
                }
                else if (textToDisp.Length >= 1 && displayMsg2.Text != textToDisp[1])
                {
                    displayMsg2.Text = textToDisp[1].Substring(0, textTicks);
                    if (displayMsg2.Text != textToDisp[1])
                    {
                        textTicks++;
                    }
                    else
                    {
                        textTicks = 1;
                    }
                }
                else if (textToDisp.Length >= 2 && displayMsg3.Text != textToDisp[2])
                {
                    displayMsg3.Text = textToDisp[2].Substring(0, textTicks);
                    if (displayMsg3.Text != textToDisp[2])
                    {
                        textTicks++;
                    }
                    else
                    {
                        textTicks = 1;
                    }
                }
                else
                {
                    blipPlayer.Stop();
                    charLayerPB.Load("base/characters/" + latestMsg.charName + "/(a)" + latestMsg.anim + ".gif");
                    redraw = false;
                }
            }
        }

        private void txtColorChanger_Click(object sender, EventArgs e)
        {
            if (colorIndex < 3)
            {
                colorIndex++;
            }
            else
            {
                colorIndex = 0;
            }

            switch (colorIndex)
            {
                case 0:
                    txtMessage.ForeColor = Color.Black;
                    selectedColor = Color.White;
                    txtColorChanger.Text = "Text Color: White";
                    break;
                case 1:
                    txtMessage.ForeColor = Color.DeepSkyBlue;
                    selectedColor = Color.DeepSkyBlue;
                    txtColorChanger.Text = "Text Color: Blue";
                    break;
                case 2:
                    txtMessage.ForeColor = Color.FromArgb(0, 255, 0);
                    selectedColor = Color.FromArgb(0, 255, 0);
                    txtColorChanger.Text = "Text Color: Green";
                    break;
                case 3:
                    txtMessage.ForeColor = Color.OrangeRed;
                    selectedColor = Color.OrangeRed;
                    txtColorChanger.Text = "Text Color: Orange";
                    break;
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