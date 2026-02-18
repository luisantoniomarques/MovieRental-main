using MovieRental.Data;
using System.Runtime.InteropServices.Marshalling;

namespace MovieRental.Movie
{
	public class MovieFeatures : IMovieFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;

		public MovieFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}
		
		public Movie Save(Movie movie)
		{
			_movieRentalDb.Movies.Add(movie);
			_movieRentalDb.SaveChanges();
			return movie;
		}

        // TODO: tell us what is wrong in this method? Forget about the async, what other concerns do you have?
        // Como tem tendencia a ser uma operação demorada, o ideal seria usar async/await para não bloquear a thread principal.
		// Além disso, o método não tem tratamento de erros, o que pode levar a falhas silenciosas ou crashes se algo der errado durante a consulta ao banco de dados.
		// Também seria interessante considerar a paginação dos resultados para evitar carregar uma grande quantidade de dados na memória de uma só vez, caso haja muitos filmes no banco de dados.
        public List<Movie> GetAll()
		{
			return _movieRentalDb.Movies.ToList();
		}


	}
}
