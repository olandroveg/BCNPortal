using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BCNPortal.Dto.Location;
using BCNPortal.DTO;
using BCNPortal.DTO.BcNode;
using BCNPortal.DTO.Location;

namespace BCNPortal.Areas.Admin.Models.BcNode
{
    public class CreateEditBcNodeViewModel
    {
        public string BcNodeId { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Controller bcNode")]
        public Guid SelectedBcNodeId { get; set; }
        public IEnumerable<BaseDTO> bcNodeList { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Latitude")]
        public string Latitude { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Longitude")]
        public string Longitude { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Group Number")]
        public string Group { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Location")]
        public Guid SelectedLocationId { get; set; }
        public IEnumerable<LocationListDto> Locations { get; set; }
        public Guid UserId { get; set; }
        public int OptionTab { get; set; }
        public BcNodeDto ModelToDto()
        {
            return new BcNodeDto
            {
                Name = Name,
                Description = Description,                
                Id = BcNodeId == null ? Guid.Empty : Guid.Parse(BcNodeId),
                PlaceId = SelectedLocationId,
                UserId = UserId,
                Group = Group,
                TopBcNode = SelectedBcNodeId
                
            };
        }
    }
}
