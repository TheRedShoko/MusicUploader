using System.Collections.Generic;
using System.Web;
using MusicUploader.Models.BindingModels.Song;
using MusicUploader.Models.EntityModels;

namespace MusicUploader.Services.Interfaces
{
    public interface ISongService
    {
        IEnumerable<Song> GetSongsByGenreId(int genreId);
        Song FindSongById(int? id);
        string UploadAudioFile(HttpPostedFileBase audioFile, string path);
        string UploadCoverFile(HttpPostedFileBase imageFile, string path);
        void AddSong(UploadSongBindingModel model, string audioFileName, string coverFileName);
        void SongModified(Song song, EditSongBindingModel model);
        void DeleteSong(Song song);
        IEnumerable<Genre> GetAvailableGenres();
        IEnumerable<Playlist> GetUserPlaylistsNotContaining(Song song);
        IEnumerable<Song> GetSongsNameContaining(string match);
        IEnumerable<Song> GetSongsUploaderNameContaining(string match);
        IEnumerable<Song> GetSongsUploaderUserNameContaining(string modelSearchFor);
    }
}
