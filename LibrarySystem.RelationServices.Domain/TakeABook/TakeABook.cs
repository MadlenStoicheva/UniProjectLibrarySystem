using LibrarySystem.BaseService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.RelationServices.Domain.TakeABook
{
   public class TakeABook : BaseEntity
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime DateTaken { get; set; }
        public DateTime DateForReturn { get; set; }
        public DateTime DateReturn { get; set; }
        //public List<SelectListItem> Books { get; set; }
        // public List<SelectListItem> Readers { get; set; }
        public virtual Book.Book Book { get; set; }
        public virtual User.User User { get; set; }

    }
}
