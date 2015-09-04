namespace Server
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.gbServer = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbMusicLoop = new System.Windows.Forms.CheckBox();
            this.gbMusic = new System.Windows.Forms.GroupBox();
            this.rbPrivateMusic = new System.Windows.Forms.RadioButton();
            this.rbProtectedMusic = new System.Windows.Forms.RadioButton();
            this.rbParticipantMusic = new System.Windows.Forms.RadioButton();
            this.rbPublicMusic = new System.Windows.Forms.RadioButton();
            this.btnSelectCase = new System.Windows.Forms.Button();
            this.cbUseCase = new System.Windows.Forms.CheckBox();
            this.tbDesc = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.gbNet = new System.Windows.Forms.GroupBox();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbOpPass = new System.Windows.Forms.TextBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.cbServerPublic = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbServer.SuspendLayout();
            this.gbMusic.SuspendLayout();
            this.gbNet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.SuspendLayout();
            // 
            // gbServer
            // 
            this.gbServer.Controls.Add(this.label2);
            this.gbServer.Controls.Add(this.label1);
            this.gbServer.Controls.Add(this.cbMusicLoop);
            this.gbServer.Controls.Add(this.gbMusic);
            this.gbServer.Controls.Add(this.btnSelectCase);
            this.gbServer.Controls.Add(this.cbUseCase);
            this.gbServer.Controls.Add(this.tbDesc);
            this.gbServer.Controls.Add(this.tbName);
            this.gbServer.Location = new System.Drawing.Point(12, 12);
            this.gbServer.Name = "gbServer";
            this.gbServer.Size = new System.Drawing.Size(363, 345);
            this.gbServer.TabIndex = 0;
            this.gbServer.TabStop = false;
            this.gbServer.Text = "Server Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Server description:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Server name:";
            // 
            // cbMusicLoop
            // 
            this.cbMusicLoop.AutoSize = true;
            this.cbMusicLoop.Checked = true;
            this.cbMusicLoop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMusicLoop.Location = new System.Drawing.Point(6, 207);
            this.cbMusicLoop.Name = "cbMusicLoop";
            this.cbMusicLoop.Size = new System.Drawing.Size(81, 17);
            this.cbMusicLoop.TabIndex = 5;
            this.cbMusicLoop.Text = "Loop Music";
            this.cbMusicLoop.UseVisualStyleBackColor = true;
            this.cbMusicLoop.CheckedChanged += new System.EventHandler(this.cbMusicLoop_CheckedChanged);
            // 
            // gbMusic
            // 
            this.gbMusic.Controls.Add(this.rbPrivateMusic);
            this.gbMusic.Controls.Add(this.rbProtectedMusic);
            this.gbMusic.Controls.Add(this.rbParticipantMusic);
            this.gbMusic.Controls.Add(this.rbPublicMusic);
            this.gbMusic.Location = new System.Drawing.Point(6, 230);
            this.gbMusic.Name = "gbMusic";
            this.gbMusic.Size = new System.Drawing.Size(351, 111);
            this.gbMusic.TabIndex = 4;
            this.gbMusic.TabStop = false;
            this.gbMusic.Text = "Music Mode: Who can change the music";
            // 
            // rbPrivateMusic
            // 
            this.rbPrivateMusic.AutoSize = true;
            this.rbPrivateMusic.Location = new System.Drawing.Point(6, 88);
            this.rbPrivateMusic.Name = "rbPrivateMusic";
            this.rbPrivateMusic.Size = new System.Drawing.Size(115, 17);
            this.rbPrivateMusic.TabIndex = 3;
            this.rbPrivateMusic.Text = "Server Admins only";
            this.rbPrivateMusic.UseVisualStyleBackColor = true;
            this.rbPrivateMusic.CheckedChanged += new System.EventHandler(this.rbPrivateMusic_CheckedChanged);
            // 
            // rbProtectedMusic
            // 
            this.rbProtectedMusic.AutoSize = true;
            this.rbProtectedMusic.Location = new System.Drawing.Point(6, 65);
            this.rbProtectedMusic.Name = "rbProtectedMusic";
            this.rbProtectedMusic.Size = new System.Drawing.Size(146, 17);
            this.rbProtectedMusic.TabIndex = 2;
            this.rbProtectedMusic.Text = "Server Admins and Judge";
            this.rbProtectedMusic.UseVisualStyleBackColor = true;
            this.rbProtectedMusic.CheckedChanged += new System.EventHandler(this.rbProtectedMusic_CheckedChanged);
            // 
            // rbParticipantMusic
            // 
            this.rbParticipantMusic.AutoSize = true;
            this.rbParticipantMusic.Location = new System.Drawing.Point(6, 42);
            this.rbParticipantMusic.Name = "rbParticipantMusic";
            this.rbParticipantMusic.Size = new System.Drawing.Size(237, 17);
            this.rbParticipantMusic.TabIndex = 1;
            this.rbParticipantMusic.Text = "Server Admins, Judge, and Case Participants";
            this.rbParticipantMusic.UseVisualStyleBackColor = true;
            this.rbParticipantMusic.CheckedChanged += new System.EventHandler(this.rbParticipantMusic_CheckedChanged);
            // 
            // rbPublicMusic
            // 
            this.rbPublicMusic.AutoSize = true;
            this.rbPublicMusic.Checked = true;
            this.rbPublicMusic.Location = new System.Drawing.Point(6, 19);
            this.rbPublicMusic.Name = "rbPublicMusic";
            this.rbPublicMusic.Size = new System.Drawing.Size(61, 17);
            this.rbPublicMusic.TabIndex = 0;
            this.rbPublicMusic.TabStop = true;
            this.rbPublicMusic.Text = "Anyone";
            this.rbPublicMusic.UseVisualStyleBackColor = true;
            this.rbPublicMusic.CheckedChanged += new System.EventHandler(this.rbPublicMusic_CheckedChanged);
            // 
            // btnSelectCase
            // 
            this.btnSelectCase.Location = new System.Drawing.Point(6, 164);
            this.btnSelectCase.Name = "btnSelectCase";
            this.btnSelectCase.Size = new System.Drawing.Size(80, 23);
            this.btnSelectCase.TabIndex = 3;
            this.btnSelectCase.Text = "Select case...";
            this.btnSelectCase.UseVisualStyleBackColor = true;
            this.btnSelectCase.Click += new System.EventHandler(this.btnSelectCase_Click);
            // 
            // cbUseCase
            // 
            this.cbUseCase.AutoSize = true;
            this.cbUseCase.Checked = true;
            this.cbUseCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseCase.Location = new System.Drawing.Point(6, 141);
            this.cbUseCase.Name = "cbUseCase";
            this.cbUseCase.Size = new System.Drawing.Size(153, 17);
            this.cbUseCase.TabIndex = 2;
            this.cbUseCase.Text = "Use Case: (None selected)";
            this.cbUseCase.UseVisualStyleBackColor = true;
            this.cbUseCase.CheckedChanged += new System.EventHandler(this.cbUseCase_CheckedChanged);
            // 
            // tbDesc
            // 
            this.tbDesc.Location = new System.Drawing.Point(6, 98);
            this.tbDesc.MaxLength = 5000;
            this.tbDesc.Name = "tbDesc";
            this.tbDesc.Size = new System.Drawing.Size(351, 20);
            this.tbDesc.TabIndex = 1;
            this.tbDesc.TextChanged += new System.EventHandler(this.tbDesc_TextChanged);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(6, 46);
            this.tbName.MaxLength = 5000;
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(351, 20);
            this.tbName.TabIndex = 0;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // gbNet
            // 
            this.gbNet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNet.Controls.Add(this.numPort);
            this.gbNet.Controls.Add(this.label5);
            this.gbNet.Controls.Add(this.label4);
            this.gbNet.Controls.Add(this.label3);
            this.gbNet.Controls.Add(this.tbOpPass);
            this.gbNet.Controls.Add(this.tbPass);
            this.gbNet.Controls.Add(this.cbServerPublic);
            this.gbNet.Location = new System.Drawing.Point(381, 12);
            this.gbNet.Name = "gbNet";
            this.gbNet.Size = new System.Drawing.Size(365, 345);
            this.gbNet.TabIndex = 1;
            this.gbNet.TabStop = false;
            this.gbNet.Text = "Net Settings";
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(6, 267);
            this.numPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(63, 20);
            this.numPort.TabIndex = 7;
            this.numPort.Value = new decimal(new int[] {
            27015,
            0,
            0,
            0});
            this.numPort.ValueChanged += new System.EventHandler(this.numPort_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(299, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Server Port (don\'t change unless you know what you\'re doing)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(356, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Admin/Operator Password (Optional, needed to use Admin Tools remotely)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Connection Password (Optional):";
            // 
            // tbOpPass
            // 
            this.tbOpPass.Location = new System.Drawing.Point(6, 184);
            this.tbOpPass.Name = "tbOpPass";
            this.tbOpPass.Size = new System.Drawing.Size(353, 20);
            this.tbOpPass.TabIndex = 2;
            this.tbOpPass.TextChanged += new System.EventHandler(this.tbOpPass_TextChanged);
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(6, 98);
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(353, 20);
            this.tbPass.TabIndex = 1;
            this.tbPass.TextChanged += new System.EventHandler(this.tbPass_TextChanged);
            // 
            // cbServerPublic
            // 
            this.cbServerPublic.AutoSize = true;
            this.cbServerPublic.Checked = true;
            this.cbServerPublic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbServerPublic.Location = new System.Drawing.Point(6, 26);
            this.cbServerPublic.Name = "cbServerPublic";
            this.cbServerPublic.Size = new System.Drawing.Size(265, 17);
            this.cbServerPublic.TabIndex = 0;
            this.cbServerPublic.Text = "Server is public (will appear on all client server lists)";
            this.cbServerPublic.UseVisualStyleBackColor = true;
            this.cbServerPublic.CheckedChanged += new System.EventHandler(this.cbServerPublic_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(538, 365);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(645, 365);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(101, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(758, 400);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbNet);
            this.Controls.Add(this.gbServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.gbServer.ResumeLayout(false);
            this.gbServer.PerformLayout();
            this.gbMusic.ResumeLayout(false);
            this.gbMusic.PerformLayout();
            this.gbNet.ResumeLayout(false);
            this.gbNet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbServer;
        private System.Windows.Forms.TextBox tbDesc;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.CheckBox cbUseCase;
        private System.Windows.Forms.Button btnSelectCase;
        private System.Windows.Forms.GroupBox gbMusic;
        private System.Windows.Forms.RadioButton rbPublicMusic;
        private System.Windows.Forms.CheckBox cbMusicLoop;
        private System.Windows.Forms.RadioButton rbParticipantMusic;
        private System.Windows.Forms.RadioButton rbProtectedMusic;
        private System.Windows.Forms.RadioButton rbPrivateMusic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbNet;
        private System.Windows.Forms.CheckBox cbServerPublic;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.TextBox tbOpPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}