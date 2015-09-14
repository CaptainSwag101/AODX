namespace Client
{
    partial class EvidenceEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EvidenceEditor));
            this.backgroundPB = new System.Windows.Forms.PictureBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.iconPB = new System.Windows.Forms.PictureBox();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.noteLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.noteTB = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPB)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundPB
            // 
            this.backgroundPB.Image = ((System.Drawing.Image)(resources.GetObject("backgroundPB.Image")));
            this.backgroundPB.Location = new System.Drawing.Point(0, 0);
            this.backgroundPB.Name = "backgroundPB";
            this.backgroundPB.Size = new System.Drawing.Size(440, 222);
            this.backgroundPB.TabIndex = 0;
            this.backgroundPB.TabStop = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(0, 222);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(220, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(219, 222);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(221, 23);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "&OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // iconPB
            // 
            this.iconPB.BackColor = System.Drawing.Color.Transparent;
            this.iconPB.Location = new System.Drawing.Point(10, 16);
            this.iconPB.Name = "iconPB";
            this.iconPB.Size = new System.Drawing.Size(70, 70);
            this.iconPB.TabIndex = 3;
            this.iconPB.TabStop = false;
            this.iconPB.Click += new System.EventHandler(this.iconPB_Click);
            // 
            // nameTB
            // 
            this.nameTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nameTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameTB.Font = new System.Drawing.Font("Ace Attorney 2", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTB.ForeColor = System.Drawing.Color.DarkOrange;
            this.nameTB.Location = new System.Drawing.Point(90, 11);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(338, 15);
            this.nameTB.TabIndex = 4;
            this.nameTB.Text = "Name";
            this.nameTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // noteLabel
            // 
            this.noteLabel.AutoSize = true;
            this.noteLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(184)))), ((int)(((byte)(160)))));
            this.noteLabel.Font = new System.Drawing.Font("Ace Attorney 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noteLabel.Location = new System.Drawing.Point(93, 29);
            this.noteLabel.Name = "noteLabel";
            this.noteLabel.Size = new System.Drawing.Size(96, 15);
            this.noteLabel.TabIndex = 5;
            this.noteLabel.Text = "Type: Evidence";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(116)))), ((int)(((byte)(92)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Ace Attorney 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(10, 110);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(418, 106);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "Description";
            // 
            // noteTB
            // 
            this.noteTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(184)))), ((int)(((byte)(160)))));
            this.noteTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.noteTB.Font = new System.Drawing.Font("Ace Attorney 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noteTB.Location = new System.Drawing.Point(96, 47);
            this.noteTB.Multiline = true;
            this.noteTB.Name = "noteTB";
            this.noteTB.Size = new System.Drawing.Size(326, 39);
            this.noteTB.TabIndex = 7;
            this.noteTB.Text = "Note";
            // 
            // EvidenceEditor
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(440, 245);
            this.Controls.Add(this.noteTB);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.noteLabel);
            this.Controls.Add(this.nameTB);
            this.Controls.Add(this.iconPB);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.backgroundPB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EvidenceEditor";
            this.Text = "Evidence Editor";
            this.Load += new System.EventHandler(this.EvidenceEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox backgroundPB;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.PictureBox iconPB;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.Label noteLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox noteTB;
    }
}