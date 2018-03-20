﻿
using LibrarySystem.RelationServices.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Web.ActionFilters
{
    public class AuthenticationFilter : ActionFilterAttribute
    {

        public bool RequireAdminRole { get; set; }

        public AuthenticationFilter()
        {
            RequireAdminRole = false;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User user = (User)HttpContext.Current.Session["LoggedUser"];

            if (user == null)
            {
                filterContext.Result = new RedirectResult("/Home/IndexPage");
                return;
            }

            if (RequireAdminRole == true && user.IsAdmin != true)
            {
                filterContext.Result = new RedirectResult("/Home/IndexPage");
                return;
            }
        }
    }
}