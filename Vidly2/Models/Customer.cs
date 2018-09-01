using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name="Date of Birth")]
        [Column(TypeName="Date")]
        public DateTime? Birthdate { get; set; }

        [Display(Name="Subscribe to Newsletter")]
        public bool isSubscribeNewsLetter { get; set; }

        public MembershipType MembershipType { get; set; }
        
        [Display(Name="Membership Type")]
        public byte MembershipTypeId { get; set; }
    }
}