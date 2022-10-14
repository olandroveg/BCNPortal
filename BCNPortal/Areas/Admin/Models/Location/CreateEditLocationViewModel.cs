using System;
using System.ComponentModel.DataAnnotations;
using CoreNetwork.DTO.Location;
using CoreNetwork.Utils;

namespace CoreNetwork.Areas.Admin.Models.Location
{
    public class CreateEditLocationViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Latitude")]
        public string Latitude { get; set; }
        public string locationId { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Longitude")]
        public string Longitude { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Location")]
        public string Location { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "State")]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public LocationDto ModelToDto()
        {
            return new LocationDto
            {
                Latitude = Normalize.InputString(Latitude) ,
                Longitude = Normalize.InputString(Longitude),
                Name = Location,
                State = Description,
                Country = Country,
                Id = locationId == null ? Guid.Empty : Guid.Parse(locationId)
            };
        }
    }
}

