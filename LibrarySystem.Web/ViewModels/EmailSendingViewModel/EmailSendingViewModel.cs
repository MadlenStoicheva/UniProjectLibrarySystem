using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.ComponentModel.DataAnnotations;
>>>>>>> 3ca160fd98a2913ce9311163666637142816615e
using System.Linq;
using System.Web;

namespace LibrarySystem.Web.ViewModels.EmailSendingViewModel
{
    public class EmailSendingViewModel
    {
<<<<<<< HEAD
        public string Email { get; set; }
        public string Name { get; set; }
=======
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
>>>>>>> 3ca160fd98a2913ce9311163666637142816615e
        public string Comment { get; set; }
    }
}