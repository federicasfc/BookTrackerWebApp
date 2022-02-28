using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookTracker.Server.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]

        public string Title { get; set; }

        [Required]

        public string Author { get; set; }

        public string Description { get; set; }

        public ICollection<Genre> Genres { get; set; }


    }
}
