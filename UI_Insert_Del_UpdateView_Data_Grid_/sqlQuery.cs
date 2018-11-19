using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public class sqlQuery
    {

        public static DataSet GetDataFromSql (string query)
        {
            DataSet ds = new DataSet();
            string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionStrin);


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
