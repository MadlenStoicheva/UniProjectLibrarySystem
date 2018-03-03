using Data.Entity.Repositories;
using LibrarySystem.Entity;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystemProject.Views.Login;
using LibrarySystemProject.Filters;
using LibrarySystemProject.Models.BookViewModel;

namespace LibrarySystemProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("IndexPage", "Home");
        }

        [HttpGet]
        public ActionResult IndexPage()
        {
            //BookRepository repository = new BookRepository();
            //List<Book> books = repository.GetAll();

            //BookListViewModel model = new BookListViewModel();
            //model.Books = books;

            //return View(model);

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            UserRepository repo = new UserRepository();
            List<User> items = repo.GetAll(i => i.username == model.Username && i.password == model.Password);
            Session["LoggedUser"] = items.Count > 0 ? items[0] : null;

            if (items.Count <= 0)
                this.ModelState.AddModelError("failedLogin", "Login failed!");

            if (!ModelState.IsValid)
                return View(model);

            return RedirectToAction("IndexPage", "Home");
        }

       public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["LoggedUser"] = null;
            return RedirectToAction("IndexPage", "Home");
        }
    }
}