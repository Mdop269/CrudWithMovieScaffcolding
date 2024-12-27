using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CrudWithMovieScaffcolding.Models;

public partial class Genre
{
    public int GenId { get; set; }

    public string? GenTitle { get; set; }

    [JsonIgnore]
    public virtual ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
}
