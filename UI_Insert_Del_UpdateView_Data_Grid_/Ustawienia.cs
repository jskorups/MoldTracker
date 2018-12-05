﻿using System;
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
    public partial class Ustawienia : Form
    {
        

        public Ustawienia()
        {
            InitializeComponent();
        }

        string oldPath;
        public string newpath = @"C:\test\";
        string newFileName = "" + loginClass.loginMain + ".jpg";

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *bmp;)|*.jpg; *.jpeg; *.gif; *.bmp;";
             
                if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    oldPath = @"" + open.FileName + "";
  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                    FileInfo f1 = new FileInfo(oldPath);
                    if (f1.Exists)
                    {
                        f1.CopyTo(string.Format("{0}{1}", newpath, newFileName), true);
                        this.Close();
                    }
                } 
            catch (Exception)
            {
                throw;
            }
        
            
        }
    }
}
