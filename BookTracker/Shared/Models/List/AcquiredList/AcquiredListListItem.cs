using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.List.AcquiredList
{
    public class AcquiredListListItem
    {
        public int Id { get; set; }

        public string BookTitle { get; set; }

        public Format Format { get; set; }
    }
}
