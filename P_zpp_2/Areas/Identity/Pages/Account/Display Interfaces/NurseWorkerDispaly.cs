using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.Areas.Identity.Pages.Account.Display_Interfaces
{
    public class NurseWorkerDispaly : INurseWorkerDisplay
    {
        public string _UserId { get; set; }
        public string _JsonData { get; set; }

        public List<IEnumerable> Converter(string _JsonData, string _UserId)
        {
            throw new NotImplementedException();
        }
    }
}
