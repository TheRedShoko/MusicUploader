using System.ComponentModel.DataAnnotations;

namespace MusicUploader.Models.BindingModels.Playlist
{
    public class EditPlaylistViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
