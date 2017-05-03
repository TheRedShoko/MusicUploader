using System.Collections.Generic;

namespace MusicUploader.Models.EntityModels
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Song> Songs { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}
