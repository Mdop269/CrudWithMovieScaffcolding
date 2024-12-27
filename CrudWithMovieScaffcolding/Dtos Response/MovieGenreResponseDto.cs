namespace CrudWithMovieScaffcolding.Dtos_Response
{
    public class MovieGenreResponseDto
    {
        public int? MovId { get; set; }

        public int? GenId { get; set; }

        public int GenresId { get; set; }

        public MovieResponseDto Movie { get; set; }
        public GenreResponseDto Genre { get; set; }
    }
}
