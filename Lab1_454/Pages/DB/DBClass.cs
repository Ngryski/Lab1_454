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
            SqlConnection connection = new SqlConnection(Lab1DBConnString);

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = connection;
            cmdProductRead.CommandText = "SELECT * FROM [USER]";
            connection.Open();

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
            String sqlQuery = "INSERT INTO [USER] (FirstName, LastName, UserType) VALUES (@FirstName, @LastName, @UserType)";

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab1DBConn;
            cmdProductRead.Connection.ConnectionString = Lab1DBConnString;
            cmdProductRead.CommandText = sqlQuery;

            cmdProductRead.Parameters.AddWithValue("@FirstName", u.FirstName);
            cmdProductRead.Parameters.AddWithValue("@LastName", u.LastName);
            cmdProductRead.Parameters.AddWithValue("@UserType", u.UserType);

            cmdProductRead.Connection.Open();

            try
            {
                cmdProductRead.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                cmdProductRead.Connection.Close();
            }
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

        public static List<Conference> GetConference()
        {
            List<Conference> conferences = new List<Conference>();

            using (SqlConnection connection = new SqlConnection(Lab1DBConnString))
            {
                connection.Open();

                SqlCommand cmdConferenceRead = new SqlCommand();
                cmdConferenceRead.Connection = connection;
                cmdConferenceRead.CommandText = "SELECT * FROM [CONFERENCE]";

                using (SqlDataReader tempReader = cmdConferenceRead.ExecuteReader())
                {
                    while (tempReader.Read())
                    {
                        conferences.Add(new Conference
                        {
                            ConferenceID = Convert.ToInt32(tempReader["ConferenceID"]),
                            EventName = tempReader["EventName"].ToString(),
                            StartDate = tempReader["StartDate"].ToString(),
                            EndDate = tempReader["EndDate"].ToString(),
                            LocationID = Convert.ToInt32(tempReader["LocationID"])
                        });
                    }
                }
            }

            return conferences;
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

        public static SqlDataReader RoomReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab1DBConn;
            cmdProductRead.Connection.ConnectionString = Lab1DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM [ROOM]";
            cmdProductRead.Connection.Open();
            // Use ExecuteReader for SELECT Query. If trying to Insert, Update, or Delete use ExecuteNonQuery. If result is going to be a single number use ExecuteScalar(Sumation Functions)
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }

        public static void UpdateUserConferences(int userID, List<int> updatedConferenceIDs)
        {
            using (SqlConnection Lab1DBConn = new SqlConnection(Lab1DBConnString))
            {
                Lab1DBConn.Open();

                // First, clear the user's existing conferences
                SqlCommand clearConferencesCmd = new SqlCommand();
                clearConferencesCmd.Connection = Lab1DBConn;
                clearConferencesCmd.CommandText = $"DELETE FROM [USERCONFERENCE] WHERE UserID = {userID}";
                clearConferencesCmd.ExecuteNonQuery();

                // Then, add the updated conferences
                foreach (var conferenceID in updatedConferenceIDs)
                {
                    SqlCommand addConferenceCmd = new SqlCommand();
                    addConferenceCmd.Connection = Lab1DBConn;
                    addConferenceCmd.CommandText = $"INSERT INTO [USERCONFERENCE] (UserID, ConferenceID) VALUES ({userID}, {conferenceID})";
                    addConferenceCmd.ExecuteNonQuery();
                }
            }
        }


        public static int LoginQuery(string loginQuery)
        {
            // This method expects to receive an SQL SELECT
            // query that uses the COUNT command.

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = Lab1DBConn;
            cmdLogin.Connection.ConnectionString = Lab1DBConnString;
            cmdLogin.CommandText = loginQuery;
            cmdLogin.Connection.Open();

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            int rowCount = (int)cmdLogin.ExecuteScalar();

            return rowCount;
        }

        public static int SecureLogin(string Username, string Password)
        {
            string loginQuery = "SELECT COUNT(*) FROM [USER] where Username = @Username and Password = @Password";

            using (SqlConnection connection = new SqlConnection(Lab1DBConnString))
            {
                connection.Open();

                SqlCommand cmdLogin = new SqlCommand(loginQuery, connection);

                cmdLogin.Parameters.AddWithValue("@Username", Username);
                cmdLogin.Parameters.AddWithValue("@Password", Password);

                int rowCount = (int)cmdLogin.ExecuteScalar();

                return rowCount;
            }
        }



        public static User GetUserByUsername(string username)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(Lab1DBConnString))
            {
                connection.Open();

                string query = "SELECT * FROM [USER] WHERE Username = @username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                Username = reader["Username"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                UserType = reader["UserType"].ToString()
                            };
                        }
                    }
                }
            }

            return user;
        }

        public static List<Meeting> GetMeetingsByConference(int conferenceID)
        {
            List<Meeting> meetings = new List<Meeting>();

            using (SqlConnection connection = new SqlConnection(Lab1DBConnString))
            {
                connection.Open();

                string query = "SELECT * FROM MEETING WHERE ConferenceID = @ConferenceID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ConferenceID", conferenceID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Meeting meeting = new Meeting
                            {
                                MeetingID = Convert.ToInt32(reader["MeetingID"]),
                                MeetingName = reader["MeetingName"].ToString(),
                                StartTime = reader["StartTime"].ToString(),
                                EndTime = reader["EndTime"].ToString(),
                                ConferenceID = Convert.ToInt32(reader["ConferenceID"]),
                                RoomID = Convert.ToInt32(reader["RoomID"])
                            };
                            meetings.Add(meeting);
                        }
                    }
                }
            }

            return meetings;
        }




        public static int GetCurrentUserID(string username)
        {
            using (SqlConnection connection = new SqlConnection(Lab1DBConnString))
            {
                connection.Open();

                string query = "SELECT UserID FROM [USER] WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    object result = command.ExecuteScalar();
                    return (result == null) ? -1 : Convert.ToInt32(result);
                }
            }
        }

        public static bool IsUserSignedUpForMeeting(int userID, int meetingID)
        {
            using (SqlConnection connection = new SqlConnection(Lab1DBConnString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM USERMEETING WHERE UserID = @UserID AND MeetingID = @MeetingID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@MeetingID", meetingID);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public static void SignUpUserForMeeting(int userID, int meetingID)
        {
            using (SqlConnection connection = new SqlConnection(Lab1DBConnString))
            {
                connection.Open();

                string query = "INSERT INTO USERMEETING (UserID, MeetingID) VALUES (@UserID, @MeetingID)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@MeetingID", meetingID);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
