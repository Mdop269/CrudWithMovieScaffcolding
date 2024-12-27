using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CrudWithMovieScaffcolding.Models;

public partial class Movie
{
    public int MovId { get; set; }

    public string? MovTitle { get; set; }

    public int? MovYear { get; set; }

    public int? MovTime { get; set; }

    public string? MovLang { get; set; }

    public DateOnly? MovRlDt { get; set; }

    public string? MovRelCountry { get; set; }

    public virtual ICollection<MovieCast> MovieCasts { get; set; } = new List<MovieCast>();

    public virtual ICollection<MovieDirection> MovieDirections { get; set; } = new List<MovieDirection>();

    public virtual ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}

