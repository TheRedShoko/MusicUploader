using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using MusicUploader.Data;
using MusicUploader.Models.BindingModels.Genre;
using MusicUploader.Models.EntityModels;
using MusicUploader.Models.ViewModels.Genre;
using MusicUploader.Services.Interfaces;
using MusicUploader.Services.Services;

namespace MusicUploader.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class GenresController : Controller
    {
        private IGenreService service;

        public GenresController(IGenreService service)
        {
            this.service = service;
        }

        public GenresController()
            : this(new GenreService(new MusicUploaderDbContext()))
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var genres = service.GetAllGenres();
            var viewModels = Mapper.Instance.Map<IEnumerable<Genre>, IEnumerable<ListGenreViewModel>>(genres);
            return View(viewModels);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateGenreBindingModel model)
        {
            if (ModelState.IsValid)
            {
                service.CreateGenre(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Admin/Genres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = service.FindGenreById(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            var viewModel = Mapper.Instance.Map<Genre, EditGenreViewModel>(genre);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditGenreBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var genre = this.service.FindGenreById(model.Id);

                if (genre == null)
                {
                    return HttpNotFound();
                }

                genre = Mapper.Instance.Map<EditGenreBindingModel, Genre>(model, genre);

                this.service.GenreModified(genre);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Genre genre = this.service.FindGenreById(id);
            if (genre == null)
            {
                return HttpNotFound();
            }

            this.service.DeleteGenre(genre);

            return RedirectToAction("Index");
        }
    }
}
