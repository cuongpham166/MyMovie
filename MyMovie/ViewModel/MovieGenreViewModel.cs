using MyMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovie.ViewModel
{
    public class MovieGenreViewModel
    {
        public IEnumerable<Movie> Movie { get; set; }
        public IEnumerable<Genre> Genre { get; set; }
        
    }
}
