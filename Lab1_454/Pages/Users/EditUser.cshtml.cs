using Lab1_454.Pages.Data_Classes;
using Lab1_454.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1_454.Pages.Users
{
    public class EditUserModel : PageModel
    {

        [BindProperty]
        public User UserToUpdate { get; set; }

        public EditUserModel()
        {
            UserToUpdate = new User();
        }

        public void OnGet(int userid)
        { 
            SqlDataReader singleUser = DBClass.SingleUserReader(userid);

            while (singleUser.Read())
            {
                UserToUpdate.UserID = userid;
                UserToUpdate.FirstName = singleUser["FirstName"].ToString();
                UserToUpdate.LastName = singleUser["LastName"].ToString();
                UserToUpdate.UserType = singleUser["UserType"].ToString();
            }

            DBClass.Lab1DBConn.Close();
        }

        public IActionResult OnPost()
        {
            DBClass.UpdateUser(UserToUpdate);

            DBClass.Lab1DBConn.Close();

            return RedirectToPage("Index");
        }
    }
}