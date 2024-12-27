using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CrudWithMovieScaffcolding.Models;

public partial class Reviewer
{
    public int RevId { get; set; }

    public string? RevName { get; set; }

    [JsonIgnore]

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
