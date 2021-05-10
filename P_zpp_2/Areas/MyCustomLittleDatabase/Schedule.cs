namespace P_zpp_2.Data
{
    public class Schedule
    {
        public int id { get; set; }
        public string scheduleName { get; set; }
        public string jsonfilewithschedule_staff_locaton { get; set; }
        public string jsonfilewithschedule_locaton { get; set; }

        public Schedule(string scheduleName, string jsonfilewithschedule_staff_locaton, string jsonfilewithschedule_locaton)
        {
            this.scheduleName = scheduleName;
            this.jsonfilewithschedule_staff_locaton = jsonfilewithschedule_staff_locaton;
            this.jsonfilewithschedule_locaton = jsonfilewithschedule_locaton;
        }
    }
}