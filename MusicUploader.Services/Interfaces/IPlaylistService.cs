using System.Collections.Generic;
using MusicUploader.Models.BindingModels.Playlist;
using MusicUploader.Models.EntityModels;

namespace MusicUploader.Services.Interfaces
{
    public interface IPlaylistService
    {
        IEnumerable<Playlist> GetExistingPlaylists();
        Playlist FindPlaylistById(int? id);
        void AddPlaylist(Playlist playlist);
        void EditPlaylist(EditPlaylistBindingModel model);
        void DeletePlaylist(Playlist playlist);
        Song FindSongById(int songId);
        void AddSongToPlaylist(Playlist playlist, Song song);
        void RemoveSongFromPlaylist(Playlist playlist, Song song);
    }
}
