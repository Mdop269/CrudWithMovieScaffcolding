namespace CrudWithMovieScaffcolding.Dtos_Response
{
    public class MovieCastResponseDto
    {
        public int? CastId { get; set; }
        public int? MovId { get; set; }
        public int? ActId { get; set; }

        public string Role { get; set; }

        public MovieResponseDto Movie { get; set; }
        public ActorResponseDto Actor { get; set; }
    }
}
