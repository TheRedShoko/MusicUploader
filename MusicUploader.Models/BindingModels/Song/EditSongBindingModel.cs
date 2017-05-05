using System.ComponentModel.DataAnnotations;

namespace MusicUploader.Models.BindingModels.Song
{
    public class EditSongBindingModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Song name")]
        public string SongName { get; set; }
        [Required]
        [Display(Name = "Song name")]
        public int GenreId { get; set; }
    }
}
