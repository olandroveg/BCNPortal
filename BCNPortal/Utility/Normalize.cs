namespace BCNPortal.Utility
{
    public static class Normalize
    {
        public static string InputString(string value)
        {
            if (value != null && value != string.Empty)
            {
                if (value.Contains("."))
                {
                    return value.Replace(".", ",");
                }
                return value;
            }
            return "0";
        }
    }
}
