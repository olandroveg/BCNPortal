﻿using System;
namespace BCNPortal.Models
{
    public class Token
    {
        public Guid Id { get; set; }
        public string? Value { get; set; }
        public DateTime DateTime { get; set; }
        public Guid BcnUserId { get; set; }
        public Guid PortalUserId { get; set; }
    }
}

