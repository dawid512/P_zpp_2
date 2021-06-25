using CashierAlgorithm.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierAlgorithm.Algorithms.FourBrigadeSystem
{
    public class Team
    {
        public int TeamNumber { get; set; }
        public List<Workers> TeamMembers { get; set; }
        public Team(int TeamNumber, List<Workers> TeamMembers)
        {
            this.TeamNumber = TeamNumber;
            this.TeamMembers = TeamMembers;
        }
    }
}
