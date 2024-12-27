using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CrudWithMovieScaffcolding.Models;

public partial class Actor
{
    public int ActId { get; set; }

    public string? ActFname { get; set; }

    public string? ActLname { get; set; }

    public string? ActGender { get; set; }

    [JsonIgnore]
    public virtual ICollection<MovieCast> MovieCasts { get; set; } = new List<MovieCast>();
}
