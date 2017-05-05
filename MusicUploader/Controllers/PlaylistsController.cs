using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MusicUploader.Data;
using MusicUploader.Models.BindingModels.Playlist;
using MusicUploader.Models.EntityModels;
using MusicUploader.Models.ViewModels.Playlist;
using MusicUploader.Services.Interfaces;
using MusicUploader.Services.Services;

namespace MusicUploader.Controllers
{
    [Authorize]
    public class PlaylistsController : Controller
    {
        private IPlaylistService service;

        public PlaylistsController(IPlaylistService service)
        {
            this.service = service;
        }
        public PlaylistsController()
            : this(new PlaylistService(new MusicUploaderDbContext()))
        {
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var playlists = service.GetExistingPlaylists();
            var viewModels = Mapper.Instance
                .Map<IEnumerable<Playlist>, IEnumerable<ViewPlaylistViewModel>>(playlists);

            return View(viewModels);
        }

        [AllowAnonymous]
        public ActionResult ViewSongs(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var playlist = this.service.FindPlaylistById(id);

            if (playlist == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Instance
                .Map<Playlist, ListPlaylistSongsViewModel>(playlist);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePlaylistBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var playlist = Mapper.Instance.Map<CreatePlaylistBindingModel, Playlist>(model);

                this.service.AddPlaylist(playlist);

                return RedirectToAction("Index");
            }

            return View(model);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var playlist = this.service.FindPlaylistById(id);

            if (playlist == null)
            {
                return HttpNotFound();
            }

            if (playlist.Owner.Id != User.Identity.GetUserId() && !User.IsInRole("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var viewModel = Mapper.Instance.Map<Playlist, EditPlaylistViewModel>(playlist);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPlaylistBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var playlist = this.service.FindPlaylistById(model.Id);

                if (playlist == null)
                {
                    return HttpNotFound();
                }

                if (playlist.Owner.Id != User.Identity.GetUserId() && !User.IsInRole("admin"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                this.service.EditPlaylist(model);

                return RedirectToAction("Index");
            }

            var viewModel = Mapper.Instance.Map<EditPlaylistBindingModel, EditPlaylistViewModel>(model);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var playlist = this.service.FindPlaylistById(id);

            if (playlist == null)
            {
                return HttpNotFound();
            }

            if (playlist.Owner.Id != User.Identity.GetUserId() && !User.IsInRole("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            this.service.DeletePlaylist(playlist);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddSongToPlaylist(int songId, int playlistId)
        {
            var playlist = service.FindPlaylistById(playlistId);
            var song = service.FindSongById(songId);

            if (playlist == null || song == null)
            {
                return HttpNotFound();
            }

            if (playlist.Songs.Contains(song))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.service.AddSongToPlaylist(playlist, song);

            return RedirectToAction("Details", "Songs", new {id = songId});
        }

        [HttpGet]
        public ActionResult RemvoeSongFromPlaylist(int songId, int playlistId)
        {
            var playlist = service.FindPlaylistById(playlistId);
            var song = service.FindSongById(songId);

            if (playlist == null || song == null)
            {
                return HttpNotFound();
            }

            if (!playlist.Songs.Contains(song))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.service.RemoveSongFromPlaylist(playlist, song);

            return RedirectToAction("ViewSongs", new {id = playlistId});
        }
    }
}
