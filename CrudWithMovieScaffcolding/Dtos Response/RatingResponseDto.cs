namespace CrudWithMovieScaffcolding.Dtos_Response
{
    public class RatingResponseDto
    {
        public int? MovId { get; set; }

        public int? RevId { get; set; }

        public decimal? RevStars { get; set; }

        public int? NumORatings { get; set; }

        public int RatingId { get; set; }
    }
}
