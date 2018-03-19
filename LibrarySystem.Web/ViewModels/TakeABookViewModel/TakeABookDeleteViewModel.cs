using LibrarySystem.BaseService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystemProject.Models.TakeABookViewModel
{
    public class TakeABookDeleteViewModel : BaseEntity
    {
        //public int Id { get; set; }
        public List<SelectListItem> Books { get; set; }
        public List<SelectListItem> Users { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime dateTaken { get; set; }
        public DateTime dateForReturn { get; set; }
        public DateTime dateReturn { get; set; }
    }
}