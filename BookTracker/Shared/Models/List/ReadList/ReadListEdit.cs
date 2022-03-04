using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.List.ReadList
{
    public class ReadListEdit
    {

        [Required]

        public int Id { get; set; }

        //Not sure why I added these two here, but not deleting just in case I remember:

        //[Required]  //keeping required for now, but might be wrong

        //public int BookId { get; set; }


        //[Required]

        //public DateTimeOffset AddedUtc { get; set; }


        [Required]
        public DateTimeOffset AcquiredUtc { get; set; } //will need to figure out a way to set this manually on client side- because it shouldn't ncessarily be .Now

        [Required]

        public Format Format { get; set; }

        public string HowAcquired { get; set; }


        [Required]

        public DateTimeOffset StartedUtc { get; set; }

        [Required]

        public bool IsFinished { get; set; }

        [Range(1, 5, ErrorMessage = "Number must be between 1 and 5")]

        public int Review { get; set; }
    }
}
