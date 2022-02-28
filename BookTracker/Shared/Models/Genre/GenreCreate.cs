using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.Genre
{
    public class GenreCreate
    {
        [Required]
        public string Name { get; set; }

    }
}
