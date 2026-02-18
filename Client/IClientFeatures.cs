namespace MovieRental.Client
{
    public interface IClientFeatures
    {
        Client Save(Client client);
        List<Client> GetAll();
    }
}
