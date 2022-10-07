using System;
namespace BCNPortal.Models
{
    public class TokenRqst
    {
        
            public string User { get; set; }
            public string Password { get; set; }

            public TokenRqst(string usernameIncom, string passwordIncom)
            {
                User = usernameIncom;
                Password = passwordIncom;
            }
    
    }
}

