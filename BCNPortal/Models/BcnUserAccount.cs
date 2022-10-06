namespace BCNPortal.Models
{
    public class BcnUserAccount
    {
        public Guid Id { get; set; }
        public Guid PortalUserId { get; set; }
        public string BcnUsername { get; set; }
        public string BcnPassword { get; set; }

    }
}
