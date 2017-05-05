using System;

namespace MusicUploader.Models.EntityModels
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Song Song { get; set; }
    }
}
