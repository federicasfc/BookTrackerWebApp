using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.List.ReadList
{
    public class ReadListCreate
    {
        [Required]  //keeping required for now, but might be wrong

        public int BookId { get; set; }


        [Required]

        public DateTimeOffset AddedUtc { get; set; }


        [Required]
        public DateTimeOffset AcquiredUtc { get; set; } = DateTimeOffset.Now; 

        [Required]

        public Format Format { get; set; }

        public string HowAcquired { get; set; }

        [Required]

        public DateTimeOffset StartedUtc { get; set; } = DateTimeOffset.Now;

        [Required]

        public bool IsFinished { get; set; }

        [Range(1, 5, ErrorMessage = "Number must be between 1 and 5")]

        public int Review { get; set; }
   


                
         
    }
}
