using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibrarySystemProject.Models.UserViewModel;
using System.Web.Security;
using LibrarySystem.DAL.Repositories.UserRepository;
using LibrarySystem.RelationServices.Domain.User;
using LibrarySystem.Web.Authentication;
using LibrarySystem.NotificationServices;

namespace LibrarySystemProject.Controllers
{
    public class UsersController : Controller
    {


        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]
        public ActionResult Index()
        {
            UserRepository repository = new UserRepository();
            List<User> users = repository.GetAll();

            UserListViewModel model = new UserListViewModel();
            model.Users = users;

            return View(model);
        }

        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Profile()
        {

            int id = UserLogin.GetUserId();
            UserRepository repository = new UserRepository();
            UserEditViewModel model = new UserEditViewModel();
            User user = repository.GetById(id);

            model.Id = UserLogin.GetUserId();
            model.imgURL = user.ImgURL;
            model.Email = user.Email;
            model.username = user.Username;
            model.password = user.Password;
            model.firstName = user.FirstName;
            model.lastName = user.LastName;
            model.isAdmin = user.IsAdmin;


            return View(model);
        }

        [HttpPost]
        public ActionResult Profile(UserEditViewModel model)
        {
            UserRepository repository = new UserRepository();

            User user = new User
            {
                Id = model.Id,
                ImgURL = model.imgURL,
                Email = model.Email,
                Username = model.username,
                Password = model.password,
                FirstName = model.firstName,
                LastName = model.lastName,
                IsAdmin = model.isAdmin
            };

            repository.Save(user);

            //id =  LoginFilter.LoginUser.GetUserId();
            //UserRepository repository = new UserRepository();
            //UserEditViewModel model = new UserEditViewModel();
            //User user = repository.GetById(id);

            //model.username = user.username;
            //model.password = user.password;
            //model.firstName = user.firstName;
            //model.lastName = user.lastName;
            //model.Id = LoginFilter.LoginUser.GetUserId();


            return RedirectToAction("IndexPage", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = new User();
            user.ImgURL = model.imgURL;
            user.Username = model.username;
            user.Password = model.password;
            user.FirstName = model.firstName;
            user.LastName = model.lastName;
            user.Email = model.Email;
            user.IsAdmin = model.isAdmin;

            var repository = new UserRepository();
            repository.Insert(user);

            //return RedirectToAction("Index");
            // return RedirectToAction("Home/IndexPage");
            EmailSender sender = new EmailSender();
            sender.SendEmail(model.Email, "Ho-ho-ho", "Merry Christmas!!!");
            return RedirectToAction("IndexPage", "Home");
        }

        [HttpPost]
        public ActionResult Create(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = new User();
            // user.Id = model.Id;
            user.ImgURL = model.imgURL;
            user.Username = model.username;
            user.Password = model.password;
            user.FirstName = model.firstName;
            user.LastName = model.lastName;
            user.Email = model.Email;
            user.IsAdmin = model.isAdmin;

            var repository = new UserRepository();
            repository.Insert(user);

            return RedirectToAction("Index");
        }

        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            UserRepository repository = new UserRepository();

            UserEditViewModel model = new UserEditViewModel();

            if (id.HasValue)
            {
                User user = repository.GetById(id.Value);
                model.Id = user.Id;
                model.imgURL = user.ImgURL;
                model.Email = user.Email;
                model.username = user.Username;
                model.password = user.Password;
                model.firstName = user.FirstName;
                model.lastName = user.LastName;
                model.isAdmin = user.IsAdmin;

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserRepository repository = new UserRepository();

            User user = new User();
            user.Id = model.Id;
            user.ImgURL = model.imgURL;
            user.Email = model.Email;
            user.Username = model.username;
            user.Password = model.password;
            user.FirstName = model.firstName;
            user.LastName = model.lastName;
            user.IsAdmin = model.isAdmin;

            repository.Save(user);

            return RedirectToAction("Index");
        }

        [LibrarySystem.Web.ActionFilters.AuthenticationFilter(RequireAdminRole = true)]

        [HttpGet]
        public ActionResult Delete(int id)
        {

            UserRepository repository = new UserRepository();

            User user = repository.GetById(id);

            UserDeleteViewModel model = new UserDeleteViewModel();
            model.imgURL = user.ImgURL;
            model.username = user.Username;
            model.password = user.Password;
            model.firstName = user.FirstName;
            model.lastName = user.LastName;
            model.Email = user.Email;
            model.isAdmin = user.IsAdmin;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(UserDeleteViewModel model)
        {

            UserRepository repository = new UserRepository();
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }


            return RedirectToAction("Index");
        }

    }
}
