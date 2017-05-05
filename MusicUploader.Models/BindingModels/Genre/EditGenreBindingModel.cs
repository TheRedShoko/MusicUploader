using System.ComponentModel.DataAnnotations;

namespace MusicUploader.Models.BindingModels.Genre
{
    public class EditGenreBindingModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Genre name")]
        public string Name { get; set; }
    }
}
