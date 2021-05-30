using P_zpp_2.Data;
using P_zpp_2.ScheduleAlgoritms.NursesAlgoritm.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.Areas.Identity.Pages.Account.Display_Interfaces
{
    public interface IWorkerScheduleDisplay
    {
        //public List<SimpleDisplayShifs> _ShiftsList { get; set; }

        public List<SimpleDisplayShifs> GetSchedule(AplicationUser AUser, string userId, string Schedule);



    }
}
