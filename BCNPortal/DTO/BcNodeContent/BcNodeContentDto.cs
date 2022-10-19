using System;
using BCNPortal.DTO;

namespace BCNPortal.Dto.BcNodeContent
{
    public class BcNodeContentDto: BaseDTO
    {
        public Guid BcNodeId { get; set; }
        public Guid ContentId { get; set; }
        public Guid ServiceId { get; set; }
        public string SourceLocation { get; set; }
        public string Service { get; set; }
        public float Bitrate { get; set; }
        public float Size { get; set; }
    }
}
