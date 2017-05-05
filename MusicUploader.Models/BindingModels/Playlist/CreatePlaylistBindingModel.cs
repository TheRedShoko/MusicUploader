using System.ComponentModel.DataAnnotations;

namespace MusicUploader.Models.BindingModels.Playlist
{
    public class CreatePlaylistBindingModel
    {
        [Required]
        public string Name { get; set; }
    }
}
