using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class TrialEnd : Form
    {
        public TrialEnd()
        {
            InitializeComponent();
            DateTimePickerKonczeniePróby.Format = DateTimePickerFormat.Time;
            DateTimePickerKonczeniePróby.CustomFormat = "hh:mm";
            DateTimePickerKonczeniePróby.ShowUpDown = true;
            DateTimePickerKonczeniePróby.Value = DateTimePicker.MinimumDateTime;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            loaddata();
        }
        public void loaddata()
        {
            string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;

            System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(connectionStrin);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.CommandText = ("UPDATE Proby SET czasTrwania = @czasTrwania, statusProby = 'Zakończona' WHERE probaId = @probaId ");
            cmd.Parameters.AddWithValue("@czasTrwania", DateTimePickerKonczeniePróby.Value);
            cmd.Parameters.AddWithValue("@probaId", selectedDataGridmaintain.selectedId);


            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            this.Close();
        }
    }
}
