﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystemProject.Models.UserViewModel
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Please input URL! It is required!")]
        //[System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please input a title! It is required!")]
        // [RegularExpression(@"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$", ErrorMessage = "The URL address is not valid")]
        public string imgURL { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Minimum length is 3 and maximum length is 80")]
        [RegularExpression(@"^([A-z.-_]+)$", ErrorMessage = "Username can consist of letters, dashes and underscores only!")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(80, ErrorMessage = "Must be between 5 and 80 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Minimum length is 3 and maximum length is 80")]
        [RegularExpression(@"^([A-z-]+)$", ErrorMessage = "First Name can consist of letters and dashes only!")]
        public string firstName { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Minimum length is 3 and maximum length is 80")]
        [RegularExpression(@"^([A-z-]+)$", ErrorMessage = "Last Name can consist of letters and dashes only!")]
        public string lastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }

        public bool isAdmin { get; set; }

    }

}