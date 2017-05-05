namespace MusicUploader.Models.ViewModels.Song
{
    public class SearchSongViewModel
    {
        public string SearchFor { get; set; }
        public bool MatchWithSongName { get; set; }
        public bool MatchWithUploaderName { get; set; }
        public bool MatchWithUploaderUserName { get; set; }
    }
}
