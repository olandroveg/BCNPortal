namespace BCNPortal.Models
{
    public class APImapping
    {
        public Guid Id { get; set; }
        public Guid NFId { get; set; }
        public NFmapping NF { get; set; }
        public string ServiceName { get; set; }
        public string ServiceApi { get; set; }
    }
}
