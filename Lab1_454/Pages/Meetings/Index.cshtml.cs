using Lab1_454.Pages.Data_Classes;
using Lab1_454.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System;

namespace Lab1_454.Pages.Meetings
{
    public class IndexModel : PageModel
    {
        public List<Meeting> Meetings { get; set; } = new List<Meeting>();
        public void OnGet()
        {
            SqlDataReader productReader = DBClass.MeetingReader();

            while (productReader.Read())
            {
                Meetings.Add(new Meeting
                {
                    MeetingID = Int32.Parse(productReader["MeetingID"].ToString()),
                    MeetingName = productReader["MeetingName"].ToString(),
                    StartTime = productReader["StartTime"].ToString(),
                    EndTime = productReader["EndTime"].ToString(),
                    ConferenceID = Int32.Parse(productReader["ConferenceID"].ToString()),
                    RoomID = Int32.Parse(productReader["RoomID"].ToString())
                });
            }

            productReader.Close(); // Close the reader when done with it
            DBClass.Lab1DBConn.Close();
        }
    }
}

