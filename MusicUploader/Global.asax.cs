using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using MusicUploader.Data;
using MusicUploader.Models.BindingModels.Comment;
using MusicUploader.Models.BindingModels.Genre;
using MusicUploader.Models.BindingModels.Playlist;
using MusicUploader.Models.BindingModels.Song;
using MusicUploader.Models.EntityModels;
using MusicUploader.Models.ViewModels.Comment;
using MusicUploader.Models.ViewModels.Genre;
using MusicUploader.Models.ViewModels.Playlist;
using MusicUploader.Models.ViewModels.Song;
using MusicUploader.Services.Interfaces;
using MusicUploader.Services.Services;
using Ninject;

namespace MusicUploader
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ConfigureMapper();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Song, ViewSongViewModel>();
                config.CreateMap<Song, DetailedSongViewModel>();
                config.CreateMap<UploadSongBindingModel, Song>();
                config.CreateMap<Song, EditSongViewModel>();
                config.CreateMap<EditSongBindingModel, Song>();
                config.CreateMap<EditSongBindingModel, EditSongViewModel>();
                config.CreateMap<Song, DeleteSongViewModel>();

                config.CreateMap<Genre, ListGenreViewModel>();
                config.CreateMap<CreateGenreBindingModel, Genre>();
                config.CreateMap<Genre, EditGenreViewModel>();
                config.CreateMap<EditGenreBindingModel, Genre>();

                config.CreateMap<Playlist, ViewPlaylistViewModel>();
                config.CreateMap<Playlist, ListPlaylistSongsViewModel>();
                config.CreateMap<CreatePlaylistBindingModel, Playlist>();
                config.CreateMap<Playlist, EditPlaylistViewModel>();
                config.CreateMap<EditPlaylistBindingModel, Playlist>();
                config.CreateMap<EditPlaylistBindingModel, EditPlaylistViewModel>();

                config.CreateMap<Comment, EditCommentViewModel>();
                config.CreateMap<EditCommentViewModel, Comment>();
                config.CreateMap<EditCommentBindingModel, EditCommentViewModel>();
                config.CreateMap<EditCommentBindingModel, Comment>();
            });
        }
    }
}
