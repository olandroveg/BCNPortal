using BCNPortal.Dto.Content;
using BCNPortal.Dto.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BCNPortal.Areas.Admin.Models.Content
{
    public class CreateEditContentViewModel
    {
        public string Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Source")]
        public string SourceLocation { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Service")]
        public Guid SelectedServiceId { get; set; }

        public IEnumerable<ServiceDto> Services { get; set; }
        public ContentDto ConvertToDto()
        {
            return new ContentDto
            {
                Id = Id != null ? Guid.Parse(Id) : Guid.Empty,
                SourceLocation = SourceLocation,
                ServicesId = SelectedServiceId,
                services = Services,
                Name = Name
            };


        }
    }
}
