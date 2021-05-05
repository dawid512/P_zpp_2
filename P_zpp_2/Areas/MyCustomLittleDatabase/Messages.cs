using System.ComponentModel.DataAnnotations;

namespace P_zpp_2.Data
{
    public class Messages
    {
        [Key]
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int ReciverId { get; set; }
        public string MessageContent { get; set; } //Json, JSON, JSON!
        public bool isRead { get; set; }
    }
}