using P_zpp_2.Data;
using P_zpp_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.Areas.MyCustomFunctions
{
    public class MessageAdder
    {
        public void CreateMessage(ApplicationUser sender, ApplicationUser receiver, string messageContent)
        {
            Messages newMessage = new Messages();
            newMessage.Sender = sender;
            newMessage.Reciver = receiver;
            newMessage.MessageContent = messageContent;
            newMessage.isRead = false;
            SendMessageToDatabase(newMessage);
        }

        public void SendMessageToDatabase(Messages message)
        {
            using (var db = new P_zpp_2DbContext())
            {
                db.messages.Add(message);
            }
        }
    }
}
