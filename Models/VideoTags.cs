using System;
using System.Collections.Generic;

namespace buttoncheckDevAPI.Models
{
    public partial class VideoTags
    {
        public VideoTags()
        {
            MapVideoTag = new HashSet<MapVideoTag>();
        }

        public int TagId { get; set; }
        public string TagName { get; set; }

        public virtual ICollection<MapVideoTag> MapVideoTag { get; set; }
    }
}
