namespace BCNPortal.Models
{
    public class NFmapping
    {
        public Guid Id { get; set; }
        public Guid NFId { get; set; }
        public string NF { get; set; }
        public string NFbaseAdd { get; set; }
        public string Version { get; set; }
        public bool Available { get; set; }
        public int Priority { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<APImapping> Apis { get; set; }
}
}
