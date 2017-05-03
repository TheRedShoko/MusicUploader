using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MusicUploader.Models.EntityModels;

namespace MusicUploader.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class MusicUploaderDbContext : IdentityDbContext<ApplicationUser>
    {
        public MusicUploaderDbContext()
            : base("MusicUploaderDbContext")
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
    }
}