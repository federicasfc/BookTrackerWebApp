using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.List.ReadingList
{
    public class ReadingListDetail
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string BookTitle { get; set; }

        public DateTimeOffset AddedUtc { get; set; }
    }
}
