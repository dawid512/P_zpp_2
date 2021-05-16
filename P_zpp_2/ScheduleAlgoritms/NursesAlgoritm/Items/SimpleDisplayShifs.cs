using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.NursesAlgoritm.Items
{
    public class SimpleDisplayShifs
    {
        public DateTime Date { get; set; }
        public String Shift { get; set; }

        public SimpleDisplayShifs(DateTime date, string shift)
        {
            Date = date;
            Shift = shift;
        }
    }
}
