namespace CrudWithMovieScaffcolding.Dtos_Response
{
    public class MovieDirectionResponseDto
    {
        public int? MovId { get; set; }

        public int? DirId { get; set; }

        public int DirectionId { get; set; }

        public MovieResponseDto Movie { get; set; }
        public DirectorResponseDto Director { get; set; }
    }
}
