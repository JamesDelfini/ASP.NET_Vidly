using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    public class Movies
    {
        public int Id { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "Genre Type")]
        [Required]
        public byte GenreId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        [Column(TypeName = "Date")]
        public DateTime ReleaseDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? DatePublish { get; set; }

        [Required]
        public int Stock { get; set; }
    }
}