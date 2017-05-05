using System.ComponentModel.DataAnnotations;

namespace MusicUploader.Models.BindingModels.Comment
{
    public class EditCommentBindingModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
