using P_zpp_2.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P_zpp_2.Data
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public string ScheduleName { get; set; }
        public DateTime LastScheduleDay { get; set; }
        public string ScheduleInJSON { get; set; }
        public string? HangingDaysInJSON { get; set; }
        public string ScheduleInstructions { get; set; }
        public string CoordinatorId { get; set; }
        [ForeignKey("CoordinatorId")]
        public virtual ApplicationUser Coordinaor { get; set; }


        public Schedule(string ScheduleName, string ScheduleInJSON, string ScheduleInstructions)
        {
            this.ScheduleName = ScheduleName;
            this.ScheduleInJSON = ScheduleInJSON;
            this.ScheduleInstructions = ScheduleInstructions;
        }

        public Schedule(string CoordinatorId, DateTime LastScheduleDay, string ScheduleInJSON, int Bs)
        {
            this.CoordinatorId = CoordinatorId;
            this.LastScheduleDay = LastScheduleDay;
            this.ScheduleInJSON = ScheduleInJSON;
        }
    }
}