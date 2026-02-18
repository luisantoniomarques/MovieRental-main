using MovieRental.Client;
using MovieRental.PaymentProviders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRental.Rental
{
	public class Rental
	{
		[Key]
		public int Id { get; set; }
		public int DaysRented { get; set; }
		public Movie.Movie? Movie { get; set; }
		
		[Required]
		public Client.Client Client { get; set; }
		
		public int Cost { get; set; }

		public bool PaymentProcessed { get; set; } // Put this rental as processed

        [ForeignKey("Movie")]
		public int MovieId { get; set; }

		[ForeignKey("Client")]
		public int ClientId { get; set; } 

		// public PayProviders PaymentMethod { get; set; }

		// TODO: we should have a table for the customers
		public Client.Client Name { get; set; }
	}
}
