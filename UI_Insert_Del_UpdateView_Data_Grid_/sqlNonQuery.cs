using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    class sqlNonQuery
    {
        public static void PutDataToSql(string query)
        {
            string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionStrin);


            using (con)
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.UpdateCommand = con.CreateCommand();
                adapter.UpdateCommand.CommandText = query;
                adapter.UpdateCommand.ExecuteNonQuery();
            }
        }
    }
}
