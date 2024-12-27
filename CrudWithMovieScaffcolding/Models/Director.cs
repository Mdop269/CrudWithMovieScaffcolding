using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CrudWithMovieScaffcolding.Models;

public partial class Director
{
    public int DirId { get; set; }

    public string? DirFname { get; set; }

    public string? DirLname { get; set; }

    [JsonIgnore]
    public virtual ICollection<MovieDirection> MovieDirections { get; set; } = new List<MovieDirection>();
}
