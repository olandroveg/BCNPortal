using System;
using System.Collections.Generic;
using BCNPortal.DTO.Location;

namespace BCNPortal.DTO.BcNode
{
    public class BcNodeDto : BaseDTO
    {
        public string Description { get; set; }
        public Guid PlaceId { get; set; }
        public Guid UserId { get; set; }
        public Guid TopBcNode { get; set; }
        public string Group { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }
}
