using System;
using System.Collections.Generic;

namespace P_zpp_2.ScheduleAlgoritms.NursesAlgoritm.Items
{
    /// <summary>
    /// A supplementary class for easier access to information required for alghoritm to work properly
    /// </summary>
    public class Nurse
    {
        public string Id { get; set; }
        public string name { get; set; }
        public int DayshiftsCompleted { get; set; }
        public int NightshitCompleted { get; set; }
        public int DaysOff { get; set; }
        public bool LastShift_night { get; set; }
        public List<DateTime> AprovedLeave { get; set; }

        public Nurse(string id, string name, int dayshiftsCompleted, int nightshitCompleted, int daysOff, List<DateTime> aprovedLeave)
        {
            Id = id;
            this.name = name;
            DayshiftsCompleted = dayshiftsCompleted;
            NightshitCompleted = nightshitCompleted;
            DaysOff = daysOff;
            LastShift_night = false;
            AprovedLeave = aprovedLeave;
        }



        /*
        public Nurse(string name)
        {
            this.name = name;
            DayshiftsCompleted = 0;
            NightshitCompleted = 0;
            AprovedLeave = new List<DateTime>();
            DaysOff = 0;
            LastShift_night = false;
        }
        public void Print()
        {
            System.Console.WriteLine("\n" + this.name + " days: " + this.DayshiftsCompleted + " nights: " + this.NightshitCompleted + " free: " + this.DaysOff);
        }

        internal void Reset()
        {
            DayshiftsCompleted = 0;
            NightshitCompleted = 0;
            DaysOff = 0;
        }
        */
    }
}