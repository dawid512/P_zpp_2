using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.Models.MyCustomLittleDatabase
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }

        public EventModel(int id, string title, string startDate)
        {
            Id = id;
            Title = title;
            StartDate = startDate;
        }
    }
}
