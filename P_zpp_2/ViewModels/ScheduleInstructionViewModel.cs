using P_zpp_2.Models.MyCustomLittleDatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace P_zpp_2.ViewModels
{
    public class ScheduleInstructionViewModel : ScheduleInstructions
    {
        public ScheduleInstructions scheduleInstructions { get; set; }

        public string startOne { get; set; }
        public string endOne { get; set; }
        public string startTwo { get; set; }
        public string endTwo { get; set; }
        public string? startThree { get; set; }
        public string? endThree { get; set; }
        public string? startFour { get; set; }
        public string? endFour { get; set; }
        public string? startFive { get; set; }
        public string? endFive { get; set; }
        
        
        [Display(Name = "długość zmiany w dniach")]
        public int? długość_zmiany_w_dniach { get; set; }

        [Display(Name = "Ilość kas")]
        public int? ilość_kas { get; set; }


    }
}
