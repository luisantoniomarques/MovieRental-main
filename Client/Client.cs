using MovieRental.Rental;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MovieRental.Client
{
    // Class representing the customer who makes the rental.
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
