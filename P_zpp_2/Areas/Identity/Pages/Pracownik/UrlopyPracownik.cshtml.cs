using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using P_zpp_2.Data;
using P_zpp_2.Models;


namespace P_zpp_2.Areas.Identity.Pages.Pracownik
{
    public class UrlopyPracownikModel : PageModel
    {
        public IActionResult OnGet()
        {
            List<int> messageIds = new List<int>();
            List<Messages> messages = new List<Messages>();
            List<string> communicates = new List<string>();
            using (var db = new P_zpp_2DbContext())
            {
                string userId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == userId);
                messages = db.messages.Where(x => x.Sender == currentUser).ToList();
                foreach(var item in messages)
                {
                    messageIds.Add(item.MessageId);
                    communicates.Add(item.MessageContent);
                }
            }
            return Page();

        }

        public IActionResult OnPost()
        {
            using (var db = new P_zpp_2DbContext())
            {
                Messages message = new Messages();
                ApplicationUser user = (ApplicationUser)db.Users.Where(x => x.Id == User.Identity.GetUserId());
                message.Sender = user;
                //message.Reciver = 
                message.MessageContent = string.Format($"Pracownik {0} {1}, na stanowisku {2} prosi o urlop dnia {3}, o godzinie {4}, z powodu {5}."
                    ,user.FirstName,user.LastName,user.Rola,0,0,null);
                message.isRead = false;
                db.messages.Add(message);
                db.SaveChanges();
            }
            return Page();
        }

    }
}
