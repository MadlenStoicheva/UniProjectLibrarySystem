using LibrarySystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystemProject.LoginFilter
{
    public static class LoginUser
    {

        public static bool IsAdmin()
        {
            User user = (User)HttpContext.Current.Session["LoggedUser"];

            if (user != null)
            {
                if (user.isAdmin)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            } 
            else
            {
                HttpContext.Current.Response.Redirect("/Home/IndexPage");
                return false;
            }
        }

        public static bool IsNotLogedUser()
        {
            if (HttpContext.Current.Session["LoggedUser"] == null)
            {
                return  true;
            }
            else
            {
                //HttpContext.Current.Response.Redirect("/Home/IndexPage");
                return false;
            }
        }

        public static int GetUserId()
        {
            User user = (User)HttpContext.Current.Session["LoggedUser"];

            return user.Id;
        }

    }
}