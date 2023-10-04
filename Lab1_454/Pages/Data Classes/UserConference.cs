namespace Lab1_454.Pages.Data_Classes
{
    public class UserConference
    {
        public int UserID { get; set; }

        public int ConferenceID { get; set; }

        public static implicit operator int(UserConference v)
        {
            throw new NotImplementedException();
        }
    }
}
