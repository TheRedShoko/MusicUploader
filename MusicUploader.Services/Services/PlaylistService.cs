using System.Collections.Generic;
using System.Web;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MusicUploader.Data;
using MusicUploader.Models.BindingModels.Playlist;
using MusicUploader.Models.EntityModels;
using MusicUploader.Services.Interfaces;

namespace MusicUploader.Services.Services
{
    public class PlaylistService : IPlaylistService
    {
        private MusicUploaderDbContext context;

        public PlaylistService(MusicUploaderDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Playlist> GetExistingPlaylists()
        {
            var playlists = this.context.Playlists;

            return playlists;
        }

        public Playlist FindPlaylistById(int? id)
        {
            var playlist = this.context.Playlists.Find(id);

            return playlist;
        }

        public void AddPlaylist(Playlist playlist)
        {
            var user = this.context.Users.Find(HttpContext.Current.User.Identity.GetUserId());
            playlist.Owner = user;

            this.context.Playlists.Add(playlist);
            this.context.SaveChanges();
        }

        public void EditPlaylist(EditPlaylistBindingModel model)
        {
            var playlist = this.FindPlaylistById(model.Id);
            playlist = Mapper.Instance.Map<EditPlaylistBindingModel, Playlist>(model, playlist);
            this.context.SaveChanges();
        }

        public void DeletePlaylist(Playlist playlist)
        {
            this.context.Playlists.Remove(playlist);
            this.context.SaveChanges();
        }

        public Song FindSongById(int songId)
        {
            var song = this.context.Songs.Find(songId);
            return song;
        }

        public void AddSongToPlaylist(Playlist playlist, Song song)
        {
            playlist.Songs.Add(song);
            this.context.SaveChanges();
        }

        public void RemoveSongFromPlaylist(Playlist playlist, Song song)
        {
            playlist.Songs.Remove(song);
            this.context.SaveChanges();
        }
    }
}
