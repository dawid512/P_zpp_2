using System;
using System.ComponentModel.DataAnnotations;

namespace testyBD.CleverWorkTimeDatabase
{
    public class Schedule
    {
        [Key]
        public int id { get; set; }
        public object mychedule { get; set; }
       
        [Obsolete]
        public Schedule()
        {
        }

        public Schedule(object mychedule) 
        { 
            this.mychedule = mychedule; 
        }

    }
}