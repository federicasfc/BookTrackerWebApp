using BookTracker.Shared.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.Genre
{
   public class GenreBooksListItem
    {
        public ICollection<BookListItem> Books { get; set; }
    }
}
