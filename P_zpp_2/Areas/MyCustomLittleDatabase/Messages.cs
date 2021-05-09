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

    }
}