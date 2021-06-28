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
        [MaxLength(1000000)]
        public string ScheduleInJSON { get; set; }
        public string? HangingDaysInJSON { get; set; }
        public string ScheduleInstructions { get; set; }
        public string ScheduleType { get; set; }
        public string CoordinatorId { get; set; }
        [ForeignKey("CoordinatorId")]
        public virtual ApplicationUser Coordinaor { get; set; }

        //FourBrigadeSystemConstructor
        public Schedule(string CoordinatorId, string ScheduleName, DateTime LastScheduleDay, string ScheduleInJSON, string? HangingDaysInJSON, string ScheduleType)
        {
            this.ScheduleName = ScheduleName;
            this.LastScheduleDay = LastScheduleDay;
            this.ScheduleInJSON = ScheduleInJSON;
            this.HangingDaysInJSON = HangingDaysInJSON;
            this.CoordinatorId = CoordinatorId;
            this.ScheduleType = ScheduleType;
        }

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