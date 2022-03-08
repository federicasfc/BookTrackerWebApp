using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.List.AcquiredList
{
    public class AcquiredListDetail
    {
        public int Id { get; set; }

        public string BookTitle { get; set; }

        public DateTimeOffset AddedUtc { get; set; }

        public DateTimeOffset AcquiredUtc { get; set; }

        public Format Format { get; set; }

        public string HowAcquired { get; set; }
    }
}
