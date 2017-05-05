using System.ComponentModel.DataAnnotations;

namespace MusicUploader.Models.ViewModels.Playlist
{
    public class ViewPlaylistViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Playlist name")]
        public string Name { get; set; }
        [Display(Name = "Playlist owner")]
        public string OwnerFullName { get; set; }
        public string OwnerUserName { get; set; }
        public string OwnerId { get; set; }
    }
}
