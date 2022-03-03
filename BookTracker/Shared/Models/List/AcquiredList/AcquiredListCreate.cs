using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.List.AcquiredList
{
   // public enum Format { Physical = 1, Ebook, Audiobook } now in EnumHolder might have to move it elsewhere, maybe to List namespace so that all the models have access to it
    public class AcquiredListCreate
    {
        //Adding first two for functionality that allows books to be added to AcquiredList without first being on the ReadingList--inheritance may have an issue with this??
        [Required]  //keeping required for now, but might be wrong

        public int BookId { get; set; }


        [Required]

        public DateTimeOffset AddedUtc { get; set; }


        [Required]
        public DateTimeOffset AcquiredUtc { get; set; } //will need to figure out a way to set this manually on client side- because it shouldn't ncessarily be .Now

        [Required]

        public Format Format { get; set; }

        public string HowAcquired { get; set; }
    }
}
