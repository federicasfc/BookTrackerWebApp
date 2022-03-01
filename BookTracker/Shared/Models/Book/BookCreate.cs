﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.Book
{
    public class BookCreate
    {
        [Required]

        public string Title { get; set; }

        [Required]

        public string Author { get; set; }

        public string Description { get; set; }

        //Potentially: public List<GenreListItem> Genres {get; set;} on User end, want to be able to check applicable genres and apply to book-- 
    }
}
