using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TeamsIdentity.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required, Display(Name = "First Name")]
        [StringLength(40)]
        public string FirstName { get; set; }
        
        [Required, Display(Name = "LastName")]
        [StringLength(40)]
        public string LastName { get; set; }
        
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public override string PhoneNumber { get; set; }

        [Required]
        public override string UserName { get; set; }
    }
}