﻿using System;
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
        public delegate void delPassData(TextBox text);

        public Planning()
        {
            InitializeComponent();
            OdswiezDane();
        }

        private void OdswiezDane()
        {
            dailyBtn.BackColor = Color.Lime;

            DataSet ds = sqlQuery.GetDataFromSql("  select prob.probaId as 'Id', proj.projektNazwa as 'Projekt', form.formaNazwa as 'F', " +
            "masz.maszynaNumer as 'M', det.detalNazwa as 'Detal', godzStart as 'Start', godzKoniec as 'Koniec', dzienStart as 'Dzień', " +
            "odpowiedzialny as 'Inżynier', statusProby as 'Status' from Projekt proj, Forma form, Maszyna  masz, Detal_komplet det,proby " +
            "prob where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and statusProby in ('Zaplanowana','Potwierdzona');");
            dataGridPlanning.DataSource = ds.Tables[0];
        }
        private void Planning_Load(object sender, EventArgs e)
        {
            dataGridPlanning.CurrentCell.Selected = false;
        }


        private void dataGridPlanning_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                senderGrid.Columns[e.ColumnIndex] == Realizuj)
            {
                try
                {
                    if (MessageBox.Show("Czy chcesz potwierdzic termin?", "Potwierdź próbęe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int idProby = int.Parse(senderGrid.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                        sqlNonQuery.PutDataToSql("update Proby set statusProby = 'Potwierdzona' where probaId = '" + idProby + "' ");
                        MessageBox.Show("Termin potwierdzony");
                        OdswiezDane();
                    }
                    else
                    {
                        MessageBox.Show("Termin niepotwierdzony");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Nie można dodać potwierdzenia. Skontaktuj się z administratorem.");
                }
            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                senderGrid.Columns[e.ColumnIndex] == Inny)
            {
                using (innyTermincs frm = new innyTermincs())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        int idProby = int.Parse(senderGrid.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                        sqlNonQuery.PutDataToSql("update Proby set dzienStart = '"+frm.innyTerminDzien.ToDateTimeString()+"', godzStart = '" + frm.innyTerminStart + "', godzKoniec = '" + frm.innyTerminKoniec+ "', statusProby = 'Potwierdzona' where probaId = '" + idProby + "' ");
                        MessageBox.Show("Termin potwierdzony");
                        OdswiezDane();
                    }
                }
            }

        }

        //public void funData(DateTimePicker dateTime)
        //{
        //    label1.Text = dateTime.Value.ToString();

        //}

        #region Cell colors
        private void dataGridPlanning_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridPlanning.Columns[e.ColumnIndex].HeaderText == "Status" && dataGridPlanning.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            // if the column is bool_badge and check null value for the extra row at dgv
            {
                if (dataGridPlanning.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Zaplanowana")
                {
                    dataGridPlanning.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
                }
                if (dataGridPlanning.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Potwierdzona")
                {
                    dataGridPlanning.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.ForestGreen;
                }
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            var add = new addFill();
            add.ShowDialog();
            OdswiezDane();
        }

        private void dailyBtn_Click(object sender, EventArgs e)
        {
            OdswiezDane();
        }

        private void weeklyBtn_Click(object sender, EventArgs e)
        {
            weeklyBtn.BackColor = Color.Lime;

            DataSet ds = sqlQuery.GetDataFromSql("  select prob.probaId as 'Id', proj.projektNazwa as 'Projekt', form.formaNazwa as 'F', " +
           "masz.maszynaNumer as 'M', det.detalNazwa as 'Detal', godzStart as 'Start', godzKoniec as 'Koniec', dzienStart as 'Dzień', " +
           "odpowiedzialny as 'Inżynier', statusProby as 'Status' from Projekt proj, Forma form, Maszyna  masz, Detal_komplet det,proby " +
           "prob where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and statusProby in ('Zaplanowana','Potwierdzona') and dzienStart between DateAdd(DD,7,GETDATE()) and GETDATE();");
            dataGridPlanning.DataSource = ds.Tables[0];
        }
    }
}

