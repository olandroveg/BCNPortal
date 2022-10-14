﻿using CoreNetwork.Dto.BcNodeContent;
using CoreNetwork.Dto.Service;
using CoreNetwork.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreNetwork.Areas.Admin.Models.BcNodeContent
{
    public class BcNodeContentViewModel
    {
        public string Id { get; set; }
        public string ContentId { get; set; }
        public string BcNodeId { get; set; }
        public string Service { get; set; }
        public int OptionTab { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Source")]
        public string SourceLocation { get; set; }

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
                Id = Guid.Parse(Id),
                BcNodeId = Guid.Parse(BcNodeId),
                ContentId = Guid.Parse(ContentId),
                Bitrate = float.Parse( Normalize.InputString(Bitrate)),
                Size = float.Parse(Normalize.InputString(Size))

            };
        }

    }
}
