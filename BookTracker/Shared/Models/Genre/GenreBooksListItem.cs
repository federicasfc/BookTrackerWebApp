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
        
        public int Id { get; set; }

        public string Name { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }
        
        //public List<BookListItem> Books {get; set;} 
    
    }
        }