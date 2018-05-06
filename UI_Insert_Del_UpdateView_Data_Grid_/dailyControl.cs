using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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
                DataSet ds = sqlQuery.GetDataFromSql("select * from planDnia;");
                dailyGrid.DataSource = ds.Tables[0];

                //foreach (DataGridViewCell row in dailyGrid.Rows)
                //{
                //    for (int i = 0; i < dailyGrid.Columns.Count, i++)
                //    {
                //        if(
                //    }
                //}


                //foreach (DataGridViewRow row in dailyGrid.Rows)
                //{
                //    foreach (DataGridViewCell cell in row.Cells)
                //    {
                //        if (cell.Value.ToString() == "m")
                //        {
                            
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




      

        public dailyControl()
        {
            InitializeComponent();
            daily();


        }


    }
}
