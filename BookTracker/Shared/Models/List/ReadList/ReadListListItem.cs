using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.List.ReadList
{
    public class ReadListListItem
    {
        public int Id { get; set; }

        public string BookTitle { get; set; }

        public bool IsFinished { get; set; }

    }
}
