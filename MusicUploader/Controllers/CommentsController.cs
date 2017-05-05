using System;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MusicUploader.Data;
using MusicUploader.Models.BindingModels.Comment;
using MusicUploader.Models.EntityModels;
using MusicUploader.Models.ViewModels.Comment;
using MusicUploader.Services.Interfaces;
using MusicUploader.Services.Services;

namespace MusicUploader.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private ICommentService service;

        public CommentsController(ICommentService service)
        {
            this.service = service;
        }
        public CommentsController()
            : this(new CommentService(new MusicUploaderDbContext()))
        {
        }

        [HttpPost]
        public ActionResult Create(CreateCommentBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment()
                {
                    Content = model.Content,
                    Author = service.GetCurrentUser(),
                    CreationDate = DateTime.Now,
                    Song = service.FindSongById(model.SongId)
                };

                this.service.AddComment(comment);
            }

            return RedirectToAction("Details", "Songs", new { id = model.SongId });
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = this.service.FindCommentById(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            if (comment.Author.Id != User.Identity.GetUserId() && !User.IsInRole("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var viewModel = Mapper.Instance.Map<Comment, EditCommentViewModel>(comment);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCommentBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = this.service.FindCommentById(model.Id);

                if (comment == null)
                {
                    return HttpNotFound();
                }

                if (comment.Author.Id != User.Identity.GetUserId() && !User.IsInRole("admin"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                this.service.EditComment(comment, model);
                
                return RedirectToAction("Details", "Songs", new {id = comment.Song.Id});
            }

            var viewModel = Mapper.Instance.Map<EditCommentBindingModel, EditCommentViewModel>(model);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var comment = this.service.FindCommentById(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            if (comment.Author.Id != User.Identity.GetUserId() && !User.IsInRole("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            int songId = comment.Song.Id;

            this.service.DeleteComment(comment);

            return RedirectToAction("Details", "Songs", new { id = songId });
        }
    }
}