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
    public partial class detailsAdd : Form
    {
        public detailsAdd()
        {
            InitializeComponent();
            fillMold2();
        }

        public void fillMold2()
        {
            try
            {
                DataSet dP = sqlQuery.GetDataFromSql("select projektNazwa from Projekt");
                ComboProjectForDetailsAdd.DataSource = dP.Tables[0];
                //ComboMachineNameRemove.ValueMember = "maszynaId";
                ComboProjectForDetailsAdd.DisplayMember = "projektNazwa";
                ComboProjectForDetailsAdd.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie można załadować listy projektów");
            }
        }

        private void ComboProjectForDetailsAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dF = sqlQuery.GetDataFromSql("SELECT formaNazwa FROM Forma INNER JOIN Projekt ON Forma.FK_projektId = Projekt.projektId where projekt.projektNazwa = '" + ComboProjectForDetailsAdd.Text + "'");
            comboBoxFormaDetailsADD.DataSource = dF.Tables[0];
            comboBoxFormaDetailsADD.ValueMember = "formaNazwa";
            comboBoxFormaDetailsADD.SelectedIndex = -1;
            detalTxtBox1.Text = String.Empty;
            detalTxtBox2.Text = String.Empty;
            detalTxtBox3.Text = String.Empty;
        }
        private void comboBoxFormaDetailsADD_SelectedIndexChanged(object sender, EventArgs e)
        {
            detalTxtBox1.Text = String.Empty;
            detalTxtBox2.Text = String.Empty;
            detalTxtBox3.Text = String.Empty;
            detalTxtBox4.Text = String.Empty;
        }
        //tutaj skonczylem
        private void BtnMoldAddClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addProjectFromAddMold_Click(object sender, EventArgs e)
        {
            var projectAdd = new ProjectAdd();
            projectAdd.Show();
        }

        private void addDetailValidationButton(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ComboProjectForDetailsAdd.Text) || string.IsNullOrEmpty(comboBoxFormaDetailsADD.Text))
            {
                BtnMoldAdd.Enabled = false;
                BtnMoldAdd.BackColor = Color.LightGray;
            }
            else if (!string.IsNullOrEmpty(ComboProjectForDetailsAdd.Text) || !string.IsNullOrEmpty(comboBoxFormaDetailsADD.Text))
            {
                BtnMoldAdd.Enabled = true;
                BtnMoldAdd.BackColor = Color.Lime;
            }
        }

        private void BtnMoldAdd_Click(object sender, EventArgs e)
        {
  //          insert into Detal_komplet(detalNazwa, FK_formaId, Forma) values(@detal1, (select projektId from Projekt where projektNazwa = @projekt),@forma),(@detal2, (select projektId from Projekt where projektNazwa = @projekt),@forma),(@detal3, (select projektId from Projekt where projektNazwa = @projekt),@forma),(@detal4, (select projektId from Projekt where projektNazwa = @projekt),@forma)
        }


    }
}
