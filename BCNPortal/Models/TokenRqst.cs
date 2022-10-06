using System;
namespace BCNPortal.Models
{
    public class TokenRqst
    {
        
            public string username { get; set; }
            public string password { get; set; }

            public TokenRqst(string usernameIncom, string passwordIncom)
            {
                username = usernameIncom;
                password = passwordIncom;
            }
    
    }
}

