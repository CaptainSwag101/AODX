using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Server
{
    public partial class SettingsForm : Form
    {
        /*string name = "";
        string desc = "";
        bool isPublic = true;
        int port = 27015;
        string password = "";
        string oppassword = "";
        byte musicmode = 1;
        bool loop = true;
        bool useCase = true;
        string caseName = ""; */

        private int musicmode = 1;
        private bool startup = true;
        private bool changesMade = false;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            string[] rawInfo = iniParser.GetServerInfo().Split('|');
            tbName.Text = rawInfo[0];
            tbDesc.Text = rawInfo[1];
            cbServerPublic.Checked = Convert.ToBoolean(Convert.ToInt16(rawInfo[2]));
            numPort.Value = Convert.ToInt32(rawInfo[3]);
            numPort.Validate();
            tbPass.Text = rawInfo[4];
            tbOpPass.Text = rawInfo[5];
            switch (Convert.ToInt32(rawInfo[6]))
            {
                case 1:
                    rbPublicMusic.PerformClick();
                    break;

                case 2:
                    rbParticipantMusic.PerformClick();
                    break;

                case 3:
                    rbProtectedMusic.PerformClick();
                    break;

                case 4:
                    rbPrivateMusic.PerformClick();
                    break;
            }
            cbMusicLoop.Checked = Convert.ToBoolean(Convert.ToInt16(rawInfo[7]));
            cbUseCase.Checked = Convert.ToBoolean(Convert.ToInt16(rawInfo[8]));
            if (!string.IsNullOrWhiteSpace(rawInfo[9]))
                cbUseCase.Text = "Use Case: " + rawInfo[9];
            else
                cbUseCase.Text = "Use Case: (None selected)";

            startup = false;
        }

        private void Save()
        {
            File.CreateText("base/settings.temp").Close();
            using (StreamWriter w = new StreamWriter("base/settings.temp"))
            {
                w.WriteLine("[net]");
                w.WriteLine("public = " + Convert.ToInt32(cbServerPublic.Checked));
                w.WriteLine("password = " + tbPass.Text);
                w.WriteLine("oppassword = " + tbOpPass.Text);
                w.WriteLine("port = " + numPort.Value.ToString());
                w.WriteLine("[server]");
                w.WriteLine("Name = " + tbName.Text);
                w.WriteLine("Desc = " + tbDesc.Text);
                w.WriteLine("musicmode = " + musicmode);
                w.WriteLine("loopmusic = " + Convert.ToInt32(cbMusicLoop.Checked));
                w.WriteLine("usecase = " + Convert.ToInt32(cbUseCase.Checked));
                if (cbUseCase.Text == "Use Case: (None selected)")
                    w.WriteLine("case = ");
                else
                    w.WriteLine("case = " + cbUseCase.Text.Substring(10));
            }
            if (File.Exists("base/settings.old"))
                File.Delete("base/settings.old");
            if (File.Exists("base/settings.ini"))
                File.Move("base/settings.ini", "base/settings.old");
            File.Move("base/settings.temp", "base/settings.ini");
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changesMade && DialogResult != DialogResult.Cancel & DialogResult != DialogResult.OK)
            {
                switch (MessageBox.Show("Do you want to save your settings?", "Save?", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        Save();
                        break;

                    case DialogResult.No:
                        //DontSave();
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                }
            }

            if (DialogResult == DialogResult.OK)
            {
                Save();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            if (startup == false)
                changesMade = true;
        }

        private void tbDesc_TextChanged(object sender, EventArgs e)
        {
            if (startup == false)
                changesMade = true;
        }

        private void btnSelectCase_Click(object sender, EventArgs e)
        {
            if (startup == false)
                changesMade = true;
            FolderBrowserDialog caseChooser = new FolderBrowserDialog();
            //caseChooser.ShowDialog(this);
            caseChooser.RootFolder = Environment.SpecialFolder.MyComputer;
            caseChooser.ShowNewFolderButton = false;
            if (Directory.Exists("base/cases"))
                caseChooser.SelectedPath = Path.GetFullPath("base/cases");
            if (caseChooser.ShowDialog() == DialogResult.OK)
            {
                string folderName = caseChooser.SelectedPath.Substring(Path.GetFullPath("base/cases").Length + 1);
                cbUseCase.Text = "Use Case: " + folderName;
            }
        }

        private void cbUseCase_CheckedChanged(object sender, EventArgs e)
        {
            if (startup == false)
                changesMade = true;
        }

        private void cbMusicLoop_CheckedChanged(object sender, EventArgs e)
        {
            if (startup == false)
                changesMade = true;
        }

        private void rbPublicMusic_CheckedChanged(object sender, EventArgs e)
        {
            if (startup == false)
                changesMade = true;
            musicmode = 1;
        }

        private void rbParticipantMusic_CheckedChanged(object sender, EventArgs e)
        {
            if (startup == false)
                changesMade = true;
            musicmode = 2;
        }

        private void rbProtectedMusic_CheckedChanged(object sender, EventArgs e)
        {
            if (startup == false)
                changesMade = true;
            musicmode = 3;
        }

        private void rbPrivateMusic_CheckedChanged(object sender, EventArgs e)
        {
            if (startup == false)
                changesMade = true;
            musicmode = 4;
        }

        private void cbServerPublic_CheckedChanged(object sender, EventArgs e)
        {
            if (startup == false)
                changesMade = true;
        }

        private void tbPass_TextChanged(object sender, EventArgs e)
        {
            if (startup == false)
                changesMade = true;
        }

        private void tbOpPass_TextChanged(object sender, EventArgs e)
        {
            if (startup == false)
                changesMade = true;
        }

        private void numPort_ValueChanged(object sender, EventArgs e)
        {
            if (startup == false)
                changesMade = true;
        }
    }
}
