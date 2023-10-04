using Lab1_454.Pages.Data_Classes;
using Lab1_454.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1_454.Pages.Room
{
    public class IndexModel : PageModel
    {
        public List<Rooms> Room { get; set; } = new List<Rooms>();
        public void OnGet()
        {
            SqlDataReader productReader = DBClass.RoomReader();

            while (productReader.Read())
            {
                Room.Add(new Rooms
                {
                    RoomID = Int32.Parse(productReader["RoomID"].ToString()),
                    RoomName = productReader["RoomName"].ToString(),
                    Capacity = Int32.Parse(productReader["Capacity"].ToString()),
                    LocationID = Int32.Parse(productReader["LocationID"].ToString())
                });
            }

            productReader.Close();
            DBClass.Lab1DBConn.Close();
        }
    }
}
