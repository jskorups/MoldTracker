﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public static class loginClass
    {
        private static string v_loginMain = "";


        public static string loginMain
        {
            get { return v_loginMain; }
            set { v_loginMain = value; }
        }


        public static string PoziomUprawnien { get { return sqlQuery.GetTop1Sql("select poziomUprawnien from Uzytkownicy where nazwauzytkownika='" + loginClass.loginMain + "'").ToString(); } }

            
    }
}
