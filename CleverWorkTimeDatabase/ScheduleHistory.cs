using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace testyBD.CleverWorkTimeDatabase
{
    public class ScheduleHistory
    {
        [Key]
        public int id { get; set; }
        public DateTime sevenDaysSince { get; set; }
        public string fileLocation { get; set; }

        public ScheduleHistory(DateTime sevenDaysSince, string fileLocation)
        {
            this.sevenDaysSince = sevenDaysSince;
            this.fileLocation = fileLocation;
        }
        [Obsolete]
        public ScheduleHistory()
        {
        }
    }
}
