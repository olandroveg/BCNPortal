using BCNPortal.DTO.Location;

namespace BCNPortal.DTO.Portal
{
    public class PortalRegisterDto
    {
        public Guid Id { get; set; }
        public string PortalName { get; set; }
        public IncomeLocationDto Location { get; set; }
        public float BusyIndex { get; set; }
    }
}
