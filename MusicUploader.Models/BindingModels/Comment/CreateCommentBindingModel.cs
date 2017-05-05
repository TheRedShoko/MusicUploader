using System.ComponentModel.DataAnnotations;

namespace MusicUploader.Models.BindingModels.Comment
{
    public class CreateCommentBindingModel
    {
        [Required]
        public int SongId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
