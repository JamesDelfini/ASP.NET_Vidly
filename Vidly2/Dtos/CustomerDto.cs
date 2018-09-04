using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vidly2.Models;

namespace Vidly2.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customer's name.")]
        [StringLength(255)]
        public string Name { get; set; }

        [Column(TypeName = "Date")]
        // [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        public bool isSubscribeNewsLetter { get; set; }

        public byte MembershipTypeId { get; set; }
    }
}