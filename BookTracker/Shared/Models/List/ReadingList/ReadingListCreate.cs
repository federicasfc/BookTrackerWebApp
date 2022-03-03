using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.List.ReadingList
{
    public class ReadingListCreate
    {
        

        [Required]  //keeping required for now, but might be wrong

        public int BookId { get; set; }


        [Required]

        public DateTimeOffset AddedUtc { get; set; }
    }
}
