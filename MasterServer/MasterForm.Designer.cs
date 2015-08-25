namespace MasterServer
{
    partial class MasterForm
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
            this.lb_Servers = new System.Windows.Forms.ListBox();
            this.refreshStatsTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lb_Servers
            // 
            this.lb_Servers.FormattingEnabled = true;
            this.lb_Servers.Location = new System.Drawing.Point(12, 12);
            this.lb_Servers.Name = "lb_Servers";
            this.lb_Servers.Size = new System.Drawing.Size(286, 173);
            this.lb_Servers.TabIndex = 0;
            // 
            // refreshStatsTimer
            // 
            this.refreshStatsTimer.Enabled = true;
            this.refreshStatsTimer.Interval = 5000;
            this.refreshStatsTimer.Tick += new System.EventHandler(this.refreshStatsTimer_Tick);
            // 
            // MasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 333);
            this.Controls.Add(this.lb_Servers);
            this.Name = "MasterForm";
            this.Text = "Attorney Online Deluxe Master Server";
            this.Load += new System.EventHandler(this.MasterForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_Servers;
        private System.Windows.Forms.Timer refreshStatsTimer;
    }
}

