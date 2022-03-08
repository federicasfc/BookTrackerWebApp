using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.List.ReadList
{
    public class ReadListDetail
    {
        public int Id { get; set; }

        public string BookTitle { get; set; }

        public DateTimeOffset AddedUtc { get; set; }

        public DateTimeOffset AcquiredUtc { get; set; }

        public DateTimeOffset StartedUtc { get; set; }

        public Format Format { get; set; }

        public string HowAcquired { get; set; }

        public bool IsFinished { get; set; }

        public int Review { get; set; }

    }
}
