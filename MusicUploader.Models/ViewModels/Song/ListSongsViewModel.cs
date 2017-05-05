using System.Collections.Generic;

namespace MusicUploader.Models.ViewModels.Song
{
    public class ListSongsViewModel
    {
        public IEnumerable<EntityModels.Song> Songs { get; set; }
    }
}
