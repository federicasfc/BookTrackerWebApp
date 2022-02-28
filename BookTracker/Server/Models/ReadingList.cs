using System;
using System.ComponentModel.DataAnnotations;

namespace BookTracker.Server.Models
{
    public class ReadingList
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        //FK

        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        [Required]

        public DateTimeOffset AddedUtc { get; set; }


    }
}
