using System;
using System.Collections.Generic;

namespace buttoncheckDevAPI.Models
{
    public partial class Characters
    {
        public Characters()
        {
            VideosP1CharacterNavigation = new HashSet<Videos>();
            VideosP2CharacterNavigation = new HashSet<Videos>();
            VideosWinnerCharacterNavigation = new HashSet<Videos>();
        }

        public int CharacterId { get; set; }
        public string CharacterName { get; set; }

        public virtual ICollection<Videos> VideosP1CharacterNavigation { get; set; }
        public virtual ICollection<Videos> VideosP2CharacterNavigation { get; set; }
        public virtual ICollection<Videos> VideosWinnerCharacterNavigation { get; set; }
    }
}
