namespace Client
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.btn_PublicServers = new System.Windows.Forms.PictureBox();
            this.background = new System.Windows.Forms.PictureBox();
            this.btn_FavoriteServers = new System.Windows.Forms.PictureBox();
            this.serverList = new System.Windows.Forms.ListBox();
            this.btn_Refresh = new System.Windows.Forms.PictureBox();
            this.btn_AddFav = new System.Windows.Forms.PictureBox();
            this.btn_Connect = new System.Windows.Forms.PictureBox();
            this.lobbyChat = new System.Windows.Forms.TextBox();
            this.lobbyName = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.userCount = new System.Windows.Forms.Label();
            this.serverDescTextBox = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.btn_PublicServers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.background)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_FavoriteServers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Refresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_AddFav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Connect)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_PublicServers
            // 
            this.btn_PublicServers.BackColor = System.Drawing.Color.Transparent;
            this.btn_PublicServers.Image = ((System.Drawing.Image)(resources.GetObject("btn_PublicServers.Image")));
            this.btn_PublicServers.Location = new System.Drawing.Point(45, 111);
            this.btn_PublicServers.Name = "btn_PublicServers";
            this.btn_PublicServers.Size = new System.Drawing.Size(116, 32);
            this.btn_PublicServers.TabIndex = 2;
            this.btn_PublicServers.TabStop = false;
            this.btn_PublicServers.Click += new System.EventHandler(this.btn_PublicServers_Click);
            // 
            // background
            // 
            this.background.Image = ((System.Drawing.Image)(resources.GetObject("background.Image")));
            this.background.Location = new System.Drawing.Point(0, 0);
            this.background.Name = "background";
            this.background.Size = new System.Drawing.Size(520, 720);
            this.background.TabIndex = 0;
            this.background.TabStop = false;
            // 
            // btn_FavoriteServers
            // 
            this.btn_FavoriteServers.BackColor = System.Drawing.Color.Transparent;
            this.btn_FavoriteServers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_FavoriteServers.Image = ((System.Drawing.Image)(resources.GetObject("btn_FavoriteServers.Image")));
            this.btn_FavoriteServers.Location = new System.Drawing.Point(164, 111);
            this.btn_FavoriteServers.Name = "btn_FavoriteServers";
            this.btn_FavoriteServers.Size = new System.Drawing.Size(116, 32);
            this.btn_FavoriteServers.TabIndex = 3;
            this.btn_FavoriteServers.TabStop = false;
            this.btn_FavoriteServers.Click += new System.EventHandler(this.btn_FavoriteServers_Click);
            // 
            // serverList
            // 
            this.serverList.BackColor = System.Drawing.Color.Gray;
            this.serverList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverList.FormattingEnabled = true;
            this.serverList.IntegralHeight = false;
            this.serverList.ItemHeight = 16;
            this.serverList.Location = new System.Drawing.Point(16, 149);
            this.serverList.Name = "serverList";
            this.serverList.Size = new System.Drawing.Size(295, 236);
            this.serverList.TabIndex = 4;
            this.serverList.SelectedIndexChanged += new System.EventHandler(this.serverList_SelectedIndexChanged);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackColor = System.Drawing.Color.Transparent;
            this.btn_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("btn_Refresh.Image")));
            this.btn_Refresh.Location = new System.Drawing.Point(56, 405);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(132, 28);
            this.btn_Refresh.TabIndex = 5;
            this.btn_Refresh.TabStop = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // btn_AddFav
            // 
            this.btn_AddFav.BackColor = System.Drawing.Color.Transparent;
            this.btn_AddFav.Image = ((System.Drawing.Image)(resources.GetObject("btn_AddFav.Image")));
            this.btn_AddFav.Location = new System.Drawing.Point(194, 405);
            this.btn_AddFav.Name = "btn_AddFav";
            this.btn_AddFav.Size = new System.Drawing.Size(132, 28);
            this.btn_AddFav.TabIndex = 6;
            this.btn_AddFav.TabStop = false;
            this.btn_AddFav.Click += new System.EventHandler(this.btn_AddFav_Click);
            // 
            // btn_Connect
            // 
            this.btn_Connect.BackColor = System.Drawing.Color.Transparent;
            this.btn_Connect.Image = ((System.Drawing.Image)(resources.GetObject("btn_Connect.Image")));
            this.btn_Connect.Location = new System.Drawing.Point(332, 405);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(132, 28);
            this.btn_Connect.TabIndex = 7;
            this.btn_Connect.TabStop = false;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // lobbyChat
            // 
            this.lobbyChat.BackColor = System.Drawing.Color.Gray;
            this.lobbyChat.Cursor = System.Windows.Forms.Cursors.Default;
            this.lobbyChat.Location = new System.Drawing.Point(0, 467);
            this.lobbyChat.Multiline = true;
            this.lobbyChat.Name = "lobbyChat";
            this.lobbyChat.ReadOnly = true;
            this.lobbyChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lobbyChat.Size = new System.Drawing.Size(520, 204);
            this.lobbyChat.TabIndex = 9;
            // 
            // lobbyName
            // 
            this.lobbyName.BackColor = System.Drawing.Color.DimGray;
            this.lobbyName.Location = new System.Drawing.Point(0, 671);
            this.lobbyName.Name = "lobbyName";
            this.lobbyName.Size = new System.Drawing.Size(100, 20);
            this.lobbyName.TabIndex = 10;
            this.lobbyName.Text = "Name";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.DimGray;
            this.textBox2.Location = new System.Drawing.Point(100, 671);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(420, 20);
            this.textBox2.TabIndex = 11;
            // 
            // userCount
            // 
            this.userCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.userCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userCount.ForeColor = System.Drawing.Color.White;
            this.userCount.Location = new System.Drawing.Point(343, 119);
            this.userCount.Name = "userCount";
            this.userCount.Size = new System.Drawing.Size(157, 16);
            this.userCount.TabIndex = 13;
            this.userCount.Text = "Offline";
            this.userCount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // serverDescTextBox
            // 
            this.serverDescTextBox.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.serverDescTextBox.Location = new System.Drawing.Point(340, 140);
            this.serverDescTextBox.Name = "serverDescTextBox";
            this.serverDescTextBox.Size = new System.Drawing.Size(164, 244);
            this.serverDescTextBox.TabIndex = 14;
            this.serverDescTextBox.Text = "serverDescTextBox";
            // 
            // versionLabel
            // 
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(63, 17);
            this.versionLabel.Text = "Version 1.0";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 696);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(520, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.Gainsboro;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(520, 24);
            this.menuStrip.TabIndex = 15;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "&Help";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.aboutMenuItem.Size = new System.Drawing.Size(157, 22);
            this.aboutMenuItem.Text = "&About...";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitMenuItem.Text = "&Exit";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 718);
            this.Controls.Add(this.serverDescTextBox);
            this.Controls.Add(this.userCount);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.lobbyName);
            this.Controls.Add(this.lobbyChat);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.btn_AddFav);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.serverList);
            this.Controls.Add(this.btn_FavoriteServers);
            this.Controls.Add(this.btn_PublicServers);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.background);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attorney Online Deluxe Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_PublicServers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.background)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_FavoriteServers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Refresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_AddFav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Connect)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox background;
        private System.Windows.Forms.PictureBox btn_PublicServers;
        private System.Windows.Forms.PictureBox btn_FavoriteServers;
        private System.Windows.Forms.ListBox serverList;
        private System.Windows.Forms.PictureBox btn_Refresh;
        private System.Windows.Forms.PictureBox btn_AddFav;
        private System.Windows.Forms.PictureBox btn_Connect;
        private System.Windows.Forms.TextBox lobbyChat;
        private System.Windows.Forms.TextBox lobbyName;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label userCount;
        private System.Windows.Forms.Label serverDescTextBox;
        private System.Windows.Forms.ToolStripStatusLabel versionLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
    }
}