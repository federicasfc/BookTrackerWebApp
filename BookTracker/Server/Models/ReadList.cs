using System;
using System.ComponentModel.DataAnnotations;

namespace BookTracker.Server.Models
{
    public class ReadList : AcquiredList
    {
        [Required]

        public DateTimeOffset  StartedUtc { get; set; }

        [Required]

        public bool IsFinished { get; set; }

        [Range(1,5, ErrorMessage = "Number must be between 1 and 5")]

        public int Review { get; set; }


    }
}
