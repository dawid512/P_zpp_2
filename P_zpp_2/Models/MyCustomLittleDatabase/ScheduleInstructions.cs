using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.Models.MyCustomLittleDatabase
{
    /// <summary>
    /// Table for schedule instructions from database.
    /// </summary>
    public class ScheduleInstructions
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ListOfShistsInJSON { get; set; }
        public string CoordinatorId { get; set; }
        [ForeignKey("CoordinatorId")]
        public virtual ApplicationUser Coordinator { get; set; }

    }
}
