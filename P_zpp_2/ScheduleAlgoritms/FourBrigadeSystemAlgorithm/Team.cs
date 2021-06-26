using CashierAlgorithm.Database;
using P_zpp_2.Models;
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
        public List<ApplicationUser> TeamMembers { get; set; }
        public Team(int TeamNumber, List<ApplicationUser> TeamMembers)
        {
            this.TeamNumber = TeamNumber;
            this.TeamMembers = TeamMembers;
        }
    }
}
