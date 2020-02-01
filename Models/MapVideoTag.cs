using System;
using System.Collections.Generic;

namespace buttoncheckDevAPI.Models
{
    public partial class MapVideoTag
    {
        public int MapId { get; set; }
        public int VideoId { get; set; }
        public int TagId { get; set; }

        public virtual VideoTags Tag { get; set; }
        public virtual Videos Video { get; set; }
    }
}
