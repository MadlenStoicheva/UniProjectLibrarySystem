
using LibrarySystem.AuthenticationService;
using LibrarySystem.DAL.Repositories.BookRepository;
using LibrarySystem.DAL.Repositories.TakeABookRepository;
using LibrarySystem.DAL.Repositories.UserRepository;
using LibrarySystem.RelationalServices.Domain.Book;
using LibrarySystem.RelationalServices.Domain.TakeABook;
using LibrarySystem.RelationalServices.Domain.User;
using LibrarySystemProject.Models.TakeABookViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystemProject.Controllers
{

    public class TakeABooksController : Controller
    {
        public ActionResult Index()
        {
            TakeABookRepository repository = new TakeABookRepository();

            List<TakeABook> takeABooks = repository.GetAll();
            List<TakeABook> takeABook = new List<TakeABook>();
            if (AuthenticateLoginUserService.IsAdmin())
            {
                takeABook = takeABooks;
            }
            else
            {
                foreach (var item in takeABooks)
                {
                    if (item.UserId == AuthenticateLoginUserService.GetUserId())
                    {
                        takeABook.Add(item);
                    }
                }
            }


            TakeABookListViewModel model = new TakeABookListViewModel();
            model.TakeABooks = takeABook;

            return View(model);
        }
        public ActionResult Create()
        {
            TakeABookCreateViewModel model = new TakeABookCreateViewModel();
            model.Books = PopuateBooksList();
            model.Users = PopuateUsersList();
            model.dateTaken = model.dateTaken.Date;
            model.dateForReturn = model.dateForReturn.Date;
            model.dateReturn = model.dateReturn.Date;


            return View(model);
        }

        private List<SelectListItem> PopuateBooksList()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            BookRepository bookRepo = new BookRepository();
            List<Book> books = bookRepo.GetAll();
            foreach (Book book in books)
            {
                SelectListItem item = new SelectListItem();
                item.Value = book.Id.ToString();
                item.Text = $"{book.Title} ({book.Author})";

                result.Add(item);
            }

            return result;
        }

        private List<SelectListItem> PopuateUsersList()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            UserRepository userRepo = new UserRepository();
            List<User> users = userRepo.GetAll();
            foreach (User user in users)
            {
                SelectListItem item = new SelectListItem();
                item.Value = user.Id.ToString();
                item.Text = $"{user.FirstName} {user.LastName}";

                result.Add(item);
            }

            return result;
        }

        [HttpPost]
        public ActionResult Create(TakeABookCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TakeABook takeABook = new TakeABook();
            takeABook.BookId = model.BookId;

            if (AuthenticateLoginUserService.IsAdmin())
            {
                takeABook.UserId = model.UserId;
            }
            else
            {
                takeABook.UserId = AuthenticateLoginUserService.GetUserId();
            }
            //takeABook.dateTaken = model.dateTaken.Date;
            takeABook.DateTaken = System.DateTime.Now;
            // takeABook.dateForReturn = model.dateForReturn.Date;
            takeABook.DateForReturn = takeABook.DateTaken.AddMonths(3);
            //takeABook.dateReturn = model.dateReturn.Date;
            DateTime now = DateTime.Now;
            takeABook.DateReturn = new DateTime(2000, 1, 1, 0, 0, 0);

            var repository = new TakeABookRepository();
            repository.Insert(takeABook);

            return RedirectToAction("Index");
        }

        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Edit(int? id)
        {

            TakeABookRepository repository = new TakeABookRepository();

            TakeABookEditViewModel model = new TakeABookEditViewModel();

            if (id.HasValue)
            {
                TakeABook takeABook = repository.GetById(id.Value);
                model.Id = takeABook.Id;
                model.Books = PopuateBooksList();
                model.Users = PopuateUsersList();
                model.dateTaken = takeABook.DateTaken.Date;
                model.dateForReturn = takeABook.DateForReturn.Date;
                model.dateReturn = takeABook.DateReturn.Date;


            }

            return View(model);
        }

        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]
        [HttpPost]
        public ActionResult Edit(TakeABookEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TakeABookRepository repository = new TakeABookRepository();

            TakeABook takeABook = new TakeABook();
            takeABook.Id = model.Id;
            takeABook.BookId = model.BookId;
            takeABook.UserId = model.UserId;
            takeABook.DateTaken = model.dateTaken.Date;
            takeABook.DateForReturn = model.dateForReturn.Date;
            takeABook.DateReturn = model.dateReturn.Date;

            repository.Save(takeABook);

            return RedirectToAction("Index");
        }

        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Delete(int id)
        {

            TakeABookRepository repository = new TakeABookRepository();

            TakeABook takeABook = repository.GetById(id);

            TakeABookDeleteViewModel model = new TakeABookDeleteViewModel();
            // model.Id = takeABook.Id;
            model.BookId = takeABook.BookId;
            model.UserId = takeABook.UserId;
            model.dateTaken = takeABook.DateTaken;
            model.dateForReturn = takeABook.DateForReturn;
            model.dateReturn = takeABook.DateReturn;

            return View(model);
        }

        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]
        [HttpPost]
        public ActionResult Delete(TakeABookDeleteViewModel model)
        {

            TakeABookRepository repository = new TakeABookRepository();
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }


            return RedirectToAction("Index");
        }

    }
}
