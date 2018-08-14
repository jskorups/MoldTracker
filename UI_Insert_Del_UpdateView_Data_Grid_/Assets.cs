using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class Assets : Form
    {
        public Assets()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void BtnMachineCircAdd_Click(object sender, EventArgs e)
        {
            var machineAdd = new machineAdd();
            machineAdd.Show();
        }

        private void BtnMachineCircRemove_Click(object sender, EventArgs e)
        {
            var machineRemove = new machineRemove();
            machineRemove.Show();
        }

        private void projectAddCirc_Click(object sender, EventArgs e)
        {
            var projectAdd = new ProjectAdd();
            projectAdd.Show();
        }

        private void moldAddCirc_Click(object sender, EventArgs e)
        {
            var moldAdd = new moldAdd();
            moldAdd.Show();
        }

        private void circularButton4_Click(object sender, EventArgs e)
        {
            //var detailAdd = new detailsAdd();
            //detailAdd.Show();
        }
    }
}
