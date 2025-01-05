namespace MoviesApp.Application.Movies
{
    public class CreateUpdateMovieDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public decimal Rate { get; set; }
    }
}
