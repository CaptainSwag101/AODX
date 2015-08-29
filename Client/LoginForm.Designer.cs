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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
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
            this.versionLabel = new System.Windows.Forms.Label();
            this.userCount = new System.Windows.Forms.Label();
            this.serverDescTextBox = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btn_PublicServers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.background)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_FavoriteServers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Refresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_AddFav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Connect)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 696);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(520, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
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
            this.background.Image = global::Client.Properties.Resources.lobby2;
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
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.versionLabel.Location = new System.Drawing.Point(6, 6);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(60, 13);
            this.versionLabel.TabIndex = 12;
            this.versionLabel.Text = "Version 1.0";
            // 
            // userCount
            // 
            this.userCount.AutoSize = true;
            this.userCount.BackColor = System.Drawing.Color.DimGray;
            this.userCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userCount.ForeColor = System.Drawing.Color.White;
            this.userCount.Location = new System.Drawing.Point(396, 119);
            this.userCount.Name = "userCount";
            this.userCount.Size = new System.Drawing.Size(52, 16);
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
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 718);
            this.Controls.Add(this.serverDescTextBox);
            this.Controls.Add(this.userCount);
            this.Controls.Add(this.versionLabel);
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
            this.Controls.Add(this.background);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attorney Online Deluxe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_PublicServers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.background)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_FavoriteServers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Refresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_AddFav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Connect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox background;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.PictureBox btn_PublicServers;
        private System.Windows.Forms.PictureBox btn_FavoriteServers;
        private System.Windows.Forms.ListBox serverList;
        private System.Windows.Forms.PictureBox btn_Refresh;
        private System.Windows.Forms.PictureBox btn_AddFav;
        private System.Windows.Forms.PictureBox btn_Connect;
        private System.Windows.Forms.TextBox lobbyChat;
        private System.Windows.Forms.TextBox lobbyName;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label userCount;
        private System.Windows.Forms.Label serverDescTextBox;
    }
}