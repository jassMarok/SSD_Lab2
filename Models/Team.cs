using System;
using System.ComponentModel.DataAnnotations;

namespace TeamsIdentity.Models
{
    public class Team
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(16)]
        public string TeamName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EstablishedDate { get; set; }
    }
}