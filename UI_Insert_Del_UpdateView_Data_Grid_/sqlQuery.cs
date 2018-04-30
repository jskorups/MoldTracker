using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public class sqlQuery
    {

        public static DataSet GetDataFromSql (string query)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4");

            using (con)
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(ds);
        
            }
            return ds;
        }
    }
}
