using LibrarySystem.BaseService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.RelationServices.Domain.User
{
    public class User : BaseEntity
    {
        //public User(string email, string validationCode)
        //{
        //    Email = email;
        //    ValidationCode = validationCode;
        //}

        //add image url property
        public string ImgURL { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string ValidationCode { get; set; }
    }
}
