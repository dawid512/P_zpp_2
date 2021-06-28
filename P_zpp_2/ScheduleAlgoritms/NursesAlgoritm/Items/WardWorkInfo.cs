using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.NursesAlgoritm.Items
{
    /// <summary>
    /// Contains information about workplace required for algiritm to work, also stores information about physical characteristics of workplace
    /// </summary>
    public static class WardWorkInfo
    {
        public static int WorkCycleLenght = 16; //days
        public static int Available_Workers = 16;
        public static int Workers_RequiredforDayshift = 4; //5max 3understaffed
        public static int Workers_RequiredforNightshift = 3; //4max 2understaffed
    }
}
