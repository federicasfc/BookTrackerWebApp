using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Shared.Models.List.AcquiredList
{
    public class AcquiredListEdit
    {
        

        public int Id { get; set; }

        

        public string BookTitle { get; set; }

        
        public DateTimeOffset AddedUtc { get; set; }

        [Required]
        public DateTimeOffset AcquiredUtc { get; set; }

        [Required]

        public Format Format { get; set; }
        
        //Leaving as required for now, but again, may change in the future
        [Required]
        public string HowAcquired { get; set; }
    }
}

/*
 * 
 * Same structure as update method
 * public Task<bool> AddToAcquiredList(int id, AcquiredListEdit model) 
 * {
 *      //null check
 *      
 *      entityToEdit = await _context.ReadingLists.FindAsync(id)
 *      
 *      //null checks/matching id to model.Id check
 *      
 *      entityToEdit.AcquiredUtc = model.AcquiredUtc
 *      entityToEdit.Format = model.Format (this is an enum, so may throw separate issues)
 *      entityToEdit.HowAcquired = model.HowAcquired
 *      
 *      return await _context.SaveChangesAsync() == 1;
 * 
 * }
 * 
 */
