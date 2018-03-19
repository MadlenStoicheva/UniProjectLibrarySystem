﻿using LibrarySystem.RelationalServices.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LibrarySystem.AuthenticationService
{
    public static class AuthenticateLoginUserService
    {
        public static bool IsAdmin()
        {
            User user = (User)HttpContext.Current.Session["LoggedUser"];

            if (user != null)
            {
                if (user.IsAdmin)
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
                return true;
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

