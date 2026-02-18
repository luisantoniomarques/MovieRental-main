using MovieRental.Data;

namespace MovieRental.Client
{
    public class ClientFeatures : IClientFeatures
    {
        // A class containing the methods representing the client.Modeled after Rental and Movie.
        private readonly MovieRentalDbContext _movieRentalDb;

        public ClientFeatures (MovieRentalDbContext movieRentalDb)
        {
            _movieRentalDb = movieRentalDb;
        }

        public Client Save(Client client)
        {
            _movieRentalDb.Clients.Add(client);
            _movieRentalDb.SaveChanges();
            return client;
        }

        public List<Client> GetAll()
        { 
            return _movieRentalDb.Clients.ToList();
        }
    }
}
