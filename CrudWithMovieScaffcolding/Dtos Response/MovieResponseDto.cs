namespace CrudWithMovieScaffcolding.Dtos_Response
{
    public class MovieResponseDto
    {
        public int MovId { get; set; }

        public string? MovTitle { get; set; }

        public int? MovYear { get; set; }

        public int? MovTime { get; set; }

        public string? MovLang { get; set; }

        public DateOnly? MovRlDt { get; set; }

        public string? MovRelCountry { get; set; }
    }
}
