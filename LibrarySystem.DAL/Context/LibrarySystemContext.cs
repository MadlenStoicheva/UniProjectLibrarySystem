using LibrarySystem.RelationalServices.Domain.Book;
using LibrarySystem.RelationalServices.Domain.TakeABook;
using LibrarySystem.RelationalServices.Domain.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.DAL.Context
{
    class LibrarySystemContext : DbContext
    {
        public LibrarySystemContext() : base("LibrarySystemData")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<TakeABook> TakeABooks { get; set; }

    }
}
