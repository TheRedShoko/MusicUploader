using MusicUploader.Models.BindingModels.Comment;
using MusicUploader.Models.EntityModels;

namespace MusicUploader.Services.Interfaces
{
    public interface ICommentService
    {
        ApplicationUser GetCurrentUser();
        Song FindSongById(int songId);
        void AddComment(Comment comment);
        Comment FindCommentById(int? id);
        void EditComment(Comment comment, EditCommentBindingModel model);
        void DeleteComment(Comment comment);
    }
}
