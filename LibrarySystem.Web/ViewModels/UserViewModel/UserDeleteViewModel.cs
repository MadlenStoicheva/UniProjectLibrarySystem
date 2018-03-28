using LibrarySystem.BaseService.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystemProject.Models.UserViewModel
{
    public class UserDeleteViewModel : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string imgURL { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Email { get; set; }
        public bool isAdmin { get; set; }
        //public bool IsEmailConfirmed { get; set; }
        //public string ValidationCode { get; set; }
    }
}