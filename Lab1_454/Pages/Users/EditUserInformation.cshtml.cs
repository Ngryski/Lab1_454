using Lab1_454.Pages.Data_Classes;
using Lab1_454.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1_454.Pages.Users
{
    public class EditUserInformationModel : PageModel
    {
        public List<Conference> Conferences { get; set; }
        public List<Meeting> Meetings { get; set; }

        [BindProperty]
        public List<int> SelectedConferences { get; set; }

        [BindProperty]
        public List<int> SelectedMeetings { get; set; }

        public void OnGet()
        {
            // Add logic to retrieve the current user's ID
            int currentUserId = DBClass.GetCurrentUserID(HttpContext.Session.GetString("Username"));

            // Get all conferences
            Conferences = DBClass.GetConference();
        }

        public void OnPost()
        {
            int currentUserId = DBClass.GetCurrentUserID(HttpContext.Session.GetString("Username"));

            if (SelectedConferences != null && SelectedConferences.Count > 0)
            {
                DBClass.UpdateUserConferences(currentUserId, SelectedConferences);
            }

            if (SelectedMeetings != null && SelectedMeetings.Count > 0)
            {
                foreach (var meetingID in SelectedMeetings)
                {
                    if (!DBClass.IsUserSignedUpForMeeting(currentUserId, meetingID))
                    {
                        DBClass.SignUpUserForMeeting(currentUserId, meetingID);
                    }
                }
            }

            // Redirect to Users Index page
            RedirectToPage("/Users/Index");
        }
    }
}
    
    



