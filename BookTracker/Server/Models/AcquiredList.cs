using System;
using System.ComponentModel.DataAnnotations;

namespace BookTracker.Server.Models
{
    public enum Format { Physical = 1, Ebook, Audiobook }
    public class AcquiredList : ReadingList

    {
        [Required]
        public DateTimeOffset AcquiredUtc { get; set; }

        [Required]

        public Format Format { get; set; }

        public string HowAcquired { get; set; }



    }
}
