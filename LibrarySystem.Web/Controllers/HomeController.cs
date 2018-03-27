using LibrarySystem.DAL.Repositories.UserRepository;
using LibrarySystem.NotificationServices;
using LibrarySystem.RelationServices.Domain.User;
using LibrarySystem.Web.ViewModels.EmailSendingViewModel;
using LibrarySystemProject.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Web.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult IndexPage()
        {

            return View();
        }

        public ActionResult Index()
        {
           
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
            List<User> items = repo.GetAll(i => i.Username == model.Username && i.Password == model.Password);
            Session["LoggedUser"] = items.Count > 0 ? items[0] : null;

            Session["UserName"] = model.Username;

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


        //[HttpGet]
        //public ActionResult Contact()
        //{
        //    return View();
        //}

        [HttpPost]
        public ActionResult IndexPage(EmailSendingViewModel emailSendingViewModel)
        {
            EmailSender emailSender = new EmailSender();

            if (emailSendingViewModel.Comment == null || emailSendingViewModel.Name == null || emailSendingViewModel.Email == null)
            {
                TempData["Email"] = "You have to enter all the needed information for sending an email!";
                return View("Contact");
            }

            emailSender.SendMail(emailSendingViewModel.Email, emailSendingViewModel.Name, emailSendingViewModel.Comment);
            TempData["Email"] = "You have send the email successfully!";

            return View("Contact");
        }


    }
}