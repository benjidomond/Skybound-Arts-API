using System;
using System.Collections.Generic;

namespace buttoncheckDevAPI.Models
{
    public partial class Videos
    {
        public Videos()
        {
            MapVideoTag = new HashSet<MapVideoTag>();
        }

        public int VideoId { get; set; }
        public string EventName { get; set; }
        public string P1Character { get; set; }
        public string P2Character { get; set; }
        public string WinnerCharacter { get; set; }
        public string P1Player { get; set; }
        public string P2Player { get; set; }
        public string WinnerPlayer { get; set; }
        public string VideoLink { get; set; }

        public virtual Characters P1CharacterNavigation { get; set; }
        public virtual Characters P2CharacterNavigation { get; set; }
        public virtual Characters WinnerCharacterNavigation { get; set; }
        public virtual ICollection<MapVideoTag> MapVideoTag { get; set; }
    }
}
