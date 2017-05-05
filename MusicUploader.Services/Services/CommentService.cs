using System.Data.Entity;
using System.Web;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MusicUploader.Data;
using MusicUploader.Models.BindingModels.Comment;
using MusicUploader.Models.EntityModels;
using MusicUploader.Services.Interfaces;

namespace MusicUploader.Services.Services
{
    public class CommentService : ICommentService
    {
        private MusicUploaderDbContext context;

        public CommentService(MusicUploaderDbContext context)
        {
            this.context = context;
        }

        public ApplicationUser GetCurrentUser()
        {
            var user = context.Users.Find(HttpContext.Current.User.Identity.GetUserId());
            return user;
        }

        public Song FindSongById(int songId)
        {
            var song = this.context.Songs.Find(songId);
            return song;
        }

        public void AddComment(Comment comment)
        {
            this.context.Comments.Add(comment);
            this.context.SaveChanges();
        }

        public Comment FindCommentById(int? id)
        {
            var comment = this.context.Comments.Find(id);
            return comment;
        }

        public void EditComment(Comment comment, EditCommentBindingModel model)
        {
            comment = Mapper.Instance.Map<EditCommentBindingModel, Comment>(model, comment);
            this.context.Entry(comment).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void DeleteComment(Comment comment)
        {
            this.context.Comments.Remove(comment);
            this.context.SaveChanges();
        }
    }
}
