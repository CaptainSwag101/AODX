using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class EvidenceEditor : Form
    {
        public List<Evidence> evidence;
        public int selected = 1;

        public EvidenceEditor()
        {
            InitializeComponent();
            iconPB.Parent = backgroundPB;
        }

        private void EvidenceEditor_Load(object sender, EventArgs e)
        {
            iconPB.Image = evidence[selected].icon;
            nameTB.Text = evidence[selected].name;
            descTB.Text = evidence[selected].desc;
            noteTB.Text = evidence[selected].note;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void iconPB_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                iconPB.Image = Image.FromStream(openFileDialog.OpenFile());
            }
        }
    }
}
