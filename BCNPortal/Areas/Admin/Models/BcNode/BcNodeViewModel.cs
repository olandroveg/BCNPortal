using System;
using System.Collections.Generic;
using CoreNetwork.DTO.BcNode;

namespace CoreNetwork.Areas.Admin.Models.BcNode
{
    public class BcNodeViewModel
    {
        public IEnumerable<BcNodeDto> bcNodes { get; set; }
    }
}
