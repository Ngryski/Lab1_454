using Lab1_454.Pages.Data_Classes;
using Lab1_454.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;

namespace Lab1_454.Pages.Users
{
    public class EditUserConferenceModel : PageModel
    {
        [BindProperty]
        public int UserID { get; set; }

        [BindProperty]
        public List<int> SelectedConferences { get; set; }

        public List<Conference> AvailableConferences { get; set; }

        public void OnGet(int userID)
        {
            
            AvailableConferences = DBClass.GetConference();

            
            SelectedConferences = DBClass.GetConferencesForUser(userID)
                .Select(c => c.ConferenceID)
                .ToList();
        }

        public IActionResult OnPost()
        {
            
            DBClass.UpdateUserConferences(UserID, SelectedConferences);

            
            return RedirectToPage("/Users/Index");
        }
    }
}





        
    

