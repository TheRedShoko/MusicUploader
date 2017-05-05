using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using MusicUploader.Models.BindingModels.Song;
using MusicUploader.Models.EntityModels;
using MusicUploader.Services.Interfaces;
using Microsoft.AspNet.Identity;
using MusicUploader.Data;

namespace MusicUploader.Services.Services
{
    public class SongService : ISongService
    {
        private MusicUploaderDbContext context;

        public SongService(MusicUploaderDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Song> GetSongsByGenreId(int genreId)
        {
            var songs = context
                .Songs
                .Where(song => song.Genre.Id == genreId || genreId == 0)
                .AsEnumerable();

            return songs;
        }

        public Song FindSongById(int? id)
        {
            Song song = context.Songs.Find(id);
            return song;
        }

        public string UploadAudioFile(HttpPostedFileBase audioFile, string path)
        {
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(audioFile.FileName)}";
            audioFile.SaveAs(Path.Combine(path, fileName));
            return fileName;
        }

        public string UploadCoverFile(HttpPostedFileBase imageFile, string path)
        {
            if (imageFile == null ||
                !imageFile.ContentType.Contains("image") ||
                imageFile.ContentLength == 0)
            {
                return "Default.jpg";
            }

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
            imageFile.SaveAs(Path.Combine(path, fileName));
            return fileName;
        }

        public void AddSong(UploadSongBindingModel model, 
            string audioFileName, string coverFileName)
        {
            Song song = Mapper.Instance.Map<UploadSongBindingModel, Song>(model);
            song.Uploader = context.Users.Find(HttpContext.Current.User.Identity.GetUserId());
            song.AudioFileName = audioFileName;
            song.ImageFileName = coverFileName;
            song.UploadDate = DateTime.Now;
            song.Genre = this.context.Genres.Find(model.GenreId);

            context.Songs.Add(song);
            context.SaveChanges();
        }

        public void SongModified(Song song)
        {
            context.Entry(song).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteSong(Song song)
        {
            context.Songs.Remove(song);
            context.SaveChanges();
        }

        public IEnumerable<Genre> GetAvailableGenres()
        {
            var genres = this.context.Genres;
            return genres;
        }

        public IEnumerable<Playlist> GetUserPlaylistsNotContaining(Song song)
        {
            var user = this.context.Users.Find(HttpContext.Current.User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            var playlists = user.Playlists.Where(pl => !pl.Songs.Contains(song)).ToList();

            return playlists;
        }
    }
}
