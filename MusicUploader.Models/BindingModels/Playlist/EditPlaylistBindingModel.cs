using System.ComponentModel.DataAnnotations;

namespace MusicUploader.Models.BindingModels.Playlist
{
    public class EditPlaylistBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
