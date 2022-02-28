using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.Genre
{
    public class GenreEdit
    {
        [Required]
        public int Id { get; set; }

        [Required]

        public string Name { get; set; }
    }
}
