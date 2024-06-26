﻿using BCNPortal.Dto.BcNodeContent;
using BCNPortal.DTO;
using BCNPortal.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BCNPortal.Areas.Admin.Models.BcNodeContent
{
    public class AddBcNodeContentViewModel
    {
        public string Id { get; set; }
        public string BcNodeId { get; set; }
        public int OptionTab { get; set; }
        
        public IEnumerable<BaseDTO> Contents { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Contents")]
        public string SelectedContentId { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Bitrate")]
        public string Bitrate { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Size")]
        public string Size { get; set; }


        public BcNodeContentDto ConvertToDto()
        {
            return new BcNodeContentDto
            {
                Id = Id == null || Id == String.Empty ? Guid.Empty : Guid.Parse(Id),
                BcNodeId = Guid.Parse(BcNodeId),
                ContentId = Guid.Parse(SelectedContentId),
                Bitrate = float.Parse(Normalize.InputString(Bitrate)),
                Size = float.Parse(Normalize.InputString(Size))

            };
        }
    }
}
