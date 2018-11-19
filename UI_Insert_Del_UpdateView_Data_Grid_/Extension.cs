using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public static class Extension
    {
        public static string ToDateTimeString(this object obj)
        {
            DateTime data = Convert.ToDateTime(obj);
            return data.ToString("yyyy-MM-dd");
        }
    }
}
