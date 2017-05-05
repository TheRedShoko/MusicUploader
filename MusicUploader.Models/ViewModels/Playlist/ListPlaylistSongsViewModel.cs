using System.Collections.Generic;

namespace MusicUploader.Models.ViewModels.Playlist
{
    public class ListPlaylistSongsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public IEnumerable<EntityModels.Song> Songs { get; set; }
    }
}
