using System;
using CSCore;
using CSCore.Codecs.WAV;
using CSCore.SoundOut;
using System.IO;
using System.Media;
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
        DataInfo,   //Get a list of music filenames, evidence, and currently unused characters that the server has loaded
        PacketSize, //Get the size in bytes of the next incoming packet so we can size our receiving packet accordingly. Used for receiving the DataInfo packets.
        Null        //No command
    }

    public partial class ClientForm : Form
    {
        public Socket clientSocket; //The main client socket
        public string strName;      //Character that the user is playing as
        private int selectedAnim = 1;
        private int colorIndex = 0;
        private Color selectedColor = Color.White;
        private string[] textToDisp = new string[3];
        private int textTicks = 1;
        private bool redraw = false;
        private int emoCount;
        private int emoPage;
        private int emoMaxPages;
        private Data latestMsg;
        private WaveFileReader blipReader;
        private DirectSoundOut blipPlayer = new DirectSoundOut();
        private WaveFileReader wr;
        private DirectSoundOut sfxPlayer = new DirectSoundOut();
        private byte[] byteData;
        private int preAnimTime;
        private int soundTime;
        private int curPreAnimTime;
        private int curSoundTime;
        private string curPreAnim;

        public ClientForm()
        {
            InitializeComponent();
            blipReader = new WaveFileReader("base/sounds/general/sfx-blipmale.wav");
            //blipReader = new WaveFileReader(Properties.Resources.sfx_blipmale);
            //blipPlayer.Latency = 200;
            blipPlayer.Initialize(FluentExtensions.Loop(blipReader));
            backgroundPB.BackColor = Color.Transparent;
            backgroundPB.Load("base/background/default/defenseempty.png");
            backgroundPB.Controls.Add(charLayerPB);
            charLayerPB.BackColor = Color.Transparent;
            charLayerPB.Image = null;
            charLayerPB.Controls.Add(deskLayerPB);
            deskLayerPB.BackColor = Color.Transparent;
            deskLayerPB.Load("base/background/default/defbench.png");
            deskLayerPB.Controls.Add(chatBGLayerPB);
            chatBGLayerPB.Load("base/misc/chat.png");
            chatBGLayerPB.BackColor = Color.Transparent;
            chatBGLayerPB.Controls.Add(objectLayerPB);
            objectLayerPB.BackColor = Color.Transparent;
            objectLayerPB.Image = null;
            objectLayerPB.Controls.Add(displayMsg1);
            objectLayerPB.Controls.Add(displayMsg2);
            objectLayerPB.Controls.Add(displayMsg3);
            nameLabel.BackColor = Color.Transparent;
            objectLayerPB.Controls.Add(nameLabel);
            displayMsg1.BackColor = Color.Transparent;
            displayMsg2.BackColor = Color.Transparent;
            displayMsg3.BackColor = Color.Transparent;
            arrowLeft.Load("base/misc/btn_arrowLeft.png");
            arrowLeft.Enabled = false;
            arrowLeft.Visible = false;
            arrowRight.Load("base/misc/btn_arrowRight.png");
            arrowRight.Enabled = false;
            arrowRight.Visible = false;
            clearDispMsg();
            setDispMsgColor(Color.White);

            //displayMsg.Text = "Sample Text";
            //Refresh();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            Text = "AODXClient: " + strName;

            emoCount = iniParser.GetEmoNum(strName);
            emoMaxPages = (int)Math.Floor((decimal)(emoCount / 10));

            loadEmoButtons();

            //byteData = new byte[incomingSize];
            byteData = new byte[1024];

            //The user has logged into the system so we now request the server to send
            //the names of all users who are in the chat room
            Data msgToSend = new Data();
            msgToSend.cmdCommand = Command.List;
            msgToSend.strName = strName;

            byteData = msgToSend.ToByte();

            clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnSend), null);

            byteData = new byte[1024];

            //Start listening to the data asynchronously
            clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
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
                if (sfxPlayer != null)
                    sfxPlayer.Dispose();
                if (wr != null)
                    wr.Dispose();
                if (blipPlayer != null)
                    blipPlayer.Dispose();
                if (blipReader != null)
                    blipReader.Dispose();
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                if (Program.debug)
                    MessageBox.Show(ex.Message + ".\r\n" + ex.StackTrace.ToString(), "AODXClient: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void loadEmoButtons(bool redraw = true)
        {

            if (emoCount < 10)
            {
                arrowRight.Enabled = false;
                arrowRight.Visible = false;
                arrowLeft.Enabled = false;
                arrowLeft.Visible = false;
            }
            else if (emoPage == 0 & emoMaxPages > 0)
            {
                arrowRight.Enabled = true;
                arrowRight.Visible = true;
                arrowLeft.Enabled = false;
                arrowLeft.Visible = false;
            }
            else if (emoPage > 0 & emoPage < emoMaxPages)
            {
                arrowRight.Enabled = true;
                arrowRight.Visible = true;
                arrowLeft.Enabled = true;
                arrowLeft.Visible = true;
            }
            else if (emoPage >= emoMaxPages)
            {
                arrowRight.Enabled = false;
                arrowRight.Visible = false;
                arrowLeft.Enabled = true;
                arrowLeft.Visible = true;
            }
            if (redraw)
            {
                emoButton1.Image = null;
                emoButton1.Enabled = false;
                emoButton1.Visible = false;
                emoButton2.Image = null;
                emoButton2.Enabled = false;
                emoButton2.Visible = false;
                emoButton3.Image = null;
                emoButton3.Enabled = false;
                emoButton3.Visible = false;
                emoButton4.Image = null;
                emoButton4.Enabled = false;
                emoButton4.Visible = false;
                emoButton5.Image = null;
                emoButton5.Enabled = false;
                emoButton5.Visible = false;
                emoButton6.Image = null;
                emoButton6.Enabled = false;
                emoButton6.Visible = false;
                emoButton7.Image = null;
                emoButton7.Enabled = false;
                emoButton7.Visible = false;
                emoButton8.Image = null;
                emoButton8.Enabled = false;
                emoButton8.Visible = false;
                emoButton9.Image = null;
                emoButton9.Enabled = false;
                emoButton9.Visible = false;
                emoButton10.Image = null;
                emoButton10.Enabled = false;
                emoButton10.Visible = false;
            }

            if (emoCount - (0 + (10 * emoPage)) <= 0)
                return;
            emoButton1.Enabled = true;
            emoButton1.Visible = true;
            emoButton1.EmoNum = 1 + (10 * emoPage);
            if (emoButton1.EmoNum == selectedAnim)
                emoButton1.Load("base/characters/" + strName + "/emotions/button" + emoButton1.EmoNum + "_on.png");
            else
                emoButton1.Load("base/characters/" + strName + "/emotions/button" + emoButton1.EmoNum + "_off.png");
            if (emoCount - (1 + (10 * emoPage)) <= 0)
                return;
            emoButton2.Enabled = true;
            emoButton2.Visible = true;
            emoButton2.EmoNum = 2 + (10 * emoPage);
            if (emoButton2.EmoNum == selectedAnim)
                emoButton2.Load("base/characters/" + strName + "/emotions/button" + emoButton2.EmoNum + "_on.png");
            else
                emoButton2.Load("base/characters/" + strName + "/emotions/button" + emoButton2.EmoNum + "_off.png");
            if (emoCount - (2 + (10 * emoPage)) <= 0)
                return;
            emoButton3.Enabled = true;
            emoButton3.Visible = true;
            emoButton3.EmoNum = 3 + (10 * emoPage);
            if (emoButton3.EmoNum == selectedAnim)
                emoButton3.Load("base/characters/" + strName + "/emotions/button" + emoButton3.EmoNum + "_on.png");
            else
                emoButton3.Load("base/characters/" + strName + "/emotions/button" + emoButton3.EmoNum + "_off.png");
            if (emoCount - (3 + (10 * emoPage)) <= 0)
                return;
            emoButton4.Enabled = true;
            emoButton4.Visible = true;
            emoButton4.EmoNum = 4 + (10 * emoPage);
            if (emoButton4.EmoNum == selectedAnim)
                emoButton4.Load("base/characters/" + strName + "/emotions/button" + emoButton4.EmoNum + "_on.png");
            else
                emoButton4.Load("base/characters/" + strName + "/emotions/button" + emoButton4.EmoNum + "_off.png");
            if (emoCount - (5 + (10 * emoPage)) <= 0)
                return;
            emoButton5.Enabled = true;
            emoButton5.Visible = true;
            emoButton5.EmoNum = 5 + (10 * emoPage);
            if (emoButton5.EmoNum == selectedAnim)
                emoButton5.Load("base/characters/" + strName + "/emotions/button" + emoButton5.EmoNum + "_on.png");
            else
                emoButton5.Load("base/characters/" + strName + "/emotions/button" + emoButton5.EmoNum + "_off.png");
            if (emoCount - (6 + (10 * emoPage)) <= 0)
                return;
            emoButton6.Enabled = true;
            emoButton6.Visible = true;
            emoButton6.EmoNum = 6 + (10 * emoPage);
            if (emoButton6.EmoNum == selectedAnim)
                emoButton6.Load("base/characters/" + strName + "/emotions/button" + emoButton6.EmoNum + "_on.png");
            else
                emoButton6.Load("base/characters/" + strName + "/emotions/button" + emoButton6.EmoNum + "_off.png");
            if (emoCount - (7 + (10 * emoPage)) <= 0)
                return;
            emoButton7.Enabled = true;
            emoButton7.Visible = true;
            emoButton7.EmoNum = 7 + (10 * emoPage);
            if (emoButton7.EmoNum == selectedAnim)
                emoButton7.Load("base/characters/" + strName + "/emotions/button" + emoButton7.EmoNum + "_on.png");
            else
                emoButton7.Load("base/characters/" + strName + "/emotions/button" + emoButton7.EmoNum + "_off.png");
            if (emoCount - (8 + (10 * emoPage)) <= 0)
                return;
            emoButton8.Enabled = true;
            emoButton8.Visible = true;
            emoButton8.EmoNum = 8 + (10 * emoPage);
            if (emoButton8.EmoNum == selectedAnim)
                emoButton8.Load("base/characters/" + strName + "/emotions/button" + emoButton8.EmoNum + "_on.png");
            else
                emoButton8.Load("base/characters/" + strName + "/emotions/button" + emoButton8.EmoNum + "_off.png");
            if (emoCount - (9 + (10 * emoPage)) <= 0)
                return;
            emoButton9.Enabled = true;
            emoButton9.Visible = true;
            emoButton9.EmoNum = 9 + (10 * emoPage);
            if (emoButton9.EmoNum == selectedAnim)
                emoButton9.Load("base/characters/" + strName + "/emotions/button" + emoButton9.EmoNum + "_on.png");
            else
                emoButton9.Load("base/characters/" + strName + "/emotions/button" + emoButton9.EmoNum + "_off.png");
            if (emoCount - (10 + (10 * emoPage)) <= 0)
                return;
            emoButton10.Enabled = true;
            emoButton10.Visible = true;
            emoButton10.EmoNum = 10 + (10 * emoPage);
            if (emoButton10.EmoNum == selectedAnim)
                emoButton10.Load("base/characters/" + strName + "/emotions/button" + emoButton10.EmoNum + "_on.png");
            else
                emoButton10.Load("base/characters/" + strName + "/emotions/button" + emoButton10.EmoNum + "_off.png");
        }

        //Broadcast the message typed by the user to everyone
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                //Fill the info for the message to be send
                Data msgToSend = new Data();

                msgToSend.strName = strName;
                msgToSend.anim = selectedAnim;
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
                if (Program.debug)
                    MessageBox.Show("Unable to send message to the server.\r\n" + ex.Message + ".\r\n" + ex.StackTrace.ToString(), "AODXClient: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message + ".\r\n" + ex.StackTrace.ToString(), "AODXClient: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        break;

                    case Command.Logout:
                        break;

                    case Command.Message:
                        //blipPlayer.PlayLooping();
                        latestMsg = msgReceived;

                        curPreAnimTime = 0;
                        curPreAnimTime = 0;
                        curPreAnim = null;
                        soundTime = 0;
                        curSoundTime = 0;

                        if (iniParser.GetSoundName(msgReceived.strName, msgReceived.anim) != "1" && iniParser.GetSoundTime(msgReceived.strName, msgReceived.anim) > 0 & File.Exists("base/sounds/general/" + iniParser.GetSoundName(latestMsg.strName, latestMsg.anim) + ".wav"))
                        {
                            sfxPlayer.Stop();
                            wr = new WaveFileReader("base/sounds/general/" + iniParser.GetSoundName(latestMsg.strName, latestMsg.anim) + ".wav");
                            sfxPlayer.Initialize(wr);
                            soundTime = iniParser.GetSoundTime(msgReceived.strName, msgReceived.anim);
                        }

                        if (iniParser.GetAnimType(msgReceived.strName, msgReceived.anim) == 5)
                            ChangeSides(true);
                        else
                            ChangeSides();

                        if (iniParser.GetAnimType(msgReceived.strName, msgReceived.anim) == 5 | iniParser.GetPreAnim(msgReceived.strName, msgReceived.anim) == null)
                        {
                            charLayerPB.Enabled = true;
                            charLayerPB.Image = Image.FromFile("base/characters/" + msgReceived.strName + "/(b)" + iniParser.GetAnim(msgReceived.strName, msgReceived.anim) + ".gif");
                            prepWriteDispBoxes(msgReceived, msgReceived.textColor);
                        }
                        else
                        {
                            //charLayerPB.Enabled = false;
                            charLayerPB.Image = Image.FromFile("base/characters/" + msgReceived.strName + "/" + iniParser.GetPreAnim(msgReceived.strName, msgReceived.anim) + ".gif");
                            preAnimTime = iniParser.GetPreAnimTime(msgReceived.strName, msgReceived.anim);
                            curPreAnim = iniParser.GetPreAnim(msgReceived.strName, msgReceived.anim);
                        }
                        //dispTextRedraw.Enabled = true;
                        break;

                    case Command.List:
                        appendTxtLogSafe("<<<" + strName + " has entered the courtroom>>>\r\n");
                        break;
                    case Command.DataInfo:
                        //Do the stuff with the incoming server data here

                        //The user has logged into the system so we now request the server to send
                        //the names of all users who are in the chat room
                        Data msgToSend = new Data();
                        msgToSend.cmdCommand = Command.Login;
                        msgToSend.strName = strName;

                        byteData = new byte[1024];
                        byteData = msgToSend.ToByte();

                        clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnSend), null);

                        byteData = new byte[1024];
                        break;
                    case Command.PacketSize:
                        break;
                }

                if (msgReceived.strMessage != null && msgReceived.cmdCommand != Command.List && msgReceived.cmdCommand != Command.DataInfo && msgReceived.cmdCommand != Command.PacketSize)
                {
                    appendTxtLogSafe(msgReceived.strMessage + "\r\n");
                }

                if (msgReceived.cmdCommand != Command.PacketSize)
                    byteData = new byte[1024];
                else
                    byteData = new byte[Convert.ToInt32(msgReceived.strMessage)];

                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);

            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                if (Program.debug)
                    MessageBox.Show(ex.Message + ".\r\n" + ex.StackTrace.ToString(), "AODXClient: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            clearDispMsg();
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
            //dispTextRedraw.Enabled = true;
            nameLabel.Text = latestMsg.strName;
            //blipPlayer.Initialize(blipReader);
            blipPlayer.Stop();
            blipPlayer.Play();
            redraw = true;
        }

        private void ChangeSides(bool zoom = false)
        {
            try
            {
                switch (iniParser.GetSide(latestMsg.strName))
                {
                    case "def":
                        if (!zoom)
                        {
                            backgroundPB.Image = Image.FromFile("base/background/default/defenseempty.png");
                            deskLayerPB.Image = Properties.Resources.Defense_Bench_Overlay_resized;
                        }
                        else
                        {
                            backgroundPB.Image = Image.FromFile("base/misc/ani_zoom_def.gif");
                            deskLayerPB.Image = null;
                        }
                        break;
                    case "pro":
                        if (!zoom)
                        {
                            backgroundPB.Image = Image.FromFile("base/background/default/prosecutorempty.png");
                            deskLayerPB.Image = Properties.Resources.Prosecutor_Bench_Overlay_resized;
                        }
                        else
                        {
                            backgroundPB.Image = Image.FromFile("base/misc/ani_zoom_pro.gif");
                            deskLayerPB.Image = null;
                        }
                        break;
                    case "jud":
                        if (!zoom)
                        {
                            backgroundPB.Image = Image.FromFile("base/background/default/judgestand.png");
                            deskLayerPB.Image = null;
                        }
                        else
                        {
                            backgroundPB.Image = Image.FromFile("base/misc/ani_zoom_def.gif");
                            deskLayerPB.Image = null;
                        }
                        break;
                    case "wit":
                        if (!zoom)
                        {
                            backgroundPB.Image = Image.FromFile("base/background/default/witnessempty.png");
                            deskLayerPB.Image = Properties.Resources.PW_Witness_Stand_Overlay;
                        }
                        else
                        {
                            backgroundPB.Image = Image.FromFile("base/misc/ani_zoom_pro.gif");
                            deskLayerPB.Image = null;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                if (Program.debug)
                    MessageBox.Show(ex.Message + ".\r\n" + ex.StackTrace.ToString(), "AODXClient", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            if (txtMessage.Text.Length == 0)
                btnSend.Enabled = false;
            else
                btnSend.Enabled = true;
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend_Click(sender, null);
            }
        }

        private void blipTimer_Tick(object sender, EventArgs e)
        {
            if (redraw == true)
            {
                //if (blipPlayer.PlaybackState == PlaybackState.Stopped)
                blipPlayer.Stop();
                blipPlayer.Play();
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
                    charLayerPB.Image = Image.FromFile("base/characters/" + latestMsg.strName + "/(a)" + iniParser.GetAnim(latestMsg.strName, latestMsg.anim) + ".gif");
                    redraw = false;
                }
            }
        }

        private void animTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (soundTime > 0 && curSoundTime < (soundTime - 4))
                    curSoundTime++;
                else if (soundTime > 0 && curSoundTime == (soundTime - 4))
                {
                    soundTime = 0; // This prevents the song from playing repeatedly every 60 ms
                    sfxPlayer.Play();
                }

                if (preAnimTime > 0 && curPreAnim != null)
                {
                    if (curPreAnimTime < preAnimTime)
                    {
                        //if (charLayerPB.Image.FrameDimensionsList.Length > curPreAnimTime)
                        //charLayerPB.Image.SelectActiveFrame(new System.Drawing.Imaging.FrameDimension(charLayerPB.Image.FrameDimensionsList[curPreAnimTime]), curPreAnimTime);
                        curPreAnimTime++;
                    }
                    else
                    {
                        curPreAnimTime = 0;
                        curPreAnimTime = 0;
                        curPreAnim = null;

                        charLayerPB.Image = Image.FromFile("base/characters/" + latestMsg.strName + "/(b)" + iniParser.GetAnim(latestMsg.strName, latestMsg.anim) + ".gif");
                        charLayerPB.Enabled = true;
                        prepWriteDispBoxes(latestMsg, latestMsg.textColor);
                        return;
                    }
                }
                /* else
                {
                    curPreAnimTime = 0;
                    curPreAnimTime = 0;
                    curPreAnim = null;

                    if (latestMsg != null)
                    {
                        charLayerPB.Load("base/characters/" + latestMsg.strName + "/(b)" + latestMsg.anim + ".gif");
                        prepWriteDispBoxes(latestMsg, latestMsg.textColor);
                    }
                } */
            }
            catch (Exception ex)
            {
                if (Program.debug)
                    MessageBox.Show(ex.Message + ".\r\n" + ex.StackTrace.ToString(), "AODXClient", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void emoButton1_Click(object sender, EventArgs e)
        {
            selectedAnim = emoButton1.EmoNum;
            loadEmoButtons(false);
        }

        private void emoButton2_Click(object sender, EventArgs e)
        {
            selectedAnim = emoButton2.EmoNum;
            loadEmoButtons(false);
        }

        private void emoButton3_Click(object sender, EventArgs e)
        {
            selectedAnim = emoButton3.EmoNum;
            loadEmoButtons(false);
        }

        private void emoButton4_Click(object sender, EventArgs e)
        {
            selectedAnim = emoButton4.EmoNum;
            loadEmoButtons(false);
        }

        private void emoButton5_Click(object sender, EventArgs e)
        {
            selectedAnim = emoButton5.EmoNum;
            loadEmoButtons(false);
        }

        private void emoButton6_Click(object sender, EventArgs e)
        {
            selectedAnim = emoButton6.EmoNum;
            loadEmoButtons(false);
        }

        private void emoButton7_Click(object sender, EventArgs e)
        {
            selectedAnim = emoButton7.EmoNum;
            loadEmoButtons(false);
        }

        private void emoButton8_Click(object sender, EventArgs e)
        {
            selectedAnim = emoButton8.EmoNum;
            loadEmoButtons(false);
        }

        private void emoButton9_Click(object sender, EventArgs e)
        {
            selectedAnim = emoButton9.EmoNum;
            loadEmoButtons(false);
        }

        private void emoButton10_Click(object sender, EventArgs e)
        {
            selectedAnim = emoButton10.EmoNum;
            loadEmoButtons(false);
        }

        private void arrowLeft_Click(object sender, EventArgs e)
        {
            emoPage--;
            loadEmoButtons();
        }

        private void arrowRight_Click(object sender, EventArgs e)
        {
            emoPage++;
            loadEmoButtons();
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
            anim = 1;
            textColor = Color.PeachPuff;
            extraData = null;
        }

        //Converts the bytes into an object of type Data
        public Data(byte[] data)
        {
            //The first four bytes are for the Command
            cmdCommand = (Command)BitConverter.ToInt32(data, 0);

            //The next four store the length of the name
            int nameLen = BitConverter.ToInt32(data, 4);

            anim = BitConverter.ToInt32(data, 8);

            int textColorLen = BitConverter.ToInt32(data, 12);

            //The next four store the length of the message
            int msgLen = BitConverter.ToInt32(data, 16);

            //This check makes sure that strName has been passed in the array of bytes
            if (nameLen > 0)
                strName = Encoding.UTF8.GetString(data, 20, nameLen);
            else
                strName = null;

            if (textColorLen > 0)
                textColor = Color.FromArgb(BitConverter.ToInt32(data, 20 + nameLen));
            else
                textColor = Color.White;

            //This checks for a null message field
            if (msgLen > 0)
                strMessage = Encoding.UTF8.GetString(data, 20 + nameLen + textColorLen, msgLen);
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

            result.AddRange(BitConverter.GetBytes(anim));

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

            //if (textColor != Color.PeachPuff)
            result.AddRange(BitConverter.GetBytes(textColor.ToArgb()));

            //And, lastly we add the message text to our array of bytes
            if (strMessage != null)
                result.AddRange(Encoding.UTF8.GetBytes(strMessage));

            return result.ToArray();
        }

        public string strName;      //Name by which the client logs into the room
        public int anim;
        public byte[] extraData;
        public Color textColor;
        public string strMessage;   //Message text
        public Command cmdCommand;  //Command type (login, logout, send message, etcetera)
    }
}