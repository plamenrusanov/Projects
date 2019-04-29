using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taxi.Domain.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserName = $"{FirstName} {LastName}";
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [ForeignKey("DriverId")]
        public string DriverId { get; set; }
        public virtual Driver Driver { get; set; }

        [ForeignKey("CustomerId")]
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
