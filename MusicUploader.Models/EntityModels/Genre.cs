using System.Collections.Generic;

namespace MusicUploader.Models.EntityModels
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
