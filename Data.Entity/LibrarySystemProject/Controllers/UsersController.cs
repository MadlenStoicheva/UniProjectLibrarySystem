using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data.Entity;
using LibrarySystem.Entity;
using Data.Entity.Repositories;
using LibrarySystemProject.Models.UserViewModel;
using System.Windows.Forms;
using LibrarySystemProject.Filters;
using System.Web.Security;

namespace LibrarySystemProject.Controllers
{
    public class UsersController : Controller
    {
        

        [AuthenticationFilter(RequireAdminRole = true)]
        public ActionResult Index()
        {
            UserRepository repository = new UserRepository();
            List<User> users = repository.GetAll();

            UserListViewModel model = new UserListViewModel();
            model.Users = users;

            return View(model);
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Profile()
        {

            int id = LoginFilter.LoginUser.GetUserId();
            UserRepository repository = new UserRepository();
            UserEditViewModel model = new UserEditViewModel();
            User user = repository.GetById(id);

            model.username = user.username;
            model.password = user.password;
            model.firstName = user.firstName;
            model.lastName = user.lastName;
            model.Id = LoginFilter.LoginUser.GetUserId();

            return View(model);
        }

        [HttpPost]
        public ActionResult Profile(UserEditViewModel model)
        {
            UserRepository repository = new UserRepository();

            User user = new User
            {
                Id = model.Id,
                username = model.username,
                password = model.password,
                firstName = model.firstName,
                lastName = model.lastName
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
            user.username = model.username;
            user.password = model.password;
            user.firstName = model.firstName;
            user.lastName = model.lastName;
            user.isAdmin = model.isAdmin;

            var repository = new UserRepository();
            repository.Insert(user);

            //return RedirectToAction("Index");
           // return RedirectToAction("Home/IndexPage");
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
            user.username = model.username;
            user.password = model.password;
            user.firstName = model.firstName;
            user.lastName = model.lastName;
            user.isAdmin = model.isAdmin;            

            var repository = new UserRepository();
            repository.Insert(user);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = true)]

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            UserRepository repository = new UserRepository();

            UserEditViewModel model = new UserEditViewModel();

            if (id.HasValue)
            {
                User user = repository.GetById(id.Value);
                model.Id = user.Id;
                model.username = user.username;
                model.password = user.password;
                model.firstName = user.firstName;
                model.lastName = user.lastName;
                model.isAdmin = user.isAdmin;

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
            user.username = model.username;
            user.password = model.password;
            user.firstName = model.firstName;
            user.lastName = model.lastName;
            user.isAdmin = model.isAdmin;

            repository.Save(user);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = true)]

        [HttpGet]
        public ActionResult Delete(int id)
        {

            UserRepository repository = new UserRepository();

            User user = repository.GetById(id);

            UserDeleteViewModel model = new UserDeleteViewModel();
            model.username = user.username;
            model.password = user.password;
            model.firstName = user.firstName;
            model.lastName = user.lastName;
            model.isAdmin = user.isAdmin;

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
