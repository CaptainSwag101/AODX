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
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.GameDisplay = new System.Windows.Forms.Panel();
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
            this.GameDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectLayerPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chatBGLayerPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deskLayerPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.charLayerPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPB)).BeginInit();
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
            resources.ApplyResources(this.txtLog, "txtLog");
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ShortcutsEnabled = false;
            // 
            // txtMessage
            // 
            resources.ApplyResources(this.txtMessage, "txtMessage");
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.TextChanged += new System.EventHandler(this.txtMessage_TextChanged);
            this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessage_KeyDown);
            // 
            // lstUsers
            // 
            resources.ApplyResources(this.lstUsers, "lstUsers");
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.SelectionMode = System.Windows.Forms.SelectionMode.None;
            // 
            // GameDisplay
            // 
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
            this.chatBGLayerPB.Image = global::Client.Properties.Resources.PW_Textbox_Trans;
            resources.ApplyResources(this.chatBGLayerPB, "chatBGLayerPB");
            this.chatBGLayerPB.Name = "chatBGLayerPB";
            this.chatBGLayerPB.TabStop = false;
            // 
            // deskLayerPB
            // 
            this.deskLayerPB.BackColor = System.Drawing.Color.Transparent;
            this.deskLayerPB.Image = global::Client.Properties.Resources.Defense_Bench_Overlay_resized;
            resources.ApplyResources(this.deskLayerPB, "deskLayerPB");
            this.deskLayerPB.Name = "deskLayerPB";
            this.deskLayerPB.TabStop = false;
            // 
            // charLayerPB
            // 
            this.charLayerPB.BackColor = System.Drawing.Color.Transparent;
            this.charLayerPB.Image = global::Client.Properties.Resources.phoenix_normal_a_;
            resources.ApplyResources(this.charLayerPB, "charLayerPB");
            this.charLayerPB.Name = "charLayerPB";
            this.charLayerPB.TabStop = false;
            // 
            // backgroundPB
            // 
            this.backgroundPB.BackColor = System.Drawing.Color.Transparent;
            this.backgroundPB.Image = global::Client.Properties.Resources.defenseempty;
            resources.ApplyResources(this.backgroundPB, "backgroundPB");
            this.backgroundPB.Name = "backgroundPB";
            this.backgroundPB.TabStop = false;
            // 
            // txtColorChanger
            // 
            resources.ApplyResources(this.txtColorChanger, "txtColorChanger");
            this.txtColorChanger.Name = "txtColorChanger";
            this.txtColorChanger.UseVisualStyleBackColor = true;
            // 
            // dispTextRedraw
            // 
            this.dispTextRedraw.Enabled = true;
            this.dispTextRedraw.Interval = 60;
            this.dispTextRedraw.Tick += new System.EventHandler(this.dispTextRedraw_Tick);
            // 
            // ClientForm
            // 
            this.AcceptButton = this.btnSend;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtColorChanger);
            this.Controls.Add(this.GameDisplay);
            this.Controls.Add(this.lstUsers);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnSend);
            this.MaximizeBox = false;
            this.Name = "ClientForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AODXClient_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GameDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectLayerPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chatBGLayerPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deskLayerPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.charLayerPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ListBox lstUsers;
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
    }
}

