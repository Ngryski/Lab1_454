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

        public void OnPost()
        {
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
    }
}
