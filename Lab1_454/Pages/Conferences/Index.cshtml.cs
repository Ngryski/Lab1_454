using Lab1_454.Pages.Data_Classes;
using Lab1_454.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1_454.Pages.Conferences
{
    public class IndexModel : PageModel
    {
        public List<Conference> Conferences { get; set; } = new List<Conference>();

        public void OnGet()
        {
            SqlDataReader productReader = DBClass.ConferenceReader();

            while (productReader.Read())
            {
                Conferences.Add(new Conference
                {
                    ConferenceID = Int32.Parse(productReader["ConferenceID"].ToString()),
                    EventName = productReader["EventName"].ToString(),
                    StartDate = productReader["StartDate"].ToString(),
                    EndDate = productReader["EndDate"].ToString(),
                    LocationID = Int32.Parse(productReader["LocationID"].ToString())
                });
            }

            productReader.Close(); 
            DBClass.Lab1DBConn.Close();
        }
    }
}
