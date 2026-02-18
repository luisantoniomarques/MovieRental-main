using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.PaymentProviders;
using MovieRental.Movie;

namespace MovieRental.Rental
{
	public class RentalFeatures : IRentalFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;

		public RentalFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}

		//TODO: make me async :(
		public Rental Save(Rental rental)
		{
			_movieRentalDb.Rentals.Add(rental);
            _movieRentalDb.SaveChanges();
			return rental;
		}

        // Para tornar o método Save assíncrono, basta adicionar a palavra-chave async e usar os métodos assíncronos do Entity Framework Core, como AddAsync e SaveChangesAsync.
        // Async torna o método assiincrono, e await é usado para esperar a conclusão das operações assíncronas, permitindo que o thread seja liberado para outras tarefas enquanto aguarda a resposta do banco de dados.

        public async Task<Rental> SaveAsync(Rental rental)		
		{
			await _movieRentalDb.Rentals.AddAsync(rental);
			await _movieRentalDb.SaveChangesAsync();
			return rental;
		}

		//TODO: finish this method and create an endpoint for it
		public IEnumerable<Rental> GetRentalsByCustomerName(string customerName)
		{
			List<Rental> rentals = _movieRentalDb.Rentals
				.Include(r => r.Client)
				.Where(r => r.Client.Name == customerName)
				.ToList();
            
			return rentals;
        }

        // Method for processing payment
        public bool ProcessPayment(string customerName)
		{
            List<Movie.Movie> mv = new List<Movie.Movie>();
            List<Client.Client> cl = new List<Client.Client>();
            IEnumerable<Rental> listRental = GetRentalsByCustomerName(customerName);

			foreach (var rental in listRental)
			{
				try
				{
					int rentalCost = rental.DaysRented * rental.Movie.Price;
					rental.Cost = rentalCost;
					rental.PaymentProcessed = true;
					bool mvv = mv.Remove(rental.Movie);
					bool cll = cl.Remove(rental.Client);
					if (!mvv || !cll) break;
				} 
				catch (Exception ex)
				{
					Console.WriteLine($"Error processing payment for rental {rental.Id}: {ex.Message}");
					return false;
                }

            }
             
			return true;
        }
    }
}
