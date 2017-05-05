using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MusicUploader.Data;
using MusicUploader.Models.BindingModels.Song;
using MusicUploader.Models.EntityModels;
using MusicUploader.Models.ViewModels.Song;
using MusicUploader.Services.Interfaces;
using MusicUploader.Services.Services;

namespace MusicUploader.Controllers
{
    public class SongsController : Controller
    {
        private ISongService service;

        public SongsController(ISongService service)
        {
            this.service = service;
        }
        public SongsController()
            : this(new SongService(new MusicUploaderDbContext()))
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new SongsIndexViewModel()
            {
                Genres = this.service.GetAvailableGenres()
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Song song = service.FindSongById(id);

            if (song == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Instance.Map<Song, DetailedSongViewModel>(song);
            ViewBag.Playlists = service.GetUserPlaylistsNotContaining(song);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Update(int genreId)
        {
            var viewModel = new ListSongsViewModel()
            {
                Songs = this.service.GetSongsByGenreId(genreId)
            };

            return PartialView("_ViewSongsPartial", viewModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Upload()
        {
            ViewBag.Genres = this.service.GetAvailableGenres();
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(UploadSongBindingModel model)
        {
            if (model.AudioFile.ContentType.Contains("audio")
                && model.AudioFile.ContentLength > 0)
            {
                var audioPath = Server.MapPath("~/uploads/audio/");
                var imagePath = Server.MapPath("~/uploads/image/");
                string audioFileName = service.UploadAudioFile(model.AudioFile, audioPath);
                string coverFileName = service.UploadCoverFile(model.ImageFile, imagePath);

                service.AddSong(model, audioFileName, coverFileName);

                return RedirectToAction("Index");
            }

            ViewBag.Genres = service.GetAvailableGenres();
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = service.FindSongById(id);

            if (song == null)
            {
                return HttpNotFound();
            }

            if (song.Uploader.Id != User.Identity.GetUserId() && !User.IsInRole("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var viewModel = Mapper.Instance.Map<Song, EditSongViewModel>(song);
            return View(viewModel);
        }

        [HttpPut]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditSongBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var song = service.FindSongById(model.Id);
                if (song == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                if (song.Uploader.Id != User.Identity.GetUserId() && !User.IsInRole("admin"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                song = Mapper.Instance.Map<EditSongBindingModel, Song>(model, song);

                service.SongModified(song);

                return RedirectToAction("Index");
            }

            var viewModel = Mapper.Instance.Map<EditSongBindingModel, EditSongViewModel>(model);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var song = service.FindSongById(id);
            if (song == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (song.Uploader.Id != User.Identity.GetUserId() && !User.IsInRole("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            service.DeleteSong(song);

            return RedirectToAction("Index");
        }
    }
}
