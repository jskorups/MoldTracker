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
        public Planning()
        {
            InitializeComponent();
            dataGridPlanning.Columns["Realizuj"].Width = 90;
            dataGridPlanning.Columns["Inny"].Width = 90;


        }



        private void OdswiezDane()
        {
            DataSet ds = sqlQuery.GetDataFromSql("  select prob.probaId as 'Id', proj.projektNazwa as 'Projekt', form.formaNazwa as 'Forma', masz.maszynaNumer as 'Maszyna', det.detalNazwa as 'Detal', godzStart as 'Start', godzKoniec as 'Koniec', dzienStart as 'Dzień', odpowiedzialny as 'Odpowiedzialny', statusProby as 'Status' from Projekt proj, Forma form, Maszyna  masz, Detal_komplet det,proby prob where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId ;");
            dataGridPlanning.DataSource = ds.Tables[0];
            
        }
        private void Planning_Load(object sender, EventArgs e)
        {
            OdswiezDane();
            dataGridPlanning.CurrentCell.Selected = false;
        }

        private void dataGridPlanning_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                senderGrid.Columns[e.ColumnIndex] == Realizuj)
            {
                int idProby = int.Parse(senderGrid.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                sqlNonQuery.PutDataToSql("update Proby set statusProby = 'Potwierdzona' where probaId = '"+idProby+"' ");
                OdswiezDane();

            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                senderGrid.Columns[e.ColumnIndex] == Inny)
            {
                MessageBox.Show("kespa");
            }


            //OdswiezDane();
        }


    }
}
