using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vidly2.Models;

namespace Vidly2.ViewModels
{
    public class MoviesFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public int? Id { get; set; }

        [Display(Name = "Genre Type")]
        [Required]
        public byte? GenreId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        [Column(TypeName = "Date")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Range(1, 20)]
        public int? Stock { get; set; }

        public string TitlePage
        {
            get
            {
                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }

        public MoviesFormViewModel()
        {
            Id = 0;
        }

        public MoviesFormViewModel(Movies movies)
        {
            Id = movies.Id;
            Title = movies.Title;
            ReleaseDate = movies.ReleaseDate;
            Stock = movies.Stock;
            GenreId = movies.GenreId;
        }
    }
}