using Lab1_454.Pages.Data_Classes;
using Lab1_454.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1_454.Pages.Users
{
    public class AddUserModel : PageModel
    {
        [BindProperty]
        public User NewUser { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            DBClass.InsertUser(NewUser);

            DBClass.Lab1DBConn.Close();

            return RedirectToPage("Index");
        }

        public void OnPostPopulateFields()
        {
            NewUser.FirstName = "Test";
            NewUser.LastName = "Population";
            NewUser.UserType = "Attendee";
        }

        public void OnPostClearFields()
        {
            NewUser.FirstName = string.Empty;
            NewUser.LastName = string.Empty;
            NewUser.UserType = string.Empty;
        }
    }
}






