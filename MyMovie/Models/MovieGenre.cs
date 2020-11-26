using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovie.Models
{
    public class MovieGenre
    {
        [Key]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [Key]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
