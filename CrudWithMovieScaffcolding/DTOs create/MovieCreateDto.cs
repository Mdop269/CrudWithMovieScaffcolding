namespace CrudWithMovieScaffcolding.DTOs_create
{
    public class MovieCreateDto
    {
        public string? MovTitle { get; set; }

        public int? MovYear { get; set; }

        public int? MovTime { get; set; }

        public string? MovLang { get; set; }

        public DateOnly? MovRlDt { get; set; }

        public string? MovRelCountry { get; set; }
    }
}
