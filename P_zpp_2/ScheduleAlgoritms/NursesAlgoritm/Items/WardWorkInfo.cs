using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.NursesAlgoritm.Items
{
    public static class WardWorkInfo
    {
        public static int WorkCycleLenght = 16; //days
        public static int Available_Workers = 16;
        public static int Workers_RequiredforDayshift = 4; //5max 3understaffed
        public static int Workers_RequiredforNightshift = 3; //4max 2understaffed
    }
}
