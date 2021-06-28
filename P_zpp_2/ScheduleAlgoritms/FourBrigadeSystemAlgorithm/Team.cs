using CashierAlgorithm.Database;
using P_zpp_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.FourBrigadeSystemAlgorithm
{
    /// <summary>
    /// Class for creating Team object.
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Number of team.
        /// </summary>
        public int TeamNumber { get; set; }
        /// <summary>
        /// List of team members.
        /// </summary>
        public List<ApplicationUser> TeamMembers { get; set; }
        public Team(int TeamNumber, List<ApplicationUser> TeamMembers)
        {
            this.TeamNumber = TeamNumber;
            this.TeamMembers = TeamMembers;
        }
    }
}
