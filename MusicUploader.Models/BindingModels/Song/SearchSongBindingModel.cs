using System.ComponentModel.DataAnnotations;

namespace MusicUploader.Models.BindingModels.Song
{
    public class SearchSongBindingModel
    {
        [Required]
        public string SearchFor { get; set; }
        public bool MatchWithSongName { get; set; }
        public bool MatchWithUploaderName { get; set; }
        public bool MatchWithUploaderUserName { get; set; }
    }
}
