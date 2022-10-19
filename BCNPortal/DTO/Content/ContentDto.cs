using BCNPortal.Dto.Service;
using System;
using System.Collections.Generic;

namespace BCNPortal.Dto.Content
{
    public class ContentDto
    {
        public Guid Id { get; set; }
        public string SourceLocation { get; set; }
        public Guid ServicesId { get; set; }
        public IEnumerable<ServiceDto> services { get; set; }
        public string ServiceName { get; set; }
        public string Name { get; set; }
    }
}
