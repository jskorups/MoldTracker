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
    public partial class machineRemove : Form
    {
        public machineRemove()
        {
            InitializeComponent();
            fillMachine();

    }
        public void fillMachine()
        {
            try
            {
                DataSet dP = sqlQuery.GetDataFromSql("select maszynaId, maszynaNumer from Maszyna");
                ComboMachineNameRemove.DataSource = dP.Tables[0];
                ComboMachineNameRemove.ValueMember = "maszynaId";
                ComboMachineNameRemove.DisplayMember = "maszynaNumer";
                ComboMachineNameRemove.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

         }
        private void BtnMachineAddClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy jesteś pewny że chcesz usunąć trwale usunąć maszynę ?", "Potwierdź próbę", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Data.SqlClient.SqlConnection sqlConnection1 =
                //new System.Data.SqlClient.SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4");
                new System.Data.SqlClient.SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True");
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "delete from Maszyna where maszynaNumer = @usuwanaMaszyna";
                cmd.Parameters.AddWithValue("@usuwanaMaszyna", ComboMachineNameRemove.Text.ToString());

                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                int a  = cmd.ExecuteNonQuery();
                if (a == 0)
                {
                    MessageBox.Show("Udało się maszyna usunięta!");
                }
                else {
                    {
                        MessageBox.Show("Nie można usunąć maszyny. Skontaktuj się z administratorem.");
                    }
                }
                sqlConnection1.Close();
                
                this.Close();

            }
            else
            {
                MessageBox.Show("Nie wykonano żadnej akcji.");
                return;
            }
        }
    }
    }
