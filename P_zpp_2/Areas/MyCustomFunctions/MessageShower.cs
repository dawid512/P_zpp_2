using P_zpp_2.Areas.Identity.Data;
using P_zpp_2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.Areas.MyCustomFunctions
{
    public class MessageShower
    {
        public List<Messages> GetMessagesForReceiver(ApplicationUser ReceivedUser)
        {
            List<Messages> messages = new List<Messages>();
            using (var db = new P_zpp_2DbContext())
            {
                messages = db.messages.Where(x => x.Reciver == ReceivedUser).ToList();
            }
            return messages;
        }

        public List<string> ShowMessagesForReceiver(ApplicationUser ReceivedUser)
        {
            List<string> communicates = new List<string>();
            List<Messages> messages = GetMessagesForReceiver(ReceivedUser);
            foreach (var item in messages)
            {
                communicates.Add(item.MessageContent);
            }
            return communicates;
        }
    }
}
