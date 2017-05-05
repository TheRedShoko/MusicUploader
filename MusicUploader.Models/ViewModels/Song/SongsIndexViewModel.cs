using System.Collections.Generic;

namespace MusicUploader.Models.ViewModels.Song
{
    public class SongsIndexViewModel
    {
        public IEnumerable<EntityModels.Genre> Genres { get; set; }
    }
}
