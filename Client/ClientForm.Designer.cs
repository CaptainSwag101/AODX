namespace Client
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            this.btnSend = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.GameDisplay = new System.Windows.Forms.Panel();
            this.nameLabel = new System.Windows.Forms.Label();
            this.displayMsg3 = new System.Windows.Forms.Label();
            this.displayMsg2 = new System.Windows.Forms.Label();
            this.displayMsg1 = new System.Windows.Forms.Label();
            this.objectLayerPB = new System.Windows.Forms.PictureBox();
            this.chatBGLayerPB = new System.Windows.Forms.PictureBox();
            this.deskLayerPB = new System.Windows.Forms.PictureBox();
            this.charLayerPB = new System.Windows.Forms.PictureBox();
            this.backgroundPB = new System.Windows.Forms.PictureBox();
            this.txtColorChanger = new System.Windows.Forms.Button();
            this.dispTextRedraw = new System.Windows.Forms.Timer(this.components);
            this.OOCChat = new System.Windows.Forms.TextBox();
            this.OOCInput = new System.Windows.Forms.TextBox();
            this.emotionPanel = new System.Windows.Forms.Panel();
            this.arrowRight = new System.Windows.Forms.PictureBox();
            this.arrowLeft = new System.Windows.Forms.PictureBox();
            this.emoButton10 = new Client.EmoButton();
            this.emoButton9 = new Client.EmoButton();
            this.emoButton8 = new Client.EmoButton();
            this.emoButton7 = new Client.EmoButton();
            this.emoButton6 = new Client.EmoButton();
            this.emoButton5 = new Client.EmoButton();
            this.emoButton4 = new Client.EmoButton();
            this.emoButton3 = new Client.EmoButton();
            this.emoButton2 = new Client.EmoButton();
            this.emoButton1 = new Client.EmoButton();
            this.animTimer = new System.Windows.Forms.Timer(this.components);
            this.GameDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectLayerPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chatBGLayerPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deskLayerPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.charLayerPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPB)).BeginInit();
            this.emotionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.arrowRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arrowLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            resources.ApplyResources(this.btnSend, "btnSend");
            this.btnSend.Name = "btnSend";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtLog, "txtLog");
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ShortcutsEnabled = false;
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.txtMessage, "txtMessage");
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.TextChanged += new System.EventHandler(this.txtMessage_TextChanged);
            this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessage_KeyDown);
            // 
            // GameDisplay
            // 
            this.GameDisplay.Controls.Add(this.nameLabel);
            this.GameDisplay.Controls.Add(this.displayMsg3);
            this.GameDisplay.Controls.Add(this.displayMsg2);
            this.GameDisplay.Controls.Add(this.displayMsg1);
            this.GameDisplay.Controls.Add(this.objectLayerPB);
            this.GameDisplay.Controls.Add(this.chatBGLayerPB);
            this.GameDisplay.Controls.Add(this.deskLayerPB);
            this.GameDisplay.Controls.Add(this.charLayerPB);
            this.GameDisplay.Controls.Add(this.backgroundPB);
            resources.ApplyResources(this.GameDisplay, "GameDisplay");
            this.GameDisplay.Name = "GameDisplay";
            // 
            // nameLabel
            // 
            this.nameLabel.BackColor = System.Drawing.Color.RoyalBlue;
            resources.ApplyResources(this.nameLabel, "nameLabel");
            this.nameLabel.ForeColor = System.Drawing.Color.White;
            this.nameLabel.Name = "nameLabel";
            // 
            // displayMsg3
            // 
            this.displayMsg3.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.displayMsg3, "displayMsg3");
            this.displayMsg3.ForeColor = System.Drawing.Color.White;
            this.displayMsg3.Name = "displayMsg3";
            // 
            // displayMsg2
            // 
            this.displayMsg2.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.displayMsg2, "displayMsg2");
            this.displayMsg2.ForeColor = System.Drawing.Color.White;
            this.displayMsg2.Name = "displayMsg2";
            // 
            // displayMsg1
            // 
            this.displayMsg1.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.displayMsg1, "displayMsg1");
            this.displayMsg1.ForeColor = System.Drawing.Color.White;
            this.displayMsg1.Name = "displayMsg1";
            // 
            // objectLayerPB
            // 
            this.objectLayerPB.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.objectLayerPB, "objectLayerPB");
            this.objectLayerPB.Name = "objectLayerPB";
            this.objectLayerPB.TabStop = false;
            // 
            // chatBGLayerPB
            // 
            this.chatBGLayerPB.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.chatBGLayerPB, "chatBGLayerPB");
            this.chatBGLayerPB.Name = "chatBGLayerPB";
            this.chatBGLayerPB.TabStop = false;
            // 
            // deskLayerPB
            // 
            this.deskLayerPB.BackColor = System.Drawing.Color.Transparent;
            this.deskLayerPB.Image = null;
            resources.ApplyResources(this.deskLayerPB, "deskLayerPB");
            this.deskLayerPB.Name = "deskLayerPB";
            this.deskLayerPB.TabStop = false;
            // 
            // charLayerPB
            // 
            this.charLayerPB.BackColor = System.Drawing.Color.Transparent;
            this.charLayerPB.Image = null;
            resources.ApplyResources(this.charLayerPB, "charLayerPB");
            this.charLayerPB.Name = "charLayerPB";
            this.charLayerPB.TabStop = false;
            // 
            // backgroundPB
            // 
            this.backgroundPB.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.backgroundPB, "backgroundPB");
            this.backgroundPB.Name = "backgroundPB";
            this.backgroundPB.TabStop = false;
            // 
            // txtColorChanger
            // 
            resources.ApplyResources(this.txtColorChanger, "txtColorChanger");
            this.txtColorChanger.Name = "txtColorChanger";
            this.txtColorChanger.UseVisualStyleBackColor = true;
            this.txtColorChanger.Click += new System.EventHandler(this.txtColorChanger_Click);
            // 
            // dispTextRedraw
            // 
            this.dispTextRedraw.Enabled = true;
            this.dispTextRedraw.Interval = 60;
            this.dispTextRedraw.Tick += new System.EventHandler(this.dispTextRedraw_Tick);
            // 
            // OOCChat
            // 
            resources.ApplyResources(this.OOCChat, "OOCChat");
            this.OOCChat.BackColor = System.Drawing.SystemColors.Window;
            this.OOCChat.Name = "OOCChat";
            this.OOCChat.ReadOnly = true;
            this.OOCChat.ShortcutsEnabled = false;
            // 
            // OOCInput
            // 
            resources.ApplyResources(this.OOCInput, "OOCInput");
            this.OOCInput.Name = "OOCInput";
            // 
            // emotionPanel
            // 
            this.emotionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.emotionPanel.Controls.Add(this.arrowRight);
            this.emotionPanel.Controls.Add(this.arrowLeft);
            this.emotionPanel.Controls.Add(this.emoButton10);
            this.emotionPanel.Controls.Add(this.emoButton9);
            this.emotionPanel.Controls.Add(this.emoButton8);
            this.emotionPanel.Controls.Add(this.emoButton7);
            this.emotionPanel.Controls.Add(this.emoButton6);
            this.emotionPanel.Controls.Add(this.emoButton5);
            this.emotionPanel.Controls.Add(this.emoButton4);
            this.emotionPanel.Controls.Add(this.emoButton3);
            this.emotionPanel.Controls.Add(this.emoButton2);
            this.emotionPanel.Controls.Add(this.emoButton1);
            resources.ApplyResources(this.emotionPanel, "emotionPanel");
            this.emotionPanel.Name = "emotionPanel";
            // 
            // arrowRight
            // 
            resources.ApplyResources(this.arrowRight, "arrowRight");
            this.arrowRight.Name = "arrowRight";
            this.arrowRight.TabStop = false;
            this.arrowRight.Click += new System.EventHandler(this.arrowRight_Click);
            // 
            // arrowLeft
            // 
            resources.ApplyResources(this.arrowLeft, "arrowLeft");
            this.arrowLeft.Name = "arrowLeft";
            this.arrowLeft.TabStop = false;
            this.arrowLeft.Click += new System.EventHandler(this.arrowLeft_Click);
            // 
            // emoButton10
            // 
            resources.ApplyResources(this.emoButton10, "emoButton10");
            this.emoButton10.Name = "emoButton10";
            this.emoButton10.TabStop = false;
            this.emoButton10.Click += new System.EventHandler(this.emoButton10_Click);
            // 
            // emoButton9
            // 
            resources.ApplyResources(this.emoButton9, "emoButton9");
            this.emoButton9.Name = "emoButton9";
            this.emoButton9.TabStop = false;
            this.emoButton9.Click += new System.EventHandler(this.emoButton9_Click);
            // 
            // emoButton8
            // 
            resources.ApplyResources(this.emoButton8, "emoButton8");
            this.emoButton8.Name = "emoButton8";
            this.emoButton8.TabStop = false;
            this.emoButton8.Click += new System.EventHandler(this.emoButton8_Click);
            // 
            // emoButton7
            // 
            resources.ApplyResources(this.emoButton7, "emoButton7");
            this.emoButton7.Name = "emoButton7";
            this.emoButton7.TabStop = false;
            this.emoButton7.Click += new System.EventHandler(this.emoButton7_Click);
            // 
            // emoButton6
            // 
            resources.ApplyResources(this.emoButton6, "emoButton6");
            this.emoButton6.Name = "emoButton6";
            this.emoButton6.TabStop = false;
            this.emoButton6.Click += new System.EventHandler(this.emoButton6_Click);
            // 
            // emoButton5
            // 
            resources.ApplyResources(this.emoButton5, "emoButton5");
            this.emoButton5.Name = "emoButton5";
            this.emoButton5.TabStop = false;
            this.emoButton5.Click += new System.EventHandler(this.emoButton5_Click);
            // 
            // emoButton4
            // 
            resources.ApplyResources(this.emoButton4, "emoButton4");
            this.emoButton4.Name = "emoButton4";
            this.emoButton4.TabStop = false;
            this.emoButton4.Click += new System.EventHandler(this.emoButton4_Click);
            // 
            // emoButton3
            // 
            resources.ApplyResources(this.emoButton3, "emoButton3");
            this.emoButton3.Name = "emoButton3";
            this.emoButton3.TabStop = false;
            this.emoButton3.Click += new System.EventHandler(this.emoButton3_Click);
            // 
            // emoButton2
            // 
            resources.ApplyResources(this.emoButton2, "emoButton2");
            this.emoButton2.Name = "emoButton2";
            this.emoButton2.TabStop = false;
            this.emoButton2.Click += new System.EventHandler(this.emoButton2_Click);
            // 
            // emoButton1
            // 
            resources.ApplyResources(this.emoButton1, "emoButton1");
            this.emoButton1.Name = "emoButton1";
            this.emoButton1.TabStop = false;
            this.emoButton1.Click += new System.EventHandler(this.emoButton1_Click);
            // 
            // animTimer
            // 
            this.animTimer.Enabled = true;
            this.animTimer.Interval = 60;
            this.animTimer.Tick += new System.EventHandler(this.animTimer_Tick);
            // 
            // ClientForm
            // 
            this.AcceptButton = this.btnSend;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.emotionPanel);
            this.Controls.Add(this.OOCInput);
            this.Controls.Add(this.OOCChat);
            this.Controls.Add(this.txtColorChanger);
            this.Controls.Add(this.GameDisplay);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnSend);
            this.MaximizeBox = false;
            this.Name = "ClientForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientForm_FormClosing);
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.GameDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectLayerPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chatBGLayerPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deskLayerPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.charLayerPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPB)).EndInit();
            this.emotionPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.arrowRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arrowLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoButton1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Panel GameDisplay;
        private System.Windows.Forms.Button txtColorChanger;
        private System.Windows.Forms.PictureBox backgroundPB;
        private System.Windows.Forms.PictureBox chatBGLayerPB;
        private System.Windows.Forms.PictureBox deskLayerPB;
        private System.Windows.Forms.PictureBox charLayerPB;
        private System.Windows.Forms.PictureBox objectLayerPB;
        private System.Windows.Forms.Label displayMsg1;
        private System.Windows.Forms.Label displayMsg2;
        private System.Windows.Forms.Label displayMsg3;
        private System.Windows.Forms.Timer dispTextRedraw;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox OOCChat;
        private System.Windows.Forms.TextBox OOCInput;
        private System.Windows.Forms.Panel emotionPanel;
        private EmoButton emoButton1;
        private EmoButton emoButton5;
        private EmoButton emoButton4;
        private EmoButton emoButton3;
        private EmoButton emoButton2;
        private EmoButton emoButton10;
        private EmoButton emoButton9;
        private EmoButton emoButton8;
        private EmoButton emoButton7;
        private EmoButton emoButton6;
        private System.Windows.Forms.PictureBox arrowLeft;
        private System.Windows.Forms.PictureBox arrowRight;
        private System.Windows.Forms.Timer animTimer;
    }
}

