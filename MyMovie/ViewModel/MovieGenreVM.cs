using MyMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovie.ViewModel
{
    public class MovieGenreVM
    {
        public Movie Movie { get; set; }
        public List <int> selectedGenres { get; set; }
    }
}
