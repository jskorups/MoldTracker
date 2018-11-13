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

        private void OdswiezDane()
        {
            DataSet ds = sqlQuery.GetDataFromSql("  select projektId, formaId, maszynaId, detalId, godzStart, godzKoniec, dzienStart, odpowiedzialny from Proby;");
            dataGridPlanning.DataSource = ds.Tables[0];
        }

        private void Planning_Load(object sender, EventArgs e)
        {

            OdswiezDane();
        }

        private void dataGridPlanning_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                senderGrid.Columns[e.ColumnIndex] == Realizuj)
            {
                //senderGrid.Rows[e.RowIndex].Cells["ProbaId"].Value
            }
            OdswiezDane();
        }


    }
}
