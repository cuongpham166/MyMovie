using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovie.Models
{
    public class Movie
    {
        [ScaffoldColumn(false)]
        [DisplayName("ID")]
        [Key]
        public int Id { get; set; }

        [DisplayName("Image")]
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Overview { get; set; }

        [Required]
        [Range(0, 10.0)]
        public double Score { get; set; }

        [Column(TypeName = "decimal(3, 1)")]
        [Required]
        public decimal Rating { get; set; }


        [DataType(DataType.Date)]
        [Column(TypeName ="Date")]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime ReleaseDate { get; set; }

        [ScaffoldColumn(false)]
        public int GenreId { get; set; }

        public List<MovieGenre> MovieGenres { get; set; }

    }
}
