namespace BCNPortal.Utility
{
    public static class HttpResponseCode
    {
        public static string GetMessageFromStatus(System.Net.HttpStatusCode code)
        {
            switch (code)
            {
                case System.Net.HttpStatusCode.BadRequest:
                    return "BadRequest";
                case System.Net.HttpStatusCode.NotFound:
                    return "NotFound";
                case System.Net.HttpStatusCode.Forbidden:
                    return "Forbidden";
                case System.Net.HttpStatusCode.Unauthorized:
                    return "Unauthorized";
                    default:
                    return "error";

            }
               
        }
    }
}
