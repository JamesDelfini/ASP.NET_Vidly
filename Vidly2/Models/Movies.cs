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
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Column(TypeName = "Date")]
        public DateTime ReleaseDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? DatePublish { get; set; }
        public int Stock { get; set; }
    }
}