using BookTracker.Shared.Models.Genre;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.Book
{
    public class BookUpdate
    {
        [Required]
        public int Id { get; set; }


        [Required]

        public string Title { get; set; }

        [Required]

        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        //may want to add logic in update service method that makes these not required; waiting to see how things play out with the front end first


        public List<GenreListItem> Genres { get; set; } = new();
    }
}
