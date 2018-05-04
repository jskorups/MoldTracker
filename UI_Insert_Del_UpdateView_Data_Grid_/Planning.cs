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
    public partial class Planning : Form
    {
        public Planning()
        {
            InitializeComponent();
        }

        private void Planning_Load(object sender, EventArgs e)
        {

        }

      
        public void colorButton(Button button)
        {
            if (button.Name == "dailyBtn")
            {
                dailyBtn.BackColor = Color.Lime;
                weeklyBtn.BackColor = SystemColors.Control;
                monthlyBtn.BackColor = SystemColors.Control;
            }
            else if (button.Name == "weeklyBtn")
            {
                weeklyBtn.BackColor = Color.Lime;
                dailyBtn.BackColor = SystemColors.Control;
                monthlyBtn.BackColor = SystemColors.Control;
            }
            else if (button.Name == "monthlyBtn")
            {
                monthlyBtn.BackColor = Color.Lime;
                dailyBtn.BackColor = SystemColors.Control;
                weeklyBtn.BackColor = SystemColors.Control;
            }
        }

        private void dailyBtnClick(object sender, EventArgs e)
        {
            colorButton(dailyBtn);
            if (!panel.Controls.Contains(dailyControl.Instance))
            {
                panel.Controls.Add(dailyControl.Instance);
                dailyControl.Instance.Dock = DockStyle.Fill;
                dailyControl.Instance.BringToFront();
            }
            else
                dailyControl.Instance.BringToFront();
        }

        private void weeklyBtnClick(object sender, EventArgs e)
        {
            colorButton(weeklyBtn);
            if (!panel.Controls.Contains(weeklyControl.Instance))
            {
                panel.Controls.Add(weeklyControl.Instance);
                weeklyControl.Instance.Dock = DockStyle.Fill;
                weeklyControl.Instance.BringToFront();
            }
            else
                weeklyControl.Instance.BringToFront();
        }

        private void monthlyBtnClick(object sender, EventArgs e)
        {
            colorButton(monthlyBtn);
            if (!panel.Controls.Contains(monthlyControl.Instance))
            {
                panel.Controls.Add(monthlyControl.Instance);
                monthlyControl.Instance.Dock = DockStyle.Fill;
                monthlyControl.Instance.BringToFront();
            }
            else
                monthlyControl.Instance.BringToFront();
        }

    }
}
