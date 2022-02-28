using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.Genre
{
    public class GenreDetail
    {
       
        public int Id { get; set; }


        public string Name { get; set; }

        //poteeentiially add list of books here, but eh- could get long and as long as GetBookByGenre exists, not super necessary
    }
}
