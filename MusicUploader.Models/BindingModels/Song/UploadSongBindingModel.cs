using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MusicUploader.Models.BindingModels.Song
{
    public class UploadSongBindingModel
    {
        [Required]
        [Display(Name = "Song name")]
        public string SongName { get; set; }
        [Required]
        [Display(Name = "Song genre")]
        public int GenreId { get; set; }

        [Required]
        [Display(Name = "Audio file")]
        public HttpPostedFileBase AudioFile { get; set; }

        [Display(Name = "Cover file")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
