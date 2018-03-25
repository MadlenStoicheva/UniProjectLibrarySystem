using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.DAL.Repositories.BookRepository;

using LibrarySystem.RelationServices.Domain.Book;
using LibrarySystemProject.Models.BookViewModel;

namespace LibrarySystemProject.Controllers
{

    public class BooksController : Controller
    {
        public ActionResult Index()
        {
            BookRepository repository = new BookRepository();
            List<Book> books = repository.GetAll();

            BookListViewModel model = new BookListViewModel();
            model.Books = books;

            return View(model);
        }

        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]
        public ActionResult Create()
        {
            return View();
        }

        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]
        public ActionResult Reserve()
        {
            return View();
        }

        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Reserve(int id)
        {

            BookRepository repository = new BookRepository();

            Book book = repository.GetById(id);

            BookDeleteViewModel model = new BookDeleteViewModel();
            model.numberISBN = book.NumberISBN;
            model.title = book.Title;
            model.author = book.Author;
            model.publishingHouse = book.PublishingHouse;
            model.releaseDate = book.ReleaseDate;
            model.availability = book.Availability;

            return View(model);
        }

        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]
        [HttpPost]
        public ActionResult Create(BookCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Book book = new Book();
            book.NumberISBN = model.numberISBN;
            book.Title = model.title;
            book.Author = model.author;
            book.PublishingHouse = model.publishingHouse;
            book.ReleaseDate = model.releaseDate;
            book.Availability = model.availability;
            book.ImgURL = model.imgURL;

            var repository = new BookRepository();
            repository.Insert(book);

            return RedirectToAction("Index", "Books");
        }

        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Edit(int? id)
        {

            BookRepository repository = new BookRepository();

            BookEditViewModel model = new BookEditViewModel();

            if (id.HasValue)
            {
                Book book = repository.GetById(id.Value);
                model.Id = book.Id;
                model.imgURL = book.ImgURL;
                model.numberISBN = book.NumberISBN;
                model.title = book.Title;
                model.author = book.Author;
                model.publishingHouse = book.PublishingHouse;
                model.releaseDate = book.ReleaseDate;
                model.availability = book.Availability;

            }

            return View(model);
        }

        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]
        [HttpPost]
        public ActionResult Edit(BookEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            BookRepository repository = new BookRepository();

            Book book = new Book();
            book.Id = model.Id;
            book.ImgURL = model.imgURL;
            book.NumberISBN = model.numberISBN;
            book.Title = model.title;
            book.Author = model.author;
            book.PublishingHouse = model.publishingHouse;
            if (book.ReleaseDate != model.releaseDate)
            {
                book.ReleaseDate = model.releaseDate;
            }
            else
            {
                book.ReleaseDate = book.ReleaseDate;
            }

            book.Availability = model.availability;


            repository.Save(book);

            return RedirectToAction("Index", "Books");
        }

        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Delete(int id)
        {

            BookRepository repository = new BookRepository();

            Book book = repository.GetById(id);

            BookDeleteViewModel model = new BookDeleteViewModel();
            model.imgURL = book.ImgURL;
            model.numberISBN = book.NumberISBN;
            model.title = book.Title;
            model.author = book.Author;
            model.publishingHouse = book.PublishingHouse;
            model.releaseDate = book.ReleaseDate;
            model.availability = book.Availability;

            return View(model);
        }
        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]
        [HttpPost]
        public ActionResult Delete(BookDeleteViewModel model)
        {

            BookRepository repository = new BookRepository();
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }


            return RedirectToAction("Index", "Books");
        }

    }
}