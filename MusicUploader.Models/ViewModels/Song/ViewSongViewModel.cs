namespace MusicUploader.Models.ViewModels.Song
{
    public class ViewSongViewModel
    {
        public int Id { get; set; }
        public string UploaderId { get; set; }
        public string SongName { get; set; }
        public string ImageFileName { get; set; }
        public string GenreName { get; set; }
    }
}
