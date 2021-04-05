using System;
using System.ComponentModel.DataAnnotations;

namespace testyBD.CleverWorkTimeDatabase
{
    public class Schedule
    {
        [Key]
        public int id { get; set; }
        public object myschedule { get; set; }
       
        [Obsolete]
        public Schedule()
        {
        }

        public Schedule(object myschedule) 
        { 
            this.myschedule = mychedule; 
        }

    }
}