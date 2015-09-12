using System;
using CSCore;
using CSCore.Codecs.WAV;
using CSCore.Codecs.MP3;
using CSCore.SoundOut;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Xml;
using System.Net.Sockets;

namespace Client
{
    //The commands for interaction between the server and the client
    enum Command
    {
        Login,          //Log into the server
        Logout,         //Logout of the server
        Message,        //Send a text message to all the chat clients
        List,           //Get a list of users in the chat room from the server
        DataInfo,       //Get a list of music filenames, evidence, and currently unused characters that the server has loaded
        PacketSize,     //Get the size in bytes of the next incoming packet so we can size our receiving packet accordingly. Used for receiving the DataInfo packets.
        ChangeMusic,    //Makes the server tell all clients to start playing the selected audio file
        ChangeHealth,
        Evidence,
        Present,
        Disconnect,
        Null            //No command
    }

    public partial class ClientForm : Form
    {
        #region declarations
        public Socket clientSocket; //The main client socket
        public string strName;      //Character that the user is playing as
        public List<string> songs; // = new List<string>();
        public List<Evidence> eviList = new List<Evidence>();
        private int selectedAnim = 1;
        private int selectedEvidence = 0;
        private byte callout = 0;
        private int colorIndex = 0;
        private Color selectedColor = Color.White;
        private string[] textToDisp = new string[3];
        private int textTicks = 1;
        private bool redraw = false;
        private bool newGuy = false;
        private int emoCount;
        private int emoPage;
        private int emoMaxPages;
        private int eviCount = 0;
        private int eviPage = 0;
        private int eviMaxPages = 0;
        private bool sendEnabled = true;
        private bool mute = false;
        private byte defHealth = 5;
        private byte proHealth = 5;
        private Data latestMsg;
        private WaveFileReader blipReader;
        private DirectSoundOut blipPlayer = new DirectSoundOut();
        private WaveFileReader wr;
        private DirectSoundOut sfxPlayer = new DirectSoundOut();
        private DmoMp3Decoder musicReader;
        private DirectSoundOut musicPlayer = new DirectSoundOut();
        private byte[] byteData;
        private int preAnimTime;
        private int soundTime;
        private int curPreAnimTime;
        private int curSoundTime;
        private string curPreAnim;
        private bool readyToPresent = false;
        private bool presenting = false;
        //global brushes with ordinary/selected colors
        private SolidBrush reportsForegroundBrushSelected = new SolidBrush(Color.White);
        private SolidBrush reportsForegroundBrush = new SolidBrush(Color.Black);
        private SolidBrush reportsBackgroundBrushSelected = new SolidBrush(Color.FromKnownColor(KnownColor.Highlight));
        private SolidBrush reportsBackgroundBrushGreen = new SolidBrush(Color.LightGreen);
        private SolidBrush reportsBackgroundBrushRed = new SolidBrush(Color.Red);
        private AboutBox AboutForm = new AboutBox();
        private PrivateFontCollection fonts = new PrivateFontCollection();
        #endregion

        #region constructor
        public ClientForm()
        {
            InitializeComponent();
            blipReader = new WaveFileReader("base/sounds/general/sfx-blipmale.wav");
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
            objectLayerPB.Controls.Add(testimonyPB);
            displayMsg1.BackColor = Color.Transparent;
            displayMsg2.BackColor = Color.Transparent;
            displayMsg3.BackColor = Color.Transparent;
            arrowLeft.Load("base/misc/btn_arrowLeft.png");
            arrowLeft.Enabled = false;
            arrowLeft.Visible = false;
            arrowRight.Load("base/misc/btn_arrowRight.png");
            arrowRight.Enabled = false;
            arrowRight.Visible = false;
            btn_objection.Image = Image.FromFile("base/misc/btn_objection_off.png");
            btn_objection.Visible = true;
            btn_holdit.Image = Image.FromFile("base/misc/btn_holdit_off.png");
            btn_holdit.Visible = true;
            btn_takethat.Image = Image.FromFile("base/misc/btn_takethat_off.png");
            btn_takethat.Visible = true;
            btn_back.Parent = courtRecordPB;
            btn_present.Parent = courtRecordPB;
            btn_edit.Parent = courtRecordPB;

            crTitle.BackColor = Color.Transparent;
            crTitle.Parent = courtRecordPB;
            courtRecordPB.Controls.Add(evi1);
            courtRecordPB.Controls.Add(evi2);
            courtRecordPB.Controls.Add(evi3);
            courtRecordPB.Controls.Add(evi4);
            courtRecordPB.Controls.Add(evi5);
            courtRecordPB.Controls.Add(evi6);
            courtRecordPB.Controls.Add(evi7);
            courtRecordPB.Controls.Add(evi8);
            courtRecordPB.Controls.Add(evi9);
            courtRecordPB.Controls.Add(evi10);
            courtRecordPB.Controls.Add(evi11);
            courtRecordPB.Controls.Add(evi12);
            courtRecordPB.Controls.Add(evi13);
            courtRecordPB.Controls.Add(evi14);
            courtRecordPB.Controls.Add(evi15);
            courtRecordPB.Controls.Add(evi16);
            courtRecordPB.Controls.Add(evi17);
            courtRecordPB.Controls.Add(evi18);

            //btn_Exclaim.Parent = uiPanel;
            //btn_Mute.Parent = uiPanel;
            //txtColorChanger.Parent = uiPanel;
            //defHealthBar.Parent = uiPanel;
            //proHealthBar.Parent = uiPanel;
            clearDispMsg();
            setDispMsgColor(Color.White);

            //displayMsg.Text = "Sample Text";
            //Refresh();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            nameLabel.Text = "";

            if (iniParser.GetSide(strName) != "jud")
            {
                btn_crossexamination.Visible = false;
                btn_crossexamination.Enabled = false;
                btn_edit.Visible = false;
                btn_edit.Enabled = false;
                courtRecordPB.Image = Image.FromFile("base/misc/inventory.png");
                btn_testimony.Visible = false;
                btn_testimony.Enabled = false;
                btn_defminus.Visible = false;
                btn_defminus.Enabled = false;
                btn_defplus.Visible = false;
                btn_defplus.Enabled = false;
                btn_prominus.Visible = false;
                btn_prominus.Enabled = false;
                btn_proplus.Visible = false;
                btn_proplus.Enabled = false;
                txtLog.Size = new Size(240, 347);
            }

            fonts.AddFontFile("base/misc/Ace-Attorney-2.ttf");

            musicList.Items.Clear();

            foreach (string song in songs)
                musicList.Items.Add(song);

            Text = "AODXClient: " + strName;

            emoCount = iniParser.GetEmoNum(strName);
            emoMaxPages = (int)Math.Floor((decimal)(emoCount / 10));

            loadEmoButtons();
            loadEviButtons();

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
        #endregion

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
                if (clientSocket.Connected)
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
                if (musicPlayer != null)
                    musicPlayer.Dispose();
                if (musicReader != null)
                    musicReader.Dispose();

                if (Directory.Exists("base/cases"))
                    Directory.Delete("base/cases", true);
            }
            catch (SocketException)
            { }
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

        //custom method to draw the items, don't forget to set DrawMode of the ListBox to OwnerDrawFixed
        private void musicList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < musicList.Items.Count)
            {
                string text = musicList.Items[index].ToString();
                Graphics g = e.Graphics;

                //background:
                SolidBrush backgroundBrush;
                if (selected)
                    backgroundBrush = reportsBackgroundBrushSelected;
                else if (!File.Exists("base/sounds/music/" + musicList.Items[index]))
                    backgroundBrush = reportsBackgroundBrushRed;
                else
                    backgroundBrush = reportsBackgroundBrushGreen;
                g.FillRectangle(backgroundBrush, e.Bounds);

                //text:
                SolidBrush foregroundBrush = (selected) ? reportsForegroundBrushSelected : reportsForegroundBrush;
                g.DrawString(text, e.Font, foregroundBrush, musicList.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
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
            emoButton1.Index = 1 + (10 * emoPage);
            if (emoButton1.Index == selectedAnim)
                emoButton1.Load("base/characters/" + strName + "/emotions/button" + emoButton1.Index + "_on.png");
            else
                emoButton1.Load("base/characters/" + strName + "/emotions/button" + emoButton1.Index + "_off.png");
            if (emoCount - (1 + (10 * emoPage)) <= 0)
                return;
            emoButton2.Enabled = true;
            emoButton2.Visible = true;
            emoButton2.Index = 2 + (10 * emoPage);
            if (emoButton2.Index == selectedAnim)
                emoButton2.Load("base/characters/" + strName + "/emotions/button" + emoButton2.Index + "_on.png");
            else
                emoButton2.Load("base/characters/" + strName + "/emotions/button" + emoButton2.Index + "_off.png");
            if (emoCount - (2 + (10 * emoPage)) <= 0)
                return;
            emoButton3.Enabled = true;
            emoButton3.Visible = true;
            emoButton3.Index = 3 + (10 * emoPage);
            if (emoButton3.Index == selectedAnim)
                emoButton3.Load("base/characters/" + strName + "/emotions/button" + emoButton3.Index + "_on.png");
            else
                emoButton3.Load("base/characters/" + strName + "/emotions/button" + emoButton3.Index + "_off.png");
            if (emoCount - (3 + (10 * emoPage)) <= 0)
                return;
            emoButton4.Enabled = true;
            emoButton4.Visible = true;
            emoButton4.Index = 4 + (10 * emoPage);
            if (emoButton4.Index == selectedAnim)
                emoButton4.Load("base/characters/" + strName + "/emotions/button" + emoButton4.Index + "_on.png");
            else
                emoButton4.Load("base/characters/" + strName + "/emotions/button" + emoButton4.Index + "_off.png");
            if (emoCount - (5 + (10 * emoPage)) <= 0)
                return;
            emoButton5.Enabled = true;
            emoButton5.Visible = true;
            emoButton5.Index = 5 + (10 * emoPage);
            if (emoButton5.Index == selectedAnim)
                emoButton5.Load("base/characters/" + strName + "/emotions/button" + emoButton5.Index + "_on.png");
            else
                emoButton5.Load("base/characters/" + strName + "/emotions/button" + emoButton5.Index + "_off.png");
            if (emoCount - (6 + (10 * emoPage)) <= 0)
                return;
            emoButton6.Enabled = true;
            emoButton6.Visible = true;
            emoButton6.Index = 6 + (10 * emoPage);
            if (emoButton6.Index == selectedAnim)
                emoButton6.Load("base/characters/" + strName + "/emotions/button" + emoButton6.Index + "_on.png");
            else
                emoButton6.Load("base/characters/" + strName + "/emotions/button" + emoButton6.Index + "_off.png");
            if (emoCount - (7 + (10 * emoPage)) <= 0)
                return;
            emoButton7.Enabled = true;
            emoButton7.Visible = true;
            emoButton7.Index = 7 + (10 * emoPage);
            if (emoButton7.Index == selectedAnim)
                emoButton7.Load("base/characters/" + strName + "/emotions/button" + emoButton7.Index + "_on.png");
            else
                emoButton7.Load("base/characters/" + strName + "/emotions/button" + emoButton7.Index + "_off.png");
            if (emoCount - (8 + (10 * emoPage)) <= 0)
                return;
            emoButton8.Enabled = true;
            emoButton8.Visible = true;
            emoButton8.Index = 8 + (10 * emoPage);
            if (emoButton8.Index == selectedAnim)
                emoButton8.Load("base/characters/" + strName + "/emotions/button" + emoButton8.Index + "_on.png");
            else
                emoButton8.Load("base/characters/" + strName + "/emotions/button" + emoButton8.Index + "_off.png");
            if (emoCount - (9 + (10 * emoPage)) <= 0)
                return;
            emoButton9.Enabled = true;
            emoButton9.Visible = true;
            emoButton9.Index = 9 + (10 * emoPage);
            if (emoButton9.Index == selectedAnim)
                emoButton9.Load("base/characters/" + strName + "/emotions/button" + emoButton9.Index + "_on.png");
            else
                emoButton9.Load("base/characters/" + strName + "/emotions/button" + emoButton9.Index + "_off.png");
            if (emoCount - (10 + (10 * emoPage)) <= 0)
                return;
            emoButton10.Enabled = true;
            emoButton10.Visible = true;
            emoButton10.Index = 10 + (10 * emoPage);
            if (emoButton10.Index == selectedAnim)
                emoButton10.Load("base/characters/" + strName + "/emotions/button" + emoButton10.Index + "_on.png");
            else
                emoButton10.Load("base/characters/" + strName + "/emotions/button" + emoButton10.Index + "_off.png");
        }

        private void loadEviButtons()
        {
            eviCount = eviList.Count();
            int nIndex = 0;
            foreach (Control ctrl in courtRecordPB.Controls)
            {
                if (ctrl is IndexButton)
                {
                    nIndex++;
                    ((IndexButton)ctrl).Index = nIndex + (18 * eviPage);

                    if (nIndex + (18 * eviPage) > eviCount)
                    {
                        ctrl.Enabled = false;
                        ctrl.Visible = false;
                    }
                    else
                    {
                        ((IndexButton)ctrl).Image = eviList[nIndex + (18 * eviPage) - 1].icon;
                        ctrl.Enabled = true;
                        ctrl.Visible = true;
                    }
                }
            }
        }

        private void updateHealth()
        {
            if (defHealth > 5)
                defHealth = 5;
            else if (defHealth < 0)
                defHealth = 0;

            if (proHealth > 5)
                proHealth = 5;
            else if (proHealth < 0)
                proHealth = 0;

            switch (defHealth)
            {
                case 5:
                    defHealthBar.Image = Image.FromFile("base/misc/healthbars/def.png");
                    break;

                case 4:
                    defHealthBar.Image = Image.FromFile("base/misc/healthbars/def4.png");
                    break;

                case 3:
                    defHealthBar.Image = Image.FromFile("base/misc/healthbars/def3.png");
                    break;

                case 2:
                    defHealthBar.Image = Image.FromFile("base/misc/healthbars/def2.png");
                    break;

                case 1:
                    defHealthBar.Image = Image.FromFile("base/misc/healthbars/def1.png");
                    break;

                case 0:
                    defHealthBar.Image = Image.FromFile("base/misc/healthbars/health5.png");
                    break;
            }

            switch (proHealth)
            {
                case 5:
                    proHealthBar.Image = Image.FromFile("base/misc/healthbars/pro.png");
                    break;

                case 4:
                    proHealthBar.Image = Image.FromFile("base/misc/healthbars/pro4.png");
                    break;

                case 3:
                    proHealthBar.Image = Image.FromFile("base/misc/healthbars/pro3.png");
                    break;

                case 2:
                    proHealthBar.Image = Image.FromFile("base/misc/healthbars/pro2.png");
                    break;

                case 1:
                    proHealthBar.Image = Image.FromFile("base/misc/healthbars/pro1.png");
                    break;

                case 0:
                    proHealthBar.Image = Image.FromFile("base/misc/healthbars/health5.png");
                    break;
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch (SocketException)
            { }
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

                    case Command.ChangeMusic:
                        if (msgReceived.strMessage != null && msgReceived.strMessage != "" & msgReceived.strName != null)
                        {
                            appendTxtLogSafe("<<<" + msgReceived.strName + " changed the music to " + msgReceived.strMessage + ">>>\r\n");
                            musicReader = new DmoMp3Decoder("base/sounds/music/" + msgReceived.strMessage);

                            if (musicPlayer.PlaybackState != PlaybackState.Stopped)
                                musicPlayer.Stop();
                            musicPlayer.Initialize(musicReader);
                            if (!mute)
                                musicPlayer.Play();
                        }
                        break;

                    case Command.ChangeHealth:
                        if (msgReceived.strName == "def")
                        {
                            if (msgReceived.strMessage == "-1")
                                defHealth--;
                            else if (msgReceived.strMessage == "+1")
                                defHealth++;
                        }
                        else if (msgReceived.strName == "pro")
                        {
                            if (msgReceived.strMessage == "-1")
                                proHealth--;
                            else if (msgReceived.strMessage == "+1")
                                proHealth++;
                        }

                        updateHealth();
                        break;

                    case Command.Message:
                    case Command.Present:
                        if (latestMsg != null && msgReceived.strName == latestMsg.strName)
                        {
                            newGuy = false;
                        }
                        else
                        {
                            newGuy = true;
                            testimonyPB.Image = null;
                        }

                        latestMsg = msgReceived;
                        objectLayerPB.Image = null;
                        objectLayerPB.Location = new Point(0, 0);
                        objectLayerPB.Size = new Size(256, 192);

                        if (msgReceived.callout <= 3)
                        {
                            sendEnabled = false;
                            curPreAnimTime = 0;
                            curPreAnimTime = 0;
                            curPreAnim = null;
                            soundTime = 0;
                            curSoundTime = 0;

                            if (msgReceived.callout > 0)
                                performCallout();

                            if (iniParser.GetSoundName(msgReceived.strName, msgReceived.anim) != "1" && iniParser.GetSoundTime(msgReceived.strName, msgReceived.anim) > 0 & File.Exists("base/sounds/general/" + iniParser.GetSoundName(latestMsg.strName, latestMsg.anim) + ".wav"))
                            {
                                sfxPlayer.Stop();
                                wr = new WaveFileReader("base/sounds/general/" + iniParser.GetSoundName(latestMsg.strName, latestMsg.anim) + ".wav");
                                sfxPlayer.Initialize(wr);
                                soundTime = iniParser.GetSoundTime(msgReceived.strName, msgReceived.anim);
                            }

                            /*  if (iniParser.GetSoundName(msgReceived.strName, msgReceived.anim) != "1" && iniParser.GetSoundTime(msgReceived.strName, msgReceived.anim) > 0 & (File.Exists("base/sounds/general/" + iniParser.GetSoundName(latestMsg.strName, latestMsg.anim) + ".wav") | File.Exists("base/characters/" + latestMsg.strName + "/" + iniParser.GetSoundName(latestMsg.strName, latestMsg.anim) + ".wav")))
                            {
                                sfxPlayer.Stop();
                                if (File.Exists("base/characters/" + latestMsg.strName + "/" + iniParser.GetSoundName(latestMsg.strName, latestMsg.anim) + ".wav"))
                                    wr = new WaveFileReader("base/characters/" + latestMsg.strName + "/" + iniParser.GetSoundName(latestMsg.strName, latestMsg.anim) + ".wav");
                                else
                                    wr = new WaveFileReader("base/sounds/general/" + iniParser.GetSoundName(latestMsg.strName, latestMsg.anim) + ".wav");
                                sfxPlayer.Initialize(wr);
                                soundTime = iniParser.GetSoundTime(msgReceived.strName, msgReceived.anim);
                            } */

                            if (iniParser.GetAnimType(msgReceived.strName, msgReceived.anim) == 5)
                                ChangeSides(true);
                            else
                                ChangeSides();

                            //If there is no pre-animation
                            if (iniParser.GetAnimType(msgReceived.strName, msgReceived.anim) == 5 | iniParser.GetPreAnim(msgReceived.strName, msgReceived.anim) == null | iniParser.GetPreAnimTime(msgReceived.strName, msgReceived.anim) <= 0)
                            {
                                charLayerPB.Enabled = true;
                                setCharSprite("base/characters/" + msgReceived.strName + "/(b)" + iniParser.GetAnim(msgReceived.strName, msgReceived.anim) + ".gif");
                                if (msgReceived.cmdCommand == Command.Present)
                                {
                                    sfxPlayer.Stop();
                                    wr = new WaveFileReader("base/sounds/general/sfx-shooop.wav");
                                    sfxPlayer.Initialize(wr);
                                    if (!mute)
                                        sfxPlayer.Play();

                                    switch (iniParser.GetSide(msgReceived.strName))
                                    {
                                        case "def":
                                            testimonyPB.Image = Image.FromFile("base/misc/ani_evidenceRight.gif");
                                            System.Threading.Thread.Sleep(100);
                                            testimonyPB.Location = new Point(173, 13);
                                            testimonyPB.Size = new Size(70, 70);
                                            testimonyPB.Image = eviList[Convert.ToInt32(msgReceived.strMessage.Split('|').Last())].icon;
                                            break;
                                        case "pro":
                                            testimonyPB.Image = Image.FromFile("base/misc/ani_evidenceLeft.gif");
                                            System.Threading.Thread.Sleep(100);
                                            testimonyPB.Location = new Point(13, 13);
                                            testimonyPB.Size = new Size(70, 70);
                                            testimonyPB.Image = eviList[Convert.ToInt32(msgReceived.strMessage.Split('|').Last())].icon;
                                            break;
                                        case "hld":
                                            testimonyPB.Image = Image.FromFile("base/misc/ani_evidenceLeft.gif");
                                            System.Threading.Thread.Sleep(100);
                                            testimonyPB.Location = new Point(13, 13);
                                            testimonyPB.Size = new Size(70, 70);
                                            testimonyPB.Image = eviList[Convert.ToInt32(msgReceived.strMessage.Split('|').Last())].icon;
                                            break;
                                        case "hlp":
                                            testimonyPB.Image = Image.FromFile("base/misc/ani_evidenceRight.gif");
                                            System.Threading.Thread.Sleep(100);
                                            testimonyPB.Location = new Point(173, 13);
                                            testimonyPB.Size = new Size(70, 70);
                                            testimonyPB.Image = eviList[Convert.ToInt32(msgReceived.strMessage.Split('|').Last())].icon;
                                            break;
                                        default:
                                            testimonyPB.Image = Image.FromFile("base/misc/ani_evidenceRight.gif");
                                            System.Threading.Thread.Sleep(100);
                                            testimonyPB.Location = new Point(173, 13);
                                            testimonyPB.Size = new Size(70, 70);
                                            testimonyPB.Image = eviList[Convert.ToInt32(msgReceived.strMessage.Split('|').Last())].icon;
                                            break;
                                    }

                                    msgReceived.strMessage = msgReceived.strMessage.Split('|')[0];
                                }
                                prepWriteDispBoxes(msgReceived, msgReceived.textColor);
                            }
                            else //if there is a pre-animation
                            {
                                //charLayerPB.Enabled = false;
                                setCharSprite("base/characters/" + msgReceived.strName + "/" + iniParser.GetPreAnim(msgReceived.strName, msgReceived.anim) + ".gif");
                                preAnimTime = iniParser.GetPreAnimTime(msgReceived.strName, msgReceived.anim);
                                curPreAnim = iniParser.GetPreAnim(msgReceived.strName, msgReceived.anim);
                            }
                            //dispTextRedraw.Enabled = true;
                        }
                        else
                        {
                            performCallout();
                        }
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

                if (msgReceived.strMessage != null & msgReceived.cmdCommand == Command.Message | msgReceived.cmdCommand == Command.Login | msgReceived.cmdCommand == Command.Logout)
                {
                    if (msgReceived.callout <= 3)
                        appendTxtLogSafe(msgReceived.strMessage + "\r\n");
                }

                if (msgReceived.cmdCommand != Command.PacketSize)
                    byteData = new byte[1024];
                else
                    byteData = new byte[Convert.ToInt32(msgReceived.strMessage)];

                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);

            }
            catch (SocketException)
            {
                if (MessageBox.Show("You have been kicked from the server.", "AODXClient", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    Close();
                }
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                if (Program.debug)
                    MessageBox.Show(ex.Message + ".\r\n" + ex.StackTrace.ToString(), "AODXClient: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void performCallout()
        {
            switch (latestMsg.callout)
            {
                case 0:

                    break;

                case 1:

                    nameLabel.Visible = false;
                    displayMsg1.Visible = false;
                    displayMsg2.Visible = false;
                    displayMsg3.Visible = false;
                    objectLayerPB.Image = Image.FromFile("base/misc/ani_objection.gif");
                    if (File.Exists("base/characters/" + latestMsg.strName + "/objection.wav"))
                        wr = new WaveFileReader("base/characters/" + latestMsg.strName + "/objection.wav");
                    else
                        wr = new WaveFileReader("base/sounds/general/sfx_objection.wav");

                    sfxPlayer.Initialize(wr);
                    if (!mute)
                        sfxPlayer.Play();

                    System.Threading.Thread.Sleep(1000);
                    break;

                case 2:

                    nameLabel.Visible = false;
                    displayMsg1.Visible = false;
                    displayMsg2.Visible = false;
                    displayMsg3.Visible = false;
                    objectLayerPB.Image = Image.FromFile("base/misc/ani_holdit.gif");
                    if (File.Exists("base/characters/" + latestMsg.strName + "/holdit.wav"))
                        wr = new WaveFileReader("base/characters/" + latestMsg.strName + "/holdit.wav");
                    else
                        wr = new WaveFileReader("base/sounds/general/sfx_objection.wav");

                    sfxPlayer.Initialize(wr);
                    if (!mute)
                        sfxPlayer.Play();

                    System.Threading.Thread.Sleep(1000);
                    break;

                case 3:

                    nameLabel.Visible = false;
                    displayMsg1.Visible = false;
                    displayMsg2.Visible = false;
                    displayMsg3.Visible = false;
                    objectLayerPB.Image = Image.FromFile("base/misc/ani_takethat.gif");
                    if (File.Exists("base/characters/" + latestMsg.strName + "/takethat.wav"))
                        wr = new WaveFileReader("base/characters/" + latestMsg.strName + "/takethat.wav");
                    else
                        wr = new WaveFileReader("base/sounds/general/sfx_objection.wav");

                    sfxPlayer.Initialize(wr);
                    if (!mute)
                        sfxPlayer.Play();

                    System.Threading.Thread.Sleep(1000);
                    break;

                case 4:
                    testimonyPB.Location = new Point(0, 3);
                    testimonyPB.Size = new Size(256, 111);
                    testimonyPB.Image = Image.FromFile("base/misc/ani_witnessTestimony2.gif");
                    wr = new WaveFileReader("base/sounds/general/sfx-testimony.wav");

                    sfxPlayer.Initialize(wr);
                    if (!mute)
                        sfxPlayer.Play();

                    System.Threading.Thread.Sleep(2800);
                    break;

                case 5:
                    testimonyPB.Location = new Point(0, 3);
                    testimonyPB.Size = new Size(256, 111);
                    testimonyPB.Image = Image.FromFile("base/misc/ani_crossexamination.gif");
                    System.Threading.Thread.Sleep(300);
                    wr = new WaveFileReader("base/sounds/general/sfx-testimony2.wav");

                    sfxPlayer.Initialize(wr);
                    if (!mute)
                        sfxPlayer.Play();

                    System.Threading.Thread.Sleep(1200);
                    sfxPlayer.Stop();
                    System.Threading.Thread.Sleep(300);

                    break;
            }

            nameLabel.Visible = true;
            displayMsg1.Visible = true;
            displayMsg2.Visible = true;
            displayMsg3.Visible = true;
            objectLayerPB.Image = null;
            testimonyPB.Image = null;
        }

        private void setCharSprite(string file)
        {
            if (File.Exists(file))
                charLayerPB.Image = Image.FromFile(file);
            else
                charLayerPB.Image = Image.FromFile("base/misc/placeholder_char.gif");
        }

        private void appendTxtLogSafe(string txt)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => txtLog.AppendText(txt)));
                return;
            }
            txtLog.AppendText(txt);
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
            nameLabel.Text = iniParser.GetDispName(latestMsg.strName) ?? latestMsg.strName;
            //blipPlayer.Initialize(blipReader);
            blipPlayer.Stop();
            if (!mute)
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
                            deskLayerPB.Image = Image.FromFile("base/background/default/defbench.png");
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
                            deskLayerPB.Image = Image.FromFile("base/background/default/probench.png");
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
                            deskLayerPB.Image = Image.FromFile("base/background/default/witstand.png");
                        }
                        else
                        {
                            backgroundPB.Image = Image.FromFile("base/misc/ani_zoom_pro.gif");
                            deskLayerPB.Image = null;
                        }
                        break;
                    case "hld":
                        if (!zoom)
                        {
                            backgroundPB.Image = Image.FromFile("base/background/default/helperstand.png");
                            deskLayerPB.Image = null;
                        }
                        else
                        {
                            backgroundPB.Image = Image.FromFile("base/misc/ani_zoom_def.gif");
                            deskLayerPB.Image = null;
                        }
                        break;
                    case "hlp":
                        if (!zoom)
                        {
                            backgroundPB.Image = Image.FromFile("base/background/default/prohelperstand.png");
                            deskLayerPB.Image = null;
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
                sendEnabled = false;
            else if (redraw == false)
                sendEnabled = true;
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter & sendEnabled)
            {
                e.SuppressKeyPress = true;
                if (redraw == false)
                {
                    try
                    {
                        //Fill the info for the message to be send
                        Data msgToSend = new Data();

                        msgToSend.strName = strName;
                        msgToSend.anim = selectedAnim;
                        msgToSend.textColor = selectedColor;
                        msgToSend.strMessage = txtMessage.Text;
                        msgToSend.callout = callout;
                        if (!presenting)
                            msgToSend.cmdCommand = Command.Message;
                        else
                        {
                            msgToSend.cmdCommand = Command.Present;
                            msgToSend.strMessage = msgToSend.strMessage + "|" + selectedEvidence;

                            readyToPresent = false;
                            presenting = false;
                            btn_back.Image = Image.FromFile("base/misc/btn_back_off.png");
                            btn_present.Image = Image.FromFile("base/misc/btn_present_off.png");

                            int nIndex = 0;
                            foreach (Control ctrl in courtRecordPB.Controls)
                            {
                                if (ctrl.Name == "infoBox" | ctrl.Name == "icon" | ctrl.Name == "eviName" | ctrl.Name == "eviDesc" | ctrl.Name == "eviNote")
                                    ctrl.Dispose();

                                if (ctrl is IndexButton)
                                {
                                    nIndex++;
                                    if (nIndex + (18 * eviPage) <= eviCount)
                                    {
                                        ctrl.Enabled = true;
                                        ctrl.Visible = true;
                                    }
                                }
                            }
                        }

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
            }
        }

        private void blipTimer_Tick(object sender, EventArgs e)
        {
            if (redraw == true)
            {
                //if (blipPlayer.PlaybackState == PlaybackState.Stopped)
                blipPlayer.Stop();
                if (!mute)
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
                    setCharSprite("base/characters/" + latestMsg.strName + "/(a)" + iniParser.GetAnim(latestMsg.strName, latestMsg.anim) + ".gif");
                    redraw = false;

                    if (txtMessage.Text.Length > 0)
                        sendEnabled = true;
                }
            }
        }

        private void animTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if ((soundTime > 4 && curSoundTime < (soundTime - 4)) | (soundTime > 0 & soundTime < 4 & curSoundTime < soundTime))
                    curSoundTime++;
                else if ((soundTime > 0 && curSoundTime == (soundTime - 4)) | (soundTime > 0 & soundTime < 4 & curSoundTime == soundTime))
                {
                    soundTime = 0; // This prevents the song from playing repeatedly every 60 ms
                    if (!mute)
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

                        if (latestMsg.cmdCommand == Command.Present)
                        {
                            sfxPlayer.Stop();
                            wr = new WaveFileReader("base/sounds/general/sfx-shooop.wav");
                            sfxPlayer.Initialize(wr);
                            if (!mute)
                                sfxPlayer.Play();

                            switch (iniParser.GetSide(latestMsg.strName))
                            {
                                case "def":
                                    testimonyPB.Image = Image.FromFile("base/misc/ani_evidenceRight.gif");
                                    System.Threading.Thread.Sleep(100);
                                    testimonyPB.Location = new Point(173, 13);
                                    testimonyPB.Size = new Size(70, 70);
                                    testimonyPB.Image = eviList[Convert.ToInt32(latestMsg.strMessage.Split('|').Last())].icon;
                                    break;
                                case "pro":
                                    testimonyPB.Image = Image.FromFile("base/misc/ani_evidenceLeft.gif");
                                    System.Threading.Thread.Sleep(100);
                                    testimonyPB.Location = new Point(13, 13);
                                    testimonyPB.Size = new Size(70, 70);
                                    testimonyPB.Image = eviList[Convert.ToInt32(latestMsg.strMessage.Split('|').Last())].icon;
                                    break;
                                case "hld":
                                    testimonyPB.Image = Image.FromFile("base/misc/ani_evidenceLeft.gif");
                                    System.Threading.Thread.Sleep(100);
                                    testimonyPB.Location = new Point(13, 13);
                                    testimonyPB.Size = new Size(70, 70);
                                    testimonyPB.Image = eviList[Convert.ToInt32(latestMsg.strMessage.Split('|').Last())].icon;
                                    break;
                                case "hlp":
                                    testimonyPB.Image = Image.FromFile("base/misc/ani_evidenceRight.gif");
                                    System.Threading.Thread.Sleep(100);
                                    testimonyPB.Location = new Point(173, 13);
                                    testimonyPB.Size = new Size(70, 70);
                                    testimonyPB.Image = eviList[Convert.ToInt32(latestMsg.strMessage.Split('|').Last())].icon;
                                    break;
                                default:
                                    testimonyPB.Image = Image.FromFile("base/misc/ani_evidenceRight.gif");
                                    System.Threading.Thread.Sleep(100);
                                    testimonyPB.Location = new Point(173, 13);
                                    testimonyPB.Size = new Size(70, 70);
                                    testimonyPB.Image = eviList[Convert.ToInt32(latestMsg.strMessage.Split('|').Last())].icon;
                                    break;
                            }

                            latestMsg.strMessage = latestMsg.strMessage.Split('|')[0];
                        }

                        setCharSprite("base/characters/" + latestMsg.strName + "/(b)" + iniParser.GetAnim(latestMsg.strName, latestMsg.anim) + ".gif");
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
                    //txtColorChanger.Text = "Text Color: White";
                    break;
                case 1:
                    txtMessage.ForeColor = Color.DeepSkyBlue;
                    selectedColor = Color.DeepSkyBlue;
                    //txtColorChanger.Text = "Text Color: Blue";
                    break;
                case 2:
                    txtMessage.ForeColor = Color.FromArgb(0, 255, 0);
                    selectedColor = Color.FromArgb(0, 255, 0);
                    //txtColorChanger.Text = "Text Color: Green";
                    break;
                case 3:
                    txtMessage.ForeColor = Color.OrangeRed;
                    selectedColor = Color.OrangeRed;
                    //txtColorChanger.Text = "Text Color: Orange";
                    break;
            }
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
                            if (elementName == "Client")
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
                string question = "New client version available: " + newVersion.ToString() + ".\r\n (You have version " + curVersion.ToString() + "). \r\n Download the new version?";
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

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm.Show();
        }

        private void musicList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (musicList.Items.Count > 0 && musicList.SelectedItem != null && (string)musicList.SelectedItem != "")
            {
                Data msgToSend = new Data();
                msgToSend.cmdCommand = Command.ChangeMusic;
                msgToSend.strName = strName;
                msgToSend.strMessage = (string)musicList.Items[musicList.SelectedIndex];
                byte[] msg = msgToSend.ToByte();

                clientSocket.BeginSend(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
                //byteData = new byte[1024];

                //clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
            }
        }

        private void btn_objection_Click(object sender, EventArgs e)
        {
            if (callout != 1)
            {
                btn_objection.Load("base/misc/btn_objection_on.png");
                btn_holdit.Load("base/misc/btn_holdit_off.png");
                btn_takethat.Load("base/misc/btn_takethat_off.png");
                callout = 1;
            }
            else
            {
                btn_objection.Load("base/misc/btn_objection_off.png");
                btn_holdit.Load("base/misc/btn_holdit_off.png");
                btn_takethat.Load("base/misc/btn_takethat_off.png");
                callout = 0;
            }
        }

        private void btn_holdit_Click(object sender, EventArgs e)
        {
            if (callout != 2)
            {
                btn_objection.Load("base/misc/btn_objection_off.png");
                btn_holdit.Load("base/misc/btn_holdit_on.png");
                btn_takethat.Load("base/misc/btn_takethat_off.png");
                callout = 2;
            }
            else
            {
                btn_objection.Load("base/misc/btn_objection_off.png");
                btn_holdit.Load("base/misc/btn_holdit_off.png");
                btn_takethat.Load("base/misc/btn_takethat_off.png");
                callout = 0;
            }
        }

        private void btn_takethat_Click(object sender, EventArgs e)
        {
            if (callout != 3)
            {
                btn_objection.Load("base/misc/btn_objection_off.png");
                btn_holdit.Load("base/misc/btn_holdit_off.png");
                btn_takethat.Load("base/misc/btn_takethat_on.png");
                callout = 3;
            }
            else
            {
                btn_objection.Load("base/misc/btn_objection_off.png");
                btn_holdit.Load("base/misc/btn_holdit_off.png");
                btn_takethat.Load("base/misc/btn_takethat_off.png");
                callout = 0;
            }
        }

        private void btn_Mute_Click(object sender, EventArgs e)
        {
            blipPlayer.Stop();
            sfxPlayer.Stop();
            musicPlayer.Stop();
            mute = !mute;
            if (mute)
                btn_Mute.Image = Image.FromFile("base/misc/btn_mute_pressed.png");
            else
                btn_Mute.Image = Image.FromFile("base/misc/btn_mute.png");
        }

        private void btn_Exclaim_Click(object sender, EventArgs e)
        {

        }

        private void btn_defminus_Click(object sender, EventArgs e)
        {
            if (btn_defminus.Visible == true)
            {
                if (defHealth > 0)
                {
                    Data msg = new Data();
                    msg.cmdCommand = Command.ChangeHealth;
                    msg.strName = "def";
                    msg.strMessage = "-1";
                    byte[] byteMsg = msg.ToByte();
                    clientSocket.BeginSend(byteMsg, 0, byteMsg.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
                }
            }
        }

        private void btn_defplus_Click(object sender, EventArgs e)
        {
            if (btn_defplus.Visible == true)
            {
                if (defHealth < 5)
                {
                    Data msg = new Data();
                    msg.cmdCommand = Command.ChangeHealth;
                    msg.strName = "def";
                    msg.strMessage = "+1";
                    byte[] byteMsg = msg.ToByte();
                    clientSocket.BeginSend(byteMsg, 0, byteMsg.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
                }
            }
        }

        private void btn_prominus_Click(object sender, EventArgs e)
        {
            if (btn_prominus.Visible == true)
            {
                if (proHealth > 0)
                {
                    Data msg = new Data();
                    msg.cmdCommand = Command.ChangeHealth;
                    msg.strName = "pro";
                    msg.strMessage = "-1";
                    byte[] byteMsg = msg.ToByte();
                    clientSocket.BeginSend(byteMsg, 0, byteMsg.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
                }
            }
        }

        private void btn_proplus_Click(object sender, EventArgs e)
        {
            if (btn_proplus.Visible == true)
            {
                if (proHealth < 5)
                {
                    Data msg = new Data();
                    msg.cmdCommand = Command.ChangeHealth;
                    msg.strName = "pro";
                    msg.strMessage = "+1";
                    byte[] byteMsg = msg.ToByte();
                    clientSocket.BeginSend(byteMsg, 0, byteMsg.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
                }
            }
        }

        private void btn_testimony_Click(object sender, EventArgs e)
        {
            if (btn_testimony.Visible == true)
            {
                Data calloutMsg = new Data();
                calloutMsg.callout = 4;
                calloutMsg.cmdCommand = Command.Message;

                byte[] msg = calloutMsg.ToByte();
                clientSocket.BeginSend(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
            }
        }

        private void btn_crossexamination_Click(object sender, EventArgs e)
        {
            if (btn_crossexamination.Visible == true)
            {
                Data calloutMsg = new Data();
                calloutMsg.callout = 5;
                calloutMsg.cmdCommand = Command.Message;

                byte[] msg = calloutMsg.ToByte();
                clientSocket.BeginSend(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
            }
        }

        private void emoButton_Click(object sender, EventArgs e)
        {
            if (sender is IndexButton)
            {
                IndexButton button = sender as IndexButton;
                if (button.Visible == true & button.Enabled == true)
                {
                    selectedAnim = button.Index;
                    loadEmoButtons(false);
                }
            }
        }

        private void eviButton_Click(object sender, EventArgs e)
        {
            if (sender is IndexButton)
            {
                IndexButton button = sender as IndexButton;
                if (button.Visible == true & button.Enabled == true)
                {
                    readyToPresent = true;
                    btn_back.Image = Image.FromFile("base/misc/btn_back.png");
                    btn_present.Image = Image.FromFile("base/misc/btn_present.png");
                    if (btn_edit.Visible)
                        btn_edit.Image = Image.FromFile("base/misc/btn_edit.png");
                    selectedEvidence = button.Index - 1;
                    showEvidenceInfo();
                }
            }
        }

        private void showEvidenceInfo()
        {
            foreach (Control ctrl in courtRecordPB.Controls)
            {
                if (ctrl is IndexButton)
                    ctrl.Visible = false;
            }
            PictureBox infoBox = new PictureBox();
            infoBox.Name = "infoBox";
            infoBox.Parent = courtRecordPB;
            infoBox.Location = new Point(30, 77);
            infoBox.Size = new Size(440, 222);
            infoBox.Image = Image.FromFile("base/misc/inventory_disp.png");

            PictureBox icon = new PictureBox();
            icon.Name = "icon";
            icon.Parent = infoBox;
            icon.Location = new Point(10, 16);
            icon.Size = new Size(70, 70);
            icon.Image = eviList[selectedEvidence].icon;

            Label eviName = new Label();
            eviName.Name = "eviName";
            eviName.Parent = infoBox;
            eviName.Location = new Point(90, 9);
            eviName.AutoSize = false;
            eviName.TextAlign = ContentAlignment.MiddleCenter;
            eviName.Size = new Size(338, 17);
            eviName.BackColor = Color.Transparent;
            eviName.ForeColor = Color.DarkOrange;
            eviName.Font = new Font(fonts.Families[0], 12.0f);
            eviName.Text = eviList[selectedEvidence].name;

            Label eviNote = new Label();
            eviNote.Name = "eviNote";
            eviNote.Parent = infoBox;
            eviNote.Location = new Point(95, 31);
            eviNote.AutoSize = false;
            eviNote.TextAlign = ContentAlignment.TopLeft;
            eviNote.Size = new Size(332, 62);
            eviNote.BackColor = Color.Transparent;
            eviNote.ForeColor = Color.Black;
            eviNote.Font = new Font(fonts.Families[0], 12.0f);
            if (eviList[selectedEvidence].note != "")
                eviNote.Text = "Type: Evidence\r\n" + eviList[selectedEvidence].note;
            else
                eviNote.Text = "Type: Evidence\r\nSubmitted by the judge."; // somewhat redundant

            Label eviDesc = new Label();
            eviDesc.Name = "eviDesc";
            eviDesc.Parent = infoBox;
            eviDesc.Location = new Point(5, 110);
            eviDesc.AutoSize = false;
            eviDesc.TextAlign = ContentAlignment.TopLeft;
            eviDesc.Size = new Size(434, 112);
            eviDesc.BackColor = Color.Transparent;
            eviDesc.ForeColor = Color.White;
            eviDesc.Font = new Font(fonts.Families[0], 12.0f);
            eviDesc.Text = eviList[selectedEvidence].desc;


            //evidencePanel.Controls.Add(infoBox);
        }

        private void btn_present_Click(object sender, EventArgs e)
        {
            if (readyToPresent)
            {
                btn_present.Image = Image.FromFile("base/misc/btn_present_off.png");
                presenting = true;
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            readyToPresent = false;
            presenting = false;
            btn_back.Image = Image.FromFile("base/misc/btn_back_off.png");
            btn_present.Image = Image.FromFile("base/misc/btn_present_off.png");
            if (btn_edit.Visible)
                btn_edit.Image = Image.FromFile("base/misc/btn_edit_off.png");

            int nIndex = 0;
            foreach (Control ctrl in courtRecordPB.Controls)
            {
                if (ctrl.Name == "infoBox" | ctrl.Name == "icon" | ctrl.Name == "eviName" | ctrl.Name == "eviDesc" | ctrl.Name == "eviNote")
                    ctrl.Dispose();

                if (ctrl is IndexButton)
                {
                    nIndex++;
                    if (nIndex + (18 * eviPage) <= eviCount)
                    {
                        ctrl.Enabled = true;
                        ctrl.Visible = true;
                    }
                }
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (readyToPresent && btn_edit.Visible & btn_edit.Enabled)
            {
                EvidenceEditor editor = new EvidenceEditor();
                if (editor.ShowDialog() == DialogResult.OK)
                {

                }
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
            anim = 1;
            callout = 0;
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

            callout = data[12];

            int textColorLen = BitConverter.ToInt32(data, 13);

            //The next four store the length of the message
            int msgLen = BitConverter.ToInt32(data, 17);

            //This check makes sure that strName has been passed in the array of bytes
            if (nameLen > 0)
                strName = Encoding.UTF8.GetString(data, 21, nameLen);
            else
                strName = null;

            if (textColorLen > 0)
                textColor = Color.FromArgb(BitConverter.ToInt32(data, 21 + nameLen));
            else
                textColor = Color.White;

            //This checks for a null message field
            if (msgLen > 0)
                strMessage = Encoding.UTF8.GetString(data, 21 + nameLen + textColorLen, msgLen);
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

            result.Add(callout);

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
        public byte callout;
        public byte[] extraData;
        public Color textColor;
        public string strMessage;   //Message text
        public Command cmdCommand;  //Command type (login, logout, send message, etcetera)
    }

    //The data structure by which the server and the client interact with 
    //each other
    class EviData
    {
        //Default constructor
        public EviData(string name, string desc, string note)
        {
            cmdCommand = Command.Evidence;
            strName = name;
            strNote = note;
            strDesc = desc;
        }

        //Converts the bytes into an object of type Data
        public EviData(byte[] data)
        {
            //The first four bytes are for the Command
            cmdCommand = (Command)BitConverter.ToInt32(data, 0);

            //The next four store the length of the name
            int nameLen = BitConverter.ToInt32(data, 4);

            int descLen = BitConverter.ToInt32(data, 8);

            int noteLen = BitConverter.ToInt32(data, 12);

            dataSize = BitConverter.ToInt32(data, 16);

            //This check makes sure that strName has been passed in the array of bytes
            if (nameLen > 0)
                strName = Encoding.UTF8.GetString(data, 20, nameLen);
            else
                strName = null;

            if (descLen > 0)
                strDesc = Encoding.UTF8.GetString(data, 20 + nameLen, descLen);
            else
                strDesc = null;

            if (noteLen > 0)
                strNote = Encoding.UTF8.GetString(data, 20 + nameLen + descLen, noteLen);
            else
                strNote = null;

            if (dataSize > 0)
                dataBytes = data.Skip(20 + nameLen + descLen + noteLen).ToArray();
            else
                dataBytes = null;
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

            if (strDesc != null)
                result.AddRange(BitConverter.GetBytes(strDesc.Length));

            if (strNote != null)
                result.AddRange(BitConverter.GetBytes(strNote.Length));
            else
                result.AddRange(BitConverter.GetBytes("Submitted by the judge.".Length));

            if (dataBytes != null)
                result.AddRange(BitConverter.GetBytes(dataBytes.Length));

            //Add the name
            if (strName != null)
                result.AddRange(Encoding.UTF8.GetBytes(strName));

            if (strDesc != null)
                result.AddRange(Encoding.UTF8.GetBytes(strDesc));

            result.AddRange(Encoding.UTF8.GetBytes(strNote ?? "Submitted by the judge."));

            result.AddRange(dataBytes);

            return result.ToArray();
        }

        public string strName;      //Name and path of the file being sent/received
        public string strDesc;
        public string strNote;
        public int dataSize;
        public byte[] dataBytes;
        public Command cmdCommand;  //Command type (login, logout, send message, etcetera)
    }

    public class Evidence
    {
        public string name;
        public string desc;
        public string note;
        public Image icon;
    }
}