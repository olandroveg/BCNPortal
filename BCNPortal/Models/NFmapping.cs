namespace BCNPortal.Models
{
    public class NFmapping
    {
        public Guid Id { get; set; }
        public string NF { get; set; }
        public string Version { get; set; }
        public bool Available { get; set; }
        public int Priority { get; set; }
        public ICollection<APImapping> Apis { get; set; }
}
}
