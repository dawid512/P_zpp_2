using P_zpp_2.Areas.Identity.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace P_zpp_2.Data
{
    public class Messages
    {
        [Key]
        public int MessageId { get; set; }
        //[ForeignKey("UserId")]
        public ApplicationUser Sender { get; set; }
       
        //[ForeignKey("UserId")]
        public ApplicationUser Reciver { get; set; }
        public string MessageContent { get; set; }
        public bool isRead { get; set; }

        public void CreateMessage(ApplicationUser sender, ApplicationUser receiver, string messageContent)
        {
            Messages newMessage = new Messages();
            newMessage.Sender = sender;
            newMessage.Reciver = receiver;
            newMessage.MessageContent = messageContent;
            isRead = false;
            SendMessageToDatabase(newMessage);
        }

        public void SendMessageToDatabase(Messages message)
        {
            using (var db = new P_zpp_2DbContext())
            {
                db.messages.Add(message);
            }
        }

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
            foreach(var item in messages)
            {
                communicates.Add(item.MessageContent);
            }
            return communicates;
        }
    }
}