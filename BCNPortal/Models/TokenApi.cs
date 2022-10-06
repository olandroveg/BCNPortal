namespace BCNPortal.Models
{
    public class TokenApi
    {
        public string token { get; set; }
        public string status { get; set; }
        public string username { get; set; }
        public class TokenBody
        {
            public string username { get; set; }
            public string password { get; set; }
            public TokenBody (string usernameIncom, string passwordIncom)
            {
                username = usernameIncom;
                password = passwordIncom;
            }
        }
        
    }
}
