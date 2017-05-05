using System.Collections.Generic;
using MusicUploader.Models.BindingModels.Genre;
using MusicUploader.Models.EntityModels;

namespace MusicUploader.Services.Interfaces
{
    public interface IGenreService
    {
        IEnumerable<Genre> GetAllGenres();
        void CreateGenre(CreateGenreBindingModel model);
        Genre FindGenreById(int? id);
        void GenreModified(Genre model);
        void DeleteGenre(Genre genre);
    }
}
