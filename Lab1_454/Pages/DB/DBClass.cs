using Lab1_454.Pages.Data_Classes;
using System.Data.SqlClient;

namespace Lab1_454.Pages.DB
{
    public class DBClass
    {

        // Connection object at the class level
        public static SqlConnection Lab1DBConn = new SqlConnection();
        // Connection String
        public static readonly String Lab1DBConnString = "Server = Localhost;Database = Lab1;Trusted_Connection = True"; //";TrustServerCertificate = True"
        // Connection Methods
        public static SqlDataReader UserReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab1DBConn;
            cmdProductRead.Connection.ConnectionString = Lab1DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM [USER]";
            cmdProductRead.Connection.Open();
            // Use ExecuteReader for SELECT Query. If trying to Insert, Update, or Delete use ExecuteNonQuery. If result is going to be a single number use ExecuteScalar(Sumation Functions)
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }



        public static SqlDataReader SingleUserReader(int UserID)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1DBConnString;
            cmdProductRead.CommandText = "Select * FROM [USER] WHERE UserID = " + UserID;
            cmdProductRead.Connection.Open();
            SqlDataReader tempRead = cmdProductRead.ExecuteReader();

            return tempRead;
        }


        public static void InsertUser(User u)
        {
            String sqlQuery = "INSERT INTO [USER] (FirstName, LastName, UserType) VALUES ('";
            sqlQuery += u.FirstName + "',";
            sqlQuery += u.LastName + ",'";
            sqlQuery += u.UserType + "')";

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab1DBConn;
            cmdProductRead.Connection.ConnectionString = Lab1DBConnString;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();

            cmdProductRead.ExecuteNonQuery();

        }


        public static void UpdateUser(User p)
        {

            string sqlQuery = "UPDATE [USER] SET ";

            sqlQuery += "FirstName='" + p.FirstName + "',";
            sqlQuery += "LastName='" + p.LastName + "',";
            sqlQuery += "UserType='" + p.UserType + "' WHERE UserID=" + p.UserID;

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab1DBConnString;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }

        public static List<Meeting> GetMeetingsForUser(int userId)
        {
            List<Meeting> meetings = new List<Meeting>();

            using (SqlConnection Lab1DBConn = new SqlConnection(Lab1DBConnString))
            {
                Lab1DBConn.Open();

                SqlCommand cmdMeetingRead = new SqlCommand();
                cmdMeetingRead.Connection = Lab1DBConn;
                cmdMeetingRead.CommandText = $"SELECT M.* FROM [MEETING] M JOIN [USERMEETING] UM ON M.MeetingID = UM.MeetingID WHERE UM.UserID = {userId}";

                SqlDataReader tempReader = cmdMeetingRead.ExecuteReader();

                while (tempReader.Read())
                {
                    meetings.Add(new Meeting
                    {
                        MeetingID = Int32.Parse(tempReader["MeetingID"].ToString()),
                        MeetingName = tempReader["MeetingName"].ToString(),
                        StartTime = tempReader["StartTime"].ToString(),
                        EndTime = tempReader["EndTime"].ToString(),
                        ConferenceID = Int32.Parse(tempReader["ConferenceID"].ToString()),
                        RoomID = Int32.Parse(tempReader["RoomID"].ToString())
                    });
                }
            }

            return meetings;
        }

        public static List<Conference> GetConferencesForUser(int userId)
        {
            List<Conference> conferences = new List<Conference>();

            using (SqlConnection Lab1DBConn = new SqlConnection(Lab1DBConnString))
            {
                Lab1DBConn.Open();

                SqlCommand cmdConferenceRead = new SqlCommand();
                cmdConferenceRead.Connection = Lab1DBConn;
                cmdConferenceRead.CommandText = $"SELECT C.* FROM [CONFERENCE] C JOIN [USERCONFERENCE] UC ON C.ConferenceID = UC.ConferenceID WHERE UC.UserID = {userId}";

                SqlDataReader tempReader = cmdConferenceRead.ExecuteReader();

                while (tempReader.Read())
                {
                    conferences.Add(new Conference
                    {
                        ConferenceID = Int32.Parse(tempReader["ConferenceID"].ToString()),
                        EventName = tempReader["EventName"].ToString(),
                        StartDate = tempReader["StartDate"].ToString(),
                        EndDate = tempReader["EndDate"].ToString(),
                        LocationID = Int32.Parse(tempReader["LocationID"].ToString())
                    });
                }
            }

            return conferences;
        }



        public static SqlDataReader ConferenceReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab1DBConn;
            cmdProductRead.Connection.ConnectionString = Lab1DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM [CONFERENCE]";
            cmdProductRead.Connection.Open();
            // Use ExecuteReader for SELECT Query. If trying to Insert, Update, or Delete use ExecuteNonQuery. If result is going to be a single number use ExecuteScalar(Sumation Functions)
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }


        public static SqlDataReader MeetingReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab1DBConn;
            cmdProductRead.Connection.ConnectionString = Lab1DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM [Meeting]";
            cmdProductRead.Connection.Open();
            // Use ExecuteReader for SELECT Query. If trying to Insert, Update, or Delete use ExecuteNonQuery. If result is going to be a single number use ExecuteScalar(Sumation Functions)
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }
    }

}

