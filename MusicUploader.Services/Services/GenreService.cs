using System.Collections.Generic;
using System.Data.Entity;
using AutoMapper;
using MusicUploader.Data;
using MusicUploader.Models.BindingModels.Genre;
using MusicUploader.Models.EntityModels;
using MusicUploader.Services.Interfaces;

namespace MusicUploader.Services.Services
{
    public class GenreService : IGenreService
    {
        private MusicUploaderDbContext context;

        public GenreService(MusicUploaderDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var genres = this.context.Genres;
            return genres;
        }

        public void CreateGenre(CreateGenreBindingModel model)
        {
            var genre = Mapper.Instance.Map<CreateGenreBindingModel, Genre>(model);
            context.Genres.Add(genre);
            context.SaveChanges();
        }

        public Genre FindGenreById(int? id)
        {
            var genre = this.context.Genres.Find(id);
            return genre;
        }

        public void GenreModified(Genre model)
        {
            this.context.Entry(model).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void DeleteGenre(Genre genre)
        {
            foreach (var song in genre.Songs)
            {
                context.Songs.Remove(song);
            }
            this.context.Genres.Remove(genre);
            this.context.SaveChanges();
        }
    }
}
