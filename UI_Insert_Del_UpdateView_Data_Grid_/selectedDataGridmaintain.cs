using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    class selectedDataGridmaintain
    {
        private static int v_selectedId;


        public static int selectedId
        {
            get { return v_selectedId; }
            set { v_selectedId = value; }
        }
    }
}
