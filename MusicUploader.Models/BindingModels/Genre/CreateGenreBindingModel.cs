using System.ComponentModel.DataAnnotations;

namespace MusicUploader.Models.BindingModels.Genre
{
    public class CreateGenreBindingModel
    {
        [Required]
        public string Name { get; set; }
    }
}
