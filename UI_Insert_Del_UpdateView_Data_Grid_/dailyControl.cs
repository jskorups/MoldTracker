using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace UI_Insert_Del_UpdateView_Data_Grid_
{

    public partial class dailyControl : UserControl
    {
        private static dailyControl _instance;
        public static dailyControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new dailyControl();
                return _instance;
            }
        }
        private void daily()
        {
            try
            {
                dailyGrid.Columns.Add("Maszyny", "Maszyna");
                for (int i = 0; i < 17; i++)
                {
                    dailyGrid.Columns.Add((i < 4 ? "0" : "") + (6 + i).ToString() + ":00", (i < 4 ? "0" : "") + (6 + i).ToString() + ":00");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void readData(DateTime day)
        {
            SqlConnection conn = //new SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True");
            new SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4");
            
            using (conn)
            {
                conn.Open();
                string sql = $@"select * from Proby where dzienStart='{day.ToShortDateString()}' order by maszynaID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dailyGrid.Rows.Count == 0 || dailyGrid[0, dailyGrid.Rows.Count - 1].Value.ToString() != dr["maszynaId"].ToString()) dailyGrid.Rows.Add();
                    dailyGrid[0, dailyGrid.Rows.Count - 1].Value = dr["maszynaId"].ToString();
                    int index = dailyGrid.Columns[dr["godzStart"].ToString().Substring(0, 5)].Index;
                    for (int i = 0; i < Convert.ToInt32(dr["czasStart"]); i++)
                    {
                        dailyGrid[index + i, dailyGrid.Rows.Count - 1].Style.BackColor = Color.Red;
                    }
                }
                conn.Close();
            }
        }
    

        public dailyControl()
        {
            InitializeComponent();
            daily();
            readData(DateTime.Now);



        }


    }
}
