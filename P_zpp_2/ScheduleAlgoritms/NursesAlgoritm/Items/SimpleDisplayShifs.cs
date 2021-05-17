using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.NursesAlgoritm.Items
{
    public class SimpleDisplayShifs
    {
        
        public String Title { get; set; }
        public DateTime StartDate { get; set; }

        public SimpleDisplayShifs(DateTime date, string shift)
        {
            StartDate = date;
            Title = shift;
        }
    }
}
