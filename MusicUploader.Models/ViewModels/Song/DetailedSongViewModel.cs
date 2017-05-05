using System;
using System.Collections.Generic;
using MusicUploader.Models.EntityModels;

namespace MusicUploader.Models.ViewModels.Song
{
    public class DetailedSongViewModel
    {
        public int Id { get; set; }
        public string SongName { get; set; }
        public string AudioFileName { get; set; }
        public string ImageFileName { get; set; }
        public DateTime UploadDate { get; set; }
        public virtual ApplicationUser Uploader { get; set; }
        public virtual ICollection<EntityModels.Comment> Comments { get; set; }
    }
}
