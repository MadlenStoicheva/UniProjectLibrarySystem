﻿
using LibrarySystem.RelationServices.Domain.Book;
using LibrarySystem.RelationServices.Domain.TakeABook;
using LibrarySystem.RelationServices.Domain.User;
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
