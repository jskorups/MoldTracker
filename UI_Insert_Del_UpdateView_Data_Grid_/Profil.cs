using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class Profil : Form
    {
        public Profil()
        {
            InitializeComponent();
        }

        private void Profil_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (wgrajZdjecieDialog.ShowDialog() == DialogResult.OK);
        }
    }
}
