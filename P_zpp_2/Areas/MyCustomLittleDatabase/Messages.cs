using P_zpp_2.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P_zpp_2.Data
{
    public class Messages
    {
        [Key]
        public int MessageId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser SenderId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ReciverId { get; set; }
        public string MessageContent { get; set; } //Json, JSON, JSON!
        public bool isRead { get; set; }
    }
}