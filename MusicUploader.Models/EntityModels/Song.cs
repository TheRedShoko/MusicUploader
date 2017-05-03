using System;
using System.Collections;
using System.Collections.Generic;

namespace MusicUploader.Models.EntityModels
{
    public class Song
    {
        public int Id { get; set; }
        public string SongName { get; set; }
        public string AudioFileName { get; set; }
        public string ImageCoverFile { get; set; }
        public DateTime UploadDate { get; set; }
        public virtual ApplicationUser Uploader { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
        public virtual IEnumerable<Playlist> Playlists { get; set; }
    }
}
