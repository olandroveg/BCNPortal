using System;
namespace BCNPortal.DTO.Location
{
    public class LocationDto : BaseDTO
    {
        public string State { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
