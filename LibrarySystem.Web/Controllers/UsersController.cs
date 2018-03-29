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
using System.Threading.Tasks;
using LibrarySystem.Common.Helper;

namespace LibrarySystemProject.Controllers
{
    public class UsersController : Controller
    {
        private SendConfirmEmail sendConfirmEmail = new SendConfirmEmail();

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
            //model.IsEmailConfirmed = user.IsEmailConfirmed;
           // model.ValidationCode = user.ValidationCode;

            return View(model);
        }

        [HttpPost]
        public ActionResult Profile(UserEditViewModel model)
        {
            UserRepository repository = new UserRepository();

            //User testUser = repository.GetById(model.Id);

            User user = repository.GetById(model.Id);


            user.Id = model.Id;
                user.ImgURL = model.imgURL;
                user.Email = model.Email;
                user.Username = model.username;
                user.Password = model.password;
                user.FirstName = model.firstName;
                user.LastName = model.lastName;
            user.IsAdmin = model.isAdmin;
                //IsEmailConfirmed = testUser.IsEmailConfirmed,
                //ValidationCode = testUser.ValidationCode
            

            repository.Save(user);

            return RedirectToAction("IndexPage", "Home");
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task <ActionResult> Register(UserCreateViewModel model)
        {
            string validationCode = HashUtils.CreateReferralCode();
            var repository = new UserRepository();
            List<User> users = repository.GetAll();

            SendConfirmEmail emailSender = new SendConfirmEmail();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (users.Where(u => u.Email == model.Email).Any())
            {
                ModelState.AddModelError("error_email","This email is already taken!");
                return View();
                //return View("Error");
            }
            else if (users.Where(u => u.Username == model.username).Any())
            {
               
                ModelState.AddModelError("error_msg","This username is already taken!");
                return View();
               // return View("Error");
            }
            else
            {
                User user = new User();
                user.ImgURL = model.imgURL;
                user.Username = model.username;
                user.Password = model.password;
                user.FirstName = model.firstName;
                user.LastName = model.lastName;
                user.Email = model.Email;
                user.IsAdmin = model.isAdmin;
                user.IsEmailConfirmed = false;
                user.ValidationCode = validationCode;

                repository.Insert(user);

                sendConfirmEmail.SendConfirmationEmailAsync(user);

            }
            return RedirectToAction("IndexPage", "Home");
        }

        [HttpPost]
        public ActionResult Create(UserCreateViewModel model)
        {
            string validationCode = HashUtils.CreateReferralCode();
            var repository = new UserRepository();
            SendConfirmEmail emailSender = new SendConfirmEmail();

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
            user.IsEmailConfirmed = false;
            user.ValidationCode = validationCode;

            repository.Insert(user);

            sendConfirmEmail.SendConfirmationEmailAsync(user);

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
                //model.IsEmailConfirmed = user.IsEmailConfirmed;
                //model.ValidationCode = user.ValidationCode;

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

            User user = repository.GetById(model.Id);
            user.Id = model.Id;
            user.ImgURL = model.imgURL;
            user.Email = model.Email;
            user.Username = model.username;
            user.Password = model.password;
            user.FirstName = model.firstName;
            user.LastName = model.lastName;
            user.IsAdmin = model.isAdmin;
            //user.IsEmailConfirmed = model.IsEmailConfirmed;
            //user.ValidationCode = model.ValidationCode;

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
            //model.isAdmin = user.IsAdmin;
            //model.IsEmailConfirmed = user.IsEmailConfirmed;


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

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ValidateEmail(string userId, string validationCode)
        {
            if (userId == null || validationCode == null)
            {
                return RedirectToAction("IndexPage", "Home");
            }

                UserRepository repository = new UserRepository();

                User user = repository.GetById(Int32.Parse(userId));
                if (user == null || validationCode != user.ValidationCode)
                {
                    return RedirectToAction("IndexPage", "Home");
                }
             
                user.Id = Int32.Parse(userId);
                user.ValidationCode = validationCode;
                user.IsEmailConfirmed = true;

                 repository.Update(user);
   
                 return View("ConfirmEmail");
        }
        public ActionResult ConfirmEmail()
        {
            return View();
        }
    }
}
