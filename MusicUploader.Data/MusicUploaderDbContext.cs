using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MusicUploader.Models.EntityModels;

namespace MusicUploader.Data
{
    public class MusicUploaderDbContext : IdentityDbContext<ApplicationUser>
    {
        public MusicUploaderDbContext()
            : base("MusicUploaderDb")
        {
        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        public static MusicUploaderDbContext Create()
        {
            return new MusicUploaderDbContext();
        }

        public System.Data.Entity.DbSet<MusicUploader.Models.ViewModels.Playlist.ListPlaylistSongsViewModel> ListPlaylistSongsViewModels { get; set; }
    }
}