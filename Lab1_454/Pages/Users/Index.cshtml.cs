using Lab1_454.Pages.Data_Classes;
using Lab1_454.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1_454.Pages.Users
{
    public class UsersModel : PageModel
    {
        public List<User> UserRecord { get; set; }

        [BindProperty]
        public int SelectedUser { get; set; }

        public List<Meeting> Meetings { get; set; }
        public List<Conference> Conferences { get; set; }

        public void LoadMeetingsAndConferences(int userId)
        {
            Meetings = DBClass.GetMeetingsForUser(userId);
            Conferences = DBClass.GetConferencesForUser(userId);
        }


        public UsersModel()
        {
            UserRecord = new List<User>();
        }
        public void OnGet()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                ViewData["LoginError"] = "You must login to access that page!";
                RedirectToPage("/Index");
            }
            else
            {
                ViewData["LoginMessage"] = "Hello, " + HttpContext.Session.GetString("Username");
            } 

            UserRecord.Clear(); 

            string username = HttpContext.Session.GetString("Username");

            User user = DBClass.GetUserByUsername(username);

            if (user != null)
            {
                UserRecord.Add(new User
                {
                    UserID = user.UserID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserType = user.UserType
                });

                LoadMeetingsAndConferences(user.UserID);
            }
        }

        public void OnPost()
        {
            UserRecord.Clear(); 

            if (SelectedUser > 0)
            {
                LoadMeetingsAndConferences(SelectedUser);
            }

            SqlDataReader productReader = DBClass.UserReader();

            while (productReader.Read())
            {
                UserRecord.Add(new User
                {
                    UserID = Int32.Parse(productReader["UserID"].ToString()),
                    FirstName = productReader["FirstName"].ToString(),
                    LastName = productReader["LastName"].ToString(),
                    UserType = productReader["UserType"].ToString()
                });
            }

            DBClass.Lab1DBConn.Close();
        }


        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index"); 
        }


    }
}
