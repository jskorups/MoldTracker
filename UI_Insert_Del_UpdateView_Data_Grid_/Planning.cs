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
            dailyPlan();
            wylaczaniePrzycisków();
            comboData();



        }

        #region wylaczanie przyciskówm - uprawienia

        public void wylaczaniePrzycisków()
        {

            if (loginClass.PoziomUprawnien == "planista")
            {
                dataGridPlanning.Columns["Realizuj"].Visible = true;
                dataGridPlanning.Columns["Inny"].Visible = true;

            }
            else
            {
                dataGridPlanning.Columns["Realizuj"].Visible = false;
                dataGridPlanning.Columns["Inny"].Visible = false;
            }
        }

        #endregion
        #region planDzienny - głowna metoda

        private void dailyPlan()
        {
            dailyBtn.BackColor = Color.Lime;
            DataSet ds = sqlQuery.GetDataFromSql("  select prob.probaId as 'Id', proj.projektNazwa as 'Projekt', form.formaNazwa as 'F', " +
            "masz.maszynaNumer as 'M', det.detalNazwa as 'Detal', godzStart as 'Start', godzKoniec as 'Koniec', dzienStart as 'Dzień', " +
            "odpowiedzialny as 'Inżynier', statusProby as 'Status' from Projekt proj, Forma form, Maszyna  masz, Detal_komplet det,proby " +
            "prob where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and statusProby in ('Zaplanowana','Potwierdzona', 'Potwierdz. (zmiana. term.)' ) and dzienStart = CONVERT(varchar,GETDATE(),112) ;");
            dataGridPlanning.DataSource = ds.Tables[0];
        }

        private void weeklyPlan()
        {

            DataSet ds = sqlQuery.GetDataFromSql("  select prob.probaId as 'Id', proj.projektNazwa as 'Projekt', form.formaNazwa as 'F', " +
          "masz.maszynaNumer as 'M', det.detalNazwa as 'Detal', godzStart as 'Start', godzKoniec as 'Koniec', dzienStart as 'Dzień', " +
          "odpowiedzialny as 'Inżynier', statusProby as 'Status' from Projekt proj, Forma form, Maszyna  masz, Detal_komplet det,proby " +
          "prob where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and statusProby in ('Zaplanowana','Potwierdzona') and dzienStart between GETDATE() and DATEADD(day,7, GETDATE());");
            dataGridPlanning.DataSource = ds.Tables[0];
        }


        private void monthlyPlan()
        {

            DataSet ds = sqlQuery.GetDataFromSql("  select prob.probaId as 'Id', proj.projektNazwa as 'Projekt', form.formaNazwa as 'F', " +
            "masz.maszynaNumer as 'M', det.detalNazwa as 'Detal', godzStart as 'Start', godzKoniec as 'Koniec', dzienStart as 'Dzień', " +
            "odpowiedzialny as 'Inżynier', statusProby as 'Status' from Projekt proj, Forma form, Maszyna  masz, Detal_komplet det,proby " +
            "prob where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and statusProby in ('Zaplanowana','Potwierdzona') and dzienStart between GETDATE() and DATEADD(MONTH,1, GETDATE());");
            dataGridPlanning.DataSource = ds.Tables[0];
        }


        #endregion

        #region Kasowanie wybrania komórki
        private void Planning_Load(object sender, EventArgs e)
        {
            //       dataGridPlanning.CurrentCell.Selected = false;
        }
        #endregion
        #region Przyciski na datagridzie
        private void dataGridPlanning_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                senderGrid.Columns[e.ColumnIndex] == Realizuj)
            {

                if (MessageBox.Show("Czy chcesz potwierdzic termin?", "Potwierdź próbę", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes /*&& dailyBtn.BackColor == Color.Lime*/)
                {
                    int idProby = int.Parse(senderGrid.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                    sqlNonQuery.PutDataToSql("update Proby set statusProby = 'Potwierdzona' where probaId = '" + idProby + "' ");
                    MessageBox.Show("Termin potwierdzony");
                    dailyPlan();
                }
                //else if (MessageBox.Show("Czy chcesz potwierdzic termin?", "Potwierdź próbę", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes && weeklyBtn.BackColor == Color.Lime)
                //{
                //    int idProby = int.Parse(senderGrid.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                //    sqlNonQuery.PutDataToSql("update Proby set statusProby = 'Potwierdzona' where probaId = '" + idProby + "' ");
                //    MessageBox.Show("Termin potwierdzony");
                //    weeklyPlan();

                //}
                //else if (MessageBox.Show("Czy chcesz potwierdzic termin?", "Potwierdź próbę", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes && monthlyBtn.BackColor == Color.Lime)
                //{
                //    int idProby = int.Parse(senderGrid.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                //    sqlNonQuery.PutDataToSql("update Proby set statusProby = 'Potwierdzona' where probaId = '" + idProby + "' ");
                //    MessageBox.Show("Termin potwierdzony");
                //   monthlyPlan();

                //}
                else
                {
                    MessageBox.Show("Termin niepotwierdzony");
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
                        sqlNonQuery.PutDataToSql("update Proby set dzienStart = '" + frm.innyTerminDzien.ToDateTimeString() + "', godzStart = '" + frm.innyTerminStart + "', godzKoniec = '" + frm.innyTerminKoniec + "', statusProby = 'Potwierdz. (zmiana. term.)' where probaId = '" + idProby + "' ");
                        MessageBox.Show("Termin potwierdzony");
                        //dataGridPlanning.Refresh();
                        dailyPlan();
                    }
                }
            }

        }
        #endregion
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
                else if (dataGridPlanning.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Potwierdzona")
                {
                    dataGridPlanning.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.ForestGreen;
                }
                else if (dataGridPlanning.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Potwierdz. (zmiana. term.)")
                {
                    dataGridPlanning.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }
        #endregion
        #region Przycisk dodaj probe z planningu
        private void button2_Click(object sender, EventArgs e)
        {
            var add = new addFill();
            add.ShowDialog();
            dailyPlan();
        }
        #endregion
        #region Przycisk daily
        private void dailyBtn_Click(object sender, EventArgs e)
        {
            dailyPlan();
            dailyBtn.BackColor = Color.Lime;
            weeklyBtn.BackColor = Color.Empty;
            monthlyBtn.BackColor = Color.Empty;
        }
        #endregion

        #region Przycisk weekly
        private void weeklyBtn_Click(object sender, EventArgs e)
        {
            dailyBtn.BackColor = Color.Empty;
            weeklyBtn.BackColor = Color.Lime;
            monthlyBtn.BackColor = Color.Empty;

            DataSet ds = sqlQuery.GetDataFromSql("  select prob.probaId as 'Id', proj.projektNazwa as 'Projekt', form.formaNazwa as 'F', " +
          "masz.maszynaNumer as 'M', det.detalNazwa as 'Detal', godzStart as 'Start', godzKoniec as 'Koniec', dzienStart as 'Dzień', " +
          "odpowiedzialny as 'Inżynier', statusProby as 'Status' from Projekt proj, Forma form, Maszyna  masz, Detal_komplet det,proby " +
          "prob where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and statusProby in ('Zaplanowana','Potwierdzona') and dzienStart between GETDATE() and DATEADD(day,7, GETDATE());");

            dataGridPlanning.DataSource = ds.Tables[0];

        }

        #endregion
        #region Przycisk monthly
        private void monthlyBtn_Click(object sender, EventArgs e)
        {
            dailyBtn.BackColor = Color.Empty;
            weeklyBtn.BackColor = Color.Empty;
            monthlyBtn.BackColor = Color.Lime;

            DataSet ds = sqlQuery.GetDataFromSql("  select prob.probaId as 'Id', proj.projektNazwa as 'Projekt', form.formaNazwa as 'F', " +
            "masz.maszynaNumer as 'M', det.detalNazwa as 'Detal', godzStart as 'Start', godzKoniec as 'Koniec', dzienStart as 'Dzień', " +
            "odpowiedzialny as 'Inżynier', statusProby as 'Status' from Projekt proj, Forma form, Maszyna  masz, Detal_komplet det,proby " +
            "prob where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and statusProby in ('Zaplanowana','Potwierdzona') and dzienStart between GETDATE() and DATEADD(MONTH,1, GETDATE());");
            dataGridPlanning.DataSource = ds.Tables[0];


        }
        #endregion

        #region Dane  - combobox projekt
        public void comboData()
        {
            DataSet dP = sqlQuery.GetDataFromSql("select * from Projekt");
            planningProjectChoice.DataSource = dP.Tables[0];
            planningProjectChoice.DisplayMember = "projektNazwa";
            planningProjectChoice.ValueMember = "projektId";
            planningProjectChoice.SelectedIndex = -1;
        }
        #endregion

        private void planningShowVsProjectBtn_Click(object sender, EventArgs e)
        {
            DataSet ds = sqlQuery.GetDataFromSql("  select prob.probaId as 'Id', proj.projektNazwa as 'Projekt', form.formaNazwa as 'F', " +
          "masz.maszynaNumer as 'M', det.detalNazwa as 'Detal', godzStart as 'Start', godzKoniec as 'Koniec', dzienStart as 'Dzień', " +
          "odpowiedzialny as 'Inżynier', statusProby as 'Status' from Projekt proj, Forma form, Maszyna  masz, Detal_komplet det,proby " +
          "prob where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and statusProby in ('Zaplanowana','Potwierdzona') and dzienStart = CONVERT(varchar,GETDATE(),112) and proj.projektId = '"+planningProjectChoice.SelectedValue+"';");
            dataGridPlanning.DataSource = ds.Tables[0];
        }
    }
}

