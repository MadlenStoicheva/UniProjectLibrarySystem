using LibrarySystem.BaseService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.RelationServices.Domain.Book
{
    public class Book : BaseEntity
    {
        public string ImgURL { get; set; }
        public string NumberISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string PublishingHouse { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Availability { get; set; }
    }
}
